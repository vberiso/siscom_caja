using Firebase.Database;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Humanizer;
using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Model;
using SOAPAP.Services;
using SOAPAP.UI;
using SOAPAP.UI.Descuentos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI
{
    public partial class Cobro : Form
    {
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        List<CollectConcepts> lCollectConcepts = null;
        List<String> Seleccion = new List<string>();
        //List<Model.Debt> tmpFiltros = null;
        List<TypeService> TypeServices = new List<TypeService>();
        List<Model.Debt> tmpFiltros = null;
        List<Model.OrderSale> orders = null;
        Search.Type TypeSearchSelect = Search.Type.Ninguno;
        querys q = new querys();
        bool anual;
        bool prepaid ;
        bool orderSale;
        decimal porcentaje = 0;
        public readonly FirebaseClient firebase = new FirebaseClient("https://siscom-notifications.firebaseio.com/");
        CashBoxAccess.Access accessParam = CashBoxAccess.Access.Cobro;

        public Cobro()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
            if (Variables.LoginModel.RolName.ToList().Find(x => x.Contains("GenerarOrden")) != null)
            {
                accessParam = CashBoxAccess.Access.GenerarOrden;
                gbxCampaign.Visible = false;
                txtTotal.Visible = false;
                lblTxtPagar.Visible = false;
                btnCobrar.Visible = false;
            }
            if (Variables.Configuration.StateOperation == 0)
            {
                btnCobrar.Visible = false;
            }
            if (!String.IsNullOrWhiteSpace(Variables.cuenta))
            {
                txtCuenta.Text = Variables.cuenta;
                ObtenerInformacion();
            }
            anual = false;
            prepaid = false;       
        }


        #region Events
        public void cobroagua_Load(object sender, EventArgs e)
        {
            pnlDatos.Width = Convert.ToInt32(Math.Truncate(this.Width * 0.4));
            pnlHeaderLeft.Width = pnlDatos.Width;
            dgvConceptosCobro.Columns["Importe"].DefaultCellStyle.Format = "c2";
            dgvConceptosCobro.Columns["Importe"].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvConceptosCobro.Columns["Importe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvConceptosCobro.Columns["Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Variables.cuenta = string.Empty;
            tableLayoutPanel3.RowStyles[0] = new RowStyle(SizeType.Percent, 0);
            tableLayoutPanel3.RowStyles[tableLayoutPanel3.RowCount - 1] = new RowStyle(SizeType.Percent, 0);
            tableLayoutPanel3.Size = new Size(344, 350);
        }

        private void pbBuscar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtCuenta.Text))
            {
                //if (Variables.Agreement != null && txtCuenta.Text.Trim() == Variables.Agreement.Account)
                //    return;
               
                ObtenerInformacion();
            }
            else
            {
                mensaje = new MessageBoxForm("Ingresar Datos", "Ingrese un número de cuenta o folio correcto", TypeIcon.Icon.Warning);
                result = mensaje.ShowDialog();
            }
        }

        //Painting Icons
        private void dgvConceptosCobro_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvConceptosCobro.Columns[e.ColumnIndex].Name == "detail" && e.RowIndex >= 0)
            {
                var image = Properties.Resources.detalle;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                e.Graphics.DrawImage(image, x, y, 15, 15);
                e.Handled = true;
            }
        }

        private void dgvConceptosCobro_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CollectConcepts temp = (CollectConcepts)((System.Windows.Forms.BindingSource)((System.Windows.Forms.DataGridView)sender).DataSource).Current;

                mensaje = new ModalDetalleCaja("Detalle Conceptos", "", TypeIcon.Icon.Warning,this.dgvConceptosCobro.Rows[e.RowIndex].Cells["Id"].Value.ToString(), TypeSearchSelect, txtCuenta.Text.Trim(), temp.Type);
                result = mensaje.ShowDialog();
            }
        }

        private void dgvConceptosCobro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             string _cuenta = txtCuenta.Text.Trim();
                txtCuenta.Text = _cuenta;
            if (e.RowIndex >= 0)
            {
                
                decimal subTotal = 0;
                var source = new BindingSource();

                if (e.ColumnIndex == dgvConceptosCobro.Columns["detail"].Index && e.RowIndex >= 0)
                {
                    CollectConcepts temp = (CollectConcepts)((System.Windows.Forms.BindingSource)((System.Windows.Forms.DataGridView)sender).DataSource).Current;                    
                    
                    mensaje = new ModalDetalleCaja("Detalle Conceptos", "", TypeIcon.Icon.Warning, this.dgvConceptosCobro.Rows[e.RowIndex].Cells["Id"].Value.ToString(), TypeSearchSelect, txtCuenta.Text.Trim(), temp.Type);
                    result = mensaje.ShowDialog();
                }
                if (this.dgvConceptosCobro.Columns[e.ColumnIndex].Name == "Select")
                {
                    DataGridViewCheckBoxCell cell = this.dgvConceptosCobro.CurrentCell as DataGridViewCheckBoxCell;
                    bool seleccionado = false;

                    if (!Convert.ToBoolean(this.dgvConceptosCobro.Rows[e.RowIndex].Cells["Select"].Value.ToString()))
                    {                       
                        lCollectConcepts.ForEach(x =>
                        {
                            if (!seleccionado)
                                x.Select = true;
                            if (x.Id == Convert.ToInt32(this.dgvConceptosCobro.Rows[e.RowIndex].Cells["Id"].Value.ToString()))
                                seleccionado = true;
                            
                        });
                    }
                    else
                    {                        
                        lCollectConcepts.ForEach(x =>
                        {
                            if (x.Id == Convert.ToInt32(this.dgvConceptosCobro.Rows[e.RowIndex].Cells["Id"].Value.ToString()))
                                seleccionado = true;
                            if (seleccionado)
                                x.Select = false;
                        });
                    }
                    if (_cuenta.Length > 2 && char.IsLetter(Convert.ToChar(_cuenta.Substring(0, 1))) && _cuenta.Contains("-"))
                    {
                        List<Model.OrderSale> ordersList = new List<Model.OrderSale>();
                        ordersList.Add(Variables.OrderSale);
                        orders = (from d in ordersList
                                      where lCollectConcepts.Where(x => x.Select == true).Select(x => x.Id).ToList().Contains(d.Id)
                                      select d).ToList();

                        subTotal = (orders.Count > 0 ? Variables.OrderSale.Amount - Variables.OrderSale.OnAccount : 0);
                        lblSubtotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", subTotal);
                        CalculateAmounts(orders);
                        btnCobrar.Enabled = Variables.OrderSale.OrderSaleDetails.Count > 0 & subTotal == 0 ? false : true;
                    }
                    else
                    {
                        tmpFiltros = (from d in Variables.Agreement.Debts
                                      where lCollectConcepts.Where(x => x.Select == true).Select(x => x.Id).ToList().Contains(d.Id)
                                      select d).ToList();
                        subTotal = tmpFiltros != null ? tmpFiltros.Sum(x => (x.Amount - x.OnAccount)) : 0;
                        lblSubtotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", subTotal);
                        CalculateAmounts(tmpFiltros);
                        btnCobrar.Enabled = Variables.Agreement.Debts.Count > 0 & subTotal == 0 ? false : true;
                    }

                    source.DataSource = lCollectConcepts ?? new List<CollectConcepts>();
                    dgvConceptosCobro.DataSource = source;
                    
                    
                   
                  
                }
            }
        }

        private void pbInformacion_Click(object sender, EventArgs e)
        {
            switch (TypeSearchSelect)
            {
                case Search.Type.Cuenta:
                    if (Variables.Agreement != null)
                    {
                        mensaje = new ModalFicha("Detalle Conceptos", "", TypeIcon.Icon.Warning, Variables.Agreement.Account, Search.Type.Cuenta);
                        result = mensaje.ShowDialog();
                    }
                    break;
                case Search.Type.Folio:
                    if (Variables.OrderSale != null)
                    {
                        mensaje = new ModalFicha("Detalle Conceptos", "", TypeIcon.Icon.Warning, Variables.OrderSale.Folio, Search.Type.Folio);
                        result = mensaje.ShowDialog();
                    }
                    break;
            }
                   
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            SendPayment();
        }

        private void txtCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Enter)) return;
            if (e.KeyChar == '-') return;
            if (char.IsLetter(e.KeyChar)) return;
            e.Handled = true;

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (!String.IsNullOrWhiteSpace(txtCuenta.Text))
                {
                    //if (Variables.Agreement != null && txtCuenta.Text.Trim() == Variables.Agreement.Account)
                    //    return;
                    ObtenerInformacion();
                }
                else
                {
                    mensaje = new MessageBoxForm("Ingresar Datos", "Ingrese un número de cuenta o folio correcto", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
            }
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            //if (Char.IsDigit(e.KeyChar)) return;
            //if (Char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Enter)) return;
            //e.Handled = true;

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (!String.IsNullOrWhiteSpace(txtCuenta.Text))
                {
                    if (txtCuenta.Text.Contains("-"))
                    {
                        SendPayment();
                    }
                    else if (Convert.ToDecimal(txtCuenta.Text.Trim()) != 0)
                    {
                        SendPayment();
                    }
                    else
                    {
                        mensaje = new MessageBoxForm("Ingresar Datos", "Ingrese un monto correcto", TypeIcon.Icon.Warning);
                        result = mensaje.ShowDialog();
                    }
                }
                else
                {
                    mensaje = new MessageBoxForm("Ingresar Datos", "Ingrese un monto correcto", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
            }
        }

        private void cmbTipos_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Seleccion = new List<string>();
            if (cmbTipos.SelectedValue.ToString() == "0")
            {
                TypeServices.ForEach(x => {
                    Seleccion.Add(x.Id);
                });
            }           
            else
                Seleccion.Add(cmbTipos.SelectedValue.ToString());
            SeleccionarDeuda(Search.Type.Cuenta);
        }
        #endregion

        #region PrivateMethod
        private void LimpiaDatos()
        {
            var source = new BindingSource();
            Variables.Agreement = null;
            Variables.OrderSale = null;
            lCollectConcepts = null;
            tmpFiltros = null;
            source.DataSource = lCollectConcepts ?? new List<CollectConcepts>();
            dgvConceptosCobro.DataSource = source;
            lblContibuyente.Text = String.Empty;
            lblRFC.Text = String.Empty;
            lblDireccion.Text = String.Empty;
            lblSubtotal.Text = String.Empty;
            lblIva.Text = String.Empty;
            lblRedondeo.Text = String.Empty;
            lblTotal.Text = String.Empty;
            txtTotal.Text = String.Empty;
            Seleccion = new List<string>();
            TypeServices = null;
            cmbTipos.DataSource = TypeServices ?? new List<TypeService>();
            cmbTipos.Text = String.Empty;
            TypeSearchSelect = Search.Type.Ninguno;
            anual = false;
            prepaid = false;
            orderSale = false;
            lblBaseGravable.Text = string.Empty;
            lblMetrosConstruidos.Text = string.Empty;
            lblMetrosTerreno.Text = string.Empty;
            lblUtimoAvaluo.Text = string.Empty;
            lblTipoPredio.Text = string.Empty;
        }

        private async void ObtenerInformacion()
        {
            LimpiaDatos();           
                if (txtCuenta.Text.Trim().Length != 0)
            {
                string _cuenta = txtCuenta.Text.Trim();
                txtCuenta.Text = _cuenta;

                if (_cuenta.Length > 2 && char.IsLetter(Convert.ToChar(_cuenta.Substring(0, 1))) && _cuenta.Contains("-"))
                {
                    gbxCampaign.Visible = false;
                    orderSale = true;
                    cmbTipos.Enabled = false;
                    TypeSearchSelect = Search.Type.Folio;
                    loading = new Loading();
                    loading.Show(this);
                    var resultOrder = await Requests.SendURIAsync(string.Format("/api/OrderSales/Folio/{0}", _cuenta), HttpMethod.Get, Variables.LoginModel.Token);
                    loading.Close();
                    if (resultOrder.Contains("error"))
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultOrder).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        Variables.OrderSale= JsonConvert.DeserializeObject<Model.OrderSale>(resultOrder);
                        if (!string.IsNullOrWhiteSpace(resultOrder))
                        {

                            //Cliente
                            if (Variables.OrderSale.TaxUser != null)
                            {
                                lblContibuyente.Text = !String.IsNullOrWhiteSpace(Variables.OrderSale.TaxUser.Name) ? Variables.OrderSale.TaxUser.Name : "Sin Dato";
                                lblRFC.Text = Variables.OrderSale.TaxUser.RFC;

                                //Dirección
                                if (Variables.OrderSale.TaxUser.TaxAddresses != null && Variables.OrderSale.TaxUser.TaxAddresses.Count > 0)
                                {
                                    lblDireccion.Text = Variables.OrderSale.TaxUser.TaxAddresses.First().Street
                                                      + " NO." + Variables.OrderSale.TaxUser.TaxAddresses.First().Outdoor
                                                      + " INT." + Variables.OrderSale.TaxUser.TaxAddresses.First().Indoor
                                                      + ", COL." + Variables.OrderSale.TaxUser.TaxAddresses.First().Suburb
                                                      + ". " + Variables.OrderSale.TaxUser.TaxAddresses.First().Town
                                                      + ". " + Variables.OrderSale.TaxUser.TaxAddresses.First().State;
                                }
                                else
                                {
                                    lblDireccion.Text = "Sin Dato";
                                    mensaje = new MessageBoxForm("Problemas", "No hay datos de dirección", TypeIcon.Icon.Warning);
                                    result = mensaje.ShowDialog();
                                }
                            }
                            else
                            {
                                mensaje = new MessageBoxForm("Problemas", "No hay datos de cliente", TypeIcon.Icon.Warning);
                                result = mensaje.ShowDialog();
                            }

                            if (Variables.OrderSale.ExpirationDate.Date >= DateTime.UtcNow.Date)
                            {
                                if (Variables.OrderSale.OrderSaleDetails != null && Variables.OrderSale.OrderSaleDetails.Count > 0)
                                {
                                    SeleccionarDeuda(Search.Type.Folio);
                                    descuentosToolStripMenuItem.Enabled = true;
                                }
                                else
                                {
                                    descuentosToolStripMenuItem.Enabled = false;
                                    mensaje = new MessageBoxForm("Error", "La orden no cuenta con detalle.", TypeIcon.Icon.Cancel);
                                    result = mensaje.ShowDialog();
                                }
                            }
                            else
                            {
                                descuentosToolStripMenuItem.Enabled = false;
                                mensaje = new MessageBoxForm("Problemas", "La orden ya ha expirado. Es necesario solicitar una nueva.", TypeIcon.Icon.Warning);
                                result = mensaje.ShowDialog();
                            }

                        }
                        else
                        {
                            mensaje = new MessageBoxForm("Sin dato", "No se encontraron datos para este folio", TypeIcon.Icon.Warning);
                            result = mensaje.ShowDialog();
                        }
                    }
                }
                else
                {
                    if (Variables.Configuration.DiscountCampaigns.Count > 0)
                    {
                        gbxCampaign.Visible = accessParam == CashBoxAccess.Access.GenerarOrden? false: true;
                        //if (!gbxCampaign.Visible)
                        //{
                        //    tableLayoutPanel3.RowStyles[0] = new RowStyle(SizeType.Percent, 0);
                        //}
                        tableLayoutPanel3.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                        lblTitleCampaign.Text = Variables.Configuration.DiscountCampaigns.First().Name;
                    }
                    orderSale = false;
                    TypeSearchSelect = Search.Type.Cuenta;
                    loading = new Loading();
                    loading.Show(this);
                    var resultAgreement = await Requests.SendURIAsync(string.Format("/api/Agreements/AgreementByAccount/Cash/{0}", _cuenta), HttpMethod.Get, Variables.LoginModel.Token);
                    loading.Close();
                    if (resultAgreement.Contains("error"))
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultAgreement).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        Variables.Agreement = JsonConvert.DeserializeObject<Model.Agreement>(resultAgreement);
                        if (Variables.Configuration.IsMunicipal)
                        {
                            tableLayoutPanel3.Size = new Size(344, 478);
                            tableLayoutPanel3.RowStyles[tableLayoutPanel3.RowCount - 1] = new RowStyle(SizeType.Percent, 31.03f);
                            lblBaseGravable.Text = Variables.Agreement.AgreementDetails.FirstOrDefault().TaxableBase.ToString();
                            lblMetrosConstruidos.Text = Variables.Agreement.AgreementDetails.FirstOrDefault().Built.ToString();
                            lblMetrosTerreno.Text = Variables.Agreement.AgreementDetails.FirstOrDefault().Ground.ToString();
                            lblUtimoAvaluo.Text = Variables.Agreement.AgreementDetails.FirstOrDefault().LastUpdate.ToString("dd/MM/yyyy");
                        }
                        if (!string.IsNullOrWhiteSpace(resultAgreement))
                        {                           
                            //Cliente
                            if (Variables.Agreement.Clients != null && Variables.Agreement.Clients.Count > 0)
                            {
                                lblContibuyente.Text = Variables.Agreement.Clients.First().Name
                                                     + ' ' + Variables.Agreement.Clients.First().LastName
                                                     + ' ' + Variables.Agreement.Clients.First().SecondLastName;
                                lblRFC.Text = Variables.Agreement.Clients.First().RFC;
                            }
                            else
                            {
                                lblContibuyente.Text = "Sin Dato";
                                mensaje = new MessageBoxForm("Problemas", "No hay datos de cliente", TypeIcon.Icon.Warning);
                                result = mensaje.ShowDialog();
                            }

                            //Dirección
                            if (Variables.Agreement.Addresses != null && Variables.Agreement.Addresses.Count > 0)
                            {
                                lblDireccion.Text = Variables.Agreement.Addresses.First().Street
                                                  + " NO." + Variables.Agreement.Addresses.First().Outdoor
                                                  + " INT." + Variables.Agreement.Addresses.First().Indoor
                                                  + ", COL." + Variables.Agreement.Addresses.First().Suburbs.Name
                                                  + ". " + Variables.Agreement.Addresses.First().Suburbs.Towns.Name
                                                  + ". " + Variables.Agreement.Addresses.First().Suburbs.Towns.States.Name;
                            }
                            else
                            {
                                lblDireccion.Text = "Sin Dato";
                                mensaje = new MessageBoxForm("Problemas", "No hay datos de dirección", TypeIcon.Icon.Warning);
                                result = mensaje.ShowDialog();
                            }


                            //Avalúo
                            if (Variables.Configuration.IsMunicipal)
                            {
                                if (Variables.Agreement.AgreementDetails != null && Variables.Agreement.AgreementDetails.Count>0)
                                {
                                    int _idetail = Variables.Agreement.AgreementDetails.Max(x => x.Id);
                                    var _detail = Variables.Agreement.AgreementDetails.SingleOrDefault(x => x.Id == _idetail);
                                    var _fechaActualizacion = _detail.LastUpdate;
                                    if (_detail.LastUpdate< DateTime.UtcNow.ToLocalTime().AddYears(-4))
                                    {
                                        mensaje = new MessageBoxForm("Actualización", "Debe Actualizar Avalúo", TypeIcon.Icon.Warning);
                                        result = mensaje.ShowDialog();
                                    }
                                }
                            }


                            if (Variables.Agreement.TypeStateServiceId == 1)
                            {
                                //Deuda
                                loading = new Loading();
                                loading.Show(this);
                                var resultDeb = await Requests.SendURIAsync(string.Format("/api/Debts/{0}", Variables.Agreement.Id), HttpMethod.Get, Variables.LoginModel.Token);
                                loading.Close();
                                if (resultDeb.Contains("error"))
                                {
                                    mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultDeb).error, TypeIcon.Icon.Cancel);
                                    result = mensaje.ShowDialog();
                                }
                                else
                                {
                                    Variables.Agreement.Debts = JsonConvert.DeserializeObject<List<Model.Debt>>(resultDeb);
                                    if (Variables.Agreement.Debts != null && Variables.Agreement.Debts.Count > 0)
                                    {                                        
                                        ObtenerSeleccion();
                                        descuentosToolStripMenuItem.Enabled = true;
                                    }
                                    else
                                    {
                                        descuentosToolStripMenuItem.Enabled = false;
                                       
                                        if (!Variables.Configuration.IsMunicipal)
                                        {
                                            mensaje = new MessageBoxForm("Sin Deuda", "La cuenta proporcionada no tiene adeudo, puede ingresar un pago anticipado", TypeIcon.Icon.Success, true);
                                            result = mensaje.ShowDialog();
                                            if (result == DialogResult.OK)
                                            {
                                                Form Anticipo = new Anticipo();
                                                if (Anticipo.ShowDialog() == DialogResult.OK)
                                                    AddPrepaid();
                                                //else
                                                //anua
                                            }
                                        }
                                        else
                                        {
                                            mensaje = new MessageBoxForm(Variables.titleprincipal, "La cuenta proporcionada no tiene adeudo", TypeIcon.Icon.Success);
                                            result = mensaje.ShowDialog();
                                        }

                                    }
                                    
                                }
                            }
                            else
                            {
                                descuentosToolStripMenuItem.Enabled = false;
                                mensaje = new MessageBoxForm("Error", "La cuenta no se encuentra activa", TypeIcon.Icon.Cancel);
                                result = mensaje.ShowDialog();
                            }
                        }
                        else
                        {
                            descuentosToolStripMenuItem.Enabled = false;
                            mensaje = new MessageBoxForm("Sin dato", "No se encontraron datos para este número de cuenta", TypeIcon.Icon.Warning);
                            result = mensaje.ShowDialog();
                        }
                    }
                }
            }
            else
            {
                mensaje = new MessageBoxForm("Error", "Debe ingresar un valor de búsqueda", TypeIcon.Icon.Warning);
                result = mensaje.ShowDialog();
            }
        }

        private void ObtenerSeleccion()
        {
            TypeServices = Variables.Agreement.Debts.ToList()
                                           .GroupBy(x => new { x.Type, x.DescriptionType })
                                           .Select(g => new TypeService
                                           {
                                               Id = g.Key.Type,
                                               Description = g.Key.DescriptionType
                                           }).ToList();

            TypeServices.Add(new TypeService()
            {
                Id = "0",
                Description = "Todos"
            });

            TypeServices.ForEach( x=> {
                Seleccion.Add(x.Id);
            });

            cmbTipos.ValueMember = "Id";
            cmbTipos.DisplayMember = "Description";
            cmbTipos.DataSource = TypeServices;
            cmbTipos.SelectedIndex = cmbTipos.FindString("Todos");
            cmbTipos.Enabled = true;
            SeleccionarDeuda(Search.Type.Cuenta);
        }

        private void SeleccionarDeuda(Search.Type type)
        {
            var source = new BindingSource();
            decimal subTotal = 0;

            switch (type)
            {
                case Search.Type.Cuenta:
                    if (Variables.Agreement != null && Variables.Agreement.Debts != null && Variables.Agreement.Debts.Count > 0)
                    {
                        tmpFiltros = Variables.Agreement.Debts.Where(x => Seleccion.Contains(x.Type)).ToList();

                        lCollectConcepts = tmpFiltros
                                            .Select(d => new CollectConcepts
                                            {
                                                Id = d.Id,
                                                Select = true,
                                                Type = d.DescriptionType,
                                                Description = d.Type == "TIP02" ? d.FromDate.Date.ToString("dd-MM-yyyy") : d.FromDate.Date.ToString("dd-MM-yyyy") + " al " + d.UntilDate.Date.ToString("dd-MM-yyyy"),
                                                Amount = (d.Amount - d.OnAccount)
                                            }).ToList();
                        subTotal = lCollectConcepts != null ? lCollectConcepts.Sum(x => x.Amount) : 0;
                        lblSubtotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", subTotal);
                        CalculateAmounts(tmpFiltros);                       
                    }
                    break;

                case Search.Type.Folio:
                    if (Variables.OrderSale.OrderSaleDetails != null && Variables.OrderSale.OrderSaleDetails.Count > 0)
                    {
                        lCollectConcepts = new List<CollectConcepts>();
                        //List<Model.OrderSale> ordersList = new List<Model.OrderSale>();
                        orders = new List<Model.OrderSale>();
                        orders.Add(Variables.OrderSale);

                        lCollectConcepts.Add(new CollectConcepts
                                               {
                                                   Id = Variables.OrderSale.Id,
                                                   Select = true,
                                                   Type = Variables.OrderSale.DescriptionType,
                                                   Description = Variables.OrderSale.DateOrder.Date.ToString("dd-MM-yyyy"),
                                                   Amount = Variables.OrderSale.Amount - Variables.OrderSale.OnAccount
                                               });
                        subTotal = lCollectConcepts != null ? lCollectConcepts.Sum(x => x.Amount) : 0;
                        lblSubtotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", subTotal);
                        CalculateAmounts(orders);
                    }
                    break;
            }

            source.DataSource = lCollectConcepts ?? new List<CollectConcepts>();
            dgvConceptosCobro.DataSource = source;
        }

        private void CalculateAmounts(List<Model.Debt> pDebts)
        {
            decimal IVA = 0;
            decimal subTotal = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblSubtotal.Text, @"[^\d.]", "")));
            decimal redondeo = 0;
            decimal total = 0;
            //Get IVA
            pDebts.ToList().ForEach(x =>
            {
                //IVA = IVA + x.DebtDetails.Where(t => t.HaveTax == true).Sum(y => (((y.Amount - y.OnAccount) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100));
                x.DebtDetails.ToList().ForEach(y =>
                {
                    if(y.HaveTax)
                        IVA = IVA + (Math.Round(((y.Amount - y.OnAccount) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100 , 2));
                });
            });
            subTotal = subTotal + IVA;
            redondeo = Math.Ceiling(subTotal) - subTotal;
            //Quitar para efectuar redondeo
            redondeo = 0;
            total = subTotal + redondeo;
            if(total > 0)
            {
                btnCobrar.Enabled = true;
            }

            lblIva.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", IVA);
            lblRedondeo.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", redondeo);
            lblTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", total);
            txtTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", total).Replace("$", "").Replace(",","");

        }

        private void CalculateAmounts(List<Model.OrderSale> pOrderSale)
        {
            decimal IVA = 0;
            decimal subTotal = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblSubtotal.Text, @"[^\d.]", "")));
            decimal redondeo = 0;
            decimal total = 0;
            //Get IVA
            pOrderSale.ToList().ForEach(x =>
            {
                //IVA = IVA + x.OrderSaleDetails.Where(t => t.HaveTax == true).Sum(y => (((y.Amount - y.OnAccount) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100));
                x.OrderSaleDetails.ToList().ForEach(y =>
                {
                    if (y.HaveTax)
                        IVA = IVA + (Math.Round(((y.Amount - y.OnAccount) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100, 2));
                });
            });
            subTotal = subTotal + IVA;
            redondeo = Math.Ceiling(subTotal) - subTotal;
            //Quitar para efectuar redondeo
            redondeo = 0;
            total = subTotal + redondeo;

            if (total > 0)
            {
                btnCobrar.Enabled = true;
            }

            lblIva.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", IVA);
            lblRedondeo.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", redondeo);
            lblTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", total);
            txtTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", total).Replace("$", "").Replace(",", "");

        }

        private void Calculate(List<Model.Debt> debts)
        {
            List<Model.Debt> tempDebt = new List<Model.Debt>(debts);
           
            decimal PorPagar = 0;
            decimal TotalIva = 0;
            decimal TotalSinIva = 0;
            decimal tmp = 0;
            decimal IVA = 0;
            decimal onAccount = 0;
            decimal totalDisponible = Convert.ToDecimal(txtTotal.Text);
            bool amout = false;

            tempDebt.ForEach(x =>
            {
                PorPagar = (x.Amount - x.OnAccount) + (x.DebtDetails.Any(z => z.HaveTax) ? Convert.ToDecimal(x.DebtDetails.Where(z => z.HaveTax).Sum(z => ((z.Amount - z.OnAccount) * 16) / 100).ToString("#.##")) : 0);
                if (PorPagar <= totalDisponible && totalDisponible != 0)
                {
        
                }
                else if (totalDisponible != 0 && totalDisponible > 0)
                {
                    TotalIva = x.DebtDetails.Where(z => z.HaveTax).Sum(z => ((z.Amount - z.OnAccount) * 16) / 100).ToString("#.##") == "" ? 0 : Convert.ToDecimal(x.DebtDetails.Where(z => z.HaveTax).Sum(z => ((z.Amount - z.OnAccount) * 16) / 100).ToString("#.##"));
                    TotalSinIva = x.DebtDetails.Sum(z => (z.Amount - z.OnAccount));
                    tmp = TotalIva + TotalSinIva;
                    //PaidUp = PaidUp + x.OnAccount;
                    bool notValid = true;
                    do
                    {

                        x.DebtDetails.ToList().ForEach(y =>
                        {
                            if (notValid == false)
                            {
                                decimal ajuste = 0;
                                if (totalDisponible < onAccount)
                                {
                                    ajuste = totalDisponible - onAccount;
                                    y.OnAccount = y.OnAccount + ajuste;
                                    y.OnPayment = y.OnPayment + ajuste;
                                }
                                else if (totalDisponible > onAccount)
                                {
                                    ajuste = onAccount - totalDisponible;
                                    y.OnAccount = y.OnAccount - ajuste;
                                    y.OnPayment = y.OnPayment - ajuste;
                                }


                                notValid = true;
                                onAccount = Convert.ToDecimal((x.DebtDetails.Sum(z => (z.OnPayment)) + IVA).ToString("#.##"));
                                //x.OnAccount = x.OnAccount + onAccount - IVA;
                                amout = true;
                                return;
                            }
                            else
                            {
                                if (totalDisponible > onAccount)
                                {
                                    var calc = Convert.ToDecimal((((y.Amount - y.OnAccount) / tmp) * totalDisponible).ToString("#.##"));
                                    //y.OnAccount = y.OnAccount + calc;
                                    IVA = (TotalIva > 0) ? Convert.ToDecimal(((TotalIva / tmp) * totalDisponible).ToString("#.##")) : 0;
                                    //y.OnPayment = calc;
                                    onAccount += calc;
                                    //if (y.HaveTax)
                                    //{
                                    //    y.Tax = Convert.ToDecimal((Convert.ToDecimal((y.OnPayment * Convert.ToDecimal(Variables.Configuration.IVA)).ToString("#.##")) / 100).ToString("#.##"));
                                    //}
                                }
                            }
                        });
                        onAccount += IVA;
                        //onAccount = Convert.ToDecimal((x.DebtDetails.Sum(z => (z.OnPayment)) + IVA).ToString("#.##"));
                        notValid = false;

                    } while (totalDisponible > onAccount || onAccount > totalDisponible);

                    //if (!amout)
                    //{
                    //    x.OnAccount = x.OnAccount + onAccount - IVA;
                    //}

                    lblIva.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", IVA);
                    lblSubtotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", (totalDisponible - IVA));
                    lblTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", (totalDisponible));
                }
            });

        }
        
        private void SendPayment()
        {
            decimal total = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblTotal.Text, @"[^\d.]", "")));
            decimal amount = Convert.ToDecimal(txtTotal.Text);
            if (orderSale)
            {
                txtTotal.Text = txtTotal.Text.Trim();
                if (string.IsNullOrWhiteSpace(txtTotal.Text))
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto capturado esta no es valido", TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                    txtTotal.Text = lblTotal.Text.Replace("$", "").Replace(",", "");
                }
                else if (Convert.ToDecimal(lblTotal.Text.Replace("$","").Replace(",","")) > Convert.ToDecimal(txtTotal.Text))
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto capturado esta no es valido para el cobro del producto favor de verificar", TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                    txtTotal.Text = lblTotal.Text.Replace("$", "").Replace(",", "");
                }
                else
                {
                    PaymentModal();
                }
            }
            else
            {
                
                if (string.IsNullOrWhiteSpace(txtTotal.Text))
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto capturado esta no es valido", TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                }
                else
                {
                    if (amount < total)
                    {
                        if (lblIva.Text != "$0.00")
                        {
                            mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto de cobro sera proporcional al iva", TypeIcon.Icon.Warning);
                            mensaje.ShowDialog();
                            PaymentModal();
                        }
                        else
                        {
                            PaymentModal();
                        }
                    }
                    else
                    {
                        PaymentModal();
                    }
                }
            }
           
        }

        public void PaymentModal()
        {
            decimal paidUp = 0;
            paidUp = Convert.ToDecimal(txtTotal.Text);
            Form mensaje = new MessageBoxForm("Operación de cobro", "¿Desea continuar con el cobro?", TypeIcon.Icon.Info, true);
            result = mensaje.ShowDialog();
            if (result == DialogResult.OK)
            {
                decimal amount = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblSubtotal.Text, @"[^\d.]", "")));
                decimal tax = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblIva.Text, @"[^\d.]", "")));
                decimal rounding = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblRedondeo.Text, @"[^\d.]", "")));
                decimal total = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblTotal.Text, @"[^\d.]", "")));
                string Padron = lblContibuyente.Text;

                ModalDetalleCobro Cobro = new ModalDetalleCobro(amount, tax, rounding, paidUp, total, tmpFiltros, Padron, porcentaje, anual, prepaid);

                Cobro.ShowDialog(this);
                ObtenerInformacion();
            }
        }
        #endregion

        #region public region

        public async void CalculateAnual()
        {
            string value = string.Empty;
            decimal amoutn = 0;
            var source = new BindingSource();
            if (Variables.Agreement.TypeIntakeId == 1)
            {

                //var result = await q.GETDiscountValidator("/api/DiscountValidator/DiscountAnnual/" + Variables.idagrement + "/" + Variables.Configuration.IsMunicipal + "");
                loading = new Loading();
                loading.Show(this);
                var resultado = await Requests.SendURIAsync("/api/DiscountValidator/DiscountAnnual/" + Variables.Agreement.Id + "/" + Variables.Configuration.IsMunicipal + "", HttpMethod.Get, Variables.LoginModel.Token);
                loading.Close();
                if (resultado != null)
                {
                    if (resultado.Contains("error"))
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultado).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        List<Adeudos> m = JsonConvert.DeserializeObject<List<Adeudos>>(resultado);
                        if(!m.Any(x => x.name_concept.Contains("Descuento")))
                        {
                            amoutn = m.Sum(s => s.amount);
                        }
                        else
                        {
                            amoutn = m.Where(x => x.name_concept.Contains("DESCUENTO")).Sum(s => s.amount);
                        }

                        lCollectConcepts = new List<CollectConcepts>();
                        lCollectConcepts = m.Select(s => new CollectConcepts
                        {
                            Id = Convert.ToInt32(s.conde_concept),
                            Amount = s.amount,
                            Description = "Pago Anual",
                            Select = true,
                            Type = s.name_concept
                        }).ToList();

                        porcentaje = Convert.ToDecimal(m.FirstOrDefault().id_discount);
                        lblIva.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", 0);
                        lblRedondeo.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", 0);
                        lblTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", amoutn);
                        lblSubtotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", amoutn);
                        txtTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", amoutn).Replace("$", "").Replace(",", "");
                        btnCobrar.Enabled = true;
                        anual = true;

                        source.DataSource = lCollectConcepts ?? new List<CollectConcepts>();
                        dgvConceptosCobro.DataSource = source;
                    }
                }
                else
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                }
                loading.Close();
            }
            else
            {
                mensaje = new MessageBoxForm(Variables.titleprincipal, "Las características del contrato no cuenta con lo necesario para asignar un descuento: [Tipo de Toma no es Habitacional]", TypeIcon.Icon.Cancel);
                mensaje.ShowDialog();
                loading.Close();
            }
        }

        public void AddPrepaid()
        {
            prepaid = true;
            btnCobrar.Enabled = true;
            lblIva.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", 0);
            lblRedondeo.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", 0);
            lblTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", 0);
            lblSubtotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", 0);
            txtTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", 0).Replace("$", "").Replace(",", "");
        }
        #endregion

        private void txtTotal_KeyUp(object sender, KeyEventArgs e)
        {
            if (prepaid)
            {
                var source = new BindingSource();
                lCollectConcepts = new List<CollectConcepts>();
                if (!string.IsNullOrWhiteSpace(txtTotal.Text))
                {
                    lCollectConcepts.Add(new CollectConcepts
                    {
                        Id = 1,
                        Select = true,
                        Type = "Pago de Servicios Anticipado",
                        Description = "Anticipo",
                        Amount = Convert.ToDecimal(txtTotal.Text)
                    });
                    source.DataSource = lCollectConcepts ?? new List<CollectConcepts>();
                    dgvConceptosCobro.DataSource = source;
                    var total = Convert.ToDecimal(txtTotal.Text);
                    lblIva.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", 0);
                    lblRedondeo.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", 0);
                    lblTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", total);
                    lblSubtotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", total);
                    //txtTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", lblTotal.Text).Replace("$", "").Replace(",", "");
                }
                else
                {
                    source.DataSource = lCollectConcepts ?? new List<CollectConcepts>();
                    dgvConceptosCobro.DataSource = source;
                }
                
            }
            if(txtTotal.Text.Trim() == "")
            {
                txtTotal.Text = "";
            }
        }

        private void DescuentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal amount = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblSubtotal.Text, @"[^\d.]", "")));
            if (orderSale)
            {
                RequestDiscount discount = new RequestDiscount(amount, orders);
                discount.ShowDialog(this);
            }
            else
            {
                RequestDiscount discount = new RequestDiscount(amount, tmpFiltros);
                discount.ShowDialog(this);
            }

        }

        private void EstatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetalleDescuentos detalle = new DetalleDescuentos(false);
            detalle.ShowDialog(this);
        }

        private CancelProduct obtenerDetalleCancelacion(object sender)
        {
            CollectConcepts temp = (CollectConcepts)((System.Windows.Forms.BindingSource)((System.Windows.Forms.DataGridView)sender).DataSource).Current;
            CancelProduct cp = new CancelProduct()
            {
                Account = txtCuenta.Text.Trim(),
                //TipoCobro = temp.Type,
                //Id = temp.Id
            };
            return cp;
        }
        //Button Campaign Recharges
        private async void BtnAcept_Click(object sender, EventArgs e)
        {
            
            
        }

        private async void BtnAcept_Click_1(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            var resultCampaign = await Requests.SendURIAsync(string.Format("/api/Discounts/{0}", Variables.Agreement.Id), HttpMethod.Post, Variables.LoginModel.Token);
            if (resultCampaign.Contains("error"))
            {
                try
                {
                    mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultCampaign).error, TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    loading.Close();
                }
                catch (Exception)
                {
                    mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    loading.Close();
                }
            }
            else
            {
                mensaje = new MessageBoxForm(Variables.titleprincipal, "La condonación de recargos se ha realizado con exito", TypeIcon.Icon.Success);
                result = mensaje.ShowDialog();
                loading.Close();
                ObtenerInformacion();
            }
        }
    }

    public partial class CollectConcepts {
        public int Id { get; set; }
        public bool Select { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }

    public partial class TypeService
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }
}
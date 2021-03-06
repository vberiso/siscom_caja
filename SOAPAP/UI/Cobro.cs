using Firebase.Database;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Humanizer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOAPAP.Enums;
using SOAPAP.Model;
using SOAPAP.Services;
using SOAPAP.UI;
using SOAPAP.UI.Condonations;
using SOAPAP.UI.Descuentos;
using SOAPAP.UI.FacturacionAnticipada;
using SOAPAP.UI.Promos;
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
        List<GroupType> groupTypes = new List<GroupType>();
        List<TypeService> TypeServices = new List<TypeService>();
        List<Model.Debt> tmpFiltros = null;
        bool ApplyMSI = false;
        decimal descuento = 0;
      
        List<Model.OrderSale> orders = null;
        Search.Type TypeSearchSelect = Search.Type.Ninguno;
        private List<int> debtApplyDiscount = new List<int>();
        private List<int> AllDebtAnnual = new List<int>();
        
        querys q = new querys();
        bool anual;
        bool prepaid;
        bool orderSale;
        decimal porcentaje = 0;
        public readonly FirebaseClient firebase = new FirebaseClient("https://siscom-notifications.firebaseio.com/");
        CashBoxAccess.Access accessParam = CashBoxAccess.Access.Cobro;
        string CorreoCliente = "";
        public Cobro()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
            if (Variables.LoginModel.RolName.ToList().Find(x => x.Contains("GenerarOrden")) != null)
            {
                accessParam = CashBoxAccess.Access.GenerarOrden;                
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

            if (Variables.LoginModel.RolName[0] == "Supervisor" || Variables.LoginModel.RolName[0] == "Supervisor")
            {
                ajusteDeReciboToolStripMenuItem.Visible = true;
            }
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

                mensaje = new ModalDetalleCaja("Detalle Conceptos", "", TypeIcon.Icon.Warning, this.dgvConceptosCobro.Rows[e.RowIndex].Cells["Id"].Value.ToString(), TypeSearchSelect, txtCuenta.Text.Trim(), temp.Type);
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
                    if (Variables.Agreement.TypeStateServiceId == 1)
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
                    else
                    {
                        string msg = "En necesario cubrir el monto total del adeudo.";
                        mensaje = new MessageBoxForm("El servicio esta cortado", msg, TypeIcon.Icon.Warning);
                        result = mensaje.ShowDialog();
                        //Siempre se seleccionan todos los campos.
                        lCollectConcepts.ForEach(x =>
                        {
                            x.Select = true;
                        });
                        //Se calculan los montos
                        tmpFiltros = (from d in Variables.Agreement.Debts
                                      where lCollectConcepts.Where(x => x.Select == true).Select(x => x.Id).ToList().Contains(d.Id)
                                      select d).ToList();
                        subTotal = tmpFiltros != null ? tmpFiltros.Sum(x => (x.Amount - x.OnAccount)) : 0;
                        lblSubtotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", subTotal);
                        CalculateAmounts(tmpFiltros);
                        btnCobrar.Enabled = Variables.Agreement.Debts.Count > 0 & subTotal == 0 ? false : true;

                        source.DataSource = lCollectConcepts ?? new List<CollectConcepts>();
                        dgvConceptosCobro.DataSource = source;
                    }

                    //Verificacion de promocion por COVID
                    if (Variables.Configuration.RecargosDescCovid != null && Variables.Configuration.RecargosDescCovid.Count > 0)
                        VerificaDescuentoPorCOVID(Variables.Agreement.Id, tmpFiltros);

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
                        mensaje = new ModalFicha("Detalle Conceptos", "", TypeIcon.Icon.Warning, Variables.Agreement.Id.ToString(), Search.Type.Cuenta);
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
            if (result == DialogResult.Yes)
            {
                ObtenerInformacion();
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
                TypeServices.ForEach(x =>
                {
                    Seleccion.Add(x.Id);
                });
            }
            else
            {
                Seleccion.AddRange(groupTypes.Where(t => t.Id == int.Parse(cmbTipos.SelectedValue.ToString())).Select(x => x.Id.ToString()).ToList() );
            }
            //else
                //Seleccion.Add(cmbTipos.SelectedValue.ToString());
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
            lblDireccionF.Text = String.Empty;
            lblDireccionN.Text = String.Empty;
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
            lblTipoPredioEncabezado.Text = string.Empty;
            lblDescuentoT.Text = "";
            lblDescuentoT.Visible = false;
            lblVulnerable.Text = "";
            lblVulnerableInfo.Text = "";
            layoutAnual.Visible = false;
        }

        private async void ObtenerInformacion()
        {
            LimpiaDatos();
            btnCobrar.Enabled = true;
            if (txtCuenta.Text.Trim().Length != 0)
            {
                string _cuenta = txtCuenta.Text.Trim();
                txtCuenta.Text = _cuenta;
               
                if (_cuenta.Length > 2 && char.IsLetter(Convert.ToChar(_cuenta.Substring(0, 1))) && _cuenta.Contains("-"))
                {                    
                    orderSale = true;
                    cmbTipos.Enabled = false;
                    TypeSearchSelect = Search.Type.Folio;
                    loading = new Loading();
                    loading.Show(this);
                    var resultOrder = await Requests.SendURIAsync(string.Format("/api/OrderSales/Folio/{0}", _cuenta), HttpMethod.Get, Variables.LoginModel.Token);
                    loading.Close();
                    if (resultOrder.Contains("error\\"))
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultOrder).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        Variables.OrderSale = JsonConvert.DeserializeObject<Model.OrderSale>(resultOrder);
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

                                    lblDireccionF.Text = Variables.OrderSale.TaxUser.TaxAddresses.First().Street
                                                      + " NO." + Variables.OrderSale.TaxUser.TaxAddresses.First().Outdoor
                                                      + " INT." + Variables.OrderSale.TaxUser.TaxAddresses.First().Indoor
                                                      + ", COL." + Variables.OrderSale.TaxUser.TaxAddresses.First().Suburb
                                                      + ". " + Variables.OrderSale.TaxUser.TaxAddresses.First().Town
                                                      + ". " + Variables.OrderSale.TaxUser.TaxAddresses.First().State;


                                }
                                else
                                {
                                    lblDireccionF.Text = "Sin Dato";
                                    lblDireccionN.Text = "Sin Dato";
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

                            //Si es una Infraccion y tiene descuentos
                            if (_cuenta.Substring(0, 1).Contains("I"))
                            {
                                if (Variables.OrderSale.OrderSaleDiscounts.Count > 0 && Variables.OrderSale.OrderSaleDiscounts.FirstOrDefault().DiscountPercentage > 0)
                                {
                                    var concept = Variables.OrderSale.OrderSaleDetails.First();
                                    mensaje = new MessageBoxForm("Aviso", $"Se ha aplicado un descuento del: " + Variables.OrderSale.OrderSaleDiscounts.FirstOrDefault().DiscountPercentage + "%" +
                                        " únicamente al concepto: "+ concept.NameConcept +" con importe original de  " + string.Format(new CultureInfo("es-MX"), "{0:C2}", concept.UnitPrice * concept.Quantity)

                                        , TypeIcon.Icon.Info);
                                    result = mensaje.ShowDialog();
                                }
                            }

                            //Si hay descuento a productos por contingenca COVID
                            if (Variables.Configuration.ProductosDescCOVID != null)
                            {
                                if (Variables.OrderSale.Observation != null && Variables.OrderSale.Observation.Contains("COVID"))
                                {
                                    mensaje = new MessageBoxForm("Orden sujeta a descuento", $"Esta orden ya tiene el descuento por contingencia COVID", TypeIcon.Icon.Success);
                                    result = mensaje.ShowDialog();
                                }
                                else   //Se ejecuta el proceso para  calcular el descuento
                                {
                                    var lstConceptosDescCOVID = Variables.Configuration.ProductosDescCOVID.TextColumn.Substring(0, Variables.Configuration.ProductosDescCOVID.TextColumn.IndexOf(" => ")).Split('|').ToList();
                                    var lstIdsToApliDesc = Variables.OrderSale.OrderSaleDetails.Where(x => lstConceptosDescCOVID.Contains(x.CodeConcept)).Select(x => x.Id).ToList();
                                    if (lstIdsToApliDesc.Count > 0)
                                    {
                                        decimal totalAdeudo = Variables.OrderSale.OrderSaleDetails.Where(x => lstIdsToApliDesc.Contains(x.Id)).Sum(x => x.Amount - x.OnAccount);
                                        decimal totalDescuento = Decimal.Round((Variables.Configuration.ProductosDescCOVID.NumberColumn / 100) * totalAdeudo, 2);

                                        mensaje = new MessageBoxForm("Orden sujeta a descuento", $"Por motivo de la contingencia la orden cuenta con un descuento de ${totalDescuento}. \n SE APLICARA EL DESCUENTO.", TypeIcon.Icon.Info);
                                        result = mensaje.ShowDialog();
                                        if (result == DialogResult.OK)
                                        {                                            
                                            string resDesc = await aplicaDescuentoProductosCOVID();
                                            if (resDesc.Contains("ok"))
                                            {
                                                ObtenerInformacion();
                                                return;
                                            }                                                
                                            else
                                                return;                                            
                                        }

                                    }
                                }                                
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
                    orderSale = false;
                    TypeSearchSelect = Search.Type.Cuenta;
                    loading = new Loading();
                    loading.Show(this);
                    var resultAgreement = await Requests.SendURIAsync(string.Format("/api/Agreements/AgreementByAccount/Cash/{0}/{1}", _cuenta, Variables.cuentaID == -1 ? "" : Variables.cuentaID.ToString()), HttpMethod.Get, Variables.LoginModel.Token);
                    Variables.cuentaID = -1;
                    loading.Close();
                    if (resultAgreement.Contains("error\\"))
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultAgreement).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        var oLAgreement = JsonConvert.DeserializeObject<List<Model.Agreement>>(resultAgreement);

                        //List < Model.Agreement > s =  JsonConvert.DeserializeObject<List<Model.Agreement>>(resultAgreement);
                        int count = oLAgreement.Count();
                        if (count <= 1)
                        {

                            Variables.Agreement = count == 1 ? oLAgreement[0] : JsonConvert.DeserializeObject<Model.Agreement>("{}");
                        }
                        else
                        {

                            var OCobroBuscarCuentaSelectOne = new CobroBuscarCuentaSelectOne(oLAgreement);
                            OCobroBuscarCuentaSelectOne.ShowDialog();
                            Variables.Agreement = OCobroBuscarCuentaSelectOne.getAgreement();

                        }

                        var isVulnerable = Variables.Agreement.AgreementDiscounts?.Where(x => x.IsActive).FirstOrDefault();
                        if (isVulnerable != null) {
                            lblVulnerable.Visible = true;
                            lblVulnerableInfo.Visible = true;
                            lblVulnerableInfo.Text = "Población vulnerable";
                            lblVulnerable.Text = isVulnerable.Discount?.Name;
                        }

                        if (Variables.Configuration.IsMunicipal && Variables.Agreement.AgreementDetails.Count() > 0)
                        {
                            tableLayoutPanel3.Size = new Size(344, 478);
                            tableLayoutPanel3.RowStyles[tableLayoutPanel3.RowCount - 1] = new RowStyle(SizeType.Percent, 31.03f);
                            lblBaseGravable.Text = Variables.Agreement.AgreementDetails.FirstOrDefault().TaxableBase.ToString();
                            lblMetrosConstruidos.Text = Variables.Agreement.AgreementDetails.FirstOrDefault().Built.ToString();
                            lblMetrosTerreno.Text = Variables.Agreement.AgreementDetails.FirstOrDefault().Ground.ToString();
                            lblUtimoAvaluo.Text = Variables.Agreement.AgreementDetails.FirstOrDefault().LastUpdate.ToString("dd/MM/yyyy");
                            lblTipoPredio.Text = Variables.Agreement.TypeIntake != null ? Variables.Agreement.TypeIntake.Name : "";
                        }
                        if (!Variables.Configuration.IsMunicipal && Variables.Agreement.TypeIntake != null)
                        {
                            lblEti_TipoPredioEncabezado.Visible = lblTipoPredioEncabezado.Visible = true;
                            lblTipoPredioEncabezado.Text = Variables.Agreement.TypeIntake != null ? Variables.Agreement.TypeIntake.Name : "";
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

                                if (!Variables.Configuration.IsMunicipal)
                                {
                                    registrarPeriodosToolStripMenuItem.Visible = true;
                                }
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

                                var address = Variables.Agreement.Addresses.Where(c => c.TypeAddress == "DIR01" && c.IsActive == true).First();
                                lblDireccionF.Text = address.Street
                                                  + " NO." + address.Outdoor
                                                  + " INT." + address.Indoor
                                                  + ", COL." + address.Suburbs.Name
                                                  + ". " + address.Suburbs.Towns.Name
                                                  + ". " + address.Suburbs.Towns.States.Name;
                                var direcc = Variables.Agreement.Addresses.Where(x => x.TypeAddress == "DIR03").FirstOrDefault();
                                if (direcc != null)
                                    lblDireccionN.Text = direcc.Street
                                                  + " NO." + direcc.Outdoor
                                                  + " INT." + direcc.Indoor
                                                  + ", COL." + direcc.Suburbs.Name
                                                  + ". " + direcc.Suburbs.Towns.Name
                                                  + ". " + direcc.Suburbs.Towns.States.Name;
                                else
                                {
                                    lblDireccionN.Text = "Sin Dato";
                                }


                            }
                            else
                            {
                                lblDireccionF.Text = "Sin Dato";
                                lblDireccionN.Text = "Sin Dato";
                                mensaje = new MessageBoxForm("Problemas", "No hay datos de dirección", TypeIcon.Icon.Warning);
                                result = mensaje.ShowDialog();
                            }


                            //Avalúo
                            if (Variables.Configuration.IsMunicipal)
                            {
                                if (Variables.Agreement.AgreementDetails != null && Variables.Agreement.AgreementDetails.Count > 0)
                                {
                                    int _idetail = Variables.Agreement.AgreementDetails.Max(x => x.Id);
                                    var _detail = Variables.Agreement.AgreementDetails.SingleOrDefault(x => x.Id == _idetail);
                                    var _fechaActualizacion = _detail.LastUpdate;
                                    if (_detail.LastUpdate < DateTime.UtcNow.ToLocalTime().AddYears(-4))
                                    {
                                        mensaje = new MessageBoxForm("Actualización", "Debe Actualizar Avalúo", TypeIcon.Icon.Warning);
                                        result = mensaje.ShowDialog();
                                    }
                                }
                            }


                            if (Variables.Agreement.TypeStateServiceId == 1 || Variables.Agreement.TypeStateServiceId == 3)
                            {
                                //Aviso previo a corte de servicio
                                var resultadoAgreement = await Requests.SendURIAsync(string.Format("/api/OrderWork/FromAccount/{0}", Variables.Agreement.Account), HttpMethod.Get, Variables.LoginModel.Token);
                                if (resultadoAgreement.Contains("error\\") || string.IsNullOrEmpty(resultadoAgreement))
                                {
                                }
                                else
                                {
                                    Agreement agree = JsonConvert.DeserializeObject<Agreement>(resultadoAgreement);
                                    Variables.Agreement.OrderWork = agree.OrderWork;
                                    //La cuenta aun no ha sido cortada.
                                    if (Variables.Agreement.TypeStateServiceId == 1)
                                    {
                                        if (agree.OrderWork.Where(ow => ow.Status == "EOT01" && ow.Type == "OT002").FirstOrDefault() != null)
                                        {
                                            //La cuenta tiene una orden de corte aun sin asinar
                                            var folio = agree.OrderWork.Where(ow => ow.Status == "EOT01" && ow.Type == "OT002").FirstOrDefault().Folio;

                                            string msgCorte = "La cuenta proporcionada tiene una orden de corte sin asignar aun. Folio de orden: " + folio;
                                            mensaje = new MessageBoxForm("Orden de corte, sin asignar", msgCorte, TypeIcon.Icon.Info);
                                            result = mensaje.ShowDialog();
                                        }
                                        else if (agree.OrderWork.Where(ow => ow.Status == "EOT02" && ow.Type == "OT002").FirstOrDefault() != null)
                                        {
                                            //La cuenta tiene una orden de corte asignada, es problabe que el tecnico este en camino
                                            var nombre = agree.OrderWork.Where(ow => ow.Status == "EOT02" && ow.Type == "OT002").FirstOrDefault().TechnicalStaff.Name;  //es el usuario que va a cortar.
                                            var cel = agree.OrderWork.Where(ow => ow.Status == "EOT02" && ow.Type == "OT002").FirstOrDefault().TechnicalStaff.Phone;
                                            var folio = agree.OrderWork.Where(ow => ow.Status == "EOT02" && ow.Type == "OT002").FirstOrDefault().Folio;

                                            btnCobrar.Enabled = false;
                                            string msgCorte = string.Format("La cuenta proporcionada tiene una orden de corte asignada a {0} {1}. Folio de orden: {2}, es probable que el técnico este en camino. No se puede recibir pago hasta que la orden se resuelta.", nombre, string.IsNullOrEmpty(cel) ? "" : ", Cel: " + cel, folio);
                                            mensaje = new MessageBoxForm("Orden de corte Asignada", msgCorte, TypeIcon.Icon.Warning);
                                            result = mensaje.ShowDialog();
                                        }
                                    }

                                    //Si la cuenta ya ha sido cortada.
                                    if (Variables.Agreement.TypeStateServiceId == 3)
                                    {
                                        string msg = "La cuenta proporcionada " + (Variables.Agreement.TypeStateService != null ? "está: " + Variables.Agreement.TypeStateService.Name : "No existe");
                                        mensaje = new MessageBoxForm("Aviso", msg, TypeIcon.Icon.Info);
                                        result = mensaje.ShowDialog();
                                    }
                                }

                                //Deuda
                                loading = new Loading();
                                loading.Show(this);
                                var resultDeb = await Requests.SendURIAsync(string.Format("/api/Debts/{0}", Variables.Agreement.Id), HttpMethod.Get, Variables.LoginModel.Token);
                                loading.Close();
                                if (resultDeb.Contains("error\\"))
                                {
                                    mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultDeb).error, TypeIcon.Icon.Cancel);
                                    result = mensaje.ShowDialog();
                                }
                                else
                                {
                                    Variables.Agreement.Debts = JsonConvert.DeserializeObject<List<Model.Debt>>(resultDeb);
                                    draw_Observaciones(Variables.Agreement.AgreementComments.ToList());
                                    if (Variables.Agreement.Debts != null && Variables.Agreement.Debts.Count > 0)
                                    {
                                        ObtenerSeleccion();
                                        descuentosToolStripMenuItem.Enabled = true;
                                        //Verificacion de promocion por COVID
                                        if (Variables.Configuration.RecargosDescCovid != null && Variables.Configuration.RecargosDescCovid.Count > 0)
                                            VerificaDescuentoPorCOVID(Variables.Agreement.Id, Variables.Agreement.Debts.ToList());
                                    }
                                    else
                                    {

                                        descuentosToolStripMenuItem.Enabled = false;

                                        if (!Variables.Configuration.IsMunicipal)
                                        {
                                            var UltimaConvenio = Variables.Agreement.PartialPayments.LastOrDefault();
                                            //Si es convenio no debe decirle que puede dar pagos anticipados
                                            if (Variables.Agreement.PartialPayments != null && Variables.Agreement.PartialPayments.Count > 0 && UltimaConvenio.Status == "COV01")
                                            {
                                                mensaje = new MessageBoxForm("Cuenta sin deuda y con convenio", "La cuenta proporcionada no tiene adeudo y tiene un convenio vigente.", TypeIcon.Icon.Info);
                                                mensaje.ShowDialog();
                                            }
                                            else  //Para realiza pagos anticipados
                                            {
                                                Variables.Configuration.Anual = checkApplyAnual(Variables.Agreement);
                                                mensaje = new MessageBoxForm("Sin Deuda", "La cuenta proporcionada no tiene adeudo, puede realizar pagos anticipados", TypeIcon.Icon.Success, true);
                                                result = mensaje.ShowDialog();                                                
                                            }
                                        }
                                        else
                                        {

                                            mensaje = new MessageBoxForm(Variables.titleprincipal, "La cuenta proporcionada no tiene adeudo", TypeIcon.Icon.Success);
                                            result = mensaje.ShowDialog();
                                        }

                                        if (result == DialogResult.OK)
                                        {
                                            //Variables.Configuration.Anual = checkApplyAnual(Variables.Agreement);
                                            Anticipo Anticipo = new Anticipo();
                                            Anticipo.setAgreement(Variables.Agreement);
                                            if (Anticipo.ShowDialog() == DialogResult.OK)
                                            {
                                                ObtenerInformacion();

                                            }
                                        }

                                    }

                                }
                            }
                            else
                            {
                                descuentosToolStripMenuItem.Enabled = false;
                                string msg = "La cuenta proporcionada " + (Variables.Agreement.TypeStateService != null ? "está: " + Variables.Agreement.TypeStateService.Name : "No existe");
                                mensaje = new MessageBoxForm("Error", msg, TypeIcon.Icon.Cancel);
                                result = mensaje.ShowDialog();
                                Variables.Agreement = null;
                            }                                                        
                        }
                        else
                        {
                            descuentosToolStripMenuItem.Enabled = false;
                            mensaje = new MessageBoxForm("Sin dato", "No se encontraron datos para este número de cuenta", TypeIcon.Icon.Warning);
                            result = mensaje.ShowDialog();
                        }
                        if (!Variables.Configuration.IsMunicipal && Variables.Agreement != null) {
                            DateTime current = DateTime.Now;
                            int HaveDebtBefore = Variables.Agreement.Debts.Where(x => x.FromDate.Year < DateTime.Now.Year).ToList().Count;
                            if (HaveDebtBefore == 0) {
                                if (current.Month == 1 && Variables.Agreement.Debts.Count() >= 1 && Variables.Agreement.Debts.Where(x => x.FromDate.Month == 1).ToList().Count() == 1)
                                {
                                    layoutAnual.Visible = true;
                                }
                                if (current.Month == 2 && Variables.Agreement.Debts.Count() >= 2 && Variables.Agreement.Debts.Where(x => x.FromDate.Month == 2).ToList().Count() == 1)
                                {
                                    layoutAnual.Visible = true;
                                }
                            }
                        }
                        


                        ////Valida esta activa la campaña anual
                        //if(Variables.Agreement != null && Variables.Configuration.anualDiscount != null)
                        //{
                        //    var deudaPasada = Variables.Agreement.Debts.Where(d => d.Year < Variables.Configuration.anualDiscount.PromocionAño).ToList();
                        //    gbxAnual.Visible = false;
                        //    if (Variables.Configuration.AnualParameter != null
                        //        && Variables.Agreement != null
                        //        //&& Variables.Agreement.Debts.Count == 0 
                        //        && deudaPasada.Count == 0
                        //        && Variables.Configuration.anualDiscount != null
                        //        && Variables.Configuration.anualDiscount.TiposToma.Contains(Variables.Agreement.TypeIntakeId)
                        //        && (Variables.Configuration.anualDiscount.VigenciaInicio <= DateTime.Now && DateTime.Now <= Variables.Configuration.anualDiscount.VigenciaFinal))
                        //    {
                        //        gbxAnual.Visible = true;
                        //        tableLayoutPanel3.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                        //    }
                        //}

                        ////Valida si hay campañas de descuentos adicionales.
                        //if (Variables.Agreement != null && Variables.Configuration.CondonationCampaings.Count > 0 && Variables.Configuration.CondonationCampaings.FirstOrDefault().Percentage > 0) // && Variables.Agreement.Addresses.FirstOrDefault().Suburbs.ApplyAnnualPromotion == true)
                        //{
                        //    gbxCondonacion.Visible = accessParam == CashBoxAccess.Access.GenerarOrden ? false : true;
                        //    tableLayoutPanel3.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                        //    lblTitleCondonation.Text = Variables.Configuration.CondonationCampaings.First().Alias;
                        //}

                        if(Variables.Agreement != null && Variables.Configuration.Promociones != null && Variables.Configuration.Promociones.Count > 0)
                        {
                            flpPromociones.Controls.Clear();
                            foreach (var item in Variables.Configuration.Promociones)
                            {
                                string prefijo = item.Nombre.Substring(0, 3);

                                //Validacion para saber si se activa boton de campaña anual.
                                if (prefijo.Equals("ANL"))
                                {
                                    var deudaPasada = Variables.Agreement.Debts.Where(d => d.Year < Variables.Configuration.anualDiscount.PromocionAño).ToList();
                                    gbxAnual.Visible = false;
                                    if (Variables.Agreement != null                                        
                                        && deudaPasada.Count == 0                                        
                                        && (item.TiposToma.Count() > 0 ? (item.TiposToma.Contains(Variables.Agreement.TypeIntakeId)) : true )
                                        && (item.VigenciaInicio <= DateTime.Now && DateTime.Now <= item.VigenciaFinal))
                                    {
                                        BtnPromotion btnPromotionAnual = new BtnPromotion(item);
                                        btnPromotionAnual.Changed += new EventHandler(Changed);
                                        flpPromociones.Controls.Add(btnPromotionAnual);
                                    }
                                }
                                //Validacion para saber si se agrega campaña de descuento
                                if(prefijo.Equals("CDN") || prefijo.Equals("DSC"))
                                {
                                    if (item.Descuentos != null
                                        && item.Descuentos.Count() > 0
                                        && (item.TiposToma.Count() > 0 ? (item.TiposToma.Contains(Variables.Agreement.TypeIntakeId)) : true) )
                                    {
                                        BtnPromotion btnPromotion = new BtnPromotion(item);
                                        btnPromotion.Changed += new EventHandler(Changed);
                                        flpPromociones.Controls.Add(btnPromotion);
                                    }
                                }
                                //Validacion para saber si se puede aplicar campaña mixta
                                if (prefijo.Equals("MXT"))
                                {
                                    var content = new StringContent(JsonConvert.SerializeObject(new List<int>() {Variables.Agreement.Id}), Encoding.UTF8, "application/json");
                                    var result = await Requests.SendURIAsync("/api/DebtCampaign/fromAgreement", HttpMethod.Post, Variables.LoginModel.Token, content);
                                    if (!result.Contains("error\\"))
                                    {
                                        //var resultO = JObject.Parse(result);
                                        //AllDebtAnnual = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(resultO["allDebtAnnual"]));
                                        List<DebtCampaign> debtCampaigns = JsonConvert.DeserializeObject<List<DebtCampaign>>(result);
                                        if(debtCampaigns.Count() > 0)
                                        {
                                            BtnPromotion btnPromotion = new BtnPromotion(item);
                                            btnPromotion.Changed += new EventHandler(Changed);
                                            flpPromociones.Controls.Add(btnPromotion);
                                        }
                                    }

                                }
                            }

                            if (flpPromociones.Controls.Count > 1)
                            {
                                tableLayoutPanel3.RowStyles[2] = new RowStyle(SizeType.Absolute, 160);
                            }
                            else
                            {
                                tableLayoutPanel3.RowStyles[2] = new RowStyle(SizeType.Absolute, 90);
                            }
                            //tableLayoutPanel3.RowStyles[2] = new RowStyle(SizeType.AutoSize);
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
        //Para cachar cuando teminan los botones de promocion.
        private void Changed(object sender, EventArgs e)
        {
            BtnPromotion fc = sender as BtnPromotion;
            if (fc != null)
            {
                ObtenerInformacion();
            }
        }

        private async void ObtenerSeleccion()
        {
            ////Codigo Original
            //TypeServices = Variables.Agreement.Debts.ToList()
            //                               .GroupBy(x => new { x.Type, x.DescriptionType })
            //                               .Select(g => new TypeService
            //                               {
            //                                   Id = g.Key.Type,
            //                                   Description = g.Key.DescriptionType
            //                               }).ToList();

            //TypeServices.Add(new TypeService()
            //{
            //    Id = "0",
            //    Description = "Todos"
            //});

            //TypeServices.ForEach(x =>
            //{
            //    Seleccion.Add(x.Id);
            //});

            //Nuevo codigo
            var resultGTypes = await Requests.SendURIAsync(string.Format("/api/Type/ByToolCode/{0}", "Caja_Cobro_cbxTipos"), HttpMethod.Get, Variables.LoginModel.Token);            
            if (resultGTypes.Contains("error\\"))
            {
                TypeServices = new List<TypeService>();
                TypeServices.Add(new TypeService()
                {
                    Id = "0",
                    Description = "Todos"
                });
            }
            else
            {
                groupTypes = JsonConvert.DeserializeObject<List<GroupType>>(resultGTypes);
                TypeServices = groupTypes.Select(gt => new TypeService() { Id = gt.Id.ToString(), Description = gt.Name }).ToList();

                TypeServices.Add(new TypeService()
                {
                    Id = "0",
                    Description = "Todos"
                });                
            }

            TypeServices.ForEach(x =>
            {
                Seleccion.Add(x.Id);
            });

            cmbTipos.ValueMember = "Id";
            cmbTipos.DisplayMember = "Description";
            cmbTipos.DataSource = TypeServices;
            cmbTipos.SelectedIndex = cmbTipos.FindString("Todos");
            cmbTipos.Enabled = true;
            SeleccionarDeuda(Search.Type.Cuenta);
        }
        private bool checkApplyAnual(Model.Agreement agreement)
        {
            var date = DateTime.Now;

            //Variables.Agreement = null;
            if (date.Month != 12 && date.Month != 1 && date.Month != 2) // &&)
            {
                return false;
            }

            if (agreement.Debts.Count > 0)
            {
                return false;
            }
            //if (agreement.Debts.Count > 0 && agreement.PartialPayments.Where(x => x.Status == "COV01").ToList().Count() > 0)
            //{
            //    return false;
            //}


            return true;

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
                        //tmpFiltros = Variables.Agreement.Debts.Where(x => Seleccion.Contains(x.Type)).ToList();
                        var tmpTipos = new List<Types>();
                        var tmpGrupos = groupTypes.Where(x => Seleccion.Contains(x.Id.ToString())).ToList();                        
                        foreach (var item in tmpGrupos)
                        {
                            tmpTipos.AddRange(item.Types);
                        }
                        tmpFiltros = Variables.Agreement.Debts.Where(x => tmpTipos.Distinct().ToList().Select(t => t.CodeName).Contains(x.Type)).ToList();                        

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
        private async Task CheckDebtIsAnnual(List<int> debtId )
        {
            Variables.Configuration.Descuento = 5;

            if (DateTime.Now.Month == 12)
            {


                Variables.Configuration.Descuento = 10;

            }
            var agreementDiscount = Variables.Agreement.AgreementDiscounts.Where(xx => xx.IsActive).FirstOrDefault();
            if (agreementDiscount != null)
            {


                Variables.Configuration.Descuento = 50;
            }


            var content = new StringContent(JsonConvert.SerializeObject(debtId), Encoding.UTF8, "application/json");
            var result =  await Requests.SendURIAsync(string.Format("/api/Debts/GetDiscountAnnual/" + Variables.Configuration.Descuento), HttpMethod.Post, Variables.LoginModel.Token, content);
            if (!result.Contains("error\\"))
            {
                
                var resultO = JObject.Parse(result);
                debtApplyDiscount = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(resultO["ids"]));
                AllDebtAnnual = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(resultO["allDebtAnnual"]));
                descuento =decimal.Parse(resultO["descuento"].ToString());
                ApplyMSI= bool.Parse(resultO["applyMSI"].ToString());
               
                 


            }
            
        }
        private async void CalculateAmounts(List<Model.Debt> pDebts)
        {
            loading = new Loading();
            loading.Show(this);

            decimal IVA = 0;
            descuento = 0;
            
            decimal subTotal = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblSubtotal.Text, @"[^\d.]", "")));
            decimal redondeo = 0;
            decimal total = 0;
            //Get IVA
           
            if ((DateTime.Now.Month == 12 || DateTime.Now.Month == 1 || DateTime.Now.Month == 2))
            {



               await CheckDebtIsAnnual(pDebts.Select(x =>x.Id).ToList());


               
            }

            foreach (var x in pDebts)
            {
                bool isAnnual = false;
                //IVA = IVA + x.DebtDetails.Where(t => t.HaveTax == true).Sum(y => (((y.Amount - y.OnAccount) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100));
             
                x.DebtDetails.ToList().ForEach(y =>
              {
                  if (y.HaveTax)
                      IVA = IVA + (Math.Round(((y.Amount - y.OnAccount) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100, 2));
                  

              });

            }
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
            if (descuento > 0 && (Variables.Agreement.TypeIntakeId != 2   && Variables.Agreement.TypeIntakeId != 3))
            {
                lblDescuentoT.Visible = true;
                lblDescuentoT.Text = "Por promoción anual se aplicó un descuento de " + string.Format(new CultureInfo("es-MX"), "{0:C2}", (total * Variables.Configuration.Descuento) / (100 - Variables.Configuration.Descuento)) + " Pesos.\nNo aplica para pagos con tarjeta de credito a MSI";
            }
            else
            {
                lblDescuentoT.Visible = false;
            }
            txtTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", total).Replace("$", "").Replace(",", "");
            loading.Close();
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
                else if (Convert.ToDecimal(lblTotal.Text.Replace("$", "").Replace(",", "")) > Convert.ToDecimal(txtTotal.Text))
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto capturado esta no es valido para el cobro del producto favor de verificar", TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                    txtTotal.Text = lblTotal.Text.Replace("$", "").Replace(",", "");
                }
                else if (Variables.Configuration.IsMunicipal)
                {
                    if (total > amount)
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto capturado esta no es valido", TypeIcon.Icon.Cancel);
                        mensaje.ShowDialog();
                        txtTotal.Text = lblTotal.Text.Replace("$", "").Replace(",", "");
                    }
                    else
                    {
                        CorreoCliente = Variables.OrderSale.TaxUser.EMail;
                        PaymentModal();
                    }
                }
                else
                {
                    CorreoCliente = Variables.OrderSale.TaxUser.EMail;
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
                else if (Variables.Configuration.IsMunicipal)
                {
                    if (total > amount)
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto capturado esta no es valido", TypeIcon.Icon.Cancel);
                        mensaje.ShowDialog();
                        txtTotal.Text = lblTotal.Text.Replace("$", "").Replace(",", "");
                    }
                    else
                    {
                        CorreoCliente = Variables.Agreement.Clients.FirstOrDefault() == null ? "" : Variables.Agreement.Clients.FirstOrDefault().EMail;
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
                else
                {
                    CorreoCliente = Variables.Agreement.Clients.FirstOrDefault() == null ? "" : Variables.Agreement.Clients.FirstOrDefault().EMail;
                    if (amount < total)
                    {
                        //En los convenios (TIP06) no se permiten pagos parciales.
                        if (Variables.Agreement.Debts.Any(d => d.Type == "TIP06"))
                        {
                            mensaje = new MessageBoxForm("Cuenta con convenio", "No se permiten pagos parciales durante un convenio.", TypeIcon.Icon.Warning);
                            mensaje.ShowDialog();
                        }
                        else
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

                ModalDetalleCobro Cobro = new ModalDetalleCobro(amount, tax, rounding, paidUp, total, tmpFiltros, Padron, porcentaje, anual, prepaid, CorreoCliente);
                Cobro.SetIsMSI(ApplyMSI, descuento, debtApplyDiscount);

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
                    if (resultado.Contains("error\\"))
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultado).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        List<Adeudos> m = JsonConvert.DeserializeObject<List<Adeudos>>(resultado);
                        if (!m.Any(x => x.name_concept.Contains("Descuento")))
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
            if (txtTotal.Text.Trim() == "")
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
                //tmpFiltros = tmpFiltros.Where(x => !AllDebtAnnual.Contains(x.Id)).ToList();
                //var ammo = tmpFiltros.Count == 0? 0: amount;
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
            if (resultCampaign.Contains("error\\"))
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

        private void registrarPeriodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PeriodosAnteriores oPeriodosAnteriores = new PeriodosAnteriores(Variables.Agreement.Id);
            result = oPeriodosAnteriores.ShowDialog();
            if (result == DialogResult.OK)
                ObtenerInformacion();

        }
        private void draw_Observaciones(List<Model.AgreementComent> Observaciones)
        {
            if (Observaciones.Count() == 0)
            {
                datadescripcion.Visible = false;
                return;
            }

            datadescripcion.Visible = true;
            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 11, FontStyle.Bold);
            datadescripcion.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Set the column header names.

            removeColumns(datadescripcion.Rows);
            Observaciones.ForEach(x =>
            {
                datadescripcion.Rows.Add(x.DateIn.ToString(), x.Observation, x.UserName);
                // datadescripcion.Rows[0].
            });

        }
        private void removeColumns(DataGridViewRowCollection rows)
        {
            while (rows.Count > 1)
            {

                datadescripcion.Rows.Remove(rows[rows.Count - 2]);
            }
            var countRows = rows.Count - 1;


        }
        private void btnCobrar_Click_1(object sender, EventArgs e)
        {
            SendPayment();
        }

        private void datadescripcion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btnCondonacion_Click(object sender, EventArgs e)
        {
            //loading = new Loading();
            //loading.Show(this);
            //string ruta = "";

            //if (Variables.Configuration.CondonationCampaings.FirstOrDefault().Name.Contains("CDN"))     //Condonacion
            //{
            //    ruta = string.Format("/api/CondonationCampaing/CondonationPromotion/{0}/{1}?us={2}&usName={3}", Variables.Agreement.Id, Variables.Configuration.CondonationCampaings.FirstOrDefault().Id, Variables.LoginModel.User, Variables.LoginModel.FullName);
            //}
            //else if (Variables.Configuration.CondonationCampaings.FirstOrDefault().Name.Contains("DSC"))   //Descuento
            //{
            //    ruta = string.Format("/api/CondonationCampaing/DiscountPromotion/{0}/{1}", Variables.Agreement.Id, Variables.Configuration.CondonationCampaings.FirstOrDefault().Id);
            //}

            //var resultCampaign = await Requests.SendURIAsync(ruta, HttpMethod.Post, Variables.LoginModel.Token);
            //if (resultCampaign.Contains("error\":"))
            //{
            //    try
            //    {
            //        mensaje = new MessageBoxForm("Promocion NO aplicada", JsonConvert.DeserializeObject<Error>(resultCampaign).error, TypeIcon.Icon.Cancel);
            //        result = mensaje.ShowDialog();
            //        loading.Close();
            //    }
            //    catch (Exception)
            //    {
            //        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador", TypeIcon.Icon.Cancel);
            //        result = mensaje.ShowDialog();
            //        loading.Close();
            //    }
            //}
            //else
            //{

            //    mensaje = new MessageBoxForm(Variables.titleprincipal, "La condonación de recargos se ha realizado con exito", TypeIcon.Icon.Success);
            //    result = mensaje.ShowDialog();
            //    loading.Close();
            //    ObtenerInformacion();
            //}
        }

        private async void btnAnual_Click(object sender, EventArgs e)
        {
            //if (Variables.Agreement != null)
            //{
            //    if (Variables.Configuration.IsMunicipal)
            //    {
            //        var Uiperiodos = new PagosAnualesAyuntamiento(Variables.Agreement);
            //        var result = Uiperiodos.ShowDialog(this);
            //        Uiperiodos.Close();
            //    }
            //    else
            //    {
            //        Variables.Configuration.Anual = true;
            //        int ultimoPeriodo = await ValidaUltimoPeriodoDePago(Variables.Configuration.anualDiscount.PromocionAño);
            //        var Uiperiodos = new PeriodosAnticipados(Variables.Agreement, true, (ultimoPeriodo > 0 ? ultimoPeriodo : 0));
            //        var result = Uiperiodos.ShowDialog(this);
            //        Uiperiodos.Close();
                    
            //        if(result == DialogResult.OK)
            //        {
            //            pbBuscar_Click(new object(), new EventArgs());                        
            //        }
            //    }
            //}
        }

        //private async Task<int> ValidaUltimoPeriodoDePago(int año)
        //{
        //    var resultDebt = await Requests.SendURIAsync(string.Format("/api/Debts/debtPaid?idAgreement={0}&year={1}",Variables.Agreement.Id, año), HttpMethod.Get, Variables.LoginModel.Token);
        //    loading.Close();
        //    if (resultDebt.Contains("error\\"))
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        List<Debt> debts = JsonConvert.DeserializeObject<List<Debt>>(resultDebt);
        //        if(debts != null && debts.Count > 0)
        //        {
        //            return debts.Count();
        //        }
        //        return 0;
        //    }
        //}

        private async void lblDescuentoT_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            if (Variables.Agreement != null) {
                UpdateDataContactsAgremment UpdateDataContactsAgremment = new UpdateDataContactsAgremment(Variables.Agreement.Clients.ToList());
                UpdateDataContactsAgremment.ShowDialog();
            }
        }

        private void btnAnuall_Click(object sender, EventArgs e)
        {
            Variables.Configuration.Anual = true;
            var Uiperiodos = new PeriodosAnticipados(Variables.Agreement, true);

            var result = Uiperiodos.ShowDialog(this);
            Uiperiodos.Close();
            if (result != DialogResult.Cancel ) {
                ObtenerInformacion();
            }

        }

        //Opcion de Ajuste de recibo.
        private void ajusteDeReciboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal amount = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblSubtotal.Text, @"[^\d.]", "")));
            if (orderSale)
            {
                Adjusment discount = new Adjusment(amount, orders);
                discount.ShowDialog(this);
            }
            else
            {
                Adjusment discount = new Adjusment(amount, tmpFiltros);
                discount.ShowDialog(this);
            }
            ObtenerInformacion();
        }

        //Verifica si a esta cueta se la condono deuda por COVID
        private async void VerificaDescuentoPorCOVID(int idAgreement, List<Debt> debts)
        {
            try
            {
                string aniosDebt = string.Join("|", debts.Where(x => x.Type == "TIP01").Select(x => x.Year).ToList());
                var resultDebDescuento = await Requests.SendURIAsync(string.Format("/api/Debts/promocionCOVID/{0}/{1}", idAgreement, aniosDebt), HttpMethod.Get, Variables.LoginModel.Token);
                loading.Close();
                if (resultDebDescuento.Contains("error\\"))
                {
                    mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultDebDescuento).error, TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    //Variables.Agreement.Debts = JsonConvert.DeserializeObject<List<Model.Debt>>(resultDebDescuento);
                    decimal TotalDescuentoCOVID = JsonConvert.DeserializeObject<decimal>(resultDebDescuento);
                    var Mensaje = Variables.Configuration.RecargosDescCovid.FirstOrDefault();

                    if(TotalDescuentoCOVID > 0)
                    {
                        Variables.Configuration.TotalDescuentoCOVID = TotalDescuentoCOVID;
                        string Titulo = Mensaje.Split('|')[0].Split('@')[0];
                        string Cuerpo = Mensaje.Split('|')[0].Split('@')[1];

                        mensaje = new MessageBoxForm(Titulo, string.Format(Cuerpo, TotalDescuentoCOVID), TypeIcon.Icon.Info);
                        result = mensaje.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //Descuento que se aplica al momento de cobrar, por contingencia COVID
        private async Task<string> aplicaDescuentoProductosCOVID()
        {
            short pTypo = Variables.Configuration.ProductosDescCOVID.TypeColumn;
            decimal pValor = Variables.Configuration.ProductosDescCOVID.NumberColumn;
            int pIdOrder = Variables.OrderSale.Id;
            string pIdsOSD = "";
            HttpContent content;
            try
            {
                var lstConceptosDescCOVID = Variables.Configuration.ProductosDescCOVID.TextColumn.Substring(0, Variables.Configuration.ProductosDescCOVID.TextColumn.IndexOf(" => ")).Split('|').ToList();
                var lstIdsToApliDesc = Variables.OrderSale.OrderSaleDetails.Where(x => lstConceptosDescCOVID.Contains(x.CodeConcept)).Select(x => x.Id).ToList();
                pIdsOSD = string.Join("|", lstIdsToApliDesc);
                if (lstIdsToApliDesc.Count > 0)
                {
                    //Se lanza sp para modificar la orden en BD y aplicar descuento.
                    var json = JsonConvert.SerializeObject(new { tipo = pTypo, valor = pValor, id = pIdOrder, idsOSD = pIdsOSD });
                    var claseAnonima = new { tipo = pTypo, valor = pValor, id = pIdOrder, idsOSD = pIdsOSD };
                    content = new StringContent(JsonConvert.SerializeObject(claseAnonima), Encoding.UTF8, "application/json");
                    var _resulTransaction = await Requests.SendURIAsync("/api/Debts/ApplyDiscountProductsCOVID", HttpMethod.Post, Variables.LoginModel.Token, content);
                    if (_resulTransaction.Contains("error:"))
                    {
                        mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        return "error";
                    }
                    int ValsActualizados = JsonConvert.DeserializeObject<int>(_resulTransaction);

                    if (lstIdsToApliDesc.Count != ValsActualizados)
                    {
                        mensaje = new MessageBoxForm("Error", "No puedo aplicar los descuentos, se realizara el cobro sin descuento. CONSULTE SON EL ADMINSTRADOR.", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        return "Se cobrara sin descuento.";
                    }
                    else
                    {
                        //Se vuelve a cargar la orden
                        var _cuenta = Variables.OrderSale.Folio;
                        var resultOrder = await Requests.SendURIAsync(string.Format("/api/OrderSales/Folio/{0}", _cuenta), HttpMethod.Get, Variables.LoginModel.Token);
                        loading.Close();
                        if (resultOrder.Contains("error\\"))
                        {
                            mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultOrder).error, TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                        }
                        else
                        {
                            Variables.OrderSale = JsonConvert.DeserializeObject<Model.OrderSale>(resultOrder);
                        }
                    }
                }
                return "ok";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

    }

    public partial class CollectConcepts
    {
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
using SOAPAP.Enums;
using SOAPAP.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using SOAPAP.Services;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using SOAPAP.UI.Email;
using Newtonsoft.Json.Linq;

namespace SOAPAP.UI
{
    public partial class ModalFicha : Form
    {
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        String agrementID = String.Empty;
        List<Model.Payment> _payments = null;
        List<Model.Debt> _debts = null;
        Model.Agreement _agreement = null;
        Model.OrderSale _orderSale = null;
        Search.Type _typeSearchSelect = Search.Type.Ninguno;

        public ModalFicha(string Title, string Message, TypeIcon.Icon TypeIcon, String AgreementId, Search.Type Type)
        {
            InitializeComponent();
            Requests = new RequestsAPI(UrlBase);
            this.agrementID = AgreementId;
            _typeSearchSelect = Type;
        }       

        #region Events
        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private async void ModalRetiroCaja_Load(object sender, EventArgs e)
        {  
            

            switch (_typeSearchSelect)
            {
                case Search.Type.Ninguno:
                    break;
                case Search.Type.Cuenta:
                    loading = new Loading();
                    loading.Show(this);
                    ConfiguracionInicial(Search.Type.Cuenta);
                    var t1 = ObtenerAgreement();
                    var t2 = ObtenerPagos();
                    var t3 = ObtenerHistoricoDeuda();
                    await Task.WhenAll(t1, t2, t3);

                    CargaDeuda();
                    CargaHistoricoDeuda();
                    CargaDescuentos();
                    CargaAnticipos();
                    CargaPagos();
                    CargaObservaciones();
                    CargarConvenios();
                    cargarOrdenes();
                    cargarRegla();
                    loading.Close();
                    break;
                case Search.Type.Folio:
                    
                    tbcInformacion.Controls.Remove(tabRegla);
                    tbcInformacion.Controls.Remove(tabConvenios);
                    tbcInformacion.Controls.Remove(tabOrdenes);
                    ConfiguracionInicial(Search.Type.Folio);
                    ObtenerFolio();
                    
                    break;
                default:
                    break;
            }
          

            
            
        }
        private void cargarRegla()
        {
            if (!Variables.Configuration.IsMunicipal && Variables.Agreement != null && Variables.Agreement.AgreementRulerCalculations.Where(x => x.IsActive).ToList().Count >0)
            {
                dataReglas.Columns.Add("column1", "Servicio");
                dataReglas.Columns.Add("column2", "Monto fijo");
                List<string> data;
                Variables.Agreement.AgreementRulerCalculations.Where(x => x.IsActive).ToList().ForEach(x =>
                {
                    data = new List<string>(){
                    x.ServiceId == 1 ? "Agua":(x.ServiceId == 2? "Drenaje": "Saneamiento"),
                    string.Format(new CultureInfo("es-MX"), "{0:C2}", x.Amount)
                    };
                    dataReglas.Rows.Add(data.ToArray());

                });



            }
            else
            {
                tbcInformacion.Controls.Remove(tabRegla);
            }
        }
        private void dgvRecibos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    mensaje = new ModalDetalleCaja("Detalle Conceptos", "", TypeIcon.Icon.Warning, this.dgvRecibos.Rows[e.RowIndex].Cells["Id"].Value.ToString(), _typeSearchSelect);
            //    result = mensaje.ShowDialog();
            //}
        }

        private void DgvRecibos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string status = this.dgvRecibos.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                mensaje = new ModalDetalleCaja("Detalle Conceptos", "", TypeIcon.Icon.Warning, this.dgvRecibos.Rows[e.RowIndex].Cells["Id"].Value.ToString(), _typeSearchSelect, status);
                result = mensaje.ShowDialog();
            }
        }

        #endregion

        #region PrivateMethod

        private void centraX(Control padre, Control hijo)
        {
            int x = 0;
            x = (padre.Width / 2) - (hijo.Width / 2);
            hijo.Location = new System.Drawing.Point(x, hijo.Location.Y);
        }

        private async Task ObtenerAgreement()
        {            
            await Task.Factory.StartNew(() =>
            {
                var resultAgreement = Requests.SendURIAsync(string.Format("/api/Agreements/GetSummary/{0}/1", agrementID), HttpMethod.Get, Variables.LoginModel.Token).Result;
                if (resultAgreement.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", resultAgreement.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    _agreement = JsonConvert.DeserializeObject<Model.Agreement>(resultAgreement);

                    if (_agreement != null)
                    {
                        ObtenerInformacion(_agreement);
                    }
                    else
                    {
                        mensaje = new MessageBoxForm("Error", "No se ha obtenido información de esa cuenta", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                }
            });           
        }       

        private async Task ObtenerPagos()
        {
            var resultPayment = await Requests.SendURIAsync(string.Format("/api/PaymentHistory/{0}", Variables.Agreement.Id), HttpMethod.Get, Variables.LoginModel.Token);
            if (resultPayment.Contains("error"))
            {
                //mensaje = new MessageBoxForm("Información", resultPayment.Split(':')[1].Replace("}", ""), TypeIcon.Icon.);
                //result = mensaje.ShowDialog();
            }
            else
                _payments = JsonConvert.DeserializeObject<List<Model.Payment>>(resultPayment);
        }

        private async Task ObtenerHistoricoDeuda()
        {
            var resultDebt = await Requests.SendURIAsync(string.Format("/api/Debts/History/{0}", Variables.Agreement.Id), HttpMethod.Get, Variables.LoginModel.Token);
            if (resultDebt.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultDebt.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
                _debts = JsonConvert.DeserializeObject<List<Model.Debt>>(resultDebt);
        }

        private async void ObtenerFolio()
        {
            loading = new Loading();
            loading.Show(this);
            var resultOrder =await Requests.SendURIAsync(string.Format("/api/OrderSales/Folio/{0}", agrementID), HttpMethod.Get, Variables.LoginModel.Token);
            loading.Close();
            if (resultOrder.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultOrder.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                _orderSale = JsonConvert.DeserializeObject<Model.OrderSale>(resultOrder);
                if (!string.IsNullOrWhiteSpace(resultOrder) && _orderSale != null)
                {
                    ObtenerInformacionOrder(_orderSale);
                }
            }

        }

        private void ConfiguracionInicial(Search.Type Type)
        {
            dgvConceptosCobro.ReadOnly = true;
            dgvConceptosCobro.Columns["Total"].DefaultCellStyle.Format = "c2";
            dgvConceptosCobro.Columns["Total"].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvConceptosCobro.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvConceptosCobro.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvConceptosCobro.Columns["Description"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvAnticipos.ReadOnly = true;
            dgvAnticipos.Columns["Amount"].DefaultCellStyle.Format = "c2";
            dgvAnticipos.Columns["Amount"].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvAnticipos.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAnticipos.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvAnticipos.Columns["Accredited"].DefaultCellStyle.Format = "c2";
            dgvAnticipos.Columns["Accredited"].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvAnticipos.Columns["Accredited"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAnticipos.Columns["Accredited"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvAnticipos.Columns["Residue"].DefaultCellStyle.Format = "c2";
            dgvAnticipos.Columns["Residue"].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvAnticipos.Columns["Residue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAnticipos.Columns["Residue"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvAnticipos.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvAnticipos.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAnticipos.Columns["Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvAnticipos.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;           

            dgvDescuentos.ReadOnly = true;           
            dgvDescuentos.Columns["Discount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDescuentos.Columns["State"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvRecibos.ReadOnly = true;
            dgvRecibos.Columns["DebType"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvRecibos.Columns["DebPeriod"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRecibos.Columns["DebStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRecibos.Columns["DebAmount"].DefaultCellStyle.Format = "c2";
            dgvRecibos.Columns["DebAmount"].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvRecibos.Columns["DebAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRecibos.Columns["DebAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvPayment.ReadOnly = true;
            dgvPayment.Columns["DatePay"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPayment.Columns["DatePay"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvPayment.Columns["BranchOffice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPayment.Columns["PayMethod"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPayment.Columns["AmountPay"].DefaultCellStyle.Format = "c2";
            dgvPayment.Columns["AmountPay"].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvPayment.Columns["AmountPay"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPayment.Columns["AmountPay"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvObservaciones.ReadOnly = true;
            dgvObservaciones.Columns["DateObservation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvObservaciones.Columns["DateObservation"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvObservaciones.Columns["Observation"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            switch (Type)
            {
                case Search.Type.Ninguno:
                    break;
                case Search.Type.Cuenta:
                    lblTxtCuenta.Text = "CUENTA";
                    break;
                case Search.Type.Folio:
                    lblTxtCuenta.Text = "FOLIO";
                    tbcInformacion.TabPages.Remove(Recibos);
                    tbcInformacion.TabPages.Remove(Anticipos);
                    tbcInformacion.TabPages.Remove(Descuentos);
                    tbcInformacion.TabPages.Remove(Pagos);
                    tbcInformacion.TabPages.Remove(Observaciones);
                    break;
            }
        }

        private void ObtenerInformacion(Model.Agreement _agreement)
        {
            if (_agreement != null)
            {
                Model.Client _clientP = _agreement.Clients.Where(c => c.TypeUser == "CLI01" && c.IsActive == true).FirstOrDefault();
                Model.Client _clientU = _agreement.Clients.Where(c => c.TypeUser == "CLI02" && c.IsActive == true).FirstOrDefault();
                String _propietario = String.Empty;
                String _usuario = String.Empty;

                if (_clientP != null)
                    _propietario = "P. " + _clientP.Name + " " + _clientP.LastName + " " + _clientP.SecondLastName;
                if (_clientU != null)
                    _usuario = "U. " + _clientU.Name + " " + _clientU.LastName + " " + _clientU.SecondLastName;

                lblCuenta.SetPropertyValue(a => a.Text, _agreement.Account);
                lblCliente.SetPropertyValue(a => a.Text, _propietario + System.Environment.NewLine + _usuario);

                if (Variables.Configuration.IsMunicipal)
                    lblTipo.SetPropertyValue(a => a.Text, _agreement.TypeIntake.Name.ToUpper());
                else {
                    lblTipo.SetPropertyValue(a => a.Text, _agreement.TypeIntake.Name.ToUpper() + "-" + _agreement.TypeConsume.Name.ToUpper());
                }
                lblEstado.SetPropertyValue(a => a.Text, _agreement.TypeStateService.Name);

                if (_agreement.Addresses.Count > 0)
                {
                    lblDireccion.SetPropertyValue(a => a.Text, _agreement.Addresses.First().Street
                                                           + " NO." + _agreement.Addresses.First().Outdoor
                                                           + " INT." + _agreement.Addresses.First().Indoor + "." + System.Environment.NewLine
                                                           + "COL." + _agreement.Addresses.First().Suburbs.Name + "." + System.Environment.NewLine
                                                           + _agreement.Addresses.First().Suburbs.Towns.Name + "." + System.Environment.NewLine
                                                           + _agreement.Addresses.First().Suburbs.Towns.States.Name);
                }

                decimal total = _agreement.Debts != null ? _agreement.Debts.Sum(x => x.Amount - x.OnAccount) : 0;
                lblTotal.SetPropertyValue(a => a.Text, string.Format(new CultureInfo("es-MX"), "{0:C2}", total));

                if (_agreement.AgreementDiscounts != null && _agreement.AgreementDiscounts.Count > 0)
                    Variables.Agreement.AgreementDiscounts = _agreement.AgreementDiscounts;

                if (_agreement.Prepaids != null && _agreement.Prepaids.Count > 0)
                    Variables.Agreement.Prepaids = _agreement.Prepaids;

                if (_agreement.AgreementLogs != null && _agreement.AgreementLogs.Count > 0)
                    Variables.Agreement.AgreementLogs = _agreement.AgreementLogs;
            }
        }

        private void ObtenerInformacionOrder(Model.OrderSale _orderSale)
        {
            var source = new BindingSource();
            List<CollectConceptsSumary> lCollectConcepts = new List<CollectConceptsSumary>();

            lblCuenta.SetPropertyValue(a => a.Text, _orderSale.Folio);
            lblCliente.SetPropertyValue(a => a.Text, _orderSale.TaxUser.Name);
            lblTipo.SetPropertyValue(a => a.Text, _orderSale.DescriptionType);
            if (_orderSale != null && _orderSale.TaxUser.TaxAddresses.Count > 0)
            {
                lblDireccion.SetPropertyValue(a => a.Text, _orderSale.TaxUser.TaxAddresses.First().Street
                                                            + " NO." + _orderSale.TaxUser.TaxAddresses.First().Outdoor
                                                            + " INT." + _orderSale.TaxUser.TaxAddresses.First().Indoor + "." + System.Environment.NewLine
                                                            + "COL." + _orderSale.TaxUser.TaxAddresses.First().Suburb + "." + System.Environment.NewLine
                                                            + _orderSale.TaxUser.TaxAddresses.First().Town + "." + System.Environment.NewLine
                                                            + _orderSale.TaxUser.TaxAddresses.First().State);
            }
            lblEstado.SetPropertyValue(a => a.Text, _orderSale.DescriptionStatus);
            lblObservaciones.SetPropertyValue(a => a.Text, !String.IsNullOrWhiteSpace(_orderSale.Observation)? _orderSale.Observation :"-");

            decimal total = _orderSale != null ? _orderSale.Amount - _orderSale.OnAccount : 0;
            lblTotal.SetPropertyValue(a => a.Text, string.Format(new CultureInfo("es-MX"), "{0:C2}", total));

            lCollectConcepts = _orderSale.OrderSaleDetails
                                         .Select(o => new CollectConceptsSumary
                                            {
                                                Description = o.NameConcept,
                                                Total = o.Amount - o.OnAccount
                                            }).ToList();

            source.DataSource = lCollectConcepts ?? new List<CollectConceptsSumary>();
            dgvConceptosCobro.DataSource = source;
        }

        private void CargaDeuda()
        {
            var source = new BindingSource();
            List<Model.DebtDetail> DebtDetailCollection = new List<Model.DebtDetail>();
            List<CollectConceptsSumary> lCollectConcepts = new List<CollectConceptsSumary>();
            List<CollectDebSumary> lCollectDebs = new List<CollectDebSumary>();

            if (_agreement !=null && _agreement.Debts != null && _agreement.Debts.Count > 0)
            {
                _agreement.Debts.ToList().ForEach(x =>
                {
                    var t = x;
                    x.DebtDetails.ToList().ForEach(y =>
                    {
                        var w = y;
                        DebtDetailCollection.Add(y);
                    });
                });

                lCollectConcepts = DebtDetailCollection
                                                 .GroupBy(x => new { x.CodeConcept, x.NameConcept })
                                                 .Select(g => new CollectConceptsSumary
                                                 {
                                                     Description = g.Key.NameConcept,
                                                     Total = g.Sum(s => (s.Amount - s.OnAccount))
                                                 }).ToList();             
            }

            source.DataSource = lCollectConcepts ?? new List<CollectConceptsSumary>();
            dgvConceptosCobro.DataSource = source;
        }

        private void CargaHistoricoDeuda()
        {
            var source = new BindingSource();
            List<CollectDebSumary> lCollectDebs = null;
            dgvRecibos.ColumnCount = 5;
            dgvRecibos.Columns[4].Name = "ID";
            int index = 0;
            
            

            if (_debts != null && _debts.Count > 0)
            {
               
                _debts.Where(d => d.Status != "ED006").ToList().ForEach(x =>
                {
                    dgvRecibos.Rows.Insert(index,new string[] {
                            x.DescriptionType,
                            x.FromDate.Date.ToString("dd-MM-yyyy") + " al " + x.UntilDate.Date.ToString("dd-MM-yyyy"),
                            x.DescriptionStatus,
                            x.Amount.ToString(),
                            x.Id.ToString()

                    });
                    index++;
                });
                if (dgvRecibos.Rows.Count > 1) {
                    //dgvRecibos.Rows.in
                }
                //dgvRecibos.ScrollBars = System.Windows.Forms.ScrollBars.Both;
                
                //lCollectDebs = _debts.ToList()
                //                    .Select(x => new CollectDebSumary
                //                    {
                //                        DebType = x.DescriptionType,
                //                        DebPeriod = x.FromDate.Date.ToString("dd-MM-yyyy") + " al " + x.UntilDate.Date.ToString("dd-MM-yyyy"),
                //                        DebStatus = x.DescriptionStatus,
                //                        DebAmount = x.Amount,
                //                        ID = x.Id
                //                    }).ToList();
            }

            //source.DataSource = lCollectDebs ?? new List<CollectDebSumary>();
            //dgvRecibos.DataSource = source;
            dgvRecibos.Columns[4].Visible = false;
        }

        private void CargaDescuentos()
        {
            //var source = new BindingSource();
            //List<CollectDiscountSumary> lCollectDiscount = null;

            if (_agreement != null && _agreement.AgreementDiscounts != null && _agreement.AgreementDiscounts.Count > 0)
            {
                var AgreementDiscounts = _agreement.AgreementDiscounts.OrderBy(x => x.EndDate).ToList();
                AgreementDiscounts.ForEach(x =>
                {
                    dgvDescuentos.Rows.Add(
                            x.Discount.Name,
                            x.IsActive ? "Vigente" : "-",
                            String.Format("{0:dd/MM/yyyy}", x.EndDate)
                        );
                });
                //lCollectDiscount = _agreement.AgreementDiscounts
                //                             .OrderBy(x => x.EndDate)
                //                             .Select(x => new CollectDiscountSumary
                //                             {
                //                                 Discount = x.Discount.Name,
                //                                 State = x.IsActive ? "Vigente" : "-",
                //                                 EndDate = String.Format("{0:dd/MM/yyyy}", x.EndDate)
                //                             }).ToList();
            }
            //source.DataSource = lCollectDiscount ?? new List<CollectDiscountSumary>();
            //dgvDescuentos.DataSource = source;
        }

        private void CargaAnticipos()
        {
            //var source = new BindingSource();
            //dgvAnticipos.ColumnCount = 5;
            //dgvAnticipos.Columns[4].Name = "ID";
            dgvAnticipos.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            //List<CollectPrepaidSumary> lCollectPrepaid = null;

            if (_agreement != null && _agreement.Prepaids != null && _agreement.Prepaids.Count > 0)
            {
                var p = _agreement.Prepaids.OrderByDescending(x => x.PrepaidDate).ToList();

                p.ForEach(x =>
                {
                    dgvAnticipos.Rows.Add(new string[] {
                            x.TypeDescription,
                            String.Format("{0:dd/MM/yyyy}", x.PrepaidDate),
                            x.StatusDescription,
                            x.Amount.ToString(),
                            x.Accredited.ToString(),
                            (x.Amount - x.Accredited).ToString()

                    });
                });
                //lCollectPrepaid = _agreement.Prepaids
                //                            .OrderByDescending(x => x.PrepaidDate)
                //                            .Select(x => new CollectPrepaidSumary
                //                            {
                //                                Type = x.TypeDescription,
                //                                Date = String.Format("{0:dd/MM/yyyy}", x.PrepaidDate),
                //                                Status = x.StatusDescription,
                //                                Amount = x.Amount,
                //                                Accredited = x.Accredited,
                //                                Residue = x.Amount - x.Accredited
                //                            }).ToList();
            }

            //source.DataSource = lCollectPrepaid ?? new List<CollectPrepaidSumary>();
            //dgvAnticipos.DataSource = source;
        }

        private void CargaPagos()
        {
            //var source = new BindingSource();
            //List<CollectPaymentSumary> lCollectPayment = null;

            if (_payments != null && _payments.Count>0)
            {
                var payments = _payments.Where(x => x.Status == "EP001")
                                    .OrderByDescending(x => x.PaymentDate).ToList();
                payments.ForEach( x =>
                {
                    dgvPayment.Rows.Add(
                           new string[] {
                               x.Id.ToString(),
                               String.Format("{0:dd/MM/yyyy}", x.PaymentDate),
                               x.BranchOffice,
                               x.PayMethod.Name,
                               x.Total.ToString()

                           }
                        );
                }
                );
            //    lCollectPayment = _payments.Where(x=> x.Status == "EP001")
            //                        .OrderByDescending(x => x.PaymentDate)
            //                          .Select(x => new CollectPaymentSumary
            //                          {
            //                              Id = x.Id,
            //                              DatePay = String.Format("{0:dd/MM/yyyy}", x.PaymentDate),
            //                              BranchOffice = x.BranchOffice,
            //                              PayMethod = x.PayMethod.Name,
            //                              AmountPay = x.Total
            //                          }).ToList();
            }
            //dgvPayment.Columns[0].Visible = true;
            //source.DataSource = lCollectPayment ?? new List<CollectPaymentSumary>();
            //dgvPayment.DataSource = source;
        }

        private void CargaObservaciones()
        {
            var source = new BindingSource();
            List<CollectObservationSumary> lCollectObservation = null;

            if (_agreement != null && _agreement.AgreementDetails.Count > 0 && Variables.Configuration.IsMunicipal)
            {
                int _idetail = _agreement.AgreementDetails.Max(x => x.Id);
                var _detail = _agreement.AgreementDetails.SingleOrDefault(x => x.Id == _idetail);
                lblDetalle.SetPropertyValue(a => a.Text, string.Format("Base Gravable: " + _detail.TaxableBase.ToString() +  "{0}Construcción: " + _detail.Ground.ToString() + "m2.{0}Terreno: " + _detail.Built + "m2{0}Última Actualización: " + _detail.LastUpdate.ToShortDateString(),Environment.NewLine));
            }

            if (_agreement != null && _agreement.AgreementLogs != null && _agreement.AgreementLogs.Count > 0)
            {
                lCollectObservation = _agreement.AgreementLogs
                                                .OrderByDescending(x => x.AgreementLogDate)
                                                .Select(x => new CollectObservationSumary
                                                {
                                                    DateObservation = x.AgreementLogDate.ToShortDateString(),
                                                    Observation = x.Observation
                                                }).ToList();

                lblObservaciones.SetPropertyValue(a => a.Text, !String.IsNullOrWhiteSpace(lCollectObservation.First().Observation) ? lCollectObservation.First().Observation : "-");
            }
            else
                lblObservaciones.SetPropertyValue(a => a.Text, "-");

            source.DataSource = lCollectObservation ?? new List<CollectObservationSumary>();
            dgvObservaciones.DataSource = source;
        }

        private async void CargarConvenios()
        {
            // Variables.Agreement.PartialPayments.
            if (Variables.Agreement !=  null && Variables.Agreement.PartialPayments.Count > 0)
            {
                decimal ivaConvenio = 0, restaConvenio = 0, cuotaIvaConvenio = 0, cuotaConvenio = 0;
                int numConvenios = 0;
                dataConvenios.Columns.Add("colum1", "ID");
                dataConvenios.Columns.Add("colum2", "CUENTA");
                dataConvenios.Columns.Add("colum3", "FOLIO");
                dataConvenios.Columns.Add("colum4", "FECHA");
                dataConvenios.Columns.Add("colum5", "MONTO");
                dataConvenios.Columns.Add("colum6", "IVA");
                dataConvenios.Columns.Add("colum7", "# CUOTAS");
                dataConvenios.Columns.Add("colum8", "STATUS");
                dataConvenios.Columns.Add("colum9", "EXPIRACIÓN");
                dataConvenios.ReadOnly = true;
                
                Variables.Agreement.PartialPayments.OrderBy(x => x.Status).ToList().ForEach(x =>
                {
                    var data = new List<object>() { 
                        x.Id,
                        Variables.Agreement.Account,
                        x.Folio,
                        x.PartialPaymentDate.ToString("dd-MM-yyyy"),
                       
                        string.Format(new CultureInfo("es-MX"), "{0:C2}", x.Amount),
                        12,
                        x.NumberOfPayments,
                        (x.Status =="COV01") ? "ACTIVO": "INACTIVO",
                        x.ExpirationDate.ToString("dd-MM-yyyy")
                    };
                    dataConvenios.Rows.Add(data.ToArray());
                   

                });
                ////foreach para obtener el iva de todo el convenio
                //Variables.Agreement.PartialPayments.FirstOrDefault().PartialPaymentDetails.ToList().ForEach(x =>
                //{
                //    //if(x.PartialPaymentDetailConcepts.Any(y => y.HaveTax))
                //    //{
                //    //    ivaConvenio = ivaConvenio +
                //    //}
                //    if (x.Status == "CUT01")
                //    {
                //        x.PartialPaymentDetailConcepts.ToList().ForEach(y =>
                //        {
                //            if (y.HaveTax)
                //            {
                //                ivaConvenio = ivaConvenio + Math.Round(((y.Amount * 16) / 100), 2);
                //            }
                //            restaConvenio = restaConvenio + y.Amount;
                //        });
                //    }
                //});


                var payment = Variables.Agreement.PartialPayments.FirstOrDefault().PartialPaymentDetails.ToList();
                if (payment.Count > 0)
                {
                    //dataConvenios.Columns.Add("colum1","Convenio");
                    //dataConvenios.Columns.Add("colum2", "Pendiente");
                    //dataConvenios.Columns.Add("colum3", "Subtotal");
                    //if (!Variables.Configuration.IsMunicipal) {
                    //    dataConvenios.Columns.Add("colum4", "iva");
                    //    dataConvenios.Columns.Add("colum5", "Total");
                    //}
                    //else
                    //{
                    //    dataConvenios.Columns.Add("colum4", "Total");
                    //}
                    //payment.Last().PartialPaymentDetailConcepts.ToList().ForEach(y =>
                    //{
                    //    if (y.HaveTax)
                    //    {
                    //        cuotaIvaConvenio = cuotaIvaConvenio + Math.Round(((y.Amount * 16) / 100), 2);
                    //    }
                    //    cuotaConvenio = cuotaConvenio + y.Amount;
                    //});

                    //numConvenios = Variables.Agreement.PartialPayments.FirstOrDefault().NumberOfPayments;
                    //var data = new List<object>(){ Variables.Agreement.PartialPayments.FirstOrDefault().Folio,

                    // "Pendiente - "+ (string.Format(new CultureInfo("es-MX"), "{0:C2}", (restaConvenio + ivaConvenio))) +" / Pago "+ (Variables.Agreement.PartialPayments.FirstOrDefault().PartialPaymentDetails.Where(x => x.Status == "CUT02").Count() + Variables.Agreement.PartialPayments.FirstOrDefault().PartialPaymentDetails.Where(x => x.Status == "CUT03").Count()) +" de "+ numConvenios
                    // ,
                    // string.Format(new CultureInfo("es-MX"), "{0:C2}", (cuotaConvenio))
                    //};
                    //if (!Variables.Configuration.IsMunicipal) {
                    //    data.Add((string.Format(new CultureInfo("es-MX"), "{0:C2}", cuotaIvaConvenio)));
                    //}
                    //data.Add(string.Format(new CultureInfo("es-MX"), "{0:C2}", (cuotaConvenio + cuotaIvaConvenio)));


                    //int index = dataConvenios.Rows.Add(data.ToArray());


                    this.dataConvenios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataConvenios_CellDoubleClick);
                    

                }



            }
            else
            {
                tbcInformacion.Controls.Remove(tabConvenios);
            }

        }

        private async void cargarOrdenes()
        {
            if (Variables.Agreement != null && Variables.Agreement.OrderWork!= null && Variables.Agreement.OrderWork.Count >0)
            {
                var TypeOrders = new List<object>() {
                    new { Type = "OT001", Value = "Inspeccion / Verificacion" },
                    new { Type = "OT002", Value = "Corte" },
                    new { Type = "OT003"  ,Value = "Reconexion" },
                    new {Type =  "OT004", Value = "Mantenimiento / Sustitucion" },
                    new { Type = "OT004", Value = "Mantenimiento / Sustitucion" },
                    new {Type = "OT004", Value = "Mantenimiento / Sustitucion" }
                };
                var StatusOrder = new List<object>()
                {
                    new { Status = "EOT01", Value = "GENERADA" },
                    new { Status = "EOT02", Value = "ASIGNADA" },
                    new { Status = "EOT03" , Value= "EJECUTADA" },
                    new { Status = "EOT04" , Value= "NO EJECUTADA" },
                    new { Status = "EOT05" , Value= "CANCELADA" },
                };

                List<string> data;
                //dataOrdenes.Enabled = false;
                dataOrdenes.Columns.Add("column1", "FOLIO");
                dataOrdenes.Columns.Add("column2", "FECHA ORDEN");
                dataOrdenes.Columns.Add("column3", "APLICO");
                dataOrdenes.Columns.Add("column4", "FECHA REALIZACIÓN");
                dataOrdenes.Columns.Add("column5", "ACTIVIDADES");
                dataOrdenes.Columns.Add("column6", "TIPO");
                dataOrdenes.Columns.Add("column7", "ESTADO");
                
                dataOrdenes.ReadOnly = true;



                dataOrdenes.Columns[0].Width = dataOrdenes.Columns[0].Width - 40;
                dataOrdenes.Columns[2].Width = dataOrdenes.Columns[2].Width + 40;
                dataOrdenes.Columns[3].Width = dataOrdenes.Columns[3].Width + 40;
                dataOrdenes.Columns[4].Width = dataOrdenes.Columns[4].Width + 100;
                dataOrdenes.Columns[5].Width = dataOrdenes.Columns[5].Width - 50;
                dataOrdenes.Columns[5].Width = dataOrdenes.Columns[6].Width + 10;
                Variables.Agreement.OrderWork.ToList().ForEach(x =>
                {
                    var tt = JObject.Parse(JsonConvert.SerializeObject(TypeOrders.Where(t => JObject.Parse(JsonConvert.SerializeObject(t))["Type"].ToString() == x.Type).ToList().First()))["Value"].ToString();
                    var ss = JObject.Parse(JsonConvert.SerializeObject(StatusOrder.Where(t => JObject.Parse(JsonConvert.SerializeObject(t))["Status"].ToString() == x.Status).ToList().First()))["Value"].ToString();


                    data = new List<string>(){
                    x.Folio,
                    x.DateOrder.ToString("dd-MM-yyyy"),
                    x.Applicant,
                    x.DateRealization.ToString("dd-MM-yyyy") == "01-01-0001" ? "---": x.DateRealization.ToString("dd-MM-yyyy"),
                    x.Activities,
                     tt,
                     ss,
                };


                    dataOrdenes.Rows.Add(data.ToArray());
                

                });
            }
            else
            {
                tbcInformacion.Controls.Remove(tabOrdenes);
            }
        }

        private void dataConvenios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            var row = dataConvenios.Rows[e.RowIndex];
            var partialdebt = Variables.Agreement.PartialPayments.Where(x => x.Id.ToString() == row.Cells[0].Value.ToString()).FirstOrDefault();
            if (row.Cells[7].Value.ToString() != "ACTIVO")
            {
                return ;
            }
            if (partialdebt != null) {
                var ListPaymentsConvenio = new ListPaymentsConvenio(partialdebt.PartialPaymentDetails.ToList(), partialdebt.Folio, Variables.Agreement);
                var result = ListPaymentsConvenio.ShowDialog();
                
                ListPaymentsConvenio.Close();
                if (result == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }

        }

        #endregion

        private void DgvPayment_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //return;
            if (e.ColumnIndex >= 0 && this.dgvPayment.Columns[e.ColumnIndex].Name == "XML" && e.RowIndex >= 0)
            {
                DataGridViewButtonCell c = (DataGridViewButtonCell)dgvPayment.Rows[e.RowIndex].Cells[5];
                DataGridViewRow row = this.dgvPayment.Rows[e.RowIndex];
                c.FlatStyle = FlatStyle.Popup;
                c.Style.BackColor = Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(133)))), ((int)(((byte)(214)))));
                c.Style.ForeColor = Color.White;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                c.Value = "XML";
                e.Handled = true;
            }
            else if (e.ColumnIndex >= 0 && this.dgvPayment.Columns[e.ColumnIndex].Name == "PDF" && e.RowIndex >= 0)
            {
                DataGridViewButtonCell c = (DataGridViewButtonCell)dgvPayment.Rows[e.RowIndex].Cells[6];
                DataGridViewRow row = this.dgvPayment.Rows[e.RowIndex];
                c.FlatStyle = FlatStyle.Popup;
                c.Style.BackColor = Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
                c.Style.ForeColor = Color.White;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                c.Value = "PDF";
                e.Handled = true;
            }
            else if(e.ColumnIndex >= 0 && this.dgvPayment.Columns[e.ColumnIndex].Name == "Email" && e.RowIndex >= 0)
            {
                DataGridViewButtonCell c = (DataGridViewButtonCell)dgvPayment.Rows[e.RowIndex].Cells[7];
                DataGridViewRow row = this.dgvPayment.Rows[e.RowIndex];
                c.Value = "Enviar";
            }
        }

        private void DgvPayment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvPayment.Rows[e.RowIndex];
                if (e.ColumnIndex == dgvPayment.Columns["XML"].Index && e.RowIndex >= 0)
                {
                    //if()
                    var idPayment = Convert.ToInt32(row.Cells["ID"].FormattedValue.ToString());
                    var payment = _payments.Where(x => x.Id == idPayment).FirstOrDefault().TaxReceipts;
                    var xml = payment.FirstOrDefault();
                    if(xml != null)
                    {
                        ExportGridToXML(xml.Xml.StartsWith("ï»¿") ? xml.Xml.Replace("ï»¿", "") : xml.Xml);
                    }
                    else
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "Xml no disponible, posiblemente este pago no este facturado para mayor información contactarse con el administrador del sistema.", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                }
                if(e.ColumnIndex == dgvPayment.Columns["PDF"].Index && e.RowIndex >= 0)
                {
                    var idPayment = Convert.ToInt32(row.Cells["ID"].FormattedValue.ToString());
                    var payment = _payments.Where(x => x.Id == idPayment).FirstOrDefault();
                    var xml = payment.TaxReceipts.FirstOrDefault();
                    if (xml != null)
                    {
                        if (payment.HaveTaxReceipt)
                        {
                            ExportGridToPDF(xml.PDFInvoce);
                        }
                        else
                        {
                            mensaje = new MessageBoxForm(Variables.titleprincipal, "Descarga no disponible, posiblemente este pago no este facturado por este sistema para mas información contactarse con el administrador del sistema.", TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                        }
                    }
                    else
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "PDF no disponible, posiblemente este pago no este facturado para mayor información contactarse con el administrador del sistema.", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                }
                if (e.ColumnIndex == dgvPayment.Columns["Email"].Index && e.RowIndex >= 0)
                {
                    var idPayment = Convert.ToInt32(row.Cells["ID"].FormattedValue.ToString());
                    var payment = _payments.Where(x => x.Id == idPayment).FirstOrDefault();
                    var xml = payment.TaxReceipts.FirstOrDefault();
                    var account = _payments.FirstOrDefault().Account;
                    if (xml != null)
                    {
                        SendEmail email = new SendEmail((xml.Xml.StartsWith("ï»¿") ? xml.Xml.Replace("ï»¿", "") : xml.Xml), account, lblCliente.Text, payment.HaveTaxReceipt, xml.PDFInvoce);
                        email.ShowDialog();
                    }
                    else
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "No se puede enviar, posiblemente este pago no este facturado para mayor información contactarse con el administrador del sistema.", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                }
            }
        }

        private void ExportGridToPDF(byte[] pdf)
        {
            SaveFileDialog SaveXMLFileDialog = new SaveFileDialog();
            SaveXMLFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            SaveXMLFileDialog.FilterIndex = 2;
            SaveXMLFileDialog.RestoreDirectory = true;
            SaveXMLFileDialog.Title = "Exportar PDF de Factura";
            if (SaveXMLFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllBytes(SaveXMLFileDialog.FileName, pdf);
            }
        }
        private void ExportGridToXML(string xml)
        {
            SaveFileDialog SaveXMLFileDialog = new SaveFileDialog();
            SaveXMLFileDialog.Filter = "Xml files (*.xml)|*.xml";
            SaveXMLFileDialog.FilterIndex = 2;
            SaveXMLFileDialog.RestoreDirectory = true;
            SaveXMLFileDialog.Title = "Exportar XML de Factura";
            if (SaveXMLFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(xml);
                    xdoc.Save(System.IO.File.OpenWrite(SaveXMLFileDialog.FileName));
                }
                catch (Exception)
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Por el momento no se puede descargar el xml", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                
            }
        }

        private void dgvRecibos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    public static class ControlExtension
    {
        delegate void SetPropertyValueHandler<TResult>(Control souce, Expression<Func<Control, TResult>> selector, TResult value);

        public static void SetPropertyValue<TResult>(this Control source, Expression<Func<Control, TResult>> selector, TResult value)
        {
            if (source.InvokeRequired)
            {
                var del = new SetPropertyValueHandler<TResult>(SetPropertyValue);
                source.Invoke(del, new object[] { source, selector, value });
            }
            else
            {
                var propInfo = ((MemberExpression)selector.Body).Member as PropertyInfo;
                propInfo.SetValue(source, value, null);
            }
        }
    }
 
    public partial class CollectConceptsSumary
    {
        public string Description { get; set; }
        public decimal Total { get; set; }
    }

    public partial class CollectDebSumary
    {
        public string DebType { get; set; }
        public string DebPeriod { get; set; }
        public string DebStatus { get; set; }
        public decimal DebAmount { get; set; }
        public int ID { get; set; }
    }

    public partial class CollectPrepaidSumary
    {
        public string Type { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public decimal Accredited { get; set; }
        public decimal Residue { get; set; }
    }

    public partial class CollectDiscountSumary
    {
        public string Discount { get; set; }
        public string State { get; set; }
        public string EndDate { get; set; }
    }

    public partial class CollectPaymentSumary
    {
        public int Id { get; set; }
        public string DatePay { get; set; }
        public string BranchOffice { get; set; }
        public string PayMethod { get; set; }
        public decimal AmountPay { get; set; }
    }

    public partial class CollectObservationSumary
    {
        public string DateObservation { get; set; }
        public string Observation { get; set; }
    }

    public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
    {
        public DataGridViewDisableButtonColumn()
        {
            this.CellTemplate = new DataGridViewDisableButtonCell();
        }
    }

    public class DataGridViewDisableButtonCell : DataGridViewButtonCell
    {
        private bool enabledValue;
        public bool Enabled
        {
            get
            {
                return enabledValue;
            }
            set
            {
                enabledValue = value;
            }
        }

        // Override the Clone method so that the Enabled property is copied.
        public override object Clone()
        {
            DataGridViewDisableButtonCell cell =
                (DataGridViewDisableButtonCell)base.Clone();
            cell.Enabled = this.Enabled;
            return cell;
        }

        // By default, enable the button cell.
        public DataGridViewDisableButtonCell()
        {
            this.enabledValue = true;
        }

        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // The button cell is disabled, so paint the border,  
            // background, and disabled button for the cell.
            if (!this.enabledValue)
            {
                // Draw the cell background, if specified.
                if ((paintParts & DataGridViewPaintParts.Background) ==
                    DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground =
                        new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                // Draw the cell borders, if specified.
                if ((paintParts & DataGridViewPaintParts.Border) ==
                    DataGridViewPaintParts.Border)
                {
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle,
                        advancedBorderStyle);
                }

                // Calculate the area in which to draw the button.
                Rectangle buttonArea = cellBounds;
                Rectangle buttonAdjustment =
                    this.BorderWidths(advancedBorderStyle);
                buttonArea.X += buttonAdjustment.X;
                buttonArea.Y += buttonAdjustment.Y;
                buttonArea.Height -= buttonAdjustment.Height;
                buttonArea.Width -= buttonAdjustment.Width;

                // Draw the disabled button.                
                ButtonRenderer.DrawButton(graphics, buttonArea,
                    System.Windows.Forms.VisualStyles.PushButtonState.Disabled);

                // Draw the disabled button text. 
                if (this.FormattedValue is String)
                {
                    TextRenderer.DrawText(graphics,
                        (string)this.FormattedValue,
                        this.DataGridView.Font,
                        buttonArea, SystemColors.GrayText);
                }
            }
            else
            {
                // The button cell is enabled, so let the base class 
                // handle the painting.
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
            }
        }
    }
}

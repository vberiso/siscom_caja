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

namespace SOAPAP.UI
{
    public partial class ModalFicha : Form
    {
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        String _account = String.Empty;
        List<Model.Payment> _payments = null;
        List<Model.Debt> _debts = null;
        Model.Agreement _agreement = null;
        Model.OrderSale _orderSale = null;
        Search.Type _typeSearchSelect = Search.Type.Ninguno;

        public ModalFicha(string Title, string Message, TypeIcon.Icon TypeIcon, String Account, Search.Type Type)
        {
            InitializeComponent();
            Requests = new RequestsAPI(UrlBase);
            _account = Account;
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
                    loading.Close();
                    break;
                case Search.Type.Folio:
                    
                    ConfiguracionInicial(Search.Type.Folio);
                    ObtenerFolio();
                    
                    break;
                default:
                    break;
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
                var resultAgreement = Requests.SendURIAsync(string.Format("/api/Agreements/GetSummary/{0}", _account), HttpMethod.Get, Variables.LoginModel.Token).Result;
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
                mensaje = new MessageBoxForm("Error", resultPayment.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
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
            var resultOrder =await Requests.SendURIAsync(string.Format("/api/OrderSales/Folio/{0}", _account), HttpMethod.Get, Variables.LoginModel.Token);
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
                lblTipo.SetPropertyValue(a => a.Text, _agreement.TypeIntake.Name.ToUpper());
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

            if (_debts != null && _debts.Count > 0)
            {
                lCollectDebs = _debts.ToList()
                                    .Select(x => new CollectDebSumary
                                    {
                                        DebType = x.DescriptionType,
                                        DebPeriod = x.FromDate.Date.ToString("dd-MM-yyyy") + " al " + x.UntilDate.Date.ToString("dd-MM-yyyy"),
                                        DebStatus = x.DescriptionStatus,
                                        DebAmount = x.Amount,
                                        ID = x.Id
                                    }).ToList();
            }

            source.DataSource = lCollectDebs ?? new List<CollectDebSumary>();
            dgvRecibos.DataSource = source;
            dgvRecibos.Columns[4].Visible = false;
        }

        private void CargaDescuentos()
        {
            var source = new BindingSource();
            List<CollectDiscountSumary> lCollectDiscount = null;

            if (_agreement != null && _agreement.AgreementDiscounts != null && _agreement.AgreementDiscounts.Count > 0)
            {
                lCollectDiscount = _agreement.AgreementDiscounts
                                             .OrderBy(x => x.EndDate)
                                             .Select(x => new CollectDiscountSumary
                                             {
                                                 Discount = x.Discount.Name,
                                                 State = x.IsActive ? "Vigente" : "-",
                                                 EndDate = String.Format("{0:dd/MM/yyyy}", x.EndDate)
                                             }).ToList();
            }
            source.DataSource = lCollectDiscount ?? new List<CollectDiscountSumary>();
            dgvDescuentos.DataSource = source;
        }

        private void CargaAnticipos()
        {
            var source = new BindingSource();
            List<CollectPrepaidSumary> lCollectPrepaid = null;

            if (_agreement != null && _agreement.Prepaids != null && _agreement.Prepaids.Count > 0)
            {
                lCollectPrepaid = _agreement.Prepaids
                                            .OrderByDescending(x => x.PrepaidDate)
                                            .Select(x => new CollectPrepaidSumary
                                            {
                                                Type = x.TypeDescription,
                                                Date = String.Format("{0:dd/MM/yyyy}", x.PrepaidDate),
                                                Status = x.StatusDescription,
                                                Amount = x.Amount,
                                                Accredited = x.Accredited,
                                                Residue = x.Amount - x.Accredited
                                            }).ToList();
            }

            source.DataSource = lCollectPrepaid ?? new List<CollectPrepaidSumary>();
            dgvAnticipos.DataSource = source;
        }

        private void CargaPagos()
        {
            var source = new BindingSource();
            List<CollectPaymentSumary> lCollectPayment = null;

            if (_payments != null && _payments.Count>0)
            {
                lCollectPayment = _payments.Where(x=> x.Status == "EP001")
                                    .OrderByDescending(x => x.PaymentDate)
                                      .Select(x => new CollectPaymentSumary
                                      {
                                          Id = x.Id,
                                          DatePay = String.Format("{0:dd/MM/yyyy}", x.PaymentDate),
                                          BranchOffice = x.BranchOffice,
                                          PayMethod = x.PayMethod.Name,
                                          AmountPay = x.Total
                                      }).ToList();
            }           

            source.DataSource = lCollectPayment ?? new List<CollectPaymentSumary>();
            dgvPayment.DataSource = source;
        }

        private void CargaObservaciones()
        {
            var source = new BindingSource();
            List<CollectObservationSumary> lCollectObservation = null;

            if (_agreement != null && _agreement.AgreementLogs!=null && _agreement.AgreementLogs.Count > 0)
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


        #endregion

        private void DgvPayment_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvPayment.Columns[e.ColumnIndex].Name == "XML" && e.RowIndex >= 0)
            {
                DataGridViewButtonCell c = (DataGridViewButtonCell)dgvPayment.Rows[e.RowIndex].Cells[0];
                DataGridViewRow row = this.dgvPayment.Rows[e.RowIndex];
                var idPayment = Convert.ToInt32(row.Cells["ID"].FormattedValue.ToString());
                if (_payments[e.RowIndex].BranchOffice.Contains("Migracion") && _payments.Where(x => x.Id == idPayment).FirstOrDefault().TaxReceipts.Count > 0)
                {
                   
                    c.FlatStyle = FlatStyle.Popup;
                    c.Style.BackColor = Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(133)))), ((int)(((byte)(214)))));
                    c.Style.ForeColor = Color.White;
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    c.Value = "XML";
                    e.Handled = true;
                }
            }

            if (e.ColumnIndex >= 0 && this.dgvPayment.Columns[e.ColumnIndex].Name == "PDF" && e.RowIndex >= 0)
            {
                DataGridViewButtonCell c = (DataGridViewButtonCell)dgvPayment.Rows[e.RowIndex].Cells[1];
                if (!_payments[e.RowIndex].BranchOffice.Contains("Migracion"))
                {
                    c.FlatStyle = FlatStyle.Popup;
                    c.Style.BackColor = Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
                    c.Style.ForeColor = Color.White;
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    c.Value = "PDF";
                    e.Handled = true;
                }  
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

                }
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
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xml);
                xdoc.Save(System.IO.File.OpenWrite(SaveXMLFileDialog.FileName));
            }
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

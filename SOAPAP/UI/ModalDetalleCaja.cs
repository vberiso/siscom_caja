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
using SOAPAP.Model;
using System.Threading.Tasks;

namespace SOAPAP.UI
{
    public partial class ModalDetalleCaja : Form
    {
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        Search.Type _typeSearchSelect = Search.Type.Ninguno;
        string _id = String.Empty;
        private string Title;
        Model.DebtandDiscount response;
        Model.OrderSale _orderSale = null;
        CancelProduct CanPro = null;
        string TipoCobro;
        string Cuenta;
        Boolean enEstadoDeDeuda = false;
        string json = string.Empty;

        public ModalDetalleCaja(string Title, string Message, TypeIcon.Icon TypeIcon, string Id, Search.Type Type, string pCuenta = "s/c", string pTipoCobro = "Servicio")
        {
            InitializeComponent();
            //btnAceptar.Location = new Point(198, 5);
            Requests = new RequestsAPI(UrlBase);
            _id = Id;
            _typeSearchSelect = Type;
            lblStatus.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            Cuenta = pCuenta;
            TipoCobro = pTipoCobro;
        }

        public ModalDetalleCaja(string Title, string Message, TypeIcon.Icon TypeIcon, string Id, Search.Type Type, string Status)
        {
            InitializeComponent();
            //btnAceptar.Location = new Point(198, 5);
            Requests = new RequestsAPI(UrlBase);
            _id = Id;
            _typeSearchSelect = Type;
            lblStatus.Visible = true;
            lblStatus.Text = Status;
            panel1.Visible = true;
            panel2.Visible = true;
        }
                
        private void centraX(Control padre, Control hijo)
        {
            int x = 0;
            x = (padre.Width / 2) - (hijo.Width / 2);
            hijo.Location = new System.Drawing.Point(x, hijo.Location.Y);
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private async void ModalRetiroCaja_Load(object sender, EventArgs e)
        {
            var source = new BindingSource();
            List<Model.DebtDetail> DebtDetailCollection = null;
            
            List<CollectConceptsDetail> lCollectConcepts = null;
            decimal Total = 0;
            dgvConceptosCobro.ReadOnly = true;

            dgvConceptosCobro.Columns["Amount"].DefaultCellStyle.Format = "c2";
            dgvConceptosCobro.Columns["Amount"].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvConceptosCobro.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvConceptosCobro.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvConceptosCobro.Columns["OnAccount"].DefaultCellStyle.Format = "c2";
            dgvConceptosCobro.Columns["OnAccount"].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvConceptosCobro.Columns["OnAccount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvConceptosCobro.Columns["OnAccount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvConceptosCobro.Columns["Total"].DefaultCellStyle.Format = "c2";
            dgvConceptosCobro.Columns["Total"].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvConceptosCobro.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvConceptosCobro.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvConceptosCobro.Columns["Descuento"].DefaultCellStyle.Format = "c2";
            dgvConceptosCobro.Columns["Descuento"].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvConceptosCobro.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvConceptosCobro.Columns["Descuento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvConceptosCobro.Columns["OriginalAmount"].DefaultCellStyle.Format = "c2";
            dgvConceptosCobro.Columns["OriginalAmount"].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvConceptosCobro.Columns["OriginalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvConceptosCobro.Columns["OriginalAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvConceptosCobro.Columns["Description"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            switch (_typeSearchSelect)
            {
                case Search.Type.Ninguno:
                    break;
                case Search.Type.Cuenta:
                    loading = new Loading();
                    loading.Show(this);
                    var resultDebt = await Requests.SendURIAsync(string.Format("/api/Debts/Detail/{0}", _id), HttpMethod.Get, Variables.LoginModel.Token);
                    loading.Close();

                    if (resultDebt.Contains("error"))
                    {
                        mensaje = new MessageBoxForm("Error", resultDebt.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        response = JsonConvert.DeserializeObject<Model.DebtandDiscount>(resultDebt);

                        DebtDetailCollection = response.Detail;

                        if (DebtDetailCollection != null & DebtDetailCollection.Count > 0)
                        {
                            lCollectConcepts = DebtDetailCollection
                                                .Select(d => new CollectConceptsDetail
                                                {
                                                    Id = d.Id,
                                                    Description = d.NameConcept,
                                                    //AmountDiscount = response.DebtDiscount.Where(x => x.CodeConcept == d.CodeConcept).Select(x => x.DiscountAmount).FirstOrDefault(),
                                                    OriginalAmount = response.DebtDiscount.Count > 0 ? response.DebtDiscount.Where(x => x.CodeConcept == d.CodeConcept).Select(x => x.OriginalAmount).FirstOrDefault() : d.Amount,
                                                    AmountDiscount = response.DebtDiscount.Count > 0 ? (response.DebtDiscount.Where(x => x.CodeConcept == d.CodeConcept).Select(x => x.DiscountAmount).FirstOrDefault() * -1 ) : Convert.ToDecimal(0),
                                                    Amount = d.Amount,
                                                    OnAccount = d.OnAccount,
                                                    Total = d.Amount - d.OnAccount
                                                }).ToList();
                        }

                        enEstadoDeDeuda = await validaSiHayDeuda(response.Detail[0].DebtId);
                    }
                    break;
                case Search.Type.Folio:
                    loading = new Loading();
                    loading.Show(this);
                    var resultOrder = await Requests.SendURIAsync(string.Format("/api/OrderSales/{0}", _id), HttpMethod.Get, Variables.LoginModel.Token);
                    if (resultOrder.Contains("error"))
                    {
                        loading.Close();
                        mensaje = new MessageBoxForm("Error", resultOrder.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        loading.Close();
                        _orderSale = JsonConvert.DeserializeObject<Model.OrderSale>(resultOrder);
                        if (resultOrder.Contains("error"))
                        {
                            mensaje = new MessageBoxForm("Error", resultOrder.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                        }
                        else
                        {
                            _orderSale = JsonConvert.DeserializeObject<Model.OrderSale>(resultOrder);
                            if (!string.IsNullOrEmpty(resultOrder) && _orderSale != null && _orderSale.OrderSaleDetails != null && _orderSale.OrderSaleDetails.Count > 0)
                            {
                                lCollectConcepts = _orderSale.OrderSaleDetails
                                                              .Select(d => new CollectConceptsDetail
                                                              {
                                                                  Id = d.Id,
                                                                  Description = d.NameConcept,
                                                                  Amount = d.Amount,
                                                                  OnAccount = d.OnAccount,
                                                                  Total = d.Amount - d.OnAccount
                                                              }).ToList();
                            }

                            enEstadoDeDeuda = _orderSale.Status == "EOS01" ? true : false;
                        }
                    }
                    break;
            }

            Total = lCollectConcepts != null ? lCollectConcepts.Sum(x => x.Total) : 0;
            var totalDiscount = lCollectConcepts.Sum(x => x.OriginalAmount);
            if (totalDiscount > 0) {
                lblTotalDescuento.Visible = true;
                lblTotalDescuento.Text = "Total Sin Descuento: " + string.Format(new CultureInfo("es-MX"), "{0:C2}", totalDiscount);
            }
            else
            {
                lblTotalDescuento.Visible = false;
            }
            lblTotal.Text = "Total: " + string.Format(new CultureInfo("es-MX"), "{0:C2}", Total);
            source.DataSource = lCollectConcepts;
            dgvConceptosCobro.DataSource = source;

            //Valida si se puede mostrar el boton para cancelar Productos
            if(TipoCobro == "Productos" || TipoCobro == "Predial" || TipoCobro == "Limpia")
            {
                btnSolicCancel.Text = "Solicitar Cancelación";
                if (enEstadoDeDeuda == true)
                    btnSolicCancel.Visible = true;
                else
                    btnSolicCancel.Visible = false;
            }   
            else if(TipoCobro == "Servicios")
            {
                btnSolicCancel.Text = "Solicitar Condonación";
                if (enEstadoDeDeuda == true)
                    btnSolicCancel.Visible = true;
                else
                    btnSolicCancel.Visible = false;
            }
            //btnSolicCancel.Visible = TipoCobro == "Productos" ? true : false;
            

        }

        Model.Debt tmpDebt = new Debt();
        private async Task<Boolean> validaSiHayDeuda(int pIdDebt)
        {            
            Boolean CumpleEstado = false;
            try
            {                
                var resultDebt = await Requests.SendURIAsync(string.Format("/api/Debts/id/{0}", pIdDebt), HttpMethod.Get, Variables.LoginModel.Token);
                if (resultDebt.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", resultDebt.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {                    
                    tmpDebt = JsonConvert.DeserializeObject<SOAPAP.Model.Debt>(resultDebt);

                    if(TipoCobro == "Productos" || TipoCobro == "Predial" || TipoCobro == "Limpia")
                    {
                        if (tmpDebt.Status == "ED001")
                            CumpleEstado = true;
                    }
                    else if(TipoCobro == "Servicios")
                    {
                        if (tmpDebt.Status == "ED001" || tmpDebt.Status == "ED004" || tmpDebt.Status == "ED007" || tmpDebt.Status == "ED011")
                            CumpleEstado = true;
                    }
                }                            
            }
            catch (Exception e)
            {

            }
            return CumpleEstado;
        }

        private async void btnSolicCancel_Click(object sender, EventArgs e)
        {
            CancelProduct cp = null;
            switch (_typeSearchSelect)
            {
                case Search.Type.Ninguno:
                    break;
                case Search.Type.Cuenta:
                    var temp = response;
                    cp = new CancelProduct()
                    {
                        Account = Cuenta,
                        CodeConcept = string.Join(",", temp.Detail.Select(x => x.CodeConcept).ToArray()),
                        NameConcept = string.Join(",", temp.Detail.Select(x => x.NameConcept).ToArray()),
                        TypeConcept = tmpDebt.Type == "TIP01" ? "Condonación" : "Cancelación",
                        Amount = tmpDebt.Amount,
                        RequestDate = DateTime.Now,
                        RequesterId = Variables.LoginModel.FullName,
                        AuthorisationDate = DateTime.MinValue,
                        SupervisorId = "s/d",
                        Status = "EC001",
                        Type = "TCA01",     //CON CUENTA
                        MotivoCancelacion = "s/d",
                        DebtId = temp.Detail[0].DebtId,
                        OrderSaleId = 0
                    };

                    break;
                case Search.Type.Folio:
                    var temp2 = _orderSale;
                    cp = new CancelProduct()
                    {
                        Account = Cuenta,
                        CodeConcept = temp2.OrderSaleDetails.FirstOrDefault().CodeConcept,
                        NameConcept = temp2.OrderSaleDetails.FirstOrDefault().NameConcept,
                        TypeConcept = tmpDebt.Type == "TIP01" ? "Condonación" : "Cancelación",
                        Amount = temp2.OrderSaleDetails.FirstOrDefault().Amount,
                        RequestDate = DateTime.Now,
                        RequesterId = Variables.LoginModel.FullName,
                        AuthorisationDate = DateTime.MinValue,
                        SupervisorId = "s/d",
                        Status = "EC001",
                        Type = "TCA02",     //SIN CUENTA
                        MotivoCancelacion = "s/d",
                        DebtId = 0,
                        OrderSaleId = temp2.OrderSaleDetails.FirstOrDefault().OrderSaleId
                    };
                    break;
            }

            HttpContent content;
           json = JsonConvert.SerializeObject(cp);
            content = new StringContent(json, Encoding.UTF8, "application/json");

            var _resulTransaction = await Requests.SendURIAsync("/api/CancelProducts/add", HttpMethod.Post, Variables.LoginModel.Token, content);

            if (_resulTransaction.Contains("error:"))
            {
                mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                mensaje = new MessageBoxForm( "Solicitud de Cancelación", "La solicitud fue enviada.", TypeIcon.Icon.Info, true);
                result = mensaje.ShowDialog();                
            }
        }
    }

    public partial class CollectConceptsDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal AmountDiscount { get; set; }
        public decimal Amount { get; set; }
        public decimal OnAccount { get; set; }
        public decimal Total { get; set; }
    }
}

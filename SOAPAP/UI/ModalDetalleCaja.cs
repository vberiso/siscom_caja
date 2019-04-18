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

        public ModalDetalleCaja(string Title, string Message, TypeIcon.Icon TypeIcon, string Id, Search.Type Type)
        {
            InitializeComponent();
            btnAceptar.Location = new Point(198, 5);
            Requests = new RequestsAPI(UrlBase);
            _id = Id;
            _typeSearchSelect = Type;
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
            Model.OrderSale _orderSale = null;
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
                        DebtDetailCollection = JsonConvert.DeserializeObject<List<Model.DebtDetail>>(resultDebt);

                        if (DebtDetailCollection != null & DebtDetailCollection.Count > 0)
                        {
                            lCollectConcepts = DebtDetailCollection
                                                .Select(d => new CollectConceptsDetail
                                                {
                                                    Id = d.Id,
                                                    Description = d.NameConcept,
                                                    Amount = d.Amount,
                                                    OnAccount = d.OnAccount,
                                                    Total = d.Amount - d.OnAccount
                                                }).ToList();
                        }
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
                        }
                    }
                    break;
            }

            Total = lCollectConcepts != null ? lCollectConcepts.Sum(x => x.Total) : 0;
            lblTotal.Text = "Total: " + string.Format(new CultureInfo("es-MX"), "{0:C2}", Total);
            source.DataSource = lCollectConcepts;
            dgvConceptosCobro.DataSource = source;
        }
    } 

    public partial class CollectConceptsDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal OnAccount { get; set; }
        public decimal Total { get; set; }
    }
}

using SOAPAP.Enums;
using SOAPAP.Model;
using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI
{
   
    public partial class ListPaymentsConvenio : Form
    {
        private List<PartialPaymentDetail>  PartialPaymentDetails;
        private Model.Agreement Agreement;
        private Form loading;
        private Form mensaje;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        private string folio_convenio;
        public ListPaymentsConvenio(List<PartialPaymentDetail> PartialPaymentDetails, string folio_convenio, Model.Agreement Agreement)
        {
            Requests = new RequestsAPI(UrlBase);
            this.Agreement = Agreement;
            this.PartialPaymentDetails = PartialPaymentDetails;
            this.folio_convenio = folio_convenio;
            InitializeComponent();
        }

        private void ListPaymentsConvenio_Load(object sender, EventArgs e)
        {
            lblDetalleConvenio.Text = lblDetalleConvenio.Text + " " + folio_convenio;
            dataPaymentsConvenio.Columns.Add("column1","# DE PAGO");
            dataPaymentsConvenio.Columns.Add("column2", "IMPORTE");
            dataPaymentsConvenio.Columns.Add("column3", "IVA");
            dataPaymentsConvenio.Columns.Add("column4", "EN CUENTA");
            dataPaymentsConvenio.Columns.Add("column5", "ESTADO");
            dataPaymentsConvenio.Columns.Add("column6", "CALENDARIO DE PAGO");
            dataPaymentsConvenio.Columns.Add("column7", "LIBERACIÓN");
            dataPaymentsConvenio.Columns.Add("column8", "FECHA PAGO");
            List<object> data;
            PartialPaymentDetails.OrderBy(x => x.PaymentNumber).ToList().ForEach(x =>{
                data = new List<object>() {
                    x.PaymentNumber,
                     string.Format(new CultureInfo("es-MX"), "{0:C2}", x.Amount),
                    string.Format(new CultureInfo("es-MX"), "{0:C2}",
                    x.PartialPaymentDetailConcepts.ToList().Where(ppc => ppc.HaveTax).Sum(ppc => ppc.Amount) * 16 /100),
                     string.Format(new CultureInfo("es-MX"), "{0:C2}",x.OnAccount),
                     x.Status == "CUT03"?"Pagado" : (x.Status == "CUT02" ? "Liberado" : "Por liberar"),
                    x.ReleasePeriod.ToString("dd-MM-yyyy"),
                     x.RelaseDate.ToString("dd-MM-yyyy") == "01-01-1900"?"---": x.RelaseDate.ToString("dd-MM-yyyy"),
                     x.PaymentDate.ToString("dd-MM-yyyy") == "01-01-1900"?"---": x.PaymentDate.ToString("dd-MM-yyyy")

                };
                dataPaymentsConvenio.Rows.Add(data.ToArray());
            });

            this.dataPaymentsConvenio.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataPaymentsConvenio_CellClick);
        }


        private void dataPaymentsConvenio_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void btnLiberar_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            var results = await Requests.SendURIAsync("api/PartialPayment/billingPartialPayment/" + Agreement.Id, HttpMethod.Post, Variables.LoginModel.Token);
            if (results.Contains("errror") || results == "")
            {
                mensaje = new MessageBoxForm("Error", "Ocurrio un error al intentar liberar la cuota, por favor comunicase con el administrador", TypeIcon.Icon.Cancel);
                mensaje.ShowDialog();
                mensaje.Close();
                loading.Close();
            }
            else
            {
                mensaje = new MessageBoxForm("Éxito", "Cuota liberada correctamente", TypeIcon.Icon.Success);
                mensaje.ShowDialog();
                mensaje.Close();
                loading.Close();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            

        }

    
    }
}

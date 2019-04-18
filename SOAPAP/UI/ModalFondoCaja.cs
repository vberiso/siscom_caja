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

namespace SOAPAP.UI
{
    public partial class ModalFondoCaja : Form
    {
        HttpContent content;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;
        decimal CashBox = 0;
        querys q = new querys();

        public ModalFondoCaja(decimal pCashBox)
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();

            if (pCashBox > 0)
            {
                lblCashBox.Text = String.Format("El monto máximo es : ${0}", pCashBox);
                CashBox = pCashBox;
            }

            btnCancelar.Visible = true;
            btnCancelar.Location = new Point(302, 5);
            btnAceptar.Location = new Point(198, 5);
        }

        private void centraX(Control padre, Control hijo)
        {
            int x = 0;
            x = (padre.Width / 2) - (hijo.Width / 2);
            hijo.Location = new System.Drawing.Point(x, hijo.Location.Y);
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            bool on = false;
            if (CashBox > 0)
            {
                if (nudAmount.Value > CashBox)
                {
                    mensaje = new MessageBoxForm("Error", "El monto ingresado es mayor al configurado para esta terminal", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                    return;
                }
            }

            loading = new Loading();
            loading.Show(this);

            SOAPAP.Model.Transaction transaction = new SOAPAP.Model.Transaction();
            transaction.Sign = true;
            transaction.Amount = nudAmount.Value;
            transaction.Aplication = "SISCOMCAJA";
            transaction.TypeTransactionId = 2;
            transaction.PayMethodId = 1;
            transaction.TerminalUserId = Variables.Configuration.Terminal.TerminalUsers.First().Id;

            string valores = JsonConvert.SerializeObject(transaction);
            content = new StringContent(valores, Encoding.UTF8, "application/json");
            var resultado = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", Variables.Configuration.Terminal.TerminalUsers.First().Id), HttpMethod.Post, Variables.LoginModel.Token, content);
            loading.Close();
            if (resultado.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultado.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                mensaje = new MessageBoxForm("Transacción Exitosa", "Fondo de caja ingresado con éxito", TypeIcon.Icon.Success);
                result = mensaje.ShowDialog();
                this.DialogResult = DialogResult.OK;
                on = q.EstaEnLineaLaImpresora(q.ImpresoraPredeterminada());
                if (on == true)
                {
                    if (Properties.Settings.Default.Printer == true)
                    {
                        q.sacarcaja(q.ImpresoraPredeterminada(), Variables.Configuration.ANSII);
                    }
                }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

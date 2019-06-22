using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOAPAP.Enums;
using SOAPAP.Model;
using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI
{
    public partial class Splash : Form
    {
        private int progress = 0;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        private int progressn = 5;
        public Splash()
        {
            InitializeComponent();
            this.progressBar.Maximum = 120;
            Requests = new RequestsAPI(UrlBase);
            GetConfigurations();
        }

        private async void GetConfigurations()
        {
            var getKey = System.Guid.NewGuid().ToString().Substring(0, 20).ToUpper();

            Configuration configuration = new Configuration();
            Variables.Configuration = configuration;

            lblProgress.Text = "Conectando.....";
            var data = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=RFC", HttpMethod.Get));
            if (data.Contains("error"))
            {
                DialogResult result = new DialogResult();
                Form mensaje = new MessageBoxForm("Error", data.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            var a = await Requests.SendURIAsync(string.Format("/api/ValueParameters/{0}", Requests.GETMacAddress()), HttpMethod.Get);
            //var a = await Requests.SendURIAsync("/api/ValueParameters/D4BED9836A58", HttpMethod.Get);
            if (a.Contains("error"))
            {
                DialogResult result = new DialogResult();
                Form mensaje = new MessageBoxForm("Error", a.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            configuration.Terminal = JsonConvert.DeserializeObject<SOAPAP.Model.Terminal>(a);
            lblProgress.Text = "Obteniendo Configuración de la Terminal ...";
            if (configuration.Terminal != null)
            {
                lblSerial.Text = "Licencia: " + configuration.Terminal.SerialNumber ?? "";
            }
            else
            {
                lblSerial.Text = "Sin Licencia";
            }
            /*1*/
            RunProgress(progressn);
            configuration.RFC = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=RFC", HttpMethod.Get));
            lblProgress.Text = "Obteniendo RFC ...";
            /*2*/
            RunProgress(progressn);
            configuration.ANSII = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=ANSII", HttpMethod.Get));
            lblProgress.Text = "Obteniendo Configuración de Caja ...";
            /*3*/
            RunProgress(progressn);
            configuration.CompanyName = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=NOMBRE_EMPRESA", HttpMethod.Get));
            lblProgress.Text = "Obteniendo Nombre de la Compañía ...";
            /*4*/
            RunProgress(progressn);
            configuration.Address = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=DIRECCION", HttpMethod.Get));
            lblProgress.Text = "Obteniendo Dirección ...";
            /*5*/
            RunProgress(progressn);
            configuration.Phone = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=TELEFONO", HttpMethod.Get));
            lblProgress.Text = "Obteniendo Teléfono ...";
            /*6*/
            RunProgress(progressn);
            configuration.Image = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=IMAGEN", HttpMethod.Get));
            lblProgress.Text = "Obteniendo Identidad Corporativa ...";
            /*7*/
            RunProgress(progressn);
            configuration.Email = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CORREO", HttpMethod.Get));
            lblProgress.Text = "Obteniendo Email ...";
            /*8*/
            RunProgress(progressn);
            configuration.IVA = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=IVA", HttpMethod.Get));
            lblProgress.Text = "Obteniendo Valor de Impuestos ...";
            /*9*/
            RunProgress(progressn);
            configuration.LegendRegime = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=LEYENDA_REGIMEN", HttpMethod.Get));
            lblProgress.Text = "Obteniendo Régimen Fiscal ...";
            /*10*/
            RunProgress(progressn);
            configuration.IsMunicipal = (ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=ISMUNICIPAL", HttpMethod.Get)) == "SI") ? true : false;
            lblProgress.Text = "Obteniendo Tipo de Aplicación ...";
            /*11*/
            RunProgress(progressn);
            configuration.minimumsalary = ValidResponses(await Requests.SendURIAsync("/api/ValueParameters?value=FACTOR", HttpMethod.Get));
            lblProgress.Text = "Obteniendo Salario Mínimo ...";
            /*12*/
            RunProgress(progressn);
            configuration.CFDI = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CFDI", HttpMethod.Get));
            lblProgress.Text = "Obteniendo CFDI ...";
            /*13*/
            RunProgress(progressn);
            configuration.CFDITest = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CFDITEST", HttpMethod.Get));
            lblProgress.Text = "Obteniendo CFDITest ...";
            /*14*/
            RunProgress(progressn);
            configuration.CFDIUser = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CFDIUSER", HttpMethod.Get));
            lblProgress.Text = "Obteniendo CFDIUser ...";
            /*15*/
            RunProgress(progressn);
            configuration.CFDIPassword = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CFDIPASSWORD", HttpMethod.Get));
            lblProgress.Text = "Obteniendo CFDIPassword ...";
            /*16*/
            RunProgress(progressn);
            configuration.CFDIKeyCancel = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CFDIKEYCANCEL", HttpMethod.Get));
            lblProgress.Text = "Obteniendo CFDIKeyCancel ...";
            /*17*/
            RunProgress(progressn);
            configuration.CFDICertificado = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CFDICERTIFICADO", HttpMethod.Get));
            lblProgress.Text = "Obteniendo CFDICFDICERTIFICADO ...";
            /*18*/
            RunProgress(progressn);

            if (configuration.Terminal == null)
            {
                var c = await Requests.SendURIAsync(string.Format("/api/ValueParameters/Terminal/{0}", 0 + ""), HttpMethod.Get);
                configuration.StateOperation = Convert.ToInt16((c == "") ? 0 : Convert.ToInt32(c));
                lblProgress.Text = "Obteniendo Estado de Operación ...";
                /*19*/
                RunProgress(progressn);
            }
            else
            {
                if (configuration.Terminal.TerminalUsers.Count == 0)
                {
                    var c = await Requests.SendURIAsync(string.Format("/api/ValueParameters/Terminal/{0}", 0 + ""), HttpMethod.Get);
                    configuration.StateOperation = Convert.ToInt16((c == "") ? 0 : Convert.ToInt32(c));
                    lblProgress.Text = "Obteniendo Estado de Operación ...";
                    /*19*/
                    RunProgress(progressn);
                }
                else
                {
                    configuration.StateOperation = Convert.ToInt16(await Requests.SendURIAsync(string.Format("/api/ValueParameters/Terminal/{0}", configuration.Terminal.TerminalUsers.FirstOrDefault().Id), HttpMethod.Get));
                    lblProgress.Text = "Obteniendo Estado de Operación ...";
                    /*19*/
                    RunProgress(progressn);
                }
            }
            /*20*/
            configuration.Anual = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=ANUAL", HttpMethod.Get)) == "" ? false : true;
            lblProgress.Text = "Obteniendo Caracteristicas de Pago ...";
            RunProgress(progressn);

            /*21*/
            configuration.StringURLFirebase = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=STRINGURLFIREBASE", HttpMethod.Get));
            lblProgress.Text = "Obteniendo Dirección de Notificaciones ...";
            RunProgress(progressn);

            /*22*/
            configuration.DefaultPrinter = Requests.ImpresoraPredeterminada();
            lblProgress.Text = "Obteniendo Impresora Predeterminada ...";
            RunProgress(progressn);

            /*23*/
            var campaign = await Requests.SendURIAsync("/api/ValueParameters/Campaign", HttpMethod.Get);
            if (campaign.Contains("error"))
            {
                DialogResult result = new DialogResult();
                Form mensaje = new MessageBoxForm("Error", campaign.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                configuration.DiscountCampaigns = JsonConvert.DeserializeObject<List<DiscountCampaign>>(campaign);
                lblProgress.Text = "Obteniendo Descuentos Disponibles ...";
                RunProgress(progressn);
            }

            /*24*/
            decimal Percentage = 0;
            Decimal.TryParse(ValidResponses(await Requests.SendURIAsync("/api/ValueParameters?value=AIM", HttpMethod.Get)), out Percentage);
            configuration.Percentage = Percentage;
            lblProgress.Text = "Obteniendo AIM ...";
            RunProgress(progressn);

            if (configuration.Terminal == null)
            {
                DialogResult result = new DialogResult();
                Form mensaje = new MessageBoxForm("Alerta", "Esta Terminal no se encuentra registrada en el sistema", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Thread t = new Thread(new ThreadStart(ThreadProc));
                    t.SetApartmentState(ApartmentState.STA);
                    t.Start();
                    this.Close();
                }
            }
            else
            {
                Thread t = new Thread(new ThreadStart(ThreadProc));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                this.Close();
            }
        }

        private string ValidResponse(string jObject)
        {
            try
            {
                return JObject.Parse(jObject)["textColumn"].ToString();
            }
            catch (Exception)
            {

                if (jObject.Contains("error"))
                {
                    DialogResult result = new DialogResult();
                    Form mensaje = new MessageBoxForm("Error", jObject.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                return "";
            }
           
        }

        private string ValidResponses(string jObject)
        {
            try
            {
                return JObject.Parse(jObject)["numberColumn"].ToString();
            }
            catch (Exception)
            {

                if (jObject.Contains("error"))
                {
                    DialogResult result = new DialogResult();
                    Form mensaje = new MessageBoxForm("Error", jObject.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                return "";
            }

        }


        private void ThreadProc()
        {
            Application.Run(new Login());
        }

        private void RunProgress(double progress)
        {
            this.progressBar.ForeColor = Color.Red;
            this.progress += (int)progress;
            this.progressBar.Value = (int)this.progress;
        }

        private void Splash_Load(object sender, EventArgs e)
        {

        }
    }
}

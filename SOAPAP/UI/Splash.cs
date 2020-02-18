using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOAPAP.Enums;
using SOAPAP.Model;
using SOAPAP.PDFManager;
using SOAPAP.Services;
using SOAPAP.Services.UpdateApplication;
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

            InstallUpdateSyncWithInfo.InstallUpdateSyncWithInfoApplication();

            //var Authentification = Convert.ToBase64String(
            //System.Text.ASCIIEncoding.ASCII.GetBytes(
            //   $"{Properties.Settings.Default.FacturamaUser}:{Properties.Settings.Default.FacturamaPassword}"));

            var version = await Requests.SendURIAsync("/api/VersionApps", HttpMethod.Get);
            if (version.Contains("error"))
            {
                try
                {
                    DialogResult result = new DialogResult();
                    Form mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(version).error, TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    this.Close();
                }
                catch (Exception)
                {
                    DialogResult result = new DialogResult();
                    Form mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                List<VersionApp> versionApp = JsonConvert.DeserializeObject<List<VersionApp>>(version);
                configuration.VersionApp = versionApp.FirstOrDefault();
            }
            
            if (configuration.VersionApp == null)
            {
                DialogResult result = new DialogResult();
                Form mensaje = new MessageBoxForm("Error", "La aplicación no está disponible por el momento estamos trabajando para mejorar el producto, disculpe las molestias", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
                this.Close();
            }

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

            int resParams = await obtenerParametrosDeSystemParameter(configuration);

            ///*1*/
            //RunProgress(progressn);
            //configuration.RFC = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=RFC", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo RFC ...";
            ///*2*/
            //RunProgress(progressn);
            //configuration.ANSII = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=ANSII", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo Configuración de Caja ...";
            ///*3*/
            //RunProgress(progressn);
            //configuration.CompanyName = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=NOMBRE_EMPRESA", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo Nombre de la Compañía ...";
            ///*4*/
            //RunProgress(progressn);
            //configuration.Address = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=DIRECCION", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo Dirección ...";
            ///*5*/
            //RunProgress(progressn);
            //configuration.Phone = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=TELEFONO", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo Teléfono ...";
            ///*6*/
            //RunProgress(progressn);
            //configuration.Image = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=IMAGEN", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo Identidad Corporativa ...";
            ///*7*/
            //RunProgress(progressn);
            //configuration.Email = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CORREO", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo Email ...";
            ///*8*/
            //RunProgress(progressn);
            //configuration.IVA = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=IVA", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo Valor de Impuestos ...";
            ///*9*/
            //RunProgress(progressn);
            //configuration.LegendRegime = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=LEYENDA_REGIMEN", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo Régimen Fiscal ...";
            /*10*/
            //RunProgress(progressn);
            //configuration.IsMunicipal = (ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=ISMUNICIPAL", HttpMethod.Get)) == "SI") ? true : false;
            //lblProgress.Text = "Obteniendo Tipo de Aplicación ...";
            ///*11*/
            //RunProgress(progressn);
            //configuration.minimumsalary = ValidResponses(await Requests.SendURIAsync("/api/ValueParameters?value=FACTOR", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo Salario Mínimo ...";
            ///*12*/
            //RunProgress(progressn);
            //configuration.CFDI = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CFDI", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo CFDI ...";
            ///*13*/
            //RunProgress(progressn);
            //configuration.CFDITest = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CFDITEST", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo CFDITest ...";
            ///*14*/
            //RunProgress(progressn);
            //configuration.CFDIUser = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CFDIUSER", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo CFDIUser ...";
            ///*15*/
            //RunProgress(progressn);
            //configuration.CFDIPassword = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CFDIPASSWORD", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo CFDIPassword ...";
            ///*16*/
            //RunProgress(progressn);
            //configuration.CFDIKeyCancel = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CFDIKEYCANCEL", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo CFDIKeyCancel ...";
            ///*17*/
            //RunProgress(progressn);
            //configuration.CFDICertificado = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=CFDICERTIFICADO", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo CFDICFDICERTIFICADO ...";
            ///*18*/
            //RunProgress(progressn);

            //if (configuration.Terminal == null)
            //{
            //    var c = await Requests.SendURIAsync(string.Format("/api/ValueParameters/Terminal/{0}", 0 + ""), HttpMethod.Get);
            //    configuration.StateOperation = Convert.ToInt16((c == "") ? 0 : Convert.ToInt32(c));
            //    lblProgress.Text = "Obteniendo Estado de Operación ...";
            //    /*19*/
            //    RunProgress(progressn);
            //}
            //else
            //{
            //    if (configuration.Terminal.TerminalUsers.Count == 0)
            //    {
            //        var c = await Requests.SendURIAsync(string.Format("/api/ValueParameters/Terminal/{0}", 0 + ""), HttpMethod.Get);
            //        configuration.StateOperation = Convert.ToInt16((c == "") ? 0 : Convert.ToInt32(c));
            //        lblProgress.Text = "Obteniendo Estado de Operación ...";
            //        /*19*/
            //        RunProgress(progressn);
            //    }
            //    else
            //    {
            //        configuration.StateOperation = Convert.ToInt16(await Requests.SendURIAsync(string.Format("/api/ValueParameters/Terminal/{0}", configuration.Terminal.TerminalUsers.FirstOrDefault().Id), HttpMethod.Get));
            //        lblProgress.Text = "Obteniendo Estado de Operación ...";
            //        /*19*/
            //        RunProgress(progressn);
            //    }
            //}
            ///*20*/
            //configuration.Anual = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=ANUAL", HttpMethod.Get)) == "" ? false : true;
            //lblProgress.Text = "Obteniendo Caracteristicas de Pago ...";
            //RunProgress(progressn);

            ///*21*/
            //configuration.StringURLFirebase = ValidResponse(await Requests.SendURIAsync("/api/ValueParameters?value=STRINGURLFIREBASE", HttpMethod.Get));
            //lblProgress.Text = "Obteniendo Dirección de Notificaciones ...";
            //RunProgress(progressn);

            ///*22*/
            //configuration.DefaultPrinter = Requests.ImpresoraPredeterminada();
            //lblProgress.Text = "Obteniendo Impresora Predeterminada ...";
            //RunProgress(progressn);

            ///*23*/
            //var campaign = await Requests.SendURIAsync("/api/ValueParameters/Campaign", HttpMethod.Get);
            //if (campaign.Contains("error"))
            //{
            //    DialogResult result = new DialogResult();
            //    Form mensaje = new MessageBoxForm("Error", campaign.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
            //    result = mensaje.ShowDialog();
            //}
            //else
            //{
            //    configuration.DiscountCampaigns = JsonConvert.DeserializeObject<List<DiscountCampaign>>(campaign);
            //    lblProgress.Text = "Obteniendo Descuentos Disponibles ...";
            //    //RunProgress(progressn);
            //}

            ///*24*/
            //var condonations = await Requests.SendURIAsync("/api/ValueParameters/Condonations", HttpMethod.Get);
            //if (condonations.Contains("error"))
            //{
            //    DialogResult result = new DialogResult();
            //    Form mensaje = new MessageBoxForm("Error", condonations.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
            //    result = mensaje.ShowDialog();
            //}
            //else
            //{
            //    configuration.CondonationCampaings = JsonConvert.DeserializeObject<List<CondonationCampaing>>(condonations);
            //    lblProgress.Text = "Obteniendo promociónes de condonacion disponibles ...";
            //    RunProgress(progressn);
            //}

            ///*25*/
            //decimal Percentage = 0;
            //Decimal.TryParse(ValidResponses(await Requests.SendURIAsync("/api/ValueParameters?value=AIM", HttpMethod.Get)), out Percentage);
            //configuration.Percentage = Percentage;
            //lblProgress.Text = "Obteniendo AIM ...";
            //RunProgress(progressn);



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

        private async Task<int> obtenerParametrosDeSystemParameter(Configuration configuration)
        {            
            var resParametros = await Requests.SendURIAsync("/api/ValueParameters/GetAllParameters", HttpMethod.Get);
            if (resParametros.Contains("error:"))
            {
                return -1;
            }
            else
            {
                var lstParametros = JsonConvert.DeserializeObject<List<Model.SystemParameters>>(resParametros);

                /*1*/
                RunProgress(progressn);
                configuration.RFC = lstParametros.FirstOrDefault(x => x.Name.Contains("RFC")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("RFC")).TextColumn; 
                lblProgress.Text = "Obteniendo RFC ...";

                /*2*/
                RunProgress(progressn);
                configuration.ANSII = lstParametros.FirstOrDefault(x => x.Name.Contains("ANSII")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("ANSII")).TextColumn;
                lblProgress.Text = "Obteniendo Configuración de Caja ...";

                /*3*/
                RunProgress(progressn);
                configuration.CompanyName = lstParametros.FirstOrDefault(x => x.Name.Contains("NOMBRE_EMPRESA")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("NOMBRE_EMPRESA")).TextColumn;
                lblProgress.Text = "Obteniendo Nombre de la Compañía ...";

                /*4*/
                RunProgress(progressn);
                configuration.Address = lstParametros.FirstOrDefault(x => x.Name.Contains("DIRECCION")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("DIRECCION")).TextColumn;
                lblProgress.Text = "Obteniendo Dirección ...";

                /*5*/
                RunProgress(progressn);
                configuration.Phone = lstParametros.FirstOrDefault(x => x.Name.Contains("TELEFONO")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("TELEFONO")).TextColumn;
                lblProgress.Text = "Obteniendo Teléfono ...";
                
                /*6*/
                RunProgress(progressn);
                configuration.Image = lstParametros.FirstOrDefault(x => x.Name.Contains("IMAGEN")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("IMAGEN")).TextColumn;
                lblProgress.Text = "Obteniendo Identidad Corporativa ...";

                /*7*/
                RunProgress(progressn);
                configuration.Email = lstParametros.FirstOrDefault(x => x.Name.Contains("CORREO")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("CORREO")).TextColumn;
                lblProgress.Text = "Obteniendo Email ...";

                /*8*/
                RunProgress(progressn);
                configuration.IVA = lstParametros.FirstOrDefault(x => x.Name.Contains("IVA")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("IVA")).TextColumn;
                lblProgress.Text = "Obteniendo Valor de Impuestos ...";

                /*9*/
                RunProgress(progressn);
                configuration.LegendRegime = lstParametros.FirstOrDefault(x => x.Name.Contains("LEYENDA_REGIMEN")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("LEYENDA_REGIMEN")).TextColumn;
                lblProgress.Text = "Obteniendo Régimen Fiscal ...";

                /*10*/
                RunProgress(progressn);
                if(lstParametros.FirstOrDefault(x => x.Name.Contains("ISMUNICIPAL")) != null)
                {
                    configuration.IsMunicipal = lstParametros.FirstOrDefault(x => x.Name.Contains("ISMUNICIPAL")).TextColumn.Contains("S") ? true : false;
                    lblProgress.Text = "Obteniendo Tipo de Aplicación ...";
                }

                /*11*/
                RunProgress(progressn);
                configuration.minimumsalary = lstParametros.FirstOrDefault(x => x.Name.Contains("FACTOR")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("FACTOR")).NumberColumn.ToString();
                lblProgress.Text = "Obteniendo Salario Mínimo ...";

                /*12*/
                RunProgress(progressn);
                configuration.CFDI = lstParametros.FirstOrDefault(x => x.Name.Contains("CFDI")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("CFDI")).TextColumn;
                lblProgress.Text = "Obteniendo CFDI ...";

                /*13*/
                RunProgress(progressn);
                configuration.CFDITest = lstParametros.FirstOrDefault(x => x.Name.Contains("CFDITEST")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("CFDITEST")).TextColumn;
                lblProgress.Text = "Obteniendo CFDITest ...";

                /*14*/
                RunProgress(progressn);
                configuration.CFDIUser = lstParametros.FirstOrDefault(x => x.Name.Contains("CFDIUSER")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("CFDIUSER")).TextColumn;
                lblProgress.Text = "Obteniendo CFDIUser ...";

                /*15*/
                RunProgress(progressn);
                configuration.CFDIPassword = lstParametros.FirstOrDefault(x => x.Name.Contains("CFDIPASSWORD")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("CFDIPASSWORD")).TextColumn;
                lblProgress.Text = "Obteniendo CFDIPassword ...";

                /*16*/
                RunProgress(progressn);
                configuration.CFDIKeyCancel = lstParametros.FirstOrDefault(x => x.Name.Contains("CFDIKEYCANCEL")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("CFDIKEYCANCEL")).TextColumn;
                lblProgress.Text = "Obteniendo CFDIKeyCancel ...";

                /*17*/
                RunProgress(progressn);
                configuration.CFDICertificado = lstParametros.FirstOrDefault(x => x.Name.Contains("CFDICERTIFICADO")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("CFDICERTIFICADO")).TextColumn;
                lblProgress.Text = "Obteniendo CFDICFDICERTIFICADO ...";

                /*20*/
                if (lstParametros.FirstOrDefault(x => x.Name.Contains("ANUAL")) != null)
                {
                    configuration.Anual = lstParametros.FirstOrDefault(x => x.Name.Contains("ANUAL")).TextColumn == "" ? false : true;
                    lblProgress.Text = "Obteniendo Caracteristicas de Pago ...";
                    RunProgress(progressn);
                }
                else
                    configuration.Anual = false;

                /*21*/
                configuration.StringURLFirebase = lstParametros.FirstOrDefault(x => x.Name.Contains("STRINGURLFIREBASE")) == null ? "" : lstParametros.FirstOrDefault(x => x.Name.Contains("STRINGURLFIREBASE")).TextColumn;
                lblProgress.Text = "Obteniendo Dirección de Notificaciones ...";
                RunProgress(progressn);



                /*22*/
                configuration.DefaultPrinter = Requests.ImpresoraPredeterminada();
                lblProgress.Text = "Obteniendo Impresora Predeterminada ...";
                RunProgress(progressn);

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
                    //RunProgress(progressn);
                }

                /*24*/
                var condonations = await Requests.SendURIAsync("/api/ValueParameters/Condonations", HttpMethod.Get);
                if (condonations.Contains("error"))
                {
                    DialogResult result = new DialogResult();
                    Form mensaje = new MessageBoxForm("Error", condonations.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    configuration.CondonationCampaings = JsonConvert.DeserializeObject<List<CondonationCampaing>>(condonations);
                    lblProgress.Text = "Obteniendo promociónes de condonacion disponibles ...";
                    RunProgress(progressn);
                }

                /*25*/
                decimal Percentage = 0;
                Decimal.TryParse(ValidResponses(await Requests.SendURIAsync("/api/ValueParameters?value=AIM", HttpMethod.Get)), out Percentage);
                configuration.Percentage = Percentage;
                lblProgress.Text = "Obteniendo AIM ...";
                RunProgress(progressn);

                return 1;
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

        public T DeserializerXML<T>(string xmlString) where T : class
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SOAPAP.Facturado.Comprobante));
            //StringReader stringReader = new StringReader(xmlString);
            //Comprobante comprobante = (Comprobante)serializer.Deserialize(stringReader);
            using (System.IO.TextReader reader = new System.IO.StringReader(xmlString))
            {
                return (T)serializer.Deserialize(reader);
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

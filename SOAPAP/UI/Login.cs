using System;
using System.Windows.Forms;
using System.Threading;
using SOAPAP.Services;
using SOAPAP.UI;
using SOAPAP.Enums;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using SOAPAP.Model;
using System.Deployment.Application;
using System.Text;

namespace SOAPAP
{
    public partial class Login : Form
    {
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        DialogResult result = new DialogResult();
        Form mensaje;

        public Login()
        {
            InitializeComponent();
            Requests = new RequestsAPI(UrlBase);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            pictureBox1.Parent = pictureBox2;
            label1.Parent = pictureBox2;
            lblEmpresa.Text = Variables.Configuration.CompanyName;
            lblEmpresa.Parent = pictureBox2;
            pictureBox1.Visible = true;
        }

        private void pbxClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pbxClose_MouseHover(object sender, EventArgs e)
        {
            pbxClose.Image = Properties.Resources.cerrar_over;
        }

        private void pbxClose_MouseLeave(object sender, EventArgs e)
        {
            pbxClose.Image = Properties.Resources.cerrar;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Ingresar();
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.UseVisualStyleBackColor = true;
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Ingresar();
            }
        }

        #region PrivateMethod
        private async void Ingresar()
        {
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                mensaje = new MessageBoxForm("Error", "Ingrese el Usuario", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }

            else if (string.IsNullOrEmpty(txtPass.Text))
            {
                mensaje = new MessageBoxForm("Error", "Ingrese la contraseña", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                Form loading = new Loading();
                loading.Show(this);
                var response = await Requests.Login(txtUsuario.Text, txtPass.Text);
                if (response.Contains("error"))
                {                   
                    mensaje = new MessageBoxForm("Error", response.Split('/')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    loading.Close();
                    result = mensaje.ShowDialog();
                    txtPass.Text = String.Empty;
                }
                else
                {
                    if(Variables.LoginModel.Divition == 9 || Variables.LoginModel.Divition == 10)
                    {
                        loading.Close();
                        mensaje = new MessageBoxForm("Error", "El usuario pertenece a un área que no permite el uso de esta aplicación, disculpe las molestias", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        var terminal = await Requests.SendURIAsync(string.Format("/api/ValueParameters/{0}", Requests.GETMacAddress()), HttpMethod.Get);
                        if (terminal.Contains("error"))
                        {
                            mensaje = new MessageBoxForm("Error", terminal.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                        }
                        else
                        {
                            Variables.Configuration.Terminal = JsonConvert.DeserializeObject<SOAPAP.Model.Terminal>(terminal);
                            if (Variables.Configuration.Terminal == null)
                            {
                                if (Variables.LoginModel.RolName.ToList().Find(x => x.Contains("Admin")) != null)
                                {
                                    Thread t = new Thread(new ThreadStart(ThreadProc));
                                    t.SetApartmentState(ApartmentState.STA);
                                    t.Start();
                                    this.Close();
                                }
                                else
                                {
                                    mensaje = new MessageBoxForm("Error", "La terminal no se encuentra registrada en el sistema y no cuenta con el perfil para realizar esa acción", TypeIcon.Icon.Cancel);
                                    result = mensaje.ShowDialog();
                                }
                            }
                            else
                            {
                                LogginLog log = new LogginLog
                                {
                                    VersionSiscom = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4),
                                    BranchOffice = Variables.Configuration.Terminal.BranchOffice.Name,
                                    User = Variables.LoginModel.User,
                                    UserName = Variables.LoginModel.FullName,
                                    Fecha = DateTime.Now,
                                    Terminal = Variables.Configuration.Terminal.MacAdress
                                };
                                StringContent content = new StringContent(JsonConvert.SerializeObject(log), Encoding.UTF8, "application/json");
                                var request = await Requests.SendURIAsync("/api/Auth/Logger", HttpMethod.Post, content);
                                if (request.Contains("error"))
                                {
                                    try
                                    {
                                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(request).error, TypeIcon.Icon.Cancel);
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
                                Thread t = new Thread(new ThreadStart(ThreadProc));
                                t.SetApartmentState(ApartmentState.STA);
                                t.Start();
                                this.Close();
                            }
                        }
                    }
                   
                }
                loading.Close();
            }
        }

        private void ThreadProc()
        {
            Application.Run(new Base());
        }
        #endregion
    }
}
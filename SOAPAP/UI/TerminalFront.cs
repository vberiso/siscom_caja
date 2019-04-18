using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Services;
using SOAPAP.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP
{
    public partial class TerminalFront : Form
    {
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        DataTable dt = new DataTable();
        querys q = new querys();
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;

        public TerminalFront()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }
       
        private  void Terminal_Load(object sender, EventArgs e)
        {
            
            pbCubo.Parent = pnlHeader;
            centraX(pnlContent, tlpTerminal);
            Variables.Configuration.Terminal = new Model.Terminal {MacAdress = q.GETMacAddress() };
            lblMacAdress.Text = Variables.Configuration.Terminal.MacAdress;
             cargaCombos();
            
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);

            if (!string.IsNullOrEmpty(Variables.Configuration.Terminal.MacAdress))
            {
                Model.Terminal terminal = new Model.Terminal();
                terminal.MacAdress = Variables.Configuration.Terminal.MacAdress;
                terminal.BranchOfficeId= Convert.ToInt16(cmbBranchOffice.SelectedValue);
                terminal.CashBox = nudAmount.Value;
                terminal.IsActive = true;

                string _terminal = JsonConvert.SerializeObject(terminal);
                HttpContent content = new StringContent(_terminal, Encoding.UTF8, "application/json");
                var _resulTerminal = await Requests.SendURIAsync("/api/Terminal", HttpMethod.Post, Variables.LoginModel.Token, content);
                if (_resulTerminal.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", _resulTerminal.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    btnGuardar.Enabled = false;
                    Variables.Configuration.Terminal = terminal;                    
                    mensaje = new MessageBoxForm("Transacción Exitosa", "Terminal dada de alta con éxito", TypeIcon.Icon.Success);
                    result = mensaje.ShowDialog();
                }
            }
            else {
                mensaje = new MessageBoxForm("Error", "Problemas para obtener Mac Adress", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
                loading.Close();                   
        }


        #region PrivateMethod
        private async void cargaCombos()
        {
            loading = new Loading();
            loading.Show(this);
            

            var resultBranchOffices = await Requests.SendURIAsync("/api/BranchOffice/", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultBranchOffices.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultBranchOffices.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var BranchOffices = JsonConvert.DeserializeObject<List<SOAPAP.Model.BranchOffice>>(resultBranchOffices);
                SOAPAP.Model.TypeTransaction typeTransaction = new SOAPAP.Model.TypeTransaction();     
                BranchOffices = BranchOffices.Where(x => x.IsActive == true).ToList();
                cmbBranchOffice.ValueMember = "id";
                cmbBranchOffice.DisplayMember = "name";
                cmbBranchOffice.DataSource = BranchOffices;
            }
            loading.Close();
        }

        private void centraX(Control padre, Control hijo)
        {
            int x = 0;

            x = (padre.Width / 2) - (hijo.Width / 2);
            hijo.Location = new System.Drawing.Point(x, hijo.Location.Y);
        }
        #endregion


    }
}

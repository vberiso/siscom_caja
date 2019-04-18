using Newtonsoft.Json;
using SOAPAP.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOAPAP.Enums;
using SOAPAP.Services;
using System.Net.Http;

namespace SOAPAP
{
    public partial class viewdetails : Form
    {
        List<debtPrepaids> debs = new List<debtPrepaids>();
        querys q = new querys();
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        string cuenta = string.Empty;
        string folio =  string.Empty;
        string pago =   string.Empty;
        string terminal = string.Empty;
        string tax = string.Empty;
        string round = string.Empty;
        string total = string.Empty;
        string subs = string.Empty;
        Form loading;
        public viewdetails(string f,string s,string v,string g,string tx,string rt,string r,string t)
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
            cuenta = s;
            folio = f;
            pago = v;
            terminal = g;
            tax = tx;
            round = rt;
            total = r;
            subs = t;
        }

        private async void viewdetails_Load(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            try
            {

                    var resultado = await Requests.SendURIAsync(string.Format("/api/Transaction/"+ cuenta), HttpMethod.Get, Variables.LoginModel.Token);
                    TransactionVM m = JsonConvert.DeserializeObject<TransactionVM>(resultado);
                    SubTotal.Text = subs;
                    NoCuenta.Text = m.transaction.account;
                    IVA.Text = tax;
                    Total.Text = total;
                    Folder.Text = folio;
                    Form_Pago.Text = pago;
                    Guid.Text = terminal;
                    redondo.Text = round;
                
            }
            catch(Exception ew){

            }
            loading.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
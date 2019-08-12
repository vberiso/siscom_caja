using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOAPAP.Enums;
using SOAPAP.Reportes;
using SOAPAP.Services;
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

namespace SOAPAP.UI.FacturacionAnticipada
{
    public partial class PeriodosAnteriores : Form
    {
        private Form loading;
        private Form mensaje;
        private string[] lOMeses = {
                "Enero",
                "Febrero" ,
                "Marzo" ,
                "Abril" ,
                "Mayo" ,
                "Junio" ,
                "Julio" ,
                "Agosto" ,
                "Septiembre" ,
                "Octubre" ,
                "Noviembre" ,
                "Diciembre"
        };
        private int agreement_id;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        public PeriodosAnteriores(int agreement_id)
        {
            this.agreement_id = agreement_id;
            InitializeComponent();

        }



        private void PeriodosAnteriores_Load(object sender, EventArgs e)
        {
            draw_year();
            Requests = new RequestsAPI(UrlBase);
            
            var tipos = new List<DataComboBox> { new DataComboBox() { keyString = "ED001", value = "En deuda" },
                                                 new DataComboBox() { keyString = "ED012", value = "Pago Acreditado" }
                                               };

            comboTipo.ValueMember = "keyString";
            comboTipo.DisplayMember = "value";
            
            comboTipo.DataSource = tipos;
            comboTipo.SelectedIndex = 0;

        }
        private void dawn_meses(int year, int currentMonth = 0)
        {
            List<DataComboBox> lstMeses = new List<DataComboBox>();
            int currentYear = DateTime.Now.Year;
            foreach ((string oMes, Int32 i) in lOMeses.Select((value, i) => (value, i)))
            {
                if (year < currentYear || (currentYear == year && currentMonth >= i + 1))
                {
                    lstMeses.Add(new DataComboBox() { keyString = (i + 1).ToString(), value = oMes });


                }


            }

            comboMes.ValueMember = "keyString";
            comboMes.DisplayMember = "value";
            comboMes.DataSource = lstMeses;
            comboMes.SelectedIndex = 0;

        }
        private void draw_year(int year = -1)
        {
           
            List<DataComboBox> lstYears = new List<DataComboBox>();

            DateTime now = DateTime.Now;
            if (year == -1)
            {
                year = now.Year;

            }
           

            for (int i = now.Year; i > now.Year - 10; i--)
            {
                lstYears.Add(new DataComboBox() { keyString = (i).ToString(), value = i.ToString() });

            }
            comboYears.ValueMember = "keyString";
            comboYears.DisplayMember = "value";
            comboYears.DataSource = lstYears;
            comboYears.SelectedIndex = 0;
            dawn_meses(year, now.Month);
        }

        private void comboYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            int mes = DateTime.Now.Month; 
            int year = Convert.ToInt32(((DataComboBox)comboYears.SelectedItem).keyString);
            dawn_meses(year, mes);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (textDescripcion.Text.Length < 25)
            {
                mensaje=  new MessageBoxForm("Validación", "La descripción debe tener como minimo 25 caracteres", TypeIcon.Icon.Cancel);
                mensaje.ShowDialog();
                mensaje.Close();
                return;


            }
            generar();
        }

        private async void generar()
        {
            int mes = Convert.ToInt32(((DataComboBox)comboMes.SelectedItem).keyString);
            int year = Convert.ToInt32(((DataComboBox)comboYears.SelectedItem).keyString);
            string tipo = Convert.ToString(((DataComboBox)comboTipo.SelectedItem).keyString);
            loading = new Loading();
            loading.Show(this);
            var url = string.Format("/api/StoreProcedure/runAccrualPeriodNow/{0}/{1}/{2}/{3}", 
                                Convert.ToInt32(agreement_id), 
                                mes, 
                                year, 
                                tipo
                                );
            
            var stringContent = new StringContent("{'descripcion':'"+ textDescripcion.Text + "','user_id':'"+Variables.LoginModel.User+"'}", Encoding.UTF8, "application/json");
            var results = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, stringContent);
            var jsonResult = JObject.Parse(results);

            bool is_null_error = jsonResult.ContainsKey("error");
            is_null_error = is_null_error == true? is_null_error : !string.IsNullOrEmpty(jsonResult["data"]["paramsOut"][0]["value"].ToString().Trim());
            if ( is_null_error)
            {
                string error = JsonConvert.DeserializeObject<Error>(results).error ;
                 error = !string.IsNullOrEmpty( error) ? error : jsonResult["data"]["paramsOut"][0]["value"].ToString();
                mensaje = new MessageBoxForm("Error", error , TypeIcon.Icon.Cancel);
                
            }
            else
            {
                mensaje = new MessageBoxForm("Éxito", jsonResult["message"].ToString(), TypeIcon.Icon.Success);
            }
            loading.Close();
            var result = mensaje.ShowDialog(this);
           
            //mensaje.Close();
            if (!is_null_error)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }



        }
    }
}

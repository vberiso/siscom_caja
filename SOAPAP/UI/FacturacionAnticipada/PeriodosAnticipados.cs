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
using System.Transactions;
using System.Windows.Forms;

namespace SOAPAP.UI.FacturacionAnticipada
{

    public partial class PeriodosAnticipados : Form
    {
        Form loading;
        Form mensaje;
        private RequestsAPI Requests = null;
        private int mesFin;
        private int mesInicio;
        private string UrlBase = Properties.Settings.Default.URL;
        DataTable Table = new DataTable();
        DialogResult result = new DialogResult();
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
        public PeriodosAnticipados(int agreement_id)
        {
            
            InitializeComponent();
            load_data();
            this.agreement_id = agreement_id;
            Requests = new RequestsAPI(UrlBase);

        }



        private void load_data()
        {
            List<DataComboBox> lstMeses = new List<DataComboBox>();
            List<DataComboBox> lstMesesF = new List<DataComboBox>();
            DateTime now = DateTime.Now;

            foreach ((string oMes, Int32 i) in lOMeses.Select((value, i) => (value, i)))
            {
                if (now.Month < i + 1)
                {
                    lstMeses.Add(new DataComboBox() { keyString = (i + 1).ToString(), value = oMes });
                    lstMesesF.Add(new DataComboBox() { keyString = (i + 1).ToString(), value = oMes });

                }

            }
            comboMesInicio.DataSource = lstMeses;
            //mes inicio
            comboMesInicio.ValueMember = "keyString";
            comboMesInicio.DisplayMember = "value";
            comboMesInicio.DataSource = lstMeses;
            comboMesInicio.SelectedIndex = 0;

            //mes fin
            comboMesFin2.ValueMember = "keyString";
            comboMesFin2.DisplayMember = "value";
            comboMesFin2.DataSource = lstMesesF;
            comboMesFin2.SelectedIndex = 0;
            lblYear.Text = now.Year.ToString();

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            if (!checkMeses())
            {
                mensaje = new MessageBoxForm("Validación", "el mes fin no debe ser menor al mes inicio", TypeIcon.Icon.Cancel);
              
                result = mensaje.ShowDialog(this);

            }
            else if (textDescripcion.Text.Length < 25)
            {
                mensaje = new MessageBoxForm("Validación", "La Observación tiene que ser mínimo de 25 caracteres", TypeIcon.Icon.Cancel);
               
                result = mensaje.ShowDialog(this);
            }
            else
            {
                generarFacturaAdelantada();
            }
            loading.Close();

        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            if (!checkMeses())
            {

                mensaje = new MessageBoxForm("Error", "el mes fin no debe ser menor al mes inicio", TypeIcon.Icon.Cancel);
                loading.Close();
                //result = mensaje.ShowDialog();

                result = mensaje.ShowDialog(this);

            }
            else
            {

                Simular uiSimular = new Simular(agreement_id, mesInicio, mesFin, Convert.ToInt32(lblYear.Text));
                loading.Close();
                //uiSimular.loadDataInTable();
                uiSimular.ShowDialog(this);
            }
        }


        private bool checkMeses()
        {
             mesFin = Convert.ToInt32(((DataComboBox)comboMesFin2.SelectedItem).keyString);
             mesInicio = Convert.ToInt32(((DataComboBox)comboMesInicio.SelectedItem).keyString);
            return !(mesFin < mesInicio);

        }


        private async void generarFacturaAdelantada()
        {

            
            int mesFin = Convert.ToInt32(((DataComboBox)comboMesFin2.SelectedItem).keyString);
            int mesInicio = Convert.ToInt32(((DataComboBox)comboMesInicio.SelectedItem).keyString);

            var url = string.Format("/api/StoreProcedure/runAccrualPeriod/{0}/{1}/{2}/{3}/{4}", Convert.ToInt32(agreement_id), mesInicio, mesFin, Convert.ToInt32(lblYear.Text), 0);
            var stringContent = new StringContent("{'descripcion':'" + textDescripcion.Text + "','user_id':'" + Variables.LoginModel.User + "'}", Encoding.UTF8, "application/json");
            var results = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, stringContent);
            var jsonResult = JObject.Parse(results);

            bool is_null_error = jsonResult.ContainsKey("error");
            is_null_error = is_null_error == true ? is_null_error : !string.IsNullOrEmpty(jsonResult["data"]["paramsOut"][0]["value"].ToString().Trim());

            if (is_null_error)
            {
                string error = JsonConvert.DeserializeObject<Error>(results).error;
                error = !string.IsNullOrEmpty(error) ? error : jsonResult["data"]["paramsOut"][0]["value"].ToString();
                
                mensaje = new MessageBoxForm("Error", error, TypeIcon.Icon.Cancel);

            }
            else
            {
                
                mensaje = new MessageBoxForm("Éxito", jsonResult["message"].ToString(), TypeIcon.Icon.Success);
            }
            
            result = mensaje.ShowDialog();
            mensaje.Close();
            if (!is_null_error)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;

        }
    }


}

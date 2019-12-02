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
        private bool IsAnual;
        int CurrentDescuento = 0;
        private Model.Agreement Agreement;
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
        public PeriodosAnticipados(Model.Agreement Agreement, bool IsAnual = false)
        {
            
            InitializeComponent();
            this.IsAnual = IsAnual;
            this.agreement_id = Agreement.Id;
            this.Agreement = Agreement;
            load_data();
          
            Requests = new RequestsAPI(UrlBase);

        }



        private void load_data()
        {
            DateTime now = DateTime.Now;
            if (!IsAnual) {
                List<DataComboBox> lstMeses = new List<DataComboBox>();
                List<DataComboBox> lstMesesF = new List<DataComboBox>();
               

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
           

                //mes fin
                comboMesFin2.ValueMember = "keyString";
                comboMesFin2.DisplayMember = "value";
                comboMesFin2.DataSource = lstMesesF;
                if (lstMeses.Count > 0)
                {
                    comboMesInicio.SelectedIndex = 0;

                    comboMesFin2.SelectedIndex = 0;
                }
               
                lblYear.Text = now.Year.ToString();
            }
            else
            {
                comboMesInicio.Visible = false;
                comboMesFin2.Visible = false;
                label1.Visible = false;
                lblTitle.Visible = false;
                label2.Text = "Pago anual";
                checkPaymentTarget.Visible = true;


                lblTextAnual.Visible = true;
                int year = now.Year;
                string descuento = "5%";
                Variables.Configuration.Descuento = 5;
                CurrentDescuento = 5;



                if (now.Month == 12)
                {
                    Variables.Configuration.Descuento = 10;
                    descuento = "10%";
                    year = year+1;
                    CurrentDescuento = 10;
                }
                var agreementDiscount = Agreement.AgreementDiscounts.Where(x => x.IsActive).FirstOrDefault();
                if (agreementDiscount != null)
                {
                    Variables.Configuration.Descuento = 50;
                    descuento = "50%";
                    CurrentDescuento = 50;
                }

                lblDescuento.Text = descuento;
                lblDescuento.Visible = true;
                lbldescuentoT.Visible = true;
                lblYear.Text = year.ToString();
            }

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
           

            if (!IsAnual && !checkMeses())
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
            if (!IsAnual && !checkMeses())
            {

                mensaje = new MessageBoxForm("Error", "el mes fin no debe ser menor al mes inicio", TypeIcon.Icon.Cancel);
                loading.Close();
                //result = mensaje.ShowDialog();

                result = mensaje.ShowDialog(this);

            }
            else
            {

                if (IsAnual)
                {
                    mesInicio = 1;
                    mesFin = 12;
                }
                Variables.Configuration.Anual = IsAnual;
               
                Simular uiSimular = new Simular(Agreement, mesInicio, mesFin, Convert.ToInt32(lblYear.Text));
                loading.Close();
                //uiSimular.loadDataInTable();
                uiSimular.ShowDialog(this);
            }
        }


        private bool checkMeses()
        {
            if (comboMesFin2.SelectedItem != null && comboMesInicio.SelectedItem != null) {
                mesFin = Convert.ToInt32(((DataComboBox)comboMesFin2.SelectedItem).keyString);
                mesInicio = Convert.ToInt32(((DataComboBox)comboMesInicio.SelectedItem).keyString);
                return !(mesFin < mesInicio);
            }
            return false;

        }


        private async void generarFacturaAdelantada()
        {
            loading = new Loading();
            loading.Show(this);

            int mesFin =12;
            int mesInicio = 1;
            int year = Convert.ToInt32(lblYear.Text);
            if (!IsAnual)
            {

                mesFin = Convert.ToInt32(((DataComboBox)comboMesFin2.SelectedItem).keyString);
                mesInicio = Convert.ToInt32(((DataComboBox)comboMesInicio.SelectedItem).keyString);
            }
            DateTime current = DateTime.Now;
            if (current.Month == 1)
            {
                mesInicio = 1;
            }
            if (current.Month == 2)
            {
                mesInicio = 1;
            }
            

            var url = string.Format("/api/StoreProcedure/runAccrualPeriod/{0}/{1}/{2}/{3}/{4}", Convert.ToInt32(agreement_id), mesInicio, mesFin,year, 0);
            var stringContent = new StringContent("{'descripcion':'" + textDescripcion.Text + "','user_id':'" + Variables.LoginModel.User + "'}", Encoding.UTF8, "application/json");
            var results = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, stringContent);
            var jsonResult = JObject.Parse(results);

            bool is_null_error = jsonResult.ContainsKey("error");
            if (!is_null_error  && jsonResult.ContainsKey("paramsOut")) {
                is_null_error = is_null_error == true ? is_null_error : !string.IsNullOrEmpty(jsonResult["data"]["paramsOut"][0]["value"].ToString().Trim());
            }

            loading.Close();
            if (is_null_error)
            {
                string error = JsonConvert.DeserializeObject<Error>(results).error;
                error = !string.IsNullOrEmpty(error) ? error : jsonResult["data"]["paramsOut"][0]["value"].ToString();

                mensaje = new MessageBoxForm("Error", error, TypeIcon.Icon.Cancel);

            }
            else
            {
                if (Variables.Configuration.Anual)
                {
                    var des = Variables.Configuration.Descuento == 50 ? 0 : Variables.Configuration.Descuento;
                     url = string.Format("/api/Agreements/GeneratePagosAnuales/{0}/{1}/{2}/{3}", Convert.ToInt32(agreement_id), des, Variables.LoginModel.FullName, Variables.LoginModel.User);
                    stringContent = new StringContent(JsonConvert.SerializeObject(jsonResult["data"]), Encoding.UTF8, "application/json");
                    results = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, stringContent);
                }
                mensaje = new MessageBoxForm("Éxito", jsonResult["message"].ToString(), TypeIcon.Icon.Success);
            }
            
            result = mensaje.ShowDialog();
            mensaje.Close();
            if (!is_null_error)
            {
                Variables.Agreement = Agreement;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;

        }

        private void checkPaymentTarget_Click(object sender, EventArgs e)
        {
            if (checkPaymentTarget.Checked)
            {
                Variables.Configuration.Descuento = 0;
            }
            else
            {
                Variables.Configuration.Descuento = CurrentDescuento;
            }
            lblMSI.Visible = checkPaymentTarget.Checked;
        }
    }


}

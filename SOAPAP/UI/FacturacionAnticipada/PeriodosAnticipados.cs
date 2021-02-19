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
        private int mesInicioXMesesPagados;
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
        public PeriodosAnticipados(Model.Agreement Agreement, bool IsAnual = false, int mesInicioXMesesYaPagados = 0)
        {
            
            InitializeComponent();
            this.IsAnual = IsAnual;
            this.agreement_id = Agreement.Id;
            this.Agreement = Agreement;
            this.mesInicioXMesesPagados = mesInicioXMesesYaPagados;
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
                label2.Text = Variables.Configuration.anualDiscount.NombrePublico;
                checkPaymentTarget.Visible = true;


                lblTextAnual.Visible = true;
                lblTextAnual.Text = Variables.Configuration.anualDiscount.DescripcionPublico;

                int year = now.Year;
                string descuento = "5%";
                //Variables.Configuration.Descuento = 5;
                //CurrentDescuento = 5;



                //if (now.Month == 12)
                //{
                //    Variables.Configuration.Descuento = 10;
                //    descuento = "10%";
                //    year = year+1;
                //    CurrentDescuento = 10;
                //}
                Variables.Configuration.Descuento = ObtenerDescuentoAAplicar();
                descuento = $"{ObtenerDescuentoAAplicar()}%";
                year = Variables.Configuration.anualDiscount.PromocionAño;
                CurrentDescuento = ObtenerDescuentoAAplicar();

                var agreementDiscount = Agreement.AgreementDiscounts.Where(x => x.IsActive).FirstOrDefault();
                if (agreementDiscount != null)
                {
                    Variables.Configuration.Descuento = 50;
                    descuento = "50%";
                    CurrentDescuento = 50;
                }
                if (Agreement.TypeIntakeId == 2 || Agreement.TypeIntakeId == 3 )
                {
                    Variables.Configuration.Descuento = 0;
                    descuento = "0%";
                    CurrentDescuento = 0;
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

        private async void btnGenerar_Click(object sender, EventArgs e)
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
                await generarFacturaAdelantada();
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
                mesInicio = 1;
                mesFin = 12;
                //if (IsAnual)
                //{
                //    mesInicio = 1;
                //    mesFin = 12;
                //}

                DateTime current = DateTime.Now;
                if (current.Month == 1 && Agreement.Debts.Count() > 1 && Agreement.Debts.Where(x => x.FromDate.Month == 1).ToList().Count() == 1)
                {
                    
                    mesInicio = 2;
                }
                if (current.Month == 2 && Agreement.Debts.Count() > 2 && Agreement.Debts.Where(x => x.FromDate.Month == 2).ToList().Count() == 1)
                {
                    mesInicio = 3;
                }

                Variables.Configuration.Anual = IsAnual;

                //Si ya hay meses pagados en BD
                mesInicio = (mesInicioXMesesPagados > 0 ? mesInicioXMesesPagados + 1 : mesInicio);

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


        private async Task<string> generarFacturaAdelantada()
        {
            //loading = new Loading();
            //loading.Show(this);

            int mesFin =12;
            int mesInicio = 1;
            int year = Convert.ToInt32(lblYear.Text);
            if (!IsAnual)
            {

                mesFin = Convert.ToInt32(((DataComboBox)comboMesFin2.SelectedItem).keyString);
                mesInicio = Convert.ToInt32(((DataComboBox)comboMesInicio.SelectedItem).keyString);
            }
            else
            {                
                if (Variables.Configuration.anualDiscount.BorrarDeudaAñoPromocion)
                {
                    var deudaAñoPromocion = Agreement.Debts.Where(d => d.Year == Variables.Configuration.anualDiscount.PromocionAño).ToList();

                    if(deudaAñoPromocion.Count > 0)
                    {
                        //Se borra la deuda del mismo año que se generara. Esto para generar los 12 meses
                        var urlBorrar = string.Format("/api/CondonationCampaing/BorrarDeudaDeAño/{0}/{1}/{2}", Convert.ToInt32(agreement_id), Variables.Configuration.anualDiscount.PromocionAño, Variables.Configuration.anualDiscount.Nombre);
                        var resultsBorrar = await Requests.SendURIAsync(urlBorrar, HttpMethod.Post, Variables.LoginModel.Token, null);

                        if (String.IsNullOrEmpty(resultsBorrar))
                        {
                            foreach (var item in deudaAñoPromocion)
                            {
                                Agreement.Debts.Remove(item);
                            }                            
                        }
                    }                    
                }

                DateTime current = DateTime.Now;
                if (current.Month == 1 && Agreement.Debts.Count() >= 1 && Agreement.Debts.Where(x => x.FromDate.Month == 1 && x.Year == Variables.Configuration.anualDiscount.PromocionAño).ToList().Count() == 1)
                {
                    mesInicio = 2;
                }
                if (current.Month == 2 && Agreement.Debts.Count() >= 2 && Agreement.Debts.Where(x => x.FromDate.Month == 2 && x.Year == Variables.Configuration.anualDiscount.PromocionAño).ToList().Count() == 1)
                {
                    mesInicio = 3;
                }
            }

            //Si ya hay meses pagados en BD
            mesInicio = (mesInicioXMesesPagados > 0 ? mesInicioXMesesPagados + 1 : mesInicio);

            var url = string.Format("/api/StoreProcedure/runAccrualPeriod/{0}/{1}/{2}/{3}/{4}", Convert.ToInt32(agreement_id), mesInicio, mesFin,year, 0);
            var stringContent = new StringContent("{'descripcion':'" + textDescripcion.Text + "','user_id':'" + Variables.LoginModel.User + "'}", Encoding.UTF8, "application/json");
            var results = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, stringContent);
            var jsonResult = JObject.Parse(results);

            bool is_null_error = results.Contains("error");
            if (!is_null_error  && jsonResult.ContainsKey("paramsOut")) {
                is_null_error = is_null_error == true ? is_null_error : !string.IsNullOrEmpty(jsonResult["data"]["paramsOut"][0]["value"].ToString().Trim());
            }

       
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
                    List<int> debts = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(jsonResult["data"]));
                    int HaveMes = 0;
                    var debtsn = Variables.Agreement.Debts.Where(x => x.Type == "TIP01").ToList();
                    if (debtsn.Count >0)
                    {
                        HaveMes = 1;
                       
                        debts.AddRange(debtsn.Select(x => x.Id));
                    }
                    var des = Variables.Configuration.Descuento == 50 ? 0 : Variables.Configuration.Descuento;
                    if (Agreement.TypeIntakeId == 2 || Agreement.TypeIntakeId == 3)
                    {
                        des = -1;
                     
                    }
                    url = string.Format("/api/Agreements/GeneratePagosAnuales/{0}/{1}/{2}/{3}/{4}/{5}?msgdesc={6}", Convert.ToInt32(agreement_id), des, Variables.LoginModel.FullName, Variables.LoginModel.User, checkPaymentTarget.Checked, HaveMes, Variables.Configuration.anualDiscount.ObservacionFactura);
                    stringContent = new StringContent(JsonConvert.SerializeObject(debts), Encoding.UTF8, "application/json");
                    results = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, stringContent);
                }
                mensaje = new MessageBoxForm("Éxito", jsonResult["message"].ToString(), TypeIcon.Icon.Success);
            }
            
            result = mensaje.ShowDialog();
            mensaje.Close();
           // loading.Close();
            if (!is_null_error)
            {
                Variables.Agreement = Agreement;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            return "";

        }

        public async Task<string> generarDeudaAdelantada()
        {
            loading = new Loading();
            loading.Show(this);

            int mesFin = Variables.Configuration.anualDiscount.PromocionMesFinal;
            int mesInicio = Variables.Configuration.anualDiscount.PromocionMesIncio;
            int year = Variables.Configuration.anualDiscount.PromocionAño;

            if (Variables.Configuration.anualDiscount.BorrarDeudaAñoPromocion)
            {
                var deudaAñoPromocion = Agreement.Debts.Where(d => d.Year == Variables.Configuration.anualDiscount.PromocionAño).ToList();

                if (deudaAñoPromocion.Count > 0)
                {
                    //Se borra la deuda del mismo año que se generara. Esto para generar los 12 meses
                    var urlBorrar = string.Format("/api/CondonationCampaing/BorrarDeudaDeAño/{0}/{1}/{2}", Convert.ToInt32(agreement_id), Variables.Configuration.anualDiscount.PromocionAño, Variables.Configuration.anualDiscount.Nombre);
                    var resultsBorrar = await Requests.SendURIAsync(urlBorrar, HttpMethod.Post, Variables.LoginModel.Token, null);

                    if (String.IsNullOrEmpty(resultsBorrar))
                    {
                        foreach (var item in deudaAñoPromocion)
                        {
                            Agreement.Debts.Remove(item);
                        }
                    }
                }
            }
            

            //Si ya hay meses pagados en BD
            mesInicio = (mesInicioXMesesPagados > 0 ? mesInicioXMesesPagados + 1 : mesInicio);

            var url = string.Format("/api/StoreProcedure/runAccrualPeriod/{0}/{1}/{2}/{3}/{4}", Convert.ToInt32(agreement_id), mesInicio, mesFin, year, 0);
            var stringContent = new StringContent("{'descripcion':'" + textDescripcion.Text + "','user_id':'" + Variables.LoginModel.User + "'}", Encoding.UTF8, "application/json");
            var results = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, stringContent);
            var jsonResult = JObject.Parse(results);

            bool is_null_error = results.Contains("error");
            if (!is_null_error && jsonResult.ContainsKey("paramsOut"))
            {
                is_null_error = is_null_error == true ? is_null_error : !string.IsNullOrEmpty(jsonResult["data"]["paramsOut"][0]["value"].ToString().Trim());
            }


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
            loading.Close();

            return "";
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

        private void PeriodosAnticipados_Load(object sender, EventArgs e)
        {
            if (Variables.Configuration.Anual)
            {
                mensaje = new MessageBoxForm("Información", @"Si va a aplicar un descuento de población vulnerable, favor de primero aplicar el descuento al contrato desde la WEB, Actualizar la información de está cuenta, y ahora si aplicar el adelanto anual", TypeIcon.Icon.Info);

                result = mensaje.ShowDialog(this);
            }
        }

        public int ObtenerDescuentoAAplicar()
        {
            var Desc = Variables.Configuration.anualDiscount.PromocionAplicar.FirstOrDefault(x => x.año == DateTime.Now.Year && x.meses.Contains(DateTime.Now.Month));
            if (Desc != null)
                return Desc.Descuento;
            return 0;
        }
    }
}

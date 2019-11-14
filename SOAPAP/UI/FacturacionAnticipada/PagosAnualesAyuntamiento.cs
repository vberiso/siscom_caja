﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOAPAP.Enums;
using SOAPAP.Model;
using SOAPAP.Reportes;
using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace SOAPAP.UI.PagosAnualesAyuntamiento
{

    public partial class PagosAnualesAyuntamiento : Form
    {
        Form loading;
        Form mensaje;
        private RequestsAPI Requests = null;
        private int mesFin;
        private bool IsAnual;
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
        public PagosAnualesAyuntamiento(Model.Agreement Agreement, bool IsAnual = false)
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
        
              
             
                lblYear.Text = (DateTime.Now.Year+1).ToString();
                RequestData();




        }


        private async void RequestData()
        {
            int year = Convert.ToInt32(lblYear.Text);
            var url = string.Format("/api/Agreements/getSimulateDebt/{0}/{1}", Convert.ToInt32(agreement_id), year);
          
            var results = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token);
            List<DebtAnnual> DebtAnnual = JsonConvert.DeserializeObject<List<DebtAnnual>>(results);
            decimal TPredial = 0;
            decimal TLimpia = 0;
            var predial = DebtAnnual.Where(x => x.Type == "TIP01").ToList();
            var limpia = DebtAnnual.Where(x => x.Type == "TIP01").ToList();
            predial.ForEach(x => {
                if (x.HaveTax)
                {
                    TPredial = TPredial + (x.Amount * 16 / 100);
                }
                else
                {
                    TPredial = TPredial + x.Amount;
                }
            });

            limpia.ForEach(x => {
                if (x.HaveTax)
                {
                    TLimpia = TLimpia + (x.Amount * 16 / 100);
                }
                else
                {
                    TLimpia = TLimpia + x.Amount;
                }
            });
            lblPredial.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TPredial); 
            lblLimpi.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TLimpia); 
            lblTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TLimpia + TPredial); 
            

        }

     
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);

            generarFacturaAdelantada();
            
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


   


        private async void generarFacturaAdelantada()
        {

            int year = Convert.ToInt32(lblYear.Text);

            List<object> content = new List<object>();
          
            content.Add(new { Key = "anio_facturar", Value = year, DbType = DbType.Int32 });

            content.Add(new { Key = "id_agreement", Value =agreement_id, DbType = DbType.Int32 });
            

            var url = string.Format("/api/StoreProcedure/runSpAsignarDeb/{0}", "billing_period_year");
            var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            var results = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, stringContent);
            var jsonResult = JObject.Parse(results);

            bool is_null_error = jsonResult.ContainsKey("error");
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
                //url = string.Format("/api/Agreements/GeneratePagosAnuales/{0}/{1}", Convert.ToInt32(agreement_id), Variables.Configuration.Descuento);
                //stringContent = new StringContent(JsonConvert.SerializeObject(jsonResult["data"]), Encoding.UTF8, "application/json");
                //results = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, stringContent);
                mensaje = new MessageBoxForm("Éxito", "Deuda generada correctamente", TypeIcon.Icon.Success);
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

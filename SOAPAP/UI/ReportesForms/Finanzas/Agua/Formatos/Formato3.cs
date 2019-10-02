using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOAPAP.Reportes.Finanzas;
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
using System.Windows.Forms;

namespace SOAPAP.UI.ReportesForms.Finanzas.Agua.Formatos
{
    public partial class Formato3 : Form
    {

        private int TUsuarios = 0, TTomas = 0, GTTomas, GTUsuarios, GTUE, GTTE;
        private int NumCallGetParams;
        private decimal TUE, TTE;
        List<Formato23Desgloce> Result;
        private decimal TImporte = 0, totalConMedidorU, totalConMedidorT;
        private decimal totalSinMedidorU, totalSinMedidorT;

        private decimal totalConMedidorUE, totalConMedidorTE;
        private decimal totalSinMedidorUE, totalSinMedidorTE;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        public Formato3()
        {
            Requests = new RequestsAPI(UrlBase);
            NumCallGetParams = 3;
            InitializeComponent();
        }
        public void SetGroups(object Group)
        {
        }
        public List<object> GetParams()
        {
            List<object> LParams = new List<object>();
            if (NumCallGetParams == 1)
            {
                NumCallGetParams = 2;
            }
            else
            {
                NumCallGetParams--;
            }
            LParams.Add(new { Key = "TypeServiceId", Value = NumCallGetParams, DbType = DbType.Int32 });



            return LParams;
        }

        /// <summary>
        /// Aignar valores a los controles de uso sin medidor
        /// </summary>
        public void DrawDataA(object data, string mes, string year)
        {


            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);

            List<object> ODsta = JsonConvert.DeserializeObject<List<object>>(data.ToString());
            List<string> accumulated = JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(ODsta.ElementAt(2)));
            List<string> accumulatede = JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(ODsta.Last()));
            data = JsonConvert.SerializeObject(ODsta.First());
            List<SOAPAP.Reportes.Finanzas.Formato2> ODataMes = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato2>>(data.ToString());
            data = JsonConvert.SerializeObject(ODsta.ElementAt(1));
            List<SOAPAP.Reportes.Finanzas.Formato2> ODataEnero = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato2>>(data.ToString());
            ////datos para uso domestico
            //En el mes
            TUsuarios = ODataMes.Where(x => x.uso == "HA").ToList().Count+ int.Parse( accumulated.First());
            TTomas = ODataMes.Where(x => x.uso == "HA" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulated.First());
            textUDSM.Text = TUsuarios.ToString();
            textTDSM.Text = TTomas.ToString();
            textTUSM.Text = TUsuarios.ToString();
            textTTSM.Text = TTomas.ToString();
            GTTomas = TTomas;
            GTUsuarios = TUsuarios;

            //De 1 de enero al  mes
            TUsuarios = ODataEnero.Where(x => x.uso == "HA").ToList().Count + int.Parse(accumulatede.First());
            TTomas = ODataEnero.Where(x => x.uso == "HA" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulatede.First());
            textUDSME.Text = TUsuarios.ToString();
            textTDSME.Text = TTomas.ToString();
            textTUSME.Text = TUsuarios.ToString();
            textTTSME.Text = TTomas.ToString();
            GTTE = TTomas;
            GTUE = TUsuarios;





            //datos para uso no domestico
            //En el mes
            TUsuarios = ODataMes.Where(x => x.uso == "CO").ToList().Count + int.Parse(accumulated.ElementAt(1));
            TTomas = ODataMes.Where(x => x.uso == "CO" && x.type_agreement == "AGR01").ToList().Count+int.Parse(accumulated.ElementAt(1));
            textUNDSM.Text = TUsuarios.ToString();
            textTNDSM.Text = TTomas.ToString();
            textTUSM.Text = (int.Parse(textTUSM.Text) + TUsuarios).ToString();
            textTTSM.Text = (int.Parse(textTTSM.Text) + TTomas).ToString();

            GTTomas += TTomas;
            GTUsuarios += TUsuarios;
            //De 1 de enero al  mes
            TUsuarios = ODataEnero.Where(x => x.uso == "CO").ToList().Count + int.Parse(accumulatede.ElementAt(1)); 
            TTomas = ODataEnero.Where(x => x.uso == "CO" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulatede.ElementAt(1)); 
            textUNDSME.Text = TUsuarios.ToString();
            textTNDSME.Text = TTomas.ToString();
            textTUSME.Text = (int.Parse(textTUSME.Text) + TUsuarios).ToString();
            textTTSME.Text = (int.Parse(textTTSME.Text) + TTomas).ToString();

            GTTE += TTomas;
            GTUE += TUsuarios;




            //datos para otros usos
            //En el mes
            TUsuarios = ODataMes.Where(x => x.uso != "HA" && x.uso != "CO").ToList().Count + int.Parse(accumulated.Last());
            TTomas = ODataMes.Where(x => x.uso != "HA" && x.uso != "CO" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulated.Last());
            textUOSM.Text = TUsuarios.ToString();
            textTOSM.Text = TTomas.ToString();
            textTUSM.Text = (int.Parse(textTUSM.Text) + TUsuarios).ToString();
            textTTSM.Text = (int.Parse(textTTSM.Text) + TTomas).ToString();
            GTTomas += TTomas;
            GTUsuarios += TUsuarios;
            //De 1 de enero al  mes
            TUsuarios = ODataEnero.Where(x => x.uso != "HA" && x.uso != "CO").ToList().Count + int.Parse(accumulatede.Last());
            TTomas = ODataEnero.Where(x => x.uso != "HA" && x.uso != "CO" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulatede.Last()); 
            textUOSME.Text = TUsuarios.ToString();
            textTOSME.Text = TTomas.ToString();
            textTUSME.Text = (int.Parse(textTUSME.Text) + TUsuarios).ToString();
            textTTSME.Text = (int.Parse(textTTSME.Text) + TTomas).ToString();
            GTTE += TTomas;
            GTUE += TUsuarios;

           

            textGTTomas.Text = GTTomas.ToString();
            textGTUsuarios.Text = GTUsuarios.ToString();

            textGTTE.Text = GTTE.ToString();
            textGTUE.Text = GTUE.ToString();
            lblC.Text = $@"No DE USUARIOS Y No DE TOMAS QUE REALIZARON ÚNICAMENTE PAGOS DE {year}
                        (C)";

            lblD.Text = $@"No DE USUARIOS Y No DE TOMAS QUE REALIZARON ÚNICAMENTE PAGOS DE EJERCICIOS ANTERIORES A {year}
                        (D)";

            lblE.Text = $@"No DE USUARIOS Y No DE TOMAS QUE QUE REALIZARON TANTO PAGOS DE {year} COMO DE EJERCICIOS ANTERIORES A {year}
                        (E)";
            // lblRecau.Text = $"RECAUDACIÓN DEL EJERCICIO {year} Y DE EJERCICIOS ANTERIORES A {year}";
           


        }



        /// <summary>
        /// Aignar valores a los controles de uso con medidor
        /// </summary>
        public void DrawData(object data, string mes, string year)
        {

            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);
            List<object> ODsta = JsonConvert.DeserializeObject<List<object>>(data.ToString());
            List<string> accumulated = JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(ODsta.ElementAt(2)));
            List<string> accumulatede = JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(ODsta.Last()));
            data = JsonConvert.SerializeObject(ODsta.First());
            List<SOAPAP.Reportes.Finanzas.Formato2> ODataMes = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato2>>(data.ToString());
            data = JsonConvert.SerializeObject(ODsta.ElementAt(1));
            List<SOAPAP.Reportes.Finanzas.Formato2> ODataEnero = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato2>>(data.ToString());
            ////datos para uso domestico
            //En el mes
            TUsuarios = ODataMes.Where(x => x.uso == "HA").ToList().Count + int.Parse(accumulated.First());
            TTomas = ODataMes.Where(x => x.uso == "HA" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulated.First());
            textUDCM.Text = TUsuarios.ToString();
            textTDCM.Text = TTomas.ToString();
            textTUCM.Text = TUsuarios.ToString();
            textTTCM.Text = TTomas.ToString();
            GTTomas += TTomas;
            GTUsuarios += TUsuarios;

            //De 1 de enero al  mes
            TUsuarios = ODataEnero.Where(x => x.uso == "HA").ToList().Count + int.Parse(accumulatede.First());
            TTomas = ODataEnero.Where(x => x.uso == "HA" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulatede.First());
            textUDCME.Text = TUsuarios.ToString();
            textTDCME.Text = TTomas.ToString();
            textTUCME.Text = TUsuarios.ToString();
            textTTCME.Text = TTomas.ToString();
            GTTE += TTomas;
            GTUE += TUsuarios;



            //datos para uso no domestico
            //En el mes
            TUsuarios = ODataMes.Where(x => x.uso == "CO").ToList().Count+ int.Parse(accumulated.ElementAt(1));
            TTomas = ODataMes.Where(x => x.uso == "CO" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulated.ElementAt(1));
            textUNDCM.Text = TUsuarios.ToString();
            textTNDCM.Text = TTomas.ToString();
            textTUCM.Text = (int.Parse(textTUCM.Text) + TUsuarios).ToString();
            textTTCM.Text = (int.Parse(textTTCM.Text) + TTomas).ToString();
            GTTomas += TTomas;
            GTUsuarios += TUsuarios;
            //De 1 de enero al  mes
            TUsuarios = ODataEnero.Where(x => x.uso == "CO").ToList().Count + int.Parse(accumulatede.ElementAt(1));
            TTomas = ODataEnero.Where(x => x.uso == "CO" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulatede.ElementAt(1));
            textUNDCME.Text = TUsuarios.ToString();
            textTNDCME.Text = TTomas.ToString();
            textTUCME.Text = (int.Parse(textTUCME.Text) + TUsuarios).ToString();
            textTTCME.Text = (int.Parse(textTTCME.Text) + TTomas).ToString();
            GTTE += TTomas;
            GTUE += TUsuarios;




            //datos para otros usos
            //En el mes
            TUsuarios = ODataMes.Where(x => x.uso != "HA" && x.uso != "CO").ToList().Count + int.Parse(accumulated.Last());
            TTomas = ODataMes.Where(x => x.uso != "HA" && x.uso != "CO" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulated.Last());
            textUOCM.Text = TUsuarios.ToString();
            textTOCM.Text = TTomas.ToString();
            textTUCM.Text = (int.Parse(textTUCM.Text) + TUsuarios).ToString();
            textTTCM.Text = (int.Parse(textTTCM.Text) + TTomas).ToString();
            GTTomas += TTomas;
            GTUsuarios += TUsuarios;
            //De 1 de enero al  mes
            TUsuarios = ODataEnero.Where(x => x.uso != "HA" && x.uso != "CO").ToList().Count + int.Parse(accumulatede.Last());
            TTomas = ODataEnero.Where(x => x.uso != "HA" && x.uso != "CO" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulatede.Last());
            textUOCME.Text = TUsuarios.ToString();
            textTOCME.Text = TTomas.ToString();
            textTUCME.Text = (int.Parse(textTUCME.Text) + TUsuarios).ToString();
            textTTCME.Text = (int.Parse(textTTCME.Text) + TTomas).ToString();

            GTTE += TTomas;
            GTUE += TUsuarios;

            textC1.Text = "Calculando ..";
            textC2.Text = "Calculando ..";
            textD1.Text = "Calculando ..";
            textD2.Text = "Calculando ..";
            textE1.Text = "Calculando ..";
            textE2.Text = "Calculando ..";
            textF1.Text = "Calculando ..";
            textF2.Text = "Calculando ..";

            GetUsuarios(mes, year);




        }

        private async Task<List<object>> GetUsuarios(string mes, string year, int returnData= 0)
        {
            List<object> content = new List<object>();
            content.Add(new { Key = "mesPresentarInicio", Value = mes, DbType = DbType.Int32 });
            content.Add(new { Key = "mesPresentarFin", Value = mes, DbType = DbType.Int32 });
            content.Add(new { Key = "yearPeriodo", Value = year, DbType = DbType.Int32 });

            var json = JsonConvert.SerializeObject(content);


            var StringContent = new StringContent(json, Encoding.UTF8, "application/json");
            string url = string.Format("/api/StoreProcedure/RunFormato23Desgloce");

            
           
            var results = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token, StringContent);
            

            bool containsError = string.IsNullOrEmpty(results);
            containsError = (containsError == true ? true : (results.Contains("error") ? true : false));
            if (containsError)
            {
                results = string.IsNullOrEmpty(results) ? "{}" : results;
                string error = JsonConvert.DeserializeObject<Error>(results).error;
                throw new Exception(string.IsNullOrEmpty(error) ? "Sin datos" : error);

            }
            else
            {
                try
                {
                    var JdDta =  JObject.Parse(results);
                    
                    var data = JsonConvert.SerializeObject(JdDta["data"]);
                    List<object> LData = JsonConvert.DeserializeObject<List<object>>(data);
                    List<Formato23Desgloce> Lusuarios = JsonConvert.DeserializeObject<List<Formato23Desgloce>>(JsonConvert.SerializeObject(LData.First()));
                   
                    if (returnData == 0)
                    {
                        var total = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(LData.Last()));
                        if (Lusuarios.Count() > 0)
                        {
                            textC1.Text = (Lusuarios.Where(x => x.tipo == "actuales").First().usuarios + total.First()).ToString();
                            textC2.Text = (Lusuarios.Where(x => x.tipo == "actuales").First().usuarios + total.First()).ToString();
                            textD1.Text = (Lusuarios.Where(x => x.tipo == "anteriores").First().usuarios + total.ElementAt(1)).ToString();
                            textD2.Text = (Lusuarios.Where(x => x.tipo == "anteriores").First().usuarios + total.ElementAt(1)).ToString();
                            textE1.Text = (Lusuarios.Where(x => x.tipo == "mixto").First().usuarios + total.Last()).ToString();
                            textE2.Text = (Lusuarios.Where(x => x.tipo == "mixto").First().usuarios + total.Last()).ToString();
                            textF1.Text = (int.Parse(textC1.Text) + int.Parse(textD1.Text) + int.Parse(textE1.Text)).ToString();
                            textF2.Text = (int.Parse(textC2.Text) + int.Parse(textD2.Text) + int.Parse(textE2.Text)).ToString();
                        }
                        else
                        {
                            textC1.Text = total.First().ToString();
                            textC2.Text = total.First().ToString();
                            textD1.Text = total.ElementAt(1).ToString();
                            textD2.Text = total.ElementAt(1).ToString();
                            textE1.Text = total.Last().ToString();
                            textE2.Text = total.Last().ToString();
                            textF1.Text = (int.Parse(textC1.Text) + int.Parse(textD1.Text) + int.Parse(textE1.Text)).ToString();
                            textF2.Text = (int.Parse(textC2.Text) + int.Parse(textD2.Text) + int.Parse(textE2.Text)).ToString();
                        }
                        return null;
                    }
                    else
                    {
                        
                        return LData;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }

        public string GetHtml1(object data, string year, string mes)
        {
            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);
            List<object> ODsta = JsonConvert.DeserializeObject<List<object>>(data.ToString());

            List<string> accumulated = JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(ODsta.ElementAt(2)));
            List<string> accumulatede = JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(ODsta.Last()));
            data = JsonConvert.SerializeObject(ODsta.First());
            List<SOAPAP.Reportes.Finanzas.Formato2> ODataMes = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato2>>(data.ToString());
            data = JsonConvert.SerializeObject(ODsta.ElementAt(1));
            List<SOAPAP.Reportes.Finanzas.Formato2> ODataEnero = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato2>>(data.ToString());


           
            //datos para uso domestico
            TUsuarios = ODataMes.Where(x => x.uso == "HA").ToList().Count + int.Parse(accumulated.First());
            TTomas = ODataMes.Where(x => x.uso == "HA" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulated.First());
            TUE = ODataEnero.Where(x => x.uso == "HA").ToList().Count + int.Parse(accumulatede.First());
            TTE = ODataEnero.Where(x => x.uso == "HA" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulatede.First());



            totalConMedidorU = TUsuarios;
            totalConMedidorT = TTomas;

            totalConMedidorUE = TUE;
            totalConMedidorTE = TTE;

            StringBuilder builder = new StringBuilder();
            builder.Append($@"<table id='datos' style='width: 99%;'>
                            <tbody>
                                <tr>
                                         <td colspan='2' rowspan='2' style='width: 47%' class='centro'>CONCEPTO</td>
                    
                                        <td colspan='2' class='centro'>No DE USUARIOS Y No DE TOMAS EXISTENTES EN EL PADRON AL ULTIMO DÍA DEL MES</td>
                                        <td colspan='2' class='centro'>No DE USUARIOS Y No DE TOMAS QUE REALIZARON PAGOS DEL 1 DE ENERO AL ÚLTIMO DÍA DEL MES</td> </tr>
                                <tr>                         
                                                <td class='centro' style='width: 13%'>No DE USUARIOS<br>(A)</td>
                                                <td class='centro'  style='width: 13%'>No DE TOMAS <br>(B)</td>
                                                <td class='centro' style='width: 13%'>No DE USUARIOS<br>(A)</td>
                                                <td class='centro'  style='width: 20%'>No DE TOMAS <br>(B)</td>
                                    </tr>
                            </tbody>
                            </table>");

            builder.Append("<h3 style='text-align: center'>DERECHOS POR SUMINISTRO DE AGUA <br> INGRESOS</h3>");
            builder.Append($@"<table id='datos' style='width: 99%' class='actuales'>
                            <caption style='background:#f1eeec'>USO DE LA BASE CON MEDIDOR</caption>
                            <tbody>
                                <tr>
                                    <td colspan='2' style='width: 47%' class='left'>USO DOMÉSTICO</td>
                                    <td class='centro' style='width: 13%'>{TUsuarios}</td>
                                    <td class='centro'  style='width: 13%'>{TTomas}</td>
                                    <td class='centro' style='width: 13%'>{TUE}</td>
                                    <td class='centro'  style='width: 20%'>{TTE}</td>

                                  
                                </tr>");

            //Uso COmercial no domestico
            TUsuarios = ODataMes.Where(x => x.uso == "CO").ToList().Count + int.Parse(accumulated.ElementAt(1));
            TTomas = ODataMes.Where(x => x.uso == "CO" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulated.ElementAt(1));
            TUE = ODataEnero.Where(x => x.uso == "CO").ToList().Count + int.Parse(accumulatede.ElementAt(1));
            TTE = ODataEnero.Where(x => x.uso == "CO" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulatede.ElementAt(1));

            totalConMedidorU += TUsuarios;
            totalConMedidorT += TTomas;
            totalConMedidorUE += TUE;
            totalConMedidorTE += TTE;
            builder.Append($@"<tr>
                                    <td colspan='2' style='width: 47%' class='left'>USO NO DOMÉSTICO (COMERCIAL)</td>
                                    <td class='centro' style='width: 13%'>{TUsuarios}</td>
                                    <td class='centro'  style='width: 13%'>{TTomas}</td>
                                    <td class='centro' style='width: 13%'>{TUE}</td>
                                    <td class='centro'  style='width: 20%'>{TTE}</td>

                                </tr>");

            //otros usos
       
            TUsuarios = ODataMes.Where(x => x.uso != "CO" && x.uso != "HA").ToList().Count + int.Parse(accumulated.Last());
            TTomas = ODataMes.Where(x => x.uso != "CO" && x.uso != "HA" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulated.Last());
            TUE = ODataEnero.Where(x => x.uso != "CO" && x.uso != "HA").ToList().Count + int.Parse(accumulatede.Last());
            TTE = ODataEnero.Where(x => x.uso != "CO" && x.uso != "HA" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulatede.Last());
            totalConMedidorU += TUsuarios;
            totalConMedidorT += TTomas;
            totalConMedidorUE += TUE;
            totalConMedidorTE += TTE;

            builder.Append($@"<tr>
                                    <td colspan='2' style='width: 47%' class='left'>OTROS USOS (INDUSTRIAL, PRESTADOR DE SERVICIOS)</td>
                                    <td class='centro' style='width: 13%'>{TUsuarios}</td>
                                    <td class='centro'  style='width: 13%'>{TTomas}</td>
                                    <td class='centro' style='width: 13%'>{TUE}</td>
                                    <td class='centro'  style='width: 20%'>{TTE}</td>

                                </tr>");

            builder.Append($@"<tr  style='background:#f1eeec'>
                                    <td colspan='2' style='width: 47%' class='left'>TOTAL USO DE BASE CON MEDIDOR</td>
                                    <td class='centro' style='width: 13%'>{totalConMedidorU}</td>
                                    <td class='centro'  style='width: 13%'>{totalConMedidorT}</td>
                                    <td class='centro' style='width: 13%'>{totalConMedidorUE}</td>
                                    <td class='centro'  style='width: 20%'>{totalConMedidorTE}</td>
                              </tr> 
                            </tbody>
                        </table>");

            return builder.ToString();
        }
        public string GetHtml2(object data, string year, string mes)
        {
            decimal GTU, GTT, GTI;
            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);
            List<object> ODsta = JsonConvert.DeserializeObject<List<object>>(data.ToString());

            List<string> accumulated = JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(ODsta.ElementAt(2)));
            List<string> accumulatede = JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(ODsta.Last()));
            data = JsonConvert.SerializeObject(ODsta.First());
            List<SOAPAP.Reportes.Finanzas.Formato2> ODataMes = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato2>>(data.ToString());
            data = JsonConvert.SerializeObject(ODsta.ElementAt(1));
            List<SOAPAP.Reportes.Finanzas.Formato2> ODataEnero = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato2>>(data.ToString());


           
            //datos para uso domestico
            TUsuarios = ODataMes.Where(x => x.uso == "HA").ToList().Count + int.Parse(accumulated.First());
            TTomas = ODataMes.Where(x => x.uso == "HA" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulated.First());
            TUE = ODataEnero.Where(x => x.uso == "HA").ToList().Count + int.Parse(accumulatede.First());
            TTE = ODataEnero.Where(x => x.uso == "HA" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulatede.First());

            GTU = totalConMedidorU;
            GTT = totalConMedidorT;
            GTUE = int.Parse(totalConMedidorUE.ToString());
            GTTE = int.Parse(totalConMedidorTE.ToString());

            totalConMedidorU = TUsuarios;
            totalConMedidorT = TTomas;
            totalConMedidorUE = TUE;
            totalConMedidorTE = TTE;
            StringBuilder builder = new StringBuilder();

            builder.Append($@"<table id='datos' style='width: 99%;margin-top:10px' class='actuales'>
                            <caption style='background:#f1eeec'>USO DE LA BASE SIN MEDIDOR</caption>
                            <tbody>
                                <tr>
                                    <td colspan='2' style='width: 47%' class='left'>USO DOMÉSTICO</td>
                                    <td class='centro' style='width: 13%'>{TUsuarios}</td>
                                    <td class='centro'  style='width: 13%'>{TTomas}</td>
                                    <td class='centro' style='width: 13%'>{TUE}</td>
                                    <td class='centro'  style='width: 20%'>{TTE}</td>
                                </tr>");
            //Uso no domestico
            TUsuarios = ODataMes.Where(x => x.uso == "CO").ToList().Count + int.Parse(accumulated.ElementAt(1));
            TTomas = ODataMes.Where(x => x.uso == "CO" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulated.ElementAt(1));
            TUE = ODataEnero.Where(x => x.uso == "CO").ToList().Count + int.Parse(accumulatede.ElementAt(1));
            TTE = ODataEnero.Where(x => x.uso == "CO" && x.type_agreement == "AGR01").ToList().Count + +int.Parse(accumulatede.ElementAt(1));


            totalConMedidorU += TUsuarios;
            totalConMedidorT += TTomas;
            totalConMedidorUE += TUE;
            totalConMedidorTE += TTE;


            builder.Append($@"<tr>
                                     <td colspan='2' style='width: 47%' class='left'>USO NO DOMÉSTICO (COMERCIAL)</td>
                                    <td class='centro' style='width: 13%'>{TUsuarios}</td>
                                    <td class='centro'  style='width: 13%'>{TTomas}</td>
                                    <td class='centro' style='width: 13%'>{TUE}</td>
                                    <td class='centro'  style='width: 20%'>{TTE}</td>
                                </tr>");

            //otros usos
            TUsuarios = ODataMes.Where(x => x.uso != "CO" && x.uso != "HA").ToList().Count + int.Parse(accumulated.Last());
            TTomas = ODataMes.Where(x => x.uso != "CO" && x.uso != "HA" && x.type_agreement == "AGR01").ToList().Count + int.Parse(accumulated.Last());
            TUE = ODataEnero.Where(x => x.uso != "CO" && x.uso != "HA").ToList().Count + +int.Parse(accumulatede.Last());
            TTE = ODataEnero.Where(x => x.uso != "CO" && x.uso != "HA" && x.type_agreement == "AGR01").ToList().Count + +int.Parse(accumulatede.Last());


            totalConMedidorU += TUsuarios;
            totalConMedidorT += TTomas;
            totalConMedidorUE += TUE;
            totalConMedidorTE += TTE;

            builder.Append($@"<tr>
                                     <td colspan='2' style='width: 47%' class='left'>OTROS USOS (INDUSTRIAL, PRESTADOR DE SERVICIOS)</td>
                                    <td class='centro' style='width: 13%'>{TUsuarios}</td>
                                    <td class='centro'  style='width: 13%'>{TTomas}</td>
                                    <td class='centro' style='width: 13%'>{TUE}</td>
                                    <td class='centro'  style='width: 20%'>{TTE}</td>
                                </tr>");
            builder.Append($@"<tr  style='background:#f1eeec'>
                                   <td colspan='2' style='width: 47%' class='left'>TOTAL USO DE LA BASE SIN MEDIDOR</td>
                                     
                                    <td class='centro' style='width: 13%'>{totalConMedidorU}</td>
                                    <td class='centro'  style='width: 13%'>{totalConMedidorT}</td>
                                    <td class='centro' style='width: 13%'>{totalConMedidorUE}</td>
                                    <td class='centro'  style='width: 20%'>{totalConMedidorTE}</td>   
                                </tr> 
                            </tbody>
                        </table>");
            builder.Append($@"<table id='datos' style='width: 99%;margin-top: 10px' class='actuales'>
                                <tbody>
                                    <tr  style='background:#f1eeec' class='centro'>
                                    <td colspan='2' style='width: 47%' class='centro'>TOTAL</td>
                                     
                                    <td class='centro' style='width: 13%'>{GTU + totalConMedidorU}</td>
                                    <td class='centro'  style='width: 13%'>{GTT + totalConMedidorT}</td>
                                    <td class='centro' style='width: 13%'>{GTUE + totalConMedidorUE}</td>
                                    <td class='centro'  style='width: 20%'>{GTTE + totalConMedidorTE}</td>    
                                    </tr>       
                                </tbody>
                                </table>");

            var RsultData = Task.Run(() => GetUsuarios(mes, year.ToString(), 1)).Result;

            Result = JsonConvert.DeserializeObject<List<Formato23Desgloce>>(JsonConvert.SerializeObject(RsultData.First()));
            var total = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(RsultData.Last()));
            int actuales ;
            int anteriores ;
            int mixto ;

            if (Result.Count > 0) {
                 actuales = Result.Where(x => x.tipo == "actuales").First().usuarios + total.First();

                 anteriores = Result.Where(x => x.tipo == "anteriores").First().usuarios + total.ElementAt(1);

                 mixto = Result.Where(x => x.tipo == "mixto").First().usuarios + total.Last();
            }
            else
            {
                 actuales = total.First();

                 anteriores = total.ElementAt(1);

                 mixto = total.Last();
            }
            builder.Append($@"<table id='datos' style='width: 99%;margin-top: 10px' >
                                <tbody>
                                    <tr  style='background:#f1eeec' class='centro'>
                                            <td colspan='2' style='width: 25%' class='centro'>No DE USUARIOS y No DE TOMAS QUE REALIZARON ÚNICAMENTE PAGOS DE {year}<br>C</td>
                                            <td  colspan='2' style='width: 25%'  class='centro'>No DE USUARIOS y No DE TOMAS QUE REALIZARON ÚNICAMENTE PAGOS DE EJERCICIOS ANTERIORES A {year}<br>D</td>
                                            <td colspan='2'  style='width: 25%' class='centro'>No DE USUARIOS y No DE TOMAS QUE REALIZARON TANTO PAGOS DE {year} Y EJERCICIOS ANTERIORES A {year}<br>E</td>
                                            <td  colspan='2' style='width: 25%' class='centro'>No DE USUARIOS y No DE TOMAS QUE REALIZARON  PAGOS EN EL MES <br>F=C+D+E</td>
                                    </tr>
                                    <tr  style='background:#f1eeec' class='centro'>
                                            <td style='width: 12.5%' class='centro'>No DE USUARIOS<br>C1</td>
                                            <td style='width: 12.5%' class='centro'>No DE TOMAS<br>C2</td>
                                            <td style='width: 12.5%' class='centro'>No DE USUARIOS<br>D1</td>
                                            <td style='width: 12.5%' class='centro'>No DE TOMAS<br>D2</td>
                                            <td style='width: 12.5%' class='centro'>No DE USUARIOS<br>E1</td>
                                            <td style='width: 12.5%' class='centro'>No DE TOMAS<br>E2</td>
                                            <td style='width: 12.5%' class='centro'>No DE USUARIOS<br>F1=C1+D1+E1</td>
                                            <td style='width: 12.5%' class='centro'>No DE TOMAS<br>F=C2+D2+E2</td>   
                                    </tr>
                                    <tr   class='centro'>
                                            <td style='width: 12.5%'>{actuales}</td>
                                            <td style='width: 12.5%'>{actuales}</td>
                                            <td  style='width: 12.5%'>{anteriores}</b></td>
                                            <td style='width: 12.5%'>{anteriores}</td>
                                            <td style='width: 12.5%'>{mixto}</td>
                                            <td style='width: 12.5%'>{mixto}</td>
                                            <td   style='width: 12.5%'>{actuales + anteriores + mixto }</td>
                                            <td   style='width: 12.5%'>{actuales + anteriores + mixto }</td>
                                    </tr>    
                                </tbody>
                              </table>");

            return builder.ToString();
        }

    }
}

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
    public partial class Formato2 : Form
    {
        private decimal TImporte = 0, totalConMedidorU, totalConMedidorT, totalConMedidorI = 0;
        private decimal totalSinMedidorU, totalSinMedidorT, totalSinMedidorI = 0;
        private int TUsuarios = 0, TTomas = 0;
        List<Formato23Desgloce> Result;
        private int NumCallGetParams;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        public Formato2()
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
            List<SOAPAP.Reportes.Finanzas.Formato2> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato2>>(data.ToString());
            //datos para uso domestico
            TUsuarios = OData.Where(x => x.uso == "HA").ToList().Count;
            TTomas = OData.Where(x => x.uso == "HA" && x.type_agreement == "AGR01").ToList().Count;
            TImporte = OData.Where(x => x.uso == "HA").ToList().Sum(x => x.importe);

            textUDSM.Text = TUsuarios.ToString();
            textTDSM.Text = TTomas.ToString();
            textIDSM.Text = TImporte.ToString();

            //totales
            textTUSM.Text = TUsuarios.ToString();
            textTTSM.Text = TTomas.ToString();
            textTISM.Text = TImporte.ToString();



            //datos para uso no domestico
            TUsuarios = OData.Where(x => x.uso == "CO").ToList().Count;
            TTomas = OData.Where(x => x.uso == "CO" && x.type_agreement == "AGR01").ToList().Count;
            TImporte = OData.Where(x => x.uso == "CO").ToList().Sum(x => x.importe);

            textUNDSM.Text = TUsuarios.ToString();
            textTNDSM.Text = TTomas.ToString();
            textINDSM.Text = TImporte.ToString();

            textTUSM.Text = (decimal.Parse(textTUSM.Text) + TUsuarios).ToString();
            textTTSM.Text = (decimal.Parse(textTTSM.Text) + TTomas).ToString();
            textTISM.Text = (decimal.Parse(textTISM.Text) + TImporte).ToString();



            //datos para otros usos
            TUsuarios = OData.Where(x => x.uso != "CO" && x.uso != "HA" && x.uso != "NO").ToList().Count;
            TTomas = OData.Where(x => x.uso != "CO" && x.uso != "HA" && x.type_agreement == "AGR01" && x.uso != "NO").ToList().Count;
            TImporte = OData.Where(x => x.uso != "CO" && x.uso != "HA" && x.uso != "NO").ToList().Sum(x => x.importe);

            textUOSM.Text = TUsuarios.ToString();
            textTOSM.Text = TTomas.ToString();
            textIOSM.Text = TImporte.ToString();

            textTUSM.Text = (decimal.Parse(textTUSM.Text) + TUsuarios).ToString();
            textTTSM.Text = (decimal.Parse(textTTSM.Text) + TTomas).ToString();
            textTISM.Text = (decimal.Parse(textTISM.Text) + TImporte).ToString();



            textIDSM.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", decimal.Parse(textIDSM.Text));
            textINDSM.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", decimal.Parse(textINDSM.Text));
            textIOSM.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", decimal.Parse(textIOSM.Text));
            textTISM.Text = textTISM.Text;
         

            textGTU.Text = (int.Parse(textTUCM.Text) + int.Parse(textTUSM.Text)).ToString();
            textGTT.Text = (int.Parse(textTTCM.Text) + int.Parse(textTTSM.Text)).ToString();
            textGTI.Text = (decimal.Parse(textTICM.Text) + decimal.Parse(textTISM.Text)).ToString();

            textIDCM.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", decimal.Parse(textIDCM.Text));
            textINDCM.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", decimal.Parse(textINDCM.Text));
            textIOCM.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", decimal.Parse(textIOCM.Text));
            textTICM.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", decimal.Parse(textTICM.Text));

            textGTI.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", decimal.Parse(textGTI.Text));
            var totalO = OData.Where(x => x.uso == "NO").ToList();
            textTISM.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", decimal.Parse(textTISM.Text)/* + totalO.Sum(x => x.importe)*/);

            lblC.Text = $@"No DE USUARIOS Y No DE TOMAS QUE REALIZARON ÚNICAMENTE PAGOS DE {year}
                        (C)";

            lblD.Text = $@"No DE USUARIOS Y No DE TOMAS QUE REALIZARON ÚNICAMENTE PAGOS DE EJERCICIOS ANTERIORES A {year}
                        (D)";

            lblE.Text = $@"No DE USUARIOS Y No DE TOMAS QUE QUE REALIZARON TANTO PAGOS DE {year} COMO DE EJERCICIOS ANTERIORES A {year}
                        (E)";
            lblRecau.Text = $"RECAUDACIÓN DEL EJERCICIO {year} Y DE EJERCICIOS ANTERIORES A {year}";




        }



        /// <summary>
        /// Aignar valores a los controles de uso con medidor
        /// </summary>
        public void DrawData(object data, string mes, string year)
        {

            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);
            List<SOAPAP.Reportes.Finanzas.Formato2> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato2>>(data.ToString());
            //datos para uso domestico
            TUsuarios = OData.Where(x => x.uso == "HA").ToList().Count;
            TTomas = OData.Where(x => x.uso == "HA" && x.type_agreement == "AGR01").ToList().Count;
            TImporte = OData.Where(x => x.uso == "HA").ToList().Sum(x => x.importe);

            textUDCM.Text = TUsuarios.ToString();
            textTDCM.Text = TTomas.ToString();
            textIDCM.Text = TImporte.ToString();

            //totales
            textTUCM.Text = TUsuarios.ToString();
            textTTCM.Text = TTomas.ToString();
            textTICM.Text = TImporte.ToString();



            //datos para uso no domestico
            TUsuarios = OData.Where(x => x.uso == "CO").ToList().Count;
            TTomas = OData.Where(x => x.uso == "CO" && x.type_agreement == "AGR01").ToList().Count;
            TImporte = OData.Where(x => x.uso == "CO").ToList().Sum(x => x.importe);

            textUNDCM.Text = TUsuarios.ToString();
            textTNDCM.Text = TTomas.ToString();
            textINDCM.Text = TImporte.ToString();

            textTUCM.Text = (decimal.Parse(textTUCM.Text) + TUsuarios).ToString();
            textTTCM.Text = (decimal.Parse(textTTCM.Text) + TTomas).ToString();
            textTICM.Text = (decimal.Parse(textTICM.Text) + TImporte).ToString();



            //datos para otros usos
            TUsuarios = OData.Where(x => x.uso != "CO" && x.uso != "HA" && x.uso!= "NO" && x.uso != "NO").ToList().Count;
            TTomas = OData.Where(x => x.uso != "CO" && x.uso != "HA" && x.uso != "NO"  && x.type_agreement == "AGR01" && x.uso != "NO").ToList().Count;
            TImporte = OData.Where(x => x.uso != "CO" && x.uso != "HA" && x.uso != "NO" && x.uso != "NO").ToList().Sum(x => x.importe);

            textUOCM.Text = TUsuarios.ToString();
            textTOCM.Text = TTomas.ToString();
            textIOCM.Text = TImporte.ToString();

            textTUCM.Text = (decimal.Parse(textTUCM.Text) + TUsuarios).ToString();
            textTTCM.Text = (decimal.Parse(textTTCM.Text) + TTomas).ToString();
            textTICM.Text = (decimal.Parse(textTICM.Text) + TImporte).ToString();

            textGTI.Text = "0";
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

        private async Task<List<Formato23Desgloce>> GetUsuarios(string mes, string year, int isReturn =0)
        {
            List<object> content = new List<object>();
            content.Add(new { Key = "mesPresentarInicio", Value = mes, DbType = DbType.Int32 });
            content.Add(new { Key = "mesPresentarFin", Value = mes, DbType = DbType.Int32 });
            content.Add(new { Key = "yearPeriodo", Value = year, DbType = DbType.Int32 });

            var json = JsonConvert.SerializeObject(content);


            var StringContent = new StringContent(json, Encoding.UTF8, "application/json");
            string url = string.Format("/api/StoreProcedure/RunFormato23Desgloce/true");



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
                    var JdDta = JObject.Parse(results);
                    var data = JsonConvert.SerializeObject(JdDta["data"]);
                    List<object> LData = JsonConvert.DeserializeObject<List<object>>(data);
                    List<Formato23Desgloce> Lusuarios = JsonConvert.DeserializeObject<List<Formato23Desgloce>>(JsonConvert.SerializeObject(LData.First()));
                 
                    if (isReturn == 0)
                    {
                        //var total = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(LData.Last()));

                        textC1.Text = Lusuarios.Where(x => x.tipo == "actuales").First().usuarios.ToString();
                        textC2.Text = Lusuarios.Where(x => x.tipo == "actuales").First().usuarios.ToString();
                        textD1.Text = (Lusuarios.Where(x => x.tipo == "anteriores").First().usuarios).ToString();
                        textD2.Text = (Lusuarios.Where(x => x.tipo == "anteriores").First().usuarios).ToString();
                        textE1.Text = Lusuarios.Where(x => x.tipo == "mixto").First().usuarios.ToString();
                        textE2.Text = Lusuarios.Where(x => x.tipo == "mixto").First().usuarios.ToString();
                        textF1.Text = (int.Parse(textC1.Text) + int.Parse(textD1.Text) + int.Parse(textE1.Text)).ToString();
                        textF2.Text = (int.Parse(textC2.Text) + int.Parse(textD2.Text) + int.Parse(textE2.Text)).ToString();




                        return null;
                    }
                    else
                    {
                        this.Result = Lusuarios;
                        return Lusuarios;
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
            List<SOAPAP.Reportes.Finanzas.Formato2> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato2>>(data.ToString());

            //datos para uso domestico
            TUsuarios = OData.Where(x => x.uso == "HA").ToList().Count;
            TTomas = OData.Where(x => x.uso == "HA" && x.type_agreement == "AGR01").ToList().Count;
            TImporte = OData.Where(x => x.uso == "HA").ToList().Sum(x => x.importe);
           
            totalConMedidorU = TUsuarios;
            totalConMedidorT = TTomas;
            totalConMedidorI = TImporte;
            StringBuilder builder = new StringBuilder();
            builder.Append($@"<table id='datos' style='width: 99%;'>
                            <tbody>
                                <tr>
                                        <td colspan='3' rowspan='2' style='width: 47%' class='centro'>CONCEPTO</td>
                    
                                        <td colspan='3' class='centro'>RECAUDACIÓN DEL EJERCICIO {year} y DE EJERCICIOS AMTERIORES A {year}</td>                   
                                </tr>
                                <tr>                         
                                        <td class='centro' style='width: 18%'>No DE USUARIOS<br>(A)</td>
                                        <td class='centro'  style='width: 17%'>No DE TOMAS <br>(B)</td>
                                        <td class='centro'  style='width: 20%'>IMPORTE <br>(PESOS)</td>
                                    </tr>
                            </tbody>
                            </table>");

            builder.Append("<h3 style='text-align: center'>DERECHOS POR SUMINISTRO DE AGUA <br> INGRESOS</h3>");
            builder.Append($@"<table id='datos' style='width: 99%' class='actuales'>
                            <caption style='background:#f1eeec'>USO DE LA BASE CON MEDIDOR</caption>
                            <tbody>
                                <tr>
                                    <td style='width: 40%' class='left'>USO DOMÉSTICO</td>
                                    <td style='width: 15%'>{TUsuarios}</td>
                                    <td style='width: 15%'>{TTomas}</td>
                                    <td style='width: 15%'>{string.Format(new CultureInfo("es-MX"), "{0:C2}", TImporte)}</td>
                                </tr>");
            TUsuarios = OData.Where(x => x.uso == "CO").ToList().Count;
            TTomas = OData.Where(x => x.uso == "CO" && x.type_agreement == "AGR01").ToList().Count;
            TImporte = OData.Where(x => x.uso == "CO").ToList().Sum(x => x.importe);

            totalConMedidorU+= TUsuarios;
            totalConMedidorT+= TTomas;
            totalConMedidorI+= TImporte;
            builder.Append($@"<tr>
                                    <td style='width: 40%' class='left'>USO NO DOMÉSTICO (COMERCIAL)</td>
                                    <td style='width: 15%'>{TUsuarios}</td>
                                    <td style='width: 15%'>{TTomas}</td>
                                    <td style='width: 15%'>{string.Format(new CultureInfo("es-MX"), "{0:C2}", TImporte)}</td>
                                </tr>");
            TUsuarios = OData.Where(x => x.uso != "CO" && x.uso != "HA" && x.uso != "NO").ToList().Count;
            TTomas = OData.Where(x => x.uso != "CO" && x.uso != "HA" && x.type_agreement == "AGR01" && x.uso != "NO").ToList().Count;
            TImporte = OData.Where(x => x.uso != "CO" && x.uso != "HA" && x.uso != "NO").ToList().Sum(x => x.importe);
            totalConMedidorU += TUsuarios;
            totalConMedidorT += TTomas;
            totalConMedidorI += TImporte;

            builder.Append($@"<tr>
                                    <td style='width: 40%' class='left'>OTROS USOS (INDUSTRIAL, PRESTADOR DE SERVICIOS)</td>
                                    <td style='width: 15%'>{TUsuarios}</td>
                                    <td style='width: 15%'>{TTomas}</td>
                                    <td style='width: 15%'>{string.Format(new CultureInfo("es-MX"), "{0:C2}", TImporte)}</td>
                                </tr>");
            builder.Append($@"<tr  style='background:#f1eeec'>
                                        <td style='width: 40%' class='left'>TOTAL USO DE LA BASE CON MEDIDOR</td>
                                        <td style='width: 15%'><b>{totalConMedidorU}</b></td>
                                        <td style='width: 15%'><b>{totalConMedidorT}</b></td>
                                        <td style='width: 15%'><b>{string.Format(new CultureInfo("es-MX"), "{0:C2}", totalConMedidorI)}</b></td>    
                                </tr> 
                            </tbody>
                        </table>");
           
            return builder.ToString();
        }
        public  string GetHtml2(object data, string year, string mes)
        {
            decimal GTU, GTT, GTI;
            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);
            List<SOAPAP.Reportes.Finanzas.Formato2> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato2>>(data.ToString());
            //datos para uso domestico
            TUsuarios = OData.Where(x => x.uso == "HA" ).ToList().Count;
            TTomas = OData.Where(x => x.uso == "HA" && x.type_agreement == "AGR01" ).ToList().Count;
            TImporte = OData.Where(x => x.uso == "HA" ).ToList().Sum(x => x.importe);
            GTU = totalConMedidorU;
            GTT = totalConMedidorT;
            GTI = totalConMedidorI;
            totalConMedidorU = TUsuarios;
            totalConMedidorT = TTomas;
            totalConMedidorI = TImporte;
            StringBuilder builder = new StringBuilder();
       
            builder.Append($@"<table id='datos' style='width: 99%;margin-top:10px' class='actuales'>
                            <caption style='background:#f1eeec'>USO DE LA BASE SIN MEDIDOR</caption>
                            <tbody>
                                <tr>
                                    <td style='width: 40%' class='left'>USO DOMÉSTICO</td>
                                    <td style='width: 15%'>{TUsuarios}</td>
                                    <td style='width: 15%'>{TTomas}</td>
                                    <td style='width: 15%'>{string.Format(new CultureInfo("es-MX"), "{0:C2}", TImporte)}</td>
                                </tr>");
            TUsuarios = OData.Where(x => x.uso == "CO").ToList().Count;
            TTomas = OData.Where(x => x.uso == "CO" && x.type_agreement == "AGR01").ToList().Count;
            TImporte = OData.Where(x => x.uso == "CO").ToList().Sum(x => x.importe);

            totalConMedidorU += TUsuarios;
            totalConMedidorT += TTomas;
            totalConMedidorI += TImporte;
            builder.Append($@"<tr>
                                    <td style='width: 40%' class='left'>USO NO DOMÉSTICO (COMERCIAL)</td>
                                    <td style='width: 15%'>{TUsuarios}</td>
                                    <td style='width: 15%'>{TTomas}</td>
                                    <td style='width: 15%'>{string.Format(new CultureInfo("es-MX"), "{0:C2}", TImporte)}</td>
                                </tr>");

            TUsuarios = OData.Where(x => x.uso != "CO" && x.uso != "HA" && x.uso != "NO").ToList().Count;
            TTomas = OData.Where(x => x.uso != "CO" && x.uso != "HA" && x.type_agreement == "AGR01" && x.uso != "NO").ToList().Count;
            TImporte = OData.Where(x => x.uso != "CO" && x.uso != "HA" && x.uso != "NO").ToList().Sum(x => x.importe);
            totalConMedidorU += TUsuarios;
            totalConMedidorT += TTomas;
            totalConMedidorI += TImporte;

            builder.Append($@"<tr>
                                    <td style='width: 40%' class='left'>OTROS USOS (INDUSTRIAL, PRESTADOR DE SERVICIOS)</td>
                                    <td style='width: 15%'>{TUsuarios}</td>
                                    <td style='width: 15%'>{TTomas}</td>
                                    <td style='width: 15%'>{string.Format(new CultureInfo("es-MX"), "{0:C2}", TImporte)}</td>
                                </tr>");
            builder.Append($@"<tr  style='background:#f1eeec'>
                                        <td style='width: 40%' class='left'>TOTAL USO DE LA BASE SIN MEDIDOR</td>
                                        <td style='width: 15%'><b>{totalConMedidorU}</b></td>
                                        <td style='width: 15%'><b>{totalConMedidorT}</b></td>
                                        <td style='width: 15%'><b>{string.Format(new CultureInfo("es-MX"), "{0:C2}", totalConMedidorI)}</b></td>    
                                </tr> 
                            </tbody>
                        </table>");
            var totalO = OData.Where(x => x.uso == "NO").ToList();
            builder.Append($@"<table id='datos' style='width: 99%;margin-top: 10px' class='actuales'>
                                <tbody>
                                    <tr  style='background:#f1eeec' class='centro'>
                                            <td style='width: 40%' class='left'>TOTAL</td>
                                            <td style='width: 15%'><b>{GTU + totalConMedidorU}</b></td>
                                            <td style='width: 15%'><b>{GTT + totalConMedidorT}</b></td>
                                            <td style='width: 15%'><b>{string.Format(new CultureInfo("es-MX"), "{0:C2}",GTI+totalConMedidorI /*+ totalO.Sum(x => x.importe)*/)}</b></td>    
                                    </tr>       
                                </tbody>
                                </table>");
           
            Result = Task.Run( () =>  GetUsuarios(mes, year.ToString(), 1)).Result;
            
           
            int actuales = Result.Where(x => x.tipo == "actuales").First().usuarios;

            int anteriores = Result.Where(x => x.tipo == "anteriores").First().usuarios ;

            int mixto = Result.Where(x => x.tipo == "mixto").First().usuarios;
           
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
                                            <td   style='width: 12.5%'>{actuales  + anteriores  + mixto }</td>
                                            <td   style='width: 12.5%'>{actuales  + anteriores  + mixto }</td>
                                    </tr>    
                                </tbody>
                              </table>");
                              
            return builder.ToString();
        }
    }
}

using Newtonsoft.Json;
using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Facturacionagrupada
    {
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        querys q = new querys();

        public Facturacionagrupada()
        {
            Requests = new RequestsAPI(UrlBase);
        }

        public async Task<string> facturar(string status, string uuid, TrasactionVMA transactions,bool checks,string xmla)
        {
            string respuesta = string.Empty;
            string rutas = string.Empty;
            string json = string.Empty;
            int contadordeiva = 0;
            int contadordeiva1 = 0;
            bool check = false;
            
            List<PaymentDetails> ps = new List<PaymentDetails>();
             transactions.lstPayment.ToList().ForEach(s => {
                 ps.AddRange(s.paymentDetails.ToList());
             });

            ps = ps.ToList().GroupBy(v => new { v.description, v.amount, v.tax, v.accountNumber, v.unitMeasurement }).Select(g => new PaymentDetails {
                description = g.Key.description,
                amount = g.Sum(xt => xt.amount),
                accountNumber = g.Key.accountNumber,
                unitMeasurement = g.Key.unitMeasurement
            }).ToList();
            
            string seriefolio = transactions.lstTransaction.FirstOrDefault().transactionFolios.FirstOrDefault().folio.ToString();
            string serie = string.Empty;
            string folio = string.Empty;
            string uuids = uuid;

            serie = seriefolio.Substring(0, 1);
            folio = seriefolio.Replace(serie, "");


            string fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><cfdi:Comprobante xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" ";
            xml = xml + "Version=\"3.3\" Fecha=\"" + fecha + "\" FormaPago=\"01\" SubTotal=\"" + transactions.lstTransaction.Sum(x => x.amount) + "\" Moneda=\"MXN\" Total=\"" + transactions.lstTransaction.Sum(x => x.total) + "\" TipoDeComprobante=\"I\" MetodoPago=\"PUE\" LugarExpedicion=\"72700\" Serie=\"" + serie + "\" Folio=\"" + folio + "\" xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\">";

            if (Variables.Configuration.CFDITest == "Verdadero")
            {
                xml = xml + "<cfdi:Emisor Rfc=\"TEST010203001\" Nombre=\"Una razón rh de cv\" RegimenFiscal=\"605\" />";
                xml = xml + "<cfdi:Receptor Rfc=\"TEST010203001\" Nombre=\"Pepe SA DE CV\" UsoCFDI=\"P01\"/>";
            }
            else
            {
                xml = xml + "<cfdi:Emisor Rfc=\"" + Variables.Configuration.RFC + "\" Nombre=\"" + Variables.Configuration.LegendRegime + "\" RegimenFiscal=\"603\" />";
                xml = xml + "<cfdi:Receptor Rfc=\"XEXX010101000\" Nombre=\"XEXX010101000\" UsoCFDI=\"P01\"/>";

            }

            xml = xml + "<cfdi:Conceptos>";

            foreach (var pay in ps)
            {

                xml = xml + "<cfdi:Concepto ClaveProdServ=\"" + pay.accountNumber + "\" Cantidad=\"1\" ClaveUnidad=\"" + pay.unitMeasurement + "\" Unidad=\"Unidad de servicio\"  Descripcion=\"" + pay.description + "\" ValorUnitario=\"" + pay.amount + "\" Importe=\"" + pay.amount + "\">";

                if (pay.haveTax == true)
                {
                    xml = xml + "<cfdi:Impuestos><cfdi:Traslados><cfdi:Traslado Base=\"" + pay.amount + "\" Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"0.160000\" Importe=\"" + pay.tax + "\" /></cfdi:Traslados></cfdi:Impuestos>";
                    xml = xml + "</cfdi:Concepto>";
                    contadordeiva = contadordeiva + 1;
                }
                else
                {
                    xml = xml + "</cfdi:Concepto>";
                }
            }

            foreach (var pays in ps)
            {
                if (pays.haveTax == true)
                {
                    if (check == false)
                    {
                        xml = xml + "</cfdi:Conceptos><cfdi:Impuestos TotalImpuestosTrasladados=\"" + transactions.lstTransaction.Sum(x => x.tax) + "\"><cfdi:Traslados>";
                        xml = xml + "<cfdi:Traslado  Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"0.160000\" Importe=\"" + transactions.lstTransaction.Sum(x => x.tax) + "\" />";
                        check = true;
                        contadordeiva1 = contadordeiva1 + 1;
                    }
                    else
                    {
                        contadordeiva1 = contadordeiva1 + 1;
                    }
                }
            }

            if (contadordeiva == contadordeiva1 && check == true)
            {
                xml = xml + "</cfdi:Traslados></cfdi:Impuestos></cfdi:Comprobante>";
            }

            else
            {
                xml = xml + "</cfdi:Conceptos></cfdi:Comprobante>";
            }

            string xmlcreado = xml;
            string xmltimbre = string.Empty;


            WsIntegral33Pruebas.WsCFDI33Client n = new WsIntegral33Pruebas.WsCFDI33Client();

            if (status == "ET002")
            {

                try
                {

                    String log_txt = "";
                    if (Variables.Configuration.CFDITest == "Verdadero")
                    {
                        log_txt = "C:/CFDI/TES030201001_b64.pem";
                    }
                    else
                    {
                        log_txt = "C:/CFDI/" + Variables.Configuration.CFDIKeyCancel + "";
                    }


                    var pkcs = System.IO.File.ReadAllText(log_txt);
                    Thread.Sleep(3000);


                    if (checks == false) { 
                    WsIntegral33Pruebas.RespuestaCancelacionV2 detalle = new WsIntegral33Pruebas.RespuestaCancelacionV2();
                    if (Variables.Configuration.CFDITest == "Verdadero")
                    {
                        detalle = n.CancelarCFDIConValidacion("CFDI010233001", "Pruebas1a$", "TES030201001", "267138D4-7E57-7E57-7E57-AD01F16DAC82", pkcs, "12345678a");

                    }
                    else
                    {
                        detalle = n.CancelarCFDIConValidacion(Variables.Configuration.CFDIUser, Variables.Configuration.CFDIPassword, "XEXX010101000", uuids, pkcs, Variables.Configuration.CFDICertificado);

                    }


                    if (detalle.OperacionExitosa)
                    {

                        List<Model.XML> xML = new List<Model.XML>();
                        transactions.lstPayment.ToList().ForEach(s =>
                        {
                            
                            s.taxReceipts.ToList().ForEach(r =>
                            {

                                if(s.id == r.paymentId)
                                {
                                    if (r.status == "ET001") { 

                                    Model.XML xMLS = new Model.XML();
                                    try
                                    {
                                        xMLS.RFC = "XEXX010101000";
                                    }
                                    catch (Exception)
                                    {

                                        xMLS.RFC = "XEXX010101000";

                                    }

                                    xMLS.status = status;
                                    xMLS.paymentId = s.id;
                                    xMLS.taxReceiptDate = DateTime.Now;
                                    xMLS.type = "CAT02";
                                    xMLS.xml = detalle.XMLAcuse;
                                    xMLS.fielXML = uuids;
                                    xMLS.userId = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().UserId;
                                    xML.Add(xMLS);
                                    }
                                }
                                
                            });

                        });

                         
                        HttpContent content;
                        json = JsonConvert.SerializeObject(xML);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        var jsonResponse = await Requests.SendURIAsync("api/TaxReceipt/CancelTaxReceipt", HttpMethod.Post, Variables.LoginModel.Token, content);
                        respuesta = "exito¡" + jsonResponse.ToString();
                        return respuesta;

                    }
                    else
                    {

                        respuesta = "error¡" + detalle.MensajeError + " " + detalle.MensajeErrorDetallado;
                        return respuesta;

                    }
                    }
                    else
                    {
                        List<Model.XML> xML = new List<Model.XML>();
                        transactions.lstPayment.ToList().ForEach(s =>
                        {
                            s.taxReceipts.ToList().ForEach(r =>
                            {
                                if (s.id == r.paymentId)
                                {
                                    if(r.status== "ET001") { 

                                    Model.XML xMLS = new Model.XML();
                                    try
                                    {
                                        xMLS.RFC = "XEXX010101000";
                                    }
                                    catch (Exception)
                                    {

                                        xMLS.RFC = "XEXX010101000";

                                    }

                                    xMLS.status = status;
                                    xMLS.paymentId = s.id;
                                    xMLS.taxReceiptDate = DateTime.Now;
                                    xMLS.type = "CAT02";
                                    xMLS.xml = xmla;
                                    xMLS.fielXML = uuids;
                                    xMLS.userId = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().UserId;
                                    xML.Add(xMLS);
                                    }
                                }

                            });

                        });

                        HttpContent content;
                        json = JsonConvert.SerializeObject(xML);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        var jsonResponse = await Requests.SendURIAsync("api/TaxReceipt/agruped", HttpMethod.Post, Variables.LoginModel.Token, content);
                        respuesta = "exito¡" + jsonResponse.ToString();
                        return respuesta;

                    }
                }
                catch (Exception)
                {

                    respuesta = "error¡La Facturacion se encuentra fuera de linea";
                    return respuesta;
                }

            }
            else
            {

                try
                {

                    Thread.Sleep(3000);

                    WsIntegral33Pruebas.Respuesta respuestas = new WsIntegral33Pruebas.Respuesta();

                    if (Variables.Configuration.CFDITest == "Verdadero")
                    {
                        respuestas = n.Sellar("CFDI010233001", "Pruebas1a$", xmlcreado, "", false, 1, "", true);

                    }
                    else
                    {
                        respuestas = n.Sellar(Variables.Configuration.CFDIUser, Variables.Configuration.CFDIPassword, xmlcreado, "", false, 1, "", true);
                    }
                    
                    if (respuestas.exito)
                    {

                        List<Model.XML> xML = new List<Model.XML>();
                        transactions.lstPayment.ToList().ForEach(s => {
                            Model.XML xMLS = new Model.XML();
                            try
                            {
                                xMLS.RFC = "XEXX010101000";
                            }
                            catch (Exception)
                            {
                                xMLS.RFC = "XEXX010101000";
                            }
                            xMLS.status = status;
                            xMLS.paymentId = s.id;
                            xMLS.taxReceiptDate = DateTime.Now;
                            xMLS.type = "CAT01";
                            xMLS.xml = respuestas.xmlTimbrado;
                            xMLS.fielXML = respuestas.uuid;
                            xMLS.userId = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().UserId;
                            xML.Add(xMLS);
                        });

                        HttpContent content;
                        json = JsonConvert.SerializeObject(xML);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        var jsonResponse = await Requests.SendURIAsync("api/TaxReceipt/agruped", HttpMethod.Post, Variables.LoginModel.Token, content);
                        String log_txts = AppDomain.CurrentDomain.BaseDirectory;
                        if (respuestas.pdf != null)
                        {

                            System.IO.File.WriteAllBytes(log_txts + @"\PDF.pdf", respuestas.pdf);
                            rutas = log_txts + @"\PDF.pdf";

                        }

                        respuesta = rutas;
                        return respuesta;

                    }
                    else
                    {

                        respuesta = "error/" + respuestas.errorGeneral + " " + respuestas.errorEspecifico;
                        return respuesta;

                    }
                }
                catch (Exception)
                {
                    respuesta = "error/La Facturacion se encuentra fuera de linea";
                    return respuesta;
                }
            }
        }
    }
}
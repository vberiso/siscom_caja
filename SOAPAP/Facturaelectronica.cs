using Newtonsoft.Json;
using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SOAPAP
{
    class Facturaelectronica
    {

        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        querys q = new querys();

        public Facturaelectronica()
        {
            Requests = new RequestsAPI(UrlBase);
        }

        public async Task<string> facturar(string idtraccaction, string status, string uuid)
        {
            string respuesta = string.Empty;
            string rutas = string.Empty;
            string json = string.Empty;
            int contadordeiva = 0;
            int contadordeiva1 = 0;
            bool check = false;



            var resultado = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", idtraccaction), HttpMethod.Get, Variables.LoginModel.Token);
            TransactionVM m = JsonConvert.DeserializeObject<TransactionVM>(resultado);

            var resultados = await Requests.SendURIAsync(string.Format("/api/Agreements/AgreementByAccount/{0}", m.payment.AgreementId), HttpMethod.Get, Variables.LoginModel.Token);
            Agreement ms = JsonConvert.DeserializeObject<Agreement>(resultados);

            string seriefolio = m.transaction.transactionFolios.FirstOrDefault().folio.ToString();
            string serie = string.Empty;
            string folio = string.Empty;
            string uuids = uuid;

            serie = seriefolio.Substring(0, 1);
            folio = seriefolio.Replace(serie, "");

            string fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><cfdi:Comprobante xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" ";
            xml = xml + "Version=\"3.3\" Fecha=\"" + fecha + "\" FormaPago=\"" + m.transaction.payMethod.code + "\" SubTotal=\"" + m.transaction.amount + "\" Moneda=\"MXN\" Total=\"" + m.transaction.total + "\" TipoDeComprobante=\"I\" MetodoPago=\"PUE\" LugarExpedicion=\"72700\" Serie=\"" + serie + "\" Folio=\"" + folio + "\" xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\">";

            if (Variables.Configuration.CFDITest == "Verdadero")
            {
                xml = xml + "<cfdi:Emisor Rfc=\"TEST010203001\" Nombre=\"Una razón rh de cv\" RegimenFiscal=\"605\" />";
                xml = xml + "<cfdi:Receptor Rfc=\"TEST010203001\" Nombre=\"Pepe SA DE CV\" UsoCFDI=\"P01\"/>";
            }
            else
            {
                xml = xml + "<cfdi:Emisor Rfc=\"" + Variables.Configuration.RFC + "\" Nombre=\"" + Variables.Configuration.LegendRegime + "\" RegimenFiscal=\"603\" />";
                xml = xml + "<cfdi:Receptor Rfc=\"" + ms.Clients.FirstOrDefault().rfc + "\" Nombre=\"" + ms.Clients.FirstOrDefault().name + " " + ms.Clients.FirstOrDefault().lastName + " " + ms.Clients.FirstOrDefault().secondLastName + " \" UsoCFDI=\"P01\"/>";

            }


            xml = xml + "<cfdi:Conceptos>";

            foreach (var pay in m.payment.PaymentDetails)
            {

                xml = xml + "<cfdi:Concepto ClaveProdServ=\"" + pay.AccountNumber + "\" Cantidad=\"1\" ClaveUnidad=\"" + pay.UnitMeasurement + "\" Unidad=\"Unidad de servicio\"  Descripcion=\"" + pay.Description + "\" ValorUnitario=\"" + pay.Amount + "\" Importe=\"" + pay.Amount + "\">";

                if (pay.HaveTax == true)
                {
                    xml = xml + "<cfdi:Impuestos><cfdi:Traslados><cfdi:Traslado Base=\"" + pay.Amount + "\" Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"0.160000\" Importe=\"" + pay.Tax + "\" /></cfdi:Traslados></cfdi:Impuestos>";
                    xml = xml + "</cfdi:Concepto>";
                    contadordeiva = contadordeiva + 1;
                }
                else
                {
                    xml = xml + "</cfdi:Concepto>";
                }
            }

            foreach (var pays in m.payment.PaymentDetails)
            {
                if (pays.HaveTax == true)
                {
                    if (check == false)
                    {
                        xml = xml + "</cfdi:Conceptos><cfdi:Impuestos TotalImpuestosTrasladados=\"" + m.transaction.tax + "\"><cfdi:Traslados>";
                        xml = xml + "<cfdi:Traslado  Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"0.160000\" Importe=\"" + m.transaction.tax + "\" />";
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


                    WsIntegral33Pruebas.RespuestaCancelacionV2 detalle = new WsIntegral33Pruebas.RespuestaCancelacionV2();
                    if (Variables.Configuration.CFDITest == "Verdadero")
                    {
                        detalle = n.CancelarCFDIConValidacion("CFDI010233001", "Pruebas1a$", "TES030201001", "267138D4-7E57-7E57-7E57-AD01F16DAC82", pkcs, "12345678a");

                    }
                    else
                    {
                        detalle = n.CancelarCFDIConValidacion(Variables.Configuration.CFDIUser, Variables.Configuration.CFDIPassword, ms.Clients.FirstOrDefault().rfc, uuids, pkcs, Variables.Configuration.CFDICertificado);

                    }


                    if (detalle.OperacionExitosa)
                    {

                        //ET001
                        //ET002

                        Model.TaxReceipt xMLS = new Model.TaxReceipt();
                        try
                        {
                            xMLS.RFC = ms.Clients.FirstOrDefault(x => x.typeUser == "CLI01").rfc;
                        }
                        catch (Exception)
                        {

                            xMLS.RFC = "XEXX010101000";

                        }

                        xMLS.Status = status;
                        xMLS.PaymentId = m.payment.Id;
                        xMLS.TaxReceiptDate = DateTime.Now;
                        xMLS.Type = "CAT02";
                        xMLS.Xml = detalle.XMLAcuse;
                        xMLS.FielXML = uuids;
                        xMLS.UserId = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().UserId;
                        HttpContent content;
                        json = JsonConvert.SerializeObject(xMLS);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        var jsonResponse = await Requests.SendURIAsync("/api/TaxReceipt/", HttpMethod.Post, Variables.LoginModel.Token, content);
                        m.payment.HaveTaxReceipt = true;
                        content = new StringContent(JsonConvert.SerializeObject(m.payment), Encoding.UTF8, "application/json");
                        var updatePayment = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", m.payment.Id), HttpMethod.Put, Variables.LoginModel.Token, content);
                        respuesta = rutas;
                        return respuesta;

                    }
                    else
                    {

                        respuesta = "error/" + detalle.MensajeError + " " + detalle.MensajeErrorDetallado;
                        return respuesta;

                    }
                }
                catch (Exception)
                {

                    respuesta = "error/La Facturacion se encuentra fuera de linea";
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

                        //ET001
                        //ET002

                        Model.TaxReceipt xMLS = new Model.TaxReceipt();
                        try
                        {
                            xMLS.RFC = ms.Clients.FirstOrDefault(x => x.typeUser == "CLI01").rfc;
                        }
                        catch (Exception)
                        {

                            xMLS.RFC = "XEXX010101000";

                        }

                        xMLS.Status = status;
                        xMLS.PaymentId = m.payment.Id;
                        xMLS.TaxReceiptDate = DateTime.Now;
                        xMLS.Type = "CAT01";
                        xMLS.Xml = respuestas.xmlTimbrado;
                        xMLS.FielXML = respuestas.uuid;
                        xMLS.UserId = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().UserId;
                        HttpContent content;
                        json = JsonConvert.SerializeObject(xMLS);

                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        var jsonResponse = await Requests.SendURIAsync("/api/TaxReceipt/", HttpMethod.Post, Variables.LoginModel.Token, content);

                        m.payment.HaveTaxReceipt = true;
                        content = new StringContent(JsonConvert.SerializeObject(m.payment), Encoding.UTF8, "application/json");

                        var updatePayment = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", m.payment.Id), HttpMethod.Put, Variables.LoginModel.Token, content);
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


        public async Task<string> facturarPago(string idtraccaction, string status, string uuid)
        {
            string respuesta = string.Empty;
            string rutas = string.Empty;
            string json = string.Empty;
            int contadordeiva = 0;
            int contadordeiva1 = 0;
            bool check = false;
            
            var resultado = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", idtraccaction), HttpMethod.Get, Variables.LoginModel.Token);
            TransactionVM m = JsonConvert.DeserializeObject<TransactionVM>(resultado);

            var resultados = await Requests.SendURIAsync(string.Format("/api/Agreements/AgreementByAccount/{0}", m.payment.AgreementId), HttpMethod.Get, Variables.LoginModel.Token);
            Agreement ms = JsonConvert.DeserializeObject<Agreement>(resultados);

            string seriefolio = m.transaction.transactionFolios.FirstOrDefault().folio.ToString();
            string serie = string.Empty;
            string folio = string.Empty;
            string uuids = uuid;

            serie = seriefolio.Substring(0, 1);
            folio = seriefolio.Replace(serie, "");

            string fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><cfdi:Comprobante xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" ";
            xml = xml + "Version=\"3.3\" Fecha=\"" + fecha + "\" FormaPago=\"" + m.transaction.payMethod.code + "\" SubTotal=\"" + m.transaction.amount + "\" Moneda=\"MXN\" Total=\"" + m.transaction.total + "\" TipoDeComprobante=\"I\" MetodoPago=\"PUE\" LugarExpedicion=\"72700\" Serie=\"" + serie + "\" Folio=\"" + folio + "\" xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\">";

            if (Variables.Configuration.CFDITest == "Verdadero")
            {
                xml = xml + "<cfdi:Emisor Rfc=\"TEST010203001\" Nombre=\"Una razón rh de cv\" RegimenFiscal=\"605\" />";
                xml = xml + "<cfdi:Receptor Rfc=\"TEST010203001\" Nombre=\"Pepe SA DE CV\" UsoCFDI=\"P01\"/>";
            }
            else
            {
                xml = xml + "<cfdi:Emisor Rfc=\"" + Variables.Configuration.RFC + "\" Nombre=\"" + Variables.Configuration.LegendRegime + "\" RegimenFiscal=\"603\" />";
                xml = xml + "<cfdi:Receptor Rfc=\"" + ms.Clients.FirstOrDefault().rfc + "\" Nombre=\"" + ms.Clients.FirstOrDefault().name + " " + ms.Clients.FirstOrDefault().lastName + " " + ms.Clients.FirstOrDefault().secondLastName + " \" UsoCFDI=\"P01\"/>";
            }

            xml = xml + "<cfdi:Conceptos>";

            foreach (var pay in m.payment.PaymentDetails)
            {
                xml = xml + "<cfdi:Concepto ClaveProdServ=\"" + pay.AccountNumber + "\" Cantidad=\"1\" ClaveUnidad=\"" + pay.UnitMeasurement + "\" Unidad=\"Unidad de servicio\"  Descripcion=\"" + pay.Description + "\" ValorUnitario=\"" + pay.Amount + "\" Importe=\"" + pay.Amount + "\">";

                if (pay.HaveTax == true)
                {
                    xml = xml + "<cfdi:Impuestos><cfdi:Traslados><cfdi:Traslado Base=\"" + pay.Amount + "\" Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"0.160000\" Importe=\"" + pay.Tax + "\" /></cfdi:Traslados></cfdi:Impuestos>";
                    xml = xml + "</cfdi:Concepto>";
                    contadordeiva = contadordeiva + 1;
                }
                else
                {
                    xml = xml + "</cfdi:Concepto>";
                }
            }

            foreach (var pays in m.payment.PaymentDetails)
            {
                if (pays.HaveTax == true)
                {
                    if (check == false)
                    {
                        xml = xml + "</cfdi:Conceptos><cfdi:Impuestos TotalImpuestosTrasladados=\"" + m.transaction.tax + "\"><cfdi:Traslados>";
                        xml = xml + "<cfdi:Traslado  Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"0.160000\" Importe=\"" + m.transaction.tax + "\" />";
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
            
            //WsIntegral33Pruebas.WsCFDI33Client n = new WsIntegral33Pruebas.WsCFDI33Client();

            //Se instancia el WS de Timbrado.
            WSCFDI33.WSCFDI33Client ServicioTimbrado = new WSCFDI33.WSCFDI33Client();

            //Se instancia la Respuesta del WS de Timbrado.
            WSCFDI33.RespuestaTFD33 RespuestaTimbrado = new WSCFDI33.RespuestaTFD33();

            if (status == "ET002")
            {
                //try
                //{
                //    String log_txt = "";
                //    if (Variables.Configuration.CFDITest == "Verdadero")
                //    {
                //        log_txt = "C:/CFDI/TES030201001_b64.pem";
                //    }
                //    else
                //    {
                //        log_txt = "C:/CFDI/" + Variables.Configuration.CFDIKeyCancel + "";
                //    }


                //    var pkcs = System.IO.File.ReadAllText(log_txt);
                //    Thread.Sleep(3000);


                //    WsIntegral33Pruebas.RespuestaCancelacionV2 detalle = new WsIntegral33Pruebas.RespuestaCancelacionV2();
                //    if (Variables.Configuration.CFDITest == "Verdadero")
                //    {
                //        detalle = n.CancelarCFDIConValidacion("CFDI010233001", "Pruebas1a$", "TES030201001", "267138D4-7E57-7E57-7E57-AD01F16DAC82", pkcs, "12345678a");

                //    }
                //    else
                //    {
                //        detalle = n.CancelarCFDIConValidacion(Variables.Configuration.CFDIUser, Variables.Configuration.CFDIPassword, ms.Clients.FirstOrDefault().rfc, uuids, pkcs, Variables.Configuration.CFDICertificado);

                //    }


                //    if (detalle.OperacionExitosa)
                //    {

                //        //ET001
                //        //ET002

                //        Model.TaxReceipt xMLS = new Model.TaxReceipt();
                //        try
                //        {
                //            xMLS.RFC = ms.Clients.FirstOrDefault(x => x.typeUser == "CLI01").rfc;
                //        }
                //        catch (Exception)
                //        {

                //            xMLS.RFC = "XEXX010101000";

                //        }

                //        xMLS.Status = status;
                //        xMLS.PaymentId = m.payment.Id;
                //        xMLS.TaxReceiptDate = DateTime.Now;
                //        xMLS.Type = "CAT02";
                //        xMLS.Xml = detalle.XMLAcuse;
                //        xMLS.FielXML = uuids;
                //        xMLS.UserId = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().UserId;
                //        HttpContent content;
                //        json = JsonConvert.SerializeObject(xMLS);
                //        content = new StringContent(json, Encoding.UTF8, "application/json");
                //        var jsonResponse = await Requests.SendURIAsync("/api/TaxReceipt/", HttpMethod.Post, Variables.LoginModel.Token, content);
                //        m.payment.HaveTaxReceipt = true;
                //        content = new StringContent(JsonConvert.SerializeObject(m.payment), Encoding.UTF8, "application/json");
                //        var updatePayment = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", m.payment.Id), HttpMethod.Put, Variables.LoginModel.Token, content);
                //        respuesta = rutas;
                //        return respuesta;

                //    }
                //    else
                //    {

                //        respuesta = "error/" + detalle.MensajeError + " " + detalle.MensajeErrorDetallado;
                //        return respuesta;

                //    }
                //}
                //catch (Exception)
                //{

                //    respuesta = "error/La Facturacion se encuentra fuera de linea";
                //    return respuesta;
                //}
                return "";
            }
            else
            {
                try
                {
                    Thread.Sleep(3000);                    
                    //WsIntegral33Pruebas.Respuesta respuestas = new WsIntegral33Pruebas.Respuesta();

                    if (Variables.Configuration.CFDITest == "Verdadero")
                    {
                        ////respuestas = n.Sellar("CFDI010233001", "Pruebas1a$", xmlcreado, "", false, 1, "", true);                                                
                        RespuestaTimbrado = ServicioTimbrado.TimbrarCFDI("DEMO010233001", "Pruebas1a$", xmlcreado, "0001");
                    }
                    //else
                    //{
                    //    respuestas = n.Sellar(Variables.Configuration.CFDIUser, Variables.Configuration.CFDIPassword, xmlcreado, "", false, 1, "", true);
                    //}
                    
                    if (RespuestaTimbrado.OperacionExitosa == true)
                    {                     
                        Model.TaxReceipt xMLS = new Model.TaxReceipt();
                        try
                        {
                            xMLS.RFC = ms.Clients.FirstOrDefault(x => x.typeUser == "CLI01").rfc;
                        }
                        catch (Exception)
                        {
                            xMLS.RFC = "XEXX010101000";
                        }

                        xMLS.Status = status;
                        xMLS.PaymentId = m.payment.Id;
                        xMLS.TaxReceiptDate = DateTime.Now;
                        xMLS.Type = "CAT01";
                        xMLS.Xml = RespuestaTimbrado.XMLResultado;
                        xMLS.FielXML = RespuestaTimbrado.Timbre.UUID;
                        xMLS.UserId = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().UserId;
                        HttpContent content;
                        json = JsonConvert.SerializeObject(xMLS);

                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        var jsonResponse = await Requests.SendURIAsync("/api/TaxReceipt/", HttpMethod.Post, Variables.LoginModel.Token, content);

                        m.payment.HaveTaxReceipt = true;
                        content = new StringContent(JsonConvert.SerializeObject(m.payment), Encoding.UTF8, "application/json");

                        var updatePayment = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", m.payment.Id), HttpMethod.Put, Variables.LoginModel.Token, content);
                        String log_txts = AppDomain.CurrentDomain.BaseDirectory;
                        if (RespuestaTimbrado.PDFResultado != null)
                        {
                            System.IO.File.WriteAllText(log_txts + @"\PDF.pdf", RespuestaTimbrado.PDFResultado);
                            //System.IO.File.WriteAllBytes(log_txts + @"\PDF.pdf", RespuestaTimbrado.PDFResultado );
                            rutas = log_txts + @"\PDF.pdf";
                        }

                        respuesta = rutas;
                        return respuesta;

                    }
                    else
                    {

                        respuesta = "error/" + RespuestaTimbrado.MensajeError + " // " + RespuestaTimbrado.MensajeErrorDetallado;
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
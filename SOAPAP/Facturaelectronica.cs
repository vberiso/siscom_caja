using Newtonsoft.Json;
using SOAPAP.Facturado;
using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

using Facturama;
using Facturama.Models;
using Facturama.Models.Request;
using Facturama.Services;

namespace SOAPAP
{
    class Facturaelectronica
    {
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        querys q = new querys();
        FacturamaApiMultiemisor facturama;
        public string msgObservacionFactura { get; set; }
        public string msgUsos { get; set; }

        public Facturaelectronica()
        {
            Requests = new RequestsAPI(UrlBase);
            facturama = new FacturamaApiMultiemisor("gfdsystems", "gfds1st95");
            //facturama = new FacturamaApiMultiemisor("pruebas", "pruebas2011");
        }
        //Metodo del Vic (con calmita...)
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

        // Con Factura inteligente
        public async Task<string> facturarPago(string idtraccaction, int idDebt, string status, string uuid)
        {
            try
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

                //Generacion de sello fiscal
                string origenKEY = "";
                string origenCER = "";
                string origenPass = "";
                if (Variables.Configuration.IsMunicipal)
                {
                    origenKEY = "C:\\Pruebas\\FIELayunt\\CSD_Cuautlancingo_MCP850101944_20180501_125624 (1).key";
                    origenCER = "C:\\Pruebas\\FIELayunt\\00001000000410637078.cer";
                    origenPass = "Cuau1312";
                }
                else
                {
                    origenKEY = "C:\\Pruebas\\FIEL\\CSD_SISTEMA_OPERADOR_DE_LOS_SERVICIOS_DE_AGUA_POTABLE_Y_ALCANTARILLA_SOS970808SM7_20190629_115551.key";
                    origenCER = "C:\\Pruebas\\FIEL\\00001000000412882009.cer";
                    origenPass = "htepox1978";
                }

                //Leectura de Cer
                FIELCertificadeReader fcr = new FIELCertificadeReader();
                Dictionary<string, string> dataCer = fcr.GetCertificateData(origenCER);

                armaXmlParaCadenaOriginal(m , ms, idDebt, dataCer);
                string CadenaOriginal = GeneradorCadenas();

                opensslkey libssl = new opensslkey();
                
                string SignedString = libssl.SignString(origenKEY, origenPass, CadenaOriginal);
                string Certificate = "";
                string CertificateNumber = "";
                libssl.CertificateData(origenCER, out Certificate, out CertificateNumber);
                
                string fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                               
                string xmlcreado = await armaXML(m, ms, idDebt, dataCer, origenKEY, origenCER, origenPass, CadenaOriginal);
                string xmltimbre = string.Empty;

                string stringXML = null;
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlcreado);
                    doc.Save(@"c:\Pruebas\XmlArmado.xml");

                    //doc.Load("C:\\Pruebas\\XmlMau.xml");
                    stringXML = doc.OuterXml;
                }
                catch (Exception ex)
                {

                }
                                             
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
                    if (Variables.Configuration.CFDITest == "Verdadero")
                    {
                        RespuestaTimbrado = ServicioTimbrado.TimbrarCFDI("GFD190307TYA", "Re2PgVBNm&", stringXML, "0001");
                    }                    

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



            }
            catch (Exception ex)
            {
                return "error/La Facturacion se encuentra fuera de linea";
            }
        }
        
        private string GeneradorCadenas()
        {
            //Cargar el XML
            StreamReader reader = new StreamReader(@"C:/Pruebas/XmlPrevio.xml");
            XPathDocument myXPathDoc = new XPathDocument(reader);

            //Cargando el XSLT
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            myXslTrans.Load(@"../../Conversion/cadenaoriginal_3_3/cadenaoriginal_3_3.xslt");

            StringWriter str = new StringWriter();
            XmlTextWriter myWriter = new XmlTextWriter(str);

            //Aplicando transformacion
            myXslTrans.Transform(myXPathDoc, null, myWriter);

            //Resultado
            string result = str.ToString();

            return result;
        }

        private async void armaXmlParaCadenaOriginal(TransactionVM TrVM, Agreement Ag, int idDebt, Dictionary<string, string> dataCer)
        {            
            string seriefolio = TrVM.transaction.transactionFolios.FirstOrDefault().folio.ToString();
            string serie = seriefolio.Substring(0, 1);
            string folio = seriefolio.Replace(serie, "");
                        
            //Se Obtienen los descuentos
            decimal Descuento = 0;
            if (TrVM.payment.OrderSaleId == 0) //Servicio
            {
                Descuento = TrVM.payment.PaymentDetails.Sum(x => x.Debt.DebtDiscounts.Where(y => y.CodeConcept == x.CodeConcept).Select(yy => yy.DiscountAmount).FirstOrDefault());
            }
            else                    //Producto
            {
                Descuento = TrVM.orderSale.OrderSaleDiscounts.Sum(x => x.DiscountAmount);
            }

            //se obtiene el metodo de pago
            string MetodoPago = "PUE"; //PUE(Pago en una sola exhibición), PPD(Pago en parcialidades o diferido)
            if (TrVM.payment.OrderSaleId == 0) //Servicio
            {
                decimal tmpTotalPagado = TrVM.payment.PaymentDetails.Sum(x => x.Amount);
                decimal tmpTotalDebt = TrVM.payment.PaymentDetails.First().Debt.Amount;
                MetodoPago = (tmpTotalPagado == tmpTotalDebt ? "PUE" : "PPD");
            }
            else                    //Producto
            {
                MetodoPago = "PUE";
            }

            string fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                        
            //COMIENZA ARMADO DE XML
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

            //NODO: Comprobante
            xml = xml + "<cfdi:Comprobante xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" ";
            xml = xml + "Version=\"3.3\" Serie=\"" + serie + "\" Folio=\"" + folio + "\" " + "Fecha =\"" + fecha + "\" ";           
            xml = xml + "FormaPago=\"" + TrVM.transaction.payMethod.code + "\" ";
            xml = xml + "SubTotal=\"" + TrVM.transaction.amount + "\" Descuento=\"" + Descuento + "\" Moneda=\"MXN\" TipoCambio=\"1.0\" ";
            xml = xml + "Total=\"" + TrVM.transaction.total + "\" TipoDeComprobante=\"I\" ";
            xml = xml + "MetodoPago=\"" + MetodoPago + "\" ";
            xml = xml + "LugarExpedicion=\"72700\" xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\">";

            //Obtengo el primer pago relacionado si lo hay, para relacionarlo a esta pago.
            if (MetodoPago == "PPD")
            {
                var tmplstPayments = await Requests.SendURIAsync(string.Format("api/Payments/fromDebt/{0}", idDebt), HttpMethod.Get, Variables.LoginModel.Token);
                List<Model.Payment> lstPayments = JsonConvert.DeserializeObject<List<Model.Payment>>(tmplstPayments);

                //remuevo el pago actual.
                lstPayments.Remove(lstPayments.FirstOrDefault(x => x.TransactionFolio == TrVM.transaction.folio));

                //Busco el primer pago con uuid para relacionarlo al actual
                string primerUUID = "";
                foreach (var item in lstPayments)
                {
                    if (item.HaveTaxReceipt)
                    {
                        var tmpUUID = await Requests.SendURIAsync(string.Format("api/TaxReceipt/UUIDFromTaxReceipt/{0}", item.Id), HttpMethod.Get, Variables.LoginModel.Token);
                        primerUUID = JsonConvert.DeserializeObject<string>(tmplstPayments);
                        break;
                    }
                }

                //NODO: CfdiRelacionados (Para pagos parciales)
                xml = xml + "<cfdi:CfdiRelacionados TipoRelacion=\"08\">";
                xml = xml + "<cfdi:CfdiRelacionado UUID = \"" + primerUUID + "\" />";
                xml = xml + "</cfdi:CfdiRelacionados>";
            }
                       
            //NODO: Emisor   
            xml = xml + "<cfdi:Emisor Rfc=\"" + dataCer["Subject"].Split(',')[2].Split('=')[1].Substring(0, 12) + "\" Nombre=\"" + dataCer["Subject"].Split(',')[4].Split('=')[1] + "\" RegimenFiscal=\"601\" />";


            //NODO: Receptor
            if (Ag.Clients != null)
                xml = xml + "<cfdi:Receptor Rfc=\"" + Ag.Clients.FirstOrDefault().rfc + "\" Nombre=\"" + Ag.Clients.FirstOrDefault().name + " " + Ag.Clients.FirstOrDefault().lastName + " " + Ag.Clients.FirstOrDefault().secondLastName + " \" UsoCFDI=\"P01\"/>";
            else
                xml = xml + "<cfdi:Receptor Rfc=\"TEST010203001\" Nombre=\"Pepe SA DE CV\" UsoCFDI=\"P01\"/>";
            

            //NODO: Conceptos
            xml = xml + "<cfdi:Conceptos>";

            foreach (var pay in TrVM.payment.PaymentDetails)
            {
                xml = xml + "<cfdi:Concepto ClaveProdServ=\"" + pay.AccountNumber + "\" Cantidad=\"1\" ClaveUnidad=\"" + pay.UnitMeasurement + "\" Unidad=\"Unidad de servicio\"  Descripcion=\"" + pay.Description + "\" ValorUnitario=\"" + pay.Amount + "\" Importe=\"" + pay.Amount + "\">";

                if (pay.HaveTax == true)
                {
                    xml = xml + "<cfdi:Impuestos><cfdi:Traslados><cfdi:Traslado Base=\"" + pay.Amount + "\" Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"0.160000\" Importe=\"" + pay.Tax + "\" /></cfdi:Traslados></cfdi:Impuestos>";
                    xml = xml + "</cfdi:Concepto>";                    
                }
                else
                {
                    xml = xml + "</cfdi:Concepto>";
                }
            }
            xml = xml + "</cfdi:Conceptos>";

            //NODO: Impuestos
            xml = xml + "<cfdi:Impuestos TotalImpuestosTrasladados =\"" + TrVM.transaction.tax + "\" />";

            xml = xml + "</cfdi:Comprobante>";

            //FIN DEL ARMADO DEL XML
                        
            string xmltimbre = string.Empty;
            string stringXML = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                doc.Save(@"c:\Pruebas\XmlPrevio.xml");
                //doc.Load("C:\\Pruebas\\XmlMau.xml");
                stringXML = doc.OuterXml;
            }
            catch (Exception ex)
            {

            }

        }

        private async Task<string> armaXML(TransactionVM TrVM, Agreement Ag, int idDebt, Dictionary<string, string> dataCer, string origenKey, string OrigenCER, string origenPass, string CadenaOriginal)
        {
            string seriefolio = TrVM.transaction.transactionFolios.FirstOrDefault().folio.ToString();
            string serie = seriefolio.Substring(0, 1);
            string folio = seriefolio.Replace(serie, "");

            //Se Obtienen los descuentos
            decimal Descuento = 0;
            if (TrVM.payment.OrderSaleId == 0) //Servicio
            {
                Descuento = TrVM.payment.PaymentDetails.Sum(x => x.Debt.DebtDiscounts.Where(y => y.CodeConcept == x.CodeConcept).Select(yy => yy.DiscountAmount).FirstOrDefault());
            }
            else                    //Producto
            {
                Descuento = TrVM.orderSale.OrderSaleDiscounts.Sum(x => x.DiscountAmount);
            }

            //se obtiene el metodo de pago
            string MetodoPago = "PUE"; //PUE(Pago en una sola exhibición), PPD(Pago en parcialidades o diferido)
            if (TrVM.payment.OrderSaleId == 0) //Servicio
            {
                decimal tmpTotalPagado = TrVM.payment.PaymentDetails.Sum(x => x.Amount);
                decimal tmpTotalDebt = TrVM.payment.PaymentDetails.First().Debt.Amount;
                MetodoPago = (tmpTotalPagado == tmpTotalDebt ? "PUE" : "PPD");
            }
            else                    //Producto
            {
                MetodoPago = "PUE";
            }

            opensslkey libssl = new opensslkey();
            string SignedString = libssl.SignString(origenKey, origenPass, CadenaOriginal);
            string Certificate = "";
            string CertificateNumber = "";
            libssl.CertificateData(OrigenCER, out Certificate, out CertificateNumber);

            string fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            //COMIENZA ARMADO DE XML
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

            //NODO: Comprobante
            xml = xml + "<cfdi:Comprobante xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" ";
            xml = xml + "Version=\"3.3\" Serie=\"" + serie + "\" Folio=\"" + folio + "\" " + "Fecha =\"" + fecha + "\" ";
            xml = xml + "Sello=\"" + SignedString + "\" ";
            xml = xml + "FormaPago=\"" + TrVM.transaction.payMethod.code + "\" NoCertificado=\"" + CertificateNumber + "\" " + " Certificado=\"" + Certificate + "\" ";
            xml = xml + "SubTotal=\"" + TrVM.transaction.amount + "\" Descuento=\"" + Descuento + "\" Moneda=\"MXN\" TipoCambio=\"1.0\" ";
            xml = xml + "Total=\"" + TrVM.transaction.total + "\" TipoDeComprobante=\"I\" ";
            xml = xml + "MetodoPago=\"" + MetodoPago + "\" ";
            xml = xml + "LugarExpedicion=\"72700\" xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\">";

            //Obtengo el primer pago relacionado si lo hay, para relacionarlo a esta pago.
            if (MetodoPago == "PPD")
            {
                var tmplstPayments = await Requests.SendURIAsync(string.Format("api/Payments/fromDebt/{0}", idDebt), HttpMethod.Get, Variables.LoginModel.Token);
                List<Model.Payment> lstPayments = JsonConvert.DeserializeObject<List<Model.Payment>>(tmplstPayments);

                //remuevo el pago actual.
                lstPayments.Remove(lstPayments.FirstOrDefault(x => x.TransactionFolio == TrVM.transaction.folio));

                //Busco el primer pago con uuid para relacionarlo al actual
                string primerUUID = "";
                foreach (var item in lstPayments)
                {
                    if (item.HaveTaxReceipt)
                    {
                        var tmpUUID = await Requests.SendURIAsync(string.Format("api/TaxReceipt/UUIDFromTaxReceipt/{0}", item.Id), HttpMethod.Get, Variables.LoginModel.Token);
                        primerUUID = JsonConvert.DeserializeObject<string>(tmplstPayments);
                        break;
                    }
                }

                //NODO: CfdiRelacionados (Para pagos parciales)
                xml = xml + "<cfdi:CfdiRelacionados TipoRelacion=\"08\">";
                xml = xml + "<cfdi:CfdiRelacionado UUID = \"" + primerUUID + "\" />";
                xml = xml + "</cfdi:CfdiRelacionados>";
            }

            //NODO: Emisor   
            xml = xml + "<cfdi:Emisor Rfc=\"" + dataCer["Subject"].Split(',')[2].Split('=')[1].Substring(0, 12) + "\" Nombre=\"" + dataCer["Subject"].Split(',')[4].Split('=')[1] + "\" RegimenFiscal=\"601\" />";
            
            //NODO: Receptor
            if (Ag.Clients != null)
                xml = xml + "<cfdi:Receptor Rfc=\"" + Ag.Clients.FirstOrDefault().rfc + "\" Nombre=\"" + Ag.Clients.FirstOrDefault().name + " " + Ag.Clients.FirstOrDefault().lastName + " " + Ag.Clients.FirstOrDefault().secondLastName + " \" UsoCFDI=\"P01\"/>";
            else
                xml = xml + "<cfdi:Receptor Rfc=\"TEST010203001\" Nombre=\"Pepe SA DE CV\" UsoCFDI=\"P01\"/>";
            
            //NODO: Conceptos
            xml = xml + "<cfdi:Conceptos>";

            foreach (var pay in TrVM.payment.PaymentDetails)
            {
                xml = xml + "<cfdi:Concepto ClaveProdServ=\"" + pay.AccountNumber + "\" Cantidad=\"1\" ClaveUnidad=\"" + pay.UnitMeasurement + "\" Unidad=\"Unidad de servicio\"  Descripcion=\"" + pay.Description + "\" ValorUnitario=\"" + pay.Amount + "\" Importe=\"" + pay.Amount + "\">";

                if (pay.HaveTax == true)
                {
                    xml = xml + "<cfdi:Impuestos><cfdi:Traslados><cfdi:Traslado Base=\"" + pay.Amount + "\" Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"0.160000\" Importe=\"" + pay.Tax + "\" /></cfdi:Traslados></cfdi:Impuestos>";
                    xml = xml + "</cfdi:Concepto>";                    
                }
                else
                {
                    xml = xml + "</cfdi:Concepto>";
                }
            }
            xml = xml + "</cfdi:Conceptos>";

            //NODO: Impuestos
            xml = xml + "<cfdi:Impuestos TotalImpuestosTrasladados =\"" + TrVM.transaction.tax + "\" />";

            xml = xml + "</cfdi:Comprobante>";

            //FIN DEL ARMADO DEL XML
            return xml;
        }


        //con Facturama PRODUCTIVO
        public async Task<string> generaFactura(string idTransaction, string status)
        {
            try
            {
                string respuesta = string.Empty;
                string rutas = string.Empty;
                string json = string.Empty;
                
                var resultado = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", idTransaction), HttpMethod.Get, Variables.LoginModel.Token);
                TransactionVM TraVM = JsonConvert.DeserializeObject<TransactionVM>(resultado);

                var resultados = await Requests.SendURIAsync(string.Format("/api/Agreements/{0}", TraVM.payment.AgreementId), HttpMethod.Get, Variables.LoginModel.Token);
                Agreement ms = JsonConvert.DeserializeObject<Agreement>(resultados);
                
                string registro = RegitraEmisor();


                string seriefolio = TraVM.transaction.transactionFolios.FirstOrDefault().folio.ToString();
                string serie = seriefolio.Substring(0, 1);
                string folio = seriefolio.Replace(serie, "");
                
                //Se Obtienen los descuentos
                decimal Descuento = 0;
                string CondicionPago = "";
                if (TraVM.payment.OrderSaleId == 0) //Servicio
                {
                    Descuento = TraVM.payment.PaymentDetails.Sum(x => x.Debt.DebtDiscounts.Where(y => y.CodeConcept == x.CodeConcept).Select(yy => yy.DiscountAmount).FirstOrDefault());
                    //Anexo el periodo de pago (Services)
                    CondicionPago = "Periodo de: " + TraVM.payment.PaymentDetails.Min(p => p.Debt.FromDate).ToString("yyyy-MM-dd") + " hasta: " + TraVM.payment.PaymentDetails.Max(p => p.Debt.UntilDate).ToString("yyyy-MM-dd");
                }
                else                    //Producto
                {
                    Descuento = TraVM.orderSale.OrderSaleDiscounts.Sum(x => x.DiscountAmount);
                    //Anexo el año y periodo (Orders)
                    CondicionPago = "Año :" + TraVM.orderSale.Year + " Perido: " + TraVM.orderSale.Year;
                }

                //se obtiene el metodo de pago
                string MetodoPago = "PUE"; //PUE(Pago en una sola exhibición), PPD(Pago en parcialidades o diferido)
                //if (TraVM.payment.OrderSaleId == 0) //Servicio
                //{
                //    decimal tmpTotalPagado = TraVM.payment.PaymentDetails.Sum(x => x.Amount);
                //    decimal tmpTotalDebt = TraVM.payment.PaymentDetails.First().Debt.Amount;
                //    MetodoPago = (tmpTotalPagado == tmpTotalDebt ? "PUE" : "PPD");
                //}
                //else                    //Producto
                //{
                //    MetodoPago = "PUE";
                //}
                
                string fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                
                var cfdi = new CfdiMulti
                {
                    Serie = serie,
                    Folio = folio,
                    Date = DateTime.Now,
                    PaymentForm = TraVM.transaction.payMethod.code,
                    Currency = "MXN",
                    CfdiType = CfdiType.Ingreso,
                    PaymentMethod = MetodoPago,
                    ExpeditionPlace = "72700",
                    PaymentConditions = CondicionPago
                };


                //Anexo el periodo de pago (Services) o La fecha de espedicion (Orders)
                
                               
                ////Obtengo el primer pago relacionado con factura si lo hay, para relacionarlo a esta pago.
                //if (MetodoPago == "PPD")
                //{                                                                                                        //Recordar que se elimino el compo idDebt
                //    var tmplstPayments = await Requests.SendURIAsync(string.Format("api/Payments/fromDebt/{0}", idDebt), HttpMethod.Get, Variables.LoginModel.Token);
                //    List<Model.Payment> lstPayments = JsonConvert.DeserializeObject<List<Model.Payment>>(tmplstPayments);

                //    //remuevo el pago actual.
                //    lstPayments.Remove(lstPayments.FirstOrDefault(x => x.TransactionFolio == TraVM.transaction.folio));

                //    if(lstPayments.Count > 0)
                //    {
                //        //Busco el primer pago con uuid para relacionarlo al actual
                //        string primerUUID = "";
                //        foreach (var item in lstPayments)
                //        {
                //            if (item.HaveTaxReceipt)
                //            {
                //                var tmpUUID = await Requests.SendURIAsync(string.Format("api/TaxReceipt/UUIDFromTaxReceipt/{0}", item.Id), HttpMethod.Get, Variables.LoginModel.Token);
                //                primerUUID = JsonConvert.DeserializeObject<string>(tmplstPayments);
                //                break;
                //            }
                //        }

                //        List<CfdiRelation> lstRels = new List<CfdiRelation>();
                //        CfdiRelation cfdirelacion = new CfdiRelation();
                //        cfdirelacion.Uuid = primerUUID;
                //        lstRels.Add(cfdirelacion);

                //        CfdiRelations cfdirelations = new CfdiRelations();
                //        cfdirelations.Type = "08";
                //        cfdirelations.Cfdis = lstRels;

                //        cfdi.Relations = cfdirelations;
                //    }                    
                //}
                               
                //////NODO: Emisor
                Issuer issuer;
                if (Variables.Configuration.IsMunicipal)
                {                   
                    issuer = new Issuer
                    {
                        FiscalRegime = "601",
                        Name = "MUNICIPIO DE CUAUTLANCINGO PUEBLA",
                        Rfc = "MCP850101944"
                    };
                }
                else
                {                  
                    issuer = new Issuer
                    {
                        FiscalRegime = "601",
                        Name = "SISTEMA OPERADOR DE LOS SERVICIOS DE AGUA POTABLE Y ALCANTARILLADO DEL MUNICIPIO DE CUAUTLANCINGO",
                        Rfc = "SOS970808SM7"
                    };
                }
                cfdi.Issuer = issuer;


                //NODO: Receptor
                Client client = ms.Clients.FirstOrDefault(x => x.typeUser == "CLI01");
                Receiver receptor;
                if (client != null)
                {                    
                    receptor = new Receiver
                    {
                        Rfc = client.rfc,
                        Name = client.name + " " + client.lastName + " " + client.secondLastName,
                        CfdiUse = "P01"
                    };
                }
                else
                {                    
                    receptor = new Receiver
                    {
                        Rfc = "XAXX010101000",
                        Name = "Público en General",
                        CfdiUse = "P01"
                    };
                }
                cfdi.Receiver = receptor;


                //NODO: Conceptos
                //xml = xml + "<cfdi:Conceptos>";
                string TipoFactura = "";
                List<Item> lstItems = new List<Item>();
                if (TraVM.payment.OrderSaleId == 0) //Servicio
                {
                    TipoFactura = "CAT01";
                    foreach (var pay in TraVM.payment.PaymentDetails)
                    {
                        Item item = new Item()
                        {
                            ProductCode = TraVM.ClavesProdServ.Where(x => x.CodeConcep == pay.CodeConcept && x.Tipo == pay.Type).Select(y => y.ClaveProdServ).FirstOrDefault(),
                            IdentificationNumber = "S" + pay.CodeConcept,                            
                            UnitCode = pay.UnitMeasurement,
                            Unit = "NO APLICA",
                            Description = pay.Description + " Periodo de:" + pay.Debt.FromDate.ToString("yyyy-MM-dd") + " hasta: " + pay.Debt.UntilDate.ToString("yyyy-MM-dd"),
                            UnitPrice = pay.Debt.DebtDetails.Where(x => x.CodeConcept == pay.CodeConcept).Select(y => y.Amount).FirstOrDefault(),
                            Quantity = pay.Debt.DebtDetails.Where(x => x.CodeConcept == pay.CodeConcept).Select(y => y.quantity).FirstOrDefault(),
                            Subtotal = pay.Debt.DebtDetails.Where(x => x.CodeConcept == pay.CodeConcept).Select(y => y.OnAccount).FirstOrDefault(),
                            Discount = pay.Debt.DebtDiscounts.Where(x => x.CodeConcept == pay.CodeConcept).Select(y => y.DiscountAmount).FirstOrDefault(),
                            Total = pay.Amount + pay.Tax                            
                        };
                        if (pay.HaveTax == true)
                        {
                            List<Tax> lstTaxs = new List<Tax>() { 
                                new Tax()
                                {
                                    Base = pay.Amount,
                                    Rate = decimal.Parse(TraVM.payment.PercentageTax)/100,
                                    Name = "IVA",
                                    Total = pay.Tax,
                                    IsRetention = false
                                }
                            };
                            item.Taxes = lstTaxs;                                             
                        }
                        lstItems.Add(item);
                    }
                }
                else                    //Producto
                {
                    TipoFactura = "CAT02";
                    foreach (var Or in TraVM.orderSale.OrderSaleDetails)
                    {
                        Item item = new Item()
                        {
                            ProductCode = TraVM.ClavesProdServ.Where(x => x.CodeConcep == Or.CodeConcept).FirstOrDefault().ClaveProdServ,
                            IdentificationNumber = "P" + Or.CodeConcept,
                            UnitCode = TraVM.payment.PaymentDetails.Where(x => x.CodeConcept == Or.CodeConcept && x.Amount == Or.Amount).FirstOrDefault().UnitMeasurement,
                            Unit = "NO APLICA",
                            Description = Or.Description,
                            UnitPrice = Or.UnitPrice,
                            Quantity = Or.Quantity,
                            Subtotal = Or.OnAccount,
                            Discount = TraVM.orderSale.OrderSaleDiscounts.Where(x => x.CodeConcept == Or.CodeConcept && x.OriginalAmount == Or.Amount).Select(y => y.DiscountAmount).FirstOrDefault() , //Or.Debt.DebtDiscounts.Where(x => x.CodeConcept == Or.CodeConcept).Select(y => y.DiscountAmount).FirstOrDefault(),
                            Total = Or.Amount + TraVM.payment.PaymentDetails.Where(x => x.CodeConcept == Or.CodeConcept && x.Amount == Or.Amount).FirstOrDefault().Tax
                        };

                        if (Or.HaveTax == true)
                        {
                            List<Tax> lstTaxs = new List<Tax>() {
                                new Tax()
                                {
                                    Base = Or.Amount,
                                    Rate = decimal.Parse(TraVM.payment.PercentageTax)/100,
                                    Name = "IVA",
                                    Total = TraVM.payment.PaymentDetails.Where(x => x.CodeConcept == Or.CodeConcept && x.Amount == Or.Amount).FirstOrDefault().Tax,
                                    IsRetention = false
                                }
                            };
                            item.Taxes = lstTaxs;
                        }
                        lstItems.Add(item);
                    }
                }
                cfdi.Items = lstItems;

                //FIN DEL ARMADO DEL XML

                string path = GeneraCarpetaDescagasXML();
                string nombreXML = string.Format("\\{0}_{1}.xml", issuer.Rfc, seriefolio);

                Facturama.Models.Response.Cfdi cfdiFacturama = TimbrarAnteFacturama(cfdi, path + nombreXML);
                string XML = LeerXML(path + nombreXML);
                string resGuardado = await guardarXMLenBD(XML, cfdiFacturama.Complement.TaxStamp.Uuid, receptor.Rfc, TipoFactura, status, TraVM.payment.Id, cfdiFacturama.Id);
                string resActPay = await actualizarPaymentConFactura(TraVM.payment);
                                
                return XML;
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }            
        }

        public Facturama.Models.Response.Cfdi TimbrarAnteFacturama(CfdiMulti cfdi, string path)
        {            
            try
            {
                //var cfdi = new CfdiMulti
                //{
                //    Folio = "101",
                //    Serie = "R",
                //    Currency = "MXN",
                //    ExpeditionPlace = "78116",
                //    PaymentConditions = "CREDITO A SIETE DIAS",
                //    CfdiType = CfdiType.Ingreso,
                //    PaymentForm = "03",
                //    PaymentMethod = "PUE",
                //    Issuer = new Issuer
                //    {
                //        FiscalRegime = "601",
                //        Name = "Ayuntamiento",
                //        Rfc = "MCP850101944"
                //    },
                //    Receiver = new Receiver
                //    {
                //        Rfc = "XAXX010101000",
                //        Name = "GFD SYSTEMS",
                //        CfdiUse = "P01"
                //    },
                //    Items = new List<Item>
                //{
                //    new Item
                //    {
                //        ProductCode = "10101504",
                //        IdentificationNumber = "EDL",
                //        Description = "Estudios de viabilidad",
                //        Unit = "NO APLICA",
                //        UnitCode = "MTS",
                //        UnitPrice = 50.00m,
                //        Quantity = 2.00m,
                //        Subtotal = 100.00m,
                //        Taxes = new List<Tax>
                //        {
                //            new Tax
                //            {
                //                Total = 16.00m,
                //                Name = "IVA",
                //                Base = 100.00m,
                //                Rate = 0.160000m,
                //                IsRetention = false
                //            }
                //        },
                //        Total = 116.0m
                //    }
                //}
                //};


                Facturama.Models.Response.Cfdi cfdiCreated = facturama.Cfdis.Create(cfdi);
                               
                //Descarga de XML
                facturama.Cfdis.SaveXml(path, cfdiCreated.Id);

                ////Consultar facturas creadas
                //facturama.Cfdis.List("Expresion en Software");

                ////Cancelación
                //facturama.Cfdis.Remove(cfdiCreated.Id);

                return cfdiCreated;
            }
            catch (Exception ex)
            {
                var text = ex.Message;
                return null;
            }            
        }

        private string RegitraEmisor()
        {
            try
            {
                Csd csdRequest;
                if (Variables.Configuration.IsMunicipal)
                {
                    csdRequest = new Csd
                    {
                        Rfc = "MCP850101944",
                        Certificate = "MIIGQzCCBCugAwIBAgIUMDAwMDEwMDAwMDA0MTA2MzcwNzgwDQYJKoZIhvcNAQELBQAwggGyMTgwNgYDVQQDDC9BLkMuIGRlbCBTZXJ2aWNpbyBkZSBBZG1pbmlzdHJhY2nDs24gVHJpYnV0YXJpYTEvMC0GA1UECgwmU2VydmljaW8gZGUgQWRtaW5pc3RyYWNpw7NuIFRyaWJ1dGFyaWExODA2BgNVBAsML0FkbWluaXN0cmFjacOzbiBkZSBTZWd1cmlkYWQgZGUgbGEgSW5mb3JtYWNpw7NuMR8wHQYJKoZIhvcNAQkBFhBhY29kc0BzYXQuZ29iLm14MSYwJAYDVQQJDB1Bdi4gSGlkYWxnbyA3NywgQ29sLiBHdWVycmVybzEOMAwGA1UEEQwFMDYzMDAxCzAJBgNVBAYTAk1YMRkwFwYDVQQIDBBEaXN0cml0byBGZWRlcmFsMRQwEgYDVQQHDAtDdWF1aHTDqW1vYzEVMBMGA1UELRMMU0FUOTcwNzAxTk4zMV0wWwYJKoZIhvcNAQkCDE5SZXNwb25zYWJsZTogQWRtaW5pc3RyYWNpw7NuIENlbnRyYWwgZGUgU2VydmljaW9zIFRyaWJ1dGFyaW9zIGFsIENvbnRyaWJ1eWVudGUwHhcNMTgwNTAxMTgwNDA4WhcNMjIwNTAxMTgwNDA4WjCB4zEqMCgGA1UEAxMhTVVOSUNJUElPIERFIENVQVVUTEFOQ0lOR08gUFVFQkxBMSowKAYDVQQpEyFNVU5JQ0lQSU8gREUgQ1VBVVRMQU5DSU5HTyBQVUVCTEExKjAoBgNVBAoTIU1VTklDSVBJTyBERSBDVUFVVExBTkNJTkdPIFBVRUJMQTElMCMGA1UELRMcTUNQODUwMTAxOTQ0IC8gQ0FURjgwMTAwNVNYMjEeMBwGA1UEBRMVIC8gQ0FURjgwMTAwNUhQTFNMTDA0MRYwFAYDVQQLEw1DdWF1dGxhbmNpbmdvMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqV/TdwxEMxtNp4bQ2DMa4JEarDtPx9rl2BMatlBgrq87oKFghfHsIc+KJANoJELApEo7tSegOmc/u+dIEbT6SsUnx2IQZy7BwbejU6+pWzhhfbNO1BleqLamX+TJ30jrOBg/M1ynWDwNflFtiXUe+69W4aP2tqr7rXeZc6Z/Udkfn2zybXRWgiHCZoZQ7B5JKlqUNvr2vmuNHh6ZeGdqmv/qW52Ll03WVZMgUkZ/OD+2GQB+Bc3ynfpR8HurFYu3Xc7agIYyXymiFz5ZwUnkYyp5m6jAuBAKaQgMoQHCUPkgKZqOn4H/vP2vX6lafjclrbKwqq5UuDtN59IOiueo+wIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEAGa7VBb5fjra/2tdvq3nNTjooo68yQNzk7uljzbvjigDK+GQmHp6XzWEuoIaw2glu7F4r8WddnsBuUl64dUMbiDw6cz9XSt0TbgfIxUdZBT8QWGj6rvqtwOMma0Q+va86QQoXJlRce1/TlnYb/gsG+Ba5Y3Tcf/gbJK+czmnDSmv6VUmvoYTi6mQyG8svrnqaHmRQ+5PpHTm8o7x9apw8c2/YV2aXsmH7ehnXYvOS66eJYKcXtW3rRg/mib+H3cqPzoqToqxTaIRxdDooYcjSaNzhLBTj5k8sC00U+1chByofIzyt3mIq5OQihqf/2JdHDuaOy8mJPPab6t427Vt1fKonQCK68361yg3Jrh04sjB7f1NMRjtDXEVjAkctusna1Oq/zUuDmIcQNtFyMpHLoMbU+YPAl9WmNgDciu6ZnwEdDe6OCu8opAlXFFIJNFb2O9VwxJceepaOTKW+ciTVJhAkjJoNxY6j0lN/q5Fm8tVGjFtZpvpGW9+Ogh0p2eAHJDcBcUihf53hEBVcGreLLSrfq2iaFRRl7EFuXVH46sLSlhbzm8KJmEKszzDo0F/wCSIMDryyd0TmaNwnLM73WgH1S3msH1K4/viRzrKmb5KwKSnzrtTT4vfUFpM3zPHntAyZexxrkAl6I6gWlecGcqpT2mJjpU6hopqk71MVdc4=",
                        PrivateKey = "MIIFDjBABgkqhkiG9w0BBQ0wMzAbBgkqhkiG9w0BBQwwDgQIAgEAAoIBAQACAggAMBQGCCqGSIb3DQMHBAgwggS+AgEAMASCBMjAKMA5RciD3fjDSzGOAurQLiD+uPmCs3a9Z8GyjHfzIvOd7aQ+XBoyUMRj81nCxHK9A1ScbP/5C0igsVnIj+s8e+iirokRQhRZwFlbAypvgXtAvIcTGU2pYr7XoPRigbFdYDGx9J1zfZWuvBLrwpbKNXdbqLshnRQvOlBM8eWkJgheR26Xv+ROdSrsKOY05cda7wdVc+2CR4kr/GnQYYCTooXhvgS7AoJsyuDdLvN57t/NgtOplYktjwO5FTqKqzTc8J0Lb1DcZh1UHsFuGbmHwLdzqE+uw5I6l0q1+h/UJlhTofLlQ7Y9+YgxHSLuxpRAQ7vJSFMTP7nITn+4GtD4rnYBLWt/bP0XXSwYLW7B2ofYNepz5/IDhawshyFNo+cg/aqWUICKF0zhDrQm499VGYSRIHS13pICSg7aSbeQ6XDcXGDU7bIwf0/Bsspnqw+deP8JplI6DPhCXKnOnybCn0qnTag+2Q86Tnskcv5t0x+6NUOuoEB2/TVCVWFYobgRWyRH0Eq/hOm3ywV095qOO74yXcK291QUOEfA0v1OmXzsD4KX47Wr7K4rYAX7FYahE7nHdya0vWvNGTth/XDdopuoLKKY5X+9NkuBt3YYohiF7d6ZsRKwczfQV3MEwhd2Wvm1ADh1SOR2n4BdtBApNoReiMSnZ76vhc2ynuJkX35cA1ofKTsWZLJ4GCpV3MN1vVQFLlpy7S7QGf/ER1t6VfFHafayp4t/UY+K6hxn4Fjfu5eqo3SQQ2o6Vsh+Qqm/6txA5ht1B0peI7vJ6AWGz5L5D6e30nUDg/aQPyCUt0r+4pgRXFUxTRrWpQ6xmnB9M1fcvjzr17CSudTJlJ35b5hGXoIGbd7tWh5kSeoUlpAycu15JIiRIt9LVaHHr49vceSgj71m+O9+k5PCZi1449oVRiXmjzyniHZ2orMdy9eTvUWP/Dobx6QBACC1eSHLag2sDnzeHi676aAOzH8K/34jH7ZyMblDD4TSWh0irPbcF1hIBIFhtJrPlLs34ceZKlPh3cYH5/e5tDSlAcoYq4TJuhn1tZSN4992kJM5y30C1bgYg2C5pZ7XfO+z/uTw6Hl+QhckVSJgKs2KY+U77loqbuP7OIWEzAot/wCUEh66l0/MUPHaCf08GYp1T0iZszTRAtc8+ORqWwnSqKDl09Vb3FszlaYD1fanPARQhTcyXr16ALS/9jkN4pguV+hkq86pPAuJy8tL2491I3jGWI76BAoa5cooOls9XAHPholIwGiChuEoIcAJ3I0SHXTnELI13lu1e7VNUQR4hTWBbh1tc0OnjPfdfUkWtgLQcQ7ONg5EzYLR9b9G6xHrP01QunUiD3Y7ZJDTjFhsuvuWi6RvZ5h/UTdupzH4eWNR+91obsIHS6c3z8uVLXG44uUnl8IXn9c1G8q+LdPXFLSF+Q7+n0yVrGBaSoqDPjPwRa1eWBNc55TCjs9DZLPaB+xdz860NWuFmGNhAzviA8aJhFZ/jQg8Eu2lHaJgcoXluwc3Dm5A2bZG7tpLACXK77ezI1T4ffs0RcKkf/cOHNWcU7jVeM9n7NeeEBvesrPvqsuhJ/xIodFJsG/ZDwkhvseKOX04r3wPmmhIiY18/gxr5h/auEJgfh4=",
                        PrivateKeyPassword = "Cuau1312"
                    };
                }
                else
                {
                    csdRequest = new Csd
                    {
                        Rfc = "SOS970808SM7",
                        Certificate = "MIIGxzCCBK+gAwIBAgIUMDAwMDEwMDAwMDA1MDA0NjU2NTIwDQYJKoZIhvcNAQELBQAwggGEMSAwHgYDVQQDDBdBVVRPUklEQUQgQ0VSVElGSUNBRE9SQTEuMCwGA1UECgwlU0VSVklDSU8gREUgQURNSU5JU1RSQUNJT04gVFJJQlVUQVJJQTEaMBgGA1UECwwRU0FULUlFUyBBdXRob3JpdHkxKjAoBgkqhkiG9w0BCQEWG2NvbnRhY3RvLnRlY25pY29Ac2F0LmdvYi5teDEmMCQGA1UECQwdQVYuIEhJREFMR08gNzcsIENPTC4gR1VFUlJFUk8xDjAMBgNVBBEMBTA2MzAwMQswCQYDVQQGEwJNWDEZMBcGA1UECAwQQ0lVREFEIERFIE1FWElDTzETMBEGA1UEBwwKQ1VBVUhURU1PQzEVMBMGA1UELRMMU0FUOTcwNzAxTk4zMVwwWgYJKoZIhvcNAQkCE01yZXNwb25zYWJsZTogQURNSU5JU1RSQUNJT04gQ0VOVFJBTCBERSBTRVJWSUNJT1MgVFJJQlVUQVJJT1MgQUwgQ09OVFJJQlVZRU5URTAeFw0xOTA2MjkxODE2MTlaFw0yMzA2MjkxODE2MTlaMIIBlDFJMEcGA1UEAxNAU0lTVEVNQSBPUEVSQURPUiBERSBMT1MgU0VSVklDSU9TIERFIEFHVUEgUE9UQUJMRSBZIEFMQ0FOVEFSSUxMQTFqMGgGA1UEKRNhU0lTVEVNQSBPUEVSQURPUiBERSBMT1MgU0VSVklDSU9TIERFIEFHVUEgUE9UQUJMRSBZIEFMQ0FOVEFSSUxMQURPIERFTCBNVU5JQ0lQSU8gREUgQ1VBVVRMQU5DSU5HTzFJMEcGA1UEChNAU0lTVEVNQSBPUEVSQURPUiBERSBMT1MgU0VSVklDSU9TIERFIEFHVUEgUE9UQUJMRSBZIEFMQ0FOVEFSSUxMQTElMCMGA1UELRMcU09TOTcwODA4U003IC8gVEVQSDc4MDgyMDFLMTEeMBwGA1UEBRMVIC8gVEVQSDc4MDgyMEhQTFBMRzEzMUkwRwYDVQQLE0BTSVNURU1BIE9QRVJBRE9SIERFIExPUyBTRVJWSUNJT1MgREUgQUdVQSBQT1RBQkxFIFkgQUxDQU5UQVJJTExBMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApWcOeIKbhag1Cvx7sj1UWURZAX2myt6qMsj0X9Oo6uvrGHi5rcn7wmAGkFpLAM4uO20o6lWH7ebe20Qj0XG+Xm3FH8rvz+Q+bx5aUMOo8jpECeIPili6BZqdFJje0+HXBaT8mAkVo+dVjS+I6j3i5jSwTZwh4HtZn1Q3DhlHma5fEAZypClVsgfRPQyr3IK3clO4KrrmzvOUV81B9Djs1iBhELXsR7Jevc/YFg3S5ijiaB9ZzbwkmkV/ko9IGafmusxbY/hoq42Pgi5pewCPhpFNVlmJ3KCHrAWPQSk18/85qzM2K2ePwfdcB124Zmwnoh8xgUjgjIE4QVS0GNVpIwIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEAb51YhK1M5EwsaayXz13krtmM6Xmr+CeICFFdF46ObXq+45KpWY8bBxP0yj6CqQ90ya8/QPXWMdPm0CiqvJ17B5ZJf7sV1hhYOfP/AZyHqQgcEQVzlV03z0jm6LayBow3OhQ66ElYof9BItCxN8H6IOZ6hJYiCwzOZAPZT4cYHqWMbnCNxvrZI861mI2pUX0cNCSGkfoREeEIbavwtKqjiaaxLCDNxHM5Zn7WlwquVW7JmDmrYf0JQCZ4MFk36TOPaxv8ExS1loyk1C23MOuvUvmjASZ+ax3/xUxalaNmo2/9o24naby/Pm639RT9hlmEdqkn+nSGcSMXkpD73pyFUWQceDJd4Yri8YTPe+lH4rX//eONktsrgZX40+tNF6tbjdE0qxxuxGPv2Z2QoZsAAjHKcMcQ4uZ7SnrylFV7sNPo5X711G3BRmCAGlwlt9P5N2L3akpzJkPxEabPe/E2PdUggtQdfBm8nBASQNocX4o2RTKNCGZkxrZ30B4g3yGE9SmCqyRMKPo0Bck2rOHDmcIQ/4XRXMLiIy0FqM7zkVj8sj6s0dyqzDHRYfkgwQ3t7/dgtW/0iiw/iNsHT2uXlMM89lO7SGz3xq5e3Q2Krg82eyjiMEtiBtD5Jn33t7b3T6uQ0yfkqP1W1neNZsAqs5PVXIEPJCmw9uUG6RV5Xmk=",
                        PrivateKey = "MIIFDjBABgkqhkiG9w0BBQ0wMzAbBgkqhkiG9w0BBQwwDgQIAgEAAoIBAQACAggAMBQGCCqGSIb3DQMHBAgwggS8AgEAMASCBMgIyBKWyh87HbNoYd5uz/91TmoajU3gwm8ZQ9gw4nOhT8/IDym4S4wlvqJG7Dc//WzcLYDQfE/V1SrW5knLw7ja/72pUG1kZ3wRBWQDJxdzbqVSbAmY43GXJRddL33ll0RnNZ8FBusel/RHtFHg+AiuPgZVQqKJz5sJKyDUCJPbpi7Rl2uuIy1BKlLWnFo3ojUZa5ji0A/rFg9ZKUWJmO3HRsJpxLmnP/pcSikMhlnpYhzeRRaZg2gs9MMluYaIDM5mP33EamTYBfMlSF6XgyCl8t22/9fQ+q3u70jlC0Ckqniw3Fp9MmvwCX1Ew78MFugaXL2xadVDf2e3I/eW+I/Hi1VeaGzdCO6+xBCpbrc0YUVEeYe42r0BbpEz/gDHTnB79DGTF8GshwxI/dcPAzg/KucZvDv3phL0qfrTykbpTbJS1Tb7Ei2DP+GMzRO7RYb4R9E81fiwhnOInGeOdQBTtINJzhIBtwAhO3sCxm7Qx2RUPle2iYDcFNRbK7iON/AGzZUN0Qol2HYUJwjMimFTKVIfLe2sa56jqCeJY1OdeIjfxfPVhSnMnADaq9lTRCylNOMtxHPJR/lsrk4LbRC6Ll5dTqQ37vTsd8yUzLlRF/ZQm0pmNgzIvoAJaYCWI6fytXf0A/Ehxzs73/ODMJTzlfOpiyElk7Q0Z6X8IVzjEnKRf7vkKx3d6vEiXAZW2nEAXhKhbsImrTLJtn01uyldFpU3aEadgTYk0HUh7Z9sPhmmbPoyMp6/d1tDV22Zfb29IOUY6kG3teZCOFtW+SkLy6fRDEKTVevwWvHHjDakNReP1Oki6VIl9yr5+QjD407n1bwlPaufNkQ2yLLBiZs7yhQbQlBeRupB8ic1iUCOydzIA4FBcQz7wYQ6Hh4Q86rLFMyeBxdiaM6WhvWPTkFMpJpbTDmwSLNU5A44nSmHbmJFdQP/y2jBIyXnC4HNUgT6iCSNXbtMfLMhSpvmf+jAzkbG4L/+5Hie3QAVph2YbrIZVDX5HIEoS7p1I6+J8wfa/sSoXsPxZl5xqL4qLIgyC7b0dhaQ/EcLNmeGaE/SkSzxrlh79p2nQyNGaSBMFfteF0g+e8EmRzllXUyGf3r+LAhWhoH8roB5T9gyVBVlIz/bneCHg5qhZHlbeiJlDtUEOJXElgOxNGVu1MrWhk7FE2o2vEDMVVFxk4MoDiMVLBl4ux0eKaCAovk+WBqehw3/YJP24XrMX7Os5W8LjHZIBp9nFC4GwGWsHQfJxvFI4qMesCq3UW/ieq8/NatIt353yvYV1U5Br14hXAk2E2mLeDdAl4e2R4RcvFOzpAgMnxbVYAoXE11BD0HKF9+BiPHu25IP5dw0QRC7+9IMBd5SGD+wOBlXCD4XFg26AX8jZRMIYzxjVOAgRe6b6i84xJ83NlvaebuyV7TyMhhvMGCt6QEPwIIxQOl1b2TLXMqMPLUn9ZDr8SI0ZmRLavVIAVIP7ER6psl5R1Aps5O1SeAunZ4Vmg67LrlGV/uorH4KhkzakvWte9bRjqT1tX5N0zN0EN/755F3NsCWuPTivGlSunqsfZNqkKqE4UgIDPPPCQHZkgN5bVarJJY8stPdnzfNiP94XAYDl2xQzqpjX3ucgZIF2lcNQEA=",
                        PrivateKeyPassword = "htepox1978"
                    };
                }

                facturama.Csds.Create(csdRequest);
                return "regitro exitoso";
            }
            catch(Exception ex)
            {
                return "Usuario ya esta registrado";
            }
        }

        private string GeneraCarpetaDescagasXML()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DownloadXML";

            try
            {
                DirectoryInfo di;
                if (!Directory.Exists(path))
                {
                    di = Directory.CreateDirectory(path);
                    return path;
                }
                return path;
            }
            catch (Exception ex)
            {
                return "El directorio no pudo ser creado.";
            }
        }
        private string LeerXML(string path)
        {
            string stringXML = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                //doc.LoadXml(xmlcreado);
                //doc.Save(@"c:\Pruebas\XmlArmado.xml");
                doc.Load(path);
                stringXML = doc.OuterXml;
                return stringXML;
            }
            catch (Exception ex)
            {
                return "error: No se encontro el archivo";
            }
        }

        private async Task<string> guardarXMLenBD(string pXML, string pUuid, string pRfcReceptor, string pTipo, string status, int pIdPayment, string idCFDI)
        {
            string json = string.Empty;

            try
            {               
                Model.TaxReceipt xMLS = new Model.TaxReceipt();               
                xMLS.TaxReceiptDate = DateTime.Now;
                xMLS.Xml = pXML;
                xMLS.FielXML = pUuid;
                xMLS.RFC = pRfcReceptor;
                xMLS.Type = pTipo;
                xMLS.Status = status;
                xMLS.PaymentId = pIdPayment;                
                xMLS.UserId = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().UserId;
                xMLS.IdXmlFacturama = idCFDI;
                xMLS.UsoCFDI = string.IsNullOrEmpty(msgUsos) ? "P01 - Por definir" : msgUsos;

                HttpContent content;
                json = JsonConvert.SerializeObject(xMLS);
                content = new StringContent(json, Encoding.UTF8, "application/json");
                var jsonResponse = await Requests.SendURIAsync("/api/TaxReceipt/", HttpMethod.Post, Variables.LoginModel.Token, content);

                return "ok";                
            }
            catch (Exception ex)
            {
                return "error: No se pudo registrar en BD la Factura.";
            }
        }
        private async Task<string> actualizarPaymentConFactura(Model.Payment pPay)
        {
            HttpContent content;
            try
            {
                pPay.HaveTaxReceipt = true;
                pPay.ObservationInvoice = string.IsNullOrEmpty(msgObservacionFactura) ? "Sin Observación" : msgObservacionFactura;
                content = new StringContent(JsonConvert.SerializeObject(pPay), Encoding.UTF8, "application/json");
                var updatePayment = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", pPay.Id), HttpMethod.Put, Variables.LoginModel.Token, content);
                return "ok";
            }
            catch(Exception ex)
            {
                return "error: No se pudo actualizar Payment";
            }
        }

    }

}
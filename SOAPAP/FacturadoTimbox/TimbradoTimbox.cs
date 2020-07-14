using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Facturama.Models.Request;
using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.FacturadoTimbox.Model;
using SOAPAP.FacturadoTimbox.Utils;
using SOAPAP.Services;
using SOAPAP.UI;
using SOAPAP.TimboxSandboxWS;
using SOAPAP.TimboxWS;

namespace SOAPAP.FacturadoTimbox
{
    public class TimbradoTimbox
    {
        private RequestsAPI Requests = null;
        //Sandbox 
        TimboxSandboxWS.timbrado_cfdi33_portClient cliente_timbrar;
        TimboxSandboxWS.timbrar_cfdi_result responseWs = new TimboxSandboxWS.timbrar_cfdi_result();
        //Productivo
        TimboxWS.timbrado_cfdi33_portClient cliente_timbrarProd;
        TimboxWS.timbrar_cfdi_result responseWsProd = new TimboxWS.timbrar_cfdi_result();

        string resultadoFinal;

        public TimbradoTimbox()
        {
            Requests = new RequestsAPI(Properties.Settings.Default.URL);
            cliente_timbrar = new TimboxSandboxWS.timbrado_cfdi33_portClient();
            cliente_timbrarProd = new TimboxWS.timbrado_cfdi33_portClient();
        }


        public async Task<Facturama.Models.Response.Cfdi> Facturar(CfdiMulti cfdiMulti)
        {
            try
            {
                #region datos Prueba
                //Comprobante comprobante = new Comprobante();
                //comprobante.Version = "3.3";
                //comprobante.Serie = "A";
                //comprobante.Folio = "123456";
                //comprobante.Fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                ////comprobante.Sello = "";
                //comprobante.FormaPago = "99"; //por definir
                ////comprobante.NoCertificado = numeroCertificado;
                ////comprobante.Certificado = "";
                //comprobante.SubTotal = 10.1m;
                //comprobante.Descuento = 2.0m;
                //comprobante.Moneda = "MXN";
                //comprobante.Total = 10.1m;
                //comprobante.TipoDeComprobante = "I";
                //comprobante.MetodoPago = "PUE";
                //comprobante.LugarExpedicion = "20131";

                //ComprobanteEmisor cEmisor = new ComprobanteEmisor();
                //cEmisor.Rfc = "POWE896754SD1";
                //cEmisor.Nombre = "Emisor ficticio";
                //cEmisor.RegimenFiscal = "603";

                //ComprobanteReceptor cReceptor = new ComprobanteReceptor();
                //cReceptor.Rfc = "MAU123456SD1";
                //cReceptor.Nombre = "Mauricio Castillo SA. de CV";
                //cReceptor.UsoCFDI = "P01";

                //comprobante.Emisor = cEmisor;
                //comprobante.Receptor = cReceptor;

                ////Conceptos
                //List<ComprobanteConcepto> lstConceptos = new List<ComprobanteConcepto>();
                //ComprobanteConcepto Concep = new ComprobanteConcepto();
                //Concep.Importe = 5.0m;
                //Concep.ClaveProdServ = "92111704";
                //Concep.Cantidad = 1;
                //Concep.ClaveUnidad = "C01";
                //Concep.Descripcion = "Misil de guerra";
                //Concep.ValorUnitario = 7.0m;
                //Concep.Descuento = 2.0m;

                //ComprobanteConcepto Concep2 = new ComprobanteConcepto();
                //Concep2.Importe = 5.1m;
                //Concep2.ClaveProdServ = "92211704";
                //Concep2.Cantidad = 1;
                //Concep2.ClaveUnidad = "C01";
                //Concep2.Descripcion = "Porta misil de guerra";
                //Concep2.ValorUnitario = 5.1m;

                //lstConceptos.Add(Concep);
                //lstConceptos.Add(Concep2);

                //comprobante.Conceptos = lstConceptos.ToArray();
                #endregion

                Comprobante comprobante = new Comprobante();
                comprobante.Version = "3.3";
                comprobante.Serie = cfdiMulti.Serie ;
                comprobante.Folio = cfdiMulti.Folio;
                comprobante.Fecha = DateTime.Now.AddMinutes(-5).ToString("yyyy-MM-ddTHH:mm:ss");
                //comprobante.Sello = "";
                comprobante.FormaPago = cfdiMulti.PaymentForm;
                comprobante.CondicionesDePago = cfdiMulti.PaymentConditions;
                //comprobante.NoCertificado = numeroCertificado;
                //comprobante.Certificado = "";
                comprobante.SubTotal = cfdiMulti.Items.Sum(x => x.Subtotal);
                comprobante.Descuento = cfdiMulti.Items.Sum(x => x.Discount);
                comprobante.Moneda = cfdiMulti.Currency; // "MXN";
                comprobante.Total = cfdiMulti.Items.Sum(x => x.Total);                
                comprobante.TipoDeComprobante = cfdiMulti.CfdiType.ToString().Substring(0,1); //"I";
                comprobante.MetodoPago = cfdiMulti.PaymentMethod; //"PUE";
                comprobante.LugarExpedicion = cfdiMulti.ExpeditionPlace;

                ComprobanteEmisor cEmisor = new ComprobanteEmisor();
                cEmisor.Rfc = cfdiMulti.Issuer.Rfc;
                cEmisor.Nombre = cfdiMulti.Issuer.Name;
                cEmisor.RegimenFiscal = cfdiMulti.Issuer.FiscalRegime;

                ComprobanteReceptor cReceptor = new ComprobanteReceptor();
                cReceptor.Rfc = cfdiMulti.Receiver.Rfc;
                cReceptor.Nombre = cfdiMulti.Receiver.Name;
                cReceptor.UsoCFDI = cfdiMulti.Receiver.CfdiUse;

                comprobante.Emisor = cEmisor;
                comprobante.Receptor = cReceptor;

                //Conceptos
                List<ComprobanteConcepto> lstConceptos = new List<ComprobanteConcepto>();
                List<ComprobanteConceptoImpuestosTraslado> lstCCIR = new List<ComprobanteConceptoImpuestosTraslado>();
                foreach (var item in cfdiMulti.Items)
                {
                    ComprobanteConcepto Concep = new ComprobanteConcepto();                    
                    Concep.ClaveProdServ = item.ProductCode;
                    Concep.ClaveUnidad = item.UnitCode ;
                    Concep.Descripcion = item.Description ;
                    Concep.Cantidad = item.Quantity;
                    Concep.ValorUnitario = item.UnitPrice;
                    Concep.Descuento = item.Discount;
                    Concep.Importe = (item.Total + item.Discount) - (item.Taxes != null ? item.Taxes.Sum(x => x.Total) : 0);

                    //Se agregan Iva si lo tiene
                    ComprobanteConceptoImpuestos cci = new ComprobanteConceptoImpuestos();
                    if (item.Taxes != null)
                    {
                        List<ComprobanteConceptoImpuestosTraslado> lstTemp = new List<ComprobanteConceptoImpuestosTraslado>();
                        foreach (var iva in item.Taxes)
                        {
                            ComprobanteConceptoImpuestosTraslado ccit = new ComprobanteConceptoImpuestosTraslado();
                            ccit.Base = iva.Base;
                            ccit.Importe = iva.Total;
                            ccit.TasaOCuota = iva.Rate;
                            ccit.Impuesto = "002";
                            ccit.TipoFactor = "Tasa";
                            lstTemp.Add(ccit);
                        }
                        if (lstTemp.Count > 0)
                        {
                            cci.Traslados = lstTemp.ToArray();
                            lstCCIR.AddRange(lstTemp);
                        }
                    }
                    Concep.Impuestos = cci;
                    lstConceptos.Add(Concep);
                }
                comprobante.Conceptos = lstConceptos.ToArray();

                //Se arma el total de impuestos
                if(lstCCIR.Count > 0)
                {
                    var res = lstCCIR.GroupBy(x => x.TasaOCuota).Select(y => new { Tasa = y.Key, elems = y.ToList() });
                    List<ComprobanteImpuestosTraslado> lstCIT = new List<ComprobanteImpuestosTraslado>();
                    foreach (var item in res)
                    {
                        ComprobanteImpuestosTraslado CIT = new ComprobanteImpuestosTraslado();
                        CIT.Importe = item.elems.Sum(x => x.Importe);
                        CIT.Impuesto = "002";
                        CIT.TasaOCuota = item.Tasa;
                        CIT.TipoFactor = "Tasa";
                        lstCIT.Add(CIT);
                    }
                    ComprobanteImpuestos CI = new ComprobanteImpuestos();
                    CI.Traslados = lstCIT.ToArray();
                    CI.TotalImpuestosTrasladados = lstCCIR.Sum(x => x.Importe);
                    comprobante.Impuestos = CI;
                }                
                
                //Se arma el xml con los datos del cfdi.
                var content = new StringContent(JsonConvert.SerializeObject(comprobante), Encoding.UTF8, "application/json");
                string resultFac = await Requests.SendURIAsync(string.Format("/api/Facturacion/SellarXml"), HttpMethod.Post, Variables.LoginModel.Token, content);                
                if (resultFac.Contains("error"))
                {
                    var mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultFac).error, TypeIcon.Icon.Cancel);
                    var result = mensaje.ShowDialog();
                }
                else
                {
                    string xmlBase64 = JsonConvert.DeserializeObject<string>(resultFac);
                                        
                    //Se obtienen nuestros datos de facturacion.
                    var dataUser = await Requests.SendURIAsync(string.Format("/api/Facturacion/DataUser"), HttpMethod.Get, Variables.LoginModel.Token);
                    if (!string.IsNullOrEmpty(dataUser))
                    {
                        var defDataUser = new { User = "", Pass = "" };
                        var resDataUser = JsonConvert.DeserializeAnonymousType(dataUser, defDataUser);

                        if(Variables.Configuration.CFDITest.Contains("F") || Variables.Configuration.CFDITest.Contains("f"))
                        {
                            responseWsProd = cliente_timbrarProd.timbrar_cfdi(resDataUser.User, resDataUser.Pass, xmlBase64);
                            resultadoFinal = responseWsProd.xml.Replace("\n", "").Replace("\\\"", "\"");
                        }
                        else
                        {
                            responseWs = cliente_timbrar.timbrar_cfdi(resDataUser.User, resDataUser.Pass, xmlBase64);
                            resultadoFinal = responseWs.xml.Replace("\n", "").Replace("\\\"", "\"");
                        }
                        
                        //Se genera xml para salvarlo en disco.
                        string path = GeneraCarpetaDescagasXML();
                        string nombreXML = string.Format("\\{0}_{1}_{2}.xml", cfdiMulti.Issuer.Rfc, cfdiMulti.Receiver.Rfc, cfdiMulti.Serie + cfdiMulti.Folio);
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(resultadoFinal);
                        xmlDocument.Save(path + nombreXML);

                        return ConvertirAClaseDeSalida(cfdiMulti, xmlDocument);
                    }
                    else
                    {
                        resultadoFinal = null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }            
        }

        private Facturama.Models.Response.Cfdi ConvertirAClaseDeSalida(CfdiMulti cfdiMulti, XmlDocument xmlDocument)
        {
            Facturama.Models.Response.Cfdi resCfdi = new Facturama.Models.Response.Cfdi();
            resCfdi.Id = "Timbox";

            Comprobante comprobante = new Comprobante();
            
            
            resCfdi.Serie = cfdiMulti.Serie;            
            resCfdi.Folio = cfdiMulti.Folio;            
            resCfdi.Date = DateTime.Now.AddMinutes(-5).ToString("yyyy-MM-ddTHH:mm:ss");            
            resCfdi.CertNumber = "";           
            resCfdi.Subtotal = cfdiMulti.Items.Sum(x => x.Subtotal);           
            resCfdi.Discount = cfdiMulti.Items.Sum(x => x.Discount);            
            resCfdi.Currency = cfdiMulti.Currency; // "MXN";            
            resCfdi.Total = cfdiMulti.Items.Sum(x => x.Total);           
            resCfdi.CfdiType = cfdiMulti.CfdiType.ToString().Substring(0, 1); //"I";            
            resCfdi.PaymentMethod = cfdiMulti.PaymentMethod;            
            resCfdi.ExpeditionPlace = cfdiMulti.ExpeditionPlace;
            resCfdi.PaymentConditions = cfdiMulti.PaymentConditions;
                       
            Facturama.Models.Response.Issuer emisor = new Facturama.Models.Response.Issuer();
            emisor.Rfc = cfdiMulti.Issuer.Rfc;
            emisor.TaxName = cfdiMulti.Issuer.Name;
            emisor.FiscalRegime = cfdiMulti.Issuer.FiscalRegime;
                       
            Facturama.Models.Response.Receiver receptor = new Facturama.Models.Response.Receiver();
            receptor.Rfc = cfdiMulti.Receiver.Rfc;
            receptor.Name = cfdiMulti.Receiver.Name;
                       
            resCfdi.Issuer = emisor;
            resCfdi.Receiver = receptor;
                        
            //Conceptos
            List<Facturama.Models.Response.Item> lstConceptos = new List<Facturama.Models.Response.Item>();
            List<ComprobanteConceptoImpuestosTraslado> lstCCIR = new List<ComprobanteConceptoImpuestosTraslado>();
            foreach (var item in cfdiMulti.Items)
            {
                Facturama.Models.Response.Item Concep = new Facturama.Models.Response.Item();
                //Concep. = item.ProductCode;
                Concep.Unit = item.UnitCode;
                Concep.Description = item.Description;
                Concep.Quantity = item.Quantity;
                Concep.UnitValue = item.UnitPrice;
                //Concep.di = item.Discount;
                Concep.Total = item.Total + item.Discount;

                if (item.Taxes != null)
                {
                    foreach (var iva in item.Taxes)
                    {
                        ComprobanteConceptoImpuestosTraslado ccit = new ComprobanteConceptoImpuestosTraslado();
                        ccit.Base = iva.Base;
                        ccit.Importe = iva.Total;
                        ccit.TasaOCuota = iva.Rate;
                        ccit.Impuesto = "002";
                        ccit.TipoFactor = "Tasa";
                        lstCCIR.Add(ccit);
                    }
                }
                
                lstConceptos.Add(Concep);
            }
            resCfdi.Items = lstConceptos.ToArray();

            //Se calcula el total de Iva.            
            var res = lstCCIR.GroupBy(x => x.TasaOCuota).Select(y => new { Tasa = y.Key, elems = y.ToList() });
            List<Facturama.Models.Response.Tax> lstTax = new List<Facturama.Models.Response.Tax>();
            foreach (var item in res)
            {
                Facturama.Models.Response.Tax tax = new Facturama.Models.Response.Tax();
                tax.Name = "IVA";
                tax.Rate = item.Tasa;
                tax.Total = item.elems.Sum(x => x.Importe);
                tax.Type = "002";                
                lstTax.Add(tax);
            }
            resCfdi.Taxes = lstTax.ToArray();

            //Complementos
            Facturama.Models.Response.Complement complement = new Facturama.Models.Response.Complement();

            //XmlNodeList elemList =  xmlDocument.GetElementsByTagName("title");
            XmlNodeList elemList = xmlDocument.GetElementsByTagName("tfd:TimbreFiscalDigital");
            Facturama.Models.Response.TaxStamp taxStamp = new Facturama.Models.Response.TaxStamp();
            taxStamp.Uuid = elemList.Item(0).Attributes["UUID"].Value;
            taxStamp.SatCertNumber = elemList.Item(0).Attributes["NoCertificadoSAT"].Value;
            taxStamp.SatSign = elemList.Item(0).Attributes["SelloSAT"].Value;
            taxStamp.CfdiSign = elemList.Item(0).Attributes["SelloCFD"].Value;
            taxStamp.Date = elemList.Item(0).Attributes["FechaTimbrado"].Value;
            complement.TaxStamp = taxStamp;
            resCfdi.Complement = complement;

            return resCfdi;
        }

        private string GeneraCarpetaDescagasXML()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Facturas";
            try
            {
                DirectoryInfo di;
                if (!Directory.Exists(path))
                {
                    di = Directory.CreateDirectory(path);
                }

                //Se crea la una nueva carpeta por fecha
                path = path + "\\Facturas" + DateTime.Now.ToString("yyyy-MM-dd");
                if (!Directory.Exists(path))
                {
                    di = Directory.CreateDirectory(path);
                }
                return path;
            }
            catch (Exception ex)
            {
                return "El directorio no pudo ser creado.";
            }
        }

        public async Task<ModFac.ResponseCFDI> getCFDI(SOAPAP.Model.TaxReceipt taxReceipt)
        {
            ModFac.ResponseCFDI resCfdi = new ModFac.ResponseCFDI();
            TimboxWS.recuperar_comprobante_result rcr = new TimboxWS.recuperar_comprobante_result();

            try
            {
                resCfdi.Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                //Se obtienen nuestros datos de facturacion.
                var dataUser = await Requests.SendURIAsync(string.Format("/api/Facturacion/DataUser"), HttpMethod.Get, Variables.LoginModel.Token);
                if (!string.IsNullOrEmpty(dataUser))
                {
                    var defDataUser = new { User = "", Pass = "" };
                    var resDataUser = JsonConvert.DeserializeAnonymousType(dataUser, defDataUser);

                    if (Variables.Configuration.CFDITest.Contains("F") || Variables.Configuration.CFDITest.Contains("f"))
                    {
                        TimboxWS.uuid uuids = new TimboxWS.uuid();
                        uuids.uuid1 = new string[] { taxReceipt.FielXML };
                        rcr = await cliente_timbrarProd.recuperar_comprobanteAsync(resDataUser.User, resDataUser.Pass, uuids);
                       
                        if (rcr.estatus.Contains("200"))
                        {
                            XmlDocument doc = new XmlDocument();
                            string xmlCadena = rcr.comprobantes.Replace("\n", "").Replace("\\\"", "\""); //.Replace("?","");
                            doc.LoadXml(xmlCadena);
                            string json = JsonConvert.SerializeXmlNode(doc);

                            //fillData(json);
                        }
                        

                        resultadoFinal = rcr.comprobantes;
                    }
                    else
                    {
                        //responseWs = cliente_timbrar.timbrar_cfdi(resDataUser.User, resDataUser.Pass, xmlBase64);
                        //resultadoFinal = responseWs.xml.Replace("\n", "").Replace("\\\"", "\"");
                    }
                }
                return resCfdi;
            }
            catch(Exception ex)
            {
                //return null;
            }
            return null;
        }

        private void fillData(string json)
        {


        }


        //public async Task<string> CancelarFactura( SOAPAP.Model.Payment payment)
        //{
        //    var key = payment.TaxReceipts.Where(x => x.Status == "ET001").FirstOrDefault();
        //    try
        //    {
        //        FolioCancelacionTimbox FCT = new FolioCancelacionTimbox();
        //        FCT.UUID = key.FielXML;
        //        FCT.ReceptorRFC = key.RFC;
        //        FCT.Total = payment.Total;
        //        FCT.PaymentId = payment.Id;
        //        FCT.TaxReceiptId = key.Id;

        //        List<FolioCancelacionTimbox> lstFCT = new List<FolioCancelacionTimbox>();
        //        lstFCT.Add(FCT);

        //        var a = JsonConvert.SerializeObject(lstFCT);
        //        HttpContent content = new StringContent(a, Encoding.UTF8, "application/json");
        //        var response = await Requests.SendURIAsync("/api/Facturacion/CancelarFacturasTimbox", HttpMethod.Post, Variables.LoginModel.Token, content);

        //        ////Complementos
        //        //Facturama.Models.Response.Complement complement = new Facturama.Models.Response.Complement();

        //        ////XmlNodeList elemList =  xmlDocument.GetElementsByTagName("title");
        //        //XmlNodeList elemList = xmlDocument.GetElementsByTagName("tfd:TimbreFiscalDigital");
        //        //Facturama.Models.Response.TaxStamp taxStamp = new Facturama.Models.Response.TaxStamp();
        //        //taxStamp.Uuid = elemList.Item(0).Attributes["UUID"].Value;
        //        //taxStamp.SatCertNumber = elemList.Item(0).Attributes["NoCertificadoSAT"].Value;
        //        //taxStamp.SatSign = elemList.Item(0).Attributes["SelloSAT"].Value;

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return "Cancelada";
        //}

    }
}

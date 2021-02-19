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
                            int indiceIni = xmlCadena.IndexOf("<xml>");
                            int indiceFin = xmlCadena.IndexOf("</xml>");
                            string xmlCFDI = xmlCadena.Substring(indiceIni + 5, indiceFin - (indiceIni + 5));

                            doc.LoadXml(xmlCFDI);
                            //string json = JsonConvert.SerializeXmlNode(doc);                                                        
                            resCfdi = fillData(doc);
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

        private ModFac.ResponseCFDI fillData(XmlDocument doc)
        {
            try
            {
                ModFac.ResponseCFDI responseCFDI = new ModFac.ResponseCFDI();
                
                foreach (XmlNode item in doc.DocumentElement.ChildNodes)
                {
                    if (item.Name.Contains("Emisor"))
                    {
                        Facturama.Models.Response.Issuer issuer = new Facturama.Models.Response.Issuer();
                        issuer.Rfc = item.Attributes["Rfc"].Value;
                        issuer.TaxName = item.Attributes["Nombre"].Value;
                        issuer.ComercialName = item.Attributes["Nombre"].Value;
                        issuer.FiscalRegime = item.Attributes["RegimenFiscal"].Value.Equals("603") ? "603 - Personas Morales con Fines no Lucrativos" : item.Attributes["RegimenFiscal"].Value;
                        responseCFDI.Issuer = issuer;
                    }
                    if (item.Name.Contains("Receptor"))
                    {
                        Facturama.Models.Response.Receiver receiver = new Facturama.Models.Response.Receiver();
                        receiver.Rfc = item.Attributes["Rfc"].Value;
                        receiver.Name = item.Attributes["Nombre"].Value;
                        //receiver.Email = item.Attributes["UsoCFDI"].Value;
                        responseCFDI.Receiver = receiver;
                    }
                    if (item.Name.Contains("Comprobante"))
                    {                        
                        responseCFDI.Serie = item.Attributes["Serie"].Value;
                        responseCFDI.Folio = item.Attributes["Folio"].Value;
                        responseCFDI.Date = item.Attributes["Fecha"].Value;
                        //responseCFDI.Serie = item.Attributes["Sello"].Value;
                        responseCFDI.PaymentTerms = getFormaPago(item.Attributes["FormaPago"].Value);
                        responseCFDI.CertNumber = item.Attributes["NoCertificado"].Value;
                        //responseCFDI.Folio = item.Attributes["Certificado"].Value;
                        responseCFDI.PaymentConditions = item.Attributes["CondicionesDePago"].Value;
                        responseCFDI.Subtotal = decimal.Parse(item.Attributes["SubTotal"].Value);
                        responseCFDI.Discount = decimal.Parse(item.Attributes["Descuento"].Value);
                        responseCFDI.Currency = item.Attributes["Moneda"].Value;
                        responseCFDI.Total = decimal.Parse(item.Attributes["Total"].Value);
                        responseCFDI.CfdiType = item.Attributes["TipoDeComprobante"].Value.Equals("I") ? "I - Ingresos" : "E - Egresos";
                        responseCFDI.PaymentMethod = item.Attributes["MetodoPago"].Value.Equals("PUE") ? "PUE - Pago en una sola exhibición" : "";
                        responseCFDI.ExpeditionPlace = item.Attributes["LugarExpedicion"].Value;
                    }
                    if (item.Name.Contains("Complemento"))
                    {
                        XmlNode nodeComplemento = item.ChildNodes[0];
                        Facturama.Models.Response.Complement complement = new Facturama.Models.Response.Complement();
                        Facturama.Models.Response.TaxStamp taxStamp = new Facturama.Models.Response.TaxStamp();
                        taxStamp.Uuid = nodeComplemento.Attributes["UUID"].Value;
                        taxStamp.Date = nodeComplemento.Attributes["FechaTimbrado"].Value;
                        //taxStamp. = nodeComplemento.Attributes["RfcProvCertif"].Value;
                        taxStamp.CfdiSign = nodeComplemento.Attributes["SelloCFD"].Value;
                        taxStamp.SatCertNumber = nodeComplemento.Attributes["NoCertificadoSAT"].Value;
                        taxStamp.SatSign = nodeComplemento.Attributes["SelloSAT"].Value;

                        complement.TaxStamp = taxStamp;                        
                        responseCFDI.Complement = complement;
                    }
                    if (item.Name.Contains("Conceptos"))
                    {
                        List<Facturama.Models.Response.Item> items = new List<Facturama.Models.Response.Item>();
                        List<Facturama.Models.Response.Tax> taxes = new List<Facturama.Models.Response.Tax>();
                        foreach (XmlNode nodeConcepto in item.ChildNodes)
                        {
                            Facturama.Models.Response.Item item1 = new Facturama.Models.Response.Item();
                            //item1.Description = nodeConcepto.Attributes["ClaveProdServ"].Value;
                            item1.Quantity = decimal.Parse(nodeConcepto.Attributes["Cantidad"].Value);
                            item1.Unit = nodeConcepto.Attributes["ClaveUnidad"].Value;
                            item1.Description = nodeConcepto.Attributes["Descripcion"].Value;
                            item1.UnitValue = decimal.Parse(nodeConcepto.Attributes["ValorUnitario"].Value);
                            item1.Total = decimal.Parse(nodeConcepto.Attributes["Importe"].Value);
                            //item1.Total = nodeConcepto.Attributes["Descuento"].Value;
                            items.Add(item1);

                            XmlNode nodeTaxes = nodeConcepto.ChildNodes[0];
                            if(nodeTaxes.Attributes.Count > 0) 
                            { }
                        }
                        responseCFDI.Items = items.ToArray();
                        responseCFDI.Taxes = taxes.ToArray();
                    }
                }

                responseCFDI.Status = "active";                
                responseCFDI.Serie = doc.DocumentElement.Attributes["Serie"].Value;
                responseCFDI.Folio = doc.DocumentElement.Attributes["Folio"].Value;
                responseCFDI.Date = doc.DocumentElement.Attributes["Fecha"].Value;
                //responseCFDI.Serie = doc.DocumentElement.Attributes["Sello"].Value;
                responseCFDI.PaymentTerms = getFormaPago(doc.DocumentElement.Attributes["FormaPago"].Value);
                responseCFDI.CertNumber = doc.DocumentElement.Attributes["NoCertificado"].Value;
                //responseCFDI.Folio = doc.DocumentElement.Attributes["Certificado"].Value;
                responseCFDI.PaymentConditions = doc.DocumentElement.Attributes["CondicionesDePago"].Value;
                responseCFDI.Subtotal = decimal.Parse(doc.DocumentElement.Attributes["SubTotal"].Value);
                responseCFDI.Discount = decimal.Parse(doc.DocumentElement.Attributes["Descuento"].Value);
                responseCFDI.Currency = doc.DocumentElement.Attributes["Moneda"].Value;
                responseCFDI.Total = decimal.Parse(doc.DocumentElement.Attributes["Total"].Value);
                responseCFDI.CfdiType = doc.DocumentElement.Attributes["TipoDeComprobante"].Value.Equals("I") ? "I - Ingresos" : "E - Egresos";
                responseCFDI.PaymentMethod = doc.DocumentElement.Attributes["MetodoPago"].Value.Equals("PUE") ? "PUE - Pago en una sola exhibición" : doc.DocumentElement.Attributes["MetodoPago"].Value;
                responseCFDI.ExpeditionPlace = doc.DocumentElement.Attributes["LugarExpedicion"].Value;

                return responseCFDI;
            }
            catch (Exception ex)
            {
                return null;
            }            
        }

        public async Task<string> CancelarFacturaDesdeAPI(DataCancelationTimbox dataCancelationTimbox)
        {
            try
            {
                List<DataCancelationTimbox> tmpList = new List<DataCancelationTimbox>() { dataCancelationTimbox };
                HttpContent content;
                var json = JsonConvert.SerializeObject(tmpList);
                content = new StringContent(json, Encoding.UTF8, "application/json");
                var responseCancelacion = await Requests.SendURIAsync("/api/Facturacion/CancelarFacturasTimbox", HttpMethod.Post, Variables.LoginModel.Token, content);
                if (responseCancelacion.Contains("error"))
                {
                    return JsonConvert.SerializeObject(new { status = "error", message = "No se ha podido realizar la cancelación, fallo la solicitud a timbox." });                    
                }
                string cfdiCancel = JsonConvert.DeserializeObject<string>(responseCancelacion);

                //ATENCION: Para este metodo, la actuaizacion de TaxReceipt y TaxReceiptCancel se hace en el EndPoint

                return JsonConvert.SerializeObject(new { status = "acepted", message = "Cancelación aceptada: " + cfdiCancel });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { status = "error", message = "No se ha podido realizar la cancelación, fallo la solicitud a Timbox." });
            }
        }

        private string getFormaPago(string code)
        {
            string res = "";
            switch (code)
            {
                case "01":
                    res = "01 = Efectivo";
                    break;
                case "02":
                    res = "02 = Cheque";
                    break;
                case "03":
                    res = "03 = Transferencia";
                    break;
                case "04":
                    res = "04 = Tarjeta (debito o credito)";
                    break;
                case "05":
                    res = "05 = Mixto";
                    break;
                default:
                    res = "01 = Efectivo";
                    break;
            }
            return res;
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

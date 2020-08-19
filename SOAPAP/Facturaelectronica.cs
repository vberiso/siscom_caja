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
using SOAPAP.PDFManager;
using SOAPAP.UI.Messages;
using SOAPAP.Model;
using SOAPAP.ModFac;
using Issuer = Facturama.Models.Request.Issuer;
using Receiver = Facturama.Models.Request.Receiver;
using Item = Facturama.Models.Request.Item;
using Tax = Facturama.Models.Request.Tax;
using SOAPAP.UI;
using SOAPAP.Enums;
using SOAPAP.FacturadoTimbox;
using DevExpress.Pdf;
using DevExpress.Pdf.ContentGeneration;
using System.Drawing;

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
        public bool showMsgObservacion { get; set; } = true;
        string msgCorreo = "";
        bool enviarCorreo = false;

        //Estas variables permiten administrar si es un administador quien trata de Facturar o actualizar factura.
        public bool isAdministrator { get; set; } = false;
        public string ActualUserId { get; set; }    //Es necesario inicializar esta propiedad, si esta facturando un administrador

        public Facturaelectronica()
        { 
            //FACTURAMA: false - productivo
            Requests = new RequestsAPI(UrlBase);
            //facturama = new FacturamaApiMultiemisor("gfdsystems", "gfds1st95", false);
            facturama = new FacturamaApiMultiemisor("pruebas", "pruebas2011");
        }
        public void setMsgs(string msgObservacionFactura, string msgUsos)
        {
            this.msgObservacionFactura = msgObservacionFactura;
            this.msgUsos = msgUsos;
            showMsgObservacion = false;
        }

        //con Facturama PRODUCTIVO
        public async Task<string> generaFactura(string idTransaction, string status, string pSerialCajero = "", string pTipoUso = "")
        {
            try
            {
                string respuesta = string.Empty;
                string rutas = string.Empty;
                string json = string.Empty;
                //Facturama.Models.Response.Cfdi cfdiFacturama = null;
                ModFac.ResponseCFDI cfdiFacturama = null;


                var resultado = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", idTransaction), HttpMethod.Get, Variables.LoginModel.Token);
                if (resultado.Contains("error\\"))
                {
                    return resultado;
                }
                TransactionVM TraVM = JsonConvert.DeserializeObject<TransactionVM>(resultado);

                Agreement ms = null;
                Model.TaxUser tu = null;
                if (TraVM.payment.OrderSaleId == 0)
                {
                    var resultados = await Requests.SendURIAsync(string.Format("/api/Agreements/{0}", TraVM.payment.AgreementId), HttpMethod.Get, Variables.LoginModel.Token);
                    ms = JsonConvert.DeserializeObject<Agreement>(resultados);
                }
                else
                {
                    var resultados = await Requests.SendURIAsync(string.Format("/api/Agreements/{0}", TraVM.payment.AgreementId), HttpMethod.Get, Variables.LoginModel.Token);
                    ms = JsonConvert.DeserializeObject<Agreement>(resultados);
                    var resulTaxUs = await Requests.SendURIAsync(string.Format("/api/TaxUsers/{0}", TraVM.orderSale.TaxUserId), HttpMethod.Get, Variables.LoginModel.Token);
                    tu = JsonConvert.DeserializeObject<Model.TaxUser>(resulTaxUs);
                }


                //string registro = RegitraEmisor();


                string seriefolio = TraVM.transaction.transactionFolios.FirstOrDefault().folio.ToString();
                string serie = seriefolio.Substring(0, 1);
                string folio = seriefolio.Replace(serie, "");

                //Se Obtienen los descuentos
                decimal Descuento = 0;
                string CondicionPago = "";
                if (TraVM.payment.OrderSaleId == 0) //Servicio
                {
                    if (TraVM.payment.Type == "PAY04")
                    {
                        Descuento = 0;
                        CondicionPago = "Pago Anticipado";
                    }
                    else
                    {
                        Descuento = TraVM.payment.PaymentDetails.Sum(x => x.Debt.DebtDiscounts.Where(y => y.CodeConcept == x.CodeConcept).Select(yy => yy.DiscountAmount).FirstOrDefault());
                        //Anexo el periodo de pago (Services)
                        CondicionPago = "Periodo de: " + TraVM.payment.PaymentDetails.Min(p => p.Debt.FromDate).ToString("yyyy-MM-dd") + " hasta: " + TraVM.payment.PaymentDetails.Max(p => p.Debt.UntilDate).ToString("yyyy-MM-dd");
                    }
                }
                else                    //Producto
                {
                    Descuento = TraVM.orderSale.OrderSaleDiscounts.Sum(x => x.DiscountAmount);
                    //Anexo el año y periodo (Orders)
                    CondicionPago = "Año : " + TraVM.orderSale.Year + " Perido: " + TraVM.orderSale.Year;
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
                    //Date = DateTime.Now.AddMinutes(-5),
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
                        FiscalRegime = "603",
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
                Receiver receptor;
                if (TraVM.payment.OrderSaleId == 0)
                {
                    Client client = ms.Clients?.FirstOrDefault(x => x.typeUser == "CLI01");
                    if (client != null)
                    {
                        receptor = new Receiver
                        {
                            Rfc = string.IsNullOrEmpty(client.rfc) ? "XAXX010101000" : client.rfc,
                            Name = client.name + " " + client.lastName + " " + client.secondLastName,
                            CfdiUse = string.IsNullOrEmpty(this.msgUsos) ? "P01" : this.msgUsos.Split('-')[0].Trim()
                        };
                        msgCorreo = string.IsNullOrEmpty(client.eMail) ? "" : client.eMail;
                    }
                    else
                    {
                        receptor = new Receiver
                        {
                            Rfc = "XAXX010101000",
                            Name = "Público en General",
                            CfdiUse = "P01"
                        };
                        msgCorreo = "";
                    }
                }
                else
                {
                    if (tu != null)
                    {
                        receptor = new Receiver
                        {
                            Rfc = tu.RFC,
                            Name = tu.Name,
                            CfdiUse = string.IsNullOrEmpty(this.msgUsos) ? "P01" : this.msgUsos.Split('-')[0].Trim()
                        };
                        msgCorreo = string.IsNullOrEmpty(tu.EMail) ? "" : tu.EMail;
                    }
                    else
                    {
                        receptor = new Receiver
                        {
                            Rfc = "XAXX010101000",
                            Name = "Público en General",
                            CfdiUse = "P01"
                        };
                        msgCorreo = "";
                    }
                }

                cfdi.Receiver = receptor;

                //NODO: Conceptos
                //xml = xml + "<cfdi:Conceptos>";
                string TipoFactura = "", msgPagoParcial = "";
                List<Item> lstItems = new List<Item>();
                if (TraVM.payment.Type == "PAY04")   //Pago Anticipado.
                {
                    TipoFactura = "CAT01";
                    SOAPAP.Model.PaymentDetail PD = TraVM.payment.PaymentDetails.FirstOrDefault();
                    Item item = new Item()
                    {
                        ProductCode = TraVM.ClavesProdServ.Where(x => x.CodeConcep == PD.CodeConcept && x.Tipo == PD.Type).Select(y => y.ClaveProdServ).FirstOrDefault(),
                        IdentificationNumber = "S" + PD.CodeConcept,
                        UnitCode = PD.UnitMeasurement,
                        Unit = "NO APLICA",
                        Description = PD.Description,
                        UnitPrice = PD.Amount,
                        Quantity = 1,
                        Subtotal = PD.Amount,
                        Discount = 0,
                        Total = PD.Amount
                    };
                    lstItems.Add(item);
                }
                else if (TraVM.payment.OrderSaleId == 0) //Servicio
                {
                    TipoFactura = "CAT01";
                    decimal AdeudoTotal = TraVM.payment.PaymentDetails.GroupBy(g => g.Debt.Id).Select(g => g.First()).Sum(gg => gg.Debt.Amount) + TraVM.payment.PaymentDetails.Sum(x => x.Tax);

                    if (AdeudoTotal == TraVM.payment.Total) //Pago total
                    {
                        foreach (var pay in TraVM.payment.PaymentDetails.ToList())
                        {
                            //Calculo del unit price.
                            decimal tmpSubtotal = 0, tmpQuantity = 1, tmpValorUnitario = 0, tmpDescuento = 0;
                            if (pay.Debt.DebtDiscounts.ToList().Count > 0 && pay.Debt.DebtDiscounts.FirstOrDefault(DDis => DDis.DebtId == pay.DebtId && DDis.CodeConcept == pay.CodeConcept) != null)
                            {
                                var OriginalAmount = pay.Debt.DebtDiscounts.FirstOrDefault(DDis => DDis.DebtId == pay.DebtId && DDis.CodeConcept == pay.CodeConcept).OriginalAmount;
                                var DiscountAmount = pay.Debt.DebtDiscounts.FirstOrDefault(DDis => DDis.DebtId == pay.DebtId && DDis.CodeConcept == pay.CodeConcept).DiscountAmount;                                
                                tmpSubtotal = pay.Amount + DiscountAmount;
                                tmpQuantity = 1;
                                tmpValorUnitario = tmpSubtotal;
                                tmpDescuento = DiscountAmount;
                            }
                            else
                            {
                                tmpSubtotal = pay.Amount;
                                tmpQuantity = 1;
                                tmpValorUnitario = tmpSubtotal;
                                tmpDescuento = 0;
                            }

                            Item item = new Item()
                            {
                                ProductCode = TraVM.ClavesProdServ.Where(x => x.CodeConcep == pay.CodeConcept && x.Tipo == pay.Type).Select(y => y.ClaveProdServ).FirstOrDefault(),
                                IdentificationNumber = "S" + pay.CodeConcept,
                                UnitCode = pay.UnitMeasurement,
                                Unit = "NO APLICA",
                                Description = pay.Description + " Periodo de: " + pay.Debt.FromDate.ToString("yyyy-MM-dd") + " hasta: " + pay.Debt.UntilDate.ToString("yyyy-MM-dd"),
                                UnitPrice = tmpValorUnitario,
                                Quantity = tmpQuantity,
                                Subtotal = tmpSubtotal,
                                Discount = tmpDescuento,
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
                    else                                                                                 //Pago parcial
                    {

                        msgPagoParcial = "Esta factura es comprobante de un pago pacial.";
                        foreach (PaymentDetail pay in TraVM.payment.PaymentDetails.ToList())
                        {
                            //Calculo del unit price.
                            decimal tmpSubtotal = 0, tmpQuantity = 1, tmpValorUnitario = 0, tmpDescuento = 0;
                            if (pay.Debt.DebtDiscounts.ToList().Count > 0 && pay.Debt.DebtDiscounts.FirstOrDefault(DDis => DDis.DebtId == pay.DebtId && DDis.CodeConcept == pay.CodeConcept) != null)
                            {
                                var OriginalAmount = pay.Debt.DebtDiscounts.FirstOrDefault(DDis => DDis.DebtId == pay.DebtId && DDis.CodeConcept == pay.CodeConcept).OriginalAmount;
                                var DiscountAmount = pay.Debt.DebtDiscounts.FirstOrDefault(DDis => DDis.DebtId == pay.DebtId && DDis.CodeConcept == pay.CodeConcept).DiscountAmount;
                                var DescuentoProporcion = (DiscountAmount * pay.Amount) / (OriginalAmount - DiscountAmount);
                                tmpSubtotal = decimal.Round(pay.Amount + DescuentoProporcion, 2);
                                tmpQuantity = 1;
                                tmpValorUnitario = tmpSubtotal;
                                tmpDescuento = decimal.Round(DescuentoProporcion, 2);
                            }
                            else
                            {
                                tmpSubtotal = pay.Amount;
                                tmpQuantity = 1;
                                tmpValorUnitario = tmpSubtotal;
                                tmpDescuento = 0;
                            }
                            //Para evitar que pasen conceptos en cero.
                            if(tmpSubtotal > 0)
                            {
                                Item item = new Item()
                                {
                                    ProductCode = TraVM.ClavesProdServ.Where(x => x.CodeConcep == pay.CodeConcept && x.Tipo == pay.Type).Select(y => y.ClaveProdServ).FirstOrDefault(),
                                    IdentificationNumber = "S" + pay.CodeConcept,
                                    UnitCode = pay.UnitMeasurement,
                                    Unit = "NO APLICA",
                                    Description = pay.Description + " Periodo de: " + pay.Debt.FromDate.ToString("yyyy-MM-dd") + " hasta: " + pay.Debt.UntilDate.ToString("yyyy-MM-dd"),
                                    UnitPrice = tmpValorUnitario,
                                    Quantity = tmpQuantity,
                                    Subtotal = tmpSubtotal,
                                    Discount = tmpDescuento > 0 ? tmpDescuento : 0,
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
                    }

                }
                else                    //Producto
                {
                    TipoFactura = "CAT02";
                    foreach (var Or in TraVM.orderSale.OrderSaleDetails)
                    {
                        //Calculo del unit price.
                        decimal tmpSubtotal = Or.Amount + TraVM.orderSale.OrderSaleDiscounts.Where(x => x.CodeConcept == Or.CodeConcept && x.DiscountAmount == Or.Amount).Select(y => y.DiscountAmount).FirstOrDefault();
                        decimal tmpUnitPrice = 0;
                        Item item;

                        //Este codigo quedara parchado con este If, hasta corroborar que todos los order vengan con su calculo correcto en BD.
                        //Pero ya deberia poderse identificar el descuento con el OrderSaleDetailId.
                        if (TraVM.orderSale.OrderSaleDiscounts != null && Or.CodeConcept == "3109")
                        {

                            if ((Or.UnitPrice * Or.Quantity) == tmpSubtotal)
                            {
                                item = new Item()
                                {
                                    ProductCode = TraVM.ClavesProdServ.Where(x => x.CodeConcep == Or.CodeConcept).FirstOrDefault().ClaveProdServ,
                                    IdentificationNumber = "P" + Or.CodeConcept,
                                    UnitCode = TraVM.payment.PaymentDetails.Where(x => x.CodeConcept == Or.CodeConcept && x.Amount == Or.Amount).FirstOrDefault().UnitMeasurement,
                                    Unit = "NO APLICA",
                                    Description = Or.NameConcept,
                                    UnitPrice = Or.UnitPrice,
                                    Quantity = Or.Quantity,
                                    Subtotal = TraVM.orderSale.OrderSaleDiscounts.Where(x => x.OrderSaleDetailId == Or.Id).Count() == 0 ? Or.Amount : TraVM.orderSale.OrderSaleDiscounts.Where(x => x.OrderSaleDetailId == Or.Id).Select(y => y.OriginalAmount).FirstOrDefault(),
                                    Discount = TraVM.orderSale.OrderSaleDiscounts.Where(x => x.OrderSaleDetailId == Or.Id).Select(y => y.DiscountAmount).FirstOrDefault(),
                                    Total = Or.Amount + TraVM.payment.PaymentDetails.Where(x => x.CodeConcept == Or.CodeConcept && x.Amount == Or.Amount).FirstOrDefault().Tax
                                };
                            }
                            else //para los dobles descuentos.
                            {
                                decimal tmpDescuentoAjustado = (Or.Quantity * Or.UnitPrice) - Or.Amount;
                                item = new Item()
                                {
                                    ProductCode = TraVM.ClavesProdServ.Where(x => x.CodeConcep == Or.CodeConcept).FirstOrDefault().ClaveProdServ,
                                    IdentificationNumber = "P" + Or.CodeConcept,
                                    UnitCode = TraVM.payment.PaymentDetails.Where(x => x.CodeConcept == Or.CodeConcept && x.Amount == Or.Amount).FirstOrDefault().UnitMeasurement,
                                    Unit = "NO APLICA",
                                    Description = Or.NameConcept,
                                    UnitPrice = Or.UnitPrice,
                                    Quantity = Or.Quantity,
                                    Subtotal = decimal.Round(Or.Quantity * Or.UnitPrice, 2),
                                    Discount = tmpDescuentoAjustado,
                                    Total = Or.Amount + TraVM.payment.PaymentDetails.Where(x => x.CodeConcept == Or.CodeConcept && x.Amount == Or.Amount).FirstOrDefault().Tax
                                };
                            }
                        }
                        else
                        {
                            if (Math.Round(Or.UnitPrice * Or.Quantity, 2) == tmpSubtotal)
                                tmpUnitPrice = Or.UnitPrice;
                            else if (Math.Round(Math.Round(Or.UnitPrice / Or.Quantity, 2) * Or.Quantity, 2) == tmpSubtotal)
                                tmpUnitPrice = Math.Round(Or.UnitPrice / Or.Quantity, 2);

                            if (tmpUnitPrice != 0)
                            {
                                item = new Item()
                                {
                                    ProductCode = TraVM.ClavesProdServ.Where(x => x.CodeConcep == Or.CodeConcept).FirstOrDefault().ClaveProdServ,
                                    IdentificationNumber = "P" + Or.CodeConcept,
                                    UnitCode = TraVM.payment.PaymentDetails.Where(x => x.CodeConcept == Or.CodeConcept && x.Amount == Or.Amount).FirstOrDefault().UnitMeasurement,
                                    Unit = "NO APLICA",
                                    Description = Or.NameConcept,
                                    UnitPrice = tmpUnitPrice,
                                    Quantity = Or.Quantity,
                                    Subtotal = Or.Amount + TraVM.orderSale.OrderSaleDiscounts.Where(x => x.CodeConcept == Or.CodeConcept).Select(y => y.DiscountAmount).FirstOrDefault(),
                                    Discount = TraVM.orderSale.OrderSaleDiscounts.Where(x => x.CodeConcept == Or.CodeConcept).Select(y => y.DiscountAmount).FirstOrDefault(),
                                    Total = Or.Amount + TraVM.payment.PaymentDetails.Where(x => x.CodeConcept == Or.CodeConcept && x.Amount == Or.Amount).FirstOrDefault().Tax
                                };
                            }
                            else              //Este metodo lo aumente porque comenzaron a mandar bien la info en BD y por tanto ya puedo tomar los datos directos de BD.
                            {
                                item = new Item()
                                {
                                    ProductCode = TraVM.ClavesProdServ.Where(x => x.CodeConcep == Or.CodeConcept).FirstOrDefault().ClaveProdServ,
                                    IdentificationNumber = "P" + Or.CodeConcept,
                                    UnitCode = TraVM.payment.PaymentDetails.Where(x => x.CodeConcept == Or.CodeConcept && x.Amount == Or.Amount).FirstOrDefault().UnitMeasurement,
                                    Unit = "NO APLICA",
                                    Description = Or.NameConcept,
                                    UnitPrice = Or.UnitPrice,
                                    Quantity = Or.Quantity,
                                    Subtotal = Or.Amount + TraVM.orderSale.OrderSaleDiscounts.Where(x => x.CodeConcept == Or.CodeConcept).Select(y => y.DiscountAmount).FirstOrDefault(),
                                    Discount = TraVM.orderSale.OrderSaleDiscounts.Where(x => x.CodeConcept == Or.CodeConcept).Select(y => y.DiscountAmount).FirstOrDefault(),
                                    Total = Or.Amount + TraVM.payment.PaymentDetails.Where(x => x.CodeConcept == Or.CodeConcept && x.Amount == Or.Amount).FirstOrDefault().Tax
                                };
                            }

                            //Si mandan el concepto en ceros.
                            if (item.UnitPrice == 0 && item.Subtotal == 0 && item.Total == 0)
                            {
                                item.UnitPrice = 0.01M;
                                item.Subtotal = 0.01M;
                                item.Total = 0.01M;
                            }

                        }
                        if (Or.HaveTax == true)
                        {
                            List<Tax> lstTaxs = new List<Tax>() {
                                new Tax()
                                {
                                    Base = Or.Amount - TraVM.orderSale.OrderSaleDiscounts.Where(x => x.CodeConcept == Or.CodeConcept && x.OriginalAmount == Or.Amount).Select(y => y.DiscountAmount).FirstOrDefault(),
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

                //Se solicita una obsevacion para cada factura.   
                if (!string.IsNullOrEmpty(pTipoUso))
                {
                    cfdi.Receiver.CfdiUse = pTipoUso;
                }
                else
                {
                    if (showMsgObservacion)
                    {
                        using (msgObservacionFactura msgObs = new msgObservacionFactura())
                        {
                            msgObs.ShowDialog();
                            msgObservacionFactura = msgObs.TextoObservacion;
                            msgUsos = msgObs.Usos;
                            msgCorreo = msgObs.tbxCorreo.Text;
                            enviarCorreo = msgObs.chbxEnviarCorreo.Checked;
                        }
                        cfdi.Receiver.CfdiUse = this.msgUsos.Split('-')[0].Trim();
                    }
                }
                
                //En caso de tener descuento de grupo vulnerable
                var tipoDescuento = "";
                if (TraVM.payment.OrderSaleId == 0)
                    tipoDescuento = TraVM.payment.PaymentDetails.Where(PD => PD.Debt.Discount != null && PD.Debt.Discount != "").Select(Sel => Sel.Debt.Discount).ToList().Distinct().FirstOrDefault();

                if (!string.IsNullOrEmpty(tipoDescuento))
                    msgObservacionFactura += msgObservacionFactura != "" ? ", Descuento de grupo vulnerable (" + tipoDescuento + ")" : "Descuento de grupo vulnerable (" + tipoDescuento + ")";

                //En caso de factura fuera de fecha
                bool printFecha = Variables.LoginModel.Divition != 12;
                if (Variables.Configuration.IsMunicipal)
                {
                    if (TraVM.payment.PaymentDate.ToString("yyyy-MM-dd") != DateTime.Today.ToString("yyyy-MM-dd") && printFecha)
                        msgObservacionFactura += msgObservacionFactura != "" ? ", Pago efectuado el " + TraVM.payment.PaymentDate.ToString("yyyy-MM-dd") : "Pago efectuado el " + TraVM.payment.PaymentDate.ToString("yyyy - MM - dd");
                }
                else
                {
                    if (TraVM.payment.PaymentDate.ToString("yyyy-MM-dd") != DateTime.Today.ToString("yyyy-MM-dd"))
                        msgObservacionFactura += msgObservacionFactura != "" ? ", Pago efectuado el " + TraVM.payment.PaymentDate.ToString("yyyy-MM-dd") : "Pago efectuado el " + TraVM.payment.PaymentDate.ToString("yyyy-MM-dd");
                }
                //Si es un pago parcial.
                if (!string.IsNullOrEmpty(msgPagoParcial))
                    msgObservacionFactura += msgObservacionFactura != "" ? ", " + msgPagoParcial : msgPagoParcial;

                //Si hay observaciones en la Orden o el debt                
                if (TraVM.payment.OrderSaleId == 0)
                {
                    if (!string.IsNullOrEmpty(TraVM.payment.PaymentDetails.FirstOrDefault().Debt.observations))
                        msgObservacionFactura += (msgObservacionFactura != "" ? ", " + TraVM.payment.PaymentDetails.FirstOrDefault().Debt.observations : TraVM.payment.PaymentDetails.FirstOrDefault().Debt.observations);
                }
                else if (!string.IsNullOrEmpty(TraVM.orderSale.Observation))
                    msgObservacionFactura += msgObservacionFactura != "" ? ", " + TraVM.orderSale.Observation : TraVM.orderSale.Observation;
                //Mensaje por promocion COVID
                if (ms.id != null && Variables.Configuration.RecargosDescCovid != null && ms.id != "0")
                {   
                    Variables.Configuration.TotalDescuentoCOVID = await VerificaDescuentoPorCOVID(int.Parse(ms.id), TraVM.payment.PaymentDetails.Select(x => x.Debt).ToList());
                    //Se corrobora que hay a un monto de descuento, si no lo hay, no se agrega ningun mensaje.
                    if(Variables.Configuration.TotalDescuentoCOVID != 0)
                    {
                        string MensajeObservacionFactura = Variables.Configuration.RecargosDescCovid.FirstOrDefault().Split('|')[1];
                        MensajeObservacionFactura = string.Format(MensajeObservacionFactura, Variables.Configuration.TotalDescuentoCOVID.ToString());
                        msgObservacionFactura += msgObservacionFactura != "" ? ", " + MensajeObservacionFactura : MensajeObservacionFactura;
                    }                    
                }
                    

                cfdi.Observations = msgObservacionFactura;

                string path = GeneraCarpetaDescagasXML();
                string nombreXML = string.Format("\\{0}_{1}_{2}.xml", issuer.Rfc, receptor.Rfc, seriefolio);
                string nombrePDF = string.Format("\\{0}_{1}_{2}.pdf", issuer.Rfc, receptor.Rfc, seriefolio);

                bool TimbroFacturama = true;
                object[] vs = solicitarTimbradoAProveedores(cfdi, path + nombreXML);
                if (vs[0] == null)
                {
                    TimbroFacturama = false;
                    if (((string)vs[1]).Contains("El cálculo") || ((string)vs[1]).Contains("RFC"))
                    { }
                    else
                    {                       
                        vs[0] = await timbrarProvedorSecundario(cfdi);
                    }
                }
                if (vs[0] != null)
                {
                    //cfdiFacturama = (Facturama.Models.Response.Cfdi)vs[0];
                    cfdiFacturama = new ModFac.ResponseCFDI((Facturama.Models.Response.Cfdi)vs[0]);
                    string XML = LeerXML(path + nombreXML);
                    TaxReceipt resGuardado = await guardarXMLenBD(XML, cfdiFacturama.Complement.TaxStamp.Uuid, receptor.Rfc, TipoFactura, status, TraVM.payment.Id, cfdiFacturama.Id);
                    string resActPay = await actualizarPaymentConFactura(TraVM.payment, resGuardado);


                    CreatePDF pDF = new CreatePDF(cfdi, cfdiFacturama, ms.account, resGuardado, fecha, (TraVM.payment.PayMethod.code + ", " + TraVM.payment.PayMethod.Name), TraVM);
                    pDF.UsoCFDI = string.IsNullOrEmpty(msgUsos) ? "P01 - Por definir" : msgUsos;
                    pDF.SerialCajero = pSerialCajero;
                    pDF.ProveedorServicioFacturacion = TimbroFacturama ? "ESO1202108R2" : "IAD121214B34";
                    if (TraVM.payment.OrderSaleId == 0)
                    {
                        pDF.ObservacionCFDI = msgObservacionFactura;
                        //pDF.ObservacionCFDI = (string.IsNullOrEmpty(msgObservacionFactura) ? "" : msgObservacionFactura);
                    }
                    else
                    {
                        pDF.ObservacionCFDI = msgObservacionFactura;
                        //pDF.ObservacionCFDI = (string.IsNullOrEmpty(TraVM.orderSale.Observation) ? "" : TraVM.orderSale.Observation + " - ") + (string.IsNullOrEmpty(msgObservacionFactura) ? "" : msgObservacionFactura);
                    }
                    string resPdf;
                    if (TraVM.payment.OrderSaleId == 0) //Servicio
                        resPdf = await pDF.Create(path + nombrePDF, TraVM.payment.AgreementId);
                    else
                    {
                        TraVM.orderSale.TaxUser = tu;
                        resPdf = await pDF.CreateForOrder(TraVM.orderSale, path + nombrePDF);
                    }

                    //return "Success -"+ cfdiFacturama.Complement.TaxStamp.Uuid;
                    return resPdf;
                }
                else
                {                    
                    string error = (string)vs[1];
                    //return "{\"error\": \"El cobro fue registrado, pero no se genero el CFDI. Detalles: "+error+"\"}";
                    return "error: El cobro fue registrado, pero no se genero el CFDI. (" + error + ")";
                }
            }
            catch (Exception ex)
            {

                return "{\"error\": \"Error al intentar realizar el timbrado, favor de comunicarse con el administrador del sistema: " + ex.Message + "\"}";
            }
        }

        public object[] solicitarTimbradoAProveedores(CfdiMulti cfdi, string path)
        {
            object[] cfdistatus = new object[2];
                        
            try
            {
                //Facturar con Facturama.
                Facturama.Models.Response.Cfdi cfdiCreated = facturama.Cfdis.Create(cfdi);                
                facturama.Cfdis.SaveXml(path, cfdiCreated.Id);

                cfdistatus[0] = cfdiCreated;
                cfdistatus[1] = null;
                return cfdistatus;
            }
            catch (FacturamaException ex)
            {
                string error = "";
                if (ex.Model != null && ex.Model.Details != null && ex.Model.Details.Count > 0)
                {
                    foreach (var messageDetail in ex.Model.Details)
                    {
                        error += "(" + messageDetail.Value.Aggregate("", (current, next) => current + ", " + next).Substring(2) + ") ";
                    }
                }
                else
                    error = "Detalle: " + ex.Message;

                cfdistatus[0] = null;
                cfdistatus[1] = error;
                if (error.Contains("El cálculo") || error.Contains("RFC"))
                    return cfdistatus;                
            }
            catch (Exception ex)
            {
                cfdistatus[0] = null;
                cfdistatus[1] = "error: " + ex.Message;                
            }
           
            return cfdistatus;
        }

        private string RegitraEmisor()
        {
            try
            {
                Csd csdRequest;
                if (Variables.Configuration.IsMunicipal)
                {
                    //csdRequest = new Csd
                    //{
                    //    Rfc = "MCP850101944",
                    //    Certificate = "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",
                    //    PrivateKey = "MIIFDjBABgkqhkiG9w0BBQ0wMzAbBgkqhkiG9w0BBQwwDgQIAgEAAoIBAQACAggAMBQGCCqGSIb3DQMHBAgwggS+AgEAMASCBMjAKMA5RciD3fjDSzGOAurQLiD+uPmCs3a9Z8GyjHfzIvOd7aQ+XBoyUMRj81nCxHK9A1ScbP/5C0igsVnIj+s8e+iirokRQhRZwFlbAypvgXtAvIcTGU2pYr7XoPRigbFdYDGx9J1zfZWuvBLrwpbKNXdbqLshnRQvOlBM8eWkJgheR26Xv+ROdSrsKOY05cda7wdVc+2CR4kr/GnQYYCTooXhvgS7AoJsyuDdLvN57t/NgtOplYktjwO5FTqKqzTc8J0Lb1DcZh1UHsFuGbmHwLdzqE+uw5I6l0q1+h/UJlhTofLlQ7Y9+YgxHSLuxpRAQ7vJSFMTP7nITn+4GtD4rnYBLWt/bP0XXSwYLW7B2ofYNepz5/IDhawshyFNo+cg/aqWUICKF0zhDrQm499VGYSRIHS13pICSg7aSbeQ6XDcXGDU7bIwf0/Bsspnqw+deP8JplI6DPhCXKnOnybCn0qnTag+2Q86Tnskcv5t0x+6NUOuoEB2/TVCVWFYobgRWyRH0Eq/hOm3ywV095qOO74yXcK291QUOEfA0v1OmXzsD4KX47Wr7K4rYAX7FYahE7nHdya0vWvNGTth/XDdopuoLKKY5X+9NkuBt3YYohiF7d6ZsRKwczfQV3MEwhd2Wvm1ADh1SOR2n4BdtBApNoReiMSnZ76vhc2ynuJkX35cA1ofKTsWZLJ4GCpV3MN1vVQFLlpy7S7QGf/ER1t6VfFHafayp4t/UY+K6hxn4Fjfu5eqo3SQQ2o6Vsh+Qqm/6txA5ht1B0peI7vJ6AWGz5L5D6e30nUDg/aQPyCUt0r+4pgRXFUxTRrWpQ6xmnB9M1fcvjzr17CSudTJlJ35b5hGXoIGbd7tWh5kSeoUlpAycu15JIiRIt9LVaHHr49vceSgj71m+O9+k5PCZi1449oVRiXmjzyniHZ2orMdy9eTvUWP/Dobx6QBACC1eSHLag2sDnzeHi676aAOzH8K/34jH7ZyMblDD4TSWh0irPbcF1hIBIFhtJrPlLs34ceZKlPh3cYH5/e5tDSlAcoYq4TJuhn1tZSN4992kJM5y30C1bgYg2C5pZ7XfO+z/uTw6Hl+QhckVSJgKs2KY+U77loqbuP7OIWEzAot/wCUEh66l0/MUPHaCf08GYp1T0iZszTRAtc8+ORqWwnSqKDl09Vb3FszlaYD1fanPARQhTcyXr16ALS/9jkN4pguV+hkq86pPAuJy8tL2491I3jGWI76BAoa5cooOls9XAHPholIwGiChuEoIcAJ3I0SHXTnELI13lu1e7VNUQR4hTWBbh1tc0OnjPfdfUkWtgLQcQ7ONg5EzYLR9b9G6xHrP01QunUiD3Y7ZJDTjFhsuvuWi6RvZ5h/UTdupzH4eWNR+91obsIHS6c3z8uVLXG44uUnl8IXn9c1G8q+LdPXFLSF+Q7+n0yVrGBaSoqDPjPwRa1eWBNc55TCjs9DZLPaB+xdz860NWuFmGNhAzviA8aJhFZ/jQg8Eu2lHaJgcoXluwc3Dm5A2bZG7tpLACXK77ezI1T4ffs0RcKkf/cOHNWcU7jVeM9n7NeeEBvesrPvqsuhJ/xIodFJsG/ZDwkhvseKOX04r3wPmmhIiY18/gxr5h/auEJgfh4=",
                    //    PrivateKeyPassword = "Cuau1312"
                    //};

                    //18-Sep-2019
                    csdRequest = new Csd
                    {
                        Rfc = "MCP850101944",
                        Certificate = "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",
                        PrivateKey = "MIIFDjBABgkqhkiG9w0BBQ0wMzAbBgkqhkiG9w0BBQwwDgQIAgEAAoIBAQACAggAMBQGCCqGSIb3DQMHBAgwggS9AgEAMASCBMgoaLzpMOsUTfA/0PzRE4Kf0I9nxh3rD2W67YVl9iNriXT+XVe2DdV+u/3co21S+GF4TX5utqYjNUeT/R0jMcAJtAia0GWYyctlrX6pcLVRUqqSKCJ2LR9+VO4u5gs3t/Ch56YxjFCI091i5bLJbc44DwCmbs2SibRQMEXM6qC+6x0E9yuSXBzpmiZ99QvWok8rEao3Pik5ICMQ21iC16zArIZSLIPKUH9Bgg3fZchn3R01KnzAmc4AnAe97pdqnnC5SUlV4CykS6i615hlUo5Q54t6FnL77qepr/WGvCaBCh5OycGF2M424IVCLm7FNWuFKzUWHkvbHnvDWghVua9vkFPrD0jyMc/xew0Mofl3jrybJobdFINqYKCJl1S7U3+FRuzWrEwqSAs6+lRhArBHxTWVKE3wQzSaSG+dZPq9q584/+ooqyNttwWNiC8/dvZPOmhCmya+Rt80dxoy92yBNgRSkqbq45Ltm2PEQP95V1NjVxkWUTUEJLV95Fq66l/170wHKsHUI9wUb9WwBJKbu6EyInMQzql6SOsdybYAi0oH6sG3xJQJVBdKeTWSASOnv3OvyZ+q4ehijQldoHQjPrTJU7gvOO01cRxXrHeEy4OpZaE6UncUYEdqFhIpcQwK6EMP3FD/x7Sj2O0LOjT3/RQCsNXmCU7DDFS7/BlaM7SdrI2zCkk/e0TsldHkoT9WzH0Mbrod6byWe69s/ENftkFBIWZ8/Pmd9qE93yk7fL665TZyguBIRCU6yofm5oW7Sz27PodUW/s+xNocZg7LSnfG4cJAHCIARcG+r2Ifx7/xkP99bmHIdbAkriiiwakkERMtgypILuAGCRLQDBzngEY67REMyoYeuOzT04nArBrgvR2NDBN4Wa05eM81+4p6fsSgkheIBJ/UukFTnxqh50nMoodA0uJ+swhCh8G6mO3a1Ea5ysqhjVEbju+xSDtROaDUk6IhUNk084ie8OfDDxKDHOuZvFi/rFCWTq0zhnUwIdLoiwSPjCcGyiZpbi9rxUCJl47pkVBfLgtyvbpn+8Bq3ngR/Y882FTRTGSJNKCfgnWZm///KWORh3I0l8qODYyiLlRw3qlLKCFJbgojiFDI65116bQPnlXysTCfAPlWWowsw13LvymdPDdXI+FhlK/eJR1dHHqIJw6x8F/rr1HvbV/jqbo0B0RDseyNDKbvZgst99V1dcbLTJ6J6+KBK+kJp8br7VBkoTKwZSi5Lrr43Lmuiw7YjXZl1WN2Tk2kjjtpeT66Az5yUjKRW5YHE3n/sDAzGN/wqo57ouUEHNjdPaez8sBvGa0KCTEb0PKxdbNZMUAs24pxudrDDKiFvPSJNd5W2w2W9CBT9jbuvjlPt6xPjwskuHYZ8XnyGHvZGZNHYzfhsqZafk0dAQbjinUMWKOmaC7vayTx1pQwe8zuaDp4MT1aPyXAmabUcPGt+xhBLcMDjs7FLZb1RYcP6iUsCZiS7C9c2Jp2/QPBnpRIydCfdKCQK1f7R1HmqadsUikD+LXdV4gaJeKF0EoyVlkEku1DkR67yQV+1IIxNRNWFkjpkWUHD2IFPXpdRXiL8BQyUobUm1n5zA4j72TIXcTrWVvc0T46vy6Khu747k9fTgdLiLQ=",
                        PrivateKeyPassword = "CUAUTLANCINGO1821"
                    };
                }
                else
                {
                    //csdRequest = new Csd
                    //{
                    //    Rfc = "SOS970808SM7",
                    //    Certificate = "MIIGxzCCBK+gAwIBAgIUMDAwMDEwMDAwMDA1MDA0NjU2NTIwDQYJKoZIhvcNAQELBQAwggGEMSAwHgYDVQQDDBdBVVRPUklEQUQgQ0VSVElGSUNBRE9SQTEuMCwGA1UECgwlU0VSVklDSU8gREUgQURNSU5JU1RSQUNJT04gVFJJQlVUQVJJQTEaMBgGA1UECwwRU0FULUlFUyBBdXRob3JpdHkxKjAoBgkqhkiG9w0BCQEWG2NvbnRhY3RvLnRlY25pY29Ac2F0LmdvYi5teDEmMCQGA1UECQwdQVYuIEhJREFMR08gNzcsIENPTC4gR1VFUlJFUk8xDjAMBgNVBBEMBTA2MzAwMQswCQYDVQQGEwJNWDEZMBcGA1UECAwQQ0lVREFEIERFIE1FWElDTzETMBEGA1UEBwwKQ1VBVUhURU1PQzEVMBMGA1UELRMMU0FUOTcwNzAxTk4zMVwwWgYJKoZIhvcNAQkCE01yZXNwb25zYWJsZTogQURNSU5JU1RSQUNJT04gQ0VOVFJBTCBERSBTRVJWSUNJT1MgVFJJQlVUQVJJT1MgQUwgQ09OVFJJQlVZRU5URTAeFw0xOTA2MjkxODE2MTlaFw0yMzA2MjkxODE2MTlaMIIBlDFJMEcGA1UEAxNAU0lTVEVNQSBPUEVSQURPUiBERSBMT1MgU0VSVklDSU9TIERFIEFHVUEgUE9UQUJMRSBZIEFMQ0FOVEFSSUxMQTFqMGgGA1UEKRNhU0lTVEVNQSBPUEVSQURPUiBERSBMT1MgU0VSVklDSU9TIERFIEFHVUEgUE9UQUJMRSBZIEFMQ0FOVEFSSUxMQURPIERFTCBNVU5JQ0lQSU8gREUgQ1VBVVRMQU5DSU5HTzFJMEcGA1UEChNAU0lTVEVNQSBPUEVSQURPUiBERSBMT1MgU0VSVklDSU9TIERFIEFHVUEgUE9UQUJMRSBZIEFMQ0FOVEFSSUxMQTElMCMGA1UELRMcU09TOTcwODA4U003IC8gVEVQSDc4MDgyMDFLMTEeMBwGA1UEBRMVIC8gVEVQSDc4MDgyMEhQTFBMRzEzMUkwRwYDVQQLE0BTSVNURU1BIE9QRVJBRE9SIERFIExPUyBTRVJWSUNJT1MgREUgQUdVQSBQT1RBQkxFIFkgQUxDQU5UQVJJTExBMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApWcOeIKbhag1Cvx7sj1UWURZAX2myt6qMsj0X9Oo6uvrGHi5rcn7wmAGkFpLAM4uO20o6lWH7ebe20Qj0XG+Xm3FH8rvz+Q+bx5aUMOo8jpECeIPili6BZqdFJje0+HXBaT8mAkVo+dVjS+I6j3i5jSwTZwh4HtZn1Q3DhlHma5fEAZypClVsgfRPQyr3IK3clO4KrrmzvOUV81B9Djs1iBhELXsR7Jevc/YFg3S5ijiaB9ZzbwkmkV/ko9IGafmusxbY/hoq42Pgi5pewCPhpFNVlmJ3KCHrAWPQSk18/85qzM2K2ePwfdcB124Zmwnoh8xgUjgjIE4QVS0GNVpIwIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEAb51YhK1M5EwsaayXz13krtmM6Xmr+CeICFFdF46ObXq+45KpWY8bBxP0yj6CqQ90ya8/QPXWMdPm0CiqvJ17B5ZJf7sV1hhYOfP/AZyHqQgcEQVzlV03z0jm6LayBow3OhQ66ElYof9BItCxN8H6IOZ6hJYiCwzOZAPZT4cYHqWMbnCNxvrZI861mI2pUX0cNCSGkfoREeEIbavwtKqjiaaxLCDNxHM5Zn7WlwquVW7JmDmrYf0JQCZ4MFk36TOPaxv8ExS1loyk1C23MOuvUvmjASZ+ax3/xUxalaNmo2/9o24naby/Pm639RT9hlmEdqkn+nSGcSMXkpD73pyFUWQceDJd4Yri8YTPe+lH4rX//eONktsrgZX40+tNF6tbjdE0qxxuxGPv2Z2QoZsAAjHKcMcQ4uZ7SnrylFV7sNPo5X711G3BRmCAGlwlt9P5N2L3akpzJkPxEabPe/E2PdUggtQdfBm8nBASQNocX4o2RTKNCGZkxrZ30B4g3yGE9SmCqyRMKPo0Bck2rOHDmcIQ/4XRXMLiIy0FqM7zkVj8sj6s0dyqzDHRYfkgwQ3t7/dgtW/0iiw/iNsHT2uXlMM89lO7SGz3xq5e3Q2Krg82eyjiMEtiBtD5Jn33t7b3T6uQ0yfkqP1W1neNZsAqs5PVXIEPJCmw9uUG6RV5Xmk=",
                    //    PrivateKey = "MIIFDjBABgkqhkiG9w0BBQ0wMzAbBgkqhkiG9w0BBQwwDgQIAgEAAoIBAQACAggAMBQGCCqGSIb3DQMHBAgwggS8AgEAMASCBMgIyBKWyh87HbNoYd5uz/91TmoajU3gwm8ZQ9gw4nOhT8/IDym4S4wlvqJG7Dc//WzcLYDQfE/V1SrW5knLw7ja/72pUG1kZ3wRBWQDJxdzbqVSbAmY43GXJRddL33ll0RnNZ8FBusel/RHtFHg+AiuPgZVQqKJz5sJKyDUCJPbpi7Rl2uuIy1BKlLWnFo3ojUZa5ji0A/rFg9ZKUWJmO3HRsJpxLmnP/pcSikMhlnpYhzeRRaZg2gs9MMluYaIDM5mP33EamTYBfMlSF6XgyCl8t22/9fQ+q3u70jlC0Ckqniw3Fp9MmvwCX1Ew78MFugaXL2xadVDf2e3I/eW+I/Hi1VeaGzdCO6+xBCpbrc0YUVEeYe42r0BbpEz/gDHTnB79DGTF8GshwxI/dcPAzg/KucZvDv3phL0qfrTykbpTbJS1Tb7Ei2DP+GMzRO7RYb4R9E81fiwhnOInGeOdQBTtINJzhIBtwAhO3sCxm7Qx2RUPle2iYDcFNRbK7iON/AGzZUN0Qol2HYUJwjMimFTKVIfLe2sa56jqCeJY1OdeIjfxfPVhSnMnADaq9lTRCylNOMtxHPJR/lsrk4LbRC6Ll5dTqQ37vTsd8yUzLlRF/ZQm0pmNgzIvoAJaYCWI6fytXf0A/Ehxzs73/ODMJTzlfOpiyElk7Q0Z6X8IVzjEnKRf7vkKx3d6vEiXAZW2nEAXhKhbsImrTLJtn01uyldFpU3aEadgTYk0HUh7Z9sPhmmbPoyMp6/d1tDV22Zfb29IOUY6kG3teZCOFtW+SkLy6fRDEKTVevwWvHHjDakNReP1Oki6VIl9yr5+QjD407n1bwlPaufNkQ2yLLBiZs7yhQbQlBeRupB8ic1iUCOydzIA4FBcQz7wYQ6Hh4Q86rLFMyeBxdiaM6WhvWPTkFMpJpbTDmwSLNU5A44nSmHbmJFdQP/y2jBIyXnC4HNUgT6iCSNXbtMfLMhSpvmf+jAzkbG4L/+5Hie3QAVph2YbrIZVDX5HIEoS7p1I6+J8wfa/sSoXsPxZl5xqL4qLIgyC7b0dhaQ/EcLNmeGaE/SkSzxrlh79p2nQyNGaSBMFfteF0g+e8EmRzllXUyGf3r+LAhWhoH8roB5T9gyVBVlIz/bneCHg5qhZHlbeiJlDtUEOJXElgOxNGVu1MrWhk7FE2o2vEDMVVFxk4MoDiMVLBl4ux0eKaCAovk+WBqehw3/YJP24XrMX7Os5W8LjHZIBp9nFC4GwGWsHQfJxvFI4qMesCq3UW/ieq8/NatIt353yvYV1U5Br14hXAk2E2mLeDdAl4e2R4RcvFOzpAgMnxbVYAoXE11BD0HKF9+BiPHu25IP5dw0QRC7+9IMBd5SGD+wOBlXCD4XFg26AX8jZRMIYzxjVOAgRe6b6i84xJ83NlvaebuyV7TyMhhvMGCt6QEPwIIxQOl1b2TLXMqMPLUn9ZDr8SI0ZmRLavVIAVIP7ER6psl5R1Aps5O1SeAunZ4Vmg67LrlGV/uorH4KhkzakvWte9bRjqT1tX5N0zN0EN/755F3NsCWuPTivGlSunqsfZNqkKqE4UgIDPPPCQHZkgN5bVarJJY8stPdnzfNiP94XAYDl2xQzqpjX3ucgZIF2lcNQEA=",
                    //    PrivateKeyPassword = "htepox1978"
                    //};

                    //12-Sep-2019
                    csdRequest = new Csd
                    {
                        Rfc = "SOS970808SM7",
                        Certificate = "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",
                        PrivateKey = "MIIFDjBABgkqhkiG9w0BBQ0wMzAbBgkqhkiG9w0BBQwwDgQIAgEAAoIBAQACAggAMBQGCCqGSIb3DQMHBAgwggS9AgEAMASCBMi3vyxxcpY1iyNMbAoWLAN8/rTlwXx9Gqfqc8kGtIFmgC4qCaISSDrh2anw3cg7QbPLj49xyn/BMqRZ0V5yfSmTaXXX82RpSanuB7C+FjBwoueasPmhtGEf2ZGrLT5Gw4oNofEtuDs6ExX6VdKcUQqVcNzKQbStizkmGdqAueVpWKd68Yv0Vw5mTbF0zqwen9P0mXS/QVFGReVZ6YCIz5zCLoMrapbWQau9dzZuu/qVOAoQog9Aj+1emeWnFqubeRb5ixLI0lqPSjh+83tF1UEC0umkpBiIJKTa0j0NHSF2YB+PQV/xsCNAI9gJ4pOcrpp49ZulUoGQOc/Tb/f/7dOTJ5x0HxocEQroq9mSALGqLGhNOlgK1AM7WUxIaSY5IWu1ykBlxwxIb/vEZpMfUtj5vY+UaV17FqC7Ulb45FSFw4SDI2nMQhujpjRd4sWywNWSQkokoO4nH/nE9wehbnMv40ySGY2t6ysBZzIjuXbE+xaMfdaVWIOM4F9A46mm06NnsXg4PJmybgsO93kE9Y16GmRXxhYK0H7II2YwVQj/COB2CDEUbPLZm3qdcHosud43WIYDrtSsrJgRIvZD4BhT452RrsBx0ndRd+joC8/ELj9yd4XDyDH/5JkUy8vdmsARGiJ6TjoeQQyTo2tu5YnKRjmWpjLsz08zAbI3zAbjDqBXpcQLOirmmqZDOHCBh9S+Wp2hx3bcwHr0yOCOJ0VCPhSLY3QYR4TNLV+TMiLkkYWDeAmHgSvQmh+YDCIjeNVStPovaYQyr0FqT1Uej/Sp1sXMJso9ZXqQlMkL8p3N7dREW0C3auyHgvkAcbCUV2TIAYZNOrKB5UjnKuIFTSSlk+CVGhxDgSX9RpR7PMvVPkoLqeV6twQF3scV9L1iUBl65FOD5gr9MU6khZObaONylGF9hYqODIDBWbJvIlgE4A7muNZ2seRSnceY/CUumUCKJybfBmd4Uhl5sgrGvJoabefCXfacOy6Ebp0iI5pgpEz1S0ZFrnr9SY8PAvHMs0BGAzxVOeJoN1iEJSEtCXVTWbGhOPWmE6hJ8xYjqRjwMQEuEMwYMr0bQh192Kfdp/e27F2lzHHX3yYe2Bh1IUNVzn60eiqGytWzHdhmG+KjoCXc3C3cAg+aK8wLXZDMP9u1Si9xdS6oyRE5UYjlYq0u+aV3zf06v8sE2307uTsdIW9SyAe4BRlpFzhuzIQerjshlngUtE0jCnGR2P1imjn7Z5houWQU/ZwE3BhVMrhQFpkUkGVHT9xyVasMEORnsZmiXSMvq10ZKTj81UadFM73iJ9IL3l82JeXBd2n5Pn1i6vFD+DPovDc9qF6ps7RKaZWPD9NQp0urtmxdBPoJV9HQqq5mxodqmi/6SF9b4MfEHpxlvDrRSQ525cM9LkXEiOXITRP0p07TZ+0vRS4aE2Dd0BFT0d4RTKRpMEKXiSCIS8XL3teyHlj5y/I2pzTfwwQMa7HluRUD7UbQBmZxxDaYSGQnokmHTxNvCo8X3mCKlhoAuLChroYoFreYPrlLXIUA2Nd0hOwbPo0g1yWw3cxu2LKLSKaiRclHf5LJWk1Y9ZP9cxquMho2UX/MOmKjTKJH18xVzzgZkG1F2EquouKZ/mACmCl088=",
                        PrivateKeyPassword = "SOSA9719"
                    };
                }

                facturama.Csds.Create(csdRequest);
                return "regitro exitoso";
            }
            catch (Exception ex)
            {
                return "Usuario ya esta registrado";
            }
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

        private async Task<TaxReceipt> guardarXMLenBD(string pXML, string pUuid, string pRfcReceptor, string pTipo, string status, int pIdPayment, string idCFDI)
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
                xMLS.UserId = isAdministrator == false ? Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().UserId : ActualUserId;
                xMLS.IdXmlFacturama = idCFDI;
                xMLS.UsoCFDI = string.IsNullOrEmpty(msgUsos) ? "P01 - Por definir" : msgUsos;

                HttpContent content;
                json = JsonConvert.SerializeObject(xMLS);
                content = new StringContent(json, Encoding.UTF8, "application/json");
                var jsonResponse = await Requests.SendURIAsync("/api/TaxReceipt/", HttpMethod.Post, Variables.LoginModel.Token, content);
                TaxReceipt tax = JsonConvert.DeserializeObject<TaxReceipt>(jsonResponse);

                return tax;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private async Task<string> actualizarPaymentConFactura(Model.Payment pPay, Model.TaxReceipt pTR)
        {
            HttpContent content;
            try
            {
                pPay.HaveTaxReceipt = true;
                pPay.ObservationInvoice = string.IsNullOrEmpty(msgObservacionFactura) ? "" : msgObservacionFactura;
                content = new StringContent(JsonConvert.SerializeObject(pPay), Encoding.UTF8, "application/json");
                var updatePayment = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", pPay.Id), HttpMethod.Put, Variables.LoginModel.Token, content);
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: No se pudo actualizar Payment";
            }
        }

        public async Task<string> CancelaFactura(string IdXmlFacturama)
        {
            try
            {
                var list = facturama.Cfdis.List();

                Facturama.Models.Response.Cfdi cfdiCancel = facturama.Cfdis.Remove(IdXmlFacturama);
                if (cfdiCancel.Complement != null)
                {
                    //facturama.Cfdis.SaveXml(@"C:\Pruebas", cfdiCancel.Id);
                    return "Se cancelo exitosamente el cfdi con el folio fiscal: " + cfdiCancel.Complement.TaxStamp.Uuid;
                }
                else
                {
                    return "{\"error\": \"No se ha podido realizar la cancelación, favor de comunicarse con el administrador del sistema\"}";
                }

            }
            catch (FacturamaException ex)
            {
                string error = string.Empty;
                foreach (var messageDetail in ex.Model.Details)
                {
                    error = $"{messageDetail.Key}: {string.Join(",", messageDetail.Value)}";
                }
                return "{\"error\": " + ex.Message.Replace("\\", "").Replace("{", "").Replace("}", "").Split(':')[1] + " - " + error + "}";
            }
            catch (Exception ex)
            {

                return "{\"error\": " + ex.Message.Replace("\\", "").Replace("{", "").Replace("}", "").Split(':')[1] + "}";

            }
        }

        public async Task<ModFac.ResponseCFDI> ObterCfdiDesdeAPI(TaxReceipt taxes)
        {
            RequestsAPI RequestsFacturama = null;
            try
            {
                RequestsFacturama = new RequestsAPI("https://api.facturama.mx/");
                var idFacturama = taxes != null ? taxes.IdXmlFacturama : "";
                var resultado = await RequestsFacturama.SendURIAsync(string.Format("api-lite/cfdis/{0}", idFacturama), HttpMethod.Get, Properties.Settings.Default.FacturamaUser, Properties.Settings.Default.FacturamaPassword);
                //var cfdiGet = JsonConvert.DeserializeObject<Facturama.Models.Response.Cfdi>(resultado);
                var cfdiGet = JsonConvert.DeserializeObject<ModFac.ResponseCFDI>(resultado);
                return cfdiGet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> CancelarFacturaDesdeAPI(TaxReceipt idXmlFacturama)
        {
            RequestsAPI RequestsFacturama = null;
            try
            {
                RequestsFacturama = new RequestsAPI("https://api.facturama.mx/");
                var resultado = await RequestsFacturama.SendURIAsync(string.Format("api-lite/cfdis/{0}", idXmlFacturama.IdXmlFacturama), HttpMethod.Delete, Properties.Settings.Default.FacturamaUser, Properties.Settings.Default.FacturamaPassword);

                if (resultado.Contains("Server Error"))
                {
                    return "{\"error\": \"No se ha podido realizar la cancelación, fallo la solicitud a facturama.\"}";
                }
                else
                {
                    string resultadoSolicitudCancelacion = "";
                    SOAPAP.ModFac.RepuestaCancelacion cfdiCancel;
                    try
                    {                        
                        cfdiCancel = JsonConvert.DeserializeObject<SOAPAP.ModFac.RepuestaCancelacion>(resultado);
                    }
                    catch (Exception exe) 
                    {
                        return "{\"error\": \"No se ha podido realizar la cancelación, fallo la solicitud a facturama.\"}";
                    }
                    
                    switch (cfdiCancel.Status)
                    {
                        case "canceled":
                            resultadoSolicitudCancelacion = "Cancelación exitosa. Estado actual: " + cfdiCancel.Message;
                            break;
                        case "active":
                            resultadoSolicitudCancelacion = "{\"error\": \"No se pudo cancelar, hay facturas ligadas: " + cfdiCancel.Message + "\"}";
                            break;
                        case "pending":
                            resultadoSolicitudCancelacion = "{\"error\": \"Cancelación Pendiente: " + cfdiCancel.Message + "\"}";
                            break;
                        case "acepted":
                            resultadoSolicitudCancelacion = "Cancelación aceptada: " + cfdiCancel.Message;
                            break;
                        case "rejected":
                            resultadoSolicitudCancelacion = "{\"error\": \"Cancelación rechazada: " + cfdiCancel.Message + "\"}";
                            break;
                        case "expired":
                            resultadoSolicitudCancelacion = "Cancelación por expiración de 72hrs: " + cfdiCancel.Message;
                            break;
                    }

                    if(cfdiCancel.Status.Contains("canceled") || cfdiCancel.Status.Contains("acepted") || cfdiCancel.Status.Contains("expired"))
                    {
                        Byte[] bytes = new byte[0];
                        if (!string.IsNullOrEmpty(cfdiCancel.AcuseXmlBase64))
                            bytes = Convert.FromBase64String(cfdiCancel.AcuseXmlBase64);
                        TaxReceiptCancel cancel = new TaxReceiptCancel
                        {
                            CancelationDate = cfdiCancel.CancelationDate,
                            AcuseXml = bytes,
                            Message = cfdiCancel.Message,
                            Status = cfdiCancel.Status,
                            RequestDateCancel = cfdiCancel.RequestDate
                        };

                        //En este endpoint se actualiza el status del taxreceipt.
                        var a = JsonConvert.SerializeObject(cancel);
                        HttpContent content = new StringContent(a, Encoding.UTF8, "application/json");
                        var response = await Requests.SendURIAsync(string.Format("/api/TaxReceipt/TaxReceiptCancel/{0}", idXmlFacturama.IdXmlFacturama), HttpMethod.Post, Variables.LoginModel.Token, content);
                        if (!response.Contains("error:"))
                        {
                            return resultadoSolicitudCancelacion;
                        }
                        else
                        {
                            return "{\"error\": \"Se ralizó la cancelacion, pero no se pudo guardar el registro de cancelación en BD, contacte al administrador.\"}";
                        }
                    }
                    else
                    {
                        return resultadoSolicitudCancelacion;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return "{\"error\": " + ex.Message.Replace("\\", "").Replace("{", "").Replace("}", "").Split(':')[1] + "}";
            }
        }

        //Volver a generar factura en PDF
        public async Task<string> actualizaPdf(string idTransaction, Boolean esCancelacion = false)
        {
            string respuesta = string.Empty;
            string rutas = string.Empty;
            string json = string.Empty;
            Facturama.Models.Response.Cfdi cfdiFacturama = null;

            var resultado = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", idTransaction), HttpMethod.Get, Variables.LoginModel.Token);
            if (resultado.Contains("error"))
                return resultado;
            TransactionVM TraVM = JsonConvert.DeserializeObject<TransactionVM>(resultado);
            
            
            Model.TaxUser tu = null;
            if (TraVM.payment.OrderSaleId != 0)
            {
                var resulTaxUs = await Requests.SendURIAsync(string.Format("/api/TaxUsers/{0}", TraVM.orderSale.TaxUserId), HttpMethod.Get, Variables.LoginModel.Token);
                if (resulTaxUs.Contains("error"))
                    return resulTaxUs;
                tu = JsonConvert.DeserializeObject<Model.TaxUser>(resulTaxUs);
            }
                        
            //Facturama.Models.Response.Cfdi cfdiGet = null;
            ModFac.ResponseCFDI cfdiGet = null;
            if (esCancelacion)
            {
                TaxReceipt tmpTR = TraVM.payment.TaxReceipts.OrderBy(xx => xx.Id).LastOrDefault();
                if (tmpTR.IdXmlFacturama.Contains("Timbox"))
                {
                    cfdiGet = await ObterCfdiDesdeTIMBOX(tmpTR);
                }
                else
                {
                    cfdiGet = await ObterCfdiDesdeAPI(tmpTR);
                }
                
                if (cfdiGet != null)
                {
                    //Verifica el estatus, en caso de no estar cancelado, solicita la cancelacion.
                    if (cfdiGet.Status == "active")
                    {
                        MessageBoxForm mensaje = new MessageBoxForm("Advertencia", "Este CFDI continua activo, consulte al administrador.", TypeIcon.Icon.Warning);
                        var result = mensaje.ShowDialog();
                    }
                }
            }
            else
            {
                TaxReceipt taxReceipt = TraVM.payment.TaxReceipts.Where(x => x.Status == "ET001").FirstOrDefault();
                if (taxReceipt == null)
                    return "error: sin factura";
                if (taxReceipt.IdXmlFacturama.Contains("Timbox"))
                    cfdiGet = await ObterCfdiDesdeTIMBOX(taxReceipt);
                else
                    cfdiGet = await ObterCfdiDesdeAPI(taxReceipt);
            }
                

            //Si esta campo viene vacio, es porque no existe el registro en facturama.
            if (cfdiGet.Items == null)
            {
                //Actualiza el payment para que sea facturable.
                SOAPAP.Model.Payment tmpPay = TraVM.payment;
                tmpPay.HaveTaxReceipt = false;
                HttpContent content;
                json = JsonConvert.SerializeObject(tmpPay);
                content = new StringContent(json, Encoding.UTF8, "application/json");

                var resulTaxUs = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", TraVM.payment.Id), HttpMethod.Put, Variables.LoginModel.Token, content);
                var resActualizacionPayment = JsonConvert.DeserializeObject<string>(resulTaxUs);

                //Actualizacion de tax receipt
                SOAPAP.Model.TaxReceipt tmpTR = TraVM.payment.TaxReceipts.FirstOrDefault(t => t.Status == "ET001");
                tmpTR.Status = "ET002";
                HttpContent contentTR;
                json = JsonConvert.SerializeObject(tmpTR);
                contentTR = new StringContent(json, Encoding.UTF8, "application/json");

                var resulTaxR = await Requests.SendURIAsync(string.Format("/api/TaxReceipt/{0}", TraVM.payment.TaxReceipts.FirstOrDefault(x => x.Status == "ET001")?.Id), HttpMethod.Put, Variables.LoginModel.Token, contentTR);
                var resTax = JsonConvert.DeserializeObject<SOAPAP.Model.TaxReceipt>(resulTaxR);

                return "aviso: Es necesario volver a facturar este pago.";
            }

            string fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            CreatePDF pDF = null;
            if (esCancelacion)
                pDF = new CreatePDF(cfdiGet, TraVM.payment.Account, TraVM.payment.TaxReceipts.OrderBy(xx => xx.Id).LastOrDefault(), fecha, (TraVM.payment.PayMethod.code + ", " + TraVM.payment.PayMethod.Name), TraVM);
            else
                pDF = new CreatePDF(cfdiGet, TraVM.payment.Account, TraVM.payment.TaxReceipts.Where(x => x.Status == "ET001").FirstOrDefault(), fecha, (TraVM.payment.PayMethod.code + ", " + TraVM.payment.PayMethod.Name), TraVM);


            string resPdf = "";
            if (cfdiGet != null)
            {
                string seriefolio = TraVM.transaction.transactionFolios.FirstOrDefault().folio.ToString();
                string path = GeneraCarpetaDescagasXML();
                string nombreXML = string.Format("\\{0}_{1}_{2}.xml", cfdiGet.Issuer.Rfc, cfdiGet.Receiver.Rfc, seriefolio);
                string nombrePDF = string.Format("\\{0}_{1}_{2}.pdf", cfdiGet.Issuer.Rfc, cfdiGet.Receiver.Rfc, seriefolio);

                //Uso
                pDF.UsoCFDI = TraVM.payment.TaxReceipts.FirstOrDefault().UsoCFDI;
                //Observaciones
                if (string.IsNullOrEmpty(cfdiGet.Observations))
                {
                    string msgObservacionFactura = string.IsNullOrEmpty(TraVM.payment.ObservationInvoice) ? "" : TraVM.payment.ObservationInvoice;
                    //bool printFecha = Variables.LoginModel.Divition != 12;
                    //if (Variables.Configuration.IsMunicipal)
                    //{
                    //    //En caso de factura fuera de fecha
                    //    if (TraVM.payment.PaymentDate.ToString("yyyy-MM-dd") != DateTime.Today.ToString("yyyy-MM-dd") && printFecha)
                    msgObservacionFactura += string.IsNullOrEmpty(msgObservacionFactura) ? "Pago efectuado el " + TraVM.payment.PaymentDate.ToString("yyyy-MM-dd") : ", Pago efectuado el " + TraVM.payment.PaymentDate.ToString("yyyy-MM-dd");
                    //}
                    //else
                    //{
                    //    if (TraVM.payment.PaymentDate.ToString("yyyy-MM-dd") != DateTime.Today.ToString("yyyy-MM-dd"))
                    //        msgObservacionFactura += ", Pago efectuado el " + TraVM.payment.PaymentDate.ToString("yyyy-MM-dd");
                    //}
                    //Verifico si es un pago parcial.
                    if (TraVM.payment.OrderSaleId == 0 && cfdiGet.Status == "active")
                    {
                        var resulDebt = await Requests.SendURIAsync(string.Format("/api/Debts/id/{0}", TraVM.payment.PaymentDetails.FirstOrDefault()?.DebtId), HttpMethod.Get, Variables.LoginModel.Token);
                        var resDebt = JsonConvert.DeserializeObject<SOAPAP.Model.Debt>(resulDebt);

                        if (TraVM.payment.Total < resDebt.Amount)
                            msgObservacionFactura += ", Esta factura es comprobante de un pago pacial";
                    }
                    else
                    {
                        if (TraVM.orderSale.Amount > TraVM.orderSale.OnAccount && cfdiGet.Status == "active")
                            msgObservacionFactura += ", Esta factura es comprobante de un pago pacial";
                    }

                    ////Si hay observaciones en la Orden o el debt
                    //if (TraVM.payment.OrderSaleId == 0)
                    //    msgObservacionFactura += (string.IsNullOrEmpty(TraVM.payment.PaymentDetails.FirstOrDefault().Debt.observations) ? "" : ", " + TraVM.payment.PaymentDetails.FirstOrDefault().Debt.observations);
                    //else
                    //{
                    //    msgObservacionFactura += (string.IsNullOrEmpty(TraVM.orderSale.Observation) ? "" : ", " + TraVM.orderSale.Observation);
                    //}
                    pDF.ObservacionCFDI = msgObservacionFactura;
                }

                //Se obtiene el serial de un cajero elejido, esto cuando el reporte lo solicita un administrador.
                string tmpSerialCajero = "";
                if (Variables.LoginModel.RolName[0] == "Supervisor")
                {
                    var _resulSerial = await Requests.SendURIAsync(string.Format("/api/UserRolesManager/SerialFromUser/{0}", ActualUserId), HttpMethod.Get, Variables.LoginModel.Token);
                    if (_resulSerial.Contains("error"))
                        tmpSerialCajero = "JdC";
                    else
                    {
                        tmpSerialCajero = JsonConvert.DeserializeObject<string>(_resulSerial);
                        if (tmpSerialCajero == null)
                            tmpSerialCajero = "JdC";
                    }
                }
                else
                {
                    tmpSerialCajero = Variables.LoginModel.User;
                }
                pDF.SerialCajero = tmpSerialCajero;

                if (TraVM.payment.OrderSaleId == 0) //Servicio
                    resPdf = await pDF.Create(path + nombrePDF, TraVM.payment.AgreementId);
                else
                {
                    TraVM.orderSale.TaxUser = tu;
                    resPdf = await pDF.CreateForOrder(TraVM.orderSale, path + nombrePDF);
                }

                if (resPdf.Contains("error"))
                    return resPdf;
                else
                    return "aviso: Actualizado correctamente.";
            }
            else
            {
                return "{\"error\": \"No se ha podido realizar la cancelación, favor de comunicarse con el administrador del sistema\"}";
            }
        }

        //public SOAPAP.Facturado.DocumentoXML DeserializerXML(string xmlString)
        //{            
        //    StringReader stringReader = new StringReader(xmlString);
        //    XmlSerializer serializer = new XmlSerializer(typeof(SOAPAP.Facturado.Comprobante), new XmlRootAttribute("Comprobante"));
        //    SOAPAP.Facturado.DocumentoXML comprobante = (SOAPAP.Facturado.DocumentoXML)serializer.Deserialize( stringReader);
        //    return comprobante;
        //}


        public async Task<string> actualizaCanceladoPDF(string idTransaction)
        {
            string respuesta = string.Empty;
            string rutas = string.Empty;
            string json = string.Empty;
            try
            {
                var resultado = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", idTransaction), HttpMethod.Get, Variables.LoginModel.Token);
                TransactionVM TraVM = JsonConvert.DeserializeObject<TransactionVM>(resultado);

                TaxReceipt taxReceipt = TraVM.payment.TaxReceipts.LastOrDefault(x => x.Status == "ET002");

                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Facturas";                            
                DirectoryInfo di;
                if (!Directory.Exists(path))
                {
                    di = Directory.CreateDirectory(path);
                }
                //Se guarda el pdf del timbre.
                string NombreFile = string.Format("{0}\\{1}_{2}_{3}_canceled.pdf", path, Variables.Configuration.RFC ,taxReceipt.RFC, TraVM.transaction.transactionFolios != null ? TraVM.transaction.transactionFolios.FirstOrDefault().folio : "AXXXXX1");
                System.IO.File.WriteAllBytes( NombreFile, taxReceipt.PDFInvoce);

                //Se edita el Pdf
                using (PdfDocumentProcessor processor = new PdfDocumentProcessor())
                {
                    processor.LoadDocument(NombreFile);
                    using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(150, Color.Red)))
                        AddGraphics(processor, "F A C T U R A   _ _   C A N C E L A D A", textBrush);                    
                    processor.SaveDocument(NombreFile);
                }

                //Se guarda el archivo en BD.
                StringContent @string = new StringContent(JsonConvert.SerializeObject(taxReceipt), Encoding.UTF8, "application/json");
                var UploadPDF = await Requests.UploadImageToServer("/api/TaxReceipt/AddPDF", Variables.LoginModel.Token, NombreFile, @string);
                if (UploadPDF.Contains("error"))
                {
                    MessageBoxForm mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(UploadPDF).error, TypeIcon.Icon.Cancel);
                    var result = mensaje.ShowDialog();                    
                }

                ////Se elimina el archivo temporal.
                //if (System.IO.File.Exists(NombreFile))
                //    System.IO.File.Delete(NombreFile);
                return NombreFile;
            }
            catch (Exception ex)
            {
                return "error: No se pudo actualizar el PDF.";
            }

        }


        public async Task<string> actualizaCanceladoPDFwithIdpayment(string idsPayment)
        {
            string respuesta = string.Empty;
            string rutas = string.Empty;
            string json = string.Empty;            
            try
            {

                var resultado = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", idsPayment), HttpMethod.Get, Variables.LoginModel.Token);
                Payment payment = JsonConvert.DeserializeObject<Payment>(resultado);

                TaxReceipt taxReceipt = payment.TaxReceipts.LastOrDefault(x => x.Status == "ET002");

                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Facturas";
                DirectoryInfo di;
                if (!Directory.Exists(path))
                {
                    di = Directory.CreateDirectory(path);
                }
                //Se guarda el pdf del timbre.
                string NombreFile = string.Format("{0}\\{1}_{2}_{3}_canceled.pdf", path, Variables.Configuration.RFC, taxReceipt.RFC, !string.IsNullOrEmpty(payment.ImpressionSheet) ? payment.ImpressionSheet : "AXXXXX1");
                System.IO.File.WriteAllBytes(NombreFile, taxReceipt.PDFInvoce);

                //Se edita el Pdf
                using (PdfDocumentProcessor processor = new PdfDocumentProcessor())
                {
                    processor.LoadDocument(NombreFile);
                    using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(150, Color.Red)))
                        AddGraphics(processor, "F A C T U R A   _ _   C A N C E L A D A", textBrush);
                    processor.SaveDocument(NombreFile);
                }

                //Se guarda el archivo en BD.
                StringContent @string = new StringContent(JsonConvert.SerializeObject(taxReceipt), Encoding.UTF8, "application/json");
                var UploadPDF = await Requests.UploadImageToServer("/api/TaxReceipt/AddPDF", Variables.LoginModel.Token, NombreFile, @string);
                if (UploadPDF.Contains("error"))
                {
                    MessageBoxForm mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(UploadPDF).error, TypeIcon.Icon.Cancel);
                    var result = mensaje.ShowDialog();
                }

                ////Se elimina el archivo temporal.
                //if (System.IO.File.Exists(NombreFile))
                //    System.IO.File.Delete(NombreFile);
                return NombreFile;
            }
            catch (Exception ex)
            {
                return "error: No se pudo actualizar el PDF.";
            }

        }

        const float DrawingDpi = 72f;
        static void AddGraphics(PdfDocumentProcessor processor, string text, SolidBrush textBrush)
        {
            IList<PdfPage> pages = processor.Document.Pages;
            for (int i = 0; i < pages.Count; i++)
            {
                PdfPage page = pages[i];
                using (PdfGraphics graphics = processor.CreateGraphics())
                {
                    SizeF actualPageSize = PrepareGraphics(page, graphics);
                    using (Font font = new Font("Segoe UI", 22, FontStyle.Regular))
                    {
                        SizeF textSize = graphics.MeasureString(text, font, PdfStringFormat.GenericDefault);
                        //PointF topLeft = new PointF(0, 0);
                        //PointF bottomRight = new PointF(actualPageSize.Width - textSize.Width, actualPageSize.Height - textSize.Height);
                        //graphics.DrawString(text, font, textBrush, topLeft);
                        //graphics.DrawString(text, font, textBrush, bottomRight);

                        PointF centerPage = new PointF((actualPageSize.Width / 2) - (textSize.Width / 2), (actualPageSize.Height / 2) - (textSize.Height / 2));
                        graphics.RotateTransform(-20);
                        graphics.DrawString(text, font, textBrush, centerPage);

                        graphics.AddToPageForeground(page, DrawingDpi, DrawingDpi);
                    }
                }
            }
        }

        static SizeF PrepareGraphics(PdfPage page, PdfGraphics graphics)
        {
            PdfRectangle cropBox = page.CropBox;
            float cropBoxWidth = (float)cropBox.Width;
            float cropBoxHeight = (float)cropBox.Height;

            switch (page.Rotate)
            {
                case 90:
                    graphics.RotateTransform(-90);
                    graphics.TranslateTransform(-cropBoxHeight, 0);
                    return new SizeF(cropBoxHeight, cropBoxWidth);
                case 180:
                    graphics.RotateTransform(-180);
                    graphics.TranslateTransform(-cropBoxWidth, -cropBoxHeight);
                    return new SizeF(cropBoxWidth, cropBoxHeight);
                case 270:
                    graphics.RotateTransform(-270);
                    graphics.TranslateTransform(0, -cropBoxWidth);
                    return new SizeF(cropBoxHeight, cropBoxWidth);
            }
            return new SizeF(cropBoxWidth, cropBoxHeight);
        }

        ////Timbrado secundario
        public async Task<Facturama.Models.Response.Cfdi> timbrarProvedorSecundario(CfdiMulti cfdiMulti)
        {
            //Facturador secundario.
            TimbradoTimbox TT = new TimbradoTimbox();
            Facturama.Models.Response.Cfdi resCfdi = await TT.Facturar(cfdiMulti);
            if (resCfdi == null)
            {
                return null;
            }
            else
            {
                return resCfdi;
            }            
        }

        public async Task<ModFac.ResponseCFDI> ObterCfdiDesdeTIMBOX(TaxReceipt taxReceipt)
        {
            //Facturador secundario.
            TimbradoTimbox TT = new TimbradoTimbox();
            ModFac.ResponseCFDI resCfdi = await TT.getCFDI(taxReceipt);           
            return resCfdi;            
        }

        //Verifica si a esta cueta se la condono deuda por COVID
        private async Task<decimal> VerificaDescuentoPorCOVID(int idAgreement, List<Debt> debts)
        {
            try
            {
                decimal TotalDescuentoCOVID = 0;
                string aniosDebt = string.Join("|", debts.Where(x => x.Type == "TIP01").Select(x => x.Year).ToList());
                var resultDebDescuento = await Requests.SendURIAsync(string.Format("/api/Debts/promocionCOVID/{0}/{1}", idAgreement, aniosDebt), HttpMethod.Get, Variables.LoginModel.Token);
                
                if (resultDebDescuento.Contains("error\\"))
                {
                    var mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultDebDescuento).error, TypeIcon.Icon.Cancel);
                    var result = mensaje.ShowDialog();
                }
                else
                {
                    //Variables.Agreement.Debts = JsonConvert.DeserializeObject<List<Model.Debt>>(resultDebDescuento);
                    TotalDescuentoCOVID = JsonConvert.DeserializeObject<decimal>(resultDebDescuento);                                      
                }
                return TotalDescuentoCOVID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }

}
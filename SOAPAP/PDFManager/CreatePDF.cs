using Facturama.Models.Request;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using HiQPdf;
using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Model;
using SOAPAP.Properties;
using SOAPAP.Services;
using SOAPAP.Tools;
using SOAPAP.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.PDFManager
{
    public class CreatePDF
    {
        public CfdiMulti CfdiMulti { get; set; }
        public Facturama.Models.Response.Cfdi Cfdi { get; set; }
        Form loading;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        DialogResult result = new DialogResult();
        Form mensaje;
        Model.Agreement _agreement = null;        
        Model.Client _clientP = null;
        List<Model.Division> _lstDivision = null;
        Model.OrderSale _orderSale = null;
        public string Account { get; set; }
        public string UsoCFDI { get; set; }
        public string ObservacionCFDI { get; set; }
        public TaxReceipt TaxReceipt { get; set; }

        public CreatePDF(CfdiMulti CfdiMulti, Facturama.Models.Response.Cfdi Cfdi, string Account, TaxReceipt TaxReceipt)
        {
            this.CfdiMulti = CfdiMulti;
            this.Cfdi = Cfdi;
            this.Account = Account;
            this.TaxReceipt = TaxReceipt;
            Requests = new RequestsAPI(UrlBase);
        }

        //Obtiene informacion para un servicio
        public async Task<string> Create(string PathNombrePdf)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                var resultAgreement = await Requests.SendURIAsync(string.Format("/api/Agreements/GetSummary/{0}", Account), HttpMethod.Get, Variables.LoginModel.Token);
                if (resultAgreement.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", resultAgreement.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    _agreement = JsonConvert.DeserializeObject<Model.Agreement>(resultAgreement);

                    if (_agreement != null)
                    {
                        ObtenerInformacion(_agreement);
                    }
                    else
                    {
                        mensaje = new MessageBoxForm("Error", "No se ha obtenido información de esa cuenta", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                }

                var resultDivision = await Requests.SendURIAsync("/api/Division", HttpMethod.Get, Variables.LoginModel.Token);
                if (resultDivision.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", resultDivision.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    _lstDivision = JsonConvert.DeserializeObject<List<Model.Division>>(resultDivision);
                    if (_lstDivision == null)
                    {
                        mensaje = new MessageBoxForm("Error", "No pudo consultarse la division", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                }

                PdfDocument document = new PdfDocument();
                document.SerialNumber = Properties.Settings.Default.SerialNumber;
                PdfPage page1 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(5), PdfPageOrientation.Portrait);
                PdfHtml html = new PdfHtml(HtmlToPdf(), null);
                html.WaitBeforeConvert = 2;
                PdfLayoutInfo layoutInfo = page1.Layout(html);
                byte[] pdfBuffer = document.WriteToMemory();
                //ExportGridToPDF(pdfBuffer);
                System.IO.File.WriteAllBytes(PathNombrePdf, pdfBuffer);
                StringContent @string = new StringContent(JsonConvert.SerializeObject(TaxReceipt), Encoding.UTF8, "application/json");
                var UploadPDF = await Requests.UploadImageToServer("/api/TaxReceipt/AddPDF", Variables.LoginModel.Token, PathNombrePdf, @string);
                if (UploadPDF.Contains("error"))
                {
                    try
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(UploadPDF).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    catch (Exception)
                    {
                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                }
                return PathNombrePdf;                
            }
            catch (Exception e)
            {
                return "error: No pudo crearse Pdf.";
            }
        }

        //Obtiene informacion para una orden
        public async Task<string> CreateForOrder(Model.OrderSale pOrSa, string PathNombrePdf)
        {
            _orderSale = pOrSa;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {                
                var resultDivision = await Requests.SendURIAsync("/api/Division", HttpMethod.Get, Variables.LoginModel.Token);
                if (resultDivision.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", resultDivision.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    _lstDivision = JsonConvert.DeserializeObject<List<Model.Division>>(resultDivision);
                    if (_lstDivision == null)
                    {                        
                        mensaje = new MessageBoxForm("Error", "No pudo consultarse la division", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                }
                
                PdfDocument document = new PdfDocument();
                document.SerialNumber = Properties.Settings.Default.SerialNumber;
                PdfPage page1 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(5), PdfPageOrientation.Portrait);
                PdfHtml html = new PdfHtml(HtmlToPdfForOrders(), null);
                html.WaitBeforeConvert = 2;
                PdfLayoutInfo layoutInfo = page1.Layout(html);
                byte[] pdfBuffer = document.WriteToMemory();

                //ExportGridToPDF(pdfBuffer);
                //Se guarda en memoria el Pdf
                System.IO.File.WriteAllBytes(PathNombrePdf, pdfBuffer);

                StringContent @string = new StringContent(JsonConvert.SerializeObject(TaxReceipt), Encoding.UTF8, "application/json");
                var UploadPDF = await Requests.UploadImageToServer("/api/TaxReceipt/AddPDF", Variables.LoginModel.Token, PathNombrePdf, @string);
                if (UploadPDF.Contains("error"))
                {
                    try
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(UploadPDF).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    catch (Exception)
                    {
                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                }
                return PathNombrePdf;
            }
            catch (Exception e)
            {
                return "error: No pudo guardarse el archivo Pdf.";
            }
        }


        private void ObtenerInformacion(Model.Agreement _agreement)
        {
            if (_agreement != null)
            {
               _clientP = _agreement.Clients.Where(c => c.TypeUser == "CLI01" && c.IsActive == true).FirstOrDefault();
                Model.Client _clientU = _agreement.Clients.Where(c => c.TypeUser == "CLI02" && c.IsActive == true).FirstOrDefault();
                String _propietario = String.Empty;
                String _usuario = String.Empty;

                if (_clientP != null)
                    _propietario = "P. " + _clientP.Name + " " + _clientP.LastName + " " + _clientP.SecondLastName;
                if (_clientU != null)
                    _usuario = "U. " + _clientU.Name + " " + _clientU.LastName + " " + _clientU.SecondLastName;
            }
        }

        //Genera Pdf para servicios
        private string HtmlToPdf()
        {
            StringBuilder builder = new StringBuilder();
            bool havtax = false;
            Numalet let = new Numalet();
            let.MascaraSalidaDecimal = "00/100 M.N";
            let.SeparadorDecimalSalida = "pesos";
            let.LetraCapital = true;
            let.ApocoparUnoParteEntera = true;
            string QrFileName = Account + Cfdi.Complement.TaxStamp.SatCertNumber;
            decimal total = CfdiMulti.Items.Sum(x => x.Total);
            string last = Cfdi.Complement.TaxStamp.CfdiSign.Substring(Cfdi.Complement.TaxStamp.CfdiSign.Length - 8);
            CfdiMulti.Items.ForEach(x =>
            {
                if (x.Taxes != null)
                {
                    havtax = true;
                }
            });
            //if (havtax)
            //{

            //}
            Model.Division Div = _lstDivision.Where(x => x.Id == Variables.LoginModel.Divition).FirstOrDefault();
            string tmpDivision = Div == null ? "" : Div.Name; 


            GeneraCodigoQR(Cfdi.Complement.TaxStamp.Uuid,
                           CfdiMulti.Issuer.Rfc,
                           CfdiMulti.Receiver.Rfc,
                           total.ToString().PadLeft(18, '0').PadRight(6, '0'),
                           last,
                           QrFileName);

            builder.Append(@"<html>");
            builder.Append(@"<html lang='es'><head>");
            builder.Append(@"<meta charset='UTF-8'>");
            builder.Append(@"<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
            builder.Append(@"<meta http-equiv='X-UA-Compatible' content='ie=edge'>");
            builder.Append(@"<link href='https://fonts.googleapis.com/css?family=Montserrat|Roboto&display=swap' rel='stylesheet'>");
            builder.Append(@"<title>Facturación</title></head>");
            builder.Append(@"<body style='margin: 40px; font-size: 10px;'>");
            builder.Append(@"<div style='font-family: \""Roboto\"", sans-serif; height: 100px;'>");
            builder.Append(@"<div class='cabecera_principal' style='margin-bottom: 15px;'>");
            builder.Append(@"<div style='text-align: center; display: inline-block; width: 15%; vertical-align: top;'>");
            if (Variables.Configuration.IsMunicipal)
            {
                 builder.Append(@"<img src='" + AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", "Resources") + @"ayuntamiento.jpg" + "' alt='Logo-Ayuntamiento' width='200' height='200'>");
            }

            else
            {
                builder.Append(@"<img src='" + AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", "Resources") + @"sosapac.png" + "' alt='Logo-SOSAPAC' width='200' height='200'>");
            }
                
            builder.Append(@"</div>");
            builder.Append(@"<div style='text-align: center; display: inline-block; width: 55%; font-size: 14px;'>");
            if (Variables.Configuration.IsMunicipal)
            {
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'><b>" + CfdiMulti.Issuer.Name+"</b></p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'><b>RFC: " + CfdiMulti.Issuer.Rfc+"</b></p><br>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'> PALACIO MUNICIPAL  S/N Centro CUAUTLANCINGO CUAUTLANCINGO 72700<br>PUEBLA MEXICO </p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'>Teléfono: (222) 2-85-13-62</p><br>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'>Régimen Fiscal: " + CfdiMulti.Issuer.FiscalRegime + " - Personas Morales con Fines no Lucrativos</p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'>Expedido en: 72700, CUAUTLANCINGO</p>");
                builder.Append(@"<p style='font-size: 16px;'><b>TESORERIA MUNICIPAL</b></p>");
                builder.Append(@"<p style='font-size: 16px;'><b>" + tmpDivision + "</b></p></div>");
            }
            else
            {
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'> <b>" + CfdiMulti.Issuer.Name + "</b></p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'><b>RFC: " + CfdiMulti.Issuer.Rfc + "</b></p><br>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'> SAN LORENZO 84  A CENTRO CUAUTLANCINGO 72700 <br>PUEBLA MEXICO </p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'>Teléfono: (222) 2269761</p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'>Email: contacto@sosapac.gob.mx </p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'><b>COMPROBANTE DE PAGO</b></p></div>");
            }
           
            builder.Append(@" <div id='datFact' style='display: inline-block; width: 20%; font-size: 12px; text-align: center; vertical-align: top;'>");
            builder.Append(@"<table style='text-align: center; font-size: 14px;'>");
            builder.Append(@"<tr><td><b>COMPROBANTE</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'><b>"+ CfdiMulti.Folio+"</b></td></tr>"); //Folio
            builder.Append(@"<tr><td><b>FOLIO FISCAL</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif; font-size:11px;'>"+ Cfdi.Complement.TaxStamp.Uuid +"</td></tr>"); //UUID
            builder.Append(@"<tr><td><b>CERTIFICADO SAT</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'>"+Cfdi.Complement.TaxStamp.SatCertNumber+"</td></tr>");// certificado sat
            builder.Append(@"<tr><td><b>FECHA AUTORIZACIÓN SAT</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'>"+Cfdi.Date+"</td></tr>");//fecha autorización
            builder.Append(@"<tr><td><b>FECHA EMISION</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'>"+CfdiMulti.Date+"</td></tr>");//Fecha Emision
            builder.Append(@"</table>");
            builder.Append(@"</div></div>");
            builder.Append(@"<div class='datos_contribuyente' style='margin-bottom: 15px;'>");
            builder.Append(@"<table style='font-size: 14px;'>");
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 120px;'><b>No. de cuenta:</b></td>");
            builder.Append(@"<td style='width: 100px; font-family:\""Montserrat\"", sans-serif;'>"+Account+"</td>"); //Cuenta
            if (Variables.Configuration.IsMunicipal)
            {
                if(Variables.LoginModel.Divition == 6)
                {
                    builder.Append(@"<td style='width: 50px;'><b>Tipo Predio:</b></td>");
                    builder.Append(@"<td style='width: 180px; font-family:\""Montserrat\"", sans-serif;'>" + _agreement.TypeIntake.Name + "</td>"); //Uso
                }
            }
            else
            {
                builder.Append(@"<td style='width: 50px;'><b>Uso:</b></td>");
                builder.Append(@"<td style='width: 180px; font-family:\""Montserrat\"", sans-serif;'>"+ _agreement.TypeIntake.Name.ToUpper() + "-" + _agreement.TypeConsume.Name.ToUpper() + "</td>"); //Uso
                builder.Append(@"<td style='width: 60px;'><b>Ruta:</b></td>");
                builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>"+_agreement.Route+"</td>"); //Ruta
            }
          
            builder.Append(@"</tr>");
            builder.Append(@"</table>");
            builder.Append(@"<table style='font-size: 14px;>");
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 120px;'><b>Periodo:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>"+CfdiMulti.PaymentConditions+"</td>"); //Periodo
            builder.Append(@"</tr>");
            builder.Append(@"<tr><td style='width: 120px;'><b>Contribuyente:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>" + _clientP.Name + " " + _clientP.LastName + " " + _clientP.SecondLastName + "</td></tr>"); //Contribuyente
            builder.Append(@"<tr><td style='width: 120px;'><b>RFC:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>"+CfdiMulti.Receiver.Rfc+"</td></tr>"); //RFC
            builder.Append(@"<tr><td style='width: 120px;'><b>Uso CFDI:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>" + UsoCFDI + "</td>"); //Uso CFDI
            builder.Append(@"</tr><tr><td style='width: 120px;'><b>Dirección:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>"+_agreement.Addresses.First().Street+ " NO " + _agreement.Addresses.First().Outdoor + (string.IsNullOrEmpty(_agreement.Addresses.First().Indoor) ? "" : "No "+_agreement.Addresses.First().Indoor)+ "COL." + _agreement.Addresses.First().Suburbs.Name +"."+ _agreement.Addresses.First().Suburbs.Towns.Name+". "+ _agreement.Addresses.First().Suburbs.Towns.States.Name + "</td></tr>"); //Dirección
            builder.Append(@"</table>");

            if (Variables.Configuration.IsMunicipal && Variables.LoginModel.Divition == 6)
            {
                builder.Append(@"<br><table style='width: 100%; style='font-size: 14px;'>");
                builder.Append(@"<th>Terreno: " + _agreement.AgreementDetails.FirstOrDefault().Ground + "</th>");
                builder.Append(@"<th>Construcción: " + _agreement.AgreementDetails.FirstOrDefault().Built + "</th>");
                builder.Append(@"<th>Base: " + string.Format(new CultureInfo("es-MX"), "{0:C2}", _agreement.AgreementDetails.FirstOrDefault().TaxableBase) + "</th>");
                builder.Append(@"<th>Fecha Avalúo: " + _agreement.AgreementDetails.FirstOrDefault().LastUpdate + "</th>");
                builder.Append(@"</table>");
            }

            builder.Append(@"</div>");
            builder.Append(@"<div class='datos_conceptos' style='margin-bottom: 10px;'>");
            builder.Append(@"<table style='width: 100%; font-size:13px;'>");
            builder.Append(@"<tr style='background-color: black; color: white;'>");
            builder.Append(@"<th>CLAVEPROD. SERV.</th>");
            builder.Append(@"<th>UNIDAD DE MEDIDA</th>");
            builder.Append(@"<th>DESCRIPCION</th>");
            builder.Append(@"<th>CANTIDAD</th>");
            builder.Append(@"<th>VALOR UNITARIO</th>");
           //builder.Append(@"<th>IVA TRASLADO %16</th>");
            builder.Append(@"<th>DESC.</th>");
            builder.Append(@"<th>IMPORTE</th>");
            builder.Append(@" </tr>");
            //Foreach Concepts
            int cont = 0;
            CfdiMulti.Items.ForEach(x =>
            {
                builder.Append(@"<tr>");
                builder.Append(@"<td>"+CfdiMulti.Items[cont].ProductCode+"</td>");
                builder.Append(@"<td>"+CfdiMulti.Items[cont].UnitCode+"</td>");
                builder.Append(@"<td>"+CfdiMulti.Items[cont].Description+"</td>");
                builder.Append(@"<td>"+CfdiMulti.Items[cont].Quantity+"</td>");
                builder.Append(@"<td>"+ string.Format(new CultureInfo("es-MX"), "{0:C2}", CfdiMulti.Items[cont].UnitPrice)+"</td>");
                builder.Append(@"<td>"+ string.Format(new CultureInfo("es-MX"), "{0:C2}", CfdiMulti.Items[cont].Discount)+"</td>");
                builder.Append(@"<td>"+ string.Format(new CultureInfo("es-MX"), "{0:C2}", CfdiMulti.Items[cont].Total)+"</td>");
                builder.Append(@"</tr>");
                cont++;
            });
            //End Foreach
            builder.Append(@"</table></div>");
            builder.Append(@"<div class='datos_sub_moneda' style='margin-bottom:5px;'>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width:70%; font-size: 12px; font-family:\""Montserrat\"", sans-serif;'>");
            CfdiMulti.Items.ForEach(x =>
            {
                if (x.Taxes != null)
                {
                    havtax = true;
                }
            });
            if(!havtax)
            {
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size:14px;'>" + let.ToCustomCardinal(CfdiMulti.Items.Sum(x => x.Total)).ToUpperInvariant() + "</p></div>"); //Numero a letras
            }
            else
            {
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size:14px;'>" + let.ToCustomCardinal((CfdiMulti.Items.Sum(x => x.Total) + CfdiMulti.Items.Sum(x => x.Taxes.Sum(c => c.Total)))).ToUpperInvariant() + "</p></div>"); //Numero a letras
            }
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 20%; font-size: 14px; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 14px;'>SubTotal: </p></div>");
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 5%; font-size: 14px; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p style='margin-top: 20px;margin-bottom: 0px; font-size: 14px;'>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", CfdiMulti.Items.Sum(x => x.Total)) + "</p></div></div>"); //subtotal
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 90%; font-size: 14px;'>");
            builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 14px;'>IVA:</p></div>");
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 4.3%; font-size: 14px;'>");
            if (havtax)
            {
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 14px;'>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", CfdiMulti.Items.Sum(x => x.Taxes.Sum(c => c.Total))) + "</p></div>");
            }
            else
            {
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 14px;'>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", 0) + "</p></div>");
            }
            
            builder.Append(@"<div class='datos_moneda' style='margin-bottom: 10px;'>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width:10%; font-size: 14px;'>");
            builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px;'><b>Mondeda:</b></p></div>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width:60%; font-size: 14px; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px;'>MXN</p></div>"); //tipo Moneda
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 19.6%; font-size: 14px; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px;'>Total:</p></div>");
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 5.1%; font-size: 14px; font-family:\""Montserrat\"", sans-serif;'>");
            if (havtax)
            {
                builder.Append(@"<p style='margin-top: 3px;margin-bottom: 7px; font-size: 14px;'>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", (CfdiMulti.Items.Sum(x => x.Total) + CfdiMulti.Items.Sum(x => x.Taxes.Sum(c => c.Total)))) + "</p></div>"); //Total
            }
            else
            {
                builder.Append(@"<p style='margin-top: 3px;margin-bottom: 7px; font-size: 14px;'>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", CfdiMulti.Items.Sum(x => x.Total)) + "</p></div>"); //Total
            }
            
            builder.Append(@"</div>");
            builder.Append(@"<div class='datos_comprobante' style='margin-bottom: 30px;'><br>");
            builder.Append(@"<table style='font-size:14px;'>");
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 180px;'><b>Tipo de comprobante:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>I-INGRESO</td></tr>"); //tipo de comprobante
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 180px;'><b>Forma de pago:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>01, EFECTIVO</td></tr>"); //Forma de Pago
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 180px;'><b>Método de pago:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>PUE, Pago en una sola exhibición</td></tr>"); //Como se realizo el pago
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 180px;'><b>No. serie certificado:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>00001000000410637078</td></tr>"); //No Certificado
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 180px;'><b>Observaciones:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>" + ObservacionCFDI + "</td></tr>"); //Oberservaciones
            builder.Append(@"</table>");
            builder.Append(@"</div>");
            builder.Append(@"<div class='sello_digital' style='margin-bottom: 30px;'>");
            builder.Append(@"<div style='text-align: center; display: inline-block; width: 15%; vertical-align: top; margin-top: 29px; margin-right: 19px;'>");
            //QR image 
            builder.Append(@"<img src='" + AppDomain.CurrentDomain.BaseDirectory + @"QR\"+QrFileName + ".png' alt='Logo-Ayuntamiento' width='180' height='180'>");
            //end QR
            builder.Append(@"</div>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width: 76%;'>");
            builder.Append(@"<div style='text-align: left;'>");
            builder.Append(@"<p><b>RFCProvCert: ESO1202108R2</b></p></div>");
            builder.Append(@"<div style='text-align: left;'>");
            builder.Append(@"<p style='margin-bottom: 0px;'><b>Sello del SAT</b></p>");
            builder.Append(@"</div>");
            builder.Append(@"<div style='text-align: left; width:100%;'>");
            builder.Append(@"<p style='font-family:\""Montserrat\"", sans-serif; width: 100%;overflow-wrap: break-word; margin-top: 0px; font-size:10px; text-align: justify;'>");
            //Cetificado RFCProvCert
            builder.Append(Cfdi.Complement.TaxStamp.SatSign.ToString());
            // end RFCProvCert
            builder.Append(@"</p></div>");
            builder.Append(@"<div style='text-align: left;'>");
            builder.Append(@"<p style='margin-bottom: 0px;'><b>Sello Digital del CFDI:</b></p></div>");
            builder.Append(@"<div style='text-align: left; width:100%;'>");
            builder.Append(@"<p style='font-family:\""Montserrat\"", sans-serif; width: 100%;overflow-wrap: break-word; margin-top: 0px; font-size:10px; text-align: justify'>");
            // Sello SAT
            builder.Append(Cfdi.Complement.TaxStamp.CfdiSign);
            //end Sello SAT
            builder.Append(@"</p></div>");
            builder.Append(@"<div style='text-align: left;'>");
            builder.Append(@"<p style='margin-bottom: 0px;'><b>Cadena Original del Complemento de Certificación del SAT:</b></p></div>");
            builder.Append(@"<div style='text-align: left; width:100%;'>");
            builder.Append(@"<p style='font-family:\""Montserrat\"", sans-serif; width: 100%;overflow-wrap: break-word; margin-top: 0px; font-size:10px; text-align: justify'>");
            //Cadena Original
            var cadenaOriginal = string.Format("||1|{0}|{1}|{2}|ESO1202108R2|{3}|{4}||", Cfdi.Complement.TaxStamp.Uuid.ToString().Trim(), Cfdi.Complement.TaxStamp.SatSign.ToString().Trim(), Cfdi.Date.ToString().Trim(), Cfdi.Complement.TaxStamp.SatSign.ToString().Trim(), Cfdi.Complement.TaxStamp.SatCertNumber.ToString().Trim());
            //builder.Append("||1|"+Cfdi.Complement.TaxStamp.Uuid+"|"+Cfdi.Complement.TaxStamp.SatSign+"|"+Cfdi.Date+"|ESO1202108R2|"+Cfdi.Complement.TaxStamp.SatSign+"|"+Cfdi.Complement.TaxStamp.SatCertNumber+"||");
            builder.Append(cadenaOriginal);
            // end Cadena Original
            builder.Append(@"</p></div>");
            builder.Append(@"</div></div>");
            builder.Append(@"<div class='firma_y_sello' style='margin-bottom:50px; margin-top: 100px;'>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width:60%;'>");
            builder.Append(@"<p style='font-size:11px;'><b>ESTE DOCUMENTO ES UNA REPRESENTACION IMPRESA DE UN CFDI. V 3.3EL PAGO DE ESTE RECIBO NO LO LIBERA DE ADEUDOS ANTERIORES.");
            builder.Append(@"CUALQUIER ACLARACION SOBRE ESTE RECIBO ES VALIDA SOLO EN LOS SIGUIENTES CINCO DIAS DE QUE FUE EXPEDIDO.</b></p></div>");
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 30%;'>");
            builder.Append(@"<p style='text-align: center; padding-top: 10px;border-top-style: solid;border-top-color: black;'>");
            builder.Append(@"FIRMA Y SELLO DEL CAJERO</p>");
            builder.Append(@"</div></div>");
            builder.Append(@"</body>");
            builder.Append(@"</html>");

            //TextWriter txt = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"");
            //TextWriter txt = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Prueba.html"));
            //string appPatht = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "OutputTem","IMG");
            //if (!Directory.Exists(appPatht))
            //{
            //    Directory.CreateDirectory(appPatht);
            //    //    byte[] buffer = new byte[Resources.ayuntamiento.Save()];
            //    Resources.ayuntamiento.Save(appPatht);
            //}
            //else
            //{

            //}
            //txt.Write(builder);
            //txt.Close();

            return builder.ToString();
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Prueba.html");

        }

        //Genera Pdf para Ordenes
        private string HtmlToPdfForOrders()
        {
            StringBuilder builder = new StringBuilder();
            bool havtax = false;
            Numalet let = new Numalet();
            let.MascaraSalidaDecimal = "00/100 M.N";
            let.SeparadorDecimalSalida = "pesos";
            let.LetraCapital = true;
            let.ApocoparUnoParteEntera = true;
            string QrFileName = Account + Cfdi.Complement.TaxStamp.SatCertNumber;
            decimal total = CfdiMulti.Items.Sum(x => x.Total);
            string last = Cfdi.Complement.TaxStamp.CfdiSign.Substring(Cfdi.Complement.TaxStamp.CfdiSign.Length - 8);
            CfdiMulti.Items.ForEach(x =>
            {
                if (x.Taxes != null)
                {
                    havtax = true;
                }
            });
            //if (havtax)
            //{

            //}
            Model.Division Div = _lstDivision.Where(x => x.Id == Variables.LoginModel.Divition).FirstOrDefault();
            string tmpDivision = Div == null ? "" : Div.Name;

            GeneraCodigoQR(Cfdi.Complement.TaxStamp.Uuid,
                           CfdiMulti.Issuer.Rfc,
                           CfdiMulti.Receiver.Rfc,
                           total.ToString().PadLeft(18, '0').PadRight(6, '0'),
                           last,
                           QrFileName);

            builder.Append(@"<html>");
            builder.Append(@"<html lang='es'><head>");
            builder.Append(@"<meta charset='UTF-8'>");
            builder.Append(@"<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
            builder.Append(@"<meta http-equiv='X-UA-Compatible' content='ie=edge'>");
            builder.Append(@"<link href='https://fonts.googleapis.com/css?family=Montserrat|Roboto&display=swap' rel='stylesheet'>");
            builder.Append(@"<title>Facturación</title></head>");
            builder.Append(@"<body style='margin: 40px; font-size: 10px;'>");
            builder.Append(@"<div style='font-family: \""Roboto\"", sans-serif; height: 100px;'>");
            builder.Append(@"<div class='cabecera_principal' style='margin-bottom: 15px;'>");
            builder.Append(@"<div style='text-align: center; display: inline-block; width: 15%; vertical-align: top;'>");
            if (Variables.Configuration.IsMunicipal)
            {
                builder.Append(@"<img src='" + AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", "Resources") + @"ayuntamiento.jpg" + "' alt='Logo-Ayuntamiento' width='200' height='200'>");
            }

            else
            {
                builder.Append(@"<img src='" + AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", "Resources") + @"sosapac.png" + "' alt='Logo-SOSAPAC' width='200' height='200'>");
            }

            builder.Append(@"</div>");
            builder.Append(@"<div style='text-align: center; display: inline-block; width: 55%; font-size: 14px;'>");
            if (Variables.Configuration.IsMunicipal)
            {
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'><b>" + CfdiMulti.Issuer.Name + "</b></p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'><b>RFC: " + CfdiMulti.Issuer.Rfc + "</b></p><br>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'> PALACIO MUNICIPAL  S/N Centro CUAUTLANCINGO CUAUTLANCINGO 72700<br>PUEBLA MEXICO </p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'>Teléfono: (222) 2-85-13-62</p><br>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'>Régimen Fiscal: " + CfdiMulti.Issuer.FiscalRegime + " - Personas Morales con Fines no Lucrativos</p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'>Expedido en: 72700, CUAUTLANCINGO</p>");
                builder.Append(@"<p style='font-size: 16px;'><b>TESORERIA MUNICIPAL</b></p>");
                builder.Append(@"<p style='font-size: 16px;'><b>" + tmpDivision + "</b></p></div>");
            }
            else
            {
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'> <b>" + CfdiMulti.Issuer.Name + "</b></p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'><b>RFC: " + CfdiMulti.Issuer.Rfc + "</b></p><br>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'> SAN LORENZO 84  A CENTRO CUAUTLANCINGO 72700 <br>PUEBLA MEXICO </p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'>Teléfono: (222) 2269761</p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'>Email: contacto@sosapac.gob.mx </p>");
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 16px;'><b>COMPROBANTE DE PAGO</b></p></div>");
            }

            builder.Append(@" <div id='datFact' style='display: inline-block; width: 20%; font-size: 11px; text-align: center; vertical-align: top;'>");
            builder.Append(@"<table style='text-align: center; font-size: 14px;'>");
            builder.Append(@"<tr><td><b>COMPROBANTE</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'><b>" + CfdiMulti.Folio + "</b></td></tr>"); //Folio
            builder.Append(@"<tr><td><b>FOLIO FISCAL</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif; font-size:11px;'>" + Cfdi.Complement.TaxStamp.Uuid + "</td></tr>"); //UUID
            builder.Append(@"<tr><td><b>CERTIFICADO SAT</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'>" + Cfdi.Complement.TaxStamp.SatCertNumber + "</td></tr>");// certificado sat
            builder.Append(@"<tr><td><b>FECHA AUTORIZACIÓN SAT</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'>" + Cfdi.Date + "</td></tr>");//fecha autorización
            builder.Append(@"<tr><td><b>FECHA EMISION</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'>" + CfdiMulti.Date + "</td></tr>");//Fecha Emision
            builder.Append(@"</table>");
            builder.Append(@"</div></div>");
            builder.Append(@"<div class='datos_contribuyente' style='margin-bottom: 15px;'>");
            builder.Append(@"<table style='font-size: 14px;>");
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 120px;'><b>No. de cuenta:</b></td>");
            builder.Append(@"<td style='width: 100px; font-family:\""Montserrat\"", sans-serif;'>" + _orderSale.Folio + "</td>"); //Cuenta

            //if (Variables.Configuration.IsMunicipal)
            //{
            //    if (Variables.LoginModel.Divition == 6)
            //    {
            //        builder.Append(@"<td style='width: 50px;'><b>Tipo Predio:</b></td>");
            //        builder.Append(@"<td style='width: 180px; font-family:\""Montserrat\"", sans-serif;'>" + _agreement.TypeIntake.Name + "</td>"); //Uso
            //    }
            //}
            //else
            //{
            //    builder.Append(@"<td style='width: 50px;'><b>Uso:</b></td>");
            //    builder.Append(@"<td style='width: 180px; font-family:\""Montserrat\"", sans-serif;'>" + _agreement.TypeIntake.Name.ToUpper() + "-" + _agreement.TypeConsume.Name.ToUpper() + "</td>"); //Uso
            //    builder.Append(@"<td style='width: 60px;'><b>Ruta:</b></td>");
            //    builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>" + _agreement.Route + "</td>"); //Ruta
            //}

            //informacion del receptor
            var tmpDir = _orderSale.TaxUser.TaxAddresses?.FirstOrDefault();
            string Direccion;
            if (tmpDir != null)
                Direccion = string.Format("{0} Num. {1}{2} Col: {3} {4}, {5}", tmpDir.Street, tmpDir.Outdoor, (string.IsNullOrEmpty(tmpDir.Indoor) ? "" : " Int: " + tmpDir.Indoor), tmpDir.Suburb, tmpDir.Zip, tmpDir.Town);
            else
                Direccion = "Sin dirección";

            builder.Append(@"</tr>");
            builder.Append(@"</table>");
            builder.Append(@"<table style='font-size: 14px;'>");
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 120px;'><b>Periodo:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>" + CfdiMulti.PaymentConditions + "</td>"); //Periodo
            builder.Append(@"</tr>");
            builder.Append(@"<tr><td style='width: 120px;'><b>Contribuyente:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>" + CfdiMulti.Receiver.Name + "</td></tr>"); //Contribuyente
            builder.Append(@"<tr><td style='width: 120px;'><b>RFC:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>" + CfdiMulti.Receiver.Rfc + "</td></tr>"); //RFC
            builder.Append(@"<tr><td style='width: 120px;'><b>Uso CFDI:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>" + UsoCFDI + "</td>"); //Uso CFDI
            builder.Append(@"</tr><tr><td style='width: 120px;'><b>Dirección:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>" + Direccion + "</td></tr>"); //Dirección
            builder.Append(@"</table>");

            //if (Variables.Configuration.IsMunicipal && Variables.LoginModel.Divition == 6)
            //{
            //    builder.Append(@"<br><table style='width: 100%;'>");
            //    builder.Append(@"<th>Terreno: " + _agreement.AgreementDetails.FirstOrDefault().Ground + "</th>");
            //    builder.Append(@"<th>Construcción: " + _agreement.AgreementDetails.FirstOrDefault().Built + "</th>");
            //    builder.Append(@"<th>Base: " + string.Format(new CultureInfo("es-MX"), "{0:C2}", _agreement.AgreementDetails.FirstOrDefault().TaxableBase) + "</th>");
            //    builder.Append(@"<th>Fecha Avalúo: " + _agreement.AgreementDetails.FirstOrDefault().LastUpdate + "</th>");
            //    builder.Append(@"</table>");
            //}

            builder.Append(@"</div>");
            builder.Append(@"<div class='datos_conceptos' style='margin-bottom: 10px;'>");
            builder.Append(@"<table style='width: 100%; font-size:13px;'>");
            builder.Append(@"<tr style='background-color: black; color: white;'>");
            builder.Append(@"<th>CLAVEPROD. SERV.</th>");
            builder.Append(@"<th>UNIDAD DE MEDIDA</th>");
            builder.Append(@"<th>DESCRIPCION</th>");
            builder.Append(@"<th>CANTIDAD</th>");
            builder.Append(@"<th>VALOR UNITARIO</th>");
            //builder.Append(@"<th>IVA TRASLADO %16</th>");
            builder.Append(@"<th>DESC.</th>");
            builder.Append(@"<th>IMPORTE</th>");
            builder.Append(@" </tr>");
            //Foreach Concepts
            int cont = 0;
            CfdiMulti.Items.ForEach(x =>
            {
                builder.Append(@"<tr>");
                builder.Append(@"<td>" + CfdiMulti.Items[cont].ProductCode + "</td>");
                builder.Append(@"<td>" + CfdiMulti.Items[cont].UnitCode + "</td>");
                builder.Append(@"<td>" + CfdiMulti.Items[cont].Description + "</td>");
                builder.Append(@"<td>" + CfdiMulti.Items[cont].Quantity + "</td>");
                builder.Append(@"<td>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", CfdiMulti.Items[cont].UnitPrice) + "</td>");
                builder.Append(@"<td>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", CfdiMulti.Items[cont].Discount) + "</td>");
                builder.Append(@"<td>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", CfdiMulti.Items[cont].Total) + "</td>");
                builder.Append(@"</tr>");
                cont++;
            });            
            builder.Append(@"</table></div>");

            builder.Append(@"<div class='datos_sub_moneda' style='margin-bottom:5px;'>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width:70%; font-size: 12px; font-family:\""Montserrat\"", sans-serif;'>");
            CfdiMulti.Items.ForEach(x =>
            {
                if (x.Taxes != null)
                {
                    havtax = true;
                }
            });
            if (!havtax)
            {
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size:14px;'>" + let.ToCustomCardinal(CfdiMulti.Items.Sum(x => x.Total)).ToUpperInvariant() + "</p></div>"); //Numero a letras
            }
            else
            {
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size:14px;'>" + let.ToCustomCardinal((CfdiMulti.Items.Sum(x => x.Total) + CfdiMulti.Items.Sum(x => x.Taxes.Sum(c => c.Total)))).ToUpperInvariant() + "</p></div>"); //Numero a letras
            }
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 20%; font-size: 14px; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px;'>SubTotal: </p></div>");
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 5%; font-size: 14px; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p style='margin-top: 20px;margin-bottom: 0px; font-size: 14px;'>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", CfdiMulti.Items.Sum(x => x.Total)) + "</p></div></div>"); //subtotal
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 90%; font-size: 14px;'>");
            builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 14px;'>IVA:</p></div>");
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 4.3%; font-size: 14px;'>");
            if (havtax)
            {
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 14px;'>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", CfdiMulti.Items.Sum(x => x.Taxes.Sum(c => c.Total))) + "</p></div>");
            }
            else
            {
                builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px; font-size: 14px;'>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", 0) + "</p></div>");
            }

            builder.Append(@"<div class='datos_moneda' style='margin-bottom: 10px;'>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width:10%; font-size: 14px;'>");
            builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px;'><b>Mondeda:</b></p></div>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width:60%; font-size: 14px; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px;'>MXN</p></div>"); //tipo Moneda
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 19.6%; font-size: 14px; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p style='margin-top: 0px;margin-bottom: 0px;'>Total:</p></div>");
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 5.1%; font-size: 14px; font-family:\""Montserrat\"", sans-serif;'>");
            if (havtax)
            {
                builder.Append(@"<p style='margin-top: 3px;margin-bottom: 7px; font-size: 14px;'>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", (CfdiMulti.Items.Sum(x => x.Total) + CfdiMulti.Items.Sum(x => x.Taxes.Sum(c => c.Total)))) + "</p></div>"); //Total
            }
            else
            {
                builder.Append(@"<p style='margin-top: 3px;margin-bottom: 7px; font-size: 14px;'>" + string.Format(new CultureInfo("es-MX"), "{0:C2}", CfdiMulti.Items.Sum(x => x.Total)) + "</p></div>"); //Total
            }

            builder.Append(@"</div>");
            builder.Append(@"<div class='datos_comprobante' style='margin-bottom: 30px;'><br>");
            builder.Append(@"<table style='font-size:14px;'>");
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 180px;'><b>Tipo de comprobante:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>I-INGRESO</td></tr>"); //tipo de comprobante
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 180px;'><b>Forma de pago:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>01, EFECTIVO</td></tr>"); //Forma de Pago
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 180px;'><b>Método de pago:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>PUE, Pago en una sola exhibición</td></tr>"); //Como se realizo el pago
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 180px;'><b>No. serie certificado:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>00001000000410637078</td></tr>"); //No Certificado
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 180px;'><b>Observaciones:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>" + ObservacionCFDI + "</td></tr>"); //Oberservaciones
            builder.Append(@"</table>");
            builder.Append(@"</div>");
            builder.Append(@"<div class='sello_digital' style='margin-bottom: 30px;'>");
            builder.Append(@"<div style='text-align: center; display: inline-block; width: 15%; vertical-align: top; margin-top: 29px; margin-right: 19px;'>");
            //QR image 
            builder.Append(@"<img src='" + AppDomain.CurrentDomain.BaseDirectory + @"QR\" + QrFileName + ".png' alt='Logo-Ayuntamiento' width='180' height='180'>");
            //end QR
            builder.Append(@"</div>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width: 76%;'>");
            builder.Append(@"<div style='text-align: left;'>");
            builder.Append(@"<p><b>RFCProvCert: ESO1202108R2</b></p></div>");
            builder.Append(@"<div style='text-align: left;'>");
            builder.Append(@"<p style='margin-bottom: 0px;'><b>Sello del SAT</b></p>");
            builder.Append(@"</div>");
            builder.Append(@"<div style='text-align: left; width:100%;'>");
            builder.Append(@"<p style='font-family:\""Montserrat\"", sans-serif; width: 100%;overflow-wrap: break-word; margin-top: 0px; font-size:10px; text-align: justify;'>");
            //Cetificado RFCProvCert
            builder.Append(Cfdi.Complement.TaxStamp.SatSign.ToString());
            // end RFCProvCert
            builder.Append(@"</p></div>");
            builder.Append(@"<div style='text-align: left;'>");
            builder.Append(@"<p style='margin-bottom: 0px;'><b>Sello Digital del CFDI:</b></p></div>");
            builder.Append(@"<div style='text-align: left; width:100%;'>");
            builder.Append(@"<p style='font-family:\""Montserrat\"", sans-serif; width: 100%;overflow-wrap: break-word; margin-top: 0px; font-size:10px; text-align: justify'>");
            // Sello SAT
            builder.Append(Cfdi.Complement.TaxStamp.CfdiSign);
            //end Sello SAT
            builder.Append(@"</p></div>");
            builder.Append(@"<div style='text-align: left;'>");
            builder.Append(@"<p style='margin-bottom: 0px;'><b>Cadena Original del Complemento de Certificación del SAT:</b></p></div>");
            builder.Append(@"<div style='text-align: left; width:100%;'>");
            builder.Append(@"<p style='font-family:\""Montserrat\"", sans-serif; width: 100%;overflow-wrap: break-word; margin-top: 0px; font-size:10px; text-align: justify'>");
            //Cadena Original
            builder.Append("||1|" + Cfdi.Complement.TaxStamp.Uuid + "|" + Cfdi.Complement.TaxStamp.SatSign + "|" + Cfdi.Date + "|ESO1202108R2|" + Cfdi.Complement.TaxStamp.SatSign + "|" + Cfdi.Complement.TaxStamp.SatCertNumber + "||");
            // end Cadena Original
            builder.Append(@"</p></div>");
            builder.Append(@"</div></div>");
            builder.Append(@"<div class='firma_y_sello' style='margin-bottom:50px; margin-top: 100px;'>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width:60%;'>");
            builder.Append(@"<p style='font-size:11px;'><b>ESTE DOCUMENTO ES UNA REPRESENTACION IMPRESA DE UN CFDI. V 3.3EL PAGO DE ESTE RECIBO NO LO LIBERA DE ADEUDOS ANTERIORES.");
            builder.Append(@"CUALQUIER ACLARACION SOBRE ESTE RECIBO ES VALIDA SOLO EN LOS SIGUIENTES CINCO DIAS DE QUE FUE EXPEDIDO.</b></p></div>");
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 30%;'>");
            builder.Append(@"<p style='text-align: center; padding-top: 10px;border-top-style: solid;border-top-color: black;'>");
            builder.Append(@"FIRMA Y SELLO DEL CAJERO</p>");
            builder.Append(@"</div></div>");
            builder.Append(@"</body>");
            builder.Append(@"</html>");

            //TextWriter txt = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"");
            //TextWriter txt = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Prueba.html"));
            //string appPatht = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "OutputTem","IMG");
            //if (!Directory.Exists(appPatht))
            //{
            //    Directory.CreateDirectory(appPatht);
            //    //    byte[] buffer = new byte[Resources.ayuntamiento.Save()];
            //    Resources.ayuntamiento.Save(appPatht);
            //}
            //else
            //{

            //}
            //txt.Write(builder);
            //txt.Close();

            return builder.ToString();
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Prueba.html");

        }

        private void GeneraCodigoQR(string UUID, string Emisor, string Receptor, string total, string UltimosDigitos, string fileName)
        {
            string Cadena = string.Format("https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?&id={0}&re={1}&rr={2}&tt={3}&fe={4}", UUID, Emisor, Receptor, total, UltimosDigitos);

            var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            var qrCode = qrEncoder.Encode(Cadena);
            var renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            using (var stream = new FileStream("qrcode.png", FileMode.Create))
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
            MemoryStream ms = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemp = new Bitmap(ms);
            var image = new Bitmap(imageTemp, new Size(new Point(300, 300)));
            //string appPatht = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "OutputTem", "IMG");
            //string appPatht = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\QR";
            string appPatht = AppDomain.CurrentDomain.BaseDirectory + "QR";
            if (!Directory.Exists(appPatht))
            {
                Directory.CreateDirectory(appPatht);
                image.Save(appPatht+"\\" + fileName + ".png");
            }
            else
            {
                image.Save(appPatht + "\\" + fileName + ".png");
            }
           
            //image.Save("c:\\Pruebas\\" + Emisor + "_" + Receptor + ".png");
        }

        private void CopyResource(string resourceName, string file)
        {
            using (Stream resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (resource == null)
                {
                    throw new ArgumentException("No such resource", "resourceName");
                }
                using (Stream output = File.OpenWrite(file))
                {
                    resource.CopyTo(output);
                }
            }
        }
        private void ExportGridToPDF(byte[] pdf)
        {
            SaveFileDialog SaveXMLFileDialog = new SaveFileDialog();
            SaveXMLFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            SaveXMLFileDialog.FilterIndex = 2;
            SaveXMLFileDialog.RestoreDirectory = true;
            SaveXMLFileDialog.Title = "Exportar PDF de Factura";
            if (SaveXMLFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllBytes(SaveXMLFileDialog.FileName, pdf);
            }
        }
    }
}

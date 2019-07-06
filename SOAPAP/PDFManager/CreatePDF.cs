using HiQPdf;
using SOAPAP.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.PDFManager
{
    public class CreatePDF
    {
        public void Create()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                //HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
                //htmlToPdfConverter.SerialNumber = Properties.Settings.Default.SerialNumber;
                //htmlToPdfConverter.ConvertedHtmlElementSelector = HtmlToPdf();
                //htmlToPdfConverter.WaitBeforeConvert = 2;
                //htmlToPdfConverter.ConvertUrlToFile();
                //htmlToPdfConverter.Document.PageSize = PdfPageSize.Letter;
                //htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;
                //htmlToPdfConverter.Document.PdfStandard = PdfStandard.Pdf;
                //htmlToPdfConverter.Document.Margins = new PdfMargins(5);
                //htmlToPdfConverter.Document.FontEmbedding = true;
                //htmlToPdfConverter.TriggerMode = ConversionTriggerMode.WaitTime;
                //htmlToPdfConverter.WaitBeforeConvert = 2;
                //ExportGridToPDF(htmlToPdfConverter.ConvertUrlToMemory(HtmlToPdf()));
                //PdfHtml html = new PdfHtml(HtmlToPdf(), null);

                //string pdfFile = null;
                //string htmlCode = HtmlToPdf();
                //pdfFile = "C:\\Pruebas\\Test.pdf";
                //htmlToPdfConverter.ConvertHtmlToFile(htmlCode, "", pdfFile);
                //var a = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, null);
                //try
                //{
                //    System.Diagnostics.Process.Start(pdfFile);
                //}
                //catch (Exception)
                //{

                //    throw;
                //}


                PdfDocument document = new PdfDocument();
                document.SerialNumber = Properties.Settings.Default.SerialNumber;
                PdfPage page1 = document.AddPage(PdfPageSize.Letter, new PdfDocumentMargins(5), PdfPageOrientation.Portrait);
                PdfHtml html = new PdfHtml(HtmlToPdf(), null);
                html.WaitBeforeConvert = 2;
                PdfLayoutInfo layoutInfo = page1.Layout(html);
                byte[] pdfBuffer = document.WriteToMemory();
                ExportGridToPDF(pdfBuffer);

                //PdfDocument document = new PdfDocument();
                //document.SerialNumber = Properties.Settings.Default.SerialNumber;
                //PdfPage page1 = document.AddPage();

                //PdfLayoutInfo textLayoutInfo = null;

                ////PdfRectangle backgroundRectangle = new PdfRectangle(page1.DrawableRectangle);
                ////backgroundRectangle.BackColor = Color.WhiteSmoke;
                ////page1.Layout(backgroundRectangle);

                //PrivateFontCollection collection = new PrivateFontCollection();
                //collection.AddFontFile(@"C:\fonts\Montserrat\Montserrat-Regular.ttf");
                //collection.AddFontFile(@"C:\fonts\Roboto\Roboto-Light.ttf");
                //collection.AddFontFile(@"C:\fonts\Roboto\Roboto-Bold.ttf");

                ////Title Principal Roboto-Bold
                //Font sysFontTitleBold = new Font(collection.Families[1], 9, System.Drawing.GraphicsUnit.Point);
                //PdfFont pdfFontTitleBold = document.CreateFont(sysFontTitleBold);
                //PdfFont pdfFontEmbedTitleBold = document.CreateFont(sysFontTitleBold, true);

                //Font sysFontTitleRegular = new Font(collection.Families[2], 8, System.Drawing.GraphicsUnit.Point);
                //PdfFont pdfFontTitleRegular = document.CreateFont(sysFontTitleRegular);
                //PdfFont pdfFontEmbedTitleRegular = document.CreateFont(sysFontTitleRegular, true);


                ////Text from Data Montserrat-Regular
                //Font sysFontText = new Font(collection.Families[0], 6, System.Drawing.GraphicsUnit.Point);
                //PdfFont pdfFont = document.CreateFont(sysFontText);
                //PdfFont pdfFontEmbed = document.CreateFont(sysFontText, true);

                ////Text accentuate Roboto-Bold
                //Font sysFontTextData = new Font(collection.Families[1], 6, System.Drawing.GraphicsUnit.Point);
                //PdfFont pdfFontData = document.CreateFont(sysFontText);
                //PdfFont pdfFontEmbedData = document.CreateFont(sysFontText, true);


                //float crtYPos = 10;
                //float crtXPos = 10;

                ////TextCenter
                //float crtYPosCenter = 31;
                //float crtXPosCenter = 170;

                //PdfImage transparentPdfImage = new PdfImage(crtXPos, crtYPos, 120, Resources.ayuntamiento);
                //PdfLayoutInfo imageLayoutInfo = page1.Layout(transparentPdfImage);
                //crtYPos += imageLayoutInfo.LastPageRectangle.Height + 10;

                //PdfText titleTextAtLocation = new PdfText(crtXPosCenter, crtYPosCenter, "MUNICIPIO DE CUAUTLANCINGO, PUEBLA 2018-2021", pdfFontEmbedTitleBold);
                //titleTextAtLocation.ForeColor = Color.Black;
                //textLayoutInfo = page1.Layout(titleTextAtLocation);

                //titleTextAtLocation = new PdfText(crtXPosCenter + 60, crtYPosCenter += 12, "RFC: MCP850101944", pdfFontEmbedTitleBold);
                //titleTextAtLocation.ForeColor = Color.Black;
                //textLayoutInfo = page1.Layout(titleTextAtLocation);


                //PdfText AddressT = new PdfText(crtXPosCenter - 28, crtYPosCenter += 20, "PALACIO MUNICIPAL S/N Centro CUAUTLANCINGO CUAUTLANCINGO 72700", pdfFontEmbedTitleRegular);
                //titleTextAtLocation.ForeColor = Color.Black;
                //textLayoutInfo = page1.Layout(AddressT);

                //AddressT = new PdfText(crtXPosCenter + 80, crtYPosCenter += 12, "PUEBLA MEXICO", pdfFontEmbedTitleRegular);
                //titleTextAtLocation.ForeColor = Color.Black;
                //textLayoutInfo = page1.Layout(AddressT);

                //AddressT = new PdfText(crtXPosCenter, crtYPosCenter += 20, "Régimen Fiscal: 603 - Personas Morales con Fines no Lucrativos", pdfFontEmbedTitleRegular);
                //titleTextAtLocation.ForeColor = Color.Black;
                //textLayoutInfo = page1.Layout(AddressT);

                //AddressT = new PdfText(crtXPosCenter + 45, crtYPosCenter += 12, "Expedido en: 72700, CUAUTLANCINGO", pdfFontEmbedTitleRegular);
                //titleTextAtLocation.ForeColor = Color.Black;
                //textLayoutInfo = page1.Layout(AddressT);

                ////PdfRectangle borderPdfRectangle = new PdfRectangle(crtXPosCenter, crtYPosCenter += 16, 230, 15);
                ////borderPdfRectangle.LineStyle.LineWidth = 0.1f;
                ////page1.Layout(borderPdfRectangle);

                //PdfText divition = new PdfText(crtXPosCenter + 50, crtYPosCenter +=12 , "TESORERIA MUNICIPAL", pdfFontEmbedTitleBold);
                //titleTextAtLocation.ForeColor = Color.Black;
                //page1.Layout(divition);

                //divition = new PdfText(crtXPosCenter, crtYPosCenter +=12, "ÁREA: TESORERIA MUNICIPAL", pdfFontEmbedTitleBold);
                //titleTextAtLocation.ForeColor = Color.Black;
                //page1.Layout(divition);



                //ExportGridToPDF(document.WriteToMemory());


            }
            catch (Exception e)
            {

                throw;
            }
        }


        private string HtmlToPdf()
        {
            StringBuilder builder = new StringBuilder();
            //var a = Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources");
           
            //string appPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "OutputTem", "IMG");
            //CopyResource(nameof(Properties.Resources.ayuntamiento), appPath);
            //if (!Directory.Exists(appPath))
            //{
            //    Directory.CreateDirectory(appPath);
            //    Bitmap bitmap = new Bitmap(Resources.ayuntamiento);
            //    Graphics gBmp = Graphics.FromImage(bitmap);
            //    CopyResource("Ayuntamiento.jpg", appPath);
            //    //File.WriteAllBytes(appPath,image);
            //}
            builder.Append(@"<html>");
            builder.Append(@"<html lang='es'><head>");
            builder.Append(@"<meta charset='UTF-8'>");
            builder.Append(@"<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
            builder.Append(@"<meta http-equiv='X-UA-Compatible' content='ie=edge'>");
            builder.Append(@"<link href='https://fonts.googleapis.com/css?family=Montserrat|Roboto&display=swap' rel='stylesheet'>");
            builder.Append(@"<title>Facturación</title></head>");
            builder.Append(@"<body style='margin: 40px;'>");
            builder.Append(@"<div style='font-family: \""Roboto\"", sans-serif; height: 100px;'>");
            builder.Append(@"<div class='cabecera_principal' style='margin-bottom: 60px;'>");
            builder.Append(@"<div style='text-align: center; display: inline-block; width: 15%; vertical-align: top;'>");           
            builder.Append(@"<img src='" + AppDomain.CurrentDomain.BaseDirectory + @"Resources\ayuntamiento.jpg" + "' alt='Logo-Ayuntamiento' width='100' height='100'>");
            builder.Append(@"</div>");
            builder.Append(@"<div style='text-align: center; display: inline-block; width: 55%;'>");
            builder.Append(@"<p> <b>MUNICIPIO DE CUAUTLANCINGO, PUEBLA 2018-2021</b></p>");
            builder.Append(@"<p style='position: relative; bottom:13px;'><b>RFC: MCP850101944</b></p><br><br>");
            builder.Append(@"<p style='position: relative; bottom:27px;'> PALACIO MUNICIPAL  S/N Centro CUAUTLANCINGO CUAUTLANCINGO 72700<br>PUEBLA MEXICO </p>");
            builder.Append(@"<div style='text-align: center; display: inline-block; width: 55%;'>");
            builder.Append(@"<table>");
            builder.Append(@"<tr style='text-align: center;'><td>Régimen Fiscal: 603 - Personas Morales con Fines no Lucrativos</td></tr>");
            builder.Append(@"<tr style='text-align: center;'><td>Expedido en: 72700, CUAUTLANCINGO</td></tr>");
            builder.Append(@"<tr style='text-align: center;'><td><b>TESORERIA MUNICIPAL</b></td></tr>");
            builder.Append(@"</table>");
            builder.Append(@"</div>");
            builder.Append(@"</div>");
            builder.Append(@" <div id='datFact' style='display: inline-block; width: 20%; font-size: 12px; text-align: center; vertical-align: top;'>");
            builder.Append(@"<table>");
            builder.Append(@"<tr><td><b>COMPROBANTE</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'><b>CM3-35032</b></td></tr>"); //Folio
            builder.Append(@"<tr><td><b>FOLIO FISCAL</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'>1E468CCF-2DDE-DC9A-2383-DB2B68AA0402</td></tr>"); //UUID
            builder.Append(@"<tr><td><b>CERTIFICADO SAT</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'>00001000000402636111</td></tr>");// certificado sat
            builder.Append(@"<tr><td><b>FECHA AUTORIZACIÓN SAT</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'>2019-05-07T13:03:29</td></tr>");//fecha autorización
            builder.Append(@"<tr><td><b>FECHA EMISION</b></td></tr>");
            builder.Append(@"<tr><td style='font-family:\""Montserrat\"", sans-serif;'>07/05/2019 12:59:36 p. m.</td></tr>");//Fecha Emision
            builder.Append(@"</table>");
            builder.Append(@"</div></div>");
            builder.Append(@"<div class='datos_contribuyente' style='margin-bottom: 60px;'>");
            builder.Append(@"<table>");
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 120px;'><b>No. de cuenta:</b></td>");
            builder.Append(@"<td style='width: 100px; font-family:\""Montserrat\"", sans-serif;'>28712</td>"); //Cuenta
            builder.Append(@"<td style='width: 50px;'><b>Uso:</b></td>");
            builder.Append(@"<td style='width: 180px; font-family:\""Montserrat\"", sans-serif;'>HABITACIONAL</td>"); //Uso
            builder.Append(@"<td style='width: 60px;'><b>Ruta:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>181</td>"); //Ruta
            builder.Append(@"</tr>");
            builder.Append(@"</table>");
            builder.Append(@"<table>");
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 120px;'><b>Periodo:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>AGOSTO 2018 - DICIEMBRE 2018</td>"); //Periodo
            builder.Append(@"</tr>");
            builder.Append(@"<tr><td style='width: 120px;'><b>Contribuyente:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>FLORES FLORES JOSE RAFAEL</td></tr>"); //Contribuyente
            builder.Append(@"<tr><td style='width: 120px;'><b>RFC:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>XAXX010101000</td></tr>"); //RFC
            builder.Append(@"<tr><td style='width: 120px;'><b>Uso CFDi:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>P01, Por definir</td></tr>"); //Uso CFDI
            builder.Append(@"<tr><tr><td style='width: 120px;'><b>Periodo:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>CALLE HUEHUETAN NO. 36  ETAPA 8 MISIONES DE SAN FRANCISCO SAN FRANCISCO OCOTLAN CORONANGO MEXICO</td></tr>"); //Dirección
            builder.Append(@"</table>");
            builder.Append(@"</div>");
            builder.Append(@"<div class='datos_conceptos' style='margin-bottom: 10px;'>");
            builder.Append(@"<table style='width: 100%;'>");
            builder.Append(@"<tr style='background-color: black; color: white;'>");
            builder.Append(@"<th>CLAVEPROD. SERV.</th>");
            builder.Append(@"<th>UNIDAD DE MEDIDA</th>");
            builder.Append(@"<th>DESCRIPCION</th>");
            builder.Append(@"<th>CANTIDAD</th>");
            builder.Append(@"<th>VALOR UNITARIO</th>");
            builder.Append(@"<th>IVA TRASLADO %16</th>");
            builder.Append(@"<th>DESC.</th>");
            builder.Append(@"<th>IMPORTE</th>");
            builder.Append(@" </tr>");
            //Foreach Concepts

            //End Foreach
            builder.Append(@"</table></div>");
            builder.Append(@"<div class='datos_sub_moneda' style='margin-bottom:5px;'>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width:70%; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p>QUINIENTOS ONCE PESOS 12/100 M.N.</p></div>"); //Numero a letras
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 20%; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p>SubTotal:</p></div>");
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 5%; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p>$277.10</p></div></div>"); //subtotal
            builder.Append(@"<div class='datos_moneda' style='margin-bottom: 10px;'>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width:10%;'>");
            builder.Append(@"<p><b>Mondeda:</b></p></div>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width:60%; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p>MXN</p></div>"); //tipo Moneda
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 20%; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p>Total:</p></div>");
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 5%; font-family:\""Montserrat\"", sans-serif;'>");
            builder.Append(@"<p>$277.10</p></div>"); //Total
            builder.Append(@"</div>");
            builder.Append(@"<div class='datos_comprobante' style='margin-bottom: 30px;'>");
            builder.Append(@"<table>");
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
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>00001000000412882009</td></tr>"); //No Certificado
            builder.Append(@"<tr>");
            builder.Append(@"<td style='width: 180px;'><b>Observaciones:</b></td>");
            builder.Append(@"<td style='font-family:\""Montserrat\"", sans-serif;'>-</td></tr>"); //Oberservaciones
            builder.Append(@"</table>");
            builder.Append(@"</div>");
            builder.Append(@"<div class='sello_digital' style='margin-bottom: 30px;'>");
            builder.Append(@"<div style='text-align: center; display: inline-block; width: 15%; vertical-align: top;'>");
            //QR image 

            //end QR
            builder.Append(@"</div>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width: 80%;'>");
            builder.Append(@"<div style='text-align: left;'>");
            builder.Append(@"<p><b>RFCProvCert:</b></p></div>");
            builder.Append(@"<div style='text-align: left; width:60%;'>");
            builder.Append(@"<p style='font-family:\""Montserrat\"", sans-serif;'>");
            //Cetificado RFCProvCert

            // end RFCProvCert
            builder.Append(@"</p></div>");
            builder.Append(@"<div style='text-align: left;'>");
            builder.Append(@"<p><b>Sello del SAT:</b></p></div>");
            builder.Append(@"<div style='text-align: left; width:60%;'>");
            builder.Append(@"<p style='font-family:\""Montserrat\"", sans-serif;'>");
            // Sello SAT

            //end Sello SAT
            builder.Append(@"</p></div>");
            builder.Append(@"<div style='text-align: left;'>");
            builder.Append(@"<p><b>Cadena Original del Complemento de Certificación del SAT:</b></p></div>");
            builder.Append(@"<div style='text-align: left; width:60%;'>");
            builder.Append(@"<p style='font-family:\""Montserrat\"", sans-serif;'>");
            //Cadena Original

            // end Cadena Original
            builder.Append(@"</p></div>");
            builder.Append(@"</div></div>");
            builder.Append(@"<div class='firma_y_sello' style='margin-bottom:50px; margin-top: 200px;'>");
            builder.Append(@"<div style='text-align: left; display: inline-block; width:80%;'>");
            builder.Append(@"<p><b>ESTE DOCUMENTO ES UNA REPRESENTACION IMPRESA DE UN CFDI. V 3.3EL PAGO DE ESTE RECIBO NO LO LIBERA DE ADEUDOS ANTERIORES.");
            builder.Append(@"CUALQUIER ACLARACION SOBRE ESTE RECIBO ES VALIDA SOLO EN LOS SIGUIENTES CINCO DIAS DE QUE FUE EXPEDIDO.</b></p></div>");
            builder.Append(@"<div style='text-align: right; display: inline-block; width: 15%;'>");
            builder.Append(@"<p style='text-align: center; padding-top: 10px;border-top-style: solid;border-top-color: black;'>");
            builder.Append(@"FIRMA Y SELLO DEL CAJERO</p>");
            builder.Append(@"</div></div>");
            builder.Append(@"<div class='pie_de_pagina' style='margin-top: 100px;'>");
            builder.Append(@"<p>Powerd by GFDSYSTEMS</p></div>");
            builder.Append(@"</div>");
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

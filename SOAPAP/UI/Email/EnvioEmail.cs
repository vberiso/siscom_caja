using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SOAPAP.UI.Email
{
    public class EnvioEmail
    {
        public string Xml { get; set; }
        public string Account { get; set; }
        public string Taxpayer { get; set; }
        public bool SiscomBill { get; set; }
        public byte[] PDFFile { get; set; }

        public EnvioEmail()
        {

        }

        public EnvioEmail(string Account, string Taxpayer)
        {            
            this.Account = Account;
            this.Taxpayer = Taxpayer;            
        }

        public EnvioEmail(string Xml, string Account, string Taxpayer, bool SiscomBill, byte[] PDFFile)
        {           
            this.Xml = Xml;
            this.Account = Account;
            this.Taxpayer = Taxpayer;
            this.SiscomBill = SiscomBill;
            this.PDFFile = PDFFile;
        }

        public string Send_Email(string Destino)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = null;
                if (Variables.Configuration.IsMunicipal)
                {
                    SmtpServer = new SmtpClient("smtp.office365.com", 587);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("facturacion@cuautlancingo.gob.mx", "e0P?k0k8");
                    mail.From = new MailAddress("facturacion@cuautlancingo.gob.mx", string.Empty, System.Text.Encoding.UTF8);
                }
                else
                {
                    SmtpServer = new SmtpClient("smtp.office365.com", 587);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("dir_sistemas@cuautlancingo.gob.mx", "Pacifico201718");
                    mail.From = new MailAddress("sosapac@cuautlancingo.gob.mx", string.Empty, System.Text.Encoding.UTF8);
                }

                mail.To.Add(Destino);
                mail.Subject = "Envió de comprobante fiscal CFDI";
                mail.AlternateViews.Add(getEmbeddedImage());

                if (SiscomBill)
                {
                    Stream streamPDF = new MemoryStream(PDFFile);
                    Attachment attachmentPDF = new Attachment(streamPDF, string.Format("Comprobante_{0}.pdf", Account), "application/pdf");
                    mail.Attachments.Add(attachmentPDF);
                }
                MemoryStream memoryStream = new MemoryStream();
                XmlDocument xDocument = new XmlDocument();
                xDocument.LoadXml(Xml);
                xDocument.Save(memoryStream);
                memoryStream.Flush();//Adjust this if you want read your data 
                memoryStream.Position = 0;
                Stream stream = memoryStream;
                Attachment attachment = new Attachment(stream, string.Format("Comprobante_{0}.xml", Account), "application/xml");
                mail.Attachments.Add(attachment);

                SmtpServer.Send(mail);

                return "Envio Exitoso";
            }
            catch (Exception ex)
            {
                return "El correo electrónico no pudo enviarse, favor de comunicarse con el administrador del sistema";                
            }
        }


        public string Send_Email(string Destino, string pathFiles, string nameFile)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = null;
                if (Variables.Configuration.IsMunicipal)
                {
                    SmtpServer = new SmtpClient("smtp.office365.com", 587);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("facturacion@cuautlancingo.gob.mx", "e0P?k0k8");
                    mail.From = new MailAddress("facturacion@cuautlancingo.gob.mx", string.Empty, System.Text.Encoding.UTF8);
                }
                else
                {
                    SmtpServer = new SmtpClient("smtp.office365.com", 587);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("dir_sistemas@cuautlancingo.gob.mx", "Pacifico201718");
                    mail.From = new MailAddress("sosapac@cuautlancingo.gob.mx", string.Empty, System.Text.Encoding.UTF8);
                }

                mail.To.Add(Destino);
                mail.Subject = "Envió de comprobante fiscal CFDI";
                mail.AlternateViews.Add(getEmbeddedImage());

                if (File.Exists(string.Format("{0}\\{1}.{2}", pathFiles, nameFile, "pdf")))
                {
                    Attachment attPDF = new Attachment(string.Format("{0}\\{1}.{2}", pathFiles, nameFile, "pdf"));
                    mail.Attachments.Add(attPDF);
                }

                if (File.Exists(string.Format("{0}\\{1}.{2}", pathFiles, nameFile, "xml")))
                {
                    Attachment attXML = new Attachment(string.Format("{0}\\{1}.{2}", pathFiles, nameFile, "xml"));
                    mail.Attachments.Add(attXML);
                }
                SmtpServer.Send(mail);

                return "Envio Exitoso";
            }
            catch (Exception ex)
            {
                return "error: El correo electrónico no pudo enviarse, favor de comunicarse con el administrador del sistema";
            }
        }

        private AlternateView getEmbeddedImage()
        {
            string htmlBody = string.Empty;
            if (Variables.Configuration.IsMunicipal)
            {
                htmlBody = string.Format(@"<!DOCTYPE html>
                                             <html lang=""es"">
                                             <head>
                                                <meta charset=""UTF-8"">
                                                 <title>Facturación - Cuautlancingo</title>
                                                 <meta name=""viewport"" content=""width=device-width, initial-scale=1.0""/>
                                                 <link href=""https://fonts.googleapis.com/css?family=Montserrat|Roboto&display=swap"" rel=""stylesheet"">
                                             </head>
                                             <body style=""margin: 0; padding: 0;"">
                                                 <table style=""width: 100%; font-family: 'Roboto', sans-serif; letter-spacing: 0.50px;"">
                                                     <th style=""width: 7%;""><img src=""https://i.ibb.co/BGRJZGD/ayuntamiento-sistema.png"" alt=""Logo""></th>
                                                     <th style=""width: 93%;"">
                                                        <div style =""position: absolute !important; top: 26px; left:145px; color: #691A1B text-align: left;"">
                                                            <div><h2> Sistema de Facturación</h2></div>
                                                            <div style=""margin-bottom:25px;""><h5> Comprobante Fiscal CFDI</h5></div>
                                                        </div>
                                                        <div style=""background-color: #691A1B; width: 100%; height: 5px; position: relative; top: 35px;""></div>
                                                     </th>
                                                     <tr style=""font-family: 'Montserrat', sans-serif; "">
                                                        <th colspan=""2"" style=""text-align: left; font-size: 15px; padding-left: 30px;"">
                                                            <br><br>
                                                            <p>Estimado Contribuyente:&nbsp;&nbsp; <strong>{0}</strong></p>
                                                            <p>Cuenta:&nbsp;&nbsp; <strong>{1}</strong></p></p>
                                                            <br>
                                                        </th>
                                                     </tr>
                                                     <tr style=""font-family: 'Montserrat', sans-serif;"">
                                                         <th colspan=""2"" style=""text-align: justify; padding: 0px 80px 0px 80px;"">
                                                             <p> Usted está recibiendo un comprobante fiscal digital (FACTURA ELECTRÓNICA) del municipio de Cuautlancingo, Puebla 2018 - 2021. 
                                                                 De acuerdo a la reglamentación del servicio de administración tributaria (SAT), publicada en el diario oficial de la federación (RMISC 2004)
                                                                 el 31 de Mayo del 2004, la factura electrónica es 100% valida y legal a partir de ahora, la entrega del documento fiscal (FACTURA ELECTRÓNICA)
                                                                 sera emitida y entregada por correo electrónico, cabe destacar que la factura electrónica se entregara en formato
                                                                 <small style=""color:#691A1B"">PDF</small> y archivo <small style=""color:#691A1B"">XML</small>, el cual podra imprimir libremente e incluirla
                                                                 en su contabilidad (ARTICULO 29, FRACCION IV DE CFF), resguardar la impresión y archivo <small style=""color:#691A1B"">XML</small> por un periodo
                                                                 de 5 años.
                                                                 <br><br>
                                                                 <small style = ""font-size: 11px;""> Importante: contenido de la factura electrónica en el anexo 20 del diario oficial de la federación, publicado
                                                                                                    el 1 de Septiembre de 2004, en el parrafo 2.22.8, se estipula que la impresión de la factura electrónica, que ademas
                                                                                                    de los datos fiscales y comerciales, deberá contener la cadena original, el certificado de sello digital, el sello
                                                                                                    digital y la leyenda: 'Este documento es una representación impresa de un CFD/CFDI'.</ small >
                                                            </p>
                                                         </th>
                                                     </tr>
                                                     <tr style=""text-align: left; font-size: 15px; padding-left: 30px;"">
                                                        <th colspan=""2"">
                                                            <p>Observaciones</p>
                                                            <p>{2}</p>
                                                        </th>
                                                    </tr>
                                                     <tr>
                                                         <th colspan=""2"">
                                                             <br><br>
                                                             <p style=""text-align: right; padding-right: 80px; font-size: 13px;""> **ESTE CORREO ES UNICAMENTE PARA ENVIOS, FAVOR DE NO RESPONDER</p>
                                                         </th>
                                                     </tr>
                                                     <tr style=""background: url('https://i.ibb.co/2Kd9dfG/breadcrumb-bg1.jpg')"">
                                                           <th colspan=""2"" style=""width: 100%; height: 300px;""></th>
                                                     </tr>
                                                   </table>
                                             </body>
                                             </html>", string.IsNullOrEmpty(Taxpayer) ? "" : Taxpayer , string.IsNullOrEmpty(Account) ? "" : Account, "Envio automatico");
            }
            else
            {
                htmlBody = string.Format(@"<!DOCTYPE html>
                                             <html lang=""es"">
                                             <head>
                                                <meta charset=""UTF-8"">
                                                 <title>Facturación - Cuautlancingo</title>
                                                 <meta name=""viewport"" content=""width=device-width, initial-scale=1.0""/>
                                                 <link href=""https://fonts.googleapis.com/css?family=Montserrat|Roboto&display=swap"" rel=""stylesheet"">
                                             </head>
                                             <body style=""margin: 0; padding: 0;"">
                                                 <table style=""width: 100%; font-family: 'Roboto', sans-serif; letter-spacing: 0.50px;"">
                                                     <th style=""width: 7%;""><img src=""https://i.ibb.co/QkKmmqK/sosapac-sistema.png"" alt=""Logo""></th>
                                                     <th style=""width: 93%;"">
                                                        <div style =""position: absolute !important; top: 26px; left:145px; color: #691A1B text-align: left;"">
                                                            <div><h2> Sistema de Facturación</h2></div>
                                                            <div style=""margin-bottom:25px;""><h5> Comprobante Fiscal CFDI</h5></div>
                                                        </div>
                                                        <div style=""background-color: #691A1B; width: 100%; height: 5px; position: relative; top: 35px;""></div>
                                                     </th>
                                                     <tr style=""font-family: 'Montserrat', sans-serif; "">
                                                        <th colspan=""2"" style=""text-align: left; font-size: 15px; padding-left: 30px;"">
                                                            <br><br>
                                                            <p>Estimado Contribuyente:&nbsp;&nbsp; <strong>{0}</strong></p>
                                                            <p>Cuenta:&nbsp;&nbsp; <strong>{1}</strong></p></p>
                                                            <br>
                                                        </th>
                                                     </tr>
                                                     <tr style=""font-family: 'Montserrat', sans-serif;"">
                                                         <th colspan=""2"" style=""text-align: justify; padding: 0px 80px 0px 80px;"">
                                                             <p> Usted está recibiendo un comprobante fiscal digital (FACTURA ELECTRÓNICA) del municipio de Cuautlancingo, Puebla 2018 - 2021. 
                                                                 De acuerdo a la reglamentación del servicio de administración tributaria (SAT), publicada en el diario oficial de la federación (RMISC 2004)
                                                                 el 31 de Mayo del 2004, la factura electrónica es 100% valida y legal a partir de ahora, la entrega del documento fiscal (FACTURA ELECTRÓNICA)
                                                                 sera emitida y entregada por correo electrónico, cabe destacar que la factura electrónica se entregara en formato
                                                                 <small style=""color:#691A1B"">PDF</small> y archivo <small style=""color:#691A1B"">XML</small>, el cual podra imprimir libremente e incluirla
                                                                 en su contabilidad (ARTICULO 29, FRACCION IV DE CFF), resguardar la impresión y archivo <small style=""color:#691A1B"">XML</small> por un periodo
                                                                 de 5 años.
                                                                 <br><br>
                                                                 <small style = ""font-size: 11px;""> Importante: contenido de la factura electrónica en el anexo 20 del diario oficial de la federación, publicado
                                                                                                    el 1 de Septiembre de 2004, en el parrafo 2.22.8, se estipula que la impresión de la factura electrónica, que ademas
                                                                                                    de los datos fiscales y comerciales, deberá contener la cadena original, el certificado de sello digital, el sello
                                                                                                    digital y la leyenda: 'Este documento es una representación impresa de un CFD/CFDI'.</ small >
                                                            </p>
                                                         </th>
                                                     </tr>
                                                     <tr style=""text-align: left; font-size: 15px; padding-left: 30px;"">
                                                        <th colspan=""2"">
                                                            <p>Observaciones</p>
                                                            <p>{2}</p>
                                                        </th>
                                                    </tr>
                                                     <tr>
                                                         <th colspan=""2"">
                                                             <br><br>
                                                             <p style=""text-align: right; padding-right: 80px; font-size: 13px;""> **ESTE CORREO ES UNICAMENTE PARA ENVIOS, FAVOR DE NO RESPONDER</p>
                                                         </th>
                                                     </tr>
                                                     <tr style=""background: url('https://i.ibb.co/2Kd9dfG/breadcrumb-bg1.jpg')"">
                                                           <th colspan=""2"" style=""width: 100%; height: 300px;""></th>
                                                     </tr>
                                                   </table>
                                             </body>
                                             </html>", string.IsNullOrEmpty(Taxpayer) ? "" : Taxpayer, string.IsNullOrEmpty(Account) ? "" : Account, "Envio automatico.");
            }

            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            return alternateView;
        }

    }
}

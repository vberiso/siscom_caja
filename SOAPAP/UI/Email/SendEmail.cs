using SOAPAP.Enums;
using SOAPAP.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SOAPAP.UI.Email
{
    public partial class SendEmail : Form
    {
        public string Xml { get; set; }
        public string Account { get; set; }
        public string Taxpayer { get; set; }
        Form loading;
        Form mensaje;
        DialogResult result = new DialogResult();
        public SendEmail()
        {
            InitializeComponent();
        }
        public SendEmail(string Xml, string Account, string Taxpayer)
        {
            InitializeComponent();
            this.Xml = Xml;
            this.Account = Account;
            this.Taxpayer = Taxpayer;
        }

        private void PbxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                loading = new Loading();
                loading.Show(this);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("sosapac.gob.mx");
                mail.From = new MailAddress("facturacion@sosapac.gob.mx");
                mail.To.Add(txtFrom.Text);
                if (!string.IsNullOrEmpty(txtCopy.Text))
                    mail.CC.Add(txtCopy.Text);
                mail.Subject = "Envió de comprobante fiscal CFDI";
                mail.AlternateViews.Add(getEmbeddedImage());
              
                MemoryStream memoryStream = new MemoryStream();
                XmlDocument xDocument = new XmlDocument();
                xDocument.LoadXml(Xml);
                xDocument.Save(memoryStream);
                memoryStream.Flush();//Adjust this if you want read your data 
                memoryStream.Position = 0;
                Stream stream = memoryStream;
                Attachment attachment = new Attachment(stream, string.Format("Comprobante_{0}.xml", Account), "application/xml");
                mail.Attachments.Add(attachment);
                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("facturacion@sosapac.gob.mx", "e0P?k0k8");
                SmtpServer.Send(mail);
                loading.Close();
                mensaje = new MessageBoxForm("Envio Exitoso","El correo electrónico se ha enviado exitosamente" , TypeIcon.Icon.Success);
                result = mensaje.ShowDialog();
            }
            catch (Exception ex)
            {
                mensaje = new MessageBoxForm("Fallo de Envio", "El correo electrónico no pudo enviarse, favor de comunicarse con el administrador del sistema", TypeIcon.Icon.Success);
                result = mensaje.ShowDialog();
            }
            
        }

        private AlternateView getEmbeddedImage()
        {
            string htmlBody = string.Format(@"<!DOCTYPE html>
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
                                             </html>", Taxpayer, Account, txtMessage.Text == "" ? "Sin detalles de envío" : txtMessage.Text);
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            return alternateView;
        }
    }
}

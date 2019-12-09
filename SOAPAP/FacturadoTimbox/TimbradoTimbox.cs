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

namespace SOAPAP.FacturadoTimbox
{
    public class TimbradoTimbox
    {
        private RequestsAPI Requests = null;
        
        public TimbradoTimbox()
        {
            Requests = new RequestsAPI(Properties.Settings.Default.URL);
        }


        public async Task<int> Facturar(CfdiMulti cfdiMulti)
        {
            //Obtenemos el numero 
            ////string numeroCertificado, aa, b, c;
            ////SelloDigital.leerCER(pathCer, out aa, out b, out c, out numeroCertificado);
            //var resNC = await Requests.SendURIAsync("/api/Facturacion/NumeroCertificado", HttpMethod.Get, Variables.LoginModel.Token);
            //if (resNC.Contains("error"))
            //{
            //    return;
            //}
            //string numeroCertificado = JsonConvert.DeserializeObject<string>(resNC);

            Comprobante comprobante = new Comprobante();
            comprobante.Version = "3.3";
            comprobante.Serie = "A";
            comprobante.Folio = "123456";
            comprobante.Fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            //comprobante.Sello = "";
            comprobante.FormaPago = "99"; //por definir
            //comprobante.NoCertificado = numeroCertificado;
            //comprobante.Certificado = "";
            comprobante.SubTotal = 10.1m;
            comprobante.Descuento = 2.0m;
            comprobante.Moneda = "MXN";
            comprobante.Total = 10.1m;
            comprobante.TipoDeComprobante = "I";
            comprobante.MetodoPago = "PUE";
            comprobante.LugarExpedicion = "20131";
            
            ComprobanteEmisor cEmisor = new ComprobanteEmisor();
            cEmisor.Rfc = "POWE896754SD1";
            cEmisor.Nombre = "Emisor ficticio";
            cEmisor.RegimenFiscal = "603";

            ComprobanteReceptor cReceptor = new ComprobanteReceptor();
            cReceptor.Rfc = "MAU123456SD1";
            cReceptor.Nombre = "Mauricio Castillo SA. de CV";
            cReceptor.UsoCFDI = "P01";

            comprobante.Emisor = cEmisor;
            comprobante.Receptor = cReceptor;

            //Conceptos
            List<ComprobanteConcepto> lstConceptos = new List<ComprobanteConcepto>();
            ComprobanteConcepto Concep = new ComprobanteConcepto();
            Concep.Importe = 5.0m;
            Concep.ClaveProdServ = "92111704";
            Concep.Cantidad = 1;
            Concep.ClaveUnidad = "C01";
            Concep.Descripcion = "Misil de guerra";
            Concep.ValorUnitario = 7.0m;
            Concep.Descuento = 2.0m;

            ComprobanteConcepto Concep2 = new ComprobanteConcepto();
            Concep2.Importe = 5.1m;
            Concep2.ClaveProdServ = "92211704";
            Concep2.Cantidad = 1;
            Concep2.ClaveUnidad = "C01";
            Concep2.Descripcion = "Porta misil de guerra";
            Concep2.ValorUnitario = 5.1m;

            lstConceptos.Add(Concep);
            lstConceptos.Add(Concep2);

            comprobante.Conceptos = lstConceptos.ToArray();

            var content = new StringContent(JsonConvert.SerializeObject(comprobante), Encoding.UTF8, "application/json");
            var resultFac = await Requests.SendURIAsync(string.Format("/api/Facturacion/SellarXml"), HttpMethod.Post, Variables.LoginModel.Token, content);
            if (resultFac.Contains("error"))
            {
                var mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultFac).error, TypeIcon.Icon.Cancel);
                var result = mensaje.ShowDialog();                
            }
            else
            {
                ////conversion de xml a base64
                //byte[] base64 = Encoding.UTF8.GetBytes(resultFac);
                //string xmlBase64 = Convert.ToBase64String(base64);

                ////Se obtienen nuestros datos de facturacion.
                //var resultFac = await Requests.SendURIAsync(string.Format("/api/Facturacion/DataUser"), HttpMethod.Get, Variables.LoginModel.Token);

                //if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(xmlBase64))
                //{
                //    respuesta_servicio = acceso_servicio.timbrar_cfdi(userName, password, xmlBase64);
                //    TextBox_resultado.Text = respuesta_servicio.ToString();
                //}
                //else
                //    MessageBox.Show("Intente de nuevo.");
            }

            return 0;
        }

        private void ConvertirAClaseDeSalida()
        {

        }

        //private void CreateXML(Comprobante pComprobante, string nombreXml)
        //{
        //    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Facturas";
        //    try
        //    {
        //        DirectoryInfo di;
        //        if (!Directory.Exists(path))
        //        {
        //            di = Directory.CreateDirectory(path);
        //        }
        //        string pathXML = string.Format("{0}\\{1}", path, nombreXml);

        //        XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
        //        xmlSerializerNamespaces.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
        //        xmlSerializerNamespaces.Add("tfd", "http://www.sat.gob.mx/timbrefiscaldigital");
        //        xmlSerializerNamespaces.Add("xsi", "http://www.w3.gob/2001/xmlschema-instance");

        //        //SERIALIZAMOS.------------------------------------------------- 
        //        XmlSerializer oXmlSerializar = new XmlSerializer(typeof(Comprobante));

        //        string sXml = "";
        //        using (var sww = new Utils.StringWriterWithEncoding(Encoding.UTF8))
        //        {
        //            using (XmlWriter writter = XmlWriter.Create(sww))
        //            {
        //                oXmlSerializar.Serialize(writter, pComprobante, xmlSerializerNamespaces);
        //                sXml = sww.ToString();
        //            }
        //        }

        //        //guardamos el string en un archivo
        //        System.IO.File.WriteAllText(pathXML, sXml);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

    }
}

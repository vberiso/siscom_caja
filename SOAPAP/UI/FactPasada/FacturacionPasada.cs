using Newtonsoft.Json;
using PdfPrintingNet;
using SOAPAP.Enums;
using SOAPAP.Facturado;
using SOAPAP.Reportes;
using SOAPAP.Services;
using SOAPAP.UI.Email;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SOAPAP.UI.FactPasada
{
    public partial class FacturacionPasada : Form
    {
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;

        Facturaelectronica Fac;
        string xmltimbrado;
        PdfPrint PdfPrint = null;

        public FacturacionPasada()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }

        private async void FacturacionPasada_Load(object sender, EventArgs e)
        {
            PdfPrint = new PdfPrint("irDevelopers", "g/4JFMjn6KvKuhWIxC2f7pv7SMPZhNDCiF/m+DtiJywU4rE0KKwoH+XQtyGxBiLg");

            //Listados de Cajeros.
            List<DataComboBox> lstCaj = new List<DataComboBox>();
            if (Variables.LoginModel.RolName[0] == "Supervisor")
            {
                //Configuracion de vista inicial
                tlpUsuario.Visible = true;

                //Peticion de Cajeros.
                var resultTypeTransaction = await Requests.SendURIAsync("/api/UserRolesManager/Users", HttpMethod.Get, Variables.LoginModel.Token);
                if (resultTypeTransaction.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    var lstCajeros = JsonConvert.DeserializeObject<List<SOAPAP.Model.Users>>(resultTypeTransaction);
                    foreach (var item in lstCajeros)
                    {
                        lstCaj.Add(new DataComboBox() { keyString = item.id, value = string.Format("{0} {1} {2}", item.name, item.lastName, item.secondLastName) });
                    }                    
                }                
            }
            else
            {
                lstCaj.Add(new DataComboBox() { keyString = Variables.LoginModel.User, value = Variables.LoginModel.FullName});
            }
            //Asignacion de combo cajeros.
            cbxUsuario.DataBindings.Clear();
            cbxUsuario.DataSource = null;
            cbxUsuario.ValueMember = "keyString";
            cbxUsuario.DisplayMember = "value";
            cbxUsuario.DataSource = lstCaj;
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                loading = new Loading();
                loading.Show(this);

                dgvMovimientos.DataSource = null;                
                //Operadores seleccionados del combo de cajeros
                var itemsOpe = cbxUsuario.SelectedItem;
                string itemSeleccionado = "";
                if (Variables.LoginModel.RolName[0] == "Supervisor")
                {
                    ////Se obtiene el cajero para filtrar la consulta
                    if (itemsOpe == null)
                    {
                        itemSeleccionado = "";
                        mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar un cajero.", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        itemSeleccionado = ((DataComboBox)itemsOpe).keyString;                        
                    }
                }
                else
                {
                    itemSeleccionado = Variables.LoginModel.User;
                }
                var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/FromUserInDay/{0}/{1}", dtpFecha.Value.ToString("yyyy-MM-dd"), itemSeleccionado), HttpMethod.Get, Variables.LoginModel.Token);
                
                if (_resulTransaction.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    List<SOAPAP.Model.TransactionMovimientosCaja> traMovs = JsonConvert.DeserializeObject<List<SOAPAP.Model.TransactionMovimientosCaja>>(_resulTransaction);

                    if (traMovs == null)
                    {
                        mensaje = new MessageBoxForm("Sin Operaciones", "No se han encontrado movimientos para esta terminal", TypeIcon.Icon.Warning);
                        result = mensaje.ShowDialog();
                    }

                    dgvMovimientos.DataSource = traMovs;
                }
            }
            catch (Exception ex)
            {
                mensaje = new MessageBoxForm("Error", ex.Message, TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            finally
            {
                loading.Close();
            }            
        }

        private void mostrarInfoPay(Model.Payment payment)
        {
            lblSucursal.Text = payment.BranchOffice;
            dgvDetallesPago.DataSource = payment.PaymentDetails.ToList();
        }
        private void visualizaPDFActual(SOAPAP.Model.TaxReceipt tr)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Facturas";
            try
            {
                pdfVwrDetalle.CloseDocument();
                pdfVwrDetalle.ClearSelection();                
                DirectoryInfo di;
                if (!Directory.Exists(path))
                {
                    di = Directory.CreateDirectory(path);
                }

                //Se descarga temporalmente el archivo
                path = path + "\\tmpFile.pdf";
                if(System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                System.IO.File.WriteAllBytes(path, tr.PDFInvoce);

                //se visualiza el pdf
                pdfVwrDetalle.LoadDocument(path);
            }
            catch (Exception ex)
            {
                pdfVwrDetalle.ClearSelection();
            }
        }

        //Se obtiene el campo seleccionado de la tabla.
        private async void dgvMovimientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvMovimientos.Rows[e.RowIndex];
            string Operacion = row.Cells["OperacionDataGridViewTextBoxColumn"].Value.ToString();
            string transactionId = row.Cells["idTransactionDataGridViewTextBoxColumn"].Value.ToString();
            int paymentId = int.Parse(row.Cells["idPaymentDataGridViewTextBoxColumn"].Value.ToString());
            string Cuenta = row.Cells["CuentaDataGridViewTextBoxColumn"].Value.ToString();
            string Cliente = row.Cells["ClienteDataGridViewTextBoxColumn"].Value != null ? row.Cells["ClienteDataGridViewTextBoxColumn"].Value.ToString() : "";
            bool EstaFacturado = (bool)row.Cells["haveInvoiceDataGridViewCheckBoxColumn"].Value;
            
            if (dgvMovimientos.Columns[e.ColumnIndex].Name == "Facturar")
            {                
                FacturaPago(Operacion, transactionId, EstaFacturado);
            }
            else if (dgvMovimientos.Columns[e.ColumnIndex].Name == "ActualizaPdf")
            {
                ActualizaFormatoPdf(Operacion, transactionId, EstaFacturado);
            }
            else if (dgvMovimientos.Columns[e.ColumnIndex].Name == "Enviar")
            {
                EnviarDocumentos(Operacion, transactionId, EstaFacturado, paymentId, Cuenta, Cliente);
            }
            else if (dgvMovimientos.Columns[e.ColumnIndex].Name == "Descargar")
            {
                DescargarDocumetos(Operacion, transactionId, EstaFacturado, paymentId, Cuenta, Cliente);
            }
            else  //Si da click en en cualquier otra opcion de la barra, actualiza la informacion.
            { 
                string transactionFolio = row.Cells["folioTransaccionDataGridViewTextBoxColumn"].Value.ToString();
                
                //if (Operacion == "Cobro")
                //{
                    Loading loadingDetalles = new Loading();
                    loadingDetalles.Show(pnlDetalle);
                    var _resulPayment = await Requests.SendURIAsync(string.Format("/api/Payments/folio/{0}", transactionFolio), HttpMethod.Get, Variables.LoginModel.Token);
                    loadingDetalles.Close();
                    if (_resulPayment.Contains("error"))
                    {
                        mensaje = new MessageBoxForm("Error", _resulPayment.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        dgvDetallesPago.Visible = true;
                        SOAPAP.Model.Payment payment = JsonConvert.DeserializeObject<SOAPAP.Model.Payment>(_resulPayment);
                        if (payment == null)
                        {
                            mensaje = new MessageBoxForm("Sin Información", "No se entro el detalle de pago.", TypeIcon.Icon.Warning);
                            result = mensaje.ShowDialog();
                        }
                        mostrarInfoPay(payment);
                        visualizaPDFActual(payment.TaxReceipts.LastOrDefault(p => p.Status == "ET001"));
                    }
                //}
                //else
                //    dgvDetallesPago.Visible = false;
            }
        }

        //Genera factura de pago seleccionado.
        private async void FacturaPago(string Operacion, string transactionId, bool EstaFacturado)
        {
            if (EstaFacturado)
            {
                mensaje = new MessageBoxForm("Aviso", "Este pago ya ha sido facturado previamente.", TypeIcon.Icon.Info);
                result = mensaje.ShowDialog();
                //this.Close();
                //return;
            }
            if (Operacion != "Cobro")
            {
                mensaje = new MessageBoxForm("Aviso", "No se puede facturar este tipo de movimiento", TypeIcon.Icon.Info);
                result = mensaje.ShowDialog();
                //this.Close();
                //return;
            }

            if (!EstaFacturado && Operacion == "Cobro")
            {
                Form loadings = new Loading();
                loadings.Show(this);
                Fac = new Facturaelectronica();

                if (Variables.LoginModel.RolName[0] == "Supervisor")
                {
                    Fac.isAdministrator = true;
                    Fac.ActualUserId = ((DataComboBox)cbxUsuario.SelectedItem).keyString;
                }
                    
                //Se obtiene el serial de un cajero elejido, esto cuando el reporte lo solicita un administrador.
                string tmpSerialCajero = "";
                var _resulSerial = await Requests.SendURIAsync(string.Format("/api/UserRolesManager/SerialFromUser/{0}", ((DataComboBox)cbxUsuario.SelectedItem).keyString), HttpMethod.Get, Variables.LoginModel.Token);
                if (_resulSerial.Contains("error"))
                    tmpSerialCajero = "JdC";
                else
                {
                    tmpSerialCajero = JsonConvert.DeserializeObject<string>(_resulSerial);
                    if (tmpSerialCajero == null)
                        tmpSerialCajero = "JdC";                    
                }
                xmltimbrado = await Fac.generaFactura(transactionId, "ET001",  tmpSerialCajero);

                if (xmltimbrado.Contains("error"))
                {
                    loadings.Close();
                    try
                    {
                        mensaje = new MessageBoxForm("Error", xmltimbrado, TypeIcon.Icon.Cancel);
                        mensaje.AutoSize = true;
                        result = mensaje.ShowDialog();
                        //this.Close();
                    }
                    catch (Exception)
                    {
                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        //this.Close();
                    }
                }
                else
                {
                    //Impresion del PDF.
                    SOAPAP.UI.Visualizador.Preview oPreview = new SOAPAP.UI.Visualizador.Preview(xmltimbrado);
                    try
                    {
                        oPreview.imprimirDocumentoSinVisualizar();
                        oPreview.Close();
                        alzheimer();
                    }
                    catch (Exception ex)
                    {
                        mensaje = new MessageBoxForm("Error", ex.Message, TypeIcon.Icon.Cancel);
                        mensaje.ShowDialog();
                    }
                    //PdfPrint.IsContentCentered = true;
                    //PdfPrint.Scale = PdfPrint.ScaleTypes.None;
                    //PdfPrint.Status result = PdfPrint.Status.OK;
                    //PrintDialog printDialog = new PrintDialog();
                    //if (printDialog.ShowDialog() == DialogResult.OK)
                    //{
                    //    try
                    //    {
                    //        result = PdfPrint.Print(xmltimbrado, printDialog.PrinterSettings);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        result = PdfPrint.Status.UNKNOWN_ERROR;
                    //        mensaje = new MessageBoxForm("Error", ex.Message, TypeIcon.Icon.Cancel);
                    //        mensaje.ShowDialog();
                    //    }
                    //}


                    btnActualizar_Click(new object(), new EventArgs());

                    loadings.Close();
                }
            }
        }

        //Actualiza el archivo pdf.
        private async void ActualizaFormatoPdf(string Operacion, string transactionId, bool EstaFacturado)
        {
            Form loadings = new Loading();
            loadings.Show(this);
            if (!EstaFacturado)
            {
                Form mensaje = new MessageBoxForm("Aviso", "Es necesario facturar previamente.", TypeIcon.Icon.Info);
                result = mensaje.ShowDialog();                
            }
            if (Operacion != "Cobro")
            {
                Form mensaje = new MessageBoxForm("Aviso", "No se puede generar factura para este tipo de movimiento", TypeIcon.Icon.Info);
                result = mensaje.ShowDialog();                
            }
            if (EstaFacturado && Operacion == "Cobro")
            {
                Fac = new Facturaelectronica();

                if (Variables.LoginModel.RolName[0] == "Supervisor")
                {
                    Fac.isAdministrator = true;
                    Fac.ActualUserId = ((DataComboBox)cbxUsuario.SelectedItem).keyString;
                }

                string temp = await Fac.actualizaPdf(transactionId);

                if (temp.Contains("error"))
                {
                    Form mensaje = new MessageBoxForm("Error", temp.Substring(6), TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
                else if(temp.Contains("aviso"))
                {
                    Form mensaje = new MessageBoxForm("Aviso", temp.Substring(6), TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();

                    btnActualizar_Click(new object(), new EventArgs());
                }                    
            }
            if (EstaFacturado && Operacion.Contains("Cancela"))
            {
                Fac = new Facturaelectronica();

                if (Variables.LoginModel.RolName[0] == "Supervisor")
                {
                    Fac.isAdministrator = true;
                    Fac.ActualUserId = ((DataComboBox)cbxUsuario.SelectedItem).keyString;
                }

                string temp = await Fac.actualizaPdf(transactionId, true);

                if (temp.Contains("error"))
                {
                    Form mensaje = new MessageBoxForm("Error", temp.Substring(6), TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
                else if (temp.Contains("aviso"))
                {
                    Form mensaje = new MessageBoxForm("Aviso", temp.Substring(6), TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();

                    btnActualizar_Click(new object(), new EventArgs());
                }
            }
            loadings.Close();
        }

        public void DeserializerXML(string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Comprobante), new XmlRootAttribute("Comprobante"));
            StringReader stringReader = new StringReader(xmlString);
            //DocumentoXML comprobante = (DocumentoXML)serializer.Deserialize(stringReader);
        }

        //Enviar Pdf y Xml
        private async void EnviarDocumentos(string Operacion, string transactionId, bool EstaFacturado, int PaymentId, string Cuenta, string Cliente)
        {
            Loading loadingMail = new Loading();
            try
            {                
                if (!EstaFacturado)
                {
                    mensaje = new MessageBoxForm("Aviso", "Es necesario facturar previamente.", TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();
                }
                if (Operacion != "Cobro")
                {
                    mensaje = new MessageBoxForm("Aviso", "No se puede generar factura para este tipo de movimiento", TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();
                }
                if (EstaFacturado && Operacion == "Cobro")
                {                    
                    loadingMail.Show(this);                    
                    var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/TaxReceipt/FromPaymentId/{0}", PaymentId), HttpMethod.Get, Variables.LoginModel.Token);
                    
                    if (_resulTransaction.Contains("error"))
                    {
                        mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        SOAPAP.Model.TaxReceipt xml = JsonConvert.DeserializeObject<SOAPAP.Model.TaxReceipt>(_resulTransaction);
                        if (xml == null)
                        {
                            mensaje = new MessageBoxForm("Sin Operaciones", "No se han encontrado movimientos para esta terminal", TypeIcon.Icon.Warning);
                            result = mensaje.ShowDialog();
                        }
                         
                        if (xml != null)
                        {
                            SendEmail email = new SendEmail((xml.Xml.StartsWith("ï»¿") ? xml.Xml.Replace("ï»¿", "") : xml.Xml), Cuenta, Cliente, EstaFacturado, xml.PDFInvoce);
                            email.ShowDialog();
                        }
                        else
                        {
                            mensaje = new MessageBoxForm(Variables.titleprincipal, "Xml no disponible, posiblemente este pago no este facturado para mayor información contactarse con el administrador del sistema.", TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = new MessageBoxForm("Error", ex.Message, TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            finally
            {
                loadingMail.Close();
            }
        }

        //Descargar Archivos
        private async void DescargarDocumetos(string Operacion, string transactionId, bool EstaFacturado, int PaymentId, string Cuenta, string Cliente)
        {
            Loading loadingMail = new Loading();
            try
            {
                if (!EstaFacturado)
                {
                    mensaje = new MessageBoxForm("Aviso", "Es necesario facturar previamente.", TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();
                }
                if (Operacion != "Cobro")
                {
                    mensaje = new MessageBoxForm("Aviso", "No se puede generar factura para este tipo de movimiento", TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();
                }
                if (EstaFacturado && Operacion == "Cobro")
                {
                    loadingMail.Show(this);
                    var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/TaxReceipt/FromPaymentId/{0}", PaymentId), HttpMethod.Get, Variables.LoginModel.Token);

                    if (_resulTransaction.Contains("error"))
                    {
                        mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        SOAPAP.Model.TaxReceipt xml = JsonConvert.DeserializeObject<SOAPAP.Model.TaxReceipt>(_resulTransaction);
                        if (xml == null)
                        {
                            mensaje = new MessageBoxForm("Sin Operaciones", "No se han encontrado movimientos para esta terminal", TypeIcon.Icon.Warning);
                            result = mensaje.ShowDialog();
                        }

                        if (xml != null)
                        {
                            ExportGridToDocumumentos(xml.PDFInvoce, xml.Xml.StartsWith("ï»¿") ? xml.Xml.Replace("ï»¿", "") : xml.Xml);
                        }
                        else
                        {
                            mensaje = new MessageBoxForm(Variables.titleprincipal, "Xml no disponible, posiblemente este pago no este facturado para mayor información contactarse con el administrador del sistema.", TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = new MessageBoxForm("Error", ex.Message, TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            finally
            {
                loadingMail.Close();
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
        private void ExportGridToXML(string xml)
        {
            SaveFileDialog SaveXMLFileDialog = new SaveFileDialog();
            SaveXMLFileDialog.Filter = "Xml files (*.xml)|*.xml";
            SaveXMLFileDialog.FilterIndex = 2;
            SaveXMLFileDialog.RestoreDirectory = true;
            SaveXMLFileDialog.Title = "Exportar XML de Factura";
            if (SaveXMLFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(xml);
                    xdoc.Save(System.IO.File.OpenWrite(SaveXMLFileDialog.FileName));
                }
                catch (Exception)
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Por el momento no se puede descargar el xml", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
            }
        }
        private void ExportGridToDocumumentos(byte[] pdf, string xml)
        {
            SaveFileDialog SaveXMLFileDialog = new SaveFileDialog();
            //SaveXMLFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            //SaveXMLFileDialog.FilterIndex = 2;
            SaveXMLFileDialog.RestoreDirectory = true;
            SaveXMLFileDialog.Title = "Exportar Factura";
            if (SaveXMLFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllBytes(SaveXMLFileDialog.FileName + ".pdf", pdf);
                }
                catch (Exception)
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Por el momento no se puede descargar el pdf", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                } 
                try
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(xml);
                    xdoc.Save(System.IO.File.OpenWrite(SaveXMLFileDialog.FileName + ".xml"));
                }
                catch (Exception)
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Por el momento no se puede descargar el xml", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
            }
        }

        private async void btnDescarga_Click(object sender, EventArgs e)
        {
            Loading loadingMail = new Loading();
            try
            {               
                loadingMail.Show(this);
                var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Facturacion/Cancelaciones/{0}/{1}", "2019-07-01", "2019-08-10"), HttpMethod.Get, Variables.LoginModel.Token);

                if (_resulTransaction.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    List<SOAPAP.Model.TaxReceipt> lstFacts = JsonConvert.DeserializeObject<List<SOAPAP.Model.TaxReceipt>>(_resulTransaction);
                    if (lstFacts == null)
                    {
                        mensaje = new MessageBoxForm("Sin Operaciones", "No se han encontrado movimientos para esta terminal", TypeIcon.Icon.Warning);
                        result = mensaje.ShowDialog();
                    }

                    if (lstFacts != null)
                    {
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FacturasCanceladas";
                        
                        DirectoryInfo di;
                        if (!Directory.Exists(path))
                        {
                            di = Directory.CreateDirectory(path);
                        }

                        string Rfc = "";
                        if (Variables.Configuration.IsMunicipal)
                            Rfc = "MCP850101944";
                        else
                            Rfc = "SOS970808SM7";
                        foreach (var item in lstFacts)
                        {
                            string nombrefile = path + "\\" + Rfc + "_" + item.RFC + "_" + item.PaymentId + "_Cancelacion("+item.Id + ").pdf";
                            System.IO.File.WriteAllBytes(nombrefile, item.PDFInvoce);
                        }
                    }
                    else
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "Xml no disponible, posiblemente este pago no este facturado para mayor información contactarse con el administrador del sistema.", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                }                
            }
            catch (Exception ex)
            {
                mensaje = new MessageBoxForm("Error", ex.Message, TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            finally
            {
                loadingMail.Close();
            }
        }

        //Liberar recursos
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        public static void alzheimer()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }
    }
}

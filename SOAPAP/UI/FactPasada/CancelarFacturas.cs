using Newtonsoft.Json;
using PdfPrintingNet;
using SOAPAP.Enums;
using SOAPAP.FacturadoTimbox;
using SOAPAP.FacturadoTimbox.Model;
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SOAPAP.UI.FactPasada
{
    public partial class CancelarFacturas : Form
    {
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;

        Facturaelectronica Fac;
        TimbradoTimbox FacTimbox;
        string xmltimbrado;
        PdfPrint PdfPrint = null;

        DataComboBox dcbBusquedaActual = null;

        public CancelarFacturas()
        {
            InitializeComponent();
            Requests = new RequestsAPI(UrlBase);
            PdfPrint = new PdfPrint("irDevelopers", "g/4JFMjn6KvKuhWIxC2f7pv7SMPZhNDCiF/m+DtiJywU4rE0KKwoH+XQtyGxBiLg");
        }

        private async void CancelarFacturas_Load(object sender, EventArgs e)
        {
            //Listado de Tipos de busqueda.
            List<DataComboBox> lstTiposBusqueda = new List<DataComboBox>();
            lstTiposBusqueda.Add(new DataComboBox() { keyString = "Co", value = "Comprobante" });
            lstTiposBusqueda.Add(new DataComboBox() { keyString = "Fo", value = "Folio Fiscal" });
            lstTiposBusqueda.Add(new DataComboBox() { keyString = "Cu", value = "Cuenta" });
            lstTiposBusqueda.Add(new DataComboBox() { keyString = "Us", value = "Usuario" });

            //Asignacion de combo Tipos de Busquda.
            cbxTipoBusqueda.DataBindings.Clear();
            cbxTipoBusqueda.DataSource = null;
            cbxTipoBusqueda.ValueMember = "keyString";
            cbxTipoBusqueda.DisplayMember = "value";
            cbxTipoBusqueda.DataSource = lstTiposBusqueda;

            //Listados de Cajeros.
            List<DataComboBox> lstCaj = new List<DataComboBox>();
            if (Variables.LoginModel.RolName[0] == "Supervisor")
            {
                ////Configuracion de vista inicial
                //tlpUsuario.Visible = true;

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
                lstCaj.Add(new DataComboBox() { keyString = Variables.LoginModel.User, value = Variables.LoginModel.FullName });
            }

            var tmpUsuarioOnline = lstCaj.FirstOrDefault(x => x.value.Contains("en linea"));
            lstCaj.Remove(tmpUsuarioOnline);

            //Asignacion de combo cajeros.
            cbxUsuarios.DataBindings.Clear();
            cbxUsuarios.DataSource = null;
            cbxUsuarios.ValueMember = "keyString";
            cbxUsuarios.DisplayMember = "value";
            cbxUsuarios.DataSource = lstCaj;

        }

        private void cbxTipoBusqueda_SelectedValueChanged(object sender, EventArgs e)
        {
            ocultarOpcionesBusqueda();
            dcbBusquedaActual = (DataComboBox)((System.Windows.Forms.ComboBox)sender).SelectedItem;
            if(dcbBusquedaActual != null)
            {
                switch (dcbBusquedaActual.keyString)
                {
                    case "Us":
                        tableLayoutUsuarios.Visible = true;
                        tableLayoutFecha.Visible = true;
                        break;
                    case "Co":
                        lblComprobante.Text = "Comprobante";
                        tableLayoutComprobante.Visible = true;
                        break;
                    case "Fo":
                        lblComprobante.Text = "Folio Fiscal";
                        tableLayoutComprobante.Visible = true;
                        break;
                    case "Cu":
                        lblComprobante.Text = "Cuenta u Orden de pago";
                        tableLayoutComprobante.Visible = true;
                        break;
                    default:
                        lblComprobante.Text = "Comprobante";
                        tableLayoutComprobante.Visible = true;
                        break;
                }
            }            
        }

        private void ocultarOpcionesBusqueda()
        {
            tableLayoutUsuarios.Visible = false;
            tableLayoutFecha.Visible = false;
            tableLayoutComprobante.Visible = false;
        }

        //Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {            
            dcbBusquedaActual = (DataComboBox)cbxTipoBusqueda.SelectedItem;
            switch (dcbBusquedaActual.keyString)
            {
                case "Us":
                    busquedaPorUsuario();
                    break;
                case "Co":
                    busquedaPorComprobante();
                    break;
                case "Fo":
                    busquedaPorFolioFiscal();
                    break;
                case "Cu":
                    busquedaPorCuenta();
                    break;
                default:
                    break;
            }
        }

        private async void busquedaPorUsuario()
        {
            try
            {
                loading = new Loading();
                loading.Show(this);                
                dgvMovimientos.DataSource = null;
                //Operadores seleccionados del combo de cajeros
                var itemsOpe = cbxUsuarios.SelectedItem;
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

        private async void busquedaPorComprobante()
        {
            try
            {
                loading = new Loading();
                loading.Show(this);
                dgvMovimientos.DataSource = null;
                //Operadores seleccionados del combo de cajeros
                var itemsOpe = tbxComprobante.Text;
                string itemSeleccionado = "";
                if (string.IsNullOrEmpty(itemsOpe))
                {                    
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe ingresar el numero de comprobante.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else if(itemsOpe.Length < 5)
                {
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe ingresar mas digitos del comprobante.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    itemSeleccionado = itemsOpe;
                }
                
                var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/FromFolio/{0}", itemSeleccionado), HttpMethod.Get, Variables.LoginModel.Token);

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

        private async void busquedaPorFolioFiscal()
        {
            try
            {
                loading = new Loading();
                loading.Show(this);
                dgvMovimientos.DataSource = null;
                //Operadores seleccionados del combo de cajeros
                var itemsOpe = tbxComprobante.Text;
                string itemSeleccionado = "";
                if (string.IsNullOrEmpty(itemsOpe))
                {
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe ingresar el numero de folio fiscal.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else if (itemsOpe.Length < 8)
                {
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe ingresar mas digitos del folio fiscal.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    itemSeleccionado = itemsOpe;
                }

                var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/FromFolioFiscal/{0}", itemSeleccionado), HttpMethod.Get, Variables.LoginModel.Token);

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

        private async void busquedaPorCuenta()
        {
            try
            {
                loading = new Loading();
                loading.Show(this);
                dgvMovimientos.DataSource = null;
                //Operadores seleccionados del combo de cajeros
                var itemsOpe = tbxComprobante.Text;
                string itemSeleccionado = "";
                if (string.IsNullOrEmpty(itemsOpe))
                {
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe ingresar el numero de Cuenta u Orden de pago.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else if (itemsOpe.Length < 3)
                {
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe ingresar mas digitos de la Cuenta u Orden de pago.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    itemSeleccionado = itemsOpe;
                }

                var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/FromCuenta/{0}", itemSeleccionado), HttpMethod.Get, Variables.LoginModel.Token);

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

        //Eventos dataGridView
        //Se obtiene el campo seleccionado de la tabla.
        private async void dgvMovimientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvMovimientos.Rows[e.RowIndex];
            string Operacion = row.Cells["OperacionDataGridViewTextBoxColumn"].Value.ToString();
            string transactionId = row.Cells["IdTransaction"].Value.ToString();
            int paymentId = int.Parse(row.Cells["IdPayment"].Value.ToString());
            string Cuenta = row.Cells["cuentaDataGridViewTextBoxColumn"].Value.ToString();
            string Cliente = row.Cells["clienteDataGridViewTextBoxColumn"].Value != null ? row.Cells["ClienteDataGridViewTextBoxColumn"].Value.ToString() : "";
            bool EstaFacturado = (bool)row.Cells["HaveInvoice"].Value;

            if (dgvMovimientos.Columns[e.ColumnIndex].Name == "Cancelar")
            {
                CancelaPago(Operacion, paymentId, EstaFacturado, transactionId);
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
                string transactionFolio = row.Cells["FolioTransaccion"].Value.ToString();
                                
                var _resulPayment = await Requests.SendURIAsync(string.Format("/api/Payments/folio/{0}", transactionFolio), HttpMethod.Get, Variables.LoginModel.Token);                
                if (_resulPayment.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", _resulPayment.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {                    
                    SOAPAP.Model.Payment payment = JsonConvert.DeserializeObject<SOAPAP.Model.Payment>(_resulPayment);
                    if (payment == null)
                    {
                        mensaje = new MessageBoxForm("Sin Información", "No se entro el detalle de pago.", TypeIcon.Icon.Warning);
                        result = mensaje.ShowDialog();
                    }
                    
                    if (Operacion.Contains("Cobro"))
                        visualizaPDFActual(payment.TaxReceipts.LastOrDefault(p => p.Status == "ET001"));
                    else if (Operacion.Contains("Cancela"))
                        visualizaPDFActual(payment.TaxReceipts.LastOrDefault(p => p.Status == "ET002"));
                }
            }
        }

        //Genera factura de pago seleccionado.
        private async void CancelaPago(string Operacion, int paymentId, bool EstaFacturado, string transactionId)
        {
            Model.Payment payment;
            string resultadoSolicitudCancelacion = "";

            if (!EstaFacturado)
            {
                mensaje = new MessageBoxForm("Aviso", "Este pago nunca fue timbrado", TypeIcon.Icon.Info);
                result = mensaje.ShowDialog();                
            }
            if (Operacion != "Cobro")
            {
                mensaje = new MessageBoxForm("Aviso", "No se puede cancelar este tipo de movimiento", TypeIcon.Icon.Info);
                result = mensaje.ShowDialog();                
            }

            if (EstaFacturado && Operacion == "Cobro")
            {
                Model.TaxReceipt taxReceipt;
                Form loadings = new Loading();
                loadings.Show(this);

                //Obtengo la informacion del pago
                var _resulPayment = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", paymentId), HttpMethod.Get, Variables.LoginModel.Token);
                if (_resulPayment.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    loadings.Close();
                    return;
                } 
                payment = JsonConvert.DeserializeObject<Model.Payment>(_resulPayment);

                //Solicitud de cancelacion de factura
                taxReceipt = payment.TaxReceipts.FirstOrDefault(x => x.Status == "ET001");
                if (taxReceipt != null && !taxReceipt.IdXmlFacturama.Contains("Timbox"))
                {
                    Fac = new Facturaelectronica();
                    int intentos = 0;
                    while(intentos < 2)
                    {
                        resultadoSolicitudCancelacion = await Fac.CancelarFacturaDesdeAPI(taxReceipt);
                        if (resultadoSolicitudCancelacion.Contains("error"))
                        {
                            Thread.Sleep(3000);
                            intentos++;
                        }
                        else
                            break;
                    }                    
                }
                else
                {
                    DataCancelationTimbox dataCancelationTimbox = new DataCancelationTimbox() {
                        uuid = taxReceipt.FielXML,
                        receptorRFC = taxReceipt.RFC,
                        total = payment.Total,
                        paymentId = payment.Id,
                        taxReceiptId = taxReceipt.Id
                    };
                    FacTimbox = new TimbradoTimbox();
                    
                    int intentos = 0;
                    while (intentos < 2)
                    {
                        resultadoSolicitudCancelacion = await FacTimbox.CancelarFacturaDesdeAPI(dataCancelationTimbox);
                        if (resultadoSolicitudCancelacion.Contains("error"))
                        {
                            Thread.Sleep(3000);
                            intentos++;
                        }
                        else
                            break;
                    }
                }

                var definition = new { status = "", message = "" };                
                var resSolCan = JsonConvert.DeserializeAnonymousType(resultadoSolicitudCancelacion, definition);

                //Reviso si fue exitoso la solicitud de cancelacion
                if (resSolCan.status.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    return;
                }
                else if(resSolCan.status.Contains("canceled") || resSolCan.status.Contains("acepted") || resSolCan.status.Contains("expired"))
                {
                    string ruta = "";
                    if(payment.OrderSaleId == 0)                    
                        ruta = $"/api/Transaction/SuperService/CancelWithoutCFDI/{transactionId}";                    
                    else                    
                        ruta = $"/api/Transaction/SuperOrders/CancelWithoutCFDI/{transactionId}";
                    
                    ////Cancelacion del pago en caja.                    
                    var responseCancelacionCaja = await Requests.SendURIAsync(ruta, HttpMethod.Post, Variables.LoginModel.Token, null);
                    if (!responseCancelacionCaja.Contains("error:"))
                    {
                        //Se actualiza el formato PDF
                        ActualizaFormatoPdf("Cancelacion", transactionId, true);

                        mensaje = new MessageBoxForm("Pago Cancelado", "Se cancelo en CFDI exitosamente.", TypeIcon.Icon.Info);
                        result = mensaje.ShowDialog();
                        loadings.Close();
                        return;
                    }
                    else
                    {
                        mensaje = new MessageBoxForm("Proceso incompleto", "Se cancelo en CFDI, pero localmente no se pudo cancelar el pago.", TypeIcon.Icon.Info);
                        result = mensaje.ShowDialog();
                        loadings.Close();
                        return;
                    }
                }
                else
                {
                    mensaje = new MessageBoxForm("No se pudo cancelar la factura.", resSolCan.message, TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();
                    loadings.Close();
                    return;
                }

            }
        }


        //Carga el pdf en el vizualizador de PDF
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
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                System.IO.File.WriteAllBytes(path, tr.PDFInvoce);

                //se visualiza el pdf
                pdfVwrDetalle.LoadDocument(path);
            }
            catch (Exception ex)
            {
                pdfVwrDetalle.Dispose();
            }
        }

        //Actualiza el archivo pdf.
        private async void ActualizaFormatoPdf(string Operacion, string transactionId, bool EstaFacturado)
        {
            Form loadings = new Loading();
            loadings.Show(this);
            if (!EstaFacturado)
            {
                if (Operacion.Contains("Cobro"))
                {
                    Form mensaje = new MessageBoxForm("Aviso", "Es necesario facturar previamente.", TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();
                }
                if (Operacion.Contains("Cancela"))
                {
                    Form mensaje = new MessageBoxForm("Aviso", "Este pago nunca se facturó, no puede ser actualizado.", TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();
                }
            }
            if (Operacion != "Cobro" && !Operacion.Contains("Cancela"))
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
                    Fac.ActualUserId = ((DataComboBox)cbxUsuarios.SelectedItem).keyString;
                }

                string temp = await Fac.actualizaPdf(transactionId);

                if (temp.Contains("error"))
                {
                    Form mensaje = new MessageBoxForm("Error", temp.Substring(6), TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
                else if (temp.Contains("aviso"))
                {
                    Form mensaje = new MessageBoxForm("Aviso", temp.Substring(6), TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();
                                        
                    btnBuscar_Click(new object(), new EventArgs());
                }
            }
            if (EstaFacturado && Operacion.Contains("Cancela"))
            {
                Fac = new Facturaelectronica();

                if (Variables.LoginModel.RolName[0] == "Supervisor")
                {
                    Fac.isAdministrator = true;
                    Fac.ActualUserId = ((DataComboBox)cbxUsuarios.SelectedItem).keyString;
                }

                string temp = await Fac.actualizaCanceladoPDF(transactionId);

                if (temp.Contains("error"))
                {
                    Form mensaje = new MessageBoxForm("Error", temp.Substring(6), TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
                else if (temp.Contains("aviso"))
                {
                    Form mensaje = new MessageBoxForm("Aviso", temp.Substring(6), TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();
                                        
                    btnBuscar_Click(new object(), new EventArgs());
                }
            }
            loadings.Close();
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
                if (!Operacion.Contains("Cobro") && !Operacion.Contains("Cancela"))
                {
                    mensaje = new MessageBoxForm("Aviso", "No se puede generar factura para este tipo de movimiento", TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();
                }
                if (EstaFacturado && (Operacion == "Cobro" || Operacion.Contains("Cancela")))
                {
                    loadingMail.Show(this);
                    string ruta = Operacion == "Cobro" ? "/api/TaxReceipt/FromPaymentId" : "/api/TaxReceipt/FromPaymentId/Canceled";
                    var _resulTransaction = await Requests.SendURIAsync(string.Format("{0}/{1}", ruta, PaymentId), HttpMethod.Get, Variables.LoginModel.Token);

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
                if (!Operacion.Contains("Cobro") && !Operacion.Contains("Cancela"))
                {
                    mensaje = new MessageBoxForm("Aviso", "No se puede generar factura para este tipo de movimiento", TypeIcon.Icon.Info);
                    result = mensaje.ShowDialog();
                }
                if (EstaFacturado && (Operacion == "Cobro" || Operacion.Contains("Cancela")))
                {
                    loadingMail.Show(this);
                    string ruta = Operacion == "Cobro" ? "/api/TaxReceipt/FromPaymentId" : "/api/TaxReceipt/FromPaymentId/Canceled";
                    var _resulTransaction = await Requests.SendURIAsync(string.Format("{0}/{1}", ruta, PaymentId), HttpMethod.Get, Variables.LoginModel.Token);

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

        private void ExportGridToDocumumentos(byte[] pdf, string xml)
        {
            SaveFileDialog SaveXMLFileDialog = new SaveFileDialog();            
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
    }
}

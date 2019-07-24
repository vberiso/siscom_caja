using Newtonsoft.Json;
using PdfPrintingNet;
using SOAPAP.Enums;
using SOAPAP.Facturado;
using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private void FacturacionPasada_Load(object sender, EventArgs e)
        {
            PdfPrint = new PdfPrint("irDevelopers", "g/4JFMjn6KvKuhWIxC2f7pv7SMPZhNDCiF/m+DtiJywU4rE0KKwoH+XQtyGxBiLg");
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                loading = new Loading();
                loading.Show(this);

                dgvMovimientos.DataSource = null;
                var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/FromUserInDay/{0}/{1}", dtpFecha.Value.ToString("yyyy-MM-dd"), Variables.LoginModel.User), HttpMethod.Get, Variables.LoginModel.Token);

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

        private async void dgvMovimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvMovimientos.Rows[e.RowIndex];
                string Operacion = row.Cells["OperacionDataGridViewTextBoxColumn"].Value.ToString();
                string transactionFolio = row.Cells["folioTransaccionDataGridViewTextBoxColumn"].Value.ToString();

                if (Operacion == "Cobro")
                {
                    loading = new Loading();
                    loading.Show(this);
                    var _resulPayment = await Requests.SendURIAsync(string.Format("/api/Payments/folio/{0}", transactionFolio), HttpMethod.Get, Variables.LoginModel.Token);
                    loading.Close();
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
                    }
                }
                else
                    dgvDetallesPago.Visible = false;
            }
        }

        private void mostrarInfoPay(Model.Payment payment)
        {
            lblSucursal.Text = payment.BranchOffice;
            dgvDetallesPago.DataSource = payment.PaymentDetails.ToList();
        }

        //Se obtiene el campo seleccionado de la tabla.
        private void dgvMovimientos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvMovimientos.Rows[e.RowIndex];
            string Operacion = row.Cells["OperacionDataGridViewTextBoxColumn"].Value.ToString();
            string transactionId = row.Cells["idTransactionDataGridViewTextBoxColumn"].Value.ToString();
            bool EstaFacturado = (bool)row.Cells["haveInvoiceDataGridViewCheckBoxColumn"].Value;

            if (dgvMovimientos.Columns[e.ColumnIndex].Name == "Facturar")
            {                
                FacturaPago(Operacion, transactionId, EstaFacturado);
            }
            else if (dgvMovimientos.Columns[e.ColumnIndex].Name == "ActualizaPdf")
            {
                ActualizaFormatoPdf(Operacion, transactionId, EstaFacturado);
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
                xmltimbrado = await Fac.generaFactura(transactionId, "ET001");
                if (xmltimbrado.Contains("error"))
                {
                    loadings.Close();
                    try
                    {
                        mensaje = new MessageBoxForm("Error", xmltimbrado, TypeIcon.Icon.Cancel);
                        mensaje.AutoSize = true;
                        result = mensaje.ShowDialog();
                        this.Close();
                    }
                    catch (Exception)
                    {
                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    PdfPrint.IsContentCentered = true;
                    PdfPrint.Scale = PdfPrint.ScaleTypes.None;
                    PdfPrint.Status result = PdfPrint.Status.OK;
                    PrintDialog printDialog = new PrintDialog();
                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            result = PdfPrint.Print(xmltimbrado, printDialog.PrinterSettings);
                        }
                        catch (Exception ex)
                        {
                            result = PdfPrint.Status.UNKNOWN_ERROR;
                            mensaje = new MessageBoxForm("Error", ex.Message, TypeIcon.Icon.Cancel);
                            mensaje.ShowDialog();
                        }
                    }


                    btnActualizar.PerformClick();

                    loadings.Close();
                }
            }
        }

        //Actualiza el archivo pdf.
        private async void ActualizaFormatoPdf(string Operacion, string transactionId, bool EstaFacturado)
        {
            if (!EstaFacturado)
            {
                mensaje = new MessageBoxForm("Aviso", "Es necesario facturar previamente.", TypeIcon.Icon.Info);
                result = mensaje.ShowDialog();                
            }
            if (Operacion != "Cobro")
            {
                mensaje = new MessageBoxForm("Aviso", "No se generar para este tipo de movimiento", TypeIcon.Icon.Info);
                result = mensaje.ShowDialog();                
            }
            if (EstaFacturado && Operacion == "Cobro")
            {
                Fac = new Facturaelectronica();
                string temp = await Fac.actualizaPdf(transactionId);
            }
        }

        public void DeserializerXML(string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Comprobante), new XmlRootAttribute("Comprobante"));
            StringReader stringReader = new StringReader(xmlString);
            DocumentoXML comprobante = (DocumentoXML)serializer.Deserialize(stringReader);
        }
    }
}

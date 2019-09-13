using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Reportes;
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
using System.Xml;

namespace SOAPAP.UI.FactPasada
{
    public partial class DescargarFacturas : Form
    {
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;

        public DescargarFacturas()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }

        private async void DescargarFacturas_Load(object sender, EventArgs e)
        {
            //Listados de Cajeros.
            List<DataComboBox> lstCaj = new List<DataComboBox>();
                       
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

                //Asignacion de combo cajeros.
                cbxUsuarios.DataBindings.Clear();
                cbxUsuarios.DataSource = null;
                cbxUsuarios.ValueMember = "keyString";
                cbxUsuarios.DisplayMember = "value";
                cbxUsuarios.DataSource = lstCaj;
            }

            //Estados de facturas.
            List<DataComboBox> lstEstados = new List<DataComboBox>();
            lstEstados.Add(new DataComboBox() { keyString = "ET001", value = "Activo" });
            lstEstados.Add(new DataComboBox() { keyString = "ET002", value = "Cancelado" });
            lstEstados.Add(new DataComboBox() { keyString = "ET111", value = "Ambos" });

            cbxEstados.ValueMember = "keyString";
            cbxEstados.DisplayMember = "value";
            cbxEstados.DataSource = lstEstados;

        }

        private async void BtnDescargar_Click(object sender, EventArgs e)
        {
            try
            {
                loading = new Loading();
                loading.Show(this);
                                
                //Operadores seleccionados del combo de cajeros
                var itemsOpe = cbxUsuarios.SelectedItem;
                string itemSeleccionado = "";                
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
                
                var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Facturacion/Facturas/{0}/{1}/{2}/{3}", dtpFecha.Value.ToString("yyyy-MM-dd"), dtFechaFin.Value.ToString("yyyy-MM-dd"), itemSeleccionado, ((DataComboBox)cbxEstados.SelectedItem).keyString), HttpMethod.Get, Variables.LoginModel.Token);

                if (_resulTransaction.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    List<SOAPAP.Model.TaxReceipt> lstTaxR = JsonConvert.DeserializeObject<List<SOAPAP.Model.TaxReceipt>>(_resulTransaction);

                    if (lstTaxR == null)
                    {
                        mensaje = new MessageBoxForm("Sin Operaciones", "No se han encontrado movimientos para esta terminal", TypeIcon.Icon.Warning);
                        result = mensaje.ShowDialog();
                    }

                    if (lstTaxR != null)
                    {
                        //Se genera la carpeta de descargas
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FacturasDescargadas";
                        DirectoryInfo di;
                        if (!Directory.Exists(path))
                        {
                            di = Directory.CreateDirectory(path);
                        }
                        //Se genera la carpeta del usuario que realizo el cobro de la factura.
                        path += "\\" + ((DataComboBox)itemsOpe).value;
                        if (!Directory.Exists(path))
                        {
                            di = Directory.CreateDirectory(path);
                        }

                        //Descarga de archivos.
                        string Rfc = "";
                        if (Variables.Configuration.IsMunicipal)
                            Rfc = "MCP850101944";
                        else
                            Rfc = "SOS970808SM7";

                        int Count = 0;
                        foreach (var item in lstTaxR)
                        {
                            string nombrefile;
                            if(item.Status == "ET001")
                                nombrefile = path + "\\" + Rfc + "_" + item.RFC + "_" + item.PaymentId + "_(" + item.Id + ")";
                            else
                                nombrefile = path + "\\" + Rfc + "_" + item.RFC + "_" + item.PaymentId + "_(" + item.Id + ")_CANCELADO";
                            
                            // Se guarda pdf
                            System.IO.File.WriteAllBytes(nombrefile + ".pdf", item.PDFInvoce);
                            //Se guarde el xml.
                            XmlDocument xdoc = new XmlDocument();
                            xdoc.LoadXml(item.Xml);
                            xdoc.Save(System.IO.File.OpenWrite(nombrefile + ".xml"));
                            
                            Count++;
                        }
                        lblResultado.Text = "CFDIs descargados: " + Count;                        
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
                loading.Close();
            }
        }
    }
}

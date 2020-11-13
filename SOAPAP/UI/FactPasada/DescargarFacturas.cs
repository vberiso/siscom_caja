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

        //Listados de Cajeros.
        List<DataComboBox> lstCaj;
        List<SOAPAP.Model.Users> lstCajeros;
        List<Model.Division> lstDivisions;

        public DescargarFacturas()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();

            lstCaj = new List<DataComboBox>();
            lstCajeros = new List<Model.Users>();
            lstDivisions = new List<Model.Division>();
        }

        private async void DescargarFacturas_Load(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);

            //Peticion de Cajeros.
            var resultTypeTransaction = await Requests.SendURIAsync("/api/UserRolesManager/Users", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                lstCajeros = JsonConvert.DeserializeObject<List<SOAPAP.Model.Users>>(resultTypeTransaction);
                foreach (var item in lstCajeros)
                {
                    lstCaj.Add(new DataComboBox() { keyString = item.id, value = string.Format("{0} {1} {2}", item.name, item.lastName, item.secondLastName) });
                }

                //Asignacion de combo cajeros.
                chcbxOperador.DataBindings.Clear();
                chcbxOperador.Properties.DataSource = null;
                chcbxOperador.Properties.ValueMember = "keyString";
                chcbxOperador.Properties.DisplayMember = "value";
                chcbxOperador.Properties.DataSource = lstCaj;
            }

            //Peticion de Cajeros.
            var resultDivisiones = await Requests.SendURIAsync("/api/Division/FromApp/1", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultDivisiones.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                lstDivisions = JsonConvert.DeserializeObject<List<Model.Division>>(resultDivisiones);                
            }

            //Estados de facturas.
            List<DataComboBox> lstEstados = new List<DataComboBox>();
            lstEstados.Add(new DataComboBox() { keyString = "ET001", value = "Activo" });
            lstEstados.Add(new DataComboBox() { keyString = "ET002", value = "Cancelado" });
            lstEstados.Add(new DataComboBox() { keyString = "ET111", value = "Ambos" });

            cbxEstados.ValueMember = "keyString";
            cbxEstados.DisplayMember = "value";
            cbxEstados.DataSource = lstEstados;

            loading.Close();
        }

        private async void BtnDescargar_Click(object sender, EventArgs e)
        {
            try
            {
                loading = new Loading();
                loading.Show(this);
                List<string> Cajeros = new List<string>();
                List<string> idsDivisionSelected = new List<string>();

                //Operadores seleccionados del combo de cajeros                
                var itemsOpe = chcbxOperador.Properties.Items.ToList();
                string itemSeleccionado = "";
                if (radUsuario.Checked)
                {
                    ////Se obtiene el cajero para filtrar la consulta                
                    if (itemsOpe.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                    {
                        itemSeleccionado = "";
                        mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar un cajero.", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        foreach (var item in itemsOpe)
                        {
                            if (item.CheckState == CheckState.Checked)
                            {
                                itemSeleccionado = itemSeleccionado + item.Value + ",";
                                Cajeros.Add((string)item.Value);
                            }
                        }
                        itemSeleccionado = itemSeleccionado.Substring(0, itemSeleccionado.Length - 1);
                    }

                    //Si hay cajeros seleccionados
                    if (Cajeros.Count > 0)
                    {
                        //Comineza el proceso de descarga
                        List<Task<string>> lstTareas = new List<Task<string>>();
                        foreach (var item in Cajeros)
                        {
                            var TareaX = EjecutaPeticion(item, dtpFecha.Value, dtFechaFin.Value, ((DataComboBox)cbxEstados.SelectedItem).keyString);
                            lstTareas.Add(TareaX);
                        }

                        try
                        {
                            string[] resultados = await Task.WhenAll(lstTareas);
                            GuardaResultados(resultados);
                            mensaje = new MessageBoxForm("Descarga terminada", "La descarga se realizo exitosamente.", TypeIcon.Icon.Success);
                            result = mensaje.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else
                {
                    ////Se obtiene la division para filtrar la consulta                
                    if (itemsOpe.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                    {
                        itemSeleccionado = "";
                        mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar una division.", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {                        
                        foreach (var item in itemsOpe)
                        {
                            if (item.CheckState == CheckState.Checked)
                                idsDivisionSelected.Add((string)item.Value);
                        }
                        //Cajeros = lstCajeros.Where(c => idsDivisionSelected.Contains(c.divitionId.ToString())).Select(x => x.id).ToList();
                    }

                    //Si hay divisiones seleccionados
                    if (idsDivisionSelected.Count > 0)
                    {
                        //Comineza el proceso de descarga
                        List<Task<string>> lstTareas = new List<Task<string>>();
                        foreach (var item in idsDivisionSelected)
                        {
                            var TareaX = EjecutaPeticionXDivision(item, dtpFecha.Value, dtFechaFin.Value, ((DataComboBox)cbxEstados.SelectedItem).keyString);
                            lstTareas.Add(TareaX);
                        }

                        try
                        {
                            string[] resultados = await Task.WhenAll(lstTareas);
                            GuardaResultados(resultados);
                            mensaje = new MessageBoxForm("Descarga terminada", "La descarga se realizo exitosamente.", TypeIcon.Icon.Success);
                            result = mensaje.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                
                
                #region antiguo

                //var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Facturacion/Facturas/{0}/{1}/{2}/{3}", dtpFecha.Value.ToString("yyyy-MM-dd"), dtFechaFin.Value.ToString("yyyy-MM-dd"), itemSeleccionado, ((DataComboBox)cbxEstados.SelectedItem).keyString), HttpMethod.Get, Variables.LoginModel.Token);

                //if (_resulTransaction.Contains("error"))
                //{
                //    mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                //    result = mensaje.ShowDialog();
                //    lblResultado.Text = "";
                //}               
                //else
                //{                    
                //    List<SOAPAP.Model.TaxReceipt> lstTaxR = JsonConvert.DeserializeObject<List<SOAPAP.Model.TaxReceipt>>(_resulTransaction);

                //    if (lstTaxR == null || lstTaxR.Count == 0)
                //    {
                //        mensaje = new MessageBoxForm("Sin Operaciones", "No se han encontrado movimientos para esta terminal", TypeIcon.Icon.Warning);
                //        result = mensaje.ShowDialog();
                //        lblResultado.Text = "";
                //    }

                //    if (lstTaxR != null && lstTaxR.Count > 0)
                //    {
                //        //Se genera la carpeta de descargas
                //        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FacturasDescargadas";
                //        DirectoryInfo di;
                //        if (!Directory.Exists(path))
                //        {
                //            di = Directory.CreateDirectory(path);
                //        }
                //        //Se genera la carpeta del usuario que realizo el cobro de la factura.
                //        path += "\\" + ((DataComboBox)itemsOpe).value + "_" + dtpFecha.Value.ToString("dd-MM-yyyy") + "al" + dtFechaFin.Value.ToString("dd-MM-yyyy");
                //        if (!Directory.Exists(path))
                //        {
                //            di = Directory.CreateDirectory(path);
                //        }

                //        //Descarga de archivos.
                //        string Rfc = "";
                //        if (Variables.Configuration.IsMunicipal)
                //            Rfc = "MCP850101944";
                //        else
                //            Rfc = "SOS970808SM7";

                //        int Count = 0;
                //        foreach (var item in lstTaxR)
                //        {
                //            string nombrefile;
                //            if(item.Status == "ET001")
                //                nombrefile = path + "\\" + Rfc + "_" + item.RFC + "_" + item.PaymentId + "_(" + item.Id + ")";
                //            else
                //                nombrefile = path + "\\" + Rfc + "_" + item.RFC + "_" + item.PaymentId + "_(" + item.Id + ")_CANCELADO";

                //            // Se guarda pdf
                //            System.IO.File.WriteAllBytes(nombrefile + ".pdf", item.PDFInvoce);
                //            //Se guarde el xml.
                //            XmlDocument xdoc = new XmlDocument();                            
                //            xdoc.LoadXml(item.Xml);
                //            //xdoc.Save(System.IO.File.OpenWrite(nombrefile + ".xml"));
                //            FileStream fs = System.IO.File.OpenWrite(nombrefile + ".xml");
                //            xdoc.Save(fs);
                //            fs.Flush();
                //            fs.Close();

                //            Count++;
                //        }
                //        lblResultado.Text = "CFDIs descargados: " + Count;                        
                //    }
                //    //else
                //    //{
                //    //    mensaje = new MessageBoxForm(Variables.titleprincipal, "Xml no disponible, posiblemente este pago no este facturado para mayor información contactarse con el administrador del sistema.", TypeIcon.Icon.Cancel);
                //    //    result = mensaje.ShowDialog();
                //    //}
                //}
                #endregion
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

        public async Task<string> EjecutaPeticion(string idCajero, DateTime FIni, DateTime FFin, string estadoFacturas)
        {
            int SegmentoDias = 10;
            int TotalFacturas = 0;            
            string Errores = "No se pudieron descargar los siguientes pagos: ";

            var Tarea = Task.Run(async () => {

                //Se realiza la descarga en segmentos de 10 dias
                DateTime FechaInicial = FIni;
                DateTime FechaFinal = FIni.AddDays(SegmentoDias) < FFin ? FIni.AddDays(SegmentoDias) : FFin;

                do
                {
                    var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Facturacion/Facturas/{0}/{1}/{2}/{3}", FechaInicial.ToString("yyyy-MM-dd"), FechaFinal.ToString("yyyy-MM-dd"), idCajero, estadoFacturas), HttpMethod.Get, Variables.LoginModel.Token);

                    if (_resulTransaction.Contains("error"))
                    {
                        mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        lblResultado.Text = "";
                    }
                    else
                    {
                        List<SOAPAP.Model.TaxReceipt> lstTaxR = JsonConvert.DeserializeObject<List<SOAPAP.Model.TaxReceipt>>(_resulTransaction);
                                                
                        if (lstTaxR != null && lstTaxR.Count > 0)
                        {
                            //Se genera la carpeta de descargas
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FacturasDescargadas";
                            DirectoryInfo di;
                            if (!Directory.Exists(path))
                            {
                                di = Directory.CreateDirectory(path);
                            }
                            //Se genera la carpeta para esta descarga.
                            path += "\\" + "Facturas_" + dtpFecha.Value.ToString("dd-MM-yyyy") + "al" + dtFechaFin.Value.ToString("dd-MM-yyyy");
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
                                                        
                            foreach (var item in lstTaxR)
                            {
                                try
                                {
                                    string nombrefile;
                                    if (item.Status == "ET001")
                                        nombrefile = path + "\\" + Rfc + "_" + item.RFC + "_" + item.FielXML;
                                    else
                                        nombrefile = path + "\\" + Rfc + "_" + item.RFC + "_" + item.FielXML + "_CANCELADO";

                                    // Se guarda pdf
                                    System.IO.File.WriteAllBytes(nombrefile + ".pdf", item.PDFInvoce);
                                    //Se guarde el xml.
                                    XmlDocument xdoc = new XmlDocument();
                                    xdoc.LoadXml(item.Xml);
                                    FileStream fs = System.IO.File.OpenWrite(nombrefile + ".xml");
                                    xdoc.Save(fs);
                                    fs.Flush();
                                    fs.Close();

                                    TotalFacturas++;
                                }
                                catch (Exception)
                                {
                                    Errores += item.PaymentId + ", ";
                                }                                
                            }                            
                        }
                        
                    }

                    FechaInicial = FechaFinal.AddDays(1);
                    FechaFinal = FechaFinal.AddDays(SegmentoDias) < FFin ? FechaFinal.AddDays(SegmentoDias) : FFin;
                } while (FechaInicial < FechaFinal);

                 return TotalFacturas;
            });

            var Resultado = await Tarea;

            var Usurio = lstCajeros.FirstOrDefault(c => c.id == idCajero);
            string ResultadoCajero = $"Resultados de {Usurio.name} {Usurio.lastName} {Usurio.secondLastName}\r\n";
            ResultadoCajero += "Facturas Descargadas: " + TotalFacturas + "\r\n";
            if(Errores.Length > 48)
                ResultadoCajero += Errores + "\r\n";

            return ResultadoCajero;
        }

        public async Task<string> EjecutaPeticionXDivision(string idDivision, DateTime FIni, DateTime FFin, string estadoFacturas)
        {
            int SegmentoDias = 10;
            int TotalFacturas = 0;
            string Errores = "No se pudieron descargar los siguientes pagos: ";

            var Tarea = Task.Run(async () => {

                //Se realiza la descarga en segmentos de 10 dias
                DateTime FechaInicial = FIni;
                DateTime FechaFinal = FIni.AddDays(SegmentoDias) < FFin ? FIni.AddDays(SegmentoDias) : FFin;

                do
                {
                    var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Facturacion/FacturasFromDivision/{0}/{1}/{2}/{3}", FechaInicial.ToString("yyyy-MM-dd"), FechaFinal.ToString("yyyy-MM-dd"), idDivision, estadoFacturas), HttpMethod.Get, Variables.LoginModel.Token);

                    if (_resulTransaction.Contains("error"))
                    {
                        mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        lblResultado.Text = "";
                    }
                    else
                    {
                        List<SOAPAP.Model.TaxReceipt> lstTaxR = JsonConvert.DeserializeObject<List<SOAPAP.Model.TaxReceipt>>(_resulTransaction);

                        if (lstTaxR != null && lstTaxR.Count > 0)
                        {
                            //Se genera la carpeta de descargas
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FacturasDescargadas";
                            DirectoryInfo di;
                            if (!Directory.Exists(path))
                            {
                                di = Directory.CreateDirectory(path);
                            }
                            //Se genera la carpeta para esta descarga.
                            path += "\\" + "Facturas_" + dtpFecha.Value.ToString("dd-MM-yyyy") + "al" + dtFechaFin.Value.ToString("dd-MM-yyyy");
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

                            foreach (var item in lstTaxR)
                            {
                                try
                                {
                                    string nombrefile;
                                    if (item.Status == "ET001")
                                        nombrefile = path + "\\" + Rfc + "_" + item.RFC + "_" + item.FielXML;
                                    else
                                        nombrefile = path + "\\" + Rfc + "_" + item.RFC + "_" + item.FielXML + "_CANCELADO";

                                    // Se guarda pdf
                                    System.IO.File.WriteAllBytes(nombrefile + ".pdf", item.PDFInvoce);
                                    //Se guarde el xml.
                                    XmlDocument xdoc = new XmlDocument();
                                    xdoc.LoadXml(item.Xml);
                                    FileStream fs = System.IO.File.OpenWrite(nombrefile + ".xml");
                                    xdoc.Save(fs);
                                    fs.Flush();
                                    fs.Close();

                                    TotalFacturas++;
                                }
                                catch (Exception)
                                {
                                    Errores += item.PaymentId + ", ";
                                }
                            }
                        }

                    }

                    FechaInicial = FechaFinal.AddDays(1);
                    FechaFinal = FechaFinal.AddDays(SegmentoDias) < FFin ? FechaFinal.AddDays(SegmentoDias) : FFin;
                } while (FechaInicial < FechaFinal);

                return TotalFacturas;
            });

            var Resultado = await Tarea;

            var Division = lstDivisions.FirstOrDefault(d => d.Id == int.Parse(idDivision));
            string ResultadoCajero = $"Resultados de {Division.Name}\r\n";
            ResultadoCajero += "Facturas Descargadas: " + TotalFacturas + "\r\n";
            if (Errores.Length > 48)
                ResultadoCajero += Errores + "\r\n";

            return ResultadoCajero;
        }

        private async void GuardaResultados(string[] Res)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FacturasDescargadas";
                DirectoryInfo di;
                if (!Directory.Exists(path))
                {
                    di = Directory.CreateDirectory(path);
                }
                path += "\\" + "Facturas_" + dtpFecha.Value.ToString("dd-MM-yyyy") + "al" + dtFechaFin.Value.ToString("dd-MM-yyyy");
                if (!Directory.Exists(path))
                {
                    di = Directory.CreateDirectory(path);
                }

                // Crea el archivo de resultados
                path += "\\ResultadosDescarga_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                int numCreados = 1;
                while (File.Exists(path))
                {
                    numCreados++;
                    if (numCreados > 2)
                        path = path.Replace($"({numCreados - 1})", "");
                    path = path.Replace(".txt", $"({numCreados}).txt");
                }

                using (FileStream fs = File.Create(path))
                {
                    for (int i = 0; i < Res.Length; i++)
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(Res[i].ToString() + "\r\n");
                        fs.Write(info, 0, info.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        //ajusta la fecha inicial segun sea final, a lo mas un mes.
        private void dtFechaFin_ValueChanged(object sender, EventArgs e)
        {
            dtpFecha.MinDate = dtFechaFin.Value.AddDays(-31);
        }

        //Verifico cual es la opcion selecciona, pues se llena el chech con usuarios o Divisiones.
        private void radUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if (radUsuario.Checked)            
                mostrarUsuarios();            
            else
                mostrarDivisiones();
        }

        private void mostrarUsuarios()
        {
            lstCaj = new List<DataComboBox>();            
            foreach (var item in lstCajeros)
            {
                lstCaj.Add(new DataComboBox() { keyString = item.id, value = string.Format("{0} {1} {2}", item.name, item.lastName, item.secondLastName) });
            }

            //Asignacion de combo cajeros.
            chcbxOperador.DataBindings.Clear();
            chcbxOperador.Properties.DataSource = null;
            chcbxOperador.Reset();
            chcbxOperador.Properties.ValueMember = "keyString";
            chcbxOperador.Properties.DisplayMember = "value";
            chcbxOperador.Properties.DataSource = lstCaj;
            chcbxOperador.RefreshEditValue();
        }
        private void mostrarDivisiones()
        {
            lstCaj = new List<DataComboBox>();
            foreach (var item in lstDivisions)
            {
                lstCaj.Add(new DataComboBox() { keyString = item.Id.ToString(), value = item.Name });
            }

            //Asignacion de combo cajeros.            
            chcbxOperador.DataBindings.Clear();
            chcbxOperador.Properties.DataSource = null;            
            chcbxOperador.Properties.ValueMember = "keyString";
            chcbxOperador.Properties.DisplayMember = "value";
            chcbxOperador.Properties.DataSource = lstCaj;
            chcbxOperador.RefreshEditValue();
        }
    }
}

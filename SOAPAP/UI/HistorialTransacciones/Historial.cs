using DevExpress.XtraBars.Docking2010;
using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Reportes;
using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SOAPAP.UI.HistorialTransacciones
{
    public partial class Historial : Form
    {
        Form loading;
        private RequestsAPI Requests = null;
        Form mensaje;
        DialogResult result = new DialogResult();

        private string UrlBase = Properties.Settings.Default.URL;
        string json = string.Empty;

        List<SOAPAP.Model.Users> lstCajeros = new List<Model.Users>();
        List<DataHistorial> lstData;

        public Historial()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }

        private async void Historial_Load(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            await CargarCombos();
            loading.Close();
        }

        private async Task CargarCombos()
        {
            //Combo de Cajeros.            
            List<DataComboBox> lstCaj = new List<DataComboBox>();
                       
            if (Variables.LoginModel.RolName[0] == "Supervisor")
            {
                //Solicitud de Cajeros.
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
                }                               
            }
            else
            {
                //Operador actual
                lstCaj.Add(new DataComboBox() { keyString = Variables.LoginModel.User, value = Variables.LoginModel.FullName });                
            }
            //combo de Cajeros
            chcbxOperador.DataBindings.Clear();
            chcbxOperador.Properties.DataSource = null;
            chcbxOperador.Properties.ValueMember = "keyString";
            chcbxOperador.Properties.DisplayMember = "value";
            chcbxOperador.Properties.DataSource = lstCaj;
            chcbxOperador.CheckAll();
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            await cargar();
            loading.Close();
        }

        #region Procesos
        public async Task cargar()
        {
            DateTime Fecha = dtpFecha.Value;

            ////Se obtiene el cajero para filtrar la consulta            
            var temp = chcbxOperador.Properties.Items.ToList();
            string idOperadorSeleccionado = "";
            
            if (Variables.LoginModel.RolName[0] == "Supervisor")
            {
                //Cajero(s) seleccionado(s)
                if (temp.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                {
                    idOperadorSeleccionado = "";
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar un cajero.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    foreach (var item in temp)
                    {
                        if (item.CheckState == CheckState.Checked)
                            idOperadorSeleccionado = idOperadorSeleccionado + item.Value + ",";
                    }
                    idOperadorSeleccionado = idOperadorSeleccionado.Substring(0, idOperadorSeleccionado.Length - 1);
                }
            }
            else
            {
                //Operador actual
                if (temp.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                {
                    idOperadorSeleccionado = "";
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar un cajero.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    idOperadorSeleccionado = temp.First().Value.ToString();
                }                
            }

            DataReportes dRep = new DataReportes()
            {
                FechaIni = Fecha.ToString("yyyy-MM-dd"),                
                CajeroId = idOperadorSeleccionado
            };

            HttpContent content;
            json = JsonConvert.SerializeObject(dRep);
            content = new StringContent(json, Encoding.UTF8, "application/json");

            var _resulTransaction = await Requests.SendURIAsync("/api/Reports/Historial", HttpMethod.Post, Variables.LoginModel.Token, content);

            if (_resulTransaction.Contains("error:"))
            {
                mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                lstData = JsonConvert.DeserializeObject<List<DataHistorial>>(_resulTransaction);

                if (lstData == null)
                {
                    mensaje = new MessageBoxForm("Sin Operaciones", "No se encontraron movimientos.", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    try
                    {
                        lstData = lstData.OrderBy(x => x.Hora).ToList();
                        pgcHistorial.DataSource = lstData;                        
                    }
                    catch (Exception e)
                    {
                        var res = e.Message;
                    }
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ////Metodo de exportar
            //DevExpress.Export.ExportSettings.DefaultExportType = DevExpress.Export.ExportType.DataAware;
            var pivotExportOptions = new DevExpress.XtraPivotGrid.PivotXlsxExportOptions();
            pivotExportOptions.ExportType = DevExpress.Export.ExportType.WYSIWYG;

            //Selecciono el directorio destino
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Selecciona el directorio destino.";

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string NombreFile = "OperacionDeCajaHistorico_" + Variables.LoginModel.FullName.Replace(" ", "") + "_" + DateTime.Now.ToString("yyyy-MM-dd");
                pgcHistorial.ExportToXlsx(fbd.SelectedPath + "\\" + NombreFile + ".xlsx", pivotExportOptions);
                Process.Start(fbd.SelectedPath + "\\" + NombreFile + ".xlsx");
                MessageBox.Show("Archivo " + NombreFile + ".xlsx" + " guardado.");
            }
        }

        private async void btnImprimir_Click(object sender, EventArgs e)
        {
            DateTime Fecha= dtpFecha.Value;
            var temp = chcbxOperador.Properties.Items.ToList();
            
            await new Movimientos().GeneratePDF(Fecha.ToString("yyyy-MM-dd"), getidCajeros() );
        }
       
        private string getidCajeros()
        {
            var temp = chcbxOperador.Properties.Items.ToList();
            string idOperadorSeleccionado = "";

            if (Variables.LoginModel.RolName[0] == "Supervisor")
            {
                //Cajero(s) seleccionado(s)
                if (temp.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                {
                    idOperadorSeleccionado = "";
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar un cajero.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    foreach (var item in temp)
                    {
                        if (item.CheckState == CheckState.Checked)
                            idOperadorSeleccionado = idOperadorSeleccionado + item.Value + ",";
                    }
                    idOperadorSeleccionado = idOperadorSeleccionado.Substring(0, idOperadorSeleccionado.Length - 1);
                }
            }
            else
            {
                //Operador actual
                if (temp.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                {
                    idOperadorSeleccionado = "";
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar un cajero.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    idOperadorSeleccionado = temp.First().Value.ToString();
                }
            }
            return idOperadorSeleccionado;
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string tag = ((WindowsUIButton)e.Button).Tag.ToString();
            switch (tag)
            {
                case "EX":
                    btnExportar_Click(sender, e);
                    break;
                case "GE":
                    btnCargar_Click(sender, e);
                    break;
                case "PR":
                    btnImprimir_Click(sender, e);
                    break;
            }
        }
        #endregion

        //private async void pgcHistorial_CellSelectionChanged(object sender, EventArgs e)
        //{            
        //    try
        //    {
        //        var Selected = ((DevExpress.XtraPivotGrid.PivotGridControl)sender).Cells.Selection;
        //        //int Indice = (lstData.Count - 1) - Selected.Y;
        //        var elementoSeleccionado = lstData[Selected.Y];

        //        if (elementoSeleccionado.TypeTransactionId == 3 || elementoSeleccionado.TypeTransactionId == 4) //Cobro o Cancelacion
        //        {
        //            string tmpFolio = elementoSeleccionado.TypeTransactionId == 3 ? elementoSeleccionado.folio : elementoSeleccionado.cancellation_folio;
        //            //Detalle
        //            var resultTypeTransaction = await Requests.SendURIAsync("/api/Reports/HistorialElemento/" + tmpFolio, HttpMethod.Get, Variables.LoginModel.Token);
        //            if (resultTypeTransaction.Contains("error"))
        //            {
        //                mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
        //                result = mensaje.ShowDialog();
        //            }
        //            else
        //            {
        //                List<DataHistorialElemento> lstPagos = JsonConvert.DeserializeObject<List<DataHistorialElemento>>(resultTypeTransaction);
        //                pgcDetalles.DataSource = lstPagos;                        
        //            }
        //        }
        //        else
        //        {
        //            List<DataHistorialElemento> lstPagos = new List<DataHistorialElemento>() { new DataHistorialElemento() { Descripcion = "Sin detalle" } };                    
        //            pgcDetalles.DataSource = lstPagos;
        //        }


        //    }
        //    catch (Exception ex){ }
        //}
    }
}

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

namespace SOAPAP.UI.ReportesForms
{
    public partial class RepOrders : Form
    {
        Form loading;
        private RequestsAPI Requests = null;
        Form mensaje;
        DialogResult result = new DialogResult();

        private string UrlBase = Properties.Settings.Default.URL;
        string json = string.Empty;

        public RepOrders()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }

        private async void RepOrders_Load(object sender, EventArgs e)
        {
            await CargarCombos();
        }

        private async Task CargarCombos()
        {
            //Combo de Cajeros.
            List<SOAPAP.Model.Users> lstCajeros = new List<Model.Users>();
            List<DataComboBox> lstCaj = new List<DataComboBox>();

            //Combo de Areas.
            List<SOAPAP.Model.Division> lstAreas = new List<Model.Division>();
            List<DataComboBox> lstAr = new List<DataComboBox>();

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

                //Solicitud de Areas
                var resultTypeTransactionOfi = await Requests.SendURIAsync("/api/Division", HttpMethod.Get, Variables.LoginModel.Token);
                if (resultTypeTransactionOfi.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", resultTypeTransactionOfi.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    lstAreas = JsonConvert.DeserializeObject<List<SOAPAP.Model.Division>>(resultTypeTransactionOfi);
                    foreach (var item in lstAreas)
                    {
                        lstAr.Add(new DataComboBox() { keyString = item.Id.ToString(), value = item.Name });
                    }
                }
            }
            else
            {
                //Operador actual
                lstCaj.Add(new DataComboBox() { keyString = Variables.LoginModel.User, value = Variables.LoginModel.FullName });

                ////Areas Actual
                //lstAr.Add(new DataComboBox() { keyString = Variables.Configuration.Terminal.BranchOffice.Name, value = Variables.Configuration.Terminal.BranchOffice.Name });
                //Solicitud de Areas
                var resultTypeTransactionOfi = await Requests.SendURIAsync("/api/Division", HttpMethod.Get, Variables.LoginModel.Token);
                if (resultTypeTransactionOfi.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", resultTypeTransactionOfi.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    lstAreas = JsonConvert.DeserializeObject<List<SOAPAP.Model.Division>>(resultTypeTransactionOfi);
                    foreach (var item in lstAreas)
                    {
                        lstAr.Add(new DataComboBox() { keyString = item.Id.ToString(), value = item.Name });
                    }
                }
            }
            //combo de Cajeros
            chcbxOperador.DataBindings.Clear();
            chcbxOperador.Properties.DataSource = null;
            chcbxOperador.Properties.ValueMember = "keyString";
            chcbxOperador.Properties.DisplayMember = "value";
            chcbxOperador.Properties.DataSource = lstCaj;
            //Combo de Areas
            chcbxArea.DataBindings.Clear();
            chcbxArea.Properties.DataSource = null;
            chcbxArea.Properties.ValueMember = "keyString";
            chcbxArea.Properties.DisplayMember = "value";
            chcbxArea.Properties.DataSource = lstAr;

            if (Variables.LoginModel.RolName[0] != "Supervisor")
            {
                chcbxOperador.CheckAll();
                chcbxArea.CheckAll();
            }            
        }

        private async void btnGenerar_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            await cargar();
            loading.Close();
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
                string NombreFile = "Ordenes_" + Variables.LoginModel.FullName.Replace(" ", "") + "_" + DateTime.Now.ToString("yyyy-MM-dd");
                pgcOrders.ExportToXlsx(fbd.SelectedPath + "\\" + NombreFile + ".xlsx", pivotExportOptions);
                Process.Start(fbd.SelectedPath + "\\" + NombreFile + ".xlsx");
                MessageBox.Show("Archivo " + NombreFile + ".xlsx" + " guardado.");
            }
        }

        public async Task cargar()
        {
            DateTime FechaIni = dtpFechaIni.Value;
            DateTime FechaFin = dtpFechaFin.Value;

            ////Se obtiene el cajero para filtrar la consulta            
            var temp = chcbxOperador.Properties.Items.ToList();
            string itemSeleccionado = "";
            ////Se la oficina para la consulta.            
            var tempAr = chcbxArea.Properties.Items.ToList();
            string ArSeleccionado = "";

            //Cajero(s) seleccionado(s)
            if (temp.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
            {
                itemSeleccionado = "";
                mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar un cajero.", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                foreach (var item in temp)
                {
                    if (item.CheckState == CheckState.Checked)
                        itemSeleccionado = itemSeleccionado + item.Value + ",";
                }
                itemSeleccionado = itemSeleccionado.Substring(0, itemSeleccionado.Length - 1);
            }

            //Oficina(s) seleccionada(s)
            if (tempAr.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
            {
                ArSeleccionado = "";
                mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar una oficina.", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                foreach (var item in tempAr)
                {
                    if (item.CheckState == CheckState.Checked)
                        ArSeleccionado = ArSeleccionado + item.Value + ",";
                }
                ArSeleccionado = ArSeleccionado.Substring(0, ArSeleccionado.Length - 1);
            }

            DataReportes dRep = new DataReportes() { FechaIni = FechaIni.ToString("yyyy-MM-dd"), FechaFin = FechaFin.ToString("yyyy-MM-dd"), CajeroId = itemSeleccionado, Oficinas = ArSeleccionado };

            HttpContent content;
            json = JsonConvert.SerializeObject(dRep);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var _resulTransaction = await Requests.SendURIAsync("/api/Reports/Orders", HttpMethod.Post, Variables.LoginModel.Token, content);

            if (_resulTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                List<DataOrders> lstData = JsonConvert.DeserializeObject<List<DataOrders>>(_resulTransaction);                
                if (lstData == null)
                {
                    mensaje = new MessageBoxForm("Sin Operaciones", "No se encontraron movimientos.", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    try
                    {
                        pgcOrders.DataSource = lstData;
                    }
                    catch (Exception e)
                    {
                        var res = e.Message;
                    }
                }
            }
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
                    btnGenerar_Click(sender, e);
                    break;

            }
        }
    }
}

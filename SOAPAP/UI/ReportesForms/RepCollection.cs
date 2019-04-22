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
    public partial class RepCollection : Form
    {
        Form loading;
        private RequestsAPI Requests = null;
        Form mensaje;
        DialogResult result = new DialogResult();

        private string UrlBase = Properties.Settings.Default.URL;
        string json = string.Empty;

        public RepCollection()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }

        private async void RepCollection_Load(object sender, EventArgs e)
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
                var resultTypeTransaction = await Requests.SendURIAsync("/api/UserRolesManager/GetUserByRoleName/User", HttpMethod.Get, Variables.LoginModel.Token);
                if (resultTypeTransaction.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    var lstCajeros = JsonConvert.DeserializeObject<List<SOAPAP.Model.ApplicationUser>>(resultTypeTransaction);
                    lstCaj.Add(new DataComboBox() { keyString = "Todos", value = "Todos" });
                    foreach (var item in lstCajeros)
                    {
                        lstCaj.Add(new DataComboBox() { keyString = string.Format("{0} {1} {2}", item.Name, item.LastName, item.SecondLastName), value = string.Format("{0} {1} {2}", item.Name, item.LastName, item.SecondLastName) });
                    }
                }
            }
            else
            {
                lstCaj.Add(new DataComboBox() { keyString = Variables.LoginModel.FullName, value = Variables.LoginModel.FullName });
            }

            cbxOperador.ValueMember = "keyString";
            cbxOperador.DisplayMember = "value";
            cbxOperador.DataSource = lstCaj;
            cbxOperador.SelectedIndex = 0;
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
                string NombreFile = "IngresosDeCaja_" + Variables.LoginModel.FullName.Replace(" ", "") + "_" + DateTime.Now.ToString("yyyy-MM-dd");
                pgcCollection.ExportToXlsx(fbd.SelectedPath + "\\" + NombreFile + ".xlsx", pivotExportOptions);                
                Process.Start(fbd.SelectedPath + "\\" + NombreFile + ".xlsx");
                MessageBox.Show("Archivo " + NombreFile + ".xlsx" + " guardado.");
            }
        }

        #region Procesos
        public async Task cargar()
        {
            DataReportes dRep = new DataReportes();

            //Se Obtiene el rango de fechas.
            dRep.FechaIni = dtpFechaIni.Value.ToString("yyyy-MM-dd");
            dRep.FechaFin = dtpFechaFin.Value.ToString("yyyy-MM-dd");

            //Se obtiene el cajero para filtrar la consulta
            string itemSeleccionado = ((DataComboBox)cbxOperador.SelectedItem).keyString;
            if (itemSeleccionado == "Todos")
            {
                dRep.CajeroNombre = "Todos";
            }
            else
            {
                dRep.CajeroNombre = itemSeleccionado.Split(' ')[0];
                dRep.CajeroAPaterno = itemSeleccionado.Split(' ')[1];
                dRep.CajeroAMaterno = itemSeleccionado.Split(' ')[2];
            }

            HttpContent content;
            json = JsonConvert.SerializeObject(dRep);
            content = new StringContent(json, Encoding.UTF8, "application/json");

            var _resulTransaction = await Requests.SendURIAsync("/api/Reports/Collection", HttpMethod.Post, Variables.LoginModel.Token, content);

            if (_resulTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var lstData = JsonConvert.DeserializeObject<List<DataCollection>>(_resulTransaction);

                if (lstData == null)
                {
                    mensaje = new MessageBoxForm("Sin Operaciones", "No se encontraron movimientos.", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }

                try
                {
                    //Filtros finales                    
                    pgcCollection.DataSource = lstData;
                   
                }
                catch (Exception e)
                {
                    var res = e.Message;
                }

            }
        }



        #endregion
    }
}

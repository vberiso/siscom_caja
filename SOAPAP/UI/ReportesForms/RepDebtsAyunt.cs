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
    public partial class RepDebtsAyunt : Form
    {
        Form loading;
        private RequestsAPI Requests = null;
        Form mensaje;
        DialogResult result = new DialogResult();

        private string UrlBase = Properties.Settings.Default.URL;
        string json = string.Empty;

        public RepDebtsAyunt()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }

        private async void RepDebtsAyunt_Load(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            btnGenerar.Enabled = false;
            await CargarCombos();
            loading.Close();
            btnGenerar.Enabled = true;
        }

        private async Task CargarCombos()
        {
            //Combo de Colonias.
            List<DataComboBox> lstColonias = new List<DataComboBox>();
            var resultTypeTransaction2 = await Requests.SendURIAsync("/api/Towns/2/Suburbs", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction2.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultTypeTransaction2.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var lstTmpColonias = JsonConvert.DeserializeObject<List<Suburb>>(resultTypeTransaction2);
                foreach (var item in lstTmpColonias.OrderBy(x => x.Name))
                {
                    lstColonias.Add(new DataComboBox() { keyInt = item.Id, value = item.Name });
                }
            }
            chlbxColonia.ValueMember = "keyString";
            chlbxColonia.DisplayMember = "value";
            chlbxColonia.DataSource = lstColonias;
        }

        private async void btnGenerar_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            await cargar();
            loading.Close();
        }

        private async void btnExportar_Click(object sender, EventArgs e)
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
                string NombreFile = "Adeudos_" + Variables.LoginModel.FullName.Replace(" ", "") + "_" + DateTime.Now.ToString("yyyy-MM-dd");
                pgcAdeudos.ExportToXlsx(fbd.SelectedPath + "\\" + NombreFile + ".xlsx", pivotExportOptions);
                Process.Start(fbd.SelectedPath + "\\" + NombreFile + ".xlsx");
                MessageBox.Show("Archivo " + NombreFile + ".xlsx" + " guardado.");
            }
        }

        public async Task cargar()
        {
            var id = Variables.LoginModel.User;

            ////Se obtiene las Colonias          
            var itemsCol = chlbxColonia.CheckedItems;
            string lstColonias = "";
            if (itemsCol.Count == 0)
            {
                mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar una colonia.", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
                return;
            }
            else
            {
                foreach (SOAPAP.Reportes.DataComboBox item in itemsCol)
                {
                    //lstColonia.Add(item);
                    lstColonias = lstColonias + item.keyInt + ",";
                }
                lstColonias = lstColonias.Substring(0, lstColonias.Length - 1);
            }

            HttpContent content;
            json = JsonConvert.SerializeObject(lstColonias);
            content = new StringContent(json, Encoding.UTF8, "application/json");

            var _resulTransaction = await Requests.SendURIAsync("/api/Reports/DebtsCouncil" , HttpMethod.Post, Variables.LoginModel.Token, content);

            if (_resulTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                List<DataDebtsAyunt> lstData = JsonConvert.DeserializeObject<List<DataDebtsAyunt>>(_resulTransaction);
                if (lstData == null)
                {
                    mensaje = new MessageBoxForm("Sin Operaciones", "No se encontraron movimientos.", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    try
                    {
                        pgcAdeudos.DataSource = lstData;
                    }
                    catch (Exception e)
                    {
                        var res = e.Message;
                    }
                }
            }
        }

        private void cheColonia_CheckedChanged(object sender, EventArgs e)
        {
            var estaChecado = ((DevExpress.XtraEditors.CheckEdit)sender).Checked;
            if (estaChecado)
            {
                chlbxColonia.CheckAll();
            }
            else
            {
                chlbxColonia.UnCheckAll();
            }
        }
    }
}

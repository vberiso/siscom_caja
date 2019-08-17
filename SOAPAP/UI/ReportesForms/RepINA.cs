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
    public partial class RepINA : Form
    {
        Form loading;
        private RequestsAPI Requests = null;
        Form mensaje;
        DialogResult result = new DialogResult();

        private string UrlBase = Properties.Settings.Default.URL;
        string json = string.Empty;

        public RepINA()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }

        private void RepINA_Load(object sender, EventArgs e)
        {
            ////Se establece los timer para seleccion de año.            
            //dtpFechaIni.Format = DateTimePickerFormat.Custom;
            //dtpFechaIni.CustomFormat = "yyyy";
            //dtpFechaIni.ShowUpDown = true;

            //dtpFechaFin.Format = DateTimePickerFormat.Custom;
            //dtpFechaFin.CustomFormat = "yyyy";
            //dtpFechaFin.ShowUpDown = true;
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
                string NombreFile = "ReporteFraccionamientosNuevos_" + Variables.LoginModel.FullName.Replace(" ", "") + "_" + DateTime.Now.ToString("yyyy-MM-dd");
                pgcINA.ExportToXlsx(fbd.SelectedPath + "\\" + NombreFile + ".xlsx", pivotExportOptions);
                Process.Start(fbd.SelectedPath + "\\" + NombreFile + ".xlsx");
                MessageBox.Show("Archivo " + NombreFile + ".xlsx" + " guardado.");
            }
        }

        public async Task cargar()
        {
            DateTime FechaIni = dtpFechaIni.Value;
            DateTime FechaFin = dtpFechaFin.Value;
            string Fechas = FechaIni.ToString("yyyy-MM-dd") + "/" + FechaFin.ToString("yyyy-MM-dd");
            var id = Variables.LoginModel.User;
            try
            {
                var _resulTransaction = await Requests.SendURIAsync("/api/Reports/IncomeNewAccounts/" + Fechas, HttpMethod.Get, Variables.LoginModel.Token);
            
                if (_resulTransaction.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    List<DataIncomeNewAccounts> lstData = JsonConvert.DeserializeObject<List<DataIncomeNewAccounts>>(_resulTransaction);
                
                    if (lstData == null)
                    {
                        mensaje = new MessageBoxForm("Sin Operaciones", "No se encontraron movimientos.", TypeIcon.Icon.Warning);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        try
                        {
                            pgcINA.DataSource = lstData;
                        }
                        catch (Exception e)
                        {
                            var res = e.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

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

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
    public partial class RepPadronWater : Form
    {
        Form loading;
        private RequestsAPI Requests = null;
        Form mensaje;
        DialogResult result = new DialogResult();

        private string UrlBase = Properties.Settings.Default.URL;
        string json = string.Empty;

        public RepPadronWater()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }

        private async void RepPadronWater_Load(object sender, EventArgs e)
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
            //Se envia como fecha inicial el primer dia del mes actual.
            dtpFechaIni.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);

            //Combo de Rutas.
            List<DataComboBox> lstRutas = new List<DataComboBox>();
            var resultTypeTransaction = await Requests.SendURIAsync("/api/Agreements/getRoutesFromAgreement", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var lstTmpRutas = JsonConvert.DeserializeObject<List<int>>(resultTypeTransaction);                
                foreach (var item in lstTmpRutas)
                {
                    if (item != 0)
                        lstRutas.Add(new DataComboBox() { keyInt = item, value = item.ToString() });
                }
            }
            chlbxRuta.ValueMember = "keyInt";
            chlbxRuta.DisplayMember = "value";
            chlbxRuta.DataSource = lstRutas;

            //Combo de Colonias.
            List<DataComboBox> lstColonias = new List<DataComboBox>();
            var resultTypeTransaction2 = await Requests.SendURIAsync("/api/Towns/2/Suburbs", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction.Contains("error"))
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
            
            //Combo estate de servicio.
            List<DataComboBox> lstStatusServicio = new List<DataComboBox>();
            var resultTypeTransaction3 = await Requests.SendURIAsync("/api/TypeStateServices", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction3.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultTypeTransaction3.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var lstTmpSS = JsonConvert.DeserializeObject<List<TypeStateService>>(resultTypeTransaction3);
                foreach (var item in lstTmpSS.OrderBy(x => x.name))
                {
                    lstStatusServicio.Add(new DataComboBox() { keyInt = item.id, value = item.name });
                }
            }            
            chlbxServicio.ValueMember = "keyString";
            chlbxServicio.DisplayMember = "value";
            chlbxServicio.DataSource = lstStatusServicio;
            chlbxServicio.SelectedIndex = 0;

            //Combo tipo toma.
            List<DataComboBox> lstTipoToma = new List<DataComboBox>();
            var resultTypeTransaction4 = await Requests.SendURIAsync("/api/TypeIntakes", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction4.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultTypeTransaction4.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var lstTmpTI = JsonConvert.DeserializeObject<List<TypeIntake>>(resultTypeTransaction4);
                foreach (var item in lstTmpTI.OrderBy(x => x.name))
                {
                    lstTipoToma.Add(new DataComboBox() { keyInt = item.id, value = item.name });
                }
            }            
            chlbxToma.ValueMember = "keyInt";
            chlbxToma.DisplayMember = "value";
            chlbxToma.DataSource = lstTipoToma;
            chlbxToma.SelectedIndex = 0;

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
                string NombreFile = "PadronAgua_" + Variables.LoginModel.FullName.Replace(" ", "") + "_" + DateTime.Now.ToString("yyyy-MM-dd");
                pgcRepPadronWater.ExportToXlsx(fbd.SelectedPath + "\\" + NombreFile + ".xlsx", pivotExportOptions);
                Process.Start(fbd.SelectedPath + "\\" + NombreFile + ".xlsx");
                MessageBox.Show("Archivo " + NombreFile + ".xlsx" + " guardado.");
            }
        }

        #region Procesos
        public async Task cargar()
        {
            DateTime FechaIni = dtpFechaIni.Value;
            DateTime FechaFin = dtpFechaFin.Value;

            var id = Variables.LoginModel.User;

            ////Se obtiene las rutas            
            var itemsRuta = chlbxRuta.CheckedItems;
            List<DataComboBox> lstRuta = new List<DataComboBox>();
            if (itemsRuta.Count == 0)
            {                
                mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar una ruta.", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
                return;
            }
            else
            {
                foreach (SOAPAP.Reportes.DataComboBox item in itemsRuta)
                {
                    lstRuta.Add(item);
                }                
            }

            ////Se obtiene las Colonias          
            var itemsCol = chlbxColonia.CheckedItems;
            List<DataComboBox> lstColonia = new List<DataComboBox>();
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
                    lstColonia.Add(item);
                }                
            }

            ////Se obtiene los estatus de Servicios            
            var itemsServ = chlbxServicio.CheckedItems;
            List<DataComboBox> lstServicio = new List<DataComboBox>();
            if (itemsServ.Count == 0)
            {                
                mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar almenos un estatus de Servicio.", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
                return;
            }
            else
            {
                foreach (SOAPAP.Reportes.DataComboBox item in itemsServ)
                {
                    lstServicio.Add(item);
                }                
            }

            ////Se obtiene los tipo de Tomas         
            var itemsToma = chlbxToma.CheckedItems;
            List<DataComboBox> lstTomas = new List<DataComboBox>();
            if (itemsToma.Count == 0)
            {                
                mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar almenos un tipo de toma.", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
                return;
            }
            else
            {
                foreach (SOAPAP.Reportes.DataComboBox item in itemsToma)
                {
                    lstTomas.Add(item);
                }                
            }

            

            DataReportes dRep = new DataReportes()
                {
                FechaIni = FechaIni.ToString("yyyy-MM-dd"),
                FechaFin = FechaFin.ToString("yyyy-MM-dd"),
                CajeroId = id,                
                pwaFiltrarPorContrato = tswtFiltro.IsOn     //chxPorFechaContrato.Checked
            };

            HttpContent content;
            json = JsonConvert.SerializeObject(dRep);
            content = new StringContent(json, Encoding.UTF8, "application/json");

            var _resulTransaction = await Requests.SendURIAsync("/api/Reports/PadronWater", HttpMethod.Post, Variables.LoginModel.Token, content);

            if (_resulTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                List<DataPadronWater> lstData = JsonConvert.DeserializeObject<List<DataPadronWater>>(_resulTransaction);
                List<DataPadronWater> lstTMP = new List<DataPadronWater>();
                if (lstData == null)
                {
                    mensaje = new MessageBoxForm("Sin Operaciones", "No se encontraron movimientos.", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    //Filtro por la rutas seleccionadas.
                    if(chlbxRuta.CheckedItems.Count == chlbxRuta.ItemCount)  //Estan seleccionados todos.
                    {
                        lstTMP = lstData;
                    }
                    else
                    {
                        lstTMP = lstData.Where(x => lstRuta.Select(y => y.keyInt.ToString()).Contains(x.RUTA)).ToList();
                    }

                    ////Filtro de Colonias Seleccionadas
                    if (chlbxColonia.CheckedItems.Count != chlbxColonia.ItemCount)                    
                    {
                        lstTMP = lstTMP.Where(x => lstColonia.Select(y => y.keyInt).Contains(x.idCOLONIA)).ToList();
                    }
                    
                    ////Filtro de Estatus de servicio
                    if(chlbxServicio.CheckedItems.Count != chlbxServicio.ItemCount)                    
                    {
                        lstTMP = lstTMP.Where(x => lstServicio.Select(y => y.keyInt).Contains(x.idESTATUS)).ToList();
                    }
                    
                    ////Filtro de Tipo de toma
                    if(chlbxToma.CheckedItems.Count != chlbxToma.ItemCount)
                    {
                        lstTMP = lstTMP.Where(x => lstTomas.Select(y => y.keyInt).Contains(x.idTIPO_TOMA)).ToList();
                    }
                    
                    
                    //Filtro por cantidad de adeudo mayor a.
                    if (numericUpDown1.Value > 0)
                    {
                        lstTMP = lstTMP.Where(x => x.ADEUDO > numericUpDown1.Value).ToList();
                    }

                    try
                    {
                        pgcRepPadronWater.DataSource = lstTMP;
                    }
                    catch (Exception e)
                    {
                        var res = e.Message;
                    }
                }
            }
        }



        #endregion

        private void cheRuta_CheckedChanged(object sender, EventArgs e)
        {
            var estaChecado = ((DevExpress.XtraEditors.CheckEdit)sender).Checked;
            if (estaChecado)
            {
                chlbxRuta.CheckAll();
            }
            else
            {
                chlbxRuta.UnCheckAll();
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

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            var estaChecado = ((DevExpress.XtraEditors.CheckEdit)sender).Checked;
            if (estaChecado)
            {
                chlbxServicio.CheckAll();
            }
            else
            {
                chlbxServicio.UnCheckAll();
            }
        }

        private void cheToma_CheckedChanged(object sender, EventArgs e)
        {
            var estaChecado = ((DevExpress.XtraEditors.CheckEdit)sender).Checked;
            if (estaChecado)
            {
                chlbxToma.CheckAll();
            }
            else
            {
                chlbxToma.UnCheckAll();
            }
        }
    }
}

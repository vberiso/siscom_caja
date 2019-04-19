using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Reportes;
using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                lstRutas.Add(new DataComboBox() { keyInt = 0, value = "Todos" });
                foreach (var item in lstTmpRutas)
                {
                    if (item != 0)
                        lstRutas.Add(new DataComboBox() { keyInt = item, value = item.ToString() });
                }
            }
            lbxRuta.ValueMember = "keyInt";
            lbxRuta.DisplayMember = "value";
            lbxRuta.DataSource = lstRutas;
            lbxRuta.SelectedIndex = 0;

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
            lstColonias.Insert(0, new DataComboBox() { keyInt = 0, value = "Todos" });
            lbxColonia.ValueMember = "keyString";
            lbxColonia.DisplayMember = "value";
            lbxColonia.DataSource = lstColonias;
            lbxColonia.SelectedIndex = 0;

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
            lstStatusServicio.Insert(0, new DataComboBox() { keyInt = 0, value = "Todos" });
            lbxServicio.ValueMember = "keyString";
            lbxServicio.DisplayMember = "value";
            lbxServicio.DataSource = lstStatusServicio;
            lbxServicio.SelectedIndex = 0;

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
            lstTipoToma.Insert(0, new DataComboBox() { keyInt = 0, value = "Todos" });
            lbxTipoToma.ValueMember = "keyInt";
            lbxTipoToma.DisplayMember = "value";
            lbxTipoToma.DataSource = lstTipoToma;
            lbxTipoToma.SelectedIndex = 0;




            //Combo Areas o Concepto de pago
            //List<DataComboBox> lstRutas = new List<DataComboBox>();
            //lstAreas.Add(new DataComboBox() { keyInt = 1, value = "Agua" });
            //cbxArea.ValueMember = "keyInt";
            //cbxArea.DisplayMember = "value";
            //cbxArea.DataSource = lstAreas;
            //cbxArea.SelectedIndex = 0;

            ////Combo de Cajeros.
            //List<DataComboBox> lstCaj = new List<DataComboBox>();
            //if (Variables.LoginModel.RolName[0] == "Supervisor")
            //{
            //    var resultTypeTransaction = await Requests.SendURIAsync("/api/UserRolesManager/GetUserByRoleName/User", HttpMethod.Get, Variables.LoginModel.Token);
            //    if (resultTypeTransaction.Contains("error"))
            //    {
            //        mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
            //        result = mensaje.ShowDialog();
            //    }
            //    else
            //    {
            //        var lstCajeros = JsonConvert.DeserializeObject<List<SOAPAP.Model.ApplicationUser>>(resultTypeTransaction);
            //        lstCaj.Add(new DataComboBox() { keyString = "Todos", value = "Todos" });
            //        foreach (var item in lstCajeros)
            //        {
            //            lstCaj.Add(new DataComboBox() { keyString = string.Format("{0} {1} {2}", item.Name, item.LastName, item.SecondLastName), value = string.Format("{0} {1} {2}", item.Name, item.LastName, item.SecondLastName) });
            //        }
            //    }
            //}
            //else
            //{
            //    lstCaj.Add(new DataComboBox() { keyString = Variables.LoginModel.FullName, value = Variables.LoginModel.FullName });
            //}

            //cbxOperador.ValueMember = "keyString";
            //cbxOperador.DisplayMember = "value";
            //cbxOperador.DataSource = lstCaj;
            //cbxOperador.SelectedIndex = 0;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            cargar();
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
                MessageBox.Show("Archivo " + NombreFile + ".xlsx" + " guardado.");
            }
        }

        #region Procesos
        public async Task cargar()
        {
            DateTime FechaIni = dtpFechaIni.Value;
            DateTime FechaFin = dtpFechaFin.Value;

            var id = Variables.LoginModel.User;
            DataReportes dRep = new DataReportes() { FechaIni = FechaIni.ToString("yyyy-MM-dd"), FechaFin = FechaFin.ToString("yyyy-MM-dd"), CajeroId = id };

            dRep.pwaFiltrarPorContrato = chxPorFechaContrato.Checked;

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
                    List<DataComboBox> lstRutaSelected = new List<DataComboBox>();
                    foreach (var item in lbxRuta.SelectedItems)
                        lstRutaSelected.Add((DataComboBox)item);
                    if (lstRutaSelected.Count == 0)
                    {
                        lbxRuta.SetSelected(0, true);
                        lbxRuta.Refresh();
                    }
                    if (lstRutaSelected.Any(x => x.value == "Todos") || lstRutaSelected.Count == 0)
                    {
                        lstTMP = lstData;
                    }
                    else
                    {
                        lstTMP = lstData.Where(x => lstRutaSelected.Select(y => y.keyInt).Contains(x.RUTA)).ToList();
                    }

                    //Filtro de Colonias Seleccionadas
                    List<DataComboBox> lstColoniaSelected = new List<DataComboBox>();
                    foreach (var item in lbxColonia.SelectedItems)
                        lstColoniaSelected.Add((DataComboBox)item);
                    if (lstColoniaSelected.Count == 0)
                    {
                        lbxColonia.SetSelected(0, true);
                        lbxColonia.Refresh();
                    }
                    if (lstColoniaSelected.Any(x => x.value == "Todos") || lstColoniaSelected.Count == 0)
                    {
                        //En este caso no se hace ningun filtro
                    }
                    else
                    {
                        lstTMP = lstTMP.Where(x => lstColoniaSelected.Select(y => y.keyInt).Contains(x.idCOLONIA)).ToList();
                    }

                    //Filtro de Estatus de servicio
                    List<DataComboBox> lstEstatusSevicioSelected = new List<DataComboBox>();
                    foreach (var item in lbxServicio.SelectedItems)
                        lstEstatusSevicioSelected.Add((DataComboBox)item);
                    if (lstEstatusSevicioSelected.Count == 0)
                    {
                        lbxServicio.SetSelected(0, true);
                        lbxServicio.Refresh();
                    }
                    if (lstEstatusSevicioSelected.Any(x => x.value == "Todos") || lstEstatusSevicioSelected.Count == 0)
                    {
                        //En este caso no se hace ningun filtro
                    }
                    else
                    {
                        lstTMP = lstTMP.Where(x => lstEstatusSevicioSelected.Select(y => y.keyInt).Contains(x.idESTATUS)).ToList();
                    }

                    //Filtro de Tipo de toma
                    List<DataComboBox> lstTipoTomaSelected = new List<DataComboBox>();
                    foreach (var item in lbxTipoToma.SelectedItems)
                        lstTipoTomaSelected.Add((DataComboBox)item);
                    if (lstTipoTomaSelected.Count == 0)
                    {
                        lbxTipoToma.SetSelected(0, true);
                        lbxTipoToma.Refresh();
                    }
                    if (lstTipoTomaSelected.Any(x => x.value == "Todos"))
                    {
                        //En este caso no se hace ningun filtro
                    }
                    else
                    {
                        lstTMP = lstTMP.Where(x => lstTipoTomaSelected.Select(y => y.keyInt).Contains(x.idTIPO_TOMA)).ToList();
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

       
    }
}

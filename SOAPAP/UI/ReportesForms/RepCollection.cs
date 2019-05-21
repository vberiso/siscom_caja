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
            //Listados de Cajeros.
            List<DataComboBox> lstCaj = new List<DataComboBox>();

            //Listados de Oficinas.
            List<SOAPAP.Model.BranchOffice> lstOficinas = new List<Model.BranchOffice>();
            List<DataComboBox> lstOfi = new List<DataComboBox>();

            if (Variables.LoginModel.RolName[0] == "Supervisor")
            {
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
                }

                //Peticion de Oficinas
                var resultTypeTransactionOfi = await Requests.SendURIAsync("/api/BranchOffice/Terminals", HttpMethod.Get, Variables.LoginModel.Token);
                if (resultTypeTransactionOfi.Contains("error"))
                {
                    mensaje = new MessageBoxForm("Error", resultTypeTransactionOfi.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    lstOficinas = JsonConvert.DeserializeObject<List<SOAPAP.Model.BranchOffice>>(resultTypeTransactionOfi);
                    foreach (var item in lstOficinas)
                    {
                        lstOfi.Add(new DataComboBox() { keyString = item.Name, value = item.Name });
                    }
                }
            }
            else
            {
                //Cajero
                lstCaj.Add(new DataComboBox() { keyString = Variables.LoginModel.User, value = Variables.LoginModel.FullName });
                //Oficina
                lstOfi.Add(new DataComboBox() { keyString = Variables.Configuration.Terminal.BranchOffice.Name, value = Variables.Configuration.Terminal.BranchOffice.Name });
            }

            //Asignacion de combo cajeros.
            chcbxOperador.DataBindings.Clear();
            chcbxOperador.Properties.DataSource = null;
            chcbxOperador.Properties.ValueMember = "keyString";
            chcbxOperador.Properties.DisplayMember = "value";
            chcbxOperador.Properties.DataSource = lstCaj;

            //Asignacion de combo Oficinas
            chcbxOficina.DataBindings.Clear();
            chcbxOficina.Properties.DataSource = null;
            chcbxOficina.Properties.ValueMember = "keyString";
            chcbxOficina.Properties.DisplayMember = "value";
            chcbxOficina.Properties.DataSource = lstOfi;

            if (Variables.LoginModel.RolName[0] != "Supervisor")
            {
                chcbxOperador.CheckAll();
                chcbxOficina.CheckAll();
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
                string NombreFile = "Recaudacion_" + Variables.LoginModel.FullName.Replace(" ", "") + "_" + DateTime.Now.ToString("yyyy-MM-dd");
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

            //Operadores seleccionados del combo de cajeros
            var itemsOpe = chcbxOperador.Properties.Items.ToList();
            string itemSeleccionado = "";
            //Oficinas seleccionadas del combo de oficinas.
            var itemsOfi = chcbxOficina.Properties.Items.ToList();
            string OfiSeleccionado = "";
            if (Variables.LoginModel.RolName[0] == "Supervisor")
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
                            itemSeleccionado = itemSeleccionado + item.Value + ",";
                    }
                    itemSeleccionado = itemSeleccionado.Substring(0, itemSeleccionado.Length - 1);
                    dRep.CajeroId = itemSeleccionado;
                }

                ////Se la oficina para la consulta.
                if (itemsOfi.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                {
                    OfiSeleccionado = "";
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar una oficina.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }                
                else
                {
                    foreach (var item in itemsOfi)
                    {
                        if (item.CheckState == CheckState.Checked)
                            OfiSeleccionado = OfiSeleccionado + item.Value + ",";
                    }
                    OfiSeleccionado = OfiSeleccionado.Substring(0, OfiSeleccionado.Length - 1);
                    dRep.Oficinas = OfiSeleccionado;
                }
            }
            else
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
                    itemSeleccionado = itemsOpe.First().Value.ToString();
                    dRep.CajeroId = itemSeleccionado;
                }

                ////Se la oficina para la consulta.
                if (itemsOfi.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                {
                    OfiSeleccionado = "";
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar una oficina.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    OfiSeleccionado = itemsOfi.First().Value.ToString();
                    dRep.Oficinas = OfiSeleccionado;
                }
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

using Newtonsoft.Json;
using SOAPAP.Reportes;
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
using SOAPAP.Services;
using SOAPAP.Enums;
using Microsoft.Reporting.WinForms;
using DevExpress.XtraBars.Docking2010;

namespace SOAPAP.UI.ReportesForms
{
    public partial class ReporteIFB : Form
    {
        Form loading;
        private RequestsAPI Requests = null;
        Form mensaje;
        DialogResult result = new DialogResult();

        private string UrlBase = Properties.Settings.Default.URL;
        string json = string.Empty;

        public ReporteIFB()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();            
        }

        private async void ReporteIFB_Load(object sender, EventArgs e)
        {
            await CargarCombos();
            this.rvwReportes.RefreshReport();
        }

        private async Task CargarCombos()
        {
            //Combo Areas o Concepto de pago
            List<DataComboBox> lstAreas = new List<DataComboBox>();
            lstAreas.Add(new DataComboBox() { keyInt = 1, value = "Agua" });
            //lstAreas.Add(new DataComboBox() { keyInt = 1, value = "Limpia" });
            //lstAreas.Add(new DataComboBox() { keyInt = 1, value = "Predial" });
            //lstAreas.Add(new DataComboBox() { keyInt = 1, value = "Tesorería" });
            cbxArea.ValueMember = "keyInt";
            cbxArea.DisplayMember = "value";
            cbxArea.DataSource = lstAreas;
            cbxArea.SelectedIndex = 0;

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

        private void button2_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            cargar();
            loading.Close();
        }

        #region Procesos
        public async Task cargar()
        {
            DataReportes dRep = new DataReportes();
            //{ FechaIni = FechaIni.ToString("yyyy-MM-dd"), FechaFin = FechaFin.ToString("yyyy-MM-dd"), CajeroId = id, statusIFB = statusIFB }

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
            
            //Se obtienen los tipos de pago            
            List<string> lstEstados = new List<string>();            
            if (rdbMosCancelados.Checked)
            {
                dRep.statusIFB = "EP001,EP002";
                lstEstados.Add("EP001");
                lstEstados.Add("EP002");
            }                
            else if (rdbSoloCancelados.Checked)
            {
                dRep.statusIFB = "EP002";                
                lstEstados.Add("EP002");
            }                
            else
            {
                dRep.statusIFB = "EP001";
                lstEstados.Add("EP001");
            }
                

            HttpContent content;
            json = JsonConvert.SerializeObject(dRep);
            content = new StringContent(json, Encoding.UTF8, "application/json");

            var _resulTransaction = await Requests.SendURIAsync("/api/Reports/IncomeFromBox", HttpMethod.Post, Variables.LoginModel.Token, content);

            if (_resulTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var lstData = JsonConvert.DeserializeObject<List<DataIncomeFromBox>>(_resulTransaction);

                if (lstData == null)
                {
                    mensaje = new MessageBoxForm("Sin Operaciones", "No se encontraron movimientos.", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
                
                try
                {
                    //Filtros finales
                    var lstFinal = lstData.Where(x => lstEstados.Contains(x.status)).ToList();

                    this.rvwReportes.LocalReport.ReportEmbeddedResource = "SOAPAP.Reportes.IncomeFromBoxReport.rdlc";
                    this.rvwReportes.LocalReport.DataSources.Clear();

                    ReportDataSource rds1 = new ReportDataSource("IFB", lstFinal);
                    this.rvwReportes.LocalReport.DataSources.Add(rds1);

                    //InfoRep INFOREP = new InfoRep() { NombreEmpresa = "", FechaDeImpresion = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Cajero = Variables.LoginModel.FullName, imgLogo = "" };
                    //ReportDataSource rds2 = new ReportDataSource("INFO", INFOREP);
                    //this.rvwReportes.LocalReport.DataSources.Add(rds2);

                    //var tmp = Variables.ImagenData.Rows;

                    //ReportDataSource rdsImagen = new ReportDataSource("Imagenes", Variables.ImagenData);
                    //rvwReportes.LocalReport.DataSources.Add(rdsImagen);

                    this.rvwReportes.RefreshReport();
                }
                catch (Exception e)
                {
                    var res = e.Message;
                }
                                
            }
        }



        #endregion

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string tag = ((WindowsUIButton)e.Button).Tag.ToString();
            switch (tag)
            {
                
                case "GE":
                    button2_Click(sender, e);
                    break;

            }
        }
    }
}

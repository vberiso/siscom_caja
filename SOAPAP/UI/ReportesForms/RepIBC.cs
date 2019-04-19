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
    public partial class RepIBC : Form
    {
        Form loading;
        private RequestsAPI Requests = null;
        Form mensaje;
        DialogResult result = new DialogResult();

        private string UrlBase = Properties.Settings.Default.URL;
        string json = string.Empty;

        public RepIBC()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }

        private async void RepIBC_Load(object sender, EventArgs e)
        {
            await CargarCombos();            
        }

        private async Task CargarCombos()
        {
            //Combo para seleccionar el tipo de reporte
            List<DataComboBox> lstCombo = new List<DataComboBox>();
            lstCombo.Add(new DataComboBox() { keyInt = 0, value = "Resumen de conceptos" }); //Ingresos por concepto
            //lstCombo.Add(new DataComboBox() { keyInt = 1, value = "Ingresos de caja" });
            //lstCombo.Add(new DataComboBox() { keyInt = 2, value = "Concepto finanzas" });

            cmbTypeReporte.ValueMember = "keyInt";
            cmbTypeReporte.DisplayMember = "value";
            cmbTypeReporte.DataSource = lstCombo;
            cmbTypeReporte.SelectedIndex = 0;

            //Combo de llenada de periodo de busqueda.
            List<DataComboBox> lstComboPeriodo = new List<DataComboBox>();
            lstComboPeriodo.Add(new DataComboBox() { keyInt = 0, value = "Selección libre" });
            lstComboPeriodo.Add(new DataComboBox() { keyInt = 1, value = "Hoy" });
            lstComboPeriodo.Add(new DataComboBox() { keyInt = 2, value = "Semana actual" });
            lstComboPeriodo.Add(new DataComboBox() { keyInt = 3, value = "Mes actual" });
            lstComboPeriodo.Add(new DataComboBox() { keyInt = 4, value = "Último bimestre" });

            cmbPeriodoBusqueda.ValueMember = "keyInt";
            cmbPeriodoBusqueda.DisplayMember = "value";
            cmbPeriodoBusqueda.DataSource = lstComboPeriodo;
            cmbPeriodoBusqueda.SelectedIndex = 0;

            ////Combo de Cajeros.
            //var resultTypeTransaction = await Requests.SendURIAsync("/api/UserRolesManager/GetUserByRoleName/User", HttpMethod.Get, Variables.LoginModel.Token);
            //if (resultTypeTransaction.Contains("error"))
            //{
            //    mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
            //    result = mensaje.ShowDialog();
            //}
            //else
            //{
            //    var lstCajeros = JsonConvert.DeserializeObject<List<SOAPAP.Model.ApplicationUser>>(resultTypeTransaction);
            //    List<DataComboBox> lstCaj = new List<DataComboBox>();
            //    foreach (var item in lstCajeros)
            //    {
            //        lstCaj.Add(new DataComboBox() { keyString = string.Format("{0} {1} {2}", item.Name, item.LastName, item.SecondLastName), value = string.Format("{0} {1} {2}", item.Name, item.LastName, item.SecondLastName) });
            //    }

            //    cmbCajero.ValueMember = "keyString";
            //    cmbCajero.DisplayMember = "value";

            //    cmbCajero.DataSource = lstCaj;
            //    cmbCajero.SelectedIndex = 0;
            //}

        }

        private void cmbPeriodoBusqueda_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataComboBox item = (DataComboBox)((ComboBox)sender).SelectedItem;
            if (item.keyInt == 0)//Libre
            {
                dtpFechaIni.Enabled = true;
                dtpFechaFin.Enabled = true;
            }
            else
            {
                dtpFechaIni.Value = DateTime.Today;
                dtpFechaIni.Enabled = false;
                dtpFechaFin.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
                dtpFechaFin.Enabled = false;
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
                string NombreFile = "ResumenConceptos_" + Variables.LoginModel.FullName.Replace(" ", "") + "_" + DateTime.Now.ToString("yyyy-MM-dd");
                pgcIBC.ExportToXlsx(fbd.SelectedPath + "\\" + NombreFile + ".xlsx", pivotExportOptions);
                MessageBox.Show("Archivo " + NombreFile + ".xlsx" + " guardado.");
            }
        }

        #region Procesos
        public async Task cargar()
        {
            DateTime FechaIni = dtpFechaIni.Value;
            DateTime FechaFin = dtpFechaFin.Value;
            DataComboBox periodo = (DataComboBox)cmbPeriodoBusqueda.SelectedItem;
            if (periodo.keyInt >= 2)
                FechaIni = obtenerPeriodo(FechaFin, periodo.keyInt);

            var id = Variables.LoginModel.User;
            DataReportes dRep = new DataReportes() { FechaIni = FechaIni.ToString("yyyy-MM-dd"), FechaFin = FechaFin.ToString("yyyy-MM-dd"), CajeroId = id };

            DataComboBox item = (DataComboBox)cmbTypeReporte.SelectedItem;

            HttpContent content;
            json = JsonConvert.SerializeObject(dRep);
            content = new StringContent(json, Encoding.UTF8, "application/json");

            var _resulTransaction = await Requests.SendURIAsync("/api/Reports/IncomeByConcept", HttpMethod.Post, Variables.LoginModel.Token, content);

            if (_resulTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var lstData = JsonConvert.DeserializeObject<List<DataIncomeByConcept>>(_resulTransaction);

                if (lstData == null)
                {
                    mensaje = new MessageBoxForm("Sin Operaciones", "No se encontraron movimientos.", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    var cuentas = lstData.GroupBy(x => x.id_payment).Select(y => new { id_payment = y.Key, lst = y.ToList() }).ToList();

                    List<IncomeByConceptVM> lstIBC = new List<IncomeByConceptVM>();                    
                    foreach (var elem in cuentas)
                    {
                        string FOLIO = elem.lst.First().folio_impresion;
                        string CUENTA = elem.lst.First().CUENTA;
                        string NOMBRE = elem.lst.First().cliente;
                        string RUTA = elem.lst.First().RUTA;
                        decimal AGUA = elem.lst.Where(x => !x.description.Contains("RECARGO") && x.description.Contains("AGUA") && x.tipo_movimiento == "TIP01").Sum(y => y.importe);
                        decimal DRENAJE = elem.lst.Where(x => !x.description.Contains("RECARGO") && x.description.Contains("DRENAJE") && x.tipo_movimiento == "TIP01").Sum(y => y.importe);
                        decimal SAN = elem.lst.Where(x => !x.description.Contains("RECARGO") && x.description.Contains("SANEAMIENTO") && x.tipo_movimiento == "TIP01").Sum(y => y.importe);
                        decimal REC = elem.lst.Where(x => x.description.Contains("RECARGO") && x.tipo_movimiento == "TIP01").Sum(y => y.importe);
                        decimal NOTIF = elem.lst.Where(x => x.description.Contains("Notificacion") && x.tipo_movimiento == "TIP03").Sum(y => y.importe);
                        decimal IVA = elem.lst.Sum(y => y.iva);
                        decimal OTROS = elem.lst.Where(x => x.tipo_movimiento == "S/T" || x.tipo_movimiento == "TIP02").Sum(y => y.importe);
                        decimal DESCUENTO = 0;
                        decimal TOTAL = AGUA + DRENAJE + SAN + REC + NOTIF + IVA + OTROS + DESCUENTO;
                        string ESTA = "ACT";
                        string MP = elem.lst.First().metodo_pago;
                        IncomeByConceptVM ibcTemp = new IncomeByConceptVM()
                        {
                            FOLIO = FOLIO,
                            CUENTA = CUENTA,
                            NOMBRE = NOMBRE,
                            RUTA = RUTA,
                            AGUA = AGUA,
                            DRENAJE = DRENAJE,
                            SAN = SAN,
                            REC = REC,
                            NOTIF = NOTIF,
                            IVA = IVA,
                            OTROS = OTROS,
                            DCTO = DESCUENTO,
                            TOTAL = TOTAL,
                            ESTA = ESTA,
                            MP = MP
                        };
                        lstIBC.Add(ibcTemp);
                    }

                    try
                    {
                        pgcIBC.DataSource = lstIBC;

                        //this.rvwReportes.LocalReport.ReportEmbeddedResource = "SOAPAP.Reportes.IncomeByConcept2Report.rdlc";
                        //this.rvwReportes.LocalReport.DataSources.Clear();

                        //ReportDataSource rds1 = new ReportDataSource("IBC", lstIBC);
                        //this.rvwReportes.LocalReport.DataSources.Add(rds1);

                        //this.rvwReportes.RefreshReport();
                    }
                    catch (Exception e)
                    {
                        var res = e.Message;
                    }
                }
            }
        }

        //Retorna una fecha origen a partir de la fechaBase, segun el periodo solicitado.
        public DateTime obtenerPeriodo(DateTime FechaBase, int opcion)
        {
            DateTime FechaIni = FechaBase;
            DateTime FechaFin = DateTime.Today;
            switch (opcion)
            {
                //Semana Actual
                case 2:
                    int val = (int)FechaIni.DayOfWeek == 0 ? -7 : (int)FechaIni.DayOfWeek * -1;
                    FechaFin = FechaIni.AddDays(val);
                    break;
                //Mes Actual
                case 3:
                    FechaFin = new DateTime(FechaIni.Year, FechaIni.Month, 01);
                    break;
                //Ultimo bimestre
                case 4:
                    DateTime PrimerDiaMes = new DateTime(FechaIni.Year, FechaIni.Month, 01);
                    FechaFin = PrimerDiaMes.AddMonths(-1);
                    break;
            }
            return FechaFin;
        }




        #endregion

       
    }
}

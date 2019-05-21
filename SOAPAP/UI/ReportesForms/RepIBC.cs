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

            //Combo de Cajeros.
            List<SOAPAP.Model.Users> lstCajeros = new List<Model.Users>();
            List<DataComboBox> lstCaj = new List<DataComboBox>();

            //Combo de Oficinas.
            List<SOAPAP.Model.BranchOffice> lstOficinas = new List<Model.BranchOffice>();
            List<DataComboBox> lstOfi = new List<DataComboBox>();

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

                //Solicitud de Oficinas
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
                //Operador actual
                lstCaj.Add(new DataComboBox() { keyString = Variables.LoginModel.User, value = Variables.LoginModel.FullName });
                //Oficina Acutal
                lstOfi.Add(new DataComboBox() { keyString = Variables.Configuration.Terminal.BranchOffice.Name, value = Variables.Configuration.Terminal.BranchOffice.Name });
            }
            //combo de Cajeros
            chcbxOperador.DataBindings.Clear();
            chcbxOperador.Properties.DataSource = null;
            chcbxOperador.Properties.ValueMember = "keyString";
            chcbxOperador.Properties.DisplayMember = "value";
            chcbxOperador.Properties.DataSource = lstCaj;
            //Combo de oficinas
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
                Process.Start(fbd.SelectedPath + "\\" + NombreFile + ".xlsx");
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

            ////Se obtiene el cajero para filtrar la consulta            
            var temp = chcbxOperador.Properties.Items.ToList();
            string itemSeleccionado = "";
            ////Se la oficina para la consulta.            
            var tempOfi = chcbxOficina.Properties.Items.ToList();
            string OfiSeleccionado = "";

            if (Variables.LoginModel.RolName[0] == "Supervisor")
            {
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
                if (tempOfi.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                {
                    OfiSeleccionado = "";
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar una oficina.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }                
                else
                {
                    foreach (var item in tempOfi)
                    {
                        if (item.CheckState == CheckState.Checked)
                            OfiSeleccionado = OfiSeleccionado + item.Value + ",";
                    }
                    OfiSeleccionado = OfiSeleccionado.Substring(0, OfiSeleccionado.Length - 1);
                }
            }
            else
            {                
                //Operador actual
                if (temp.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                {
                    itemSeleccionado = "";
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar un cajero.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    itemSeleccionado = temp.First().Value.ToString();                    
                }
                //Oficina actual
                if (tempOfi.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                {
                    OfiSeleccionado = "";
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar una oficina.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    OfiSeleccionado = tempOfi.First().Value.ToString();
                }
            }
            
            DataReportes dRep = new DataReportes() { FechaIni = FechaIni.ToString("yyyy-MM-dd"), FechaFin = FechaFin.ToString("yyyy-MM-dd"), CajeroId = itemSeleccionado, Oficinas = OfiSeleccionado };
                       
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
                    //////Filtro por oficinas.
                    ////List<DataIncomeByConcept> lstDataTmp;
                    ////if (OfiSeleccionado == "Todos")
                    ////    lstDataTmp = lstData;
                    ////else
                    ////    lstDataTmp = lstData.Where(x => OfiSeleccionado.Split(',').Contains(x.branch_office)).ToList();
              
                    //Generacion de lista que se manda a reporte.
                    var cuentas = lstData.GroupBy(x => x.id_payment).Select(y => new { id_payment = y.Key, lst = y.ToList() }).ToList();

                    List<IncomeByConceptVM> lstIBC = new List<IncomeByConceptVM>();                    
                    foreach (var elem in cuentas)
                    {
                        string FOLIO = elem.lst.First().folio_impresion;
                        string CUENTA = elem.lst.First().CUENTA;
                        string NOMBRE = elem.lst.First().cliente;
                        string RUTA = elem.lst.First().RUTA;
                        string MP = elem.lst.First().metodo_pago;
                        string OFICINA = elem.lst.First().branch_office;
                        string FECHA = elem.lst.First().fecha_pago;
                        string CAJERO = elem.lst.First().cajero;

                        decimal IVA = elem.lst.Sum(y => y.iva);
                        decimal DESCUENTO = elem.lst.Sum(y => y.Descuento);

                        decimal AGUA = elem.lst.Where(x => !x.description.Contains("RECARGO") && x.description.Contains("AGUA") && x.tipo_movimiento == "TIP01").Sum(y => y.importe);
                        foreach (var SubElem in elem.lst.Where(x => !x.description.Contains("RECARGO") && x.description.Contains("AGUA") && x.tipo_movimiento == "TIP01").ToList())
                            elem.lst.Remove(SubElem);

                        decimal DRENAJE = elem.lst.Where(x => !x.description.Contains("RECARGO") && x.description.Contains("DRENAJE") && x.tipo_movimiento == "TIP01").Sum(y => y.importe);
                        foreach (var SubElem in elem.lst.Where(x => !x.description.Contains("RECARGO") && x.description.Contains("DRENAJE") && x.tipo_movimiento == "TIP01").ToList())
                            elem.lst.Remove(SubElem);

                        decimal SAN = elem.lst.Where(x => !x.description.Contains("RECARGO") && x.description.Contains("SANEAMIENTO") && x.tipo_movimiento == "TIP01").Sum(y => y.importe);
                        foreach (var SubElem in elem.lst.Where(x => !x.description.Contains("RECARGO") && x.description.Contains("SANEAMIENTO") && x.tipo_movimiento == "TIP01").ToList())
                            elem.lst.Remove(SubElem);

                        decimal REC = elem.lst.Where(x => x.description.Contains("RECARGO") && x.tipo_movimiento == "TIP01").Sum(y => y.importe);
                        foreach (var SubElem in elem.lst.Where(x => x.description.Contains("RECARGO") && x.tipo_movimiento == "TIP01").ToList())
                            elem.lst.Remove(SubElem);

                        decimal NOTIF = elem.lst.Where(x => x.description.Contains("Notifica") && x.tipo_movimiento == "TIP03").Sum(y => y.importe);
                        foreach (var SubElem in elem.lst.Where(x => x.description.Contains("Notifica") && x.tipo_movimiento == "TIP03").ToList())
                            elem.lst.Remove(SubElem);

                        decimal ANTI = elem.lst.Where(x => x.tipo_movimiento == "TIP05").Sum(y => y.importe);
                        foreach (var SubElem in elem.lst.Where(x => x.tipo_movimiento == "TIP05").ToList())
                            elem.lst.Remove(SubElem);

                        //decimal OTROS = elem.lst.Where(x => x.tipo_movimiento == "S/T" || x.tipo_movimiento == "TIP02").Sum(y => y.importe);
                        decimal OTROS = elem.lst.Sum(y => y.importe);

                        decimal TOTAL = AGUA + DRENAJE + SAN + REC + NOTIF + IVA + ANTI + OTROS; // + DESCUENTO;
                        string ESTA = "ACT";
                        
                        IncomeByConceptVM ibcTemp = new IncomeByConceptVM()
                        {
                            FOLIO = FOLIO,
                            CUENTA = CUENTA,
                            NOMBRE = NOMBRE,
                            RUTA = RUTA,
                            OFICINA = OFICINA,
                            CAJERO = CAJERO,
                            FECHA = FECHA,
                            AGUA = AGUA,
                            DRENAJE = DRENAJE,
                            SAN = SAN,
                            REC = REC,
                            NOTIF = NOTIF,
                            IVA = IVA,
                            OTROS = OTROS,
                            DCTO = DESCUENTO,
                            ANTI = ANTI,
                            TOTAL = TOTAL,
                            ESTA = ESTA,
                            MP = MP
                        };
                        lstIBC.Add(ibcTemp);
                    }

                    try
                    {
                        pgcIBC.DataSource = lstIBC;                       
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

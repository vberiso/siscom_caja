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

namespace SOAPAP.UI.HistorialTransacciones
{
    public partial class Historial : Form
    {
        Form loading;
        private RequestsAPI Requests = null;
        Form mensaje;
        DialogResult result = new DialogResult();

        private string UrlBase = Properties.Settings.Default.URL;
        string json = string.Empty;

        List<SOAPAP.Model.Users> lstCajeros = new List<Model.Users>();

        public Historial()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }

        private async void Historial_Load(object sender, EventArgs e)
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
            }
            else
            {
                //Operador actual
                lstCaj.Add(new DataComboBox() { keyString = Variables.LoginModel.User, value = Variables.LoginModel.FullName });                
            }
            //combo de Cajeros
            chcbxOperador.DataBindings.Clear();
            chcbxOperador.Properties.DataSource = null;
            chcbxOperador.Properties.ValueMember = "keyString";
            chcbxOperador.Properties.DisplayMember = "value";
            chcbxOperador.Properties.DataSource = lstCaj;
            chcbxOperador.CheckAll();
        }

        private async void btnCargar_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            await cargar();
            loading.Close();
        }

        #region Procesos
        public async Task cargar()
        {
            DateTime FechaIni = dtpFecha.Value;
            
            ////Se obtiene el cajero para filtrar la consulta            
            var temp = chcbxOperador.Properties.Items.ToList();
            string idOperadorSelecciionado = "";
            
            if (Variables.LoginModel.RolName[0] == "Supervisor")
            {
                //Cajero(s) seleccionado(s)
                if (temp.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                {
                    idOperadorSelecciionado = "";
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar un cajero.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    foreach (var item in temp)
                    {
                        if (item.CheckState == CheckState.Checked)
                            idOperadorSelecciionado = idOperadorSelecciionado + item.Value + ",";
                    }
                    idOperadorSelecciionado = idOperadorSelecciionado.Substring(0, idOperadorSelecciionado.Length - 1);
                }
            }
            else
            {
                //Operador actual
                if (temp.Where(x => x.CheckState == CheckState.Checked).Count() == 0)
                {
                    idOperadorSelecciionado = "";
                    mensaje = new MessageBoxForm("Advertencia: ", "Debe seleccionar un cajero.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    idOperadorSelecciionado = temp.First().Value.ToString();
                }                
            }
                        
            HttpContent content;
            json = JsonConvert.SerializeObject(idOperadorSelecciionado);
            content = new StringContent(json, Encoding.UTF8, "application/json");

            var _resulTransaction = await Requests.SendURIAsync("/api/Transaction/ResumeTransactions/" + FechaIni.ToString("yyyy-MM-dd"), HttpMethod.Post, Variables.LoginModel.Token, content);

            if (_resulTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var lstData = JsonConvert.DeserializeObject<List<Model.Transaction>>(_resulTransaction);

                if (lstData == null)
                {
                    mensaje = new MessageBoxForm("Sin Operaciones", "No se encontraron movimientos.", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
                else
                {                    
                    List<DataHistorial> lstDH = new List<DataHistorial>();
                    foreach (var elem in lstData)
                    {
                        int Signo = elem.Sign ? 1 : -1;

                        foreach(var subElem in elem.TransactionDetails)
                        {
                            DataHistorial ibcTemp = new DataHistorial()
                            {
                                IdTransaction = elem.Id,
                                FechaTransaction = elem.DateTransaction.ToString("yyyy/MM/dd"),
                                TypeTransactionId = elem.TypeTransactionId,
                                TypeTransactionName = elem.TypeTransaction.Name,
                                Sign = elem.Sign ? 1 : 0,
                                Amount = elem.Amount,
                                Tax = elem.Tax,
                                rounding = elem.Rounding,
                                Total = elem.Total * Signo,
                                MetodoPago = elem.PayMethod.Name,
                                Cajero = lstCajeros.Where(x => x.id == elem.TerminalUser.UserId).Select(y => string.Format("{0} {1} {2}", y.name, y.lastName, y.secondLastName)).FirstOrDefault(),
                                Detalle = subElem.Description,
                                Subtotal = subElem.Amount * Signo
                            };
                            lstDH.Add(ibcTemp);
                        }
                    }

                    try
                    {
                        pgcHistorial.DataSource = lstDH;
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

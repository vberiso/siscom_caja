using Newtonsoft.Json;
using SOAPAP.Enums;
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

namespace SOAPAP.UI
{
    public partial class Cajas : Form
    {
        public Cajas()
        {
            InitializeComponent();
            Requests = new RequestsAPI(UrlBase);
        }

        Form loading;
        Form mensaje;
        private RequestsAPI Requests = null;
        DialogResult result = new DialogResult();
        private string UrlBase = Properties.Settings.Default.URL;
        HttpContent content;

        private async void cargaCombos()
        {
            
            var resultBranchOffices = await Requests.SendURIAsync("/api/BranchOffice/", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultBranchOffices.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultBranchOffices.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {

                var BranchOffices  =JsonConvert.DeserializeObject<List<SOAPAP.Model.BranchOffice>>(resultBranchOffices);
                BranchOffices.Add(new SOAPAP.Model.BranchOffice()
                {
                    Id = 0,
                    Name = "TODOS",
                    IsActive = true
                });

                SOAPAP.Model.TypeTransaction typeTransaction = new SOAPAP.Model.TypeTransaction();

                BranchOffices = BranchOffices.Where(x => x.IsActive == true).OrderBy(x => x.Id).ToList();
                
                cmbBranchOffice.ValueMember = "id";
                cmbBranchOffice.DisplayMember = "name";

                cmbBranchOffice.DataSource = BranchOffices;
            }
            
        }


        public async void cargar()
        {
            loading = new Loading();
            loading.Show(this);
            int combo = 0;
            if(cmbBranchOffice.SelectedValue == null)
            {
                combo = 0;
            }
            else
            {
                combo = Convert.ToInt32(cmbBranchOffice.SelectedValue);
            }
            var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/TerminalUser/{0}/{1}",dateTimePicker1.Value.ToString("yyyy-MM-dd"), combo), HttpMethod.Get, Variables.LoginModel.Token);
            
            if (_resulTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var source = new BindingSource();
                List<SOAPAP.Model.TerminalUser> transactions = JsonConvert.DeserializeObject<List<SOAPAP.Model.TerminalUser>>(_resulTransaction);

                if (transactions == null)
                {
                    mensaje = new MessageBoxForm("Sin Operaciones", "No se han encontrado movimientos para esta terminal", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }

                var transactionsFilter = transactions.Select(s => new 
                {
                    id = s.Id,
                    OpenDate = s.OpenDate,
                    InOperation = s.InOperation,
                    sucursals= s.Terminal.BranchOffice.Name,
                    userid= s.User.Name + " " + s.User.LastName + " " + s.User.SecondLastName                    
                }).ToList();
                
                source.DataSource = transactionsFilter;
                dgvMovimientos.DataSource = source;
                dgvMovimientos.Refresh();
                
            }
            loading.Close();
        }

        private void centraX(Control padre, Control hijo)
        {
            int x = 0;
            x = (padre.Width / 2) - (hijo.Width / 2);
            hijo.Location = new System.Drawing.Point(x, hijo.Location.Y);
        }


        private  void Cajas_Load(object sender, EventArgs e)
        {
            cargaCombos();
            centraX(pnlHeader, pbBG);
            centraX(pnlHeader, tableLayoutPanel1);
            cargar();
        }

        private async void dgvMovimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvMovimientos.Rows[e.RowIndex];
                
                if (e.ColumnIndex == dgvMovimientos.Columns["Cierre"].Index && e.RowIndex >= 0)
                {
                    loading = new Loading();
                    loading.Show(this);
                    if (Convert.ToBoolean(row.Cells["inoperation"].Value) == true)
                    { 
                    
                    SOAPAP.Model.Transaction transactionCierre = new SOAPAP.Model.Transaction();
                    transactionCierre.Sign = true;
                    transactionCierre.Amount = 0;
                    transactionCierre.Aplication = "SISCOMCAJA";
                    transactionCierre.TypeTransactionId = 5;
                    transactionCierre.PayMethodId = 1;
                    transactionCierre.TerminalUserId = Convert.ToInt32(row.Cells["TerminalId"].Value.ToString());

                    string valoresCierre = JsonConvert.SerializeObject(transactionCierre);
                    content = new StringContent(valoresCierre, Encoding.UTF8, "application/json");
                    var resultadoCierre = await Requests.SendURIAsync(string.Format("/api/Transaction/Super/{0}", row.Cells["TerminalId"].Value.ToString()), HttpMethod.Post, Variables.LoginModel.Token, content);
                    if (resultadoCierre.Contains("error"))
                    {
                        loading.Close();
                        mensaje = new MessageBoxForm("Error", resultadoCierre.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        loading.Close();
                        mensaje = new MessageBoxForm("Transacción Exitosa", "Caja cerrada con éxito.", TypeIcon.Icon.Success);
                        result = mensaje.ShowDialog();
                        cargar();
                    }
                  }
                    loading.Close();
                }

                else if (e.ColumnIndex == dgvMovimientos.Columns["Liquidacion"].Index && e.RowIndex >= 0)
                {
                    if (Convert.ToBoolean(row.Cells["inoperation"].Value) == true)
                    { 
                        mensaje = new MessageBoxForm("¿Está seguro de liquidar la caja?", "Esta acción bloqueará la caja y no podrá hacer movimientos", TypeIcon.Icon.Warning, true);
                    result = mensaje.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        loading = new Loading();
                        loading.Show(this);
                        var resultTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}/{1}", Convert.ToDateTime(row.Cells["openDate"].Value.ToString()).ToString("yyyy-MM-dd"), row.Cells["TerminalId"].Value.ToString()), HttpMethod.Get, Variables.LoginModel.Token);
                        loading.Close();
                        if (resultTransaction.Contains("error"))
                        {
                            mensaje = new MessageBoxForm("Error", resultTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                        }
                        else
                        {
                            loading = new Loading();
                            loading.Show(this);

                            KeyValuePair<int, decimal> _fondoCaja = new KeyValuePair<int, decimal>(0, 0);
                            KeyValuePair<int, decimal> _retirado = new KeyValuePair<int, decimal>(0, 0);
                            KeyValuePair<int, decimal> _cobrado = new KeyValuePair<int, decimal>(0, 0);
                            KeyValuePair<int, decimal> _cancelado = new KeyValuePair<int, decimal>(0, 0);
                            decimal _saldo = 0;

                            List<SOAPAP.Model.Transaction> transactions = JsonConvert.DeserializeObject<List<SOAPAP.Model.Transaction>>(resultTransaction);

                            transactions.ForEach(x =>
                            {

                                switch (x.TypeTransactionId)
                                {
                                    case 1://apertura                                          
                                        break;
                                    case 2://Fondo                                          
                                        _fondoCaja = new KeyValuePair<int, decimal>(_fondoCaja.Key + 1, x.Total);
                                        break;
                                    case 3://Cobrado                       
                                        _cobrado = new KeyValuePair<int, decimal>(_cobrado.Key + 1, _cobrado.Value + x.Total);
                                        break;
                                    case 4://Cancelado                        
                                        _cancelado = new KeyValuePair<int, decimal>(_cancelado.Key + 1, _cancelado.Value + x.Total);
                                        break;
                                    case 5://Cierre    
                                        break;
                                    case 6: //Retiro
                                        _retirado = new KeyValuePair<int, decimal>(_retirado.Key + 1, _retirado.Value + x.Total);
                                        break;
                                    case 7: //Liquidada                                          
                                        break;
                                }

                            });

                            _saldo = (_fondoCaja.Value + _cobrado.Value) - _cancelado.Value - _retirado.Value;
                            loading.Close();

                            mensaje = new MessageBoxForm(string.Format("La caja se liquidará: ${0}", _saldo), string.Format("COBROS: ({0}) -> ${1}.{2}    CANCELACIONES: ({3}) -> ${4}.{5}    RETIROS: ({6}) -> ${7}",
                                                                                                   _cobrado.Key,
                                                                                                   _cobrado.Value,
                                                                                                    Environment.NewLine,
                                                                                                   _cancelado.Key,
                                                                                                   _cancelado.Value,
                                                                                                    Environment.NewLine,
                                                                                                   _retirado.Key,
                                                                                                   _retirado.Value), TypeIcon.Icon.Warning, true);

                            result = mensaje.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                loading = new Loading();
                                loading.Show(this);

                                SOAPAP.Model.Transaction transactionLiquidacion = new SOAPAP.Model.Transaction();
                                transactionLiquidacion.Sign = false;
                                transactionLiquidacion.Amount = _saldo;
                                transactionLiquidacion.Aplication = "SISCOMCAJA";
                                transactionLiquidacion.TypeTransactionId = 7;
                                transactionLiquidacion.PayMethodId = 1;
                                transactionLiquidacion.TerminalUserId = Convert.ToInt32(row.Cells["TerminalId"].Value.ToString());

                                string valoresLiquidacion = JsonConvert.SerializeObject(transactionLiquidacion);
                                content = new StringContent(valoresLiquidacion, Encoding.UTF8, "application/json");
                                var resultadoLiquidacion = await Requests.SendURIAsync(string.Format("/api/Transaction/Super/{0}", row.Cells["TerminalId"].Value.ToString()), HttpMethod.Post, Variables.LoginModel.Token, content);
                                if (resultadoLiquidacion.Contains("error"))
                                {
                                    mensaje = new MessageBoxForm("Error", resultadoLiquidacion.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                                    result = mensaje.ShowDialog();
                                }
                                else
                                {
                                    Variables.Configuration.StateOperation = Convert.ToInt16(transactionLiquidacion.TypeTransactionId);
                                    mensaje = new MessageBoxForm("Transacción Exitosa", string.Format("Caja liquidada con éxito. Puede retirar el efectivo de fondo: ${0}", _fondoCaja.Value), TypeIcon.Icon.Success);
                                    result = mensaje.ShowDialog();
                                }

                                loading.Close();
                            }
                        }
                      }
                   }
                }

                else if (e.ColumnIndex == dgvMovimientos.Columns["Retiros"].Index && e.RowIndex >= 0)
                {
                    if (Convert.ToBoolean(row.Cells["inoperation"].Value) == true)
                    { 
                    mensaje = new ModalRetiroCaja(Convert.ToInt32(row.Cells["TerminalId"].Value.ToString()), Convert.ToDateTime(row.Cells["opendate"].Value.ToString()).ToString("yyyy-MM-dd"), "/api/Transaction/Super/{0}", 2);
                    result = mensaje.ShowDialog(this);
                    }
                }
                }
        }

        private void dgvMovimientos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvMovimientos.Columns[e.ColumnIndex].Name == "Retiros" && e.RowIndex >= 0)
            {
                    var image = Properties.Resources.retiros;
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                    e.Graphics.DrawImage(image, x, y, 15, 15);
                    e.Handled = true;  
            }
            if (e.ColumnIndex >= 0 && this.dgvMovimientos.Columns[e.ColumnIndex].Name == "Liquidacion" && e.RowIndex >= 0)
            {
                var image = Properties.Resources.liquidar;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                e.Graphics.DrawImage(image, x, y, 15, 15);
                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvMovimientos.Columns[e.ColumnIndex].Name == "Cierre" && e.RowIndex >= 0)
            {
                var image = Properties.Resources.cerrar_gris;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                e.Graphics.DrawImage(image, x, y, 15, 15);
                e.Handled = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
         
            cargar();
        }

        private void cmbBranchOffice_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cargar();
        }
    }
}
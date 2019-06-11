using Newtonsoft.Json;
using SOAPAP.Services;
using SOAPAP.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOAPAP.Model;
using SOAPAP.Enums;
using System.Threading;
using System.Net;
using Firebase.Database;
using SOAPAP.Properties;
using SOAPAP.UI.Descuentos;
using Firebase.Database.Query;

namespace SOAPAP
{
    public partial class Base : Form, IForm
    {
        Button btnSeleccionado = new Button();
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;
        HttpContent content;
        string macadrres = string.Empty;
        int _typeTransaction = 0;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        public readonly FirebaseClient firebase = new FirebaseClient(Variables.Configuration.StringURLFirebase);
        private List<string> keystemp = new List<string>();

        #region IForm Members
        public void ShowForm(string nameSpace, string nameForm)
        {
            Type t = Type.GetType(nameSpace + "." + nameForm);
            Form newForm = Activator.CreateInstance(t) as Form;
            newForm.Owner = this;
            AddFormInPanel(newForm);
        }
        #endregion

        public Base()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();

            var observable = firebase
                             .Child("DiscountAuthorization")
                             .AsObservable<PushNotification>()
                             .Subscribe(d =>
                             {
                                 if(d.Object != null)
                                     if (d.Object.UserRequestId == Variables.LoginModel.User)
                                     {
                                         if(d.Object.IsReply == true && !d.Object.IsView)
                                         {
                                             if (!Variables.keys.Contains(d.Key))
                                             {
                                                 notificacionesToolStripMenuItem.ImageAlign = ContentAlignment.MiddleRight;
                                                 notificacionesToolStripMenuItem.Image = Resources.notificacion;

                                                 ToolStripItem newDropDownItem = new ToolStripMenuItem();
                                                 newDropDownItem.ToolTipText = d.Key;
                                                 newDropDownItem.Text = $"El descuento ha sido autorizado {Environment.NewLine} para la cuenta: {d.Object.Account} ";
                                                 newDropDownItem.Image = Resources.comprobado;
                                                 newDropDownItem.ImageAlign = ContentAlignment.MiddleCenter;
                                                 newDropDownItem.Click += (object sender, EventArgs e) =>
                                                 {
                                                     Showdiscount(d.Object.AuthorizationDiscountId, d.Key);
                                                 };
                                                 try
                                                 {
                                                     Invoke(new MethodInvoker(() => {
                                                             notificacionesToolStripMenuItem.Enabled = true;
                                                             notificacionesToolStripMenuItem.DropDownItems.Add(newDropDownItem);
                                                     }));
                                                 }
                                                 catch (Exception)
                                                 {
                                                     if (!this.IsDisposed)
                                                     {
                                                         Invoke(new MethodInvoker(() => {
                                                             if (!this.IsDisposed)
                                                             {
                                                                 notificacionesToolStripMenuItem.Enabled = true;
                                                                 notificacionesToolStripMenuItem.DropDownItems.Add(newDropDownItem);
                                                             }
                                                         }));
                                                     }
                                                 }
                                               
                                                 Variables.keys.Add(d.Key);
                                                 keystemp.Add(d.Key);
                                             }
                                         }
                                         else
                                         {
                                             if (Variables.keys.Contains(d.Key))
                                             {
                                                 if (d.Object.IsReply == false)
                                                 {
                                                     foreach (ToolStripItem item in notificacionesToolStripMenuItem.DropDown.Items)
                                                     {
                                                         if (item.Text.Contains(d.Object.Account))
                                                         {
                                                             //notificacionesToolStripMenuItem.DropDown.Items.Remove(item);
                                                             try
                                                             {
                                                                 Invoke(new MethodInvoker(() => {
                                                                     item.Text = $"Se ha cancelado el descuento para la cuenta: {d.Object.Account}";
                                                                     item.Image = Resources.cancel;
                                                                     item.ImageAlign = ContentAlignment.MiddleCenter;
                                                                 }));
                                                             }
                                                             catch (Exception)
                                                             {

                                                                 if (!this.IsDisposed)
                                                                 {
                                                                     Invoke(new MethodInvoker(() => {
                                                                         if (!this.IsDisposed)
                                                                         {
                                                                             item.Text = $"Se ha cancelado el descuento para la cuenta: {d.Object.Account}";
                                                                             item.Image = Resources.cancel;
                                                                             item.ImageAlign = ContentAlignment.MiddleCenter;
                                                                         }
                                                                     }));
                                                                 }
                                                             }
                                                         }
                                                     }
                                                 }
                                                 else
                                                 {
                                                     foreach (ToolStripItem item in notificacionesToolStripMenuItem.DropDown.Items)
                                                     {
                                                         if (item.Text.Contains(d.Object.Account))
                                                         {
                                                             //notificacionesToolStripMenuItem.DropDown.Items.Remove(item);
                                                             try
                                                             {
                                                                 Invoke(new MethodInvoker(() => {
                                                                     item.Text = $"El descuento ha sido autorizado {Environment.NewLine} para la cuenta: {d.Object.Account} ";
                                                                     item.Image = Resources.comprobado;
                                                                     item.ImageAlign = ContentAlignment.MiddleCenter;
                                                                 }));
                                                             }
                                                             catch (Exception)
                                                             {

                                                                 if (!this.IsDisposed)
                                                                 {
                                                                     Invoke(new MethodInvoker(() => {
                                                                         if (!this.IsDisposed)
                                                                         {
                                                                             item.Text = $"El descuento ha sido autorizado {Environment.NewLine} para la cuenta: {d.Object.Account} ";
                                                                             item.Image = Resources.comprobado;
                                                                             item.ImageAlign = ContentAlignment.MiddleCenter;
                                                                         }
                                                                     }));
                                                                 }
                                                             }
                                                         }
                                                     }
                                                 }
                                             }
                                         }
                                     }
                             });
        }


        public void RemoveItemSelected(string account)
        {
            for (int i = 0; i < notificacionesToolStripMenuItem.DropDown.Items.Count; i++)
            {
                ToolStripItem item = notificacionesToolStripMenuItem.DropDown.Items[i];
                if (item.Text.Contains(account))
                    notificacionesToolStripMenuItem.DropDown.Items.Remove(item);

                if(notificacionesToolStripMenuItem.DropDown.Items.Count == 0)
                {
                    notificacionesToolStripMenuItem.Image = null;
                    notificacionesToolStripMenuItem.Enabled = false;
                }
            }
        }

        private async void Showdiscount(int idDiscount, string key)
        {
            var notification = await firebase
                  .Child("DiscountAuthorization")
                  .Child(key)
                  .OnceSingleAsync<PushNotification>();

            notification.IsView = true;

            await firebase
                  .Child("DiscountAuthorization")
                  .Child(key)
                  .PutAsync(notification);

            DetalleDescuentos detalle = new DetalleDescuentos(idDiscount, true);
            detalle.ShowDialog(this);
        }

        private void Base_Load(object sender, EventArgs e)
        {            
            pictureBox4.Parent = pictureBox1;
            CargaInformacion();
            //notificacionesToolStripMenuItem.Enabled = false;
            notificacionesToolStripMenuItem.DropDownItems.Clear();
        }           
        private void AddFormInPanel(Form fh)
        {
            if (this.pnlContenido.Controls.Count > 0)
                this.pnlContenido.Controls.RemoveAt(0);
            fh.Visible = false;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            this.pnlContenido.Controls.Add(fh);
            this.pnlContenido.Tag = fh;
            fh.Show();
        }

        /// <summary>
        /// MENU
        /// </summary>
        private void salirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Variables.keys.Clear();
            Thread t = new Thread(new ThreadStart(ThreadProc));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            this.Close();
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// ATERPURA DE CAJA
        /// </summary>
        private async void btnApertura_Click(object sender, EventArgs e)
        {


            if (Variables.LoginModel.RolName.ToList().Find(x => x.Contains("Admin"))!=null)
                ShowForm("SOAPAP", "TerminalFront");

            if (Variables.LoginModel.RolName.ToList().Find(x => x.Contains("User"))!=null)
            {
                SOAPAP.Model.TerminalUser terminalUser;

                switch (_typeTransaction)
                {
                    case 0:

                        loading = new Loading();
                        loading.Show(this);
                        //En caso de no existir en TerminalUser
                        if (Variables.Configuration.Terminal.TerminalUsers.Count == 0)
                        {
                            terminalUser = new SOAPAP.Model.TerminalUser();
                            terminalUser.InOperation = true;
                            terminalUser.TerminalId = Variables.Configuration.Terminal.Id;
                            terminalUser.UserId = Variables.LoginModel.User;

                            string _terminalUser = JsonConvert.SerializeObject(terminalUser);
                            content = new StringContent(_terminalUser, Encoding.UTF8, "application/json");
                            var _resulTerminalUser = await Requests.SendURIAsync("/api/TerminalUser", HttpMethod.Post, Variables.LoginModel.Token, content);
                            if (_resulTerminalUser.Contains("error"))
                            {
                                mensaje = new MessageBoxForm("Error", _resulTerminalUser.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                                result = mensaje.ShowDialog();
                            }
                            else
                            {
                                Variables.Configuration.Terminal.TerminalUsers.Add(JsonConvert.DeserializeObject<SOAPAP.Model.TerminalUser>(_resulTerminalUser));

                                SOAPAP.Model.Transaction transactionAterura = new SOAPAP.Model.Transaction();
                                transactionAterura.Sign = true;
                                transactionAterura.Amount = 0;
                                transactionAterura.Aplication = "SISCOMCAJA";
                                transactionAterura.TypeTransactionId = 1;
                                transactionAterura.PayMethodId = 1;
                                transactionAterura.TerminalUserId = Variables.Configuration.Terminal.TerminalUsers.First().Id;

                                string valoresApertura = JsonConvert.SerializeObject(transactionAterura);
                                content = new StringContent(valoresApertura, Encoding.UTF8, "application/json");
                                var resultadoApertura = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", Variables.Configuration.Terminal.TerminalUsers.First().Id), HttpMethod.Post, Variables.LoginModel.Token, content);
                                if (resultadoApertura.Contains("error"))
                                {
                                    mensaje = new MessageBoxForm("Error", resultadoApertura.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                                    result = mensaje.ShowDialog();
                                }
                                else
                                {
                                    Variables.Configuration.StateOperation = Convert.ToInt16(transactionAterura.TypeTransactionId);
                                    CargaMenu(CashBoxAccess.Access.SinApertura);
                                    mensaje = new MessageBoxForm("Transacción Exitosa", "Caja aperturada con éxito", TypeIcon.Icon.Success);
                                    result = mensaje.ShowDialog();
                                }
                            }
                        }
                        else
                        {
                            SOAPAP.Model.Transaction transactionAterura = new SOAPAP.Model.Transaction();
                            transactionAterura.Sign = true;
                            transactionAterura.Amount = 0;
                            transactionAterura.Aplication = "SISCOMCAJA";
                            transactionAterura.TypeTransactionId = 1;
                            transactionAterura.PayMethodId = 1;
                            transactionAterura.TerminalUserId = Variables.Configuration.Terminal.TerminalUsers.First().Id;

                            string valoresApertura = JsonConvert.SerializeObject(transactionAterura);
                            content = new StringContent(valoresApertura, Encoding.UTF8, "application/json");
                            var resultadoApertura = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", Variables.Configuration.Terminal.TerminalUsers.First().Id), HttpMethod.Post, Variables.LoginModel.Token, content);
                            if (resultadoApertura.Contains("error"))
                            {
                                mensaje = new MessageBoxForm("Error", resultadoApertura.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                                result = mensaje.ShowDialog();
                            }
                            else
                            {
                                Variables.Configuration.StateOperation = Convert.ToInt16(transactionAterura.TypeTransactionId);
                                CargaMenu(CashBoxAccess.Access.SinApertura);
                                mensaje = new MessageBoxForm("Transacción Exitosa", "Caja aperturada con éxito", TypeIcon.Icon.Success);
                                result = mensaje.ShowDialog();
                            }
                        }
                        CargaInformacion();
                        loading.Close();
                        break;
                    case 1: //Aperturar                         
                        mensaje = new ModalFondoCaja(Variables.Configuration.Terminal.CashBox);
                        result = mensaje.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            Variables.Configuration.StateOperation = 2;
                            CargaInformacion();
                        }
                        break;
                    case 2: //Para liquidar  
                        mensaje = new MessageBoxForm("¿Está seguro de liquidar la caja?", "Esta acción bloqueará la caja y no podrá hacer movimientos", TypeIcon.Icon.Warning, true);
                        result = mensaje.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            loading = new Loading();
                            loading.Show(this);
                            var resultTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}/{1}", DateTime.Now.ToString("yyyy-MM-dd"), Variables.Configuration.Terminal.TerminalUsers.First().Id), HttpMethod.Get, Variables.LoginModel.Token);
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
                                    transactionLiquidacion.TerminalUserId = Variables.Configuration.Terminal.TerminalUsers.First().Id;

                                    string valoresLiquidacion = JsonConvert.SerializeObject(transactionLiquidacion);
                                    content = new StringContent(valoresLiquidacion, Encoding.UTF8, "application/json");
                                    var resultadoLiquidacion = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", Variables.Configuration.Terminal.TerminalUsers.First().Id), HttpMethod.Post, Variables.LoginModel.Token, content);
                                    if (resultadoLiquidacion.Contains("error"))
                                    {
                                        mensaje = new MessageBoxForm("Error", resultadoLiquidacion.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                                        result = mensaje.ShowDialog();
                                    }
                                    else
                                    {
                                        Variables.Configuration.StateOperation = Convert.ToInt16(transactionLiquidacion.TypeTransactionId);
                                        CargaMenu(CashBoxAccess.Access.Liquidada);
                                        mensaje = new MessageBoxForm("Transacción Exitosa", string.Format("Caja liquidada con éxito. Puede retirar el efectivo de fondo: ${0}", _fondoCaja.Value), TypeIcon.Icon.Success);
                                        result = mensaje.ShowDialog();
                                        CargaInformacion();
                                    }
                                    loading.Close();
                                }
                            }
                        }
                        break;
                    case 5://Cerrada  

                        loading = new Loading();
                        loading.Show(this);

                        SOAPAP.Model.Transaction transaction = new SOAPAP.Model.Transaction();
                        transaction.Sign = true;
                        transaction.Amount = 0;
                        transaction.Aplication = "SISCOMCAJA";
                        transaction.TypeTransactionId = 1;
                        transaction.PayMethodId = 1;
                        transaction.TerminalUserId = Variables.Configuration.Terminal.TerminalUsers.First().Id;

                        string valores = JsonConvert.SerializeObject(transaction);
                        content = new StringContent(valores, Encoding.UTF8, "application/json");
                        var resultado = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", Variables.Configuration.Terminal.TerminalUsers.First().Id), HttpMethod.Post, Variables.LoginModel.Token, content);
                        if (resultado.Contains("error"))
                        {
                            mensaje = new MessageBoxForm("Error", resultado.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                        }
                        else
                        {
                            Variables.Configuration.StateOperation = Convert.ToInt16(transaction.TypeTransactionId);
                            CargaMenu(CashBoxAccess.Access.SinApertura);
                            mensaje = new MessageBoxForm("Transacción Exitosa", "Caja aperturada con éxito", TypeIcon.Icon.Success);
                            result = mensaje.ShowDialog();
                        }
                        loading.Close();
                        break;
                    case 7://Liquidada   
                        mensaje = new MessageBoxForm("¿Está seguro de cerrar la caja?", "Verifique sus movimientos de caja con su tira auditora", TypeIcon.Icon.Warning, true);
                        result = mensaje.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            loading = new Loading();
                            loading.Show(this);

                            SOAPAP.Model.Transaction transactionCierre = new SOAPAP.Model.Transaction();
                            transactionCierre.Sign = true;
                            transactionCierre.Amount = 0;
                            transactionCierre.Aplication = "SISCOMCAJA";
                            transactionCierre.TypeTransactionId = 5;
                            transactionCierre.PayMethodId = 1;
                            transactionCierre.TerminalUserId = Variables.Configuration.Terminal.TerminalUsers.First().Id;

                            string valoresCierre = JsonConvert.SerializeObject(transactionCierre);
                            content = new StringContent(valoresCierre, Encoding.UTF8, "application/json");
                            var resultadoCierre = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", Variables.Configuration.Terminal.TerminalUsers.First().Id), HttpMethod.Post, Variables.LoginModel.Token, content);
                            if (resultadoCierre.Contains("error"))
                            {
                                mensaje = new MessageBoxForm("Error", resultadoCierre.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                                result = mensaje.ShowDialog();
                            }
                            else
                            {
                                Variables.Configuration.StateOperation = Convert.ToInt16(transactionCierre.TypeTransactionId);
                                CargaMenu(CashBoxAccess.Access.Liquidada);
                                mensaje = new MessageBoxForm("Transacción Exitosa", "Caja cerrada con éxito.", TypeIcon.Icon.Success);
                                result = mensaje.ShowDialog();
                                CargaInformacion();
                            }
                            loading.Close();
                        }
                        break;
                    default:
                        break;
                }


            }
            SelectOption(btnApertura);
        }                
        private void btnApertura_MouseLeave(object sender, EventArgs e)
        {
            if (btnSeleccionado!=btnApertura)
                btnApertura.BackColor = System.Drawing.Color.FromArgb(45, 50, 62);
        }

        /// <summary>
        /// COBRO
        /// </summary>
        private void btnCobro_Click(object sender, EventArgs e)
        {
            Variables.oprtions = false;
            ShowForm("SOAPAP.UI", "Cobro");
            SelectOption(btnCobro);
            //SelectOption(btnBuscar);
        }        
        private void btnCobro_MouseLeave(object sender, EventArgs e)
        {
            if (btnSeleccionado != btnCobro)
                btnCobro.BackColor = System.Drawing.Color.FromArgb(45, 50, 62);
        }

        /// <summary>
        /// MOVIMIENTOS DE CAJA
        /// </summary>
        private void btnMovimientos_Click(object sender, EventArgs e)
        {      
            ShowForm("SOAPAP", "Movimientos");
            SelectOption(btnMovimientos);
        }
        private void btnMovimientos_MouseLeave(object sender, EventArgs e)
        {
            if (btnSeleccionado != btnMovimientos)          
                btnMovimientos.BackColor = System.Drawing.Color.FromArgb(45, 50, 62);
        }

        /// <summary>
        /// BUSCAR
        /// </summary>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "cobro");
//            SelectOption(btnCobro);
            SelectOption(btnBuscar);
        }

        private void btnBuscar_MouseLeave(object sender, EventArgs e)
        {
            if (btnSeleccionado != btnBuscar)
                btnBuscar.BackColor = System.Drawing.Color.FromArgb(45, 50, 62);
        }

        /// <summary>
        /// REPORTES DE CAJA
        /// </summary>
        private void btnReportes_Click(object sender, EventArgs e)
        {

            ShowForm("SOAPAP", "UI.ReportesMenu");
            SelectOption(btnReportes);
        }        
        private void btnReportes_MouseLeave(object sender, EventArgs e)
        {
            if (btnSeleccionado != btnReportes)
                btnReportes.BackColor = System.Drawing.Color.FromArgb(45, 50, 62);
        }

        /// <summary>
        /// REPORTES DE CAJA
        /// </summary>
        private void btnHistorial_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.HistorialTransacciones.Historial");
            SelectOption(btnHistorial);
        }
        private void btnHistorial_MouseLeave(object sender, EventArgs e)
        {
            if (btnSeleccionado != btnHistorial)
                btnHistorial.BackColor = System.Drawing.Color.FromArgb(45, 50, 62);
        }


        /// <summary>
        /// VENTA DE PRODUCTOS
        /// </summary>   
        private void btnProductos_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP.UI", "Productos");
            SelectOption(btnProductos);
        }
        private void btnProductos_MouseLeave(object sender, EventArgs e)
        {
            if (btnSeleccionado != btnProductos)
                btnReportes.BackColor = System.Drawing.Color.FromArgb(45, 50, 62);
        }

        #region PrivateMethod
        /// <summary>
        /// Cambia el background dinamicamente segun la selección
        /// </summary>
        /// <param name="btnSelect"></param>
        private void SelectOption(Button btnSelect)
        {
            btnSeleccionado.BackColor = System.Drawing.Color.FromArgb(45, 50, 62);
            btnSelect.BackColor = System.Drawing.Color.FromArgb(3, 138, 255);
            btnSeleccionado = btnSelect;
        }

        private async void CargaMenu(CashBoxAccess.Access accessParam)
        {
            if (accessParam == CashBoxAccess.Access.Admin)
                btnApertura.Text = "Alta de Terminal";


            btnApertura.Visible = accessParam == CashBoxAccess.Access.SinAcceso ? false : true;
            btnCobro.Visible = accessParam == CashBoxAccess.Access.SinAcceso || accessParam == CashBoxAccess.Access.Admin ? false : true;
            btnBuscar.Visible = accessParam == CashBoxAccess.Access.SinAcceso || accessParam == CashBoxAccess.Access.Admin ? false : true;
            btnMovimientos.Visible = accessParam == CashBoxAccess.Access.SinAcceso || accessParam == CashBoxAccess.Access.Admin ? false : true;
            //btnReportes.Visible = accessParam == CashBoxAccess.Access.SinAcceso || accessParam == CashBoxAccess.Access.Admin ? false : true;
            btnReportes.Visible = false;
            btnProductos.Visible = accessParam == CashBoxAccess.Access.SinAcceso || accessParam == CashBoxAccess.Access.Admin ? false : true;
            //btnHistorial.Visible = accessParam == CashBoxAccess.Access.SinAcceso || accessParam == CashBoxAccess.Access.Admin ? false : true;
            reportesToolStripMenuItem.Visible = accessParam == CashBoxAccess.Access.SinAcceso || accessParam == CashBoxAccess.Access.Admin ? false : true;

            btnApertura.Enabled = accessParam == CashBoxAccess.Access.SinCierreAnterior ? false : true;
            btnCobro.Enabled = accessParam != CashBoxAccess.Access.Cobro ? false : true;
            btnBuscar.Enabled = accessParam != CashBoxAccess.Access.Cobro ? false : true;
            btnMovimientos.Enabled = accessParam != CashBoxAccess.Access.Cobro ? false : true;
            //btnReportes.Enabled = accessParam != CashBoxAccess.Access.Cobro ? false : true;
            btnProductos.Enabled = accessParam != CashBoxAccess.Access.Cobro ? false : true;
            //btnHistorial.Enabled = accessParam != CashBoxAccess.Access.Cobro ? false : true;
            reportesToolStripMenuItem.Enabled = accessParam != CashBoxAccess.Access.Cobro ? false : true;

            btnApertura.Image = accessParam == CashBoxAccess.Access.Cobro ? Properties.Resources.abrir_caja : Properties.Resources.cerrar_caja;
        }

        private void CargaInformacion()
        {
            usuarioToolStripMenuItem.Text = Variables.LoginModel.FullName;
            tslUsuario.Text = "Usuario: " + Variables.LoginModel.User;
            try
            {
                pbxLogoEmpresa.Image = Bitmap.FromStream(WebRequest.Create(Variables.Configuration.Image).GetResponse().GetResponseStream());
            }
            catch (Exception)
            {

                
            }
            

            if (Variables.LoginModel.RolName.ToList().Find(x => x.Contains("User")) != null)
            {
                if (Variables.Configuration.Terminal != null)
                {
                    if (Variables.Configuration.Terminal.TerminalUsers.Count > 0)
                    {
                        lblTerminal.Text = Variables.Configuration.Terminal.TerminalUsers.First().Id.ToString();

                        if (Variables.Configuration.Terminal.TerminalUsers.First().UserId != Variables.LoginModel.User)
                        {
                            CargaMenu(CashBoxAccess.Access.SinAcceso);
                            mensaje = new MessageBoxForm("Error", "Otro usuario está usando esta termial.", TypeIcon.Icon.Warning);
                            result = mensaje.ShowDialog();
                        }
                        else
                        {
                            if (Variables.Configuration.Terminal.TerminalUsers.First().InOperation && Variables.Configuration.Terminal.TerminalUsers.First().OpenDate.Date != System.DateTime.Now.Date)
                            {
                                lblEstadoCaja.Text = "Sin Cierre";
                                CargaMenu(CashBoxAccess.Access.SinCierreAnterior);
                                mensaje = new MessageBoxForm("Error", string.Format("La caja no cerró correctamente el día: {0}. Acudir con  supervisor.", Variables.Configuration.Terminal.TerminalUsers.First().OpenDate.ToShortDateString()), TypeIcon.Icon.Warning);
                                result = mensaje.ShowDialog();
                            }
                            else
                            {
                                if (Variables.LoginModel.RolName.ToList().Find(x => x.Contains("User")) != null)
                                {
                                    _typeTransaction = Variables.Configuration.StateOperation;

                                    switch (_typeTransaction)
                                    {
                                        case 0:
                                            btnApertura.Text = "   Aperturar Caja";
                                            lblEstadoCaja.Text = "Sin aperturar";
                                            CargaMenu(CashBoxAccess.Access.SinApertura);
                                            break;
                                        case 1:
                                            btnApertura.Text = "   Ingresar Fondo";
                                            lblEstadoCaja.Text = "Abierta";
                                            CargaMenu(CashBoxAccess.Access.Abierta);
                                            break;
                                        case 2:
                                            btnApertura.Text = "   Liquidar Caja";
                                            lblEstadoCaja.Text = "Operando";
                                            CargaMenu(CashBoxAccess.Access.Cobro);
                                            break;
                                        case 5:
                                            btnApertura.Text = "   Aperturar Caja";
                                            lblEstadoCaja.Text = "Cerrada";
                                            CargaMenu(CashBoxAccess.Access.Cierre);
                                            break;
                                        case 7:
                                            btnApertura.Text = "   Cerrar Caja";
                                            lblEstadoCaja.Text = "Liquidada";
                                            CargaMenu(CashBoxAccess.Access.Liquidada);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        CargaMenu(CashBoxAccess.Access.SinApertura);
                        lblEstadoCaja.Text = "Sin Apertura";
                        lblTerminal.Text = "-";
                    }
                    tslMac.Text = "ID:" + Variables.Configuration.Terminal.MacAdress;
                    tslFechaApertura.Text = "Abierta:" + (Variables.Configuration.Terminal.TerminalUsers.Count > 0 ? Variables.Configuration.Terminal.TerminalUsers.First().OpenDate.ToShortDateString() : "-");
                    tslTerminal.Text = "Terminal:" + (Variables.Configuration.Terminal.TerminalUsers.Count > 0 ? Variables.Configuration.Terminal.TerminalUsers.First().Id.ToString() : "-");
                }
                else
                {
                    mensaje = new MessageBoxForm("Error", "La terminal no ha sido dada de alta.", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    Thread t = new Thread(new ThreadStart(ThreadProc));
                    t.SetApartmentState(ApartmentState.STA);
                    t.Start();
                    this.Close();
                }
            }
            if (Variables.LoginModel.RolName.ToList().Find(x => x.Contains("Admin")) != null)
            {
                lblTitulo.Text = "Bienvenido(a)";
                lblTerminal.Text = String.Empty;
                lblEstadoCaja.Text = String.Empty;
                btnApertura.Text = "Alta de Caja";
                CargaMenu(CashBoxAccess.Access.Admin);
            }
            //if (Variables.LoginModel.RolName.ToList().Find(x => x.Contains("Supervisor")) != null)
            //{
                opcionesToolStripMenuItem.Visible = true;
            //}
        }

        private void ThreadProc()
        {
            Application.Run(new Login());
        }
        #endregion

        private void pnlMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cajasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.Cajas");
        }

        private void facturacionAgrupadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                ShowForm("SOAPAP", "UI.FacturaAgrupada");
            

        }

        private void facturacionAgrupadaCanceladasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.FacturacionAgrupadaCancelacion");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            querys q = new querys();
            q.sacarcaja(Requests.ImpresoraPredeterminada(), Variables.Configuration.ANSII);
        }

        private void ingresosDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.ReportesForms.RepIFB");
        }

        private void ingresosPorConceptoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.ReportesForms.RepIA");
        }

        private void padrónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.ReportesForms.RepPadronWater");
        }

        private void recaudaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.ReportesForms.RepIbyC");
        }

        private void buscarContribuyenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.ReportesForms.RepTaxpayer");
        }


        private void ingresosDeTesoreríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.ReportesForms.RepIOT");
        }

        private void adeudosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.ReportesForms.RepDebts");
        }

        private void fraccionamientosNuevosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.ReportesForms.RepINA");
        }

        private void ordenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.ReportesForms.RepOrders");
        }
    }
}

using SOAPAP.Enums;
using SOAPAP.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using SOAPAP.Services;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using Spire.Pdf;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using System.Drawing.Imaging;
using Humanizer;
using SOAPAP.Tools;
using SOAPAP.Model;

namespace SOAPAP.UI
{
    public partial class ModalDetalleCobro : Form
    {
        private string NumberCardOrCheck { get; set; }
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        private bool mixEfectivo = false, mixCheque = false, mixTarjeta = false, mixTransfer = false; 
        private decimal mixAmountEfectivo = 0, mixAmountCheque = 0, mixAmountTarjeta = 0, mixAmountTransfer = 0;
        private int countMixed = 0;
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;
        private decimal Total { get; set; }
        private int paymethod = 1;
        private bool validCard = false;
        private bool validCheque = false;
        private decimal Amount { get; set; }
        private decimal Tax { get; set; }
        private decimal Rounding { get; set; }
        private decimal PaidUp { get; set; }
        private decimal TotalMixed { get; set; }
        private List<Model.Debt> Debts { get; set; }
        private string Padron { get; set; }
        decimal Porcentaje;
        bool Anual;
        bool Prepaid;
        bool validResponse = false;
        Model.TransactionVM transaction = new Model.TransactionVM();
        querys q = new querys();
        DataTable dt = new DataTable();

        public ModalDetalleCobro(decimal Amount, decimal Tax, decimal Rounding, decimal PaidUp, decimal Total, List<Model.Debt> Debts, string Padron, decimal Porcentaje, bool Anual, bool Prepaid)
        {
            InitializeComponent();
            Requests = new RequestsAPI(UrlBase);
            this.Total = Total;
            this.Amount = Amount;
            this.Tax = Tax;
            this.Rounding = Rounding;
            this.PaidUp = PaidUp;
            this.Debts = Debts;
            this.Padron = Padron;
            this.Porcentaje = Porcentaje;
            this.Anual = Anual;
            this.Prepaid = Prepaid;
        }

        private void ModalDetalleCobro_Load(object sender, EventArgs e)
        {
            pnlMixto.Visible = false;
            CargaCombo();
            lblTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", PaidUp);
            lblTotalOtros.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", PaidUp);
            lblTotalTra.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", PaidUp);
            txtEntregado.Focus();
            centraX(pnlTotal, lblTotal);
            centraX(pnlTotalOtros, lblTotalOtros);
            centraX(pnlCambio, lblCambiopnl);
            txtEntregado.Focus();
        }

        private void centraX(Control padre, Control hijo)
        {
            int x = 0;
            x = (padre.Width / 2) - (hijo.Width / 2);
            hijo.Location = new System.Drawing.Point(x, hijo.Location.Y);
        }

        private async void CargaCombo()
        {
            pnlEfectivo.Visible = false;
            pnlOtros.Visible = false;
            groupBox1.Visible = false;
            loading = new Loading();
            loading.Show(this);
            var getPaymentMethods = await Requests.SendURIAsync("/api/PayMethod", HttpMethod.Get, Variables.LoginModel.Token);
           
            if (getPaymentMethods.Contains("error"))
            {
                pnlEfectivo.Visible = true;
                groupBox1.Visible = true;
                loading.Close();
                try
                {
                    mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(getPaymentMethods).error, TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    this.Close();
                }
                catch (Exception)
                {
                    mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    this.Close();
                }
                txtEntregado.Focus();
            }
            else
            {
                var Payments = JsonConvert.DeserializeObject<List<SOAPAP.Model.PayMethod>>(getPaymentMethods).Where(x => x.IsActive).ToList();

                cmbPaymentMethod.ValueMember = "Id";
                cmbPaymentMethod.DisplayMember = "Name";
                cmbPaymentMethod.DataSource = Payments;
                cmbPaymentMethod.SelectedIndex = 0;
                loading.Close();
                pnlEfectivo.Visible = true;
                groupBox1.Visible = true;
                txtEntregado.Focus();
            }
        }

        private String MaskedNumber(String source)
        {
            StringBuilder sb = new StringBuilder(source);

            const int skipLeft = 0;
            const int skipRight = 4;

            int left = -1;

            for (int i = 0, c = 0; i < sb.Length; ++i)
            {
                if (Char.IsDigit(sb[i]))
                {
                    c += 1;

                    if (c > skipLeft)
                    {
                        left = i;

                        break;
                    }
                }
            }

            for (int i = sb.Length - 1, c = 0; i >= left; --i)
                if (Char.IsDigit(sb[i]))
                {
                    c += 1;

                    if (c > skipRight)
                        sb[i] = '*';
                }

            return sb.ToString();
        }

        private async void cmbPaymentMethod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var payment = cmbPaymentMethod.SelectedItem as Model.PayMethod;
            var name = payment.Name.ToLower();

            if (name.Contains("efectivo"))
            {
                pnlEfectivo.Visible = true;
                pnlOtros.Visible = false;
                pnlReferencia.Visible = false;
                pnlMixto.Visible = false;
                paymethod = 1;
            }
            else if (name.Contains("cheque"))
            {
                paymethod = 2;
                pnlEfectivo.Visible = false;
                pnlOtros.Visible = false;
                pnlReferencia.Visible = false;
                pnlMixto.Visible = false;
                loading = new Loading();
                loading.Show(this);
                var getBanks = await Requests.SendURIAsync("/api/ExternalOriginPayments", HttpMethod.Get, Variables.LoginModel.Token);
                if (getBanks.Contains("error"))
                {
                    pnlOtros.Visible = true;
                    txtTarjetaCheque.Visible = false;
                    txtCheque.Visible = true;
                    panel2.Visible = false;
                    lblTCheque.Text = "No. Cheque:";
                    lblAuth.Text = "    No. Cuenta:";
                    loading.Close();
                    try
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(getBanks).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        this.Close();
                    }
                    catch (Exception)
                    {
                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    var Banks = JsonConvert.DeserializeObject<List<SOAPAP.Model.ExternalOriginPayment>>(getBanks).Where(x => x.IsBank && x.IsActive).ToList();
                    Banks.Add(new Model.ExternalOriginPayment
                    {
                        Id = 0,
                        Name = "Seleccionar"
                    });
                    Banks.OrderBy(x => x.Name);
                    cmbBank.ValueMember = "Id";
                    cmbBank.DisplayMember = "Name";
                    cmbBank.DataSource = Banks;
                    cmbBank.SelectedIndex = cmbBank.FindString("Seleccionar");
                    loading.Close();
                    pnlOtros.Visible = true;
                    txtTarjetaCheque.Visible = false;
                    txtCheque.Visible = true;
                    panel2.Visible = false;
                    lblTCheque.Text = "No. Cheque:";
                    lblAuth.Text = "    No. Cuenta:";
                }
            }
            else if (name.Contains("transferencia"))
            {
                paymethod = 3;
                pnlEfectivo.Visible = false;
                pnlOtros.Visible = false;
                pnlReferencia.Visible = true;
                pnlMixto.Visible = false;
            }
            else if (name.Contains("tarjeta"))
            {
                paymethod = 4;
                pnlEfectivo.Visible = false;
                pnlOtros.Visible = false;
                pnlReferencia.Visible = false;
                pnlMixto.Visible = false;
                loading = new Loading();
                loading.Show(this);
                var getBanks = await Requests.SendURIAsync("/api/ExternalOriginPayments", HttpMethod.Get, Variables.LoginModel.Token);
                if (getBanks.Contains("error"))
                {
                    pnlOtros.Visible = true;
                    txtTarjetaCheque.Visible = true;
                    panel2.Visible = true;
                    txtCheque.Visible = false;
                    lblTCheque.Text = "No. Tarjeta:";
                    lblAuth.Text = "Autorización:";
                    loading.Close();
                    try
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(getBanks).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        this.Close();
                    }
                    catch (Exception)
                    {
                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                }
                else
                {
                    var Banks = JsonConvert.DeserializeObject<List<SOAPAP.Model.ExternalOriginPayment>>(getBanks).Where(x => x.IsBank && x.IsActive).ToList();
                    Banks.Add(new Model.ExternalOriginPayment
                    {
                        Id = 0,
                        Name = "Seleccionar"
                    });
                    Banks.OrderBy(x => x.Name);
                    cmbBank.ValueMember = "Id";
                    cmbBank.DisplayMember = "Name";
                    cmbBank.DataSource = Banks;
                    cmbBank.SelectedIndex = cmbBank.FindString("Seleccionar");
                    loading.Close();
                    pnlOtros.Visible = true;
                    txtTarjetaCheque.Visible = true;
                    txtCheque.Visible = false;
                    panel2.Visible = true;
                    lblTCheque.Text = "No. Tarjeta:";
                    lblAuth.Text = "Autorización:";
                }
            }
            else if (name.Contains("mixto"))
            {
                paymethod = 5;
                lblMixedAmountEfectivo.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", PaidUp);
                centraX(pnlContainerMixedAmount, lblMixedAmountEfectivo);
                pnlEfectivo.Visible = false;
                pnlOtros.Visible = false;
                pnlReferencia.Visible = false;
                pnlMixto.Visible = true;
                loading = new Loading();
                loading.Show(this);
                var getBanks = await Requests.SendURIAsync("/api/ExternalOriginPayments", HttpMethod.Get, Variables.LoginModel.Token);
                if (getBanks.Contains("error"))
                {
                    loading.Close();
                    try
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(getBanks).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        this.Close();
                    }
                    catch (Exception)
                    {
                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                }
                else
                {
                    var Banks = JsonConvert.DeserializeObject<List<SOAPAP.Model.ExternalOriginPayment>>(getBanks).Where(x => x.IsBank && x.IsActive).ToList();
                    Banks.Add(new Model.ExternalOriginPayment
                    {
                        Id = 0,
                        Name = "Seleccionar"
                    });
                    Banks.OrderBy(x => x.Name);
                    cbxMixedCheque.ValueMember = "Id";
                    cbxMixedCheque.DisplayMember = "Name";
                    cbxMixedCheque.DataSource = Banks;
                    cbxMixedCheque.SelectedIndex = cbxMixedCheque.FindString("Seleccionar");

                    cbxMixedTarjeta.ValueMember = "Id";
                    cbxMixedTarjeta.DisplayMember = "Name";
                    cbxMixedTarjeta.DataSource = Banks;
                    cbxMixedTarjeta.SelectedIndex = cbxMixedTarjeta.FindString("Seleccionar");
                    loading.Close();
                }
            }
        }

        private void txtTarjetaCheque_Leave(object sender, EventArgs e)
        {
            if ((new Regex(@"^4[0-9]{12}(?:[0-9]{3})?$")).IsMatch(txtTarjetaCheque.Text)) //Visa
            {
                lblTypeElectron.ForeColor = Color.Navy;
                lblTypeElectron.Text = "Visa";
                txtTarjetaCheque.ForeColor = Color.Black;
                txtTarjetaCheque.Text = MaskedNumber(txtTarjetaCheque.Text);
                validCard = true;
            }
            else if ((new Regex(@"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$")).IsMatch(txtTarjetaCheque.Text)) //MasterCard
            {
                lblTypeElectron.ForeColor = Color.OrangeRed;
                lblTypeElectron.Text = "Mastercard";
                txtTarjetaCheque.ForeColor = Color.Black;
                txtTarjetaCheque.Text = MaskedNumber(txtTarjetaCheque.Text);
                validCard = true;
            }
            else if((new Regex(@"^3[47][0-9]{13}$")).IsMatch(txtTarjetaCheque.Text)) //American Express
            {
                lblTypeElectron.ForeColor = Color.DarkSeaGreen;
                lblTypeElectron.Text = "American Express";
                txtTarjetaCheque.ForeColor = Color.Black;
                txtTarjetaCheque.Text = MaskedNumber(txtTarjetaCheque.Text);
                validCard = true;
            }
            else
            {
                mensaje = new MessageBoxForm("Error","Error en la tarjeta", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
                txtTarjetaCheque.ForeColor = Color.Magenta;
                validCard = false;
            }
        }

        private void txtCheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCheque_TextChanged(object sender, EventArgs e)
        {
            if (txtCheque.Text.Length < 31)
            {
                txtCheque.ForeColor = Color.Red;
                validCheque = false;
            }
            else if (txtCheque.Text.Length == 31)
            {
                txtCheque.ForeColor = Color.Black;
                validCheque = true;
            }
        }

        private void txtEntregado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtEntregado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtEntregado.Text))
                {
                    mensaje = new MessageBoxForm("Error", "No se ha ingresado un monto, favor de verificar", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else if (Convert.ToDecimal(txtEntregado.Text) < PaidUp)
                {

                    mensaje = new MessageBoxForm("Error", "El monto que se está recibiendo es menor a la cantidad a pagar", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    if (Debts != null || Anual || Prepaid)
                        PostTransaction();
                    else
                        PostTransactionOrder();
                }
            }
        }

        private void txtEntregado_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEntregado.Text) && !txtEntregado.Text.StartsWith("."))
            {

                var entregado = Convert.ToDecimal(txtEntregado.Text);
                if (entregado > PaidUp)
                {
                    if(((PaidUp - entregado) * -1) > 1000){
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto proporcionado supera en cambio máximo permitido, favor de verificar", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        txtEntregado.Text = "";
                    }
                    else
                    {
                        lblCambiopnl.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", (PaidUp - entregado) * -1);
                        this.lblCambiopnl.ForeColor = System.Drawing.Color.Black;
                        centraX(pnlCambio, lblCambiopnl);
                    }
                }
                else
                {
                    lblCambiopnl.Text = "$0.00";
                    this.lblCambiopnl.ForeColor = System.Drawing.Color.DarkGray;
                    centraX(pnlCambio, lblCambiopnl);
                }
            }
            else
            {
                lblCambiopnl.Text = "$0.00";
                this.lblCambiopnl.ForeColor = System.Drawing.Color.DarkGray;
                centraX(pnlCambio, lblCambiopnl);
            }
        }

        private void txtAuth_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.Enter)
            //{
            //    if (string.IsNullOrEmpty(txtEntregado.Text))
            //    {
            //        mensaje = new MessageBoxForm("Error", "No se ha ingresado un monto, favor de verificar", TypeIcon.Icon.Cancel);
            //        result = mensaje.ShowDialog();
            //    }
            //    else if (Convert.ToDecimal(txtEntregado.Text) < PaidUp)
            //    {

            //        mensaje = new MessageBoxForm("Error", "El monto que se está recibiendo es menor a la cantidad a pagar", TypeIcon.Icon.Cancel);
            //        result = mensaje.ShowDialog();
            //    }
            //    else
            //    {
            //        PostTransaction();
            //    }
            //}
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (paymethod)
            {
                case 1:
                    if (string.IsNullOrEmpty(txtEntregado.Text))
                    {
                        mensaje = new MessageBoxForm("Error", "No se ha ingresado un monto, favor de verificar", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else if (Convert.ToDecimal(txtEntregado.Text) < PaidUp)
                    {

                        mensaje = new MessageBoxForm("Error", "El monto que se está recibiendo es menor a la cantidad a pagar", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        if (Debts != null || Anual || Prepaid)
                            PostTransaction();
                        else
                            PostTransactionOrder();
                    }
                    break;
                case 2:
                    if (string.IsNullOrEmpty(txtCheque.Text) || string.IsNullOrEmpty(txtAuth.Text) || cmbBank.SelectedIndex == cmbBank.FindString("Seleccionar"))
                    {
                        mensaje = new MessageBoxForm("Error", "Error al intentar realizar el cobro favor de verificar los campos", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else if (!validCheque)
                    {
                        mensaje = new MessageBoxForm("Error", "Error al intentar realizar el cobro, los datos de la tarjeta no son validos", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        if (Debts != null || Anual || Prepaid)
                            PostTransaction();
                        else
                            PostTransactionOrder();
                    }
                    break;
                case 3:
                    if (string.IsNullOrEmpty(txtReferencia.Text))
                    {
                        mensaje = new MessageBoxForm("Error", "Error al intentar realizar el cobro favor de verificar los campos", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        if (Debts != null || Anual || Prepaid)
                            PostTransaction();
                        else
                            PostTransactionOrder();
                    }
                    break;
                case 4:
                    if (string.IsNullOrEmpty(txtTarjetaCheque.Text) || string.IsNullOrEmpty(txtAuth.Text) || cmbBank.SelectedIndex == cmbBank.FindString("Seleccionar"))
                    {
                        mensaje = new MessageBoxForm("Error", "Error al intentar realizar el cobro favor de verificar los campos", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else if (!validCard)
                    {
                        mensaje = new MessageBoxForm("Error", "Error al intentar realizar el cobro, los datos de la tarjeta no son validos", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        if (Debts != null || Anual || Prepaid)
                            PostTransaction();
                        else
                            PostTransactionOrder();
                    }
                  
                    break;
                case 5:
                    decimal resta = Convert.ToDecimal((Regex.Replace(lblMixedAmountEfectivo.Text, @"[^\d.]", "")));
                    //ChangeTabMixed();
                    if (resta > 0 || resta < 0)
                    {
                        mensaje = new MessageBoxForm("Error", "Error al intentar realizar el cobro, los montos son incorrectos, favor de verificar", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else if (ValidPayMixed())
                    {
                        if (Debts != null || Anual || Prepaid)
                            PostTransaction();
                        else
                            PostTransactionOrder();
                    }
                    break;
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtAuth_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private async void PostTransaction()
        {
            List<Model.DebtDetail> DebtDetailCollection = new List<Model.DebtDetail>();
            Model.PaymentConceptsVM paymentVM = new Model.PaymentConceptsVM();      

            decimal PorPagar = 0;
            decimal TotalIva = 0;
            decimal TotalSinIva = 0;
            decimal tmp = 0;
            decimal IVA = 0;
            decimal onAccount = 0;
            decimal totalDisponible = PaidUp;
            bool amout = false;
            bool cambio = false;
            bool haveProduct = false;
            decimal pagado = 0;
            decimal ivaTotal = 0;
            string xmltimbrado = string.Empty;
            string resultados = string.Empty;
            int typeOfPay = 0;

            loading = new Loading();
            loading.Show(this);
          
            transaction.Sign = true;
            transaction.Amount = PaidUp;
            transaction.PercentageTax = Variables.Configuration.IVA;
            transaction.Rounding = Rounding;
            transaction.Total = PaidUp;
            transaction.Aplication = "SISCOMCAJA";
            transaction.TypeTransactionId = 3;
            transaction.PayMethodId = Convert.ToInt32(cmbPaymentMethod.SelectedValue.ToString());
            transaction.TerminalUserId = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id;
            transaction.Cancellation = "";
            transaction.OriginPaymentId = 1;
            transaction.ExternalOriginPaymentId = cmbBank.SelectedValue == null ? 1 : Convert.ToInt32(cmbBank.SelectedValue.ToString());
            
            transaction.AgreementId = Variables.Agreement.Id;
            transaction.Account = Variables.Agreement.Account;
            transaction.NumberBank = (string.IsNullOrEmpty(txtCheque.Text) ? txtTarjetaCheque.Text : txtCheque.Text);
            //if (transaction.PayMethodId == 4)
            //{
            //    //TODO
            //    List<DetailOfPaymentMethods> detailOfPaymentMethods = new List<DetailOfPaymentMethods>();
            //    for (int i = 0; i <= countMixed; i++)
            //    {
            //        if (mixEfectivo)
            //        {
                       
            //        }
            //    }
            //}
            if (paymethod == 2)
            {
                transaction.AccountNumber = txtAuth.Text;
            }
            else
            {
                transaction.AuthorizationOriginPayment = (string.IsNullOrEmpty(txtAuth.Text) ? "" : txtAuth.Text);
            }
            try
            {
                if (Anual)
                {
                    transaction.Percentage = (Int16)Porcentaje;
                    transaction.Type = "PAY06";
                    transaction.PaytStatus = "ED005";
                    transaction.transactionDetails.Add(new Model.TransactionDetail
                    {
                        CodeConcept = "350",
                        Description = "PAGO ANUAL ANTICIPADO",
                        Amount = PaidUp
                    });

                    var a = JsonConvert.SerializeObject(transaction);
                    HttpContent content = new StringContent(a, Encoding.UTF8, "application/json");
                    //TODO
                    resultados = await Requests.SendURIAsync("/api/Transaction/Prepaid/" + Variables.Agreement.Id, HttpMethod.Post, Variables.LoginModel.Token, content);

                    if (resultados.Contains("error"))
                    {
                        loading.Close();
                        try
                        {
                            mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultados).error, TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                            this.Close();
                        }
                        catch (Exception)
                        {
                            mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                    {
                        validResponse = true;
                    }
                    typeOfPay = 1;
                }
                else if(Prepaid)
                {
                    transaction.Percentage = 0;
                    transaction.Type = "PAY04";
                    transaction.PaytStatus = "ED005";
                    transaction.transactionDetails.Add(new Model.TransactionDetail
                    {
                        CodeConcept = "350",
                        Description = "PAGO ANTICIPADO",
                        Amount = PaidUp
                    });

                    var a = JsonConvert.SerializeObject(transaction);
                    HttpContent content = new StringContent(a, Encoding.UTF8, "application/json");
                    //TODO
                    resultados = await Requests.SendURIAsync("/api/Transaction/Prepaid/" + Variables.Agreement.Id, HttpMethod.Post, Variables.LoginModel.Token, content);

                    if (resultados.Contains("error"))
                    {
                        loading.Close();
                        try
                        {
                            mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultados).error, TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                            this.Close();
                        }
                        catch (Exception)
                        {
                            mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                    {
                        validResponse = true;
                    }
                   
                    typeOfPay = 2;
                }
                else
                {
                    //Validate that there is a discount at the time of making a payment
                    resultados = await Requests.SendURIAsync($"/api/DiscountAuthorizations/GetDiscountAuthorizationByAccount/{Variables.Agreement.Account}", HttpMethod.Get, Variables.LoginModel.Token);
                    if (resultados.Contains("error"))
                    {
                        loading.Close();
                        try
                        {
                            mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultados).error, TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                            this.Close();
                        }
                        catch (Exception)
                        {
                            mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                    {
                        List<DiscountAuthorizationVM> discounts = JsonConvert.DeserializeObject<List<DiscountAuthorizationVM>>(resultados);
                        if(discounts.Count > 0)
                        {
                            loading.Close();
                            mensaje = new MessageBoxForm(Variables.titleprincipal, "La cuenta que intenta cobrar tiene una solicitud de descuento pendiente de autorización, de proceder con el cobro la solicitud quedara anulada, ¿Deseas proceder con el cobro de todas formas?", TypeIcon.Icon.Info, true);
                            result = mensaje.ShowDialog();
                            if(result == DialogResult.Cancel)
                                throw new Exception();
                            if(result == DialogResult.OK)
                            {
                                loading = new Loading();
                                loading.Show(this);
                                AuthorizationDiscountVM discountVM = new AuthorizationDiscountVM
                                {
                                    Id = discounts.FirstOrDefault().Id,
                                    Key = discounts.FirstOrDefault().KeyFirebase,
                                    Status = "EDE03",
                                    UserId = Variables.LoginModel.User,
                                    ResponseObservations = "SOLICITUD CANCELADA POR CAJERO AL REALIZAR EL COBRO"
                                };
                                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(discountVM), Encoding.UTF8, "application/json");
                                resultados = await Requests.SendURIAsync($"/api/DiscountAuthorizations/{discounts.FirstOrDefault().Id}", HttpMethod.Post, Variables.LoginModel.Token, httpContent);
                                if (resultados.Contains("error"))
                                {
                                    loading.Close();
                                    try
                                    {
                                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultados).error, TypeIcon.Icon.Cancel);
                                        result = mensaje.ShowDialog();
                                        this.Close();
                                    }
                                    catch (Exception)
                                    {
                                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                                        result = mensaje.ShowDialog();
                                        this.Close();
                                    }
                                }
                            }
                        }
                    }

                    //------------------------------------- Validacion de Productos en cuenta ---------------------------------------
                    if (Debts.Any(x => x.Type.Contains("TIP02")))
                    {
                        haveProduct = true;
                    }
                    //---------------------------------------------------------------------------------------------------------------

                    if (Debts.Any(x => x.Type == "TIP01") && Debts.Any(x => x.Type == "TIP03") && Debts.Any(x => x.Type == "TIP02"))
                        transaction.Type = "PAY03";
                    else if (Debts.Any(x => x.Type == "TIP01") && Debts.Any(x => x.Type == "TIP03"))
                        transaction.Type = "PAY03";
                    else if (Debts.Any(x => x.Type == "TIP01") && Debts.Any(x => x.Type == "TIP02"))
                        transaction.Type = "PAY03";
                    if (Debts.Any(x => x.Type == "TIP03") && Debts.Any(x => x.Type == "TIP02"))
                        transaction.Type = "PAY03";
                    else if (Debts.Any(x => x.Type == "TIP01"))
                        transaction.Type = "PAY01";
                    else if (Debts.Any(x => x.Type == "TIP04"))
                        transaction.Type = "PAY01";
                    else if (Debts.Any(x => x.Type == "TIP03"))
                        transaction.Type = "PAY05";
                    else if (Debts.Any(x => x.Type == "TIP02"))
                        transaction.Type = "PAY02";


                    if (haveProduct)
                    {
                        Debts.Where(x => x.Type == "TIP02").ToList().ForEach(x =>
                        {
                            TotalIva = 0;
                            x.DebtDetails.ToList().ForEach(y =>
                            {
                                if (y.HaveTax)
                                    TotalIva = TotalIva + (Math.Round(((y.Amount - y.OnAccount) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100, 2));

                            });
                            PorPagar = x.DebtDetails.Sum(z => ((z.Amount - z.OnAccount))) + TotalIva;
                            if(PorPagar > totalDisponible)
                            {
                                var textIva = (TotalIva > 0) ? "IVA incluido" : "";
                                mensaje = new MessageBoxForm("Error", "El Monto proporcionado no cubre el producto, " +
                                                                      "el monto minimo para pagar es de: "+ string.Format(new CultureInfo("es-MX"), "{0:C2}", PorPagar) + " " + textIva +
                                                                      ", y no se puede generar un pago parcial a la deuda, favor de verificar ", TypeIcon.Icon.Cancel);
                                result = mensaje.ShowDialog();
                                throw new Exception();
                            }
                            else
                            {
                                decimal total = 0;
                                x.DebtDetails.ToList().ForEach(y =>
                                {
                                    if (y.HaveTax)
                                    {
                                        y.OnPayment = (y.Amount - y.OnAccount);
                                        y.OnAccount = y.OnAccount + y.OnPayment;
                                        y.Tax = (Math.Round((y.OnPayment * Convert.ToDecimal(Variables.Configuration.IVA)) / 100, 2));
                                        total += y.OnAccount;
                                        pagado += y.OnPayment;
                                        ivaTotal += y.Tax;
                                    }
                                    else
                                    {
                                        y.OnPayment = (y.Amount - y.OnAccount);
                                        y.OnAccount = y.OnAccount + y.OnPayment;
                                        total += y.OnAccount;
                                        pagado += y.OnPayment;
                                    }

                                });
                                onAccount = Convert.ToDecimal((x.DebtDetails.Sum(z => (z.OnPayment)) + ivaTotal).ToString("#.##"));

                                x.NewStatus = "ED005";
                                x.OnAccount = total;
                                transaction.PaytStatus = "ED005";
                                transaction.Tax += ivaTotal;
                                transaction.Total = PaidUp;
                                transaction.Amount = PaidUp - Math.Round(transaction.Tax, 2);
                                totalDisponible = totalDisponible - PorPagar;
                                ivaTotal = 0;
                            }
                        });
                    }

                    pagado = 0;
                    ivaTotal = 0;

                    Debts.ToList().ForEach(x =>
                    {
                        TotalIva = 0;
                        x.DebtDetails.ToList().ForEach(y =>
                        {
                            if (y.HaveTax)
                                TotalIva = TotalIva + (Math.Round(((y.Amount - y.OnAccount) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100, 2));
                        });
                        PorPagar = x.DebtDetails.Sum(z => ((z.Amount - z.OnAccount))) + TotalIva;

                        if (PorPagar <= totalDisponible && totalDisponible != 0)
                        {

                            decimal total = 0;
                            x.DebtDetails.ToList().ForEach(y =>
                            {
                                if (y.HaveTax)
                                {
                                    y.OnPayment = (y.Amount - y.OnAccount);
                                    y.OnAccount = y.OnAccount + y.OnPayment;
                                    y.Tax = (Math.Round(y.OnPayment * Convert.ToDecimal(.16), 2));
                                    total += y.OnAccount;
                                    pagado += y.OnPayment;
                                    ivaTotal += y.Tax;
                                }
                                else
                                {
                                    y.OnPayment = (y.Amount - y.OnAccount);
                                    y.OnAccount = y.OnAccount + y.OnPayment;
                                    total += y.OnAccount;
                                    pagado += y.OnPayment;
                                }
                            });
                            onAccount = Convert.ToDecimal((x.DebtDetails.Sum(z => (z.OnPayment)) + ivaTotal).ToString("#.##"));

                            x.NewStatus = "ED005";
                            x.OnAccount = total;
                            transaction.PaytStatus = "ED005";
                            transaction.Tax += ivaTotal;
                            transaction.Total = PaidUp;
                            transaction.Amount = PaidUp - Math.Round(transaction.Tax, 2);
                            totalDisponible = totalDisponible - PorPagar;
                            ivaTotal = 0;
                            cambio = false;
                        }
                        else if (totalDisponible != 0 && totalDisponible > 0)
                        {
                            ivaTotal = 0;
                            TotalSinIva = x.DebtDetails.Sum(z => (z.Amount - z.OnAccount));
                            tmp = TotalIva + TotalSinIva;
                            x.NewStatus = "ED004";
                            bool notValid = true;
                            bool wrongIVA = false;
                            do
                            {
                                x.DebtDetails.ToList().ForEach(y =>
                                {
                                    if (notValid == false)
                                    {
                                        decimal ajuste = 0;
                                        if (wrongIVA)
                                        {
                                            var detail = x.DebtDetails.Where(h => h.HaveTax).FirstOrDefault();
                                            if (IVA < ivaTotal)
                                            {
                                                ajuste = IVA - ivaTotal;
                                                detail.Tax = detail.Tax + ajuste;
                                                ivaTotal = ivaTotal + ajuste;
                                                ajuste = 0;
                                            }
                                            else if (IVA > ivaTotal)
                                            {
                                                ajuste = ivaTotal - IVA;
                                                detail.Tax = detail.Tax - ajuste;
                                                ivaTotal = ivaTotal - ajuste;
                                                ajuste = 0;
                                            }
                                            notValid = true;
                                            cambio = true;
                                            wrongIVA = false;
                                            return;
                                        }
                                        else
                                        {
                                            if (totalDisponible < onAccount)
                                            {
                                                ajuste = totalDisponible - onAccount;
                                                var detail = x.DebtDetails.Where(h => !h.HaveTax).FirstOrDefault();
                                                detail.OnAccount = detail.OnAccount + ajuste;
                                                detail.OnPayment = detail.OnPayment + ajuste;
                                                ajuste = 0;
                                            }
                                            else if (totalDisponible > onAccount)
                                            {
                                                ajuste = onAccount - totalDisponible;
                                                var detail = x.DebtDetails.Where(h => !h.HaveTax).FirstOrDefault();
                                                detail.OnAccount = detail.OnAccount - ajuste;
                                                detail.OnPayment = detail.OnPayment - ajuste;

                                                ajuste = 0;
                                            }


                                            notValid = true;
                                            onAccount = Convert.ToDecimal((x.DebtDetails.Sum(z => (z.OnPayment)) + ivaTotal).ToString("#.##"));
                                            x.OnAccount = x.OnAccount + onAccount - ivaTotal;
                                            amout = true;
                                            cambio = true;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (!cambio)
                                        {
                                            if (totalDisponible > onAccount || totalDisponible != 0)
                                            {
                                                var calc = Math.Round(((y.Amount - y.OnAccount) / tmp) * totalDisponible, 2);
                                                y.OnAccount = y.OnAccount + calc;
                                                IVA = (TotalIva > 0) ? Convert.ToDecimal(((TotalIva / tmp) * totalDisponible).ToString("#.##")) : 0;
                                                y.OnPayment = calc;
                                                if (y.HaveTax)
                                                {
                                                    y.Tax = Convert.ToDecimal((Math.Round(y.OnPayment * Convert.ToDecimal(.16), 2)));
                                                    ivaTotal += y.Tax;
                                                }
                                            }
                                        }
                                    }
                                });
                                onAccount = Math.Round((x.DebtDetails.Sum(z => (z.OnPayment)) + ivaTotal), 2);
                                notValid = false;
                            } while (totalDisponible > onAccount || onAccount > totalDisponible || wrongIVA);


                            if (!amout)
                            {
                                x.OnAccount = x.OnAccount + onAccount - ivaTotal;
                            }


                            transaction.Tax += ivaTotal;
                            transaction.Amount = PaidUp - (transaction.Tax);
                            transaction.PaytStatus = "ED003";
                            transaction.Total = Math.Round(PaidUp, 2);

                            totalDisponible = totalDisponible - onAccount;
                        }
                    });

                    Debts.ForEach(x =>
                    {
                        x.DebtDetails.ToList().ForEach(y =>
                        {
                            DebtDetailCollection.Add(y);
                        });
                    });

                    var dd = JsonConvert.SerializeObject(Debts);

                    transaction.transactionDetails = DebtDetailCollection
                                            .GroupBy(x => new { x.CodeConcept, x.NameConcept })
                                            .Select(g => new Model.TransactionDetail
                                            {
                                                CodeConcept = g.Key.CodeConcept,
                                                Description = g.Key.NameConcept,
                                                Amount = g.Sum(s => (s.OnPayment))
                                            }).ToList();


                    paymentVM.Transaction = transaction;
                    paymentVM.Debt = Debts;

                    var a = JsonConvert.SerializeObject(paymentVM);
                    HttpContent content = new StringContent(a, Encoding.UTF8, "application/json");
                    //TODO
                    resultados = await Requests.SendURIAsync("/api/Transaction", HttpMethod.Post, Variables.LoginModel.Token, content);
                    if (resultados.Contains("error"))
                    {
                        loading.Close();
                        try
                        {
                            mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultados).error, TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                            this.Close();
                        }
                        catch (Exception)
                        {
                            mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                    {
                        validResponse = true;
                    }
                    typeOfPay = 3;
                }

              
                if(validResponse)
                {
                    dt = await q.GETTransactionID("/api/Transaction/" + resultados);
                    Variables.idtransaction = Convert.ToInt32(resultados);
                    

                    if (Requests.EstaEnLineaLaImpresora(Requests.ImpresoraPredeterminada()))
                    {
                        if (Properties.Settings.Default.Printer == true)
                        {
                            if (Variables.Configuration.CFDI == "Verdadero")
                            {
                                Form loadings = new Loading();
                                loadings.Show(this);
                                Facturaelectronica fs = new Facturaelectronica();
                                //xmltimbrado = await fs.facturar(resultados, "ET001", "");                               
                                xmltimbrado = await fs.generaFactura(Variables.idtransaction.ToString(), "ET001");
                                if (xmltimbrado.Contains("error"))
                                {
                                    loading.Close();
                                    mensaje = new MessageBoxForm(Variables.titleprincipal, xmltimbrado.Split('/')[1].ToString(), TypeIcon.Icon.Cancel);
                                    mensaje.ShowDialog();
                                }
                                else
                                {

                                    PdfDocument pdfdocument = new PdfDocument();
                                    pdfdocument.LoadFromFile(xmltimbrado);
                                    pdfdocument.PrinterName = Requests.ImpresoraPredeterminada();
                                    pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                                    pdfdocument.PrintDocument.Print();
                                    pdfdocument.Dispose();
                                    loading.Close();
                                }
                            }
                            else
                            {
                                Tiket imp = new Tiket();
                                imp.Imprime(dt, 2, (PaidUp - (IVA + Math.Round(ivaTotal, 2))).ToString()
                                                 , (IVA + Math.Round(ivaTotal, 2)).ToString()
                                                 , Rounding.ToString()
                                                 , (PaidUp).ToString()
                                                 , cmbPaymentMethod.Text
                                                 , Padron
                                                 , Variables.foliocaja
                                                 , ""//TODO
                                                 , Variables.Agreement.Account
                                                 , Variables.Agreement.Clients.First().RFC
                                                 , Variables.foliotransaccion
                                                 , (Variables.Agreement.Addresses.First().Street
                                                      + " NO." + Variables.Agreement.Addresses.First().Outdoor
                                                      + " INT." + Variables.Agreement.Addresses.First().Indoor
                                                      + ", COL." + Variables.Agreement.Addresses.First().Suburbs.Name
                                                      + ". " + Variables.Agreement.Addresses.First().Suburbs.Towns.Name
                                                      + ". " + Variables.Agreement.Addresses.First().Suburbs.Towns.States.Name));
                                q.sacarcaja(Requests.ImpresoraPredeterminada(), Variables.Configuration.ANSII);
                            }
                        }
                        else
                        {
                            if (Variables.Configuration.CFDI == "Verdadero")
                            {
                                Form loadings = new Loading();
                                loadings.Show(this);
                                Facturaelectronica fs = new Facturaelectronica();
                                //xmltimbrado = await fs.facturar(Variables.idtransaction.ToString(), "ET001", "");                                
                                xmltimbrado = await fs.generaFactura(Variables.idtransaction.ToString(), "ET001");
                                //separadas = xmltimbrado.Split('/');
                                if (xmltimbrado.Contains("error"))
                                {
                                    loading.Close();
                                    try
                                    {
                                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultados).error, TypeIcon.Icon.Cancel);
                                        result = mensaje.ShowDialog();
                                        this.Close();
                                    }
                                    catch (Exception)
                                    {
                                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                                        result = mensaje.ShowDialog();
                                        this.Close();
                                    }
                                }
                                //if (xmltimbrado.Contains("Success"))
                                else
                                {
                                    PdfDocument pdfdocument = new PdfDocument();
                                    pdfdocument.LoadFromFile(xmltimbrado);
                                    pdfdocument.PrinterName = q.ImpresoraPredeterminada();
                                    pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                                    pdfdocument.PrintDocument.Print();
                                    pdfdocument.Dispose();
                                    loadings.Close();
                                }
                               
                            }
                            else
                            {
                                Form loadings = new Loading();
                                loadings.Show(this);
                                Variables.optionvistaimpresion = 1;
                                impresionhoja();
                                loadings.Close();
                            }
                        }
                    }
                    switch (typeOfPay)
                    {
                        case 1:
                            mensaje = new MessageBoxForm("Detalle de Cobro", "El cobro anual con exito", TypeIcon.Icon.Success);
                            break;
                        case 2:
                            mensaje = new MessageBoxForm("Detalle de Cobro", "Se a generado el anticipo correctamente", TypeIcon.Icon.Success);
                            break;
                        case 3:
                            mensaje = new MessageBoxForm("Detalle de Cobro", "El cobro se ha realizado con exito", TypeIcon.Icon.Success);
                            break;
                        default:
                            break;
                    }
                   
                    if (mensaje.ShowDialog() == DialogResult.OK)
                    {
                        this.Close();
                    }

                }
            }
            catch (Exception e) { loading.Close(); }
           
        }

        private async void PostTransactionOrder()
        {
            List<Model.OrderSaleDetails> OrderDetailCollection = new List<Model.OrderSaleDetails>();
            Model.PaymentOrdersVM paymentVM = new Model.PaymentOrdersVM();
            List<Model.OrderSale> OrderSaleCollection = new List<Model.OrderSale>();

            decimal PorPagar = 0;
            decimal TotalIva = 0;
            decimal TotalSinIva = 0;
            decimal tmp = 0;
            decimal IVA = 0;
            decimal onAccount = 0;
            decimal totalDisponible = PaidUp;
            bool amout = false;
            bool cambio = false;
            decimal pagado = 0;
            decimal ivaTotal = 0;
            decimal total = 0;
            string xmltimbrado = string.Empty;

            loading = new Loading();
            loading.Show(this);

            transaction.Sign = true;
            transaction.Amount = PaidUp;
            transaction.PercentageTax = Variables.Configuration.IVA;
            transaction.Rounding = Rounding;
            transaction.Total = PaidUp;
            transaction.Aplication = "SISCOMCAJA";
            transaction.TypeTransactionId = 3;
            transaction.PayMethodId = Convert.ToInt32(cmbPaymentMethod.SelectedValue.ToString());
            transaction.TerminalUserId = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id;
            transaction.Cancellation = "";
            transaction.OriginPaymentId = 1;
            transaction.ExternalOriginPaymentId = 1;
            transaction.Type = "PAY02";
            transaction.AgreementId = Variables.Agreement != null ? Variables.Agreement.Id : 0;
            transaction.OrderSaleId = Variables.OrderSale != null ? Variables.OrderSale.Id : 0;
            transaction.Account = Variables.OrderSale.Folio;
            transaction.NumberBank = (string.IsNullOrEmpty(txtCheque.Text) ? txtTarjetaCheque.Text : txtCheque.Text);
            if (paymethod == 2)
            {
                transaction.AccountNumber = txtAuth.Text;
            }
            else
            {
                transaction.AuthorizationOriginPayment = (string.IsNullOrEmpty(txtAuth.Text) ? "" : txtAuth.Text);
            }

            Variables.OrderSale.OrderSaleDetails.ToList().ForEach(x =>
            {
                PorPagar = Math.Round((x.Amount - x.OnAccount) + (x.HaveTax ? Convert.ToDecimal(((x.Amount - x.OnAccount) * 16) / 100) : 0), 2);
                total = 0;
                x.NameConcept = string.Join(" ", Regex.Split(x.NameConcept, @"(?:\r\n|\n|\r)"));
                if (PorPagar > PaidUp)
                {
                    mensaje = new MessageBoxForm("Error", "El monto recibido es menor a la cantidad a pagar, favor de verificar", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    this.Close();
                }
                else
                {
                    if (x.HaveTax)
                    {
                        x.OnAccount = (x.Amount - x.OnAccount);
                        x.Tax = (Math.Round(((x.OnAccount) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100, 2));
                        total += x.OnAccount;
                        ivaTotal += x.Tax;
                    }
                    else
                    {
                        x.OnAccount = (x.Amount - x.OnAccount);
                        total += x.OnAccount;
                    }


                }
                Variables.OrderSale.OnAccount += total;
            });

            transaction.Tax += ivaTotal;
            transaction.Total = PaidUp;
            transaction.Amount = PaidUp - Math.Round(transaction.Tax, 2);
            Variables.OrderSale.Status = "EOS02";
            
            #region Cobro Parcial Productos
            //--------------------------------------------------------------------------------------------------------------------------------------------
            //Variables.OrderSale.OrderSaleDetails.ToList().ForEach(x =>
            //{
            //    TotalIva = 0;

            //    Variables.OrderSale.OrderSaleDetails.ToList().ForEach(y =>
            //    {
            //        if (y.HaveTax)
            //            TotalIva = TotalIva + (Math.Round(((y.Amount - y.OnAccount) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100, 2));
            //    });

            //    PorPagar = Math.Round((x.Amount - x.OnAccount) + (x.HaveTax ? Convert.ToDecimal(((x.Amount - x.OnAccount) * 16) / 100) : 0), 2);
            //    total = 0;
            //    x.NameConcept = string.Join(" ", Regex.Split(x.NameConcept, @"(?:\r\n|\n|\r)"));

            //    if (PorPagar <= totalDisponible && totalDisponible != 0)
            //    {
            //        if (x.HaveTax)
            //        {
            //            x.OnAccount = (x.Amount - x.OnAccount);
            //            x.Tax = (Math.Round(((x.OnAccount) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100, 2));
            //            total += x.OnAccount;
            //            ivaTotal += x.Tax;
            //        }
            //        else
            //        {
            //            x.OnAccount = (x.Amount - x.OnAccount);
            //            total += x.OnAccount;
            //        }
            //        Variables.OrderSale.OnAccount += total;
            //    }
            //    else if (totalDisponible != 0 && totalDisponible > 0)
            //    {
            //        ivaTotal = 0;
            //        TotalSinIva = x.Amount - x.OnAccount;
            //        tmp = TotalIva + TotalSinIva;
            //        Variables.OrderSale.Status = "EOS04";
            //        bool notValid = true;
            //        bool wrongIVA = false;
            //        do
            //        {
            //            if (notValid == false)
            //            {
            //                decimal ajuste = 0;
            //                if (wrongIVA)
            //                {
            //                    if (x.HaveTax)
            //                    {
            //                        if (IVA < ivaTotal)
            //                        {
            //                            ajuste = IVA - ivaTotal;
            //                            x.Tax = x.Tax + ajuste;
            //                            ivaTotal = ivaTotal + ajuste;
            //                            ajuste = 0;
            //                        }
            //                        else if (IVA > ivaTotal)
            //                        {
            //                            ajuste = ivaTotal - IVA;
            //                            x.Tax = x.Tax - ajuste;
            //                            ivaTotal = ivaTotal - ajuste;
            //                            ajuste = 0;
            //                        }
            //                    }
            //                    notValid = true;
            //                    cambio = true;
            //                    wrongIVA = false;
            //                    return;
            //                }
            //                else
            //                {
            //                    if (totalDisponible < onAccount)
            //                    {
            //                        ajuste = totalDisponible - onAccount;
            //                        if (!x.HaveTax)
            //                        {
            //                            x.OnAccount = x.OnAccount + ajuste;
            //                            x.OnPayment = x.OnPayment + ajuste;
            //                            ajuste = 0;
            //                        }
            //                    }
            //                    else if (totalDisponible > onAccount)
            //                    {
            //                        ajuste = onAccount - totalDisponible;
            //                        if (!x.HaveTax)
            //                        {
            //                            x.OnAccount = x.OnAccount - ajuste;
            //                            x.OnPayment = x.OnPayment - ajuste;
            //                            ajuste = 0;
            //                        }
            //                    }
            //                    notValid = true;
            //                    onAccount = Math.Round(x.OnPayment + ivaTotal, 2);
            //                    Variables.OrderSale.OnAccount = Variables.OrderSale.OnAccount + onAccount - ivaTotal;
            //                    amout = true;
            //                    cambio = true;
            //                    return;
            //                }
            //            }
            //            else
            //            {
            //                if (!cambio)
            //                {
            //                    if (totalDisponible > onAccount || totalDisponible != 0)
            //                    {
            //                        var calc = Math.Round(((x.Amount - x.OnAccount) / tmp) * totalDisponible, 2);
            //                        x.OnAccount = x.OnAccount + calc;
            //                        IVA = (TotalIva > 0) ? Convert.ToDecimal(((TotalIva / tmp) * totalDisponible).ToString("#.##")) : 0;
            //                        x.OnPayment = calc;
            //                        if (x.HaveTax)
            //                        {
            //                            x.Tax = Convert.ToDecimal((Math.Round(x.OnPayment * Convert.ToDecimal(.16), 2)));
            //                            ivaTotal += x.Tax;
            //                        }
            //                    }
            //                }
            //            }
            //            onAccount = Math.Round((x.OnPayment + ivaTotal), 2);
            //            notValid = false;
            //        } while (totalDisponible > onAccount || onAccount > totalDisponible || wrongIVA);

            //        if (!amout)
            //        {
            //            x.OnAccount = x.OnAccount + onAccount - ivaTotal;
            //        }

            //        transaction.Tax += ivaTotal;
            //        transaction.Amount = PaidUp - (transaction.Tax);
            //        transaction.PaytStatus = "EOS02";
            //        transaction.Total = Math.Round(PaidUp, 2);

            //        totalDisponible = totalDisponible - onAccount;
            //    }
            //});
            //------------------------------------------------------------------------------------------------------------------------------------------------
            #endregion

            Variables.OrderSale.OrderSaleDetails.ToList().ForEach(x =>
            {
                OrderDetailCollection.Add(x);
            });

            var dd = JsonConvert.SerializeObject(Variables.OrderSale);

            transaction.transactionDetails = OrderDetailCollection
                                    .GroupBy(x => new { x.CodeConcept, x.NameConcept })
                                    .Select(g => new Model.TransactionDetail
                                    {
                                        CodeConcept = g.Key.CodeConcept,
                                        Description = g.Key.NameConcept,
                                        Amount = g.Sum(s => (s.Amount))
                                    }).ToList();


            paymentVM.Transaction = transaction;
            paymentVM.OrderSale.Add(Variables.OrderSale);

            var a = JsonConvert.SerializeObject(paymentVM);
            HttpContent content = new StringContent(a, Encoding.UTF8, "application/json");
            //TODO
            var resultados = await Requests.SendURIAsync("/api/Transaction/OrderTransaction", HttpMethod.Post, Variables.LoginModel.Token, content);

            if (resultados.Contains("error"))
            {
                loading.Close();
                try
                {
                    mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultados).error, TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    this.Close();
                }
                catch (Exception)
                {
                    mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador: -conexion interrumpida-", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                Variables.idtransaction = Convert.ToInt32(resultados);
                dt = await q.GETTransactionID("/api/Transaction/" + resultados);

                if (Requests.EstaEnLineaLaImpresora(Requests.ImpresoraPredeterminada()))
                {
                    loading.Close();
                    if (Properties.Settings.Default.Printer == true)
                    {
                        if (Variables.Configuration.CFDI == "Verdadero")
                        {
                            Form loadings = new Loading();
                            loadings.Show(this);
                            Facturaelectronica fs = new Facturaelectronica();
                            //xmltimbrado = await fs.facturar(resultados, "ET001", "");
                            xmltimbrado = await fs.generaFactura(resultados, "ET001" );
                            if (xmltimbrado.Contains("error"))
                            {
                                loadings.Close();
                                mensaje = new MessageBoxForm(Variables.titleprincipal, xmltimbrado.Split('/')[1].ToString(), TypeIcon.Icon.Cancel);
                                mensaje.ShowDialog();
                            }
                            else
                            {
                                PdfDocument pdfdocument = new PdfDocument();
                                pdfdocument.LoadFromFile(xmltimbrado);
                                pdfdocument.PrinterName = Requests.ImpresoraPredeterminada();
                                pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                                pdfdocument.PrintDocument.Print();
                                pdfdocument.Dispose();
                                loadings.Close();
                            }
                        }
                        else
                        {//////////////
                            Tiket imp = new Tiket();
                            if(Variables.Agreement != null)
                            {
                                imp.Imprime(dt, 2, (PaidUp - (IVA + Math.Round(ivaTotal, 2))).ToString()
                                             , (IVA + Math.Round(ivaTotal, 2)).ToString()
                                             , Rounding.ToString()
                                             , (PaidUp).ToString()
                                             , cmbPaymentMethod.Text
                                             , Padron
                                             , Variables.foliocaja
                                             , ""//TODO
                                             , Variables.Agreement.Account
                                             , Variables.Agreement.Clients.First().RFC
                                             , Variables.foliotransaccion
                                             , (Variables.Agreement.Addresses.First().Street
                                                  + " NO." + Variables.Agreement.Addresses.First().Outdoor
                                                  + " INT." + Variables.Agreement.Addresses.First().Indoor
                                                  + ", COL." + Variables.Agreement.Addresses.First().Suburbs.Name
                                                  + ". " + Variables.Agreement.Addresses.First().Suburbs.Towns.Name
                                                  + ". " + Variables.Agreement.Addresses.First().Suburbs.Towns.States.Name));
                            }
                            else
                            {
                                imp.Imprime(dt, 2, (PaidUp - (IVA + Math.Round(ivaTotal, 2))).ToString()
                                             , (IVA + Math.Round(ivaTotal, 2)).ToString()
                                             , Rounding.ToString()
                                             , (PaidUp).ToString()
                                             , cmbPaymentMethod.Text
                                             , Padron
                                             , Variables.foliocaja
                                             , ""//TODO
                                             , Variables.OrderSale.Folio
                                             , Variables.OrderSale.TaxUser.RFC
                                             , Variables.foliotransaccion
                                             , (Variables.Agreement.Addresses.First().Street
                                                  + " NO." + Variables.OrderSale.TaxUser.TaxAddresses.First().Outdoor
                                                  + " INT." + Variables.OrderSale.TaxUser.TaxAddresses.First().Indoor
                                                  + ", COL." + Variables.OrderSale.TaxUser.TaxAddresses.First().Suburb
                                                  + ". " + Variables.OrderSale.TaxUser.TaxAddresses.First().Town
                                                  + ". " + Variables.OrderSale.TaxUser.TaxAddresses.First().State));
                            }
                                

                            q.sacarcaja(Requests.ImpresoraPredeterminada(), Variables.Configuration.ANSII);
                        }
                    }
                    else
                    {
                        if (Variables.Configuration.CFDI == "Verdadero")
                        {
                            Form loadings = new Loading();
                            loadings.Show(this);
                            Facturaelectronica fs = new Facturaelectronica();
                            //xmltimbrado = await fs.facturar(Variables.idtransaction.ToString(), "ET001", "");                            
                            xmltimbrado = await fs.generaFactura(Variables.idtransaction.ToString(), "ET001");
                            
                            //separadas = xmltimbrado.Split('/');
                            if (xmltimbrado.Contains("error"))
                            {
                                loadings.Close();
                                mensaje = new MessageBoxForm(Variables.titleprincipal, xmltimbrado.Split('/')[1].ToString(), TypeIcon.Icon.Cancel);
                                mensaje.ShowDialog();
                            }

                            else
                            {
                                loadings.Close();
                                PdfDocument pdfdocument = new PdfDocument();
                                pdfdocument.LoadFromFile(xmltimbrado);
                                pdfdocument.PrinterName = q.ImpresoraPredeterminada();
                                pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                                pdfdocument.PrintDocument.Print();
                                pdfdocument.Dispose();
                                
                                // Directory.Delete(xmltimbrado, true);
                            }

                        }
                        else
                        {
                            Form loadings = new Loading();
                            loadings.Show(this);
                            Variables.optionvistaimpresion = 1;
                            impresionhoja();
                            loadings.Close();
                        }
                    }
                }
                mensaje = new MessageBoxForm("Detalle de Cobro", "El cobro se realizo con exito", TypeIcon.Icon.Success);
                if (mensaje.ShowDialog() == DialogResult.OK)
                {
                    this.Close();
                }

            }
        }

        public decimal RoundDown(decimal i, double decimalPlaces)
        {
            var power = Math.Pow(10, decimalPlaces);
            return Math.Floor(i * 100) / 100;
        }

        public decimal RoundUp(decimal i, double decimalPlaces)
        {
            var power = Math.Pow(10, decimalPlaces);
            return Math.Ceiling(i * 100) / 100;
        }

        public void impresionhoja()
        {
            Variables.datosgenerales.Columns.Clear();
            Variables.datosgenerales.Rows.Clear();
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-MX");

            DataColumn columns;
            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "Nombredeins";
            Variables.datosgenerales.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "RFCdeInstitucion";
            Variables.datosgenerales.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "DomiciliodeInstitucion";
            Variables.datosgenerales.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "TelefonodeInstitucion";
            Variables.datosgenerales.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "CorreodeInstitucion";
            Variables.datosgenerales.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "NombreFiscaldeInstitucion";
            Variables.datosgenerales.Columns.Add(columns);

            DataRow rows = Variables.datosgenerales.NewRow();
            rows["Nombredeins"] = Variables.Configuration.CompanyName;
            rows["RFCdeInstitucion"] = Variables.Configuration.RFC;
            rows["DomiciliodeInstitucion"] = Variables.Configuration.Address;
            rows["TelefonodeInstitucion"] = Variables.Configuration.Phone;
            rows["CorreodeInstitucion"] = Variables.Configuration.Email;
            rows["NombreFiscaldeInstitucion"] = Variables.Configuration.LegendRegime;
            Variables.datosgenerales.Rows.Add(rows);

            Variables.datospadron.Columns.Clear();
            Variables.datospadron.Rows.Clear();

            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "FoliodeCaja";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Fechayhora";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Cuenta";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Contribuyente";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Rfc";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Domicilio";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Caja";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Sucursal";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Usuario";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Periodo";
            Variables.datospadron.Columns.Add(column);

            DataRow row = Variables.datospadron.NewRow();
            row["FoliodeCaja"] = Variables.foliocaja;
            row["Fechayhora"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            row["Cuenta"] = Variables.Agreement == null ? Variables.OrderSale.Folio : Variables.Agreement.Account;
            row["Contribuyente"] = Padron;
            row["Rfc"] = Variables.Agreement == null ? Variables.OrderSale.TaxUser.RFC : Variables.Agreement.Clients.First().RFC;
            row["Domicilio"] = Variables.Agreement == null ? Variables.OrderSale.TaxUser.TaxAddresses.First().Street
                                                              + " NO." + Variables.OrderSale.TaxUser.TaxAddresses.First().Outdoor
                                                              + " INT." + Variables.OrderSale.TaxUser.TaxAddresses.First().Indoor
                                                              + ", COL." + Variables.OrderSale.TaxUser.TaxAddresses.First().Suburb
                                                              + ". " + Variables.OrderSale.TaxUser.TaxAddresses.First().Town
                                                              + ". " + Variables.OrderSale.TaxUser.TaxAddresses.First().State 
                                                              :
                                                    (Variables.Agreement.Addresses.First().Street
                                                  + " NO." + Variables.Agreement.Addresses.First().Outdoor
                                                  + " INT." + Variables.Agreement.Addresses.First().Indoor
                                                  + ", COL." + Variables.Agreement.Addresses.First().Suburbs.Name
                                                  + ". " + Variables.Agreement.Addresses.First().Suburbs.Towns.Name
                                                  + ". " + Variables.Agreement.Addresses.First().Suburbs.Towns.States.Name);

            row["Caja"] = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id;
            row["Sucursal"] = Variables.Configuration.Terminal.BranchOffice.Name;
            row["Usuario"] = Variables.LoginModel.FullName;
            row["Periodo"] = "";//TODO;
            Variables.datospadron.Rows.Add(row);

            Variables.pagos.Columns.Clear();
            Variables.pagos.Rows.Clear();

            DataColumn columnt;
            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "ID";
            Variables.pagos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Concepto";
            Variables.pagos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Importe";
            Variables.pagos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Year";
            Variables.pagos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Debperiod";
            Variables.pagos.Columns.Add(columnt);
            int count = 0;
            foreach (DataRow rowr in dt.Rows)
            {
                DataRow rowt = Variables.pagos.NewRow();
                rowt["ID"] = rowr[0].ToString();
                //rowt["Concepto"] = rowr[5].ToString();
                rowt["Concepto"] = Variables.OrderSale == null ? rowr[5].ToString().Replace("\\"," - ") : Variables.OrderSale.OrderSaleDetails.ToArray()[count].Description.Replace("\\", " - ");
                rowt["Importe"] = rowr[7].ToString();
                rowt["Year"] = rowr[8].ToString() == "" ? "2019" : rowr[8].ToString();
                rowt["Debperiod"] = rowr[9].ToString() == "" ? DateTime.Now.ToString("MMMM",culture).ToUpperInvariant() : rowr[9].ToString();
                Variables.pagos.Rows.Add(rowt);
                count++;
            }
            count = 0;
            Variables.ImagenData.Columns.Clear();
            Variables.ImagenData.Rows.Clear();

            DataColumn columnts;
            columnts = new DataColumn();
            columnts.DataType = System.Type.GetType("System.Byte[]");
            columnts.ColumnName = "Imagen1";
            Variables.ImagenData.Columns.Add(columnts);

            columnts = new DataColumn();
            columnts.DataType = System.Type.GetType("System.Byte[]");
            columnts.ColumnName = "Imagen2";
            Variables.ImagenData.Columns.Add(columnts);

            System.Drawing.Image img = q.Imagen();
            byte[] arr;
            ImageConverter converter = new ImageConverter();
            arr = (byte[])converter.ConvertTo(img, typeof(byte[]));

            var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            var qrCode = qrEncoder.Encode(Variables.foliotransaccion);
            var renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            using (var stream = new FileStream("qrcode.png", FileMode.Create))
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
            MemoryStream ms = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemp = new Bitmap(ms);
            System.Drawing.Image image = new Bitmap(imageTemp, new Size(new Point(200, 200)));
            byte[] arrs;
            ImageConverter converters = new ImageConverter();
            arrs = (byte[])converters.ConvertTo(image, typeof(byte[]));

            DataRow rowt1 = Variables.ImagenData.NewRow();
            rowt1["Imagen1"] = arr;
            rowt1["Imagen2"] = arrs;
            Variables.ImagenData.Rows.Add(rowt1);

            Variables.Foliotiket.Columns.Clear();
            Variables.Foliotiket.Rows.Clear();

            DataColumn columntss;
            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "Foliotransaccion";
            Variables.Foliotiket.Columns.Add(columntss);

            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "Letra";
            Variables.Foliotiket.Columns.Add(columntss);

            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "Pago";
            Variables.Foliotiket.Columns.Add(columntss);

            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "Subtotal";
            Variables.Foliotiket.Columns.Add(columntss);

            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "IVA";
            Variables.Foliotiket.Columns.Add(columntss);

            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "Redondeo";
            Variables.Foliotiket.Columns.Add(columntss);

            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "Total";
            Variables.Foliotiket.Columns.Add(columntss);

            Numalet let = new Numalet();
            let.MascaraSalidaDecimal = "00/100 M.N";
            let.SeparadorDecimalSalida = "pesos";
            let.LetraCapital = true;
            let.ApocoparUnoParteEntera = true;

            DataRow rowt2 = Variables.Foliotiket.NewRow();
            rowt2["Foliotransaccion"] = Variables.foliotransaccion;
            rowt2["Letra"] = let.ToCustomCardinal(PaidUp).ToUpperInvariant();
            rowt2["Pago"] = cmbPaymentMethod.Text;
            rowt2["Subtotal"] = transaction.Amount;
            rowt2["IVA"] = transaction.Tax;
            rowt2["Redondeo"] = transaction.Rounding;
            rowt2["Total"] = transaction.Total;
            Variables.Foliotiket.Rows.Add(rowt2);

            Impresion im = new Impresion();
            im.ShowDialog();
        }

        private void ModalDetalleCobro_FormClosing(object sender, FormClosingEventArgs e)
        {
            //UI.Cobro Return = ((UI.Cobro)this.Owner.OwnedForms[0]);
            //Return.anual = false;
            //Return.prepaid = false;
        }

        #region PayMixed
       
        private void TxtMixedEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void TxtMixedMontoTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void TxtMixedMontoCheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void TabMixto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeTabMixed();
        }

        private void BtnCleanCash_Click(object sender, EventArgs e)
        {
            txtMixedEfectivo.Text = "";
            SumAmount();
        }

        private void BtnCleanCard_Click(object sender, EventArgs e)
        {
            txtMixedMontoTarjeta.Text = "";
            SumAmount();
            txtMixedAutorizacion.Text = "";
            txtMixedNumeroTarjeta.Text = "";
            cbxMixedTarjeta.SelectedIndex = cbxMixedTarjeta.FindString("Seleccionar");
            lblMixedTypeElectron.Text = "";
        }

        private void BtnCleanCheque_Click(object sender, EventArgs e)
        {
            txtMixedMontoCheque.Text = "";
            SumAmount();
            txtMixedNumeroCuenta.Text = "";
            txtMixedNumeroCheque.Text = "";
            cbxMixedCheque.SelectedIndex = cbxMixedCheque.FindString("Seleccionar");
        }

        private void BtnCleanTransfer_Click(object sender, EventArgs e)
        {
            txtMixedMontoTransfer.Text = "";
            SumAmount();
            txtMixedReferencia.Text = "";
        }

        private void TxtMixedMontoTransfer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void TxtMixedNumeroTarjeta_Leave(object sender, EventArgs e)
        {
            if ((new Regex(@"^4[0-9]{12}(?:[0-9]{3})?$")).IsMatch(txtMixedNumeroTarjeta.Text)) //Visa
            {
                lblMixedTypeElectron.ForeColor = Color.Navy;
                lblMixedTypeElectron.Text = "Visa";
                txtMixedNumeroTarjeta.ForeColor = Color.Black;
                txtMixedNumeroTarjeta.Text = MaskedNumber(txtMixedNumeroTarjeta.Text);
                validCard = true;
            }
            else if ((new Regex(@"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$")).IsMatch(txtMixedNumeroTarjeta.Text)) //MasterCard
            {
                lblMixedTypeElectron.ForeColor = Color.OrangeRed;
                lblMixedTypeElectron.Text = "Mastercard";
                txtMixedNumeroTarjeta.ForeColor = Color.Black;
                txtMixedNumeroTarjeta.Text = MaskedNumber(txtMixedNumeroTarjeta.Text);
                validCard = true;
            }
            else if ((new Regex(@"^3[47][0-9]{13}$")).IsMatch(txtMixedNumeroTarjeta.Text)) //American Express
            {
                lblMixedTypeElectron.ForeColor = Color.DarkSeaGreen;
                lblMixedTypeElectron.Text = "American Express";
                txtMixedNumeroTarjeta.ForeColor = Color.Black;
                txtMixedNumeroTarjeta.Text = MaskedNumber(txtMixedNumeroTarjeta.Text);
                validCard = true;
            }
            else
            {
                mensaje = new MessageBoxForm("Error", "Error en la tarjeta", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
                txtMixedNumeroTarjeta.ForeColor = Color.Magenta;
                validCard = false;
            }
        }

        private void TxtMixedNumeroCheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TxtMixedNumeroCheque_TextChanged(object sender, EventArgs e)
        {
            if (txtMixedNumeroCheque.Text.Length < 31)
            {
                txtMixedNumeroCheque.ForeColor = Color.Red;
                validCheque = false;
            }
            else if (txtMixedNumeroCheque.Text.Length == 31)
            {
                txtMixedNumeroCheque.ForeColor = Color.Black;
                validCheque = true;
            }
        }
        
        private void ValidAmount()
        {
            if (!string.IsNullOrEmpty(txtMixedEfectivo.Text) && !mixEfectivo)
            {
                decimal resta = Convert.ToDecimal((Regex.Replace(lblMixedAmountEfectivo.Text, @"[^\d.]", "")));
                decimal efectivo = Convert.ToDecimal(txtMixedEfectivo.Text);
                mixAmountEfectivo = efectivo;
                lblMixedAmountEfectivo.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", (resta - efectivo));
                mixEfectivo = true;
                countMixed++;
            }
            if (!string.IsNullOrEmpty(txtMixedMontoTarjeta.Text) && !mixTarjeta)
            {
                decimal resta = Convert.ToDecimal((Regex.Replace(lblMixedAmountEfectivo.Text, @"[^\d.]", "")));
                decimal efectivo = Convert.ToDecimal(txtMixedMontoTarjeta.Text);
                mixAmountTarjeta = efectivo;
                lblMixedAmountEfectivo.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", (resta - efectivo));
                mixTarjeta = true;
                countMixed++;
            }
            if (!string.IsNullOrEmpty(txtMixedMontoCheque.Text) && !mixCheque)
            {
                decimal resta = Convert.ToDecimal((Regex.Replace(lblMixedAmountEfectivo.Text, @"[^\d.]", "")));
                decimal efectivo = Convert.ToDecimal(txtMixedMontoCheque.Text);
                mixAmountCheque= efectivo;
                lblMixedAmountEfectivo.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", (resta - efectivo));
                mixCheque = true;
                countMixed++;
            }
            if (!string.IsNullOrEmpty(txtMixedMontoTransfer.Text) && !mixTransfer)
            {
                decimal resta = Convert.ToDecimal((Regex.Replace(lblMixedAmountEfectivo.Text, @"[^\d.]", "")));
                decimal efectivo = Convert.ToDecimal(txtMixedMontoTransfer.Text);
                mixAmountTransfer = efectivo;
                lblMixedAmountEfectivo.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", (resta - efectivo));
                mixTransfer = true;
                countMixed++;
            }
        }
        
        private void SumAmount()
        {
            decimal cash = string.IsNullOrEmpty(txtMixedEfectivo.Text) ? 0 : Convert.ToDecimal(txtMixedEfectivo.Text);
            decimal card = string.IsNullOrEmpty(txtMixedMontoTarjeta.Text) ? 0 : Convert.ToDecimal(txtMixedMontoTarjeta.Text);
            decimal cheque = string.IsNullOrEmpty(txtMixedMontoCheque.Text) ? 0 : Convert.ToDecimal(txtMixedMontoCheque.Text);
            decimal transfer = string.IsNullOrEmpty(txtMixedMontoTransfer.Text) ? 0 : Convert.ToDecimal(txtMixedMontoTransfer.Text);
            //decimal resta = Convert.ToDecimal((Regex.Replace(lblMixedAmountEfectivo.Text, @"[^\d.]", "")));

            decimal total = PaidUp - (card + cash + cheque + transfer);
            lblMixedAmountEfectivo.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", total );
        }

        private bool ValidPayMixed()
        {
            if (mixTarjeta)
            {
                if (string.IsNullOrEmpty(txtMixedMontoTarjeta.Text) || string.IsNullOrEmpty(txtMixedAutorizacion.Text) || cbxMixedTarjeta.SelectedIndex == cmbBank.FindString("Seleccionar") || string.IsNullOrEmpty(txtMixedNumeroTarjeta.Text))
                {
                    mensaje = new MessageBoxForm("Error", "Error al intentar realizar el cobro favor de verificar los campos", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    return false;
                }
                else if (!validCard)
                {
                    mensaje = new MessageBoxForm("Error", "Error al intentar realizar el cobro, los datos de la tarjeta no son validos", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            if (mixCheque)
            {
                if (string.IsNullOrEmpty(txtMixedMontoCheque.Text) || string.IsNullOrEmpty(txtMixedNumeroCuenta.Text) || cbxMixedCheque.SelectedIndex == cmbBank.FindString("Seleccionar") || string.IsNullOrEmpty(txtMixedNumeroTarjeta.Text))
                {
                    mensaje = new MessageBoxForm("Error", "Error al intentar realizar el cobro favor de verificar los campos", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    return false;
                }
                else if (!validCheque)
                {
                    mensaje = new MessageBoxForm("Error", "Error al intentar realizar el cobro, los datos de la tarjeta no son validos", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            if (mixTransfer)
            {
                if(string.IsNullOrEmpty(txtMixedMontoTransfer.Text) || string.IsNullOrEmpty(txtMixedReferencia.Text))
                {
                    mensaje = new MessageBoxForm("Error", "Error al intentar realizar el cobro favor de verificar los campos", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            if(countMixed > 1)
            {
                return true;
            }
            else
            {
                mensaje = new MessageBoxForm("Error", "Error al intentar realizar el cobro MIXTO, debe seleccionar al menos 2 forma de pago. Favor de verificar.", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
                return false;
            }

            //return false;
        }
        
        private void ChangeTabMixed()
        {
            switch (tabMixto.SelectedIndex)
            {
                case 0:
                    ValidAmount();
                    if (mixAmountEfectivo != Convert.ToDecimal(txtMixedEfectivo.Text != "" ? txtMixedEfectivo.Text : "0"))
                    {
                        mixEfectivo = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    if (mixAmountTarjeta != Convert.ToDecimal(txtMixedMontoTarjeta.Text != "" ? txtMixedMontoTarjeta.Text : "0"))
                    {
                        mixTarjeta = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    if (mixAmountCheque != Convert.ToDecimal(txtMixedMontoCheque.Text != "" ? txtMixedMontoCheque.Text : "0"))
                    {
                        mixCheque = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    if (mixAmountTransfer != Convert.ToDecimal(txtMixedMontoTransfer.Text != "" ? txtMixedMontoTransfer.Text : "0"))
                    {
                        mixTransfer = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    decimal labelpaidUpE = Convert.ToDecimal((Regex.Replace(lblMixedAmountEfectivo.Text, @"[^\d.]", "")));
                    if (labelpaidUpE < 0)
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto recibido es mayor al monte de deuda, favor de verificar", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    break;
                case 1:
                    ValidAmount();
                    if (mixAmountTarjeta != Convert.ToDecimal(txtMixedMontoTarjeta.Text != "" ? txtMixedMontoTarjeta.Text : "0"))
                    {
                        mixTarjeta = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    if (mixAmountEfectivo != Convert.ToDecimal(txtMixedEfectivo.Text != "" ? txtMixedEfectivo.Text : "0"))
                    {
                        mixEfectivo = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    if (mixAmountCheque != Convert.ToDecimal(txtMixedMontoCheque.Text != "" ? txtMixedMontoCheque.Text : "0"))
                    {
                        mixCheque = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    if (mixAmountTransfer != Convert.ToDecimal(txtMixedMontoTransfer.Text != "" ? txtMixedMontoTransfer.Text : "0"))
                    {
                        mixTransfer = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    decimal labelpaidUpT = Convert.ToDecimal((Regex.Replace(lblMixedAmountEfectivo.Text, @"[^\d.]", "")));
                    if (labelpaidUpT < 0)
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto recibido es mayor al monte de deuda, favor de verificar", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    break;
                case 2:
                    ValidAmount();
                    if (mixAmountCheque != Convert.ToDecimal(txtMixedMontoCheque.Text != "" ? txtMixedMontoCheque.Text : "0"))
                    {
                        mixCheque = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    if (mixAmountTarjeta != Convert.ToDecimal(txtMixedMontoTarjeta.Text != "" ? txtMixedMontoTarjeta.Text : "0"))
                    {
                        mixTarjeta = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    if (mixAmountEfectivo != Convert.ToDecimal(txtMixedEfectivo.Text != "" ? txtMixedEfectivo.Text : "0"))
                    {
                        mixEfectivo = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    if (mixAmountTransfer != Convert.ToDecimal(txtMixedMontoTransfer.Text != "" ? txtMixedMontoTransfer.Text : "0"))
                    {
                        mixTransfer = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    decimal labelpaidUpC = Convert.ToDecimal((Regex.Replace(lblMixedAmountEfectivo.Text, @"[^\d.]", "")));
                    if (labelpaidUpC < 0)
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto recibido es mayor al monte de deuda, favor de verificar", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    break;
                case 3:
                    ValidAmount();
                    if (mixAmountTransfer != Convert.ToDecimal(txtMixedMontoTransfer.Text != "" ? txtMixedMontoTransfer.Text : "0"))
                    {
                        mixTransfer = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    if (mixAmountCheque != Convert.ToDecimal(txtMixedMontoCheque.Text != "" ? txtMixedMontoCheque.Text : "0"))
                    {
                        mixCheque = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    if (mixAmountTarjeta != Convert.ToDecimal(txtMixedMontoTarjeta.Text != "" ? txtMixedMontoTarjeta.Text : "0"))
                    {
                        mixTarjeta = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    if (mixAmountEfectivo != Convert.ToDecimal(txtMixedEfectivo.Text != "" ? txtMixedEfectivo.Text : "0"))
                    {
                        mixEfectivo = false;
                        countMixed--;
                        ValidAmount();
                        SumAmount();
                    }
                    decimal labelpaidUp = Convert.ToDecimal((Regex.Replace(lblMixedAmountEfectivo.Text, @"[^\d.]", "")));
                    if (labelpaidUp < 0)
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto recibido es mayor al monte de deuda, favor de verificar", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    break;
            }
        }
        #endregion


    }
}
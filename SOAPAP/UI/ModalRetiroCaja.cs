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

namespace SOAPAP.UI
{
    public partial class ModalRetiroCaja : Form
    {
        HttpContent content;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;
        int idterminals = 0;
        string datetimesf = string.Empty;
        string url1 = string.Empty;
        int options = 0;

        querys q = new querys();
        DataTable dtcombo1 = new DataTable();
        public ModalRetiroCaja(int idterminal,string datetimes,string url0,int option)
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
            btnCancelar.Visible = true;
            btnCancelar.Location = new Point(302, 5);
            btnAceptar.Location = new Point(198, 5);
            idterminals = idterminal;
            datetimesf = datetimes;
            url1 = url0;
            options = option;
        }

        public void cargarmetodosdepago()
        {
            comboBox1.DataSource = dtcombo1;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
        }

        private void centraX(Control padre, Control hijo)
        {
            int x = 0;
            x = (padre.Width / 2) - (hijo.Width / 2);
            hijo.Location = new System.Drawing.Point(x, hijo.Location.Y);
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(comboBox1.SelectedValue) ==1 && (Convert.ToDecimal(nudAmount.Value) > Convert.ToDecimal(lblRetiradoEfectivo.Text.Replace("$",""))))
            {
                     mensaje = new MessageBoxForm("Error", "El monto ingresado es mayor que al retirado", TypeIcon.Icon.Warning);
                     mensaje.ShowDialog();
            }
            else if (Convert.ToInt32(comboBox1.SelectedValue) == 2 && (Convert.ToDecimal(nudAmount.Value) > Convert.ToDecimal(lblRetiradoCheque.Text.Replace("$", ""))))
            {
                     mensaje = new MessageBoxForm("Error", "El monto ingresado es mayor que al retirado", TypeIcon.Icon.Warning);
                     mensaje.ShowDialog();
            }
            else if (Convert.ToInt32(comboBox1.SelectedValue) == 4 && (Convert.ToDecimal(nudAmount.Value) > Convert.ToDecimal(lblRetiradoTarjeta.Text.Replace("$", ""))))
            {
                mensaje = new MessageBoxForm("Error", "El monto ingresado es mayor que al retirado", TypeIcon.Icon.Warning);
                mensaje.ShowDialog();
            }
            else if (Convert.ToInt32(comboBox1.SelectedValue) == 3 && (Convert.ToDecimal(nudAmount.Value) > Convert.ToDecimal(label10.Text.Replace("$", ""))))
            {
                mensaje = new MessageBoxForm("Error", "El monto ingresado es mayor que al retirado", TypeIcon.Icon.Warning);
                mensaje.ShowDialog();
            }

            else
            { 

            loading = new Loading();
            loading.Show(this);

            SOAPAP.Model.Transaction transaction = new SOAPAP.Model.Transaction();
            transaction.Sign = false;
            transaction.Amount = nudAmount.Value;
            transaction.Aplication = "SISCOMCAJA";
            transaction.TypeTransactionId = 6;
            transaction.PayMethodId = Convert.ToInt32(comboBox1.SelectedValue);
            transaction.TerminalUserId = idterminals;

            string valores = JsonConvert.SerializeObject(transaction);
            content = new StringContent(valores, Encoding.UTF8, "application/json");
            var resultado = await Requests.SendURIAsync(string.Format(url1, idterminals), HttpMethod.Post, Variables.LoginModel.Token, content);
            if (resultado.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultado.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                mensaje = new MessageBoxForm("Transacción Exitosa", "Retiro ingresado con éxito", TypeIcon.Icon.Success);
                result = mensaje.ShowDialog();
                this.DialogResult = DialogResult.OK;

                    if (options == 1)
                    {
                        IForm formInterface = this.Owner as IForm;
                        if (formInterface != null)
                        {
                            formInterface.ShowForm("SOAPAP", "Movimientos");
                        }
                    }
                    else if (options == 2)
                    {
                        this.Close();
                    }
                    


                }
            loading.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        
        private async void ModalRetiroCaja_Load(object sender, EventArgs e)
        {
            decimal positivoe = 0;
            decimal negativoe = 0;
            decimal resultadoe = 0;

            decimal positivoc = 0;
            decimal negativoc = 0;
            decimal resultadoc = 0;

            decimal positivot = 0;
            decimal negativot  = 0;
            decimal resultadot = 0;

            decimal positivotr = 0;
            decimal negativotr = 0;
            decimal resultadotr = 0;


            decimal positivoes = 0;
            decimal negativoes = 0;
            decimal resultadoes = 0;

            decimal positivocs = 0;
            decimal negativocs = 0;
            decimal resultadocs = 0;

            decimal positivots = 0;
            decimal negativots = 0;
            decimal resultadots = 0;


            decimal positivotsr = 0;
            decimal negativotsr = 0;
            decimal resultadotsr = 0;

            string[] separadas;

            DataTable dt = new DataTable();
            loading = new Loading();
            loading.Show(this);
            dt = await q.GETTransaction("/api/Transaction/" + datetimesf + "/" + idterminals.ToString());
            foreach (DataRow row in dtcombo1.Rows)
            {
                separadas = row[0].ToString().Split('/');
                if (separadas[0].ToString() == "error")
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                    loading.Close();
                    return;
                }
            }

            foreach (DataRow row in dt.Rows)
            {

                if (Convert.ToInt32(row[14]) == 1 && Convert.ToBoolean(row[3]) == true && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 6 && Convert.ToInt32(row[19]) != 7)
                {
                    positivoe = positivoe + Convert.ToDecimal(row[7]);
                }

                else if (Convert.ToInt32(row[14]) == 1 && Convert.ToBoolean(row[3]) == false && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 7)
                {
                    negativoe = negativoe + Convert.ToDecimal(row[7]);
                }

                if (Convert.ToInt32(row[14]) == 2 && Convert.ToBoolean(row[3]) == true && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 6 && Convert.ToInt32(row[19]) != 7)
                {
                    positivoc = positivoc + Convert.ToDecimal(row[7]);
                }

                else if (Convert.ToInt32(row[14]) == 2 && Convert.ToBoolean(row[3]) == false && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 7)
                {
                    negativoc = negativoc + Convert.ToDecimal(row[7]);
                }

                if (Convert.ToInt32(row[14]) == 4 && Convert.ToBoolean(row[3]) == true && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 6 && Convert.ToInt32(row[19]) != 7)
                {
                    positivot = positivot + Convert.ToDecimal(row[7]);
                }

                else if (Convert.ToInt32(row[14]) == 4 && Convert.ToBoolean(row[3]) == false && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 7)
                {
                    negativot = negativot + Convert.ToDecimal(row[7]);
                }


                if (Convert.ToInt32(row[14]) == 3 && Convert.ToBoolean(row[3]) == true && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 6 && Convert.ToInt32(row[19]) != 7)
                {
                    positivotr = positivotr + Convert.ToDecimal(row[7]);
                }

                else if (Convert.ToInt32(row[14]) == 3 && Convert.ToBoolean(row[3]) == false && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 7)
                {
                    negativotr = negativotr + Convert.ToDecimal(row[7]);
                }

                ////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////

                if (Convert.ToInt32(row[14]) == 1 && Convert.ToBoolean(row[3]) == true && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 6 && Convert.ToInt32(row[19]) != 7)
                {
                    positivoes = positivoes + Convert.ToDecimal(row[7]);
                }

                else if (Convert.ToInt32(row[14]) == 1 && Convert.ToBoolean(row[3]) == false && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 7 && Convert.ToInt32(row[19]) != 6)
                {
                    negativoes = negativoes + Convert.ToDecimal(row[7]);
                }

                if (Convert.ToInt32(row[14]) == 2 && Convert.ToBoolean(row[3]) == true && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 6 && Convert.ToInt32(row[19]) != 7)
                {
                    positivocs = positivocs + Convert.ToDecimal(row[7]);
                }

                else if (Convert.ToInt32(row[14]) == 2 && Convert.ToBoolean(row[3]) == false && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 7 && Convert.ToInt32(row[19]) != 6)
                {
                    negativocs = negativocs + Convert.ToDecimal(row[7]);
                }

                if (Convert.ToInt32(row[14]) == 4 && Convert.ToBoolean(row[3]) == true && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 6 && Convert.ToInt32(row[19]) != 7)
                {
                    positivots = positivots + Convert.ToDecimal(row[7]);
                }

                else if (Convert.ToInt32(row[14]) == 4 && Convert.ToBoolean(row[3]) == false && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 7 && Convert.ToInt32(row[19]) != 6)
                {
                    negativots = negativots + Convert.ToDecimal(row[7]);
                }

                if (Convert.ToInt32(row[14]) == 3 && Convert.ToBoolean(row[3]) == true && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 6 && Convert.ToInt32(row[19]) != 7)
                {
                    positivotsr = positivotsr + Convert.ToDecimal(row[7]);
                }

                else if (Convert.ToInt32(row[14]) == 3 && Convert.ToBoolean(row[3]) == false && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 7 && Convert.ToInt32(row[19]) != 6)
                {
                    negativotsr = negativotsr + Convert.ToDecimal(row[7]);
                }

            }

            resultadoe = positivoe - negativoe;
            resultadoes = positivoes - negativoes;
            lblCobradoEfectivo.Text = resultadoes.ToString();
            lblRetiradoEfectivo.Text = resultadoe.ToString();

            resultadoc = positivoc - negativoc;
            resultadocs = positivocs - negativocs;
            lblCobradoCheque.Text = resultadocs.ToString();
            lblRetiradoCheque.Text = resultadoc.ToString();
            
            resultadot = positivot - negativot;
            resultadots = positivots - negativots;
            lblCobradoTarjeta.Text = resultadots.ToString();
            lblRetiradoTarjeta.Text = resultadot.ToString();

            resultadotr = positivotr - negativotr;
            resultadotsr = positivotsr - negativotsr;
            label9.Text = resultadotsr.ToString();
            label10.Text = resultadotr.ToString();


            dtcombo1 = await q.GETPayMethod("/api/PayMethod");
            if (dtcombo1 != null)
            {
                foreach (DataRow row in dtcombo1.Rows)
                {
                    separadas = row[0].ToString().Split('/');
                    if (separadas[0].ToString() == "error")
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                        mensaje.ShowDialog();
                        dtcombo1.Rows.Clear();
                        return;
                    }

                }
            }

            else
            {
                mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                mensaje.ShowDialog();
            }

            cargarmetodosdepago();
            loading.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
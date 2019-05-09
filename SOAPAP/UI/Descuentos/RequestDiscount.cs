using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOAPAP.Enums;
using SOAPAP.Model;
using SOAPAP.Properties;
using SOAPAP.Services;
using SOAPAP.Tools;
using SOAPAP.UI.Visualizador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI.Descuentos
{
    public partial class RequestDiscount : Form
    {
        public readonly FirebaseClient firebase = new FirebaseClient("https://siscom-notifications.firebaseio.com/");
        private decimal Total { get; set; }
        private bool SelectImage { get; set; } = false;
        private string FilePath { get; set; }
        private List<Model.Debt> pDebts { get; set; }
        private List<Model.OrderSale> pOrderSale { get; set; }
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        public Image Image { get; set; }
        private HttpContent content;
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;
       
        public RequestDiscount(decimal Total, List<Model.Debt> Debts)
        {
            InitializeComponent();
            this.Total = Total;
            this.pDebts = Debts;
            Requests = new RequestsAPI(UrlBase);
        }

        public RequestDiscount(decimal Total, List<Model.OrderSale> OrderSale)
        {
            InitializeComponent();
            this.Total = Total;
            this.pOrderSale = OrderSale;
            Requests = new RequestsAPI(UrlBase);
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void centraX(Control padre, Control hijo)
        {
            int x = 0;
            x = (padre.Width / 2) - (hijo.Width / 2);
            hijo.Location = new System.Drawing.Point(x, hijo.Location.Y);
        }

        private void RequestDiscount_Load(object sender, EventArgs e)
        {
            txtFolio.Text = Shuffler.GetFolio();
            txtAmount.Text = Total.ToString("c2");
            cmbTypeDescount.SelectedIndex = 0;
            //centraX(pnlFolio, lblFolioGenerate);
        }

        private void CmbTypeDescount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cmbox = (ComboBox)sender;
            switch (cmbox.SelectedIndex)
            {
                case 1:
                    lblAmountDiscount.Visible = true;
                    txtAmountDiscount.Visible = true;
                    lblAmountDiscount.Text = "Monto a Descontar";
                    break;
                case 2:
                    lblAmountDiscount.Visible = true;
                    txtAmountDiscount.Visible = true;
                    lblAmountDiscount.Text = "Porcentaje a Descontar";
                    break;
                default:
                    lblAmountDiscount.Visible = false;
                    txtAmountDiscount.Visible = false;
                    break;
            }
            
        }

        private void TxtAmountDiscount_KeyPress(object sender, KeyPressEventArgs e)
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

        private async void BtnSend_Click(object sender, EventArgs e)
        {
            DiscountAuthorization discountAuthorization;
            loading = new Loading();
            loading.Show(this);

            if (ValidationManager.HasValidationErrors(this.Controls))
            {
                loading.Close();
                return;
            }
            if (!ValidationDiscount())
            {
                loading.Close();
                return;
            }
            if (!SelectImage)
            {
                loading.Close();
                mensaje = new MessageBoxForm(Variables.titleprincipal, "Debe seleccionar el archivo de justificación del descuento para poder continuar", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                discountAuthorization = new DiscountAuthorization()
                {
                    Folio = txtFolio.Text,
                    Amount = Convert.ToDecimal(Regex.Replace(txtAmount.Text, @"[^\d.]", "")),
                    RequestDate = DateTime.Now.ToLocalTime(),
                    ExpirationDate = DateTime.Now.ToLocalTime().AddDays(1),
                    BranchOffice = Variables.Configuration.Terminal.BranchOffice.Name,
                    AmountDiscount = cmbTypeDescount.SelectedIndex == 1 ? Convert.ToDecimal(txtAmountDiscount.Text) :
                                     cmbTypeDescount.SelectedIndex == 2 ? ((Convert.ToDecimal(txtAmountDiscount.Text) * Total) / 100) :
                                     0,
                    Type = cmbTypeDescount.SelectedIndex == 1 ? "TDI01" : "TDI02",
                    Status = "EDE01",
                    Observation = txtObservations.Text,
                    DiscountPercentage = cmbTypeDescount.SelectedIndex == 1 ? (short)((Convert.ToDecimal(txtAmountDiscount.Text) / Total) * 100) :
                                        cmbTypeDescount.SelectedIndex == 2 ? (short)Convert.ToDecimal(txtAmountDiscount.Text) :
                                        Convert.ToInt16(0),
                    UserRequestId = Variables.LoginModel.User,
                };
                if (Variables.Agreement != null)
                {
                    discountAuthorization.Account = Variables.Agreement.Account;
                    //Variables.Agreement.Debts.ToList().ForEach(x =>
                    //{
                    //    discountAuthorization.DiscountAuthorizationDetails.Add(new DiscountAuthorizationDetail
                    //    {
                    //        DebtId = x.Id,
                    //        OrderSaleId = 0,
                    //    });
                    //});
                    pDebts.ToList().ForEach(x =>
                    {
                        discountAuthorization.DiscountAuthorizationDetails.Add(new DiscountAuthorizationDetail
                        {
                            DebtId = x.Id,
                            OrderSaleId = 0,
                        });
                    });
                }
                else
                {
                    discountAuthorization.Account = Variables.OrderSale.Folio;
                    pOrderSale.ToList().ForEach(x =>
                    {
                        discountAuthorization.DiscountAuthorizationDetails.Add(new DiscountAuthorizationDetail
                        {
                            DebtId = 0,
                            OrderSaleId = x.Id,
                        });
                    });
                }

                StringContent @string = new StringContent(JsonConvert.SerializeObject(discountAuthorization), Encoding.UTF8, "application/json");
                var resultDiscount= await Requests.UploadImageToServer("/api/DiscountAuthorizations", Variables.LoginModel.Token, FilePath, @string);
               
                if (resultDiscount.Contains("error"))
                {
                    loading.Close();
                    mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultDiscount).error, TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    int id = Convert.ToInt32(JObject.Parse(resultDiscount)["id"].ToString());
                    string fileName = JObject.Parse(resultDiscount)["fileName"].ToString();

                    discountAuthorization.Id = id;
                    discountAuthorization.FileName = fileName;

                    FirebaseObject<PushNotification> @object = await firebase
                                                                 .Child("DiscountAuthorization")
                                                                 .PostAsync(new PushNotification()
                                                                 {
                                                                     RequestDate = DateTime.Now.ToLocalTime(),
                                                                     AuthorizationDiscountId = discountAuthorization.Id,
                                                                     UserRequestId = Variables.LoginModel.User,
                                                                     BranchOffice = Variables.Configuration.Terminal.BranchOffice.Name,
                                                                     IsReply = false,
                                                                     Status = Enum.GetName(typeof(TypeStatus), TypeStatus.Activo),
                                                                     Account = discountAuthorization.Account,
                                                                     ExpirationDate = DateTime.Now.ToLocalTime().AddDays(1),
                                                                     UserResponseId = string.Empty,
                                                                     Observation = discountAuthorization.Observation
                                                                 }, true);
                    string key = @object.Key;
                    discountAuthorization.KeyFirebase = key;
                    content = new StringContent(JsonConvert.SerializeObject(discountAuthorization), Encoding.UTF8, "application/json");
                    var resultUpdateDiscount = await Requests.SendURIAsync($"/api/DiscountAuthorizations/{discountAuthorization.Id}", HttpMethod.Put, Variables.LoginModel.Token, content);

                    if (resultUpdateDiscount.Contains("error"))
                    {
                        loading.Close();
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultDiscount).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        loading.Close();
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "La solicitud de descuento se ha enviado satisfactoriamente, verificar en área de notificaciones la respuesta", TypeIcon.Icon.Success);
                        result = mensaje.ShowDialog();
                        this.Close();
                    }
                } 
            }
            
        }

        private void CmbTypeDescount_Validating(object sender, CancelEventArgs e)
        {
            var cmbx = sender as ComboBox;
            if (cmbx.SelectedIndex == 0)
            {
                errorProvider1.SetError(cmbTypeDescount, "Seleccionar Tipo de descuento");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(cmbTypeDescount, "");
            }
        }

        private void TxtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFolio.Text))
            {
                errorProvider1.SetError(txtFolio, "El Folio es requerido");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(txtFolio, "");
        }

        private void TxtAmount_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                errorProvider1.SetError(txtAmount, "El Monto es requerido");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(txtAmount, "");
        }

        private void TxtAmountDiscount_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAmountDiscount.Text))
            {
                errorProvider1.SetError(txtAmountDiscount, "El Monto de descuento es requerido");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(txtAmountDiscount, "");
        }

        private void TxtObservations_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtObservations.Text))
            {
                errorProvider1.SetError(txtObservations, "La decripción de descuento es requerido");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(txtObservations, "");
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Files | *.pdf; *.jpg; *.jpeg; *.png";
            open.Title = "Seleccionar Archivo";

            if(open.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(open.FileName).Contains("pdf"))
                {
                    Preview preview = new Preview(open.FileName);
                    preview.ShowDialog();
                    pcbPreview.BackgroundImage = Resources.pdfF;
                    pcbPreview.BackgroundImageLayout = ImageLayout.Center;
                    FilePath = open.FileName;
                }
                else
                {
                    pcbPreview.SizeMode = PictureBoxSizeMode.StretchImage;
                    pcbPreview.Image = new Bitmap(open.FileName);
                    FilePath = open.FileName;
                }
               
                SelectImage = true;
                btnRemove.Enabled = true;
            }
            else
            {
                btnRemove.Enabled = false;
                SelectImage = false;
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            pcbPreview.Image = null;
            pcbPreview.Update();
            pcbPreview.BackgroundImage = Resources.folder;
            pcbPreview.BackgroundImageLayout = ImageLayout.Center;
            SelectImage = false;
            btnRemove.Enabled = false;
        }

        private bool ValidationDiscount()
        {
            switch (cmbTypeDescount.SelectedIndex)
            {
                case 1:
                    if (Convert.ToDecimal(txtAmountDiscount.Text) > (Total - 1))
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto del descuento no puede ser mayor al monto de deuda, favor de verificar", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        return false;
                    }
                    else return true;
                case 2:
                    if (Convert.ToDecimal(txtAmountDiscount.Text) > 99)
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "El porcentaje del descuento no puede ser mayor al 99%, favor de verificar", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        return false;
                    }
                    else return true;
                default:
                    return false;
            }
        }
    }

    public class Shuffler
    {
        /// <summary>Creates the shuffler with a <see cref="MersenneTwister"/> as the random number generator.</summary>

        public Shuffler()
        {
            _rng = new Random();
        }

        /// <summary>Shuffles the specified array.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="array">The array to shuffle.</param>

        public void Shuffle<T>(IList<T> array)
        {
            for (int n = array.Count; n > 1;)
            {
                int k = _rng.Next(n);
                --n;
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        public static string GetFolio()
        {
            Shuffler shuffler = new Shuffler();
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            shuffler.Shuffle(list);
            return string.Join("", list.ToArray());
        }

        private System.Random _rng;
    }
}

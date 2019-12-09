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
using System.Globalization;
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
        public readonly FirebaseClient firebase = new FirebaseClient(Variables.Configuration.StringURLFirebase);
        private decimal Total { get; set; }
        private bool SelectImage { get; set; } = false;
        private string FilePath { get; set; }
        private List<Model.Debt> pDebts { get; set; }
        private List<Model.OrderSale> pOrderSale { get; set; }
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        public Image Image { get; set; }
        private HttpContent content;
        private int IdDiscount { get; set; }
        public bool ClickNotification { get; set; }
        private List<DiscountAuthorization> authorization;
        private DataTable Table = new DataTable();
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
            //if (!SelectImage)
            //{
            //    loading.Close();
            //    mensaje = new MessageBoxForm(Variables.titleprincipal, "Debe seleccionar el archivo de justificación del descuento para poder continuar", TypeIcon.Icon.Cancel);
            //    result = mensaje.ShowDialog();
            //}
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
                    AccountAdjusted = "0"
                };
                if (Variables.Agreement != null)
                {
                    discountAuthorization.Account = Variables.Agreement.Account;
 
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
                                                                     Observation = discountAuthorization.Observation,
                                                                     IsView = false
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
            if(Total <= 0)
            {
                mensaje = new MessageBoxForm(Variables.titleprincipal, "No se puede aplicar descuento, el total es incorrecto.", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
                return false;
            }         
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void tbcDescuento_Selected(object sender, TabControlEventArgs e)
        {
            string account = string.Empty;
            loading = new Loading();
            loading.Show(this);
            var results = await Requests.SendURIAsync(String.Format("/api/DiscountAuthorizations/List/{0}/{1}", Variables.LoginModel.User, DateTime.Now.ToString("yyyy-MM-dd")), HttpMethod.Get, Variables.LoginModel.Token);
            if (results.Contains("error"))
            {
                try
                {
                    mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(results).error, TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    loading.Close();
                }
                catch (Exception)
                {
                    mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                    loading.Close();
                }
            }
            else
            {
                loading.Close();
                authorization = JsonConvert.DeserializeObject<List<DiscountAuthorization>>(results);
                var DiscountAuth = authorization.Select(x => new DiscountAuthorizationVM
                {
                    Id = x.Id,
                    Cuenta = x.Account,
                    Monto_Original = x.Amount,
                    Monto_Ajustado = x.Amount - x.AmountDiscount,
                    Descuento = x.AmountDiscount,
                    Fecha_Solicitud = String.Format("{0:G}", x.RequestDate),
                    Autorizacion = x.AuthorizationDate.ToShortDateString(),
                    Sucursal = x.BranchOffice,
                    Porcentaje = x.DiscountPercentage,
                    Archivo = x.FileName,
                    Folio = x.Folio,
                    Autorizo = x.NameUserResponse,
                    Observaciones = x.ObservationResponse,
                    Estatus = x.Status == "EDE01" ? "Solicitado" :
                                x.Status == "EDE02" ? "Autorizado" :
                                x.Status == "EDE03" ? "Cancelado" :
                                "Rechazado",
                    Ajuste_Cuenta = x.AccountAdjusted
                }).ToList();

                Table = ConvertToDataTable<DiscountAuthorizationVM>(DiscountAuth);
                BindingSource source = new BindingSource();
                source.DataSource = Table;

                dgvDiscounts.AutoGenerateColumns = true;
                dgvDiscounts.Columns.Clear();
                dgvDiscounts.DataSource = source;

                for (int i = 0; i < dgvDiscounts.Columns.Count; i++)
                {
                    dgvDiscounts.Columns[i].DataPropertyName = Table.Columns[i].ColumnName;
                    dgvDiscounts.Columns[i].HeaderText = Table.Columns[i].Caption.Replace("_", " ");
                }

                dgvDiscounts.Refresh();

                dgvDiscounts.Columns[0].Visible = false;
                dgvDiscounts.Columns[1].Visible = false;
                dgvDiscounts.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDiscounts.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[5].Visible = false;
                dgvDiscounts.Columns[6].Visible = false;
                dgvDiscounts.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDiscounts.Columns[7].DefaultCellStyle.Format = "c2";
                dgvDiscounts.Columns[7].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
                dgvDiscounts.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDiscounts.Columns[9].Visible = false;
                dgvDiscounts.Columns[10].Visible = false;
                dgvDiscounts.Columns[11].Visible = false;
                dgvDiscounts.Columns[12].Visible = false;
                dgvDiscounts.Columns[13].Visible = false;
                dgvDiscounts.Columns[14].Visible = false;

                foreach (DataGridViewRow item in dgvDiscounts.Rows)
                {
                    if (ClickNotification)
                    {
                        if ((item.Cells[0].FormattedValue.ToString() != "" ? Convert.ToInt32(item.Cells[0].FormattedValue.ToString()) : 0) == IdDiscount)
                        {
                            EnsureVisibleRow(dgvDiscounts, item.Index);
                            dgvDiscounts.Refresh();
                            account = item.Cells[2].FormattedValue.ToString();
                            dgvDiscounts.Rows[item.Index].Selected = true;
                        }
                    }

                    switch (item.Cells[3].FormattedValue.ToString())
                    {
                        case "Solicitado":
                            item.Cells[3].Style.BackColor = Color.FromArgb(43, 187, 173);
                            item.Cells[3].Style.ForeColor = Color.White;
                            item.Cells[3].Style.Font = new Font("Century Gothic", 8, FontStyle.Bold);
                            break;
                        case "Autorizado":
                            item.Cells[3].Style.BackColor = Color.FromArgb(66, 133, 244);
                            item.Cells[3].Style.ForeColor = Color.White;
                            item.Cells[3].Style.Font = new Font("Century Gothic", 8, FontStyle.Bold);
                            break;
                        case "Cancelado":
                            item.Cells[3].Style.BackColor = Color.FromArgb(204, 0, 0);
                            item.Cells[3].Style.ForeColor = Color.White;
                            item.Cells[3].Style.Font = new Font("Century Gothic", 8, FontStyle.Bold);
                            break;
                    }
                }
                if (ClickNotification)
                {
                    SOAPAP.Base formBase = this.Owner as SOAPAP.Base;
                    formBase.RemoveItemSelected(account);
                }
            }
        }

        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        private void EnsureVisibleRow(DataGridView view, int rowToShow)
        {
            if (rowToShow >= 0 && rowToShow < view.RowCount)
            {
                var countVisible = view.DisplayedRowCount(false);
                var firstVisible = view.FirstDisplayedScrollingRowIndex;
                if (rowToShow < firstVisible)
                {
                    view.FirstDisplayedScrollingRowIndex = rowToShow;
                }
                else if (rowToShow >= firstVisible + countVisible)
                {
                    view.FirstDisplayedScrollingRowIndex = rowToShow - countVisible + 1;
                }
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

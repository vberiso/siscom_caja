using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Model;
using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI.Descuentos
{
    public partial class DetalleDescuentos : Form
    {
        private Form loading;
        private Form mensaje;
        private RequestsAPI Requests = null;
        private DataTable Table = new DataTable();
        private DialogResult result = new DialogResult();
        private string UrlBase = Properties.Settings.Default.URL;
        private List<SOAPAP.Model.DiscountAuthorizationVM> authorization;
        private int IdDiscount { get; set; }
        public bool ClickNotification { get; set; }

        public DetalleDescuentos()
        {
            InitializeComponent();
            Requests = new RequestsAPI(UrlBase);
        }

        public DetalleDescuentos(int IdDiscount, bool ClickNotification)
        {
            InitializeComponent();
            this.IdDiscount = IdDiscount;
            Requests = new RequestsAPI(UrlBase);
            this.ClickNotification = ClickNotification;
        }

        public DetalleDescuentos(bool ClickNotification)
        {
            InitializeComponent();
            Requests = new RequestsAPI(UrlBase);
            this.ClickNotification = ClickNotification;
        }

        private async void DetalleDescuentos_Load(object sender, EventArgs e)
        {
            string account = string.Empty;
            loading = new Loading();
            loading.Show(this);
            var results = await Requests.SendURIAsync(String.Format("/api/DiscountAuthorizations/List/{0}/{1}", Variables.LoginModel.User,DateTime.Now.ToString("yyyy-MM-dd")), HttpMethod.Get, Variables.LoginModel.Token);
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
                authorization = JsonConvert.DeserializeObject<List<SOAPAP.Model.DiscountAuthorizationVM>>(results);
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
                dgvDiscounts.Columns[2].Width = 75;
                dgvDiscounts.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDiscounts.Columns[3].Width = 75;
                dgvDiscounts.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[4].Width = 300;
                dgvDiscounts.Columns[5].Visible = false;
                dgvDiscounts.Columns[6].Visible = false;
                dgvDiscounts.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[7].Width = 120;
                dgvDiscounts.Columns[7].DefaultCellStyle.Format = "c2";
                dgvDiscounts.Columns[7].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
                dgvDiscounts.Columns[8].Visible = true;
                dgvDiscounts.Columns[9].Visible = true;
                dgvDiscounts.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDiscounts.Columns[11].Visible = false;
                dgvDiscounts.Columns[12].Visible = false;
                dgvDiscounts.Columns[13].Visible = false;
                dgvDiscounts.Columns[14].Visible = false;
                dgvDiscounts.Columns[15].Visible = false;

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
                            item.Cells[3].Style.BackColor = Color.FromArgb(7, 96, 125, 139);
                            item.Cells[3].Style.ForeColor = Color.White;
                            item.Cells[3].Style.Font = new Font("Century Gothic", 8, FontStyle.Bold);
                            break;
                        case "Autorizado":
                            item.Cells[3].Style.BackColor = Color.FromArgb(7,76, 175, 80);
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

        private void DgvDiscounts_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var account = dgvDiscounts.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            if(dgvDiscounts.Rows[e.RowIndex].Cells[3].FormattedValue.ToString() != "Autorizado")
            {
                mensaje = new MessageBoxForm(Variables.titleprincipal, "El descuento aún no ha sido autorizado", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                if (account.Contains("-"))
                    Variables.cuenta = dgvDiscounts.Rows[e.RowIndex].Cells[14].FormattedValue.ToString();
                else
                    Variables.cuenta = account;

                SOAPAP.Base @base = this.Owner as SOAPAP.Base;
                @base.ShowForm("SOAPAP.UI", "Cobro");
                this.Close();
            }
        }

        private async void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            if(dgvDiscounts.Rows.Count > 0)
            {
                dgvDiscounts.DataSource = null;
                dgvDiscounts.Refresh();
            }
            var results = await Requests.SendURIAsync(String.Format("/api/DiscountAuthorizations/List/{0}/{1}", Variables.LoginModel.User, dateTimePicker1.Value.ToString("yyyy-MM-dd")), HttpMethod.Get, Variables.LoginModel.Token);
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
                authorization = JsonConvert.DeserializeObject<List<SOAPAP.Model.DiscountAuthorizationVM>>(results);
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
                    Ajuste_Cuenta = x.AccountAdjusted,
                    Cobrado = x.IsApplied
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
                dgvDiscounts.Columns[2].Width = 75;
                dgvDiscounts.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDiscounts.Columns[3].Width = 75;
                dgvDiscounts.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[4].Width = 270;
                dgvDiscounts.Columns[5].Visible = false;
                dgvDiscounts.Columns[6].Visible = false;
                dgvDiscounts.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[7].Width = 120;
                dgvDiscounts.Columns[7].DefaultCellStyle.Format = "c2";
                dgvDiscounts.Columns[7].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
                dgvDiscounts.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[8].Width = 120;
                dgvDiscounts.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[9].Width = 80;
                dgvDiscounts.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDiscounts.Columns[11].Visible = false;
                dgvDiscounts.Columns[12].Visible = false;
                dgvDiscounts.Columns[13].Visible = false;
                dgvDiscounts.Columns[14].Visible = false;
                dgvDiscounts.Columns[15].Visible = false;

                foreach (DataGridViewRow item in dgvDiscounts.Rows)
                {
                    switch (item.Cells[3].FormattedValue.ToString())
                    {
                        case "Solicitado":
                            item.Cells[3].Style.BackColor = Color.FromArgb(255, 96, 125, 139);
                            item.Cells[3].Style.ForeColor = Color.White;
                            item.Cells[3].Style.Font = new Font("Century Gothic", 8, FontStyle.Bold);
                            break;
                        case "Autorizado":
                            item.Cells[3].Style.BackColor = Color.FromArgb(255, 0, 150, 136);
                            item.Cells[3].Style.ForeColor = Color.White;
                            item.Cells[3].Style.Font = new Font("Century Gothic", 8, FontStyle.Bold);
                            break;
                        case "Cancelado":
                            item.Cells[3].Style.BackColor = Color.FromArgb(255, 244, 67, 54);
                            item.Cells[3].Style.ForeColor = Color.White;
                            item.Cells[3].Style.Font = new Font("Century Gothic", 8, FontStyle.Bold);
                            break;
                    }
                }
            }
        }
    }
    public partial class DiscountAuthorizationVM
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public string Cuenta { get; set; }
        public string Estatus { get; set; }
        public string Autorizo { get; set; }
        public string Sucursal { get; set; }
        public decimal Monto_Original { get; set; }
        public decimal Monto_Ajustado { get; set; }
        public string Fecha_Solicitud { get; set; }
        public bool Cobrado { get; set; }
        public string Observaciones { get; set; }
        public string Autorizacion { get; set; }
        public decimal Descuento { get; set; }
        public Int16 Porcentaje { get; set; }
        public string Archivo { get; set; }
        public string Ajuste_Cuenta { get; set; }
    }
}

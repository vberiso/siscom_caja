using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Model;
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
        private List<DiscountAuthorization> authorization;
        private int IdDiscount { get; set; }
        public DetalleDescuentos(int IdDiscount)
        {
            InitializeComponent();
            this.IdDiscount = IdDiscount;
            Requests = new RequestsAPI(UrlBase);
        }

        private async void DetalleDescuentos_Load(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            var results = await Requests.SendURIAsync(String.Format("/api/DiscountAuthorizations/List/{0}", Variables.LoginModel.User), HttpMethod.Get, Variables.LoginModel.Token);
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
                                "Rechazado"
                }).ToList();

                Table = ConvertToDataTable<DiscountAuthorizationVM>(DiscountAuth);
                BindingSource source = new BindingSource();
                source.DataSource = Table;

                dgvDiscounts.AutoGenerateColumns = true;
                dgvDiscounts.Columns.Clear();
                dgvDiscounts.DataSource = source;

                for (int i = 0; i < dgvDiscounts.Columns.Count; i++)
                {
                    dgvDiscounts.Columns[i].DataPropertyName = Table.Columns[i].ColumnName.Replace("_", " ");
                    dgvDiscounts.Columns[i].HeaderText = Table.Columns[i].Caption.Replace("_", " ");
                }

                dgvDiscounts.Refresh();

                //dgvDiscounts.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[0].Visible = false;
                dgvDiscounts.Columns[1].Visible = false;
                dgvDiscounts.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[2].Width = 70;
                dgvDiscounts.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[3].Width = 70;
                dgvDiscounts.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvDiscounts.Columns[4].Width = 250;
                dgvDiscounts.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                foreach (DataGridViewRow item in dgvDiscounts.Rows)
                {
                    if((item.Cells[0].FormattedValue.ToString() != "" ? Convert.ToInt32(item.Cells[0].FormattedValue.ToString()): 0) == IdDiscount)
                    {
                        //dgvDiscounts.FirstDisplayedScrollingRowIndex = item.Index;
                        //dgvDiscounts.Refresh();
                        EnsureVisibleRow(dgvDiscounts, item.Index);
                        dgvDiscounts.Refresh();
                    }
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
        public string Observaciones { get; set; }
        public string Fecha_Solicitud { get; set; }
        public string Autorizacion { get; set; }
        public decimal Descuento { get; set; }
        public Int16 Porcentaje { get; set; }
        public string Archivo { get; set; }
    }
}

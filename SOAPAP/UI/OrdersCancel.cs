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
    public partial class OrdersCancel : Form
    {
        Form loading;
        Form mensaje;
        DataTable Table = new DataTable();
        DialogResult result = new DialogResult();
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;

        public OrdersCancel()
        {
            InitializeComponent();
            Requests = new RequestsAPI(UrlBase);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OrdersCancel_Load(object sender, EventArgs e)
        {
            centraX(pnpTiltle, pnlCalendar);
            //Cargar();
        }

        private void centraX(Control padre, Control hijo)
        {
            int x = 0;
            x = (padre.Width / 2) - (hijo.Width / 2);
            hijo.Location = new System.Drawing.Point(x, hijo.Location.Y);
        }

        private void FindOrders_Resize(object sender, EventArgs e)
        {
            centraX(pnpTiltle, pnlCalendar);
        }

        //private async void Cargar()
        //{
        //    loading = new Loading();
        //    loading.Show(this);
        //    var results = await Requests.SendURIAsync(String.Format("/api/OrderSales/FindAllOrdersByDate/{0}", dateTimePicker1.Value.ToString("yyyy-MM-dd")), HttpMethod.Get, Variables.LoginModel.Token);
        //    if (results.Contains("error"))
        //    {
        //        try
        //        {
        //            mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(results).error, TypeIcon.Icon.Cancel);
        //            result = mensaje.ShowDialog();
        //            loading.Close();
        //        }
        //        catch (Exception)
        //        {
        //            mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador", TypeIcon.Icon.Cancel);
        //            result = mensaje.ShowDialog();
        //            loading.Close();
        //        }
        //    }
        //    else
        //    {

        //        var orderSales = JsonConvert.DeserializeObject<List<OrderSale>>(results);
        //        var dataOrder = orderSales.Select(x => new SearchOrders
        //        {
        //            Id = x.Id,
        //            Folio = x.Folio,
        //            Monto = x.OrderSaleDetails.Sum(y => y.Amount + y.Tax),
        //            Estatus = x.DescriptionStatus,
        //            Tipo = x.DescriptionType,
        //            Expedicion = x.DateOrder.ToLocalTime().ToShortDateString(),
        //            Expiracion = x.ExpirationDate.ToLocalTime().ToShortDateString(),
        //            Contribuyente = x.TaxUser.Name
        //        }).ToList();

        //        Table = ConvertToDataTable<SearchOrders>(dataOrder);

        //        BindingSource source = new BindingSource();
        //        source.DataSource = Table;

        //        dgvOrders.AutoGenerateColumns = true;
        //        dgvOrders.Columns.Clear();
        //        dgvOrders.DataSource = source;
        //        dgvOrders.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);

        //        for (int i = 0; i < dgvOrders.Columns.Count; i++)
        //        {
        //            dgvOrders.Columns[i].DataPropertyName = Table.Columns[i].ColumnName;
        //            dgvOrders.Columns[i].HeaderText = Table.Columns[i].Caption;
        //        }
        //        dgvOrders.Columns[0].Visible = false;
        //        dgvOrders.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
        //        dgvOrders.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
        //        dgvOrders.Columns[2].DefaultCellStyle.Format = "c2";
        //        dgvOrders.Columns[2].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
        //        dgvOrders.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
        //        dgvOrders.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
        //        dgvOrders.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
        //        dgvOrders.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
        //        dgvOrders.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        //        dgvOrders.Refresh();
        //        loading.Close();
        //    }
        //}

    }
}

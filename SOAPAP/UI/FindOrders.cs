using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Model;
using SOAPAP.Services;
using SOAPAP.UI.Email;
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
using System.Xml;

namespace SOAPAP.UI
{
    public partial class FindOrders : Form
    {
        Form loading;
        Form mensaje;
        DataTable Table = new DataTable();
        DialogResult result = new DialogResult();
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;

        public FindOrders()
        {
            InitializeComponent();
            Requests = new RequestsAPI(UrlBase);
        }

        private void FindOrders_Load(object sender, EventArgs e)
        {
            centraX(pnpTiltle, pnlCalendar);
            Cargar();
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

        private async void Cargar(string  folio =null)
        {
            loading = new Loading();
            loading.Show(this);
            string url = "";
            if (folio == null)
            {
                url = String.Format("/api/OrderSales/FindAllOrdersByDate/{0}", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            }
            else
            {
                url = String.Format("/api/OrderSales/All/Folio/{0}", folio);

            }
            var results = await Requests.SendURIAsync(url, HttpMethod.Get, Variables.LoginModel.Token);
            if (results == "")
            {
                mensaje = new MessageBoxForm("Error", "No se encontro resultado para el folio: "+ folio, TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
                loading.Close();
                return;
            }
            else if (results.Contains("error"))
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
                List<OrderSale> oList = null;
                if (folio == null)
                    oList = JsonConvert.DeserializeObject<List<OrderSale>>(results);
                else
                {
                    oList = new List<OrderSale>();
                    oList.Add(JsonConvert.DeserializeObject<OrderSale>(results));
                }
                var orderSales = oList;

                var dataOrder = orderSales.Select(x => new SearchOrders
                {
                    Id = x.Id,
                    Folio = x.Folio,
                    Monto = x.OrderSaleDetails.Sum(y => y.Amount + y.Tax),
                    Estatus = x.DescriptionStatus,
                    Tipo = x.DescriptionType,
                    Expedicion = x.DateOrder.ToLocalTime().ToShortDateString(),
                    Expiracion = x.ExpirationDate.ToLocalTime().ToShortDateString(),
                    Contribuyente = x.TaxUser.Name
                }).ToList();

                Table = ConvertToDataTable<SearchOrders>(dataOrder);

                BindingSource source = new BindingSource();
                source.DataSource = Table;

                dgvOrders.AutoGenerateColumns = true;
                dgvOrders.Columns.Clear();
                dgvOrders.DataSource = source;
                dgvOrders.DefaultCellStyle.Font = new Font("Microsoft Sans Serif",8, FontStyle.Regular);

                for (int i = 0; i < dgvOrders.Columns.Count; i++)
                {
                    dgvOrders.Columns[i].DataPropertyName = Table.Columns[i].ColumnName;
                    dgvOrders.Columns[i].HeaderText = Table.Columns[i].Caption;
                }
                dgvOrders.Columns[0].Visible = false;
                dgvOrders.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvOrders.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvOrders.Columns[2].DefaultCellStyle.Format = "c2";
                dgvOrders.Columns[2].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
                dgvOrders.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvOrders.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvOrders.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvOrders.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                dgvOrders.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DataGridViewButtonColumn sendEmail = new DataGridViewButtonColumn();
                sendEmail.Name = "Email";
                sendEmail.Text = "Eviar";
                if (dgvOrders.Columns["Email"] == null)
                {
                    dgvOrders.Columns.Insert(8, sendEmail);
                }
                dgvOrders.CellClick += dgvOrders_CellClick;
                dgvOrders.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvOrders.Columns[8].Width = 90;

                DataGridViewButtonColumn descragaPDF = new DataGridViewButtonColumn();
                descragaPDF.Name = "PDF";
                descragaPDF.Text = "Descargar";
                if (dgvOrders.Columns["PDF"] == null)
                {
                    dgvOrders.Columns.Insert(9, descragaPDF);
                }
                dgvOrders.CellClick += dgvOrders_CellClickPDF;
                dgvOrders.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvOrders.Columns[9].Width = 120;

                //DataGridViewButtonCell c = (DataGridViewButtonCell)dgvPayment.Rows[e.RowIndex].Cells[0];
                dgvOrders.Refresh();
                loading.Close();
            }
        }

        private async void dgvOrders_CellClickPDF(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 9)
            {
                DataGridViewRow row = this.dgvOrders.Rows[e.RowIndex];
                if (e.ColumnIndex == dgvOrders.Columns["PDF"].Index && e.RowIndex >= 0)
                {
                    loading = new Loading();
                    loading.Show(this);
                    var folio = row.Cells["Folio"].FormattedValue.ToString();
                    var results = await Requests.SendURIAsync(String.Format("/api/Payments/TaxReceipt/{0}", folio), HttpMethod.Get, Variables.LoginModel.Token);
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
                        Payment paymentt = JsonConvert.DeserializeObject<Payment>(results);
                        if (paymentt != null)
                        {
                            if (paymentt.HaveTaxReceipt)
                            {
                                var xmll = paymentt.TaxReceipts.FirstOrDefault();
                                var account = paymentt.Account;
                                if (xmll != null)
                                {
                                    if (xmll.PDFInvoce != null)
                                    {
                                        ExportGridToPDF(xmll.PDFInvoce);
                                    }
                                    else
                                    {
                                        mensaje = new MessageBoxForm(Variables.titleprincipal, "Descarga no disponible, posiblemente este pago no este facturado, para mas información contactarse con el administrador del sistema.", TypeIcon.Icon.Cancel);
                                        result = mensaje.ShowDialog();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private async void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 8)
            {
                DataGridViewRow row = this.dgvOrders.Rows[e.RowIndex];
                if (e.ColumnIndex == dgvOrders.Columns["Email"].Index && e.RowIndex >= 0)
                {
                    loading = new Loading();
                    loading.Show(this);
                    var folio = row.Cells["Folio"].FormattedValue.ToString();
                    var results = await Requests.SendURIAsync(String.Format("/api/Payments/TaxReceipt/{0}",folio), HttpMethod.Get, Variables.LoginModel.Token);
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
                        Payment payment = JsonConvert.DeserializeObject<Payment>(results);
                        if(payment != null)
                        {
                            if (payment.HaveTaxReceipt)
                            {
                                var xml = payment.TaxReceipts.FirstOrDefault();
                                var account = payment.Account;
                                if (xml != null)
                                {
                                    SendEmail email = new SendEmail((xml.Xml.StartsWith("ï»¿") ? xml.Xml.Replace("ï»¿", "") : xml.Xml), account, row.Cells["Contribuyente"].FormattedValue.ToString(), payment.HaveTaxReceipt, xml.PDFInvoce);
                                    email.ShowDialog();
                                }
                                else
                                {
                                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Xml no disponible, posiblemente este pago no este facturado, para mayor información contactarse con el administrador del sistema.", TypeIcon.Icon.Cancel);
                                    result = mensaje.ShowDialog();
                                }
                            }
                        }
                        else
                        {
                            mensaje = new MessageBoxForm(Variables.titleprincipal, "Errro al enviar el CFDI, posiblemente este pago no este facturado para mayor información contactarse con el administrador del sistema.", TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                        }
                        
                    }
                }
            }
               
        }

        private void ExportGridToPDF(byte[] pdf)
        {
            SaveFileDialog SaveXMLFileDialog = new SaveFileDialog();
            SaveXMLFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            SaveXMLFileDialog.FilterIndex = 2;
            SaveXMLFileDialog.RestoreDirectory = true;
            SaveXMLFileDialog.Title = "Exportar PDF de Factura";
            if (SaveXMLFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllBytes(SaveXMLFileDialog.FileName, pdf);
            }
        }
        private void DgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvOrders.Columns[e.ColumnIndex].Name == "Email" && e.RowIndex >= 0)
            {
                DataGridViewButtonCell c = (DataGridViewButtonCell)dgvOrders.Rows[e.RowIndex].Cells[8];
                DataGridViewRow row = this.dgvOrders.Rows[e.RowIndex];
                c.Value = "Enviar CFDI";
            }
            if (e.ColumnIndex >= 0 && this.dgvOrders.Columns[e.ColumnIndex].Name == "PDF" && e.RowIndex >= 0)
            {
                DataGridViewButtonCell c = (DataGridViewButtonCell)dgvOrders.Rows[e.RowIndex].Cells[9];
                DataGridViewRow row = this.dgvOrders.Rows[e.RowIndex];
                c.Value = "Descargar CFDI";
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Cargar();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExportGridToXML(string xml)
        {
            SaveFileDialog SaveXMLFileDialog = new SaveFileDialog();
            SaveXMLFileDialog.Filter = "Xml files (*.xml)|*.xml";
            SaveXMLFileDialog.FilterIndex = 2;
            SaveXMLFileDialog.RestoreDirectory = true;
            SaveXMLFileDialog.Title = "Exportar XML de Factura";
            if (SaveXMLFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(xml);
                    xdoc.Save(System.IO.File.OpenWrite(SaveXMLFileDialog.FileName));
                }
                catch (Exception)
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Por el momento no se puede descargar el xml", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioFecha_CheckedChanged(object sender, EventArgs e)
        {

            //pbBG.Visible = true;
            dateTimePicker1.Visible = true;
            tableLayoutPanel1.Visible = true;
            pbBuscar.Visible = false;
            txtFolioSearch.Visible = false;
            txtFolioSearch.Text = "";
            Cargar();
            
        }

        private void radioFolio_CheckedChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = false;
            //pbBG.Visible = false;
            dateTimePicker1.Visible = false;
            pbBuscar.Visible = true;
            txtFolioSearch.Visible = true;
            

        }

       

        private void txtFolioSearch_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
           
        }

        private void pbBuscar_Click(object sender, EventArgs e)
        {
            if (txtFolioSearch.Text != "")
                Cargar(txtFolioSearch.Text);
            else
            {
                mensaje = new MessageBoxForm("Error", "Ingrese un folio", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
                loading.Close();


            }
        }
    }
    public partial class SearchOrders
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public decimal Monto { get; set; }
        public string Estatus { get; set; }
        public string Tipo { get; set; }
        public string Expedicion { get; set; }
        public string Expiracion { get; set; }
        public string Contribuyente { get; set; }
    }
}

using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOAPAP.Services;
using SOAPAP.Enums;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using SOAPAP.Model;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using System.Drawing.Imaging;
using Humanizer;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Globalization;
using SOAPAP.Tools;
using DevExpress.XtraTreeList.Nodes;

namespace SOAPAP.UI
{
    public partial class Productos : Form
    {
        //public static DataTable dts1 = new DataTable();
        DataTable dts1 = new DataTable();
        Form mensaje;
        public static int vp { get; set; } = 0;
        Form loading;
        string idproducto { get; set; }
        string namesconcept { get; set; }
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        SOAPAP.Model.AgreementProductVM agrements = new SOAPAP.Model.AgreementProductVM();
        string cuenta = String.Empty;
        string cuentap = string.Empty;
        string namesp = string.Empty;
        string rfcp = string.Empty;
        string direccionp = string.Empty;
        TreeView _fieldsTreeCache = new TreeView();

        public Productos()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
            if (Variables.LoginModel.RolName.ToList().Find(x => x.Contains("GenerarOrden")) != null)
            {
                pbBG.Visible = false;
                pictureBox1.Visible = false;
                txtCuenta.Visible = false;
                checkBox1.Checked = true;
                checkBox1.Visible = false;
            }
            //dgvMovimientos.Columns["UNITPRICE"].Visible = false;
        }

        querys q = new querys();
        public static Tariff m { get; set; } = new Tariff();

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        //siscom
        protected void PopulateTreeView(TreeNodeCollection parentNode, string parentID, DataTable folders)
        {
            foreach (DataRow folder in folders.Rows)
            {
                if (folder[2].ToString() == parentID)
                {
                    String key = folder["ID"].ToString();
                    String text = folder["Descripcion"].ToString();
                    TreeNodeCollection newParentNode = parentNode.Add(key, text).Nodes;
                    PopulateTreeView(newParentNode, folder["ID"].ToString(), folders);
                }
            }
        }

        void Total()
        {
            decimal total = 0;
            decimal iva = 0;
            bool havTax = false;
            foreach (DataGridViewRow row in dgvMovimientos.Rows)
            {
                total = total + (decimal)row.Cells[3].Value;
                iva += (decimal)row.Cells[6].Value;
            }
            if (iva <= 0)
                lblIVA.Text = "$0.00";
            else
                lblIVA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", iva);
            //label23.Text = "$" + total;
            lblSubtotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", total);
            label23.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", total + iva);
        }

        public async void cargar(string dato, string name)
        {

            loading = new Loading();
            loading.Show(this);
            int rowIndex = -1;

            try
            {
                m = await q.GETTariff("/api/Products/Tariff/" + dato);
                if (m.type == "TTP04")
                {
                    ModalProduct modal = new ModalProduct();
                    ModalProduct.TypeProduct = 1;
                    ModalProduct.Title = "Importe Dictaminado";
                    ModalProduct.Measure = "";
                    modal.ShowDialog(this);

                    if (!ModalProduct.IsCancel)
                    {
                        DataRow row = dts1.NewRow();
                        row["ID"] = m.productId;
                        row["NOMBRE"] = namesconcept == "" ? GetPath(treeList1.FindNodeByID(treeList1.FocusedNode.Id), "") : namesconcept;
                        //row["TOTAL"] = ModalProduct.Quatity
                        row["TOTAL"] =  ModalProduct.Quatity;
                        row["UNITPRICE"] = ModalProduct.Quatity;
                        row["IVA"] = m.haveTax;
                        if (m.haveTax)
                            row["AMOUNTIVA"] = Math.Round((((decimal)row["TOTAL"] * Convert.ToDecimal(Variables.Configuration.IVA)) / 100), 2);
                        else
                            row["AMOUNTIVA"] = 0;
                        row["CANTIDAD"] = 1;
                        row["UNIDAD"] = m.product.ProductParams.FirstOrDefault().UnitMeasurement;
                        dts1.Rows.Add(row);

                        tarifagrid();
                        Total();
                    }    
                }

                else if (m.type == "TTP02")
                {
                    ModalProduct modal = new ModalProduct();
                    ModalProduct.TypeProduct = 2;
                    ModalProduct.Title = "Costo de Obra";
                    ModalProduct.Measure = "";
                    modal.ShowDialog(this);

                    if (!ModalProduct.IsCancel)
                    {
                        DataRow row = dts1.NewRow();
                        row["ID"] = m.productId;
                        row["NOMBRE"] = namesconcept == "" ? GetPath(treeList1.FindNodeByID(treeList1.FocusedNode.Id), "") : namesconcept;
                        //row["TOTAL"] = (ModalProduct.Quatity * m.percentage) / 100;
                        row["TOTAL"] = (ModalProduct.Quatity * m.percentage) / 100;
                        row["UNITPRICE"] = (ModalProduct.Quatity * m.percentage) / 100;
                        row["IVA"] = m.haveTax;
                        if (m.haveTax)
                            row["AMOUNTIVA"] = Math.Round((((decimal)row["TOTAL"] * Convert.ToDecimal(Variables.Configuration.IVA)) / 100), 2);
                        else
                            row["AMOUNTIVA"] = 0;
                        row["CANTIDAD"] = 1;
                        row["UNIDAD"] = m.product.ProductParams.FirstOrDefault().UnitMeasurement;
                        dts1.Rows.Add(row);
                        tarifagrid();
                        Total();
                    }    
                }

                else if (m.type == "TTP05")
                {
                    ModalProduct modal = new ModalProduct();
                    ModalProduct.TypeProduct = 3;
                    ModalProduct.Title = "Unidades";
                    ModalProduct.Measure = "M², M³, Hrs ...";
                    modal.ShowDialog(this);

                    if (!ModalProduct.IsCancel)
                    {
                        DataRow row = dts1.NewRow();
                        row["ID"] = m.productId;
                        row["NOMBRE"] = namesconcept == "" ? GetPath(treeList1.FindNodeByID(treeList1.FocusedNode.Id), "") : namesconcept;
                        //row["TOTAL"] = ModalProduct.Quatity * Convert.ToDecimal(m.amount);
                        row["TOTAL"] = ModalProduct.Quatity * Convert.ToDecimal(m.amount);
                        row["UNITPRICE"] = Convert.ToDecimal(m.amount);
                        row["IVA"] = m.haveTax;
                        if (m.haveTax)
                            row["AMOUNTIVA"] = Math.Round((((decimal)row["TOTAL"] * Convert.ToDecimal(Variables.Configuration.IVA)) / 100), 2);
                        else
                            row["AMOUNTIVA"] = 0;
                        row["CANTIDAD"] = ModalProduct.Quatity;
                        row["UNIDAD"] = m.product.ProductParams.FirstOrDefault().UnitMeasurement;
                        dts1.Rows.Add(row);
                        tarifagrid();
                        Total();
                    }   
                }

                else if (m.type == "TTP03")
                {
                    ModalProduct modal = new ModalProduct();
                    ModalProduct.TypeProduct = 4;
                    ModalProduct.Title = "Unidad de Medida y Actualización (UMA)";
                    ModalProduct.Measure = "";
                    modal.ShowDialog(this);

                    if (!ModalProduct.IsCancel)
                    {
                        DataRow row = dts1.NewRow();
                        row["ID"] = m.productId;
                        row["NOMBRE"] = namesconcept == "" ? GetPath(treeList1.FindNodeByID(treeList1.FocusedNode.Id), "") : namesconcept;
                        //row["TOTAL"] = Convert.ToDecimal(Variables.Configuration.minimumsalary) * ModalProduct.Quatity;
                        row["TOTAL"] = Convert.ToDecimal(Variables.Configuration.minimumsalary) * ModalProduct.Quatity;
                        row["UNITPRICE"] = Convert.ToDecimal(Variables.Configuration.minimumsalary);
                        row["IVA"] = m.haveTax;
                        decimal amount = Convert.ToDecimal(Variables.Configuration.minimumsalary) * ModalProduct.Quatity;
                        if (m.haveTax)
                            row["AMOUNTIVA"] = Math.Round(((amount * Convert.ToDecimal(Variables.Configuration.IVA)) / 100), 2);
                        else
                            row["AMOUNTIVA"] = 0;
                        row["CANTIDAD"] = 1;
                        row["UNIDAD"] = m.product.ProductParams.FirstOrDefault().UnitMeasurement;
                        dts1.Rows.Add(row);
                        tarifagrid();
                        Total();
                    }  
                }
                else if (m.type == "TTP01")
                {
                    ModalProduct modal = new ModalProduct();
                    ModalProduct.TypeProduct = 5;
                    ModalProduct.Title = "Cantidad";
                    ModalProduct.Measure = "";
                    modal.ShowDialog(this);

                    if (!ModalProduct.IsCancel)
                    {
                        rowIndex = RonwIndexIfExist();

                        if (rowIndex == -1)
                        {
                            if (m.amount > 0)
                            {
                                DataRow row = dts1.NewRow();
                                row["ID"] = m.productId;
                                row["NOMBRE"] = name;
                                //row["TOTAL"] = (decimal)m.amount * ModalProduct.Quatity;
                                row["TOTAL"] = (decimal)m.amount * ModalProduct.Quatity;
                                row["UNITPRICE"] = (decimal)m.amount;
                                row["IVA"] = m.haveTax;
                                row["CANTIDAD"] = ModalProduct.Quatity;
                                if (m.haveTax)
                                    row["AMOUNTIVA"] = Math.Round(((((decimal)m.amount * ModalProduct.Quatity) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100), 2);
                                else
                                    row["AMOUNTIVA"] = 0;
                                //  row["UNIDAD"] = "E48";
                                row["UNIDAD"] = m.product.ProductParams.FirstOrDefault().UnitMeasurement;
                                dts1.Rows.Add(row);
                            }
                            tarifagrid();
                            Total();
                        }
                        else
                        {
                            var RowUpdate = dgvMovimientos.Rows[rowIndex];
                            RowUpdate.Cells[3].Value = Convert.ToDecimal(Regex.Replace(dgvMovimientos.Rows[rowIndex].Cells[3].FormattedValue.ToString(), @"[^\d.]", "")) + ((decimal)m.amount * ModalProduct.Quatity);
                            RowUpdate.Cells[6].Value = (Convert.ToInt32(dgvMovimientos.Rows[rowIndex].Cells[6].FormattedValue) + ModalProduct.Quatity).ToString();
                            if (m.haveTax)
                                RowUpdate.Cells[5].Value = Math.Round(((Convert.ToDecimal(Regex.Replace(RowUpdate.Cells[3].FormattedValue.ToString(), @"[^\d.]", "")) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100), 2);
                            tarifagrid();
                            Total();
                        }
                    }

                }
                else if (m.type == "TTP06")
                {
                    ModalProduct modal = new ModalProduct();
                    ModalProduct.TypeProduct = 6;
                    ModalProduct.Title = "Dictaminado";
                    ModalProduct.Measure = name;
                    modal.ShowDialog(this);
                    if (!ModalProduct.IsCancel)
                    {
                        DataRow row = dts1.NewRow();
                        row["ID"] = m.productId;
                        row["NOMBRE"] = ModalProduct.Measure == "" ? GetPath(treeList1.FindNodeByID(treeList1.FocusedNode.Id), "") : ModalProduct.Measure;
                        //row["TOTAL"] = ModalProduct.Quatity
                        row["TOTAL"] = ModalProduct.Quatity;
                        row["UNITPRICE"] = ModalProduct.Quatity;
                        row["IVA"] = m.haveTax;
                        if (m.haveTax)
                            row["AMOUNTIVA"] = Math.Round((((decimal)row["TOTAL"] * Convert.ToDecimal(Variables.Configuration.IVA)) / 100), 2);
                        else
                            row["AMOUNTIVA"] = 0;
                        row["CANTIDAD"] = 1;
                        row["UNIDAD"] = m.product.ProductParams.FirstOrDefault().UnitMeasurement;
                        dts1.Rows.Add(row);

                        tarifagrid();
                        Total();

                    }
                }
            }
            catch (Exception e)
            {


            }

            loading.Close();

        }

        void tarifagrid()
        {
            //dgvMovimientos.Rows.Clear(); 
            BindingSource source = new BindingSource();
            source.DataSource = dts1;
            dgvMovimientos.DataSource = source;
            dgvMovimientos.AutoGenerateColumns = false;
            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.AutoGenerateColumns = false;
            dgvMovimientos.Refresh();
            //dgvMovimientos.Columns["UNITPRICE"].Visible = false;
        }


        public static void alzheimer()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }

        public static void CreateTreeViewNodesRecursive(DataTable model, TreeListNodes nodesCollection, Int32 parentID)
        {
            var rows = model.Select("[ParentID] = " + parentID);


            foreach (DataRow row in rows)
            {
                var node = nodesCollection.Add(row["Descripcion"].ToString(), row["ID"].ToString());
                CreateTreeViewNodesRecursive(model, node.Nodes, Convert.ToInt32(row["ID"]));
            }
        }

        private async void Productos_Load(object sender, EventArgs e)
        {

            loading = new Loading();
            loading.Show(this);
            //alzheimer();

            dgvMovimientos.Columns["CANTIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMovimientos.Columns[4].DefaultCellStyle.Format = "c2";
            dgvMovimientos.Columns[4].DefaultCellStyle.FormatProvider = new CultureInfo("es-MX");
            dgvMovimientos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(System.Int32));
            dt.Columns.Add("Descripcion", typeof(System.String));
            dt.Columns.Add("ParentID", typeof(System.Int32));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };

            dt = await q.GETTProdcutos("/api/Products/Division/" + Variables.LoginModel.Divition);

            CreateTreeViewNodesRecursive(dt, treeList1.Nodes, 0);

            treeList1.OptionsBehavior.EnableFiltering = true;
            treeList1.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Extended;

            //TreeNodeCollection parentNode = treeView1.Nodes;
            //PopulateTreeView(parentNode, "0", dt);

            //CopyTreeNodes(treeView1, _fieldsTreeCache);


            dts1.Columns.Clear();
            dts1.Rows.Clear();
            dts1.Clear();
            dgvMovimientos.DataSource = null;

            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ID";
            dts1.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "NOMBRE";
            dts1.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "TOTAL";
            dts1.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "UNITPRICE";
            dts1.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "IVA";
            dts1.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "AMOUNTIVA";
            dts1.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "CANTIDAD";
            dts1.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "UNIDAD";
            dts1.Columns.Add(column);

            loading.Close();
        }

        private int RonwIndexIfExist()
        {
            int rowIndex = -1;
            foreach (DataGridViewRow row in dgvMovimientos.Rows)
            {
                if (row.Cells["ID"].Value != null) // Need to check for null if new row is exposed
                {
                    if (row.Cells["ID"].Value.ToString().Equals(m.productId.ToString()))
                    {
                        rowIndex = row.Index;
                        break;
                    }
                }
            }
            return rowIndex;
        }

        public void AddProductToGrid(int vp)
        {
            int rowIndex = RonwIndexIfExist();
            var rowCountAfterAdding = dts1.Rows.Count;
        }

        private void dgvMovimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvMovimientos.Rows[e.RowIndex];

                if (e.ColumnIndex == dgvMovimientos.Columns["Eliminar"].Index && e.RowIndex >= 0)
                {
                    dgvMovimientos.Rows.RemoveAt(dgvMovimientos.CurrentRow.Index);
                    Total();
                }
            }
        }

        private void dgvMovimientos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvMovimientos.Columns[e.ColumnIndex].Name == "Eliminar" && e.RowIndex >= 0)
            {
                var image = Properties.Resources.cancelar;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                e.Graphics.DrawImage(image, x, y, 15, 15);
                e.Handled = true;
            }
        }

        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            dts1.Rows.Clear();
            Total();
            tarifagrid();
            var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Agreements/AgreementByAccount/{0}", txtCuenta.Text), HttpMethod.Get, Variables.LoginModel.Token);
            try
            {

                Agreement agrements = JsonConvert.DeserializeObject<Agreement>(_resulTransaction);

                if (agrements == null)
                {
                    mensaje = new MessageBoxForm("No hay cuenta", "No se han encontrado cuenta para generar la facturación", TypeIcon.Icon.Warning);
                    mensaje.ShowDialog();
                    return;
                }

                cuenta = agrements.id;
                cuentap = agrements.account;
                namesp = agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").name + " " + agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").lastName + " " + agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").secondLastName;
                direccionp = agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Street + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Name + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.name + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.States.name + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.States.countries.name;
                rfcp = agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").rfc;
                label3.Text = agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").name + " " + agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").lastName + " " + agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").secondLastName;
                label4.Text = agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Street + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Name + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.name + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.States.name + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.States.countries.name;
                tableLayoutPanel2.Enabled = true;
                tableLayoutPanel6.Enabled = true;
                dts1.Rows.Clear();
            }
            catch (Exception)
            {
                mensaje = new MessageBoxForm("No hay cuenta", "No se han encontrado cuenta para generar la facturación", TypeIcon.Icon.Warning);
                mensaje.ShowDialog();

            }
            loading.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Variables.LoginModel.RolName.ToList().Find(x => x.Contains("GenerarOrden")) != null)
            {
                pbBG.Visible = false;
                pictureBox1.Visible = false;
                txtCuenta.Visible = false;
                checkBox1.Checked = true;
                checkBox1.Visible = false;
                tableLayoutPanel2.Enabled = true;
                tableLayoutPanel6.Enabled = true;
                pbxIcon.Location = new Point(13, 43);
                lblTitulo.Location = new Point(36, 41);
            }
            else
            {
                if (checkBox1.Checked == true)
                {
                    tableLayoutPanel2.Enabled = true;
                    tableLayoutPanel6.Enabled = true;
                    pbBG.Enabled = false;
                    pictureBox1.Enabled = false;
                    txtCuenta.Enabled = false;
                    txtCuenta.Text = "";
                    label3.Text = "";
                    label4.Text = "";
                    dts1.Rows.Clear();
                    Total();
                    tarifagrid();
                    cuenta = "";
                }
                else
                {
                    pbBG.Enabled = true;
                    pictureBox1.Enabled = true;
                    txtCuenta.Enabled = true;
                    tableLayoutPanel2.Enabled = false;
                    tableLayoutPanel6.Enabled = false;
                    txtCuenta.Text = "";
                    label3.Text = "";
                    label4.Text = "";
                    dts1.Rows.Clear();
                    Total();
                    tarifagrid();
                    cuenta = "";
                }
            }
            
        }

        void impresionhoja()
        {
            Variables.datosgenerales.Columns.Clear();
            Variables.datosgenerales.Rows.Clear();
            decimal subtotal = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblSubtotal.Text, @"[^\d.]", "")));
            decimal tax = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblIVA.Text, @"[^\d.]", "")));

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
            row["Cuenta"] = cuentap;
            row["Contribuyente"] = namesp;
            row["Rfc"] = rfcp;
            row["Domicilio"] = direccionp;
            row["Caja"] = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id;
            row["Sucursal"] = Variables.Configuration.Terminal.BranchOffice.Name;
            row["Usuario"] = Variables.LoginModel.FullName;
            Variables.datospadron.Rows.Add(row);

            Variables.Productos.Columns.Clear();
            Variables.Productos.Rows.Clear();

            DataColumn columnt;
            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "id";
            Variables.Productos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Cantidad";
            Variables.Productos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Descripcion";
            Variables.Productos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Total";
            Variables.Productos.Columns.Add(columnt);

            foreach (DataRow rowr in dts1.Rows)
            {
                DataRow rowt = Variables.Productos.NewRow();
                rowt["id"] = rowr[0].ToString();
                rowt["Cantidad"] = rowr[5].ToString();
                rowt["Descripcion"] = rowr[1].ToString();
                rowt["Total"] = rowr[2].ToString();
                Variables.Productos.Rows.Add(rowt);
            }

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

            Image img = q.Imagen();
            byte[] arr;
            ImageConverter converter = new ImageConverter();
            arr = (byte[])converter.ConvertTo(img, typeof(byte[]));

            DataRow rowt1 = Variables.ImagenData.NewRow();
            rowt1["Imagen1"] = arr;
            rowt1["Imagen2"] = arr;
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

            string k1 = label23.Text.Replace("$", "");
            //int totals = Convert.ToInt32(Math.Round(Convert.ToDecimal(k1)));
            //string k = totals.ToWords();
            Numalet let = new Numalet();
            let.MascaraSalidaDecimal = "00/100 M.N";
            let.SeparadorDecimalSalida = "pesos";
            let.LetraCapital = true;
            let.ApocoparUnoParteEntera = true;

            DataRow rowt2 = Variables.Foliotiket.NewRow();
            rowt2["Foliotransaccion"] = Variables.foliotransaccion;
            rowt2["Letra"] = let.ToCustomCardinal(k1);
            rowt2["Total"] = label23.Text.Replace("$", "");
            rowt2["Subtotal"] = string.Format(new CultureInfo("es-MX"), "{0:C2}", subtotal);
            rowt2["IVA"] = string.Format(new CultureInfo("es-MX"), "{0:C2}", tax);
            Variables.Foliotiket.Rows.Add(rowt2);

            Impresion im = new Impresion();
            im.ShowDialog();
        }

        private async void txtCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                loading = new Loading();
                loading.Show(this);
                e.Handled = true;
                dts1.Rows.Clear();
                Total();
                tarifagrid();
                var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Agreements/AgreementByAccount/{0}", txtCuenta.Text), HttpMethod.Get, Variables.LoginModel.Token);
                try
                {

                    Agreement agrements = JsonConvert.DeserializeObject<Agreement>(_resulTransaction);

                    if (agrements == null)
                    {
                        mensaje = new MessageBoxForm("No hay cuenta", "No se han encontrado cuenta para generar la facturación", TypeIcon.Icon.Warning);
                        mensaje.ShowDialog();
                        return;
                    }

                    cuenta = agrements.id;
                    cuentap = agrements.account;
                    namesp = agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").name + " " + agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").lastName + " " + agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").secondLastName;
                    direccionp = agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Street + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Name + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.name + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.States.name + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.States.countries.name;
                    rfcp = agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").rfc;
                    label3.Text = agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").name + " " + agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").lastName + " " + agrements.Clients.FirstOrDefault(x => x.typeUser == "CLI01").secondLastName;
                    label4.Text = agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Street + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Name + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.name + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.States.name + " " + agrements.Addresses.FirstOrDefault(x => x.TypeAddress == "DIR01").Suburbs.Towns.States.countries.name;
                    tableLayoutPanel2.Enabled = true;
                    tableLayoutPanel6.Enabled = true;
                }
                catch (Exception)
                {
                    mensaje = new MessageBoxForm("No hay cuenta", "No se han encontrado cuenta para generar la facturación", TypeIcon.Icon.Warning);
                    mensaje.ShowDialog();

                }
                loading.Close();

            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string[] separadas;
            bool on = false;
            loading = new Loading();
            loading.Show(this);
            decimal subtotal = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblSubtotal.Text, @"[^\d.]", "")));
            decimal tax = Convert.ToDecimal((System.Text.RegularExpressions.Regex.Replace(lblIVA.Text, @"[^\d.]", "")));

            if (checkBox1.Checked == true)
            {
                if (dgvMovimientos.Rows.Count == 0)
                {

                    loading.Close();
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Ingrese productos para generar la operación", TypeIcon.Icon.Warning);
                    mensaje.ShowDialog();
                }
                else
                {


                    loading.Close();

                    Model.OrderSale orderSale = new Model.OrderSale();
                    orderSale.DateOrder = DateTime.Now;
                    orderSale.Amount = Convert.ToDecimal(lblSubtotal.Text.Replace("$", ""));
                    orderSale.OnAccount = 0;
                    orderSale.Year = Convert.ToInt16(DateTime.Now.Year);
                    orderSale.Period = 1;
                    orderSale.Type = "OA001";
                    orderSale.Status = "ED001";
                    orderSale.DivisionId = Variables.LoginModel.Divition;

                    string check = string.Empty;
                    List<Model.OrderSaleDetails> lista = new List<Model.OrderSaleDetails>();
                    foreach (DataGridViewRow row in dgvMovimientos.Rows)
                    {

                        lista.Add(new Model.OrderSaleDetails
                        {
                            Amount = Convert.ToDecimal(row.Cells[3].Value),
                            CodeConcept = Convert.ToString(row.Cells[1].Value),
                            HaveTax = Convert.ToBoolean(row.Cells[5].Value),
                            Tax = Convert.ToDecimal(row.Cells[6].Value),
                            OnAccount = 0,
                            NameConcept = row.Cells[2].Value.ToString(),
                            Quantity = Convert.ToInt32(row.Cells[7].Value),
                            Description = row.Cells[2].Value.ToString().Replace("\\", " - "),
                            UnitPrice = Convert.ToDecimal(row.Cells["UNITPRICE"].Value),
                            Unity = "Unidad de servicio"
                        });
                    }

                    orderSale.OrderSaleDetails = lista;


                    UI.ModalTax modalTax = new UI.ModalTax(orderSale, dts1, label23.Text, subtotal, tax);
                    modalTax.ShowDialog(this);

                }

                loading.Close();
            }

            else
            {
                if (cuenta == "")
                {

                    loading.Close();
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "No hay cuenta para generr la operación", TypeIcon.Icon.Warning);
                    mensaje.ShowDialog();
                }
                else
                {
                    if (dgvMovimientos.Rows.Count == 0)
                    {
                        loading.Close();
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "Ingrese productos para generar la operación", TypeIcon.Icon.Warning);
                        mensaje.ShowDialog();
                    }
                    else
                    {

                        Model.Debt d = new Model.Debt();
                        d.AgreementId = Convert.ToInt32(cuenta);
                        //d.Amount = Convert.ToDecimal(label23.Text.Replace("$", ""));
                        d.Amount = subtotal;
                        d.OnAccount = 0;
                        d.Type = "TIP02";
                        d.Status = "ED001";
                        d.TypeIntake = "0";
                        d.Consumption = "0";
                        d.TypeService = "0";

                        string check = string.Empty;
                        List<Model.DebtDetail> lista = new List<Model.DebtDetail>();
                        foreach (DataGridViewRow row in dgvMovimientos.Rows)
                        {
                            lista.Add(new Model.DebtDetail
                            {
                                Amount = Math.Round(Convert.ToDecimal(row.Cells[3].Value),2),
                                OnPayment = 0,
                                CodeConcept = Convert.ToString(row.Cells[1].Value),
                                HaveTax = Convert.ToBoolean(row.Cells[5].Value),
                                OnAccount = 0,
                                NameConcept = Convert.ToString(row.Cells[2].Value),
                                quantity = Convert.ToDecimal(row.Cells[7].Value),

                            });
                        }

                        d.DebtDetails = lista;
                        check = await q.POSTProduct("/api/Products/Agreement/" + cuenta, d);
                        separadas = check.Split('/');
                        if (separadas[0].ToString() == "error")
                        {
                            mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                            mensaje.ShowDialog();
                            loading.Close();
                        }
                        else
                        {
                            mensaje = new MessageBoxForm(Variables.titleprincipal, "Generado Correctamente", TypeIcon.Icon.Success);
                            mensaje.ShowDialog();
                            loading.Close();
                            if (Properties.Settings.Default.Printer == true)
                            {
                                on = q.EstaEnLineaLaImpresora(q.ImpresoraPredeterminada());
                                if (on == true)
                                {
                                    TiketProductos imp = new TiketProductos();
                                    imp.Imprime(dts1, 2, label23.Text.Replace("$", ""), namesp, cuentap, rfcp);
                                }
                            }
                            else
                            {
                                Variables.optionvistaimpresion = 3;
                                impresionhoja();
                            }

                            dgvMovimientos.DataSource = null;
                            cuenta = "";
                            txtCuenta.Text = "";
                            label3.Text = "";
                            label4.Text = "";
                            label23.Text = "$0";
                            tableLayoutPanel2.Enabled = false;
                            tableLayoutPanel6.Enabled = false;
                            Total();
                        }
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                string activeFilterString = String.Format("StartsWith([Description], '{0}')", "");
                treeList1.ActiveFilterString = activeFilterString;
                treeList1.FilterNodes();
                //treeList1.ExpandAll();
            }
        }

        public void CopyTreeNodes(TreeView treeview1, TreeView treeview2)
        {
            TreeNode newTn;
            foreach (TreeNode tn in treeview1.Nodes)
            {
                newTn = new TreeNode(tn.Text, tn.ImageIndex, tn.SelectedImageIndex);
                CopyChildren(newTn, tn);
                treeview2.Nodes.Add(newTn);
            }
        }
        public void CopyChildren(TreeNode parent, TreeNode original)
        {
            TreeNode newTn;
            foreach (TreeNode tn in original.Nodes)
            {
                newTn = new TreeNode(tn.Text, tn.ImageIndex, tn.SelectedImageIndex);
                parent.Nodes.Add(newTn);
                CopyChildren(newTn, tn);
            }
        }

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            string k = string.Empty;
            string n = string.Empty;
            vp = 0;
            if (treeList1.Selection.FirstOrDefault().RootNode != null)
            {
                k = treeList1.Selection.Select(x => x.GetDisplayText("ID")).FirstOrDefault();
                n = GetPath(treeList1.FindNodeByID(treeList1.FocusedNode.Id), "");
                idproducto = k;
                namesconcept = n;
                cargar(k, n);
                dgvMovimientos.ClearSelection();
            }
        }

        string GetPath(TreeListNode node, string path)
        {
            if (node.ParentNode != null)
                return GetPath(node.ParentNode, node.GetDisplayText("Description") + " - " + path);
            string s = node.GetDisplayText("Description") + " - " + path;
            return s.Substring(0, s.Length - 3);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //string activeFilterString = String.Format("StartsWith([Description], '{0}')", txtSearch.Text);
            string activeFilterString = String.Format("Contains([Description], '{0}')", txtSearch.Text);
            treeList1.ActiveFilterString = activeFilterString;
            treeList1.FilterNodes();
            treeList1.ExpandAll();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string activeFilterString = String.Format("StartsWith([Description], '{0}')", txtSearch.Text);
            //treeList1.ActiveFilterString = activeFilterString;
            //treeList1.FilterNodes();
            //treeList1.ExpandAll();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string activeFilterString = String.Format("Contains([Description], '{0}')", txtSearch.Text);
                treeList1.ActiveFilterString = activeFilterString;
                treeList1.FilterNodes();
                treeList1.ExpandAll();
            } 
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
           
        }

        private void tsmiSearOders_Click(object sender, EventArgs e)
        {
            FindOrders orders = new FindOrders();
            orders.ShowDialog(this);
        }

        private void cancelarOrdenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindOrders orders = new FindOrders();
            orders.ShowDialog(this);
        }
    }
}
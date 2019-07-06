﻿using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Humanizer;
using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Services;
using SOAPAP.UI;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Runtime.InteropServices;
using System.Globalization;
using SOAPAP.UI.Messages;

namespace SOAPAP
{
    public partial class Movimientos : Form
    {
        DataTable dtt = new DataTable();
        querys q = new querys();
        Form cuadritos3;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dts = new DataTable();
        string idp = string.Empty;
        string cuentap = string.Empty;
        string namesp = string.Empty;
        string rfcp = string.Empty;
        string direccionp = string.Empty;
        string numDerivatives = string.Empty;
        string typeService = string.Empty;
        string typeConsume = string.Empty;
        string typeRegime = string.Empty;
        string typePeriod = string.Empty;
        string typeStateService = string.Empty;
        string typeIntake = string.Empty;
        DataTable desiredResult = new DataTable();
        string foliotrast = string.Empty;
        decimal totalt = 0;
        DialogResult result = new DialogResult();
        Form mensaje;
        Form loading;
        Form cuadritos6;
        HttpContent content;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        BindingSource source;
        public Movimientos()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }



        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public static void alzheimer()
        {
            //Console.WriteLine("--LiberarMemoria--"); 
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }


        private async void consultadecobros_Load(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            alzheimer();
            await cargaCombos();
            await Total();
            await cargar();
            label1.Text = "FECHA:" + DateTime.Now.ToString("dd-MM-yyyy");
            loading.Close();
        }

        public static void DataGridViewCellVisibility(DataGridViewCell cell, bool visible)
        {
            cell.Style = visible ?
                  new DataGridViewCellStyle { Padding = new Padding(0, 0, 0, 0) } :
                  new DataGridViewCellStyle { Padding = new Padding(cell.OwningColumn.Width, 0, 0, 0) };

            cell.ReadOnly = !visible;

        }

        private void dgvMovimientos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvMovimientos.Columns[e.ColumnIndex].Name == "Imprimir" && e.RowIndex >= 0)
            {
                if (this.dgvMovimientos.Rows[e.RowIndex].Cells["typeTransactionId"].Value.ToString() == "3" || this.dgvMovimientos.Rows[e.RowIndex].Cells["typeTransactionId"].Value.ToString() == "6")
                {
                    var image = Properties.Resources.imprimir;
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                    e.Graphics.DrawImage(image, x, y, 15, 15);
                    e.Handled = true;
                }
                else
                {
                    DataGridViewCellVisibility(dgvMovimientos.Rows[e.RowIndex].Cells["Imprimir"], false);
                }
            }

            if (e.ColumnIndex >= 0 && this.dgvMovimientos.Columns[e.ColumnIndex].Name == "Cancelar" && e.RowIndex >= 0)
            {
                if (this.dgvMovimientos.Rows[e.RowIndex].Cells["typeTransactionId"].Value.ToString() == "3")
                {
                    var image = Properties.Resources.borrar;
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                    e.Graphics.DrawImage(image, x, y, 15, 15);
                    e.Handled = true;
                }
                else
                {
                    DataGridViewCellVisibility(dgvMovimientos.Rows[e.RowIndex].Cells["Cancelar"], false);
                }
            }

            if (e.ColumnIndex >= 0 && this.dgvMovimientos.Columns[e.ColumnIndex].Name == "PDF" && e.RowIndex >= 0)
            {
                if (this.dgvMovimientos.Rows[e.RowIndex].Cells["typeTransactionId"].Value.ToString() == "3")
                {
                    var image = Properties.Resources.pdf;
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                    e.Graphics.DrawImage(image, x, y, 15, 15);
                    e.Handled = true;
                }
                else
                {
                    DataGridViewCellVisibility(dgvMovimientos.Rows[e.RowIndex].Cells["PDF"], false);
                }
            }

            if (e.ColumnIndex >= 0 && this.dgvMovimientos.Columns[e.ColumnIndex].Name == "XML" && e.RowIndex >= 0)
            {
                if (this.dgvMovimientos.Rows[e.RowIndex].Cells["typeTransactionId"].Value.ToString() == "3" || this.dgvMovimientos.Rows[e.RowIndex].Cells["typeTransactionId"].Value.ToString() == "4")
                {
                    var image = Properties.Resources.xml;
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                    e.Graphics.DrawImage(image, x , y , 15, 15);
                    e.Handled = true;
                }
                else
                {
                    DataGridViewCellVisibility(dgvMovimientos.Rows[e.RowIndex].Cells["XML"], false);
                }
            }

            if (e.ColumnIndex >= 0 && this.dgvMovimientos.Columns[e.ColumnIndex].Name == "TIMBRAR" && e.RowIndex >= 0)
            {
                if (this.dgvMovimientos.Rows[e.RowIndex].Cells["typeTransactionId"].Value.ToString() == "3" || this.dgvMovimientos.Rows[e.RowIndex].Cells["typeTransactionId"].Value.ToString() == "4")
                {
                    var image = Properties.Resources.Timbre; 
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                    e.Graphics.DrawImage(image, x , y, 15, 15);
                    e.Handled = true;
                }
                else
                {
                    DataGridViewCellVisibility(dgvMovimientos.Rows[e.RowIndex].Cells["TIMBRAR"], false);
                }
            }

            if (e.ColumnIndex >= 0 && this.dgvMovimientos.Columns[e.ColumnIndex].Name == "Estado" && e.RowIndex >= 0)
            {
                var image = Properties.Resources.sin_estado;

                if (this.dgvMovimientos.Rows[e.RowIndex].Cells["typeTransactionId"].Value.ToString() == "3")
                    image = Properties.Resources.cobrado;

                if (this.dgvMovimientos.Rows[e.RowIndex].Cells["typeTransactionId"].Value.ToString() == "4")
                    image = Properties.Resources.cancelado;

                if (this.dgvMovimientos.Rows[e.RowIndex].Cells["typeTransactionId"].Value.ToString() == "5")
                    image = Properties.Resources.cerrada;

                if (this.dgvMovimientos.Rows[e.RowIndex].Cells["typeTransactionId"].Value.ToString() == "6")
                    image = Properties.Resources.retirado;

                var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                e.Graphics.DrawImage(image, x, y, 10, 10);
                e.Handled = true;
            }
        }



        private async void cmbTypeTransaction_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            await cargar();
            loading.Close();
        }

        //Se selecciona una nueva fecha
        private async void dtpFechaBusqueda_ValueChanged(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            alzheimer();
            await cargaCombos();
            await Total();
            await cargar();
            label1.Text = "FECHA:" + DateTime.Now.ToString("dd-MM-yyyy");
            loading.Close();
        }

        //Clic switch
        private void tswSeleccionDia_Toggled(object sender, EventArgs e)
        {
            if (tswSeleccionDia.IsOn)
            {
                pbxFondoFechaBusqueda.Visible = true;
                dtpFechaBusqueda.Visible = true;
            }
            else
            {
                pbxFondoFechaBusqueda.Visible = false;
                dtpFechaBusqueda.Visible = false;
            }
        }

        private async void prevvisualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ObtenerCorte();
            if (Properties.Settings.Default.Printer == true)
            {
                Tiraauditora imp = new Tiraauditora();
                imp.Imprime(dt, 1, totalt.ToString());
            }
            else
            {
                Variables.optionvistaimpresion = 2;
                impresionhoja();
            }

        }

        private async void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ObtenerCorte();
            if (Properties.Settings.Default.Printer == true)
            {
                Tiraauditora imp = new Tiraauditora();
                imp.Imprime(dt, 2, totalt.ToString());
            }
            else
            {
                Variables.optionvistaimpresion = 2;
                impresionhoja();
            }
        }

        private void retirarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mensaje = new ModalRetiroCaja(Variables.Configuration.Terminal.TerminalUsers.First().Id,DateTime.Now.ToString("yyyy-MM-dd"), "/api/Transaction/{0}",1);
            result = mensaje.ShowDialog(this);
        }

        #region PrivateMethod
        public async Task cargar()
        {
            centraX(pnlHeader, pbBG);
            centraX(pnlHeader, cmbTypeTransaction);
            centraX(pnlHeader, pnlSearch);

            string _resulTransaction;
            if (tswSeleccionDia.IsOn)            
                _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/FromUserInDay/{0}/{1}", dtpFechaBusqueda.Value.ToString("yyyy-MM-dd") , Variables.LoginModel.User), HttpMethod.Get, Variables.LoginModel.Token);            
            else            
                _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}/{1}", DateTime.Now.ToString("yyyy-MM-dd") , Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString()), HttpMethod.Get, Variables.LoginModel.Token);
            

            if (_resulTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                source = new BindingSource();
                List<SOAPAP.Model.Transaction> transactions = JsonConvert.DeserializeObject<List<SOAPAP.Model.Transaction>>(_resulTransaction);

                if (transactions == null)
                {
                    mensaje = new MessageBoxForm("Sin Operaciones", "No se han encontrado movimientos para esta terminal", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }

                SOAPAP.Model.TypeTransaction typeTransaction = new SOAPAP.Model.TypeTransaction();
                typeTransaction = cmbTypeTransaction.SelectedValue as SOAPAP.Model.TypeTransaction;

                if (cmbTypeTransaction.Items.Count > 0 && typeTransaction.Id != 0)
                {
                    var transactionsFilter = transactions.Where(x => x.TypeTransactionId == typeTransaction.Id)
                                                       .Select(s => new
                                                       {
                                                           id = s.Id,
                                                           TypeTransaction = s.TypeTransaction.Name,
                                                           FolioImpresion = s.TransactionFolios.Count > 0 ? s.TransactionFolios.First().Folio.ToString() : String.Empty,
                                                           dateTransaction = s.DateTransaction.ToString("HH:mm"),
                                                           sign = s.Sign,
                                                           amount = s.Amount,
                                                           tax = s.Tax,
                                                           rounding = s.Rounding,
                                                           total = s.Total,
                                                           aplication = s.Aplication,
                                                           cancellationFolio = s.CancellationFolio,
                                                           authorizationOriginPayment = s.AuthorizationOriginPayment,
                                                           nameterminalUser = s.TypeTransaction.Name,
                                                           idpayMethod = s.PayMethodId,
                                                           namepayMethod = s.PayMethod.Name,
                                                           Folio = s.Folio,
                                                           typeTransactionId = s.TypeTransactionId,
                                                           
                                                       }).ToList();
                    source.DataSource = transactionsFilter;
                }
                else
                {
                    var transactionsFilter = transactions.Select(s => new
                    {
                        id = s.Id,
                        TypeTransaction = s.TypeTransaction.Name,
                        FolioImpresion = s.TransactionFolios.Count > 0 ? s.TransactionFolios.First().Folio.ToString() : String.Empty,
                        dateTransaction = s.DateTransaction.ToString("HH:mm"),
                        sign = s.Sign,
                        amount = s.Amount,
                        tax = s.Tax,
                        rounding = s.Rounding,
                        total = s.Total,
                        aplication = s.Aplication,
                        cancellationFolio = s.CancellationFolio,
                        authorizationOriginPayment = s.AuthorizationOriginPayment,
                        nameterminalUser = s.TypeTransaction.Name,
                        idpayMethod = s.PayMethodId,
                        namepayMethod = s.PayMethod.Name,
                        Folio = s.Folio,
                        typeTransactionId = s.TypeTransactionId
                    }).ToList();
                    source.DataSource = transactionsFilter;
                }
                dgvMovimientos.DataSource = source;
                dgvMovimientos.Refresh();
                await Total();
            }
        }
        #endregion

        #region PrivateMethod
        private async Task cargaCombos()
        {
            var resultTypeTransaction = await Requests.SendURIAsync("/api/TypeTransaction/", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var Types = JsonConvert.DeserializeObject<List<SOAPAP.Model.TypeTransaction>>(resultTypeTransaction);
                SOAPAP.Model.TypeTransaction typeTransaction = new SOAPAP.Model.TypeTransaction();
                typeTransaction.Id = 0;
                typeTransaction.Name = "Todos";
                Types.Add(typeTransaction);

                cmbTypeTransaction.ValueMember = "id_type_transaction";
                cmbTypeTransaction.DisplayMember = "name";
                cmbTypeTransaction.DataSource = Types;
                cmbTypeTransaction.SelectedIndex = cmbTypeTransaction.FindString("Todos");
            }

            if (tswSeleccionDia.IsOn)
            {
                pbxFondoFechaBusqueda.Visible = true;
                dtpFechaBusqueda.Visible = true;
            }
            else
            {
                pbxFondoFechaBusqueda.Visible = false;
                dtpFechaBusqueda.Visible = false;
            }
        }

        private void centraX(Control padre, Control hijo)
        {
            int x = 0;
            x = (padre.Width / 2) - (hijo.Width / 2);
            hijo.Location = new System.Drawing.Point(x, hijo.Location.Y);
        }

        private async Task ObtenerCorte()
        {
            Variables.efectivo = 0;
            Variables.cheques = 0;
            Variables.tarjetas = 0;
            Variables.otros = 0;
            decimal positivo = 0;
            decimal negativo = 0;
            loading = new Loading();
            loading.Show(this);
            dt = await q.GETTransaction("/api/Transaction/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString());
            loading.Close();
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToBoolean(row[3]) == true && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 6 && Convert.ToInt32(row[19]) != 7)
                {
                    positivo = positivo + Convert.ToDecimal(row[7]);
                }
                else if (Convert.ToBoolean(row[3]) == false && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 7 && Convert.ToInt32(row[19]) != 6)
                {
                    negativo = negativo + Convert.ToDecimal(row[7]);
                }
            }

            totalt = positivo - negativo;
           
        }

        private async Task Total()
        {
            Variables.efectivo = 0;
            Variables.cheques = 0;
            Variables.tarjetas = 0;
            Variables.otros = 0;
            decimal positivo = 0;
            decimal negativo = 0;
           
            dt = await q.GETTransaction("/api/Transaction/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString());
           
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToBoolean(row[3]) == true && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 6 && Convert.ToInt32(row[19]) != 7)
                {
                    positivo = positivo + Convert.ToDecimal(row[7]);
                }
                else if (Convert.ToBoolean(row[3]) == false && Convert.ToInt32(row[19]) != 1 && Convert.ToInt32(row[19]) != 2 && Convert.ToInt32(row[19]) != 5 && Convert.ToInt32(row[19]) != 7 && Convert.ToInt32(row[19]) != 6)
                {
                    negativo = negativo + Convert.ToDecimal(row[7]);
                }
            }

            totalt = positivo - negativo;
            label2.Text = "TOTAL: " + string.Format(new CultureInfo("es-MX"), "{0:C2}", totalt);

        }
        
        private void impresionhoja()
        {
            Variables.datosgenerales.Columns.Clear();
            Variables.datosgenerales.Rows.Clear();

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
            row["FoliodeCaja"] = "1";
            row["Fechayhora"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            row["Cuenta"] = "";
            row["Contribuyente"] = "";
            row["Rfc"] = "";
            row["Domicilio"] = "";
            row["Caja"] = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id;
            row["Sucursal"] = Variables.Configuration.Terminal.BranchOffice.Name;
            row["Usuario"] = Variables.LoginModel.FullName;
            row["Periodo"] = "";
            Variables.datospadron.Rows.Add(row);

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

            foreach (DataRow rowr in dt.Rows)
            {
                DataRow rowt = Variables.pagos.NewRow();
                rowt["ID"] = rowr[0].ToString();
                rowt["Concepto"] = rowr[13].ToString();
                rowt["Importe"] = rowr[7].ToString();
                rowt["Year"] = rowr[8].ToString();
                rowt["Debperiod"] = rowr[9].ToString();
                Variables.pagos.Rows.Add(rowt);
            }

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


            string k1 = totalt.ToString();
            int totalss = Convert.ToInt32(Math.Round(Convert.ToDecimal(k1)));
            string k = totalss.ToWords();

            DataRow rowt2 = Variables.Foliotiket.NewRow();
            rowt2["Foliotransaccion"] = Variables.otros;
            rowt2["Letra"] = k.ToUpper() + " PESOS M.N.";
            rowt2["Pago"] = Variables.otros;
            rowt2["Subtotal"] = Variables.efectivo;
            rowt2["IVA"] = Variables.cheques;
            rowt2["Redondeo"] = Variables.tarjetas;
            rowt2["Total"] = totalt.ToString();
            Variables.Foliotiket.Rows.Add(rowt2);

            Impresion im = new Impresion();
            im.ShowDialog();
        }


        public void impresionhojas()
        {
            Variables.datosgenerales.Columns.Clear();
            Variables.datosgenerales.Rows.Clear();

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
            row["Periodo"] = "";
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

            foreach (DataRow rowr in dt2.Rows)
            {
                DataRow rowt = Variables.pagos.NewRow();
                rowt["ID"] = rowr[0].ToString();
                rowt["Concepto"] = rowr[5].ToString();
                rowt["Importe"] = rowr[7].ToString();
                rowt["Year"] = rowr[8].ToString();
                rowt["Debperiod"] = rowr[9].ToString();
                Variables.pagos.Rows.Add(rowt);
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

            var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            var qrCode = qrEncoder.Encode(Variables.foliotransaccion);
            var renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            using (var stream = new FileStream("qrcode.png", FileMode.Create))
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
            MemoryStream ms = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemp = new Bitmap(ms);
            Image image = new Bitmap(imageTemp, new Size(new Point(200, 200)));
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

            string k1 = Variables.totalp.ToString();
            int totals = Convert.ToInt32(Math.Round(Convert.ToDecimal(k1)));
            string k = totals.ToWords();

            DataRow rowt2 = Variables.Foliotiket.NewRow();
            rowt2["Foliotransaccion"] = Variables.foliotransaccion;
            rowt2["Letra"] = k.ToUpper() + " PESOS M.N.";
            rowt2["Pago"] = Variables.metododepagop;
            rowt2["Subtotal"] = Variables.subtotalp;
            rowt2["IVA"] = Variables.ivap;
            rowt2["Redondeo"] = Variables.redondeop;
            rowt2["Total"] = Variables.totalp;
            Variables.Foliotiket.Rows.Add(rowt2);
            Impresion im = new Impresion();
            im.ShowDialog();
        }

        #endregion

        private async void dgvMovimientos_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string textoObservacion = "";
            string textoUsos = "";
            string[] separadas;
            DataTable dt1 = new DataTable();
            Model.TransactionPaymentVM transactionSelect = new Model.TransactionPaymentVM();
            transactionSelect.Transaction = new Model.Transaction();
            transactionSelect.Payment = new Model.Payment();

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvMovimientos.Rows[e.RowIndex];
                transactionSelect.Transaction.Id = !String.IsNullOrEmpty(row.Cells["Id"].Value.ToString()) ? Convert.ToInt32(row.Cells["Id"].Value) : 0;

                if (e.ColumnIndex == dgvMovimientos.Columns["Imprimir"].Index && e.RowIndex >= 0)
                {
                    if (row.Cells["typeTransactionId"].Value.ToString() == "3" || row.Cells["typeTransactionId"].Value.ToString() == "6")
                    {
                        mensaje = new MessageBoxForm("¿Imprimir operación?", "Verifique que la impresora se encuentre lista para imprimir", TypeIcon.Icon.Warning, true);
                        result = mensaje.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            loading = new Loading();
                            loading.Show(this);

                            if( row.Cells["typeTransactionId"].Value.ToString() == "6")
                            {
                                dt2 = await q.GETTransactionIDCR("/api/Transaction/" + row.Cells["ID"].Value.ToString(),"RETIRO");
                                
                                if (Properties.Settings.Default.Printer == true)
                                {
                                    Tiket imp = new Tiket();
                                    imp.Imprime(dt2, 2, Variables.subtotalp.ToString(), Variables.ivap.ToString(), Variables.redondeop.ToString(), Variables.totalp.ToString(), Variables.metododepagop, namesp, Variables.foliocaja, "", cuentap, rfcp, Variables.foliotransaccion, direccionp);
                                    loading.Close();
                                }

                                else
                                {
                                    Variables.optionvistaimpresion = 4;
                                    impresionhojas();
                                }
                            }
                            else
                            {
                                dt2 = await q.GETTransactionID("/api/Transaction/" + row.Cells["ID"].Value.ToString());
                                dt1 = await q.GETAgreementsbyaccount("/api/Agreements/" + Variables.idagrement + "");
                                if (dt1 != null)
                                {
                                    foreach (DataRow rows in dt1.Rows)
                                    {
                                        separadas = rows[0].ToString().Split('/');
                                        if (separadas[0].ToString() == "error")
                                        {
                                            mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                            mensaje.ShowDialog();
                                            dt1.Rows.Clear();
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                                    mensaje.ShowDialog();
                                }

                                foreach (DataRow rows in dt1.Rows)
                                {
                                    idp = rows[0].ToString();
                                    cuentap = rows[1].ToString();
                                    namesp = rows[2].ToString();
                                    rfcp = rows[3].ToString();
                                    direccionp = rows[4].ToString();
                                    numDerivatives = rows[5].ToString();
                                    typeService = rows[6].ToString();
                                    typeConsume = rows[7].ToString();
                                    typeRegime = rows[8].ToString();
                                    typePeriod = rows[9].ToString();
                                    typeStateService = rows[10].ToString();
                                    typeIntake = rows[11].ToString();
                                }

                                if (Properties.Settings.Default.Printer == true)
                                {
                                    Tiket imp = new Tiket();
                                    imp.Imprime(dt2, 2, Variables.subtotalp.ToString(), Variables.ivap.ToString(), Variables.redondeop.ToString(), Variables.totalp.ToString(), Variables.metododepagop, namesp, Variables.foliocaja, "", cuentap, rfcp, Variables.foliotransaccion, direccionp);
                                    loading.Close();
                                }
                                else
                                {
                                    Variables.optionvistaimpresion = 1;
                                    impresionhojas();
                                }
                            }
                            
                            loading.Close();

                        }
                    }
                }

                if (e.ColumnIndex == dgvMovimientos.Columns["XML"].Index && e.RowIndex >= 0)
                {
                    if (row.Cells["typeTransactionId"].Value.ToString() == "3" || row.Cells["typeTransactionId"].Value.ToString() == "4")
                    {
                            loading = new Loading();
                            loading.Show(this);
                        
                        var resultTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", transactionSelect.Transaction.Id), HttpMethod.Get, Variables.LoginModel.Token);
                        transactionSelect = JsonConvert.DeserializeObject<Model.TransactionPaymentVM>(resultTransaction);
                        var resultTransactions = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", transactionSelect.Payment.Id), HttpMethod.Get, Variables.LoginModel.Token);
                        Model.Payment payment = JsonConvert.DeserializeObject<Model.Payment>(resultTransactions);

                        if(row.Cells["typeTransactionId"].Value.ToString() == "3")
                        {

                            try
                            {
                                ExportGridToXML(payment.TaxReceipts.FirstOrDefault(x => x.Status == "ET001").Xml);
                            }
                            catch (Exception)
                            {

                            mensaje = new MessageBoxForm(Variables.titleprincipal, "Este cobro no cuenta con timbrado favor de contactar al Administrador", TypeIcon.Icon.Cancel);
                            mensaje.ShowDialog();
                        }
                        }

                        else if(row.Cells["typeTransactionId"].Value.ToString() == "4")
                        {
                            try
                            {
                                ExportGridToXML(payment.TaxReceipts.FirstOrDefault(x => x.Status == "ET002").Xml);
                            }
                            catch (Exception)
                            {

                                mensaje = new MessageBoxForm(Variables.titleprincipal, "Este cobro no cuenta con timbrado favor de contactar al Administrador", TypeIcon.Icon.Cancel);
                                mensaje.ShowDialog();
                            }
                        }
                        
                        

                        loading.Close();

                    }
                }

                if (e.ColumnIndex == dgvMovimientos.Columns["PDF"].Index && e.RowIndex >= 0)
                {
                    if (row.Cells["typeTransactionId"].Value.ToString() == "3" || row.Cells["typeTransactionId"].Value.ToString() == "6")
                    {
                        loading = new Loading();
                        loading.Show(this);
                        var resultTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", transactionSelect.Transaction.Id), HttpMethod.Get, Variables.LoginModel.Token);
                        transactionSelect = JsonConvert.DeserializeObject<Model.TransactionPaymentVM>(resultTransaction);
                        var resultTransactions = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", transactionSelect.Payment.Id), HttpMethod.Get, Variables.LoginModel.Token);
                        Model.Payment payment = JsonConvert.DeserializeObject<Model.Payment>(resultTransactions);
                        WsIntegral33Pruebas.WsCFDI33Client n = new WsIntegral33Pruebas.WsCFDI33Client();
                        string test = "before passing";
                        
                        try
                        {
                            var rest = n.ObtenerPDF("CFDI010233001", "Pruebas1a$", payment.TaxReceipts.FirstOrDefault(x=>x.Status== "ET001").Xml, "", "", "", 1, ref test);
                            ExportGridToPDF(rest);
                        }
                        catch (Exception)
                        {
                            mensaje = new MessageBoxForm(Variables.titleprincipal, "Este cobro no cuenta con timbrado", TypeIcon.Icon.Cancel);
                            mensaje.ShowDialog();

                        }
                        
                        loading.Close();
                    }
                }


                if (e.ColumnIndex == dgvMovimientos.Columns["TIMBRAR"].Index && e.RowIndex >= 0)
                {
                        string xmltimbrado = string.Empty;

                    using (msgObservacionFactura msgObs = new msgObservacionFactura())
                    {
                        msgObs.ShowDialog(this);
                        textoObservacion = msgObs.TextoObservacion;
                        textoUsos = msgObs.Usos;
                    }                        
                    

                    loading = new Loading();
                        loading.Show(this);
                        
                      
                   
                            if(row.Cells["typeTransactionId"].Value.ToString() == "3")
                            {

                            var resultTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", transactionSelect.Transaction.Id), HttpMethod.Get, Variables.LoginModel.Token);
                            transactionSelect = JsonConvert.DeserializeObject<Model.TransactionPaymentVM>(resultTransaction);


                            var resultTransactions = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", transactionSelect.Payment.Id), HttpMethod.Get, Variables.LoginModel.Token);
                            Model.Payment payment = JsonConvert.DeserializeObject<Model.Payment>(resultTransactions);


                            try
                            {

                                if (payment.TaxReceipts.FirstOrDefault(x => x.Status == "ET001").Xml != null)
                                {
                                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Esta trasaccion esta timbrada", TypeIcon.Icon.Cancel);
                                    mensaje.ShowDialog();
                                }


                            }
                            catch (Exception)
                            {

                                Facturaelectronica fs = new Facturaelectronica();
                                //xmltimbrado = await fs.facturar(transactionSelect.Transaction.Id.ToString(), "ET001","");
                                fs.msgObservacionFactura = textoObservacion;
                                fs.msgUsos = textoUsos;
                                xmltimbrado = await fs.generaFactura(transactionSelect.Transaction.Id.ToString(), "ET001");
                                if (xmltimbrado.Contains("Success"))
                                {
                                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Pago se ha timbtado correctamente - UUid" + xmltimbrado.Split('-')[1], TypeIcon.Icon.Success);
                                    mensaje.ShowDialog();
                                }
                                else
                                {
                                mensaje = new MessageBoxForm(Variables.titleprincipal, "Error al realizar el timbrado" + xmltimbrado.Split('-')[1], TypeIcon.Icon.Success);
                                mensaje.ShowDialog();
                            }
                            //separadas = xmltimbrado.Split('/');
                            //if (separadas[0].ToString() == "error")
                            //{
                            //    mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                            //    mensaje.ShowDialog();
                            //}
                            //else
                            //{
                            //    PdfDocument pdfdocument = new PdfDocument();
                            //    pdfdocument.LoadFromFile(xmltimbrado);
                            //    pdfdocument.PrinterName = q.ImpresoraPredeterminada();
                            //    pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                            //    pdfdocument.PrintDocument.Print();
                            //    pdfdocument.Dispose();

                            //}

                        }

                            }

                            if(row.Cells["typeTransactionId"].Value.ToString() == "4")
                            {

                            var resultTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", transactionSelect.Transaction.Id), HttpMethod.Get, Variables.LoginModel.Token);
                            transactionSelect = JsonConvert.DeserializeObject<Model.TransactionPaymentVM>(resultTransaction);


                            var resultTransactions = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", transactionSelect.Payment.Id), HttpMethod.Get, Variables.LoginModel.Token);
                            Model.Payment payment = JsonConvert.DeserializeObject<Model.Payment>(resultTransactions);
                            try
                                {

                                    if (payment.TaxReceipts.FirstOrDefault(x => x.Status== "ET002").Xml != null)
                                    {
                                        mensaje = new MessageBoxForm(Variables.titleprincipal, "Esta trasaccion esta timbrada", TypeIcon.Icon.Cancel);
                                        mensaje.ShowDialog();
                                    }


                                }
                                catch (Exception)
                                {

                                    Facturaelectronica fs = new Facturaelectronica();
                                    xmltimbrado = await fs.facturar(transactionSelect.Transaction.Id.ToString(), "ET002", payment.TaxReceipts.FirstOrDefault().FielXML);
                                    separadas = xmltimbrado.Split('/');
                                    if (separadas[0].ToString() == "error")
                                    {
                                        mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                        mensaje.ShowDialog();
                                    }
                                    else
                                    {
                                    
                                    var resultTransactionss = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", transactionSelect.Payment.Id), HttpMethod.Get, Variables.LoginModel.Token);
                                    Model.Payment payments = JsonConvert.DeserializeObject<Model.Payment>(resultTransactionss);
                                    ExportGridToXML(payments.TaxReceipts.FirstOrDefault(x => x.Status == "ET002").Xml);
                                   }
                                }
                            }
                      
                        loading.Close();
                    
                }

                if (e.ColumnIndex == dgvMovimientos.Columns["Cancelar"].Index && e.RowIndex >= 0)
                {

                    if (row.Cells["typeTransactionId"].Value.ToString() == "3")
                    {
                        mensaje = new MessageBoxForm("¿Cancelar operación?", "Se enviará una solictud de autorización", TypeIcon.Icon.Warning, true);
                        result = mensaje.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            mensaje = new MessageBoxForm("¿Seguro de cancelar movimiento?", "Este proceso será irreversible", TypeIcon.Icon.Warning, true);
                            result = mensaje.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                loading = new Loading();
                                loading.Show(this);

                                var resultTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/{0}", transactionSelect.Transaction.Id), HttpMethod.Get, Variables.LoginModel.Token);
                                transactionSelect = JsonConvert.DeserializeObject<Model.TransactionPaymentVM>(resultTransaction);
                                var resultTransactions = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", transactionSelect.Payment.Id), HttpMethod.Get, Variables.LoginModel.Token);
                                Model.Payment payment = JsonConvert.DeserializeObject<Model.Payment>(resultTransactions);

                                Model.CancelPaymentVM cancelPaymentVM = new Model.CancelPaymentVM();
                                cancelPaymentVM.Transaction = new Model.TransactionVM();
                                cancelPaymentVM.Payment = new Model.Payment();
                                cancelPaymentVM.Transaction.Sign = false;
                                cancelPaymentVM.Transaction.Amount = transactionSelect.Transaction.Amount;
                                cancelPaymentVM.Transaction.Tax = transactionSelect.Transaction.Tax;
                                cancelPaymentVM.Transaction.Rounding = transactionSelect.Transaction.Rounding;
                                cancelPaymentVM.Transaction.Total = transactionSelect.Transaction.Total;
                                cancelPaymentVM.Transaction.Aplication = transactionSelect.Transaction.Aplication;
                                cancelPaymentVM.Transaction.TypeTransactionId = 4;
                                cancelPaymentVM.Transaction.PayMethodId = transactionSelect.Transaction.PayMethodId;
                                cancelPaymentVM.Transaction.TerminalUserId = transactionSelect.Transaction.TerminalUserId;
                                cancelPaymentVM.Transaction.Cancellation = transactionSelect.Transaction.Folio;
                                cancelPaymentVM.Transaction.AuthorizationOriginPayment = transactionSelect.Transaction.AuthorizationOriginPayment;
                                cancelPaymentVM.Transaction.OriginPaymentId = transactionSelect.Transaction.OriginPaymentId;
                                cancelPaymentVM.Transaction.ExternalOriginPaymentId = transactionSelect.Transaction.ExternalOriginPaymentId;
                                cancelPaymentVM.Transaction.PaytStatus = "EP002";
                                cancelPaymentVM.Transaction.transactionDetails = transactionSelect.Transaction.TransactionDetails.ToList();
                                cancelPaymentVM.Payment = transactionSelect.Payment;

                                string valores = JsonConvert.SerializeObject(cancelPaymentVM);
                                string url = string.Empty;
                                content = new StringContent(valores, Encoding.UTF8, "application/json");
                                var xxx = cancelPaymentVM.Payment.PaymentDetails.Where(x => x.Type == "TIP02");
                                if (cancelPaymentVM.Payment.Account.Contains("-"))
                                {
                                    url = "/api/Transaction/Orders/Cancel/{0}";
                                }


                                else if (transactionSelect.Payment.Type == "PAY06" || transactionSelect.Payment.Type == "PAY04")
                                    url = "/api/Transaction/Prepaid/Cancel/{0}";
                                else
                                    url = "/api/Transaction/Cancel/{0}";

                                var resultado = await Requests.SendURIAsync(string.Format(url, transactionSelect.Transaction.Id), HttpMethod.Post, Variables.LoginModel.Token, content);
                               
                                if (resultado.Contains("error"))
                                {
                                    mensaje = new MessageBoxForm("Error", resultado.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                                    result = mensaje.ShowDialog();
                                }
                                else
                                {
                                    mensaje = new MessageBoxForm("Transacción Exitosa", "La trasaccion se ha cancelado", TypeIcon.Icon.Success);
                                    result = mensaje.ShowDialog();

                                    string xmltimbrado = string.Empty;
                                    
                                    try
                                    {

                                        if (payment.TaxReceipts.FirstOrDefault(x => x.Status == "ET002").Xml != null)
                                        {
                                            mensaje = new MessageBoxForm(Variables.titleprincipal, "Esta trasaccion esta timbrada", TypeIcon.Icon.Cancel);
                                            mensaje.ShowDialog();
                                        }
                                        
                                    }
                                    catch (Exception)
                                    {
                                        if(payment.TaxReceipts.Count() > 0)
                                        {
                                            Facturaelectronica fst = new Facturaelectronica();
                                            xmltimbrado = await fst.facturar(transactionSelect.Transaction.Id.ToString(), "ET002", payment.TaxReceipts.FirstOrDefault().FielXML);
                                            separadas = xmltimbrado.Split('/');
                                            if (separadas[0].ToString() == "error")
                                            {
                                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                                mensaje.ShowDialog();
                                            }
                                            else
                                            {

                                                var resultTransactionss = await Requests.SendURIAsync(string.Format("/api/Payments/{0}", transactionSelect.Payment.Id), HttpMethod.Get, Variables.LoginModel.Token);
                                                Model.Payment payments = JsonConvert.DeserializeObject<Model.Payment>(resultTransactionss);
                                                ExportGridToXML(payments.TaxReceipts.FirstOrDefault(x => x.Status == "ET002").Xml);
                                            }
                                        }
                                      
                                    }

                                    await cargar();
                                }
                                loading.Close();
                            }
                        }
                    }
                }
            }
        }

        private async void dgvMovimientos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
               
               DataGridViewRow row = this.dgvMovimientos.Rows[e.RowIndex];
               
               if (row.Cells["typeTransactionId"].Value.ToString() == "3")
               {
               cuadritos3 = new viewdetails(
               dgvMovimientos.SelectedRows[0].Cells["FolioImpresion"].Value.ToString(),
               dgvMovimientos.SelectedRows[0].Cells["ID"].Value.ToString(),
               dgvMovimientos.SelectedRows[0].Cells["namepayMethod"].Value.ToString(),
               dgvMovimientos.SelectedRows[0].Cells["Folio"].Value.ToString(),
               dgvMovimientos.SelectedRows[0].Cells["tax"].Value.ToString(),
               dgvMovimientos.SelectedRows[0].Cells["rounding"].Value.ToString(),
               dgvMovimientos.SelectedRows[0].Cells["total"].Value.ToString(),
               dgvMovimientos.SelectedRows[0].Cells["amount"].Value.ToString()
               );

                    loading = new Loading();
                    loading.Show(this);
                    cuadritos3.ShowDialog();
                    loading.Close();
               }

            }
        }

        private async void corteDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ObtenerCorte();
            if (Properties.Settings.Default.Printer == true)
            {
                Tiraauditora imp = new Tiraauditora();
                imp.Imprime(dt, 1, totalt.ToString());
            }
            else
            {
                Variables.optionvistaimpresion = 2;
                impresionhoja();
            }
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
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xml);
                xdoc.Save(File.OpenWrite(SaveXMLFileDialog.FileName));
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

        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    // You could potentially name the column based on the DGV column name (beware of dupes)
                    // or assign a type based on the data type of the data bound to this DGV column.
                    dt.Columns.Add();
                }
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }

            return dt;
        }

        private async void TxtSearchFolio_TextChanged(object sender, EventArgs e)
        {
            DataTable table = GetDataTableFromDGV(dgvMovimientos);
            DataView dataView = new DataView(table);
            dataView.RowFilter = string.Format("FOLIO LIKE '%{0}%'", txtSearchFolio.Text);
            dgvMovimientos.DataSource = dataView;
            dgvMovimientos.Refresh();
            await Total();
        }

        
    }
}
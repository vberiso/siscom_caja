using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Services;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI
{
    public partial class FacturaAgrupada : Form
    {
        Form loading;
        private RequestsAPI Requests = null;
        DialogResult result = new DialogResult();
        Form mensaje;

        private string UrlBase = Properties.Settings.Default.URL;
        TrasactionVMA transactions = new TrasactionVMA();
        querys q = new querys();
        
        public FacturaAgrupada()
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

        
        private async Task cargaCombos()
        {
            var resultTypeTransaction = await Requests.SendURIAsync("/api/BranchOffice/", HttpMethod.Get, Variables.LoginModel.Token);
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
                typeTransaction.Name = "Todos los Cobros";
                Types.Add(typeTransaction);

                cmbTypeTransaction.ValueMember = "id";
                cmbTypeTransaction.DisplayMember = "name";

                cmbTypeTransaction.DataSource = Types;
                cmbTypeTransaction.SelectedIndex = 0;
            }
        }

        private async void FacturaAgrupada_Load(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            alzheimer();
            centraX(pnlHeader, cmbTypeTransaction);
            await cargaCombos();
            await cargar();
            loading.Close();
        }

        private void centraX(Control padre, Control hijo)
        {
            int x = 0;
            x = (padre.Width / 2) - (hijo.Width / 2);
            hijo.Location = new System.Drawing.Point(x, hijo.Location.Y);
        }

        #region PrivateMethod
        public async Task cargar()
        {

            var _resulTransaction = await Requests.SendURIAsync(string.Format("/api/Transaction/TransactionPaymentWithoutFactura/{0}/{1}/{2}", dateTimePicker1.Value.ToString("yyyy-MM-dd"), cmbTypeTransaction.SelectedValue.ToString(),3), HttpMethod.Get, Variables.LoginModel.Token);

            if (_resulTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", _resulTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var source = new BindingSource();
                transactions = JsonConvert.DeserializeObject<TrasactionVMA>(_resulTransaction);

                if (transactions == null)
                {
                    mensaje = new MessageBoxForm("Sin Operaciones", "No se han encontrado movimientos en la sucursal.", TypeIcon.Icon.Warning);
                    result = mensaje.ShowDialog();
                }
                else
                {
                    
                var transactionsFilter = transactions.lstTransaction.Where(x=>x.typeTransactionId==3 && x.tax ==0)
                                                       .Select(s => new
                                                       {
                                                           
                                                           id = s.id,
                                                           typeTransaction = s.typeTransaction,
                                                           folioImpresion = s.transactionFolios.Count > 0 ? s.transactionFolios.First().folio.ToString() : String.Empty,
                                                           dateTransaction = s.dateTransaction.ToString("HH:mm"),
                                                           sign = s.sign,
                                                           amount = s.amount,
                                                           tax = s.tax,
                                                           rounding = s.rounding,
                                                           total = s.total,
                                                           folio = s.folio,
                                                           typeTransactionId = s.typeTransactionId
                                                       }).ToList();
                    source.DataSource = transactionsFilter;
                }

               dgvMovimientos.DataSource = source;
               dgvMovimientos.Refresh();
            }
        }
        #endregion
        
        private async void cmbTypeTransaction_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            await cargar();
            loading.Close();
        }

        private async void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            await cargar();
            loading.Close();
        }
        
        private async void corteDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
                string xmltimbrado = string.Empty;
                string[] separadas;
                loading = new Loading();
                loading.Show(this);

            if (dgvMovimientos.Rows.Count>0)
            { 

                Facturacionagrupada fs = new Facturacionagrupada();
                xmltimbrado = await fs.facturar("ET001", "",transactions,false,"");
                separadas = xmltimbrado.Split('/');
                if (separadas[0].ToString() == "error")
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                }
                else
                {
                    PdfDocument pdfdocument = new PdfDocument();
                    pdfdocument.LoadFromFile(xmltimbrado);
                    pdfdocument.PrinterName = q.ImpresoraPredeterminada();
                    pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                    pdfdocument.PrintDocument.Print();
                    pdfdocument.Dispose();

                }
            await cargar();
            }
            else
            {
                mensaje = new MessageBoxForm("Sin Operaciones", "No se han encontrado movimientos en la sucursal.", TypeIcon.Icon.Warning);
                result = mensaje.ShowDialog();

            }
            loading.Close();
        }

    }
}
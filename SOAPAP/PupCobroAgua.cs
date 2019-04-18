using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Humanizer;
using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP
{
    public partial class PupCobroAgua : Form
    {

        public PupCobroAgua()
        {
            InitializeComponent();
        }

        querys q = new querys();
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
        string taxableBase = string.Empty;
        string ground = string.Empty;
        string built = string.Empty;
        string sa = string.Empty;
        DataTable dt = new DataTable();
        DataTable recibe = new DataTable();
        string k = string.Empty;
        Form mensaje;
        Form cuadritos;
        Form cuadritos2;

        public void cargardetalle()
        {
            Recibos.DataSource = recibe;
            Recibos.AutoGenerateColumns = false;
            Recibos.AllowUserToAddRows = false;
            Recibos.AutoGenerateColumns = false;
        }
   

        public async void cargars()
        {
            Form loading = new Loading();
            loading.Show(this);
            string[] separadas;
            dt = await q.GETAgreementsbyaccount("/api/Agreements/AgreementByAccount/" + Variables.cuenta + "");
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    separadas = row[0].ToString().Split('/');
                    if (separadas[0].ToString() == "error")
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                        mensaje.ShowDialog();
                        dt.Rows.Clear();
                        break;
                    }
                }
            }

            else
            {
                mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                mensaje.ShowDialog();
            }


            foreach (DataRow row in dt.Rows)
            {
                idp = row[0].ToString();
                cuentap = row[1].ToString();
                namesp = row[2].ToString();
                rfcp = row[3].ToString();
                direccionp = row[4].ToString();
                numDerivatives = row[5].ToString();
                typeService = row[6].ToString();
                typeConsume = row[7].ToString();
                typeRegime = row[8].ToString();
                typePeriod = row[9].ToString();
                typeStateService = row[10].ToString();
                typeIntake = row[11].ToString();
                taxableBase = row[12].ToString();
                ground = row[13].ToString();
                built = row[14].ToString();
            }


            Variables.idagrement = idp;
            label8.Text = idp;
            label1.Text = cuentap;
            label2.Text = namesp;
            label3.Text = rfcp;
            label4.Text = direccionp;
            label12.Text = numDerivatives;
            label20.Text = typeService;
            label21.Text = typeConsume;
            label15.Text = typeRegime;
            label16.Text = typePeriod;
            label22.Text = typeStateService;
            label17.Text = typeIntake;
            label27.Text = typeIntake;
            label29.Text = taxableBase;
            label28.Text = ground;
            label30.Text = built;
            recibe = await q.GetRecibos("api/Debts/GetDebtByPeriod/" + Variables.idagrement + "");

            if (recibe != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    separadas = row[0].ToString().Split('/');
                    if (separadas[0].ToString() == "error")
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                        mensaje.ShowDialog();
                        dt.Rows.Clear();
                        break;
                    }
                }
            }
            cargardetalle();
            DataTable dt1;
            dt1 = await q.GETPaymentHistory("/api/PaymentHistory/" + Variables.idagrement + "");
            Pagos.DataSource = dt1;
            DataTable dt2;
            dt2 = await q.GetAnticipos("/api/Notifications/" + Variables.idagrement + "");
            Notify.DataSource = dt2;

            DataTable dt3;
            dt3 = await q.GetNotify("/api/Prepaid/" + Variables.idagrement + "");
            Anticipos.DataSource = dt3;
            
            loading.Close();

        }

        public  void cobroagua_Load(object sender, EventArgs e)
        {
            label5.Parent = pictureBox12;
            pictureBox1.Parent = pictureBox12;
            

            DataColumn column;
            column = new DataColumn();
           
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Array4";
            recibe.Columns.Add(column);

            if (Variables.Configuration.IsMunicipal != true)
            {
                tableLayoutPanel10.Visible = false;
                tabControl1.TabPages[1].Hide();
                tableLayoutPanel1.Location = new System.Drawing.Point(14, 377);
            }
          
            


            //if (Variables.Configuration.IsMunicipal != true)
            //{

            //    tableLayoutPanel10.Visible = false;
            //    tabControl1.TabPages[1].Hide();
            //    tableLayoutPanel1.Location = new System.Drawing.Point(14, 377);
              

            //    tabControl1.TabPages.Remove(tabPage10);
            //    tabControl1.TabPages.Remove(tabPage11);


            //}
            //else
            //{
            //    tabControl1.TabPages.Remove(tabPage12);
            //    tabControl1.TabPages.Remove(tabPage13);
               

            //}

            cargars();



           
        }

        private void tabPage10_Click(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Recibos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.Recibos.Rows[e.RowIndex];
              
               // MessageBox.Show(row.Cells["Array"].Value.ToString());
                cuadritos =  new results(row.Cells["Array"].Value.ToString());
                cuadritos.ShowDialog();
            }


           

            }

            private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void tabPage11_Click(object sender, EventArgs e)
        {

        }

        private void Notify_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Notify_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.Notify.Rows[e.RowIndex];
                cuadritos2 = new PopNotificacion(row.Cells["Array2"].Value.ToString());
                cuadritos2.ShowDialog();
            }


        }

        private void Anticipos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.Anticipos.Rows[e.RowIndex];
                cuadritos2 = new PopAnticipo(row.Cells["Array1"].Value.ToString());
                cuadritos2.ShowDialog();
            }

        }

        private void tabPage13_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void Notify_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Recibos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
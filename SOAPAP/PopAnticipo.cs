using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP
{
    public partial class PopAnticipo : Form
    {
        List<prepaidDetails> debs = new List<prepaidDetails>();
        DataTable dt = new DataTable();
        Form cuadritos2;
       
        public PopAnticipo(string s)
        {
            InitializeComponent();
            
            dataGridView1.Columns["prepaidDetailDate"].Width = (dataGridView1.Width*50)/100;
            dataGridView1.Columns["amount"].Width = (dataGridView1.Width * 50) / 100;
            //dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "";
            DataColumn column;


            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "prepaidDetailDate";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "amount";
            dt.Columns.Add(column);
            string k = string.Empty;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Array";
            dt.Columns.Add(column);
            debs = JsonConvert.DeserializeObject<List<prepaidDetails>>(s);


            foreach (var rows in debs)
            {

                DataRow row = dt.NewRow();
                row["amount"] = rows.amount;
                row["prepaidDetailDate"] = rows.prepaidDetailDate;
                k = JsonConvert.SerializeObject(rows.debtPrepaids);
                row["Array"] = k;
                
                dt.Rows.Add(row);
            }

            dataGridView1.DataSource = dt;
        }

        private void PopAnticipo_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
               
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlButtons_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void dataGridView1_CellDoubleClick_2(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //this.Visible = false;
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                cuadritos2 = new PopAnticipo2(row.Cells["Array"].Value.ToString());

                cuadritos2.ShowDialog();

            }

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}

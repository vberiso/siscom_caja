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
    public partial class PopAnticipo2 : Form
    {
        List<debtPrepaids> debs = new List<debtPrepaids>();
        DataTable dt = new DataTable();
        Form cuadritos2;
        public PopAnticipo2(string s)
        {
            InitializeComponent();
            dataGridView1.Columns["nameConcept"].Width = (dataGridView1.Width * 70) / 100;
            dataGridView1.Columns["originalAmount"].Width = (dataGridView1.Width * 30) / 100;

           // dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataColumn column;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "";

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nameConcept";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "originalAmount";
            dt.Columns.Add(column);
            debs = JsonConvert.DeserializeObject<List<debtPrepaids>>(s);


            foreach (var rows in debs)
            {
              
                DataRow row = dt.NewRow();
                row["originalAmount"] = rows.originalAmount;
                row["nameConcept"] = rows.nameConcept;
                
                dt.Rows.Add(row);
            }


            dataGridView1.DataSource = dt;
        }

        private void PopAnticipo2_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class PopNotificacion : Form
    {
        List<notificationDetails> debs = new List<notificationDetails>();
        DataTable dt = new DataTable();
        public PopNotificacion(string s)
        {
            InitializeComponent();
            DataColumn column;


            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nameConcept";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "amount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "checkbox";
            dt.Columns.Add(column);
            debs = JsonConvert.DeserializeObject<List<notificationDetails>>(s);

            int x = 0; 
            foreach (var rows in debs)
            {

                DataRow row = dt.NewRow();             
                row["amount"] = rows.amount;               
                row["nameConcept"] = rows.nameConcept;
                row["checkbox"] = x.ToString();
                dt.Rows.Add(row);
                x++;
            }


            dataGridView1.DataSource = dt;
        

    }

        private void PopNotificacion_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            MessageBox.Show(Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells["checkbox"].Value).ToString());
            
        }
    }
}

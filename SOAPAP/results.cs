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
    public partial class results : Form
    {
        
        
        List<Debtdetails> debs = new List<Debtdetails>();
        DataTable dt = new DataTable();
        public results(string s)
        {
            InitializeComponent();

            dataGridView1.Columns["prepaidDetailDate"].Width = (dataGridView1.Width * 60) / 100;
            dataGridView1.Columns["amount"].Width = (dataGridView1.Width * 20) / 100;
            dataGridView1.Columns["onAccount"].Width = (dataGridView1.Width * 20) / 100;
            //dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataColumn column;
          

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "amount";
            dt.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "onAccount";
            dt.Columns.Add(column);

            

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nameConcept";
            dt.Columns.Add(column);

       






            debs = JsonConvert.DeserializeObject<List<Debtdetails>>(s);
            decimal total = 0;
            decimal tota1 = 0;
            foreach (var rows in debs)
            {

                DataRow row = dt.NewRow();
                total += Convert.ToDecimal(rows.amount);
                tota1 += Convert.ToDecimal(rows.onAccount);
                row["amount"] = rows.amount;
                row["onAccount"] = rows.onAccount;
               // row["onPayment"] = rows.onPayment;
                //row["haveTax"] = rows.haveTax;
               // row["codeConcept"] = rows.codeConcept;
                row["nameConcept"] = rows.nameConcept;

               // row["debtId"] =  rows.debtId;
               
               

                dt.Rows.Add(row);

                /*
                 
             "id": 651,
                "amount": 150,
                "onAccount": 0,
                "onPayment": 0,
                "haveTax": false,
                "codeConcept": "1",
                "nameConcept": "PREDIAL",
                "debtId": 326
             */

            }

            DataRow rod = dt.NewRow();
            rod["amount"] =total.ToString();
            rod["onAccount"] = tota1.ToString();
            rod["nameConcept"] = "TOTAL";
            dt.Rows.Add(rod);

        }

        private void results_Load(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = dt;

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

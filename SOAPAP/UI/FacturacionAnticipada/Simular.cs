using Newtonsoft.Json.Linq;
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

namespace SOAPAP.UI.FacturacionAnticipada
{
    public partial class Simular : Form
    {

        private RequestsAPI Requests = null;
        private int AgreementId;
        private int MesInicio;
        private int MesIFin;
        private int Year;
        private string UrlBase = Properties.Settings.Default.URL;
        public Simular(int AgreementId, int MesInicio, int MesFin, int Year)
        {
            InitializeComponent();
            this.AgreementId = AgreementId;
            this.MesInicio = MesInicio;
            this.MesIFin = MesFin;
            this.Year = Year;
            Requests = new RequestsAPI(UrlBase);
            loadDataInTable();
           
            

        }

        private void btnAccept_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void loadDataInTable()
        {
            dataGridViewServicios.ColumnCount = 5;
            dataGridViewServicios.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridViewServicios.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
           
            // Set the column header names.
            dataGridViewServicios.Columns[0].Name = "Servicio";
            dataGridViewServicios.Columns[1].Name = "Precio";
            dataGridViewServicios.Columns[2].Name = "Meses a pagar";
            dataGridViewServicios.Columns[3].Name = "Total";
            dataGridViewServicios.Columns[4].Name = "Iva";

            decimal total = 0;
            decimal taltalIva = 0;
            
            var url = string.Format("/api/StoreProcedure/runAccrualPeriod/{0}/{1}/{2}/{3}/{4}", AgreementId, MesInicio, MesIFin, Year, 1);
            var results = await Requests.SendURIAsync(url, HttpMethod.Post, Variables.LoginModel.Token);
            var data = JObject.Parse(results)["data"]["data"];
            foreach (var  rowArray in data)
            {     
                decimal totalMeses = Convert.ToDecimal(MesIFin - (MesInicio-1));
                decimal iva = 0;
                if (Convert.ToBoolean(rowArray["have_tax"]))
                     iva = Math.Round((Convert.ToDecimal(rowArray["amount"].ToString()) * totalMeses) * Convert.ToDecimal(Variables.Configuration.IVA) / 100,2);
                dataGridViewServicios.Rows.Add(new string[] { rowArray["name_concept"].ToString(), rowArray["amount"].ToString() , totalMeses.ToString(), (totalMeses * Convert.ToDecimal(rowArray["amount"].ToString())).ToString(), iva.ToString() });
                if (Convert.ToBoolean(rowArray["have_tax"]))
                     taltalIva = Convert.ToDecimal(rowArray["amount"].ToString()) * totalMeses; 
                else
                    total +=  Convert.ToDecimal(rowArray["amount"].ToString()) * totalMeses;
            }
            taltalIva = taltalIva - Math.Round( taltalIva * Convert.ToDecimal(Variables.Configuration.IVA) / 100,2);
            lblTotal.Text = Math.Round(taltalIva + total).ToString();
            lblIva.Text = Math.Round(Math.Round(taltalIva * Convert.ToDecimal(Variables.Configuration.IVA) / 100, 2)).ToString();
        }
    }
}

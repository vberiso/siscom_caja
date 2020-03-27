using Newtonsoft.Json;
using SOAPAP.Enums;
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

namespace SOAPAP.UI.Condonations
{
    public partial class Adjusment : Form
    {
        private decimal _Total { get; set; }
        private List<Model.Debt> _Debts { get; set; }
        private List<Model.OrderSale> _OrderSale { get; set; }
        private RequestsAPI Requests = null;

        Form loading;

        public Adjusment()
        {
            InitializeComponent();
        }

        public Adjusment(decimal Total, List<Model.Debt> Debts)
        {
            InitializeComponent();
            _Total = Total;
            _Debts = Debts;
            Requests = new RequestsAPI(Properties.Settings.Default.URL);
        }

        public Adjusment(decimal Total, List<Model.OrderSale> orderSales)
        {
            InitializeComponent();
            _Total = Total;
            _OrderSale = orderSales;
            Requests = new RequestsAPI(Properties.Settings.Default.URL);
        }

        private void Adjusment_Load(object sender, EventArgs e)
        {
            List<DetalleCondonations> lstDC = new List<DetalleCondonations>();
            if(_Debts != null)
            {
                foreach(var item in _Debts)
                {                    
                    foreach(var subitem in item.DebtDetails)
                    {
                        DetalleCondonations detalleCondonations = new DetalleCondonations();
                        detalleCondonations.IdDebt = item.Id;
                        detalleCondonations.FromDate = item.FromDate;
                        detalleCondonations.UntilDate = item.UntilDate;
                        detalleCondonations.Year = item.Year;
                        detalleCondonations.IdDebtDetail = subitem.Id;
                        detalleCondonations.CodeConcept = subitem.CodeConcept;
                        detalleCondonations.Name = item.Type.Contains("TIP01") ? subitem.NameConcept + " de: " + item.FromDate.ToString("dd/MM/yyyy") + " a: " + item.UntilDate.ToString("dd/MM/yyyy") : subitem.NameConcept ;
                        detalleCondonations.Amount = subitem.Amount;
                        detalleCondonations.OnAccount = subitem.OnAccount;
                        detalleCondonations.Total = subitem.Amount - subitem.OnAccount;
                        lstDC.Add(detalleCondonations);
                    }
                }
            }
            else
            {
                foreach (var item in _OrderSale)
                {
                    foreach (var subitem in item.OrderSaleDetails)
                    {
                        DetalleCondonations detalleCondonations = new DetalleCondonations();
                        detalleCondonations.IdDebt = item.Id;                        
                        detalleCondonations.Year = item.Year;
                        detalleCondonations.IdDebtDetail = subitem.Id;
                        detalleCondonations.CodeConcept = subitem.CodeConcept;
                        detalleCondonations.Name = subitem.Description;
                        detalleCondonations.Amount = subitem.Amount;
                        detalleCondonations.OnAccount = subitem.OnAccount;
                        detalleCondonations.Total = subitem.Amount - subitem.OnAccount;
                        lstDC.Add(detalleCondonations);
                    }
                }
            }

            grcDetalles.DataSource = lstDC;
            lblTotal.Text = _Total.ToString();
        }

        //Boton aceptar
        private async void button1_Click(object sender, EventArgs e)
        {
            if(tbxComentarios.Text.Length < 10)
            {
                var mensaje = new MessageBoxForm("Agrega un comentario", "Es necesario agregar un comentario de cancelación.", TypeIcon.Icon.Warning, true);
                var result = mensaje.ShowDialog();
            }
            else
            {
                var mensaje = new MessageBoxForm("Advertencia", "Esta a punto de condonar los conceptos mostrados. El proceso sera irreversible. ¿Deseas continuar?", TypeIcon.Icon.Warning, true);
                var result = mensaje.ShowDialog();
                if (result == DialogResult.OK)
                {
                    loading = new Loading();
                    loading.Show(this);

                    var content = new StringContent(JsonConvert.SerializeObject(_Debts.Select(d => d.Id).ToList()), Encoding.UTF8, "application/json");
                    var resultUpdateDebt = await Requests.SendURIAsync(string.Format("/api/Debts/Condonation/{0}/{1}", Variables.LoginModel.User, tbxComentarios.Text), HttpMethod.Post, Variables.LoginModel.Token, content);
                    if (resultUpdateDebt.Contains("\"error"))
                    {
                        loading.Close();
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultUpdateDebt).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        loading.Close();
                        mensaje = new MessageBoxForm("Condonación exitosa", "La condonación se ha realizado satisfactoriamente.", TypeIcon.Icon.Success);
                        result = mensaje.ShowDialog();
                        this.Close();
                    }
                }
            }                      
        }

        //Boton Cancelar
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

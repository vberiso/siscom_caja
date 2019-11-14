using SOAPAP.UI;
using SOAPAP.UI.FacturacionAnticipada;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks; 
using System.Windows.Forms;

namespace SOAPAP
{
    public partial class Anticipo : Form
    {
        private int agreement_id;
        private Model.Agreement Agreement;
         
        public Anticipo()
        {
            
            InitializeComponent();
        }
        public void setAgreement(Model.Agreement Agreement)
        {
            this.agreement_id = Agreement.Id;
            this.Agreement = Agreement;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PeriodosAnticipados Uiperiodos = new PeriodosAnticipados(Agreement);
            var result = Uiperiodos.ShowDialog(this);
            Uiperiodos.Close();
            
            this.DialogResult = result;
            this.Close();
            //Variables.anticipo = 2;
            //Variables.oprtions = true;
            //IForm formInterface = this.Owner as IForm;

            //if (formInterface != null)
            //{
            //    formInterface.ShowForm("SOAPAP", "cobroagua");
            //}

            //UI.Cobro Return = ((UI.Cobro)this.Owner.OwnedForms.Where(x => x.Name == "Cobro").FirstOrDefault());
            //Return.AddPrepaid();

            //Close();
            //this.DialogResult = DialogResult.OK;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Variables.Configuration.Anual = true;
            PeriodosAnticipados Uiperiodos = new PeriodosAnticipados(Agreement, true);
            var result = Uiperiodos.ShowDialog(this);
            Uiperiodos.Close();

            this.DialogResult = result;
            this.Close();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Anticipo_Load(object sender, EventArgs e)
        {
            if (Variables.Configuration.Anual)
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }
    }
}

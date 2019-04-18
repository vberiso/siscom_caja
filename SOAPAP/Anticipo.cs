using SOAPAP.UI;
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
        public Anticipo()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
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
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Variables.anticipo = 1;
            //Variables.oprtions = true;
            //IForm formInterface = this.Owner as IForm;

            //if (formInterface != null)
            //{
            //    formInterface.ShowForm("SOAPAP", "cobroagua");
            //}
            //UI.Cobro Return = ((UI.Cobro)this.Owner.OwnedForms.Where(x => x.Name == "Cobro").FirstOrDefault());
            //Return.CalculateAnual();
            //Close();
            this.DialogResult = DialogResult.Yes;

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

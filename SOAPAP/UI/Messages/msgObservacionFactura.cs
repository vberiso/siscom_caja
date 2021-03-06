using SOAPAP.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI.Messages
{
    public partial class msgObservacionFactura : Form
    {
        public string TextoObservacion { get; set; }
        public string Usos { get; set; }

        public msgObservacionFactura()
        {
            InitializeComponent();
        }

        public msgObservacionFactura(string pCorreo)
        {
            InitializeComponent();
            tbxCorreo.Text = pCorreo;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                TextoObservacion = tbxMensage.Text;
                
                if(chbxEnviarCorreo.Checked == true)
                {
                    if (string.IsNullOrEmpty(tbxCorreo.Text))
                    {
                        lblMensajeCorreo.Text = "Debe ingresar una dirección de correo.";
                    }
                    else
                    {
                        var eMailValidator = new System.Net.Mail.MailAddress(tbxCorreo.Text);
                        this.Close();
                    }                    
                }
                else
                {                    
                    this.Close();
                }                
            }
            catch (FormatException ex)
            {
                lblMensajeCorreo.Text = "Debe especificar una dirección de correo valida.";
            }            
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {
            //Combo Areas o Concepto de pago
            List<DataComboBox> lstUsos = new List<DataComboBox>();
            lstUsos.Add(new DataComboBox() { keyString = "P01", value = "Por definir" });
            lstUsos.Add(new DataComboBox() { keyString = "G03", value = "Gastos en general" });
            cbxUsoCFDI.ValueMember = "keyString";
            cbxUsoCFDI.DisplayMember = "value";
            cbxUsoCFDI.DataSource = lstUsos;
            cbxUsoCFDI.SelectedIndex = 0;

            Usos = "P01 - Por definir";
        }

        private void cbxUsoCFDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            Usos = ((DataComboBox)cbxUsoCFDI.SelectedItem).keyString + " - " + ((DataComboBox)cbxUsoCFDI.SelectedItem).value;

        }

        private void tbxMensage_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

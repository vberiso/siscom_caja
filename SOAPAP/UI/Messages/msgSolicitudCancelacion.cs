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
    public partial class msgSolicitudCancelacion : Form
    {
        public string TextoMotivo { get; set; }
        public string Usos { get; set; }
        public msgSolicitudCancelacion()
        {
            InitializeComponent();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if(tbxMotivo.TextLength < 5)
            {
                lblMensajeTextoRequerido.Visible = true;
            }
            else
            {
                lblMensajeTextoRequerido.Visible = false;
                TextoMotivo = tbxMotivo.Text;
                this.Close();
            }            
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            TextoMotivo = null;
            this.Close();
        }
    }
}

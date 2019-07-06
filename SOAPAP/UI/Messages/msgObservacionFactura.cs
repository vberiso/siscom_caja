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

        public msgObservacionFactura()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            TextoObservacion = tbxMensage.Text;
            this.Close();
        }
    }
}

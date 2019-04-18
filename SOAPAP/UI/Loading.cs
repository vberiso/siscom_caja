using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form Return = (Form)this.Owner;
            Return.Enabled = true;
            Return.ShowIcon = false;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            Form Return = (Form)this.Owner;
            Return.Enabled = false;
        }
    }
}

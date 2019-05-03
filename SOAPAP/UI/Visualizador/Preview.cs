using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI.Visualizador
{
    public partial class Preview : Form
    {
        private string PathResource { get; set; }
        public Preview(string path)
        {
            InitializeComponent();
            PathResource = path;
        }

        private void Preview_Load(object sender, EventArgs e)
        {
            pdfViewer1.LoadDocument(PathResource);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI.Visualizador
{
    public partial class Preview : Form
    {
        private string PathResource { get; set; }
        private Stream StreamReader { get; set; }
        public Preview(string path)
        {
            InitializeComponent();
            PathResource = path;
        }
        public Preview(Stream StreamReader)
        {
            InitializeComponent();
            this.StreamReader = StreamReader;
        }

        private void Preview_Load(object sender, EventArgs e)
        {
           
            if (StreamReader == null)
            {
                pdfViewer1.LoadDocument(PathResource);
            }
            else
            {
                pdfViewer1.LoadDocument(StreamReader);
                       
            }
        }

        

        private void btnCobrar_Click(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            pdfViewer1.Print();
        }
    }
}

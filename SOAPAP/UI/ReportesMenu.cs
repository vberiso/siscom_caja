using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI
{
    public partial class ReportesMenu : Form
    {
        public ReportesMenu()
        {
            InitializeComponent();
        }

        #region IForm Members
        Control PanelContenido;

        public void ShowForm(string nameSpace, string nameForm)
        {            
            Type t = Type.GetType(nameSpace + "." + nameForm);
            Form newForm = Activator.CreateInstance(t) as Form;
            newForm.Owner = this;
            AddFormInPanel(newForm);
        }

        private void AddFormInPanel(Form fh)
        {
            PanelContenido = this.Parent;
            if (PanelContenido.Controls.Count > 0)
                PanelContenido.Controls.RemoveAt(0);
            fh.Visible = false;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;

            PanelContenido.Controls.Add(fh);
            PanelContenido.Tag = fh;
            fh.Show();
        }
        #endregion
         
        private void button2_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.ReportesForms.RepIFB");
        }

        private void btnIPC_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.ReportesForms.RepIBC");            
        }

        private void btnCF_Click(object sender, EventArgs e)
        {
            ShowForm("SOAPAP", "UI.ReportesForms.RepPadronWater");
        }
    }
}

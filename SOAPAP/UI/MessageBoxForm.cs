using SOAPAP.Enums;
using SOAPAP.Properties;
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
    public partial class MessageBoxForm : Form
    {
        private string v1;
        private TypeIcon.Icon warning;
        private bool v2;

        public MessageBoxForm(string Title, string Message, TypeIcon.Icon TypeIcon, bool ShowButtonCancel = false)
        {
            InitializeComponent();
            lblTitle.Text = Title;
            txtMessage.Text = Message;

            switch (TypeIcon)
            {
                case SOAPAP.Enums.TypeIcon.Icon.Success:
                    pbxIcon.Image = Resources.confirm;
                    break;
                case SOAPAP.Enums.TypeIcon.Icon.Cancel:
                    pbxIcon.Image = Resources.calcel;
                    break;
                case SOAPAP.Enums.TypeIcon.Icon.Info:
                    pbxIcon.Image = Resources.info;
                    break;
                case SOAPAP.Enums.TypeIcon.Icon.Warning:
                    pbxIcon.Image = Resources.warnning;
                    break;
            }

            //centraX(pbxIcon, pnlImagen);
            //centraX(pnlTitle, lblTitle);

          
            if (ShowButtonCancel)
            {
                btnCancelar.Visible = true;
                btnCancelar.Location = new Point(302, 5);
                btnAceptar.Location = new Point(198, 5);
            }
            else
            {
                btnAceptar.Location = new Point(302, 5);
            }

            //Por si el mensaje es muy largo.
            if (Message.Length > 150)
                this.Height = 350;
        }

        public MessageBoxForm(string v1, TypeIcon.Icon warning, bool v2)
        {
            this.v1 = v1;
            this.warning = warning;
            this.v2 = v2;
        }

        private void centraX(Control padre, Control hijo)
        {
            int x = 0;

            x = (padre.Width / 2) - (hijo.Width / 2);
            hijo.Location = new System.Drawing.Point(x, hijo.Location.Y);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

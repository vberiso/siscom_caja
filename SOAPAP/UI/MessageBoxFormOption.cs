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
    public partial class MessageBoxFormOption : Form
    {
        public MessageBoxFormOption(string Title, string Message, TypeIcon.Icon TypeIcon, bool ShowButtonCancel = false)
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


        }


        private void MessageBoxFormOption_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            IForm formInterface = this.Owner as IForm;
            if (formInterface != null)
            {

                formInterface.ShowForm("SOAPAP", "cobroagua");
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            IForm formInterface = this.Owner as IForm;
            if (formInterface != null)
            {

                formInterface.ShowForm("SOAPAP", "PupCobroAgua");
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

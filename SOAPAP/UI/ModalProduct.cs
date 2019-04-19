using SOAPAP.Enums;
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
    public partial class ModalProduct : Form
    {
        public static string Title { get; set; } = "Cantidad";
        public static string Measure { get; set; } = "";
        public static int TypeProduct { get; set; }
        public static decimal Quatity { get; set; } = 0;
        public static bool IsCancel { get; set; } = false;
        DialogResult result = new DialogResult();
        Form mensaje;

        public ModalProduct()
        {
            InitializeComponent();

        }

        private void ModalProduct_Load(object sender, EventArgs e)
        {
            lblTitle.Text = Title;
            lblMeasure.Text = Measure;
            txtFactor.Text = "1";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            ModalProduct.Quatity = 1;
            ModalProduct.IsCancel = true;
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFactor.Text))
            {
                ModalProduct.IsCancel = false;
                UI.Productos Return = ((UI.Productos)this.Owner.OwnedForms.Where(x => x.Name == "Productos").FirstOrDefault());
                switch (TypeProduct)
                {
                    case 1:
                        Productos.vp = 1;
                        Quatity = Convert.ToDecimal(txtFactor.Text);
                        Return.AddProductToGrid(1);
                        break;
                    case 2:
                        Productos.vp = 2;
                        Quatity = Convert.ToDecimal(txtFactor.Text);
                        Return.AddProductToGrid(2);
                        break;
                    case 3:
                        Productos.vp = 3;
                        Quatity = Convert.ToDecimal(txtFactor.Text);
                        Return.AddProductToGrid(3);
                        break;
                    case 4:
                        Productos.vp = 4;
                        Quatity = Convert.ToDecimal(txtFactor.Text);
                        Return.AddProductToGrid(4);
                        break;
                    case 5:
                        Productos.vp = 5;
                        Quatity = Convert.ToDecimal(txtFactor.Text);
                        Return.AddProductToGrid(5);
                        break;
                }

                this.Close();
            }
            else
            {
                mensaje = new MessageBoxForm("Error","Deebe Ingresar una cantidad para poder continuar", TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
           
        }
    }
}

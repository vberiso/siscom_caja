using SOAPAP.Enums;
using SOAPAP.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SOAPAP
{
    public  partial class cobro : Form
    {
        public cobro()
        {
            InitializeComponent();
        }

        querys q = new querys();
        string cuentas = string.Empty;
        DataTable dt = new DataTable();
        Form mensaje;
        Form mensajes;


        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public static void alzheimer()
        {
            //Console.WriteLine("--LiberarMemoria--"); 
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }


        public void cargar()
        {
            dataGridView1.DataSource = dt;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;
        }

        private void cobro_Load(object sender, EventArgs e)
        {
            alzheimer();
            Variables.anticipo = 0;
            Variables.oprtions = false;
            Variables.cuenta = "";
        }

        private async void busqueda()

        {
            int radio = 0;
            string[] separadas;
            if (textBox1.Text == "")
            {
                mensaje = new MessageBoxForm(Variables.titleprincipal, "Ingrese dato", TypeIcon.Icon.Cancel);
                mensaje.ShowDialog();
            }
            else
            {
                Variables.oprtions = true;
                Form loading = new Loading();
                loading.Show(this);
                
                if (comboBox1.Text == "CUENTA")
                {
                    radio = 1;

                    dt = await q.GETAgreementsFindAgreement("/api/Agreements/FindAgreementParam?type=" + radio + "&stringSearch=" + textBox1.Text);
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            separadas = row[0].ToString().Split('/');
                            if (separadas[0].ToString() == "error")
                            {
                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                mensaje.ShowDialog();
                                dt.Rows.Clear();
                                loading.Close();
                                return;
                            }

                        }
                    }
                    else
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                        mensaje.ShowDialog();
                    }
                    cargar();
                    dataGridView1.ClearSelection();
                }


                else if (comboBox1.Text == "NOMBRE")
                {
                    radio = 2;

                    dt = await q.GETAgreementsFindAgreement("/api/Agreements/FindAgreementParam?type=" + radio + "&stringSearch=" + textBox1.Text);
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            separadas = row[0].ToString().Split('/');
                            if (separadas[0].ToString() == "error")
                            {
                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                mensaje.ShowDialog();
                                dt.Rows.Clear();
                                loading.Close();
                                return;
                            }

                        }
                    }
                    else
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                        mensaje.ShowDialog();
                    }
                    cargar();
                    dataGridView1.ClearSelection();
                }
                else if (comboBox1.Text == "RFC")
                {
                    radio = 4;
                    dt = await q.GETAgreementsFindAgreement("/api/Agreements/FindAgreementParam?type=" + radio + "&stringSearch=" + textBox1.Text);
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            separadas = row[0].ToString().Split('/');
                            if (separadas[0].ToString() == "error")
                            {
                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                mensaje.ShowDialog();
                                dt.Rows.Clear();
                                loading.Close();
                                return;
                            }
                        }
                    }
                    else
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                        mensaje.ShowDialog();
                    }
                    cargar();
                    dataGridView1.ClearSelection();
                }
                else if (comboBox1.Text == "DOMICILIO")
                {
                    radio = 3;
                    dt = await q.GETAgreementsFindAgreement("/api/Agreements/FindAgreementParam?type=" + radio + "&stringSearch=" + textBox1.Text);
                    if(dt != null)
                    { 
                        foreach (DataRow row in dt.Rows)
                        {
                            separadas = row[0].ToString().Split('/');
                            if (separadas[0].ToString() == "error")
                            {
                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                mensaje.ShowDialog();
                                dt.Rows.Clear();
                                    loading.Close();
                                    return;
                            }
                        }
                    }
                    else
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                        mensaje.ShowDialog();
                    }
                    cargar();
                    dataGridView1.ClearSelection();
                }

                

                loading.Close();
            }
        }

        private  void button1_Click(object sender, EventArgs e)
        {
             busqueda();
        }
       
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string on = string.Empty;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                cuentas = row.Cells["cuenta"].Value.ToString();
                //on = row.Cells[8].Value.ToString();
                on = row.Cells["Status"].Value.ToString();

                if (on == "ACTIVO")
                {
                    Variables.cuenta = cuentas;
                    Variables.cuentaID = Convert.ToInt32(row.Cells[1].Value.ToString()); 
                    IForm formInterface = this.Owner as IForm;
                    if (formInterface != null)
                    {

                        formInterface.ShowForm("SOAPAP", "UI.Cobro");
                    }
                }
                else
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "La cuenta seleccionada está: " + on, TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                }
            }
                
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].Name == "Imprimir" && e.RowIndex >= 0)
            {             
                    var image = Properties.Resources.detalle;
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                    e.Graphics.DrawImage(image, x, y, 15, 15);
                    e.Handled = true;  
            }
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (comboBox1.Text != "")
                {
                e.Handled = true;
                busqueda();
                }
                else
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Seleccione Filtro", TypeIcon.Icon.Info);
                    mensaje.ShowDialog();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            { 
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                if (e.ColumnIndex == dataGridView1.Columns["Imprimir"].Index && e.RowIndex >= 0)
                {
                    cuentas = row.Cells[2].Value.ToString();
                    Variables.cuenta = cuentas;
                    IForm formInterface = this.Owner as IForm;
                    if (formInterface != null)
                    {
                        formInterface.ShowForm("SOAPAP", "UI.Cobro");
                    }
                }
                else
                {

                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                busqueda();
            }
            else
            {
                mensaje = new MessageBoxForm(Variables.titleprincipal, "Seleccione Filtro", TypeIcon.Icon.Info);
                mensaje.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IForm formInterface = this.Owner as IForm;
            if (formInterface != null)
            {

                formInterface.ShowForm("SOAPAP", "cobroagua");
            }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
           
        
        }
    }
}

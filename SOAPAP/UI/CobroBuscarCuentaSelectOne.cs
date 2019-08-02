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
    public partial class CobroBuscarCuentaSelectOne : Form
    {
        private List<Model.Agreement> LAgreement;
        private Model.Agreement  Agreement;
        private MessageBoxForm mensaje ;
        public CobroBuscarCuentaSelectOne(List<Model.Agreement> LAgreement)
        {
            this.LAgreement = LAgreement;
            InitializeComponent();
        }

       
        public Model.Agreement getAgreement()
        {
            return Agreement;
        }

        private void CobroBuscarCuentaSelectOne_Load(object sender, EventArgs e)
        {
            dataGridViewCuentas.ColumnCount = 3;
            dataGridViewCuentas.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridViewCuentas.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Set the column header names.
            dataGridViewCuentas.Columns[0].Name = "Cuenta";
            dataGridViewCuentas.Columns[1].Name = "Usuario";
            dataGridViewCuentas.Columns[2].Name = "Tipo";
           
            LAgreement.ForEach(x=>
            {
                var Oclient = x.Clients.Where(client => client.TypeUser == "CLI01").FirstOrDefault();
                dataGridViewCuentas.Rows.Add(new string[] { x.Account, Oclient.Name+ " "+ Oclient.LastName, x.TypeIntake.Name });

            });
        }

        private void dataGridViewCuentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Agreement = LAgreement[e.RowIndex];
                this.Close();
            }catch(Exception ex)
            {
                mensaje = new MessageBoxForm("Error", "Ocurrio un error interno, porfavor contacte con el administrador", TypeIcon.Icon.Cancel);
                mensaje.ShowDialog();
            }
        }
    }
}

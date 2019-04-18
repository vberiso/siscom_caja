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
    public partial class SearchUser : Form
    {

        public SearchUser(DataTable data)
        {
            InitializeComponent();

            BindingSource source = new BindingSource();
            source.DataSource = data;

            dgvContribuyentes.AutoGenerateColumns = true;
            dgvContribuyentes.Columns.Clear();
            dgvContribuyentes.DataSource = source;

            for (int i = 0; i < dgvContribuyentes.Columns.Count; i++)
            {
                dgvContribuyentes.Columns[i].DataPropertyName = data.Columns[i].ColumnName;
                dgvContribuyentes.Columns[i].HeaderText = data.Columns[i].Caption;
            }

            dgvContribuyentes.Refresh();

            dgvContribuyentes.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            dgvContribuyentes.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvContribuyentes.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        private void dgvContribuyentes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvContribuyentes.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
            UI.ModalTax Return = this.Owner as ModalTax;
            Return.idUser = id;
            Return.addInfoUser(id);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

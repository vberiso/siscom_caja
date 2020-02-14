using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI.CuentasContables
{
    public partial class AddCuentaContable : Form
    {
        private string root;
        private TreeListItem treeListItem;

        Form loading;
        private RequestsAPI Requests = null;
        Form mensaje;
        DialogResult result = new DialogResult();

        public AddCuentaContable(string pRoot, TreeListItem pTreeListItem)
        {
            InitializeComponent();
            this.root = pRoot;
            this.treeListItem = pTreeListItem;
        }

        private void AddCuentaContable_Load(object sender, EventArgs e)
        {
            tbxRoot.Text = this.root;
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);

            TreeListItem treeListItem = new TreeListItem();
            treeListItem.ParentId = treeListItem.Id;
            treeListItem.AccountNumber = tbxCodigo.Text;
            treeListItem.Description = tbxNombre.Text;
            treeListItem.CodeConcept = "";
            treeListItem.IsActive = true;
            treeListItem.TipeService = treeListItem.TipeService;            

            HttpContent content;
            string json = JsonConvert.SerializeObject(treeListItem);
            content = new StringContent(json, Encoding.UTF8, "application/json");

            var _resulActualizacion = await Requests.SendURIAsync("/api/CuentasContables", HttpMethod.Post, Variables.LoginModel.Token, content);

            if (_resulActualizacion.Contains("error:"))
            {
                mensaje = new MessageBoxForm("Error", _resulActualizacion.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                mensaje = new MessageBoxForm("Cambios guardados", "La cuenta fue guardada.", TypeIcon.Icon.Info, true);
                result = mensaje.ShowDialog();
            }

            loading.Close();
        }
    }
}

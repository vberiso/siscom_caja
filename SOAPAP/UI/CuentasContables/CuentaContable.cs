using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Reportes;
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
    public partial class CuentaContable : Form
    {
        Form loading;
        private RequestsAPI Requests = null;
        Form mensaje;
        DialogResult result = new DialogResult();
        List<TreeListItem> lstCContables;

        public CuentaContable()
        {
            InitializeComponent();
            Requests = new RequestsAPI(Properties.Settings.Default.URL);
        }

        private async void CuentaContable_Load(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            //Peticion de servicios
            //Peticion de Servicio y productos.
            var resultCuentasContables = await Requests.SendURIAsync("/api/CuentasContables", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultCuentasContables.Contains("error:"))
            {
                mensaje = new MessageBoxForm("Error", resultCuentasContables.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                lstCContables = JsonConvert.DeserializeObject<List<TreeListItem>>(resultCuentasContables);                
            }

            //Inicializa tipos de servicio
            List<DataComboBox> lstCaj = new List<DataComboBox>();
            lstCaj.Add(new DataComboBox() { keyString = "Servicio", value = "Servicio" });
            lstCaj.Add(new DataComboBox() { keyString = "Producto", value = "Producto" });

            cbxTipoServicio.ValueMember = "keyString";
            cbxTipoServicio.DisplayMember = "value";
            cbxTipoServicio.DataSource = lstCaj;
            loading.Close();
        }

        private void cbxTipoServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((SOAPAP.Reportes.DataComboBox)((System.Windows.Forms.ComboBox)sender).SelectedItem).keyString.Contains("Servicio"))
            {   
                treeListCuentasContables.DataSource = lstCContables.Where(x => x.TipeService.Contains("Service")).ToList();
            }
            else
            {             
                treeListCuentasContables.DataSource = lstCContables.Where(x => x.TipeService.Contains("Product")).ToList();
            }
        }

        private async void treeListCuentasContables_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "AccountNumber")
            {
                if (e.Node.HasChildren == false)
                {
                    loading = new Loading();
                    loading.Show(this);

                    var temp = treeListCuentasContables.Selection.FirstOrDefault();
                    var item = (treeListCuentasContables.DataSource as List<TreeListItem>).ToArray()[temp.Id];

                    HttpContent content;
                    string json = JsonConvert.SerializeObject(item);
                    content = new StringContent(json, Encoding.UTF8, "application/json");

                    var _resulActualizacion = await Requests.SendURIAsync("/api/CuentasContables/" + item.Id, HttpMethod.Post, Variables.LoginModel.Token, content);

                    if (_resulActualizacion.Contains("error:"))
                    {
                        mensaje = new MessageBoxForm("Error", _resulActualizacion.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        //mensaje = new MessageBoxForm("Cambios guardados", "Los cambios fueron fuardados.", TypeIcon.Icon.Info, true);
                        //result = mensaje.ShowDialog();
                    }

                    loading.Close();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var temp = treeListCuentasContables.Selection.FirstOrDefault();
            var item = (treeListCuentasContables.DataSource as List<TreeListItem>).ToArray()[temp.Id];

            string origen = item.Description;
            var Padre = lstCContables.FirstOrDefault(x => x.Id == item.ParentId);
            while (Padre != null)
            {
                origen = Padre.Description + " - " + origen;
                Padre = lstCContables.FirstOrDefault(x => x.Id == Padre.ParentId);
            }
            
            AddCuentaContable addCuentaContable = new AddCuentaContable(origen, item);
            addCuentaContable.ShowDialog(this);
        }
    }
}

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

namespace SOAPAP.UI.ReportesForms
{
    public partial class RepTaxpayer : Form
    {
        Form loading;
        private RequestsAPI Requests = null;
        Form mensaje;
        DialogResult result = new DialogResult();

        private string UrlBase = Properties.Settings.Default.URL;
        string json = string.Empty;

        public RepTaxpayer()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }

        private async void RepTaxpayer_Load(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            await CargarCombos();
            loading.Close();
        }

        private async Task CargarCombos()
        {
            //Combo de Opciones de busqueda.
            List<DataComboBox> lstBusquedaPor = new List<DataComboBox>();
            lstBusquedaPor.Add(new DataComboBox() { keyInt = 0, value = "Cuenta" });
            lstBusquedaPor.Add(new DataComboBox() { keyInt = 1, value = "Folio" });
            lstBusquedaPor.Add(new DataComboBox() { keyInt = 2, value = "Nombre" });
            lstBusquedaPor.Add(new DataComboBox() { keyInt = 3, value = "Dirección" });
            
            cbxBusqudaPor.ValueMember = "keyString";
            cbxBusqudaPor.DisplayMember = "value";
            cbxBusqudaPor.DataSource = lstBusquedaPor;
            cbxBusqudaPor.SelectedIndex = 0;

            //Opciones de Clientes
            var resultTypeTransaction = await Requests.SendURIAsync("/api/Reports/GetClientesContains", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var lstClientes = JsonConvert.DeserializeObject<List<SOAPAP.Model.Client>>(resultTypeTransaction);

                //Text box Cuenta
                var sourceCuenta = new AutoCompleteStringCollection();
                sourceCuenta.AddRange(lstClientes.Select(x => x.AgreementId.ToString() ).ToArray());
                tbxCuenta.AutoCompleteMode = AutoCompleteMode.Suggest;
                tbxCuenta.AutoCompleteCustomSource = sourceCuenta;
                tbxCuenta.AutoCompleteSource = AutoCompleteSource.CustomSource;

                //Text box Nombre
                var source = new AutoCompleteStringCollection();
                source.AddRange(lstClientes.Select(x => string.Format("{0} {1} {2}", x.Name, x.LastName, x.SecondLastName)).ToArray());
                tbxNombre.AutoCompleteMode = AutoCompleteMode.Suggest;
                tbxNombre.AutoCompleteCustomSource = source;
                tbxNombre.AutoCompleteSource = AutoCompleteSource.CustomSource;

                //Text box RFC
                var sourceRFC = new AutoCompleteStringCollection();
                sourceRFC.AddRange(lstClientes.Where(y => y.RFC != null).Select(x => x.RFC).Distinct().ToArray());
                tbxRFC.AutoCompleteMode = AutoCompleteMode.Suggest;
                tbxRFC.AutoCompleteCustomSource = sourceRFC;
                tbxRFC.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }

            //Opciones de Dirección
            var resultTypeTransaction2 = await Requests.SendURIAsync("/api/Towns/2/Suburbs", HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction2.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultTypeTransaction2.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var lstDirecciones = JsonConvert.DeserializeObject<List<SOAPAP.Model.Suburb>>(resultTypeTransaction2);

                //Text box Dirección
                var sourceDireccion = new AutoCompleteStringCollection();
                sourceDireccion.AddRange(lstDirecciones.Select(x => x.Name).ToArray());
                tbxColonia.AutoCompleteMode = AutoCompleteMode.Suggest;
                tbxColonia.AutoCompleteCustomSource = sourceDireccion;
                tbxColonia.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        private void cbxBusqudaPor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataComboBox item = (DataComboBox)((ComboBox)sender).SelectedItem;
            switch (item.keyInt)
            {
                case 0:
                    //Cuenta
                    lblOpcionBusqueda.Text = "No de Cuenta:";
                    tbxCuenta.Text = "";
                    tbxCuenta.Enabled = true;
                    pnlOpContribuyente.Visible = false;
                    pnlDireccion.Visible = false;
                    break;
                case 1:
                    //Folio
                    lblOpcionBusqueda.Text = "No de Folio:";
                    tbxCuenta.Text = "";
                    tbxCuenta.Enabled = true;
                    pnlOpContribuyente.Visible = false;
                    pnlDireccion.Visible = false;
                    break;
                case 2:
                    //Taxpayer
                    tbxCuenta.Enabled = false;
                    pnlOpContribuyente.Visible = true;
                    pnlDireccion.Visible = false;
                    break;
                case 3:
                    //Address
                    tbxCuenta.Enabled = false;
                    pnlOpContribuyente.Visible = false;
                    pnlDireccion.Visible = true;
                    break;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataComboBox item = (DataComboBox)cbxBusqudaPor.SelectedItem;
            switch (item.keyInt)
            {
                case 0:
                    //Cuenta
                    BusquedaPorCuenta();
                    break;
                case 1:
                    //Folio
                    BusquedaPorFolio();
                    break;
                case 2:
                    //Taxpayer
                    BusquedaPorNombre();
                    break;
                case 3:
                    //Address
                    BusquedaPorDireccion();
                    break;
            }
        }

        private async void BusquedaPorCuenta()
        {
            //Busqueda por numero de cuenta
            var resultTypeTransaction = await Requests.SendURIAsync("/api/Agreements/GetSummary/" + cbxBusqudaPor.Text, HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var lstClientes = JsonConvert.DeserializeObject<List<SOAPAP.Model.Agreement>>(resultTypeTransaction);

                ////Text box Cuenta
                //var sourceCuenta = new AutoCompleteStringCollection();
                //sourceCuenta.AddRange(lstClientes.Select(x => x.AgreementId.ToString()).ToArray());
                //tbxCuenta.AutoCompleteMode = AutoCompleteMode.Suggest;
                //tbxCuenta.AutoCompleteCustomSource = sourceCuenta;
                //tbxCuenta.AutoCompleteSource = AutoCompleteSource.CustomSource;

            }
        }
        private void BusquedaPorFolio()
        {

        }
        private void BusquedaPorNombre()
        {

        }
        private void BusquedaPorDireccion()
        {

        }

        private void tswInfoContriyente_Toggled(object sender, EventArgs e)
        {            
            gbxContribuyente.Visible = ((DevExpress.XtraEditors.ToggleSwitch)sender).IsOn;    
            lblOffContribuyente.Visible = !((DevExpress.XtraEditors.ToggleSwitch)sender).IsOn;
            sepOffContribuyente.Visible = !((DevExpress.XtraEditors.ToggleSwitch)sender).IsOn;
        }

        private void tswDomicilio_Toggled(object sender, EventArgs e)
        {
            gbxDomicilio.Visible = ((DevExpress.XtraEditors.ToggleSwitch)sender).IsOn;
            lblOffDomicilio.Visible = !((DevExpress.XtraEditors.ToggleSwitch)sender).IsOn;
            sepOffDomicilio.Visible = !((DevExpress.XtraEditors.ToggleSwitch)sender).IsOn;
        }
    }
}

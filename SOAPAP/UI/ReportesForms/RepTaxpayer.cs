using DevExpress.XtraBars.Docking2010;
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
using System.Text.RegularExpressions;
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

        List<SOAPAP.Model.ClientFinding> lstClientes;

        public RepTaxpayer()
        {
            Requests = new RequestsAPI(UrlBase);
            InitializeComponent();
        }

        private async void RepTaxpayer_Load(object sender, EventArgs e)
        {
            loading = new Loading();
            loading.Show(this);
            CargarCombos();
            loading.Close();
        }

        private void CargarCombos()
        {
            //Combo de Opciones de busqueda.
            List<DataComboBox> lstBusquedaPor = new List<DataComboBox>();
            lstBusquedaPor.Add(new DataComboBox() { keyInt = 0, value = "Cuenta" });
            lstBusquedaPor.Add(new DataComboBox() { keyInt = 1, value = "Folio" });
            lstBusquedaPor.Add(new DataComboBox() { keyInt = 2, value = "Nombre" });
            lstBusquedaPor.Add(new DataComboBox() { keyInt = 3, value = "Dirección" });

            cbxBusqudaPor.ValueMember = "keyInt";
            cbxBusqudaPor.DisplayMember = "value";
            cbxBusqudaPor.DataSource = lstBusquedaPor;
            cbxBusqudaPor.SelectedIndex = 0;

            //Opciones de Clientes
            //var resultTypeTransaction = await Requests.SendURIAsync("/api/Reports/GetClientesContains", HttpMethod.Get, Variables.LoginModel.Token);
            //if (resultTypeTransaction.Contains("error"))
            //{
            //    mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
            //    result = mensaje.ShowDialog();
            //}
            //else
            //{
            //    lstClientes = JsonConvert.DeserializeObject<List<SOAPAP.Model.ClientFinding>>(resultTypeTransaction);

                //Text box Cuenta
                //var sourceCuenta = new AutoCompleteStringCollection();
                //sourceCuenta.AddRange(lstClientes.Select(x => x.Cuenta).Distinct().ToArray());
                //tbxCuenta.AutoCompleteMode = AutoCompleteMode.Suggest;
                //tbxCuenta.AutoCompleteCustomSource = sourceCuenta;
                //tbxCuenta.AutoCompleteSource = AutoCompleteSource.CustomSource;

                //Text box Nombre
                //var source = new AutoCompleteStringCollection();
                //source.AddRange(lstClientes.Select(x => x.Nombre).ToArray());
                //tbxNombre.AutoCompleteMode = AutoCompleteMode.Suggest;
                //tbxNombre.AutoCompleteCustomSource = source;
                //tbxNombre.AutoCompleteSource = AutoCompleteSource.CustomSource;

                //Text box RFC
                //var sourceRFC = new AutoCompleteStringCollection();
                //sourceRFC.AddRange(lstClientes.Where(y => y.RFC != null).Select(x => x.RFC).Distinct().ToArray());
                //tbxRFC.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //tbxRFC.AutoCompleteCustomSource = sourceRFC;
                //tbxRFC.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //}

            //Opciones de Dirección
            //var resultTypeTransaction2 = await Requests.SendURIAsync("/api/Towns/2/Suburbs", HttpMethod.Get, Variables.LoginModel.Token);
            //if (resultTypeTransaction2.Contains("error"))
            //{
            //    mensaje = new MessageBoxForm("Error", resultTypeTransaction2.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
            //    result = mensaje.ShowDialog();
            //}
            //else
            //{
            //    var lstDirecciones = JsonConvert.DeserializeObject<List<SOAPAP.Model.Suburb>>(resultTypeTransaction2);

            //    //Text box Dirección
            //    var sourceDireccion = new AutoCompleteStringCollection();
            //    sourceDireccion.AddRange(lstDirecciones.Select(x => x.Name).ToArray());
            //    //tbxColonia.AutoCompleteMode = AutoCompleteMode.Suggest;
            //    tbxColonia.AutoCompleteCustomSource = sourceDireccion;
            //    //tbxColonia.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //}
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

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            DataComboBox item = (DataComboBox)cbxBusqudaPor.SelectedItem;

            loading = new Loading();
            loading.Show(this);

            switch (item.keyInt)
            {
                case 0:
                    //Cuenta
                    await BusquedaPorCuenta();
                    break;
                case 1:
                    //Folio
                    await BusquedaPorFolio();
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

            loading.Close();
        }

        private async Task BusquedaPorCuenta()
        {
            //Busqueda por numero de cuenta
            var resultTypeTransaction = await Requests.SendURIAsync("/api/Agreements/GetSummary/" + tbxCuenta.Text, HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var Contrato = JsonConvert.DeserializeObject<SOAPAP.Model.Agreement>(resultTypeTransaction);
                if (Contrato.Account == null)
                {
                    mensaje = new MessageBoxForm("Error", "Cuenta no existe", TypeIcon.Icon.Cancel);
                    result = mensaje.ShowDialog();
                }else
                    await LlenaDatos(Contrato);                
            }
        }
        private async Task BusquedaPorFolio()
        {
            //Busqueda por numero de cuenta
            var resultTypeTransaction = await Requests.SendURIAsync("/api/Payments/Resume/" + tbxCuenta.Text, HttpMethod.Get, Variables.LoginModel.Token);
            if (resultTypeTransaction.Contains("error"))
            {
                mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                result = mensaje.ShowDialog();
            }
            else
            {
                var Contrato = JsonConvert.DeserializeObject<SOAPAP.Model.PaymentResume>(resultTypeTransaction);
                await LlenaDatosProducto(Contrato);
            }
        }
        private void BusquedaPorNombre()
        {            
            List<Model.ClientFinding> lstEncontrados = new List<Model.ClientFinding>();            
            //desfragmento la cadena y busco los nombres que contienen cada palabra (aunque el orden de las palabras llegara a cambiar).
            string[] NombrePartes = obtenerCadenaSinAcentos(tbxNombre.Text.ToLower()).Split(' ');
            NombrePartes = NombrePartes.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            lstEncontrados.AddRange( lstClientes.Where(c => NombrePartes.Where(np => obtenerCadenaSinAcentos(c.Nombre.ToLower()).Split(' ').Contains(np)).Count() == NombrePartes.Count()).ToList() );
            if (lstEncontrados.Count == 0)
            {
                lstEncontrados.Add(new Model.ClientFinding() { Cuenta = "Sin datos", Nombre = "No se encontraron resultados." });
            }

            lbcListadoContratos.ValueMember = "id";
            lbcListadoContratos.DisplayMember = "DisplayData";
            lbcListadoContratos.DataSource = lstEncontrados;
            lbcListadoContratos.SelectedIndex = 0;

            pnlOpContribuyente.Visible = false;
            pnlResultadosBusquda.Visible = true;
        }
        private void BusquedaPorDireccion()
        {
            List<Model.ClientFinding> lstEncontrados = new List<Model.ClientFinding>();
            //busqueda de la direccion.
            string[] DireccionPartes = { tbxCalle.Text.ToLower(), tbxNumero.Text.ToLower(), tbxColonia.Text.ToLower() };
            //lstEncontrados.AddRange(lstClientes.Where(c => DireccionPartes.Where(dp => obtenerCadenaSinAcentos(string.Format("{0} {1} {2} {3}",c.Street, c.Outdoor, c.Indoor, c.Colonia).ToLower()).Split(' ').Contains(dp)).Count() == DireccionPartes.Count()).ToList());
            lstEncontrados.AddRange(lstClientes
                .Where(c => obtenerCadenaSinAcentos(string.Format("{0} {1} {2} {3}", c.Street, c.Outdoor, c.Indoor, c.Colonia).ToLower()).Contains(tbxCalle.Text.ToLower()) 
                            && obtenerCadenaSinAcentos(string.Format("{0} {1} {2} {3}", c.Street, c.Outdoor, c.Indoor, c.Colonia).ToLower()).Contains(tbxNumero.Text.ToLower())
                            && obtenerCadenaSinAcentos(string.Format("{0} {1} {2} {3}", c.Street, c.Outdoor, c.Indoor, c.Colonia).ToLower()).Contains(tbxColonia.Text.ToLower())
                    )
            );
            if (lstEncontrados.Count == 0)
            {
                lstEncontrados.Add(new Model.ClientFinding() { Cuenta = "Sin datos", Nombre = "No se encontraron resultados." });
            }
            
            lbcListadoContratos.ValueMember = "id";
            lbcListadoContratos.DisplayMember = "DisplayData";
            lbcListadoContratos.DataSource = null;
            lbcListadoContratos.DataSource = lstEncontrados;
            lbcListadoContratos.SelectedIndex = 0;

            pnlDireccion.Visible = false;
            pnlResultadosBusquda.Visible = true;
        }
               
        //Llena datos de servicios
        private async Task LlenaDatos(SOAPAP.Model.Agreement agr)
        {
            //Contribuyente
            SOAPAP.Model.Client cli = agr.Clients.First();
            lblNombre.Text = string.Format("{0} {1} {2}", cli.Name, cli.LastName, cli.SecondLastName);
            lblRFC.Text = cli.RFC;
            lblIne.Text = cli.INE;
            lblCURP.Text = cli.CURP;
            lblCel.Text = "";
            lblMail.Text = cli.EMail;
            //Domicilio
            SOAPAP.Model.Address add = agr.Addresses.First();
            lblCalle.Text = add.Street;
            lblNumero.Text = string.Format("{0} - {1}", add.Outdoor, add.Indoor);
            lblColonia.Text = add.Suburbs.Name;
            lblCP.Text = add.Zip;
            lblLocalidad.Text = add.Suburbs.Towns.Name;
            lblMunicipio.Text = add.Suburbs.Towns.States.Name;
            //Padron
            lblTipoServicio.Text = "";
            lblToma.Text = agr.TypeIntake.Name;
            lblConsumo.Text = "";
            lblPoblacionVul.Text = "";
            lblRuta.Text = agr.Route;
            lblFechaContrato.Text = agr.AccountDate.ToString("yyyy-MM-dd");
            //Prepagos            
            if(agr.Prepaids.Count > 0)
            {
                pnlRepPrepaid.Visible = true;                
                List<DataTaxpayerPrepaid> lstPrepaid = new List<DataTaxpayerPrepaid>();               
                foreach(var x in agr.Prepaids)
                {
                    lstPrepaid.Add(new DataTaxpayerPrepaid
                    {
                        Fecha = x.PrepaidDate.ToString("yyyy-MM-dd"),
                        Amount = x.Amount,
                        DescriptionStatus = x.StatusDescription,
                        DescriptionType = x.TypeDescription
                    });
                }
                pgcPrepaid.DataSource = lstPrepaid;
            }            
            //Deuda
            if(agr.Debts.Count > 0)
            {
                pnlRepDebt.Visible = true;                
                List<DataTaxpayerDebt> lstDebt = new List<DataTaxpayerDebt>();                
                foreach(var item in agr.Debts)
                {
                    lstDebt.AddRange( item.DebtDetails.Select(y => new DataTaxpayerDebt { Description = y.NameConcept, Monto = y.Amount, IVA = y.Tax,  Total = y.Amount + y.Tax})  );
                }
                pgcDebt.DataSource = lstDebt;
            }
            

        }

        //Llena datos de Productos(folio)
        private async Task LlenaDatosProducto(SOAPAP.Model.PaymentResume pr)
        {
            //Contribuyente
            SOAPAP.Model.TaxUser cli = pr.orderSale.TaxUser;
            lblNombre.Text = cli.Name;
            lblRFC.Text = cli.RFC;
            lblIne.Text = "s/d";
            lblCURP.Text = cli.CURP;
            lblCel.Text = cli.PhoneNumber;
            lblMail.Text = cli.EMail;
            //Domicilio     
            Model.TaxAddress add = cli.TaxAddresses.FirstOrDefault();
            lblCalle.Text = add.Street ;
            lblNumero.Text = string.Format("{0} - {1}", add.Outdoor, add.Indoor);
            lblColonia.Text = add.Suburb;
            lblCP.Text = add.Zip;
            lblLocalidad.Text = add.Town;
            lblMunicipio.Text = add.State;
            //Padron
            lblTipoServicio.Text = "";
            lblToma.Text = "";
            lblConsumo.Text = "";
            lblPoblacionVul.Text = "";
            lblRuta.Text = "";
            lblFechaContrato.Text =  pr.payment.PaymentDate.ToString("yyyy-MM-dd");
            //pagos            
            if (pr.orderSale.OrderSaleDetails.Count > 0)
            {
                pnlRepOrderSale.Visible = true;
                List<DataTaxpayerOrderSale> lstOS = new List<DataTaxpayerOrderSale>();
                foreach (var x in pr.orderSale.OrderSaleDetails)
                {
                    lstOS.Add(new DataTaxpayerOrderSale
                    {
                        FechaPago = pr.orderSale.DateOrder.ToString("yyyy-MM-dd"),
                        FechaExpiracion = pr.orderSale.ExpirationDate.ToString("yyyy-MM-dd"),
                        NameConcept = x.NameConcept,
                        Description = x.Description,
                        UnitPrice = x.UnitPrice,
                        Quantity = x.Quantity,
                        Amount = x.Amount,
                        Tax = x.Tax,
                        Total = x.Amount + x.Tax
                    });
                }
                pgcOrderSale.DataSource = lstOS;
            }
        }

        private string obtenerCadenaSinAcentos(string pCadena)
        {
            string CadenaNormalizada = pCadena.Normalize(NormalizationForm.FormD);
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            string textoSinAcentos = reg.Replace(CadenaNormalizada, "");
            return textoSinAcentos.TrimEnd().TrimStart();
        }

        //Elemento seleccionado de la lista de usuarios encontrados. (Tambien aplica para direcciones.)
        private async void lbcListadoContratos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Model.ClientFinding cf = (Model.ClientFinding)((DevExpress.XtraEditors.ListBoxControl)sender).SelectedItem;

            if (cf.Cuenta != "Sin datos")
            {   
                if (cf.Id_Client != 0)  //Es un aggrement
                {
                    //Busqueda por numero de cuenta
                    var resultTypeTransaction = await Requests.SendURIAsync("/api/Agreements/GetSummary/" + cf.Cuenta, HttpMethod.Get, Variables.LoginModel.Token);
                    if (resultTypeTransaction.Contains("error"))
                    {
                        mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        var Contrato = JsonConvert.DeserializeObject<SOAPAP.Model.Agreement>(resultTypeTransaction);
                        if (Contrato != null)
                            await LlenaDatos(Contrato);
                        else
                        {
                            mensaje = new MessageBoxForm("Advertencia", "No se encontró el detalle del pago.", TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                        }
                    }
                }
                else                   //Es un OrderSale
                {
                    //Busqueda por numero de cuenta
                    var resultTypeTransaction = await Requests.SendURIAsync("/api/Payments/Resume/" + cf.Cuenta, HttpMethod.Get, Variables.LoginModel.Token);
                    if (resultTypeTransaction.Contains("error"))
                    {
                        mensaje = new MessageBoxForm("Error", resultTypeTransaction.Split(':')[1].Replace("}", ""), TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                    }
                    else
                    {
                        var Contrato = JsonConvert.DeserializeObject<SOAPAP.Model.PaymentResume>(resultTypeTransaction);
                        if (Contrato != null)
                            await LlenaDatosProducto(Contrato);
                        else
                        {
                            mensaje = new MessageBoxForm("Advertencia", "No se encontró el detalle del producto.", TypeIcon.Icon.Cancel);
                            result = mensaje.ShowDialog();
                        }
                    }
                } 
            }            
        }

        //Volver a vista de origen de busqueda (Nombre o direccion.)
        private void button1_Click(object sender, EventArgs e)
        {
            DataComboBox item = (DataComboBox)cbxBusqudaPor.SelectedItem;
            if (item.value == "Dirección")
            {
                pnlResultadosBusquda.Visible = false;
                pnlDireccion.Visible = true;
            }
            else
            {
                pnlResultadosBusquda.Visible = false;
                pnlOpContribuyente.Visible = true;
            }
        }

        #region togglebuttons - Botones para mostrar u ocultar secciones de informacion. 

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

        private void tswDatosPadron_Toggled(object sender, EventArgs e)
        {
            gbxDatosPadron.Visible = ((DevExpress.XtraEditors.ToggleSwitch)sender).IsOn;
            lblOffDatosPadron.Visible = !((DevExpress.XtraEditors.ToggleSwitch)sender).IsOn;
            sepOffDatosPadron.Visible = !((DevExpress.XtraEditors.ToggleSwitch)sender).IsOn;
        }

        private void tswSaldo_Toggled(object sender, EventArgs e)
        {
            gbxSaldo.Visible = ((DevExpress.XtraEditors.ToggleSwitch)sender).IsOn;
            lblOffSaldo.Visible = !((DevExpress.XtraEditors.ToggleSwitch)sender).IsOn;
            sepOffSaldo.Visible = !((DevExpress.XtraEditors.ToggleSwitch)sender).IsOn;
        }

        #endregion

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string tag = ((WindowsUIButton)e.Button).Tag.ToString();
            switch (tag)
            {

                case "GE":
                    btnBuscar_Click(sender, e);
                    break;

            }
        }
    }
}

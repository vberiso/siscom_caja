using Humanizer;
using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Model;
using SOAPAP.Services;
using SOAPAP.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI
{
    public partial class ModalTax : Form
    {
        Model.OrderSale orderSales = new Model.OrderSale();
        Form loading;
        Form mensaje;
        DataTable Table = new DataTable();
        DialogResult result = new DialogResult();
        querys q = new querys();
        Model.TaxUser Tax = new Model.TaxUser();
        DataTable dts = new DataTable();
        String totals=string.Empty;
        decimal Subtotal;
        decimal IVA;
        Model.Taxresult taxresult = new Model.Taxresult();
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        List<TaxUser> users;
        TaxUser TaxUser = new TaxUser();
        public int idUser { get; set; } = 0;

        public ModalTax(Model.OrderSale orderSale,DataTable dt,String total, decimal Subtotal, decimal IVA)
        {
            InitializeComponent();
            Requests = new RequestsAPI(UrlBase);
            orderSales = orderSale;
            dts = dt;
            totals = total;
            this.Subtotal = Subtotal;
            this.IVA = IVA;
        }

        private void ModalTax_Load(object sender, EventArgs e)
        {

        }

        void impresionhoja()
        {
            Variables.datosgenerales.Columns.Clear();
            Variables.datosgenerales.Rows.Clear();

            DataColumn columns;
            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "Nombredeins";
            Variables.datosgenerales.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "RFCdeInstitucion";
            Variables.datosgenerales.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "DomiciliodeInstitucion";
            Variables.datosgenerales.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "TelefonodeInstitucion";
            Variables.datosgenerales.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "CorreodeInstitucion";
            Variables.datosgenerales.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "NombreFiscaldeInstitucion";
            Variables.datosgenerales.Columns.Add(columns);

            DataRow rows = Variables.datosgenerales.NewRow();
            rows["Nombredeins"] = Variables.Configuration.CompanyName;
            rows["RFCdeInstitucion"] = Variables.Configuration.RFC;
            rows["DomiciliodeInstitucion"] = Variables.Configuration.Address;
            rows["TelefonodeInstitucion"] = Variables.Configuration.Phone;
            rows["CorreodeInstitucion"] = Variables.Configuration.Email;
            rows["NombreFiscaldeInstitucion"] = Variables.Configuration.LegendRegime;
            Variables.datosgenerales.Rows.Add(rows);

            Variables.datospadron.Columns.Clear();
            Variables.datospadron.Rows.Clear();

            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "FoliodeCaja";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Fechayhora";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Cuenta";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Contribuyente";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Rfc";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Domicilio";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Caja";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Sucursal";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Usuario";
            Variables.datospadron.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Periodo";
            Variables.datospadron.Columns.Add(column);

            DataRow row = Variables.datospadron.NewRow();
            row["FoliodeCaja"] = Variables.foliocaja;
            row["Fechayhora"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            row["Cuenta"] = taxresult.folio;
            row["Contribuyente"] = taxresult.TaxUser.Name;
            row["Rfc"] = taxresult.TaxUser.RFC;
            row["Domicilio"] = txtAddress.Text;
            row["Caja"] = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id;
            row["Sucursal"] = Variables.Configuration.Terminal.BranchOffice.Name;
            row["Usuario"] = Variables.LoginModel.FullName;
            Variables.datospadron.Rows.Add(row);

            Variables.Productos.Columns.Clear();
            Variables.Productos.Rows.Clear();

            DataColumn columnt;
            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "id";
            Variables.Productos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Cantidad";
            Variables.Productos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Descripcion";
            Variables.Productos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Total";
            Variables.Productos.Columns.Add(columnt);

            foreach (DataRow rowr in dts.Rows)
            {
                DataRow rowt = Variables.Productos.NewRow();
                rowt["id"] = rowr[0].ToString();
                rowt["Cantidad"] = rowr[5].ToString();
                rowt["Descripcion"] = rowr[1].ToString();
                rowt["Total"] = rowr[2].ToString();
                Variables.Productos.Rows.Add(rowt);
            }

            Variables.ImagenData.Columns.Clear();
            Variables.ImagenData.Rows.Clear();

            DataColumn columnts;
            columnts = new DataColumn();
            columnts.DataType = System.Type.GetType("System.Byte[]");
            columnts.ColumnName = "Imagen1";
            Variables.ImagenData.Columns.Add(columnts);

            columnts = new DataColumn();
            columnts.DataType = System.Type.GetType("System.Byte[]");
            columnts.ColumnName = "Imagen2";
            Variables.ImagenData.Columns.Add(columnts);

            Image img = q.Imagen();
            byte[] arr;
            ImageConverter converter = new ImageConverter();
            arr = (byte[])converter.ConvertTo(img, typeof(byte[]));

            DataRow rowt1 = Variables.ImagenData.NewRow();
            rowt1["Imagen1"] = arr;
            rowt1["Imagen2"] = arr;
            Variables.ImagenData.Rows.Add(rowt1);

            Variables.Foliotiket.Columns.Clear();
            Variables.Foliotiket.Rows.Clear();

            DataColumn columntss;
            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "Foliotransaccion";
            Variables.Foliotiket.Columns.Add(columntss);

            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "Letra";
            Variables.Foliotiket.Columns.Add(columntss);

            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "Pago";
            Variables.Foliotiket.Columns.Add(columntss);

            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "Subtotal";
            Variables.Foliotiket.Columns.Add(columntss);

            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "IVA";
            Variables.Foliotiket.Columns.Add(columntss);

            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "Redondeo";
            Variables.Foliotiket.Columns.Add(columntss);

            columntss = new DataColumn();
            columntss.DataType = System.Type.GetType("System.String");
            columntss.ColumnName = "Total";
            Variables.Foliotiket.Columns.Add(columntss);

            string k1 = totals.Replace("$", "").Replace(",","");
            //int totalss = Convert.ToInt32(Math.Round(Convert.ToDecimal(k1)));
            //string k = totalss.ToWords();
            Numalet let = new Numalet();
            let.MascaraSalidaDecimal = "00/100 M.N";
            let.SeparadorDecimalSalida = "pesos";
            let.LetraCapital = true;
            let.ApocoparUnoParteEntera = true;

            DataRow rowt2 = Variables.Foliotiket.NewRow();
            rowt2["Foliotransaccion"] = Variables.foliotransaccion;
            rowt2["Letra"] = let.ToCustomCardinal(k1);
            rowt2["Total"] = totals.Replace("$", "").Replace(",", "");
            rowt2["Subtotal"] = string.Format(new CultureInfo("es-MX"), "{0:C2}", Subtotal);
            rowt2["IVA"] = string.Format(new CultureInfo("es-MX"), "{0:C2}", IVA);
            Variables.Foliotiket.Rows.Add(rowt2);

            Impresion im = new Impresion();
            im.ShowDialog();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            string[] separadas;
            string check = string.Empty;
            bool on = false;
            loading = new Loading();
            loading.Show(this);
            
            Tax.IsActive = true;
            Tax.Name = txtName.Text;
            Tax.RFC = string.IsNullOrEmpty(txtRFC.Text) ? "XAXX010101000" : txtRFC.Text;
            Tax.CURP = txtCURP.Text;
            Tax.PhoneNumber = txtPhone.Text == "" ? "0" : txtPhone.Text;
            Tax.EMail = txtEmail.Text;
            if(idUser != 0)
            {
                orderSales.TaxUserId = idUser;
            }
            else if (txtName.Text=="")
            {

                orderSales.TaxUserId = 1;
            }
            else
            {
                orderSales.TaxUserId = 0;
                List<Model.TaxAddress> taxAddresses = new List<Model.TaxAddress>();

                Model.TaxAddress taxs = new Model.TaxAddress
                {
                    Street = txtAddress.Text,
                    Outdoor = string.IsNullOrEmpty(txtOutdoor.Text) ? "0" : txtOutdoor.Text,
                    Indoor = txtIndoor.Text,
                    Zip = txtCP.Text,
                    Suburb = txtSuburb.Text,
                    Town = txtTown.Text,
                    State = txtState.Text
                };
                taxAddresses.Add(taxs);
                Tax.TaxAddresses = taxAddresses;
                orderSales.TaxUser = Tax;
            }
            
            check = await q.POSTOrderSales("/api/OrderSales", orderSales);
            loading.Close();
            separadas = check.Split('/');
            if (check.Contains("error"))
            {

                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                mensaje.ShowDialog();

            }

            else
            {

                taxresult = JsonConvert.DeserializeObject<Model.Taxresult>(check);
                mensaje = new MessageBoxForm(Variables.titleprincipal, string.Format("Orden generado correctamente con el folio: [ {0} ]", taxresult.folio), TypeIcon.Icon.Success);
                mensaje.ShowDialog();
                
                if (Properties.Settings.Default.Printer == true)
                {
                    on = q.EstaEnLineaLaImpresora(q.ImpresoraPredeterminada());
                    if (on == true)
                    {
                        TiketProductos imp = new TiketProductos();
                        imp.Imprime(dts, 2, totals.Replace("$", ""), taxresult.TaxUser.Name, taxresult.folio, taxresult.TaxUser.RFC);
                    }
                }
                else
                {
                    Variables.optionvistaimpresion = 3;
                    impresionhoja();
                }

                IForm formInterface = this.Owner as IForm;
                if (formInterface != null)
                {
                    formInterface.ShowForm("SOAPAP", "UI.Productos");
                }

                this.Close();
            }
            
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                e.Handled = true;
            }
        }

        private void txtRFC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && !(char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
           
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtCURP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && !(char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }

            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                TaxAddress address = null;
                loading = new Loading();
                loading.Show(this);
                var results = await Requests.SendURIAsync(String.Format("/api/TaxUsers/Search/{0}",txtName.Text), HttpMethod.Get, Variables.LoginModel.Token);
                if (results.Contains("error"))
                {
                    try
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(results).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        loading.Close();
                    }
                    catch (Exception)
                    {
                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        loading.Close();
                    }
                }
                else
                {

                    loading.Close();
                    users = JsonConvert.DeserializeObject<List<TaxUser>>(results);
                    if(users.Count > 1)
                    {
                        var datauser = users.Select(x => new DataUsers
                        {
                            id = x.Id,
                            Nombre = x.Name,
                            RFC = x.RFC
                        }).ToList();

                        Table = ConvertToDataTable<DataUsers>(datauser);


                        SearchUser searchUser = new SearchUser(Table);
                        searchUser.ShowDialog(this);

                    }
                    else
                    {
                        loading = new Loading();
                        loading.Show(this);
                        var resultss = await Requests.SendURIAsync(String.Format("/api/TaxUsers/SearchAddress/{0}", users.FirstOrDefault().Id), HttpMethod.Get, Variables.LoginModel.Token);
                        if (results.Contains("error"))
                        {
                            try
                            {
                                mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(resultss).error, TypeIcon.Icon.Cancel);
                                result = mensaje.ShowDialog();
                                loading.Close();
                            }
                            catch (Exception)
                            {
                                mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador", TypeIcon.Icon.Cancel);
                                result = mensaje.ShowDialog();
                                loading.Close();
                            }
                        }
                        else
                        {
                            loading.Close();
                            address = JsonConvert.DeserializeObject<TaxAddress>(resultss);
                            txtAddress.Text = address.Street;
                            txtOutdoor.Text = address.Outdoor;
                            txtIndoor.Text = address.Indoor;
                            txtCP.Text = address.Zip;
                            txtState.Text = address.State;
                            txtSuburb.Text = address.Suburb;
                            txtTown.Text = address.Town;
                        }

                        txtName.Text = users.FirstOrDefault().Name;
                        txtRFC.Text = users.FirstOrDefault().RFC;
                        txtCURP.Text = users.FirstOrDefault().CURP;
                        txtPhone.Text = users.FirstOrDefault().PhoneNumber;
                        txtEmail.Text = users.FirstOrDefault().EMail;

                        TaxUser.Name = users.FirstOrDefault().Name;
                        TaxUser.RFC = users.FirstOrDefault().RFC;
                        TaxUser.CURP = users.FirstOrDefault().CURP;
                        TaxUser.PhoneNumber = users.FirstOrDefault().PhoneNumber;
                        TaxUser.EMail = users.FirstOrDefault().EMail;
                        TaxUser.TaxAddresses.Add(address);
                    }
                  
                }
               
            }
        }

        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public async void addInfoUser(int id)
        {
            TaxUser user = users.Find(x => x.Id == id);
            if(user != null)
            {
                loading = new Loading();
                loading.Show(this);
                var results = await Requests.SendURIAsync(String.Format("/api/TaxUsers/SearchAddress/{0}", id), HttpMethod.Get, Variables.LoginModel.Token);
                if (results.Contains("error"))
                {
                    try
                    {
                        mensaje = new MessageBoxForm("Error", JsonConvert.DeserializeObject<Error>(results).error, TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        loading.Close();
                    }
                    catch (Exception)
                    {
                        mensaje = new MessageBoxForm("Error", "Servicio no disponible favor de comunicarse con el administrador", TypeIcon.Icon.Cancel);
                        result = mensaje.ShowDialog();
                        loading.Close();
                    }
                }
                else
                {
                    loading.Close();
                    TaxAddress address = JsonConvert.DeserializeObject<TaxAddress>(results);
                    txtAddress.Text = address.Street;
                    txtOutdoor.Text = address.Outdoor;
                    txtIndoor.Text = address.Indoor;
                    txtCP.Text = address.Zip;
                    txtState.Text = address.State;
                    txtSuburb.Text = address.Suburb;
                    txtTown.Text = address.Town;
                }

                txtName.Text = user.Name;
                txtRFC.Text = user.RFC;
                txtCURP.Text = user.CURP;
                txtPhone.Text = user.PhoneNumber;
                txtEmail.Text = user.EMail;

            }
        }
    }

    public partial class DataUsers
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
    }
}
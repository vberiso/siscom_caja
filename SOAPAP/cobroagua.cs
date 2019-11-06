using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Humanizer;
using SOAPAP.Enums;
using SOAPAP.UI;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SOAPAP
{
    public partial class cobroagua : Form
    {

        public cobroagua()
        {
            InitializeComponent();
        }

        querys q = new querys();
        string idp = string.Empty;
        string cuentap = string.Empty;
        string namesp = string.Empty;
        string rfcp = string.Empty;
        string direccionp = string.Empty;
        string numDerivatives = string.Empty;
        string typeService = string.Empty;
        string typeConsume = string.Empty;
        string typeRegime = string.Empty;
        string typePeriod = string.Empty;
        string typeStateService = string.Empty;
        string typeIntake = string.Empty;
        string taxableBase = string.Empty;
        string ground = string.Empty;
        string built = string.Empty;
        DataTable dt = new DataTable();
        DataTable dtcombo1 = new DataTable();
        DataTable dtcombo2 = new DataTable();
        DataTable desiredResult = new DataTable();
        DataTable debs = new DataTable();
        DataTable predial = new DataTable();
        DataTable limpia = new DataTable();
        DataTable servicos = new DataTable();
        DataTable notificaciones = new DataTable();
        DataTable productos = new DataTable();
        string k=string.Empty;
        bool parcial = false;
        decimal totalT = 0;
        int porcentaje = 0;
        int contador = 0;
        List<Deb> n = new List<Deb>();
        Form mensaje;
        DialogResult result = new DialogResult();
        bool checkbancos = false;
        bool pl = false;


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
            try
            {
                desiredResult = dt.AsEnumerable()
                  .GroupBy(r => r.Field<string>("name_concept"))
                  .Select(g =>
                  {
                      var row = dt.NewRow();
                      row["name_concept"] = g.Key;
                      row["deuda"] = g.Sum(r => r.Field<decimal>("deuda"));
                      row["code_concept"] = g.Average(r => r.Field<int>("code_concept"));
                      return row;
                  }).CopyToDataTable();
            }
            catch
            {

            }
            
            dataGridView1.DataSource = desiredResult;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;

        }


        public void cargardetalle()
        {
            
            dataGridView2.DataSource = debs;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AutoGenerateColumns = false;

        }

        public void predials()
        {
            dataGridView3.DataSource = predial;
            dataGridView3.AutoGenerateColumns = false;
            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.AutoGenerateColumns = false;
        }

        public void limpias()
        {
            dataGridView4.DataSource = limpia;
            dataGridView4.AutoGenerateColumns = false;
            dataGridView4.AllowUserToAddRows = false;
            dataGridView4.AutoGenerateColumns = false;
        }

        public void servicios()
        {
            dataGridView7.DataSource = servicos;
            dataGridView7.AutoGenerateColumns = false;
            dataGridView7.AllowUserToAddRows = false;
            dataGridView7.AutoGenerateColumns = false;
        }

        public void notificacione()
        {
            dataGridView8.DataSource = notificaciones;
            dataGridView8.AutoGenerateColumns = false;
            dataGridView8.AllowUserToAddRows = false;
            dataGridView8.AutoGenerateColumns = false;
        }

        public void productoss()
        {
            dataGridView9.DataSource = productos;
            dataGridView9.AutoGenerateColumns = false;
            dataGridView9.AllowUserToAddRows = false;
            dataGridView9.AutoGenerateColumns = false;
        }

        public void productossA()
        {
            dataGridView5.DataSource = productos;
            dataGridView5.AutoGenerateColumns = false;
            dataGridView5.AllowUserToAddRows = false;
            dataGridView5.AutoGenerateColumns = false;
        }

        public void cargarmetodosdepago()
        {
            comboBox1.DataSource = dtcombo1;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
        }

        public  void cargarbancos()
        {
            DataView dv = new DataView(dtcombo2);
            dv.RowFilter = "isBank = 'True'";
            comboBox2.DataSource = dv;
            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "id";
        }

        public async Task cargars()
        {
            alzheimer();
            decimal resultado = 0;
            decimal subtotal = 0;
            decimal iva = 0;
            decimal ivatotal = 0;
            string[] separadas;
            parcial = false;
            pl = false;

            Form loading = new Loading();
            loading.Show(this);

            dt = await q.GETAgreementsbyaccount("/api/Agreements/AgreementByAccount/" + Variables.cuenta + "");
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    separadas = row[0].ToString().Split('/');
                    if (separadas[0].ToString() == "error")
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "No existe cuenta", TypeIcon.Icon.Info);
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

            foreach (DataRow row in dt.Rows)
            {
                idp = row[0].ToString();
                cuentap = row[1].ToString();
                namesp = row[2].ToString();
                rfcp = row[3].ToString();
                direccionp = row[4].ToString();
                numDerivatives = row[5].ToString();
                typeService = row[6].ToString();
                typeConsume = row[7].ToString();
                typeRegime = row[8].ToString();
                typePeriod = row[9].ToString();
                typeStateService = row[10].ToString();
                typeIntake = row[11].ToString();
                taxableBase = row[12].ToString();
                ground = row[13].ToString();
                built = row[14].ToString();
            }


            Variables.idagrement = idp;
            label8.Text = idp;
            label1.Text = cuentap;
            textBox3.Text = cuentap;
            label2.Text = namesp;
            label3.Text = rfcp;
            label4.Text = direccionp;
            label12.Text = numDerivatives;
            label20.Text = typeService;
            label21.Text = typeConsume;
            label15.Text = typeRegime;
            label16.Text = typePeriod;
            label22.Text = typeStateService;
            label17.Text = typeIntake;
            label27.Text = typeIntake;
            label29.Text = taxableBase;
            label28.Text = ground;
            label30.Text = built;

            iva = Convert.ToDecimal(Variables.Configuration.IVA);

            if (Variables.anticipo == 1)
            {

                decimal total = 0;
                string cadena = string.Empty;

                if (typeIntake == "HABITACIONAL")
                {

                    dt = await q.GETDiscountValidator("/api/DiscountValidator/DiscountAnnual/" + Variables.idagrement + "/" + Variables.Configuration.IsMunicipal + "");
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

                    foreach (DataRow row in dt.Rows)
                    {
                        cadena = row[5].ToString();
                        string[] splitString = cadena.Split(' ');
                        string result = splitString[0].Trim();
                        if (result != "DESCUENTO")
                        {
                            total = total + Convert.ToDecimal(row[7]);
                            porcentaje = Convert.ToInt32(row[6]);
                        }
                    }

                    try
                    {
                        
                        totalT = total ;
                        label11.Text = "$" + total.ToString();
                        label13.Text = "$0.00";
                        label23.Text = "$" + totalT.ToString();
                        textBox1.Text = totalT.ToString();
                        dataGridView1.ClearSelection();
                    }

                    catch (Exception)
                    {

                    }
                    dtcombo1 = await q.GETPayMethod("/api/PayMethod");
                    if (dtcombo1 != null)
                    {
                        foreach (DataRow row in dtcombo1.Rows)
                        {
                            separadas = row[0].ToString().Split('/');
                            if (separadas[0].ToString() == "error")
                            {
                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                mensaje.ShowDialog();
                                dtcombo1.Rows.Clear();
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


                    cargarmetodosdepago();
                    loading.Close();
                }
                else
                {
                    button2.Enabled = false;

                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Las características del contrato no cuenta con lo necesario para asignar un descuento: [Tipo de Toma no es Habitacional]", TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                    loading.Close();
                }

            }

            else if (Variables.anticipo == 2)
            {
                dtcombo1 = await q.GETPayMethod("/api/PayMethod");
                if (dtcombo1 != null)
                {
                    foreach (DataRow row in dtcombo1.Rows)
                    {
                        separadas = row[0].ToString().Split('/');
                        if (separadas[0].ToString() == "error")
                        {
                            mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                            mensaje.ShowDialog();
                            dtcombo1.Rows.Clear();
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

                cargarmetodosdepago();
                loading.Close();

            }
            else
            {
                dt = await q.GETDebts("/api/Debts/" + idp + "");

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

                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToBoolean(row[3]) == true)
                    {
                        resultado = resultado + Math.Round((((Convert.ToDecimal(row[1]) - Convert.ToDecimal(row[2])) * iva) / 100), 2);
                        row[10] = Math.Round((((Convert.ToDecimal(row[1]) - Convert.ToDecimal(row[2])) * iva) / 100), 2);
                        Variables.sitieneiva = true;
                    }
                    subtotal = subtotal + Convert.ToDecimal(row[7]);
                }

                try
                {
                    bool saber = false;
                    string x = string.Empty;
                    foreach (var dt in Variables.debst)
                    {
                        if ((Convert.ToString(dt.type) == "TIP01" || Convert.ToString(dt.type) == "TIP04") && saber == false)
                        {
                            x = Variables.debst[0].fromDate.ToString("dd-MM-yyyy");
                            label25.Text = Variables.debst[0].fromDate.ToString("dd-MM-yyyy");
                            label25.Text = label25.Text + " a " + Variables.debst[0].untilDate.ToString("dd-MM-yyyy");
                            saber = true;
                        }
                        else if ((Convert.ToString(dt.type) == "TIP01" || Convert.ToString(dt.type) == "TIP04") && saber == true)
                        {
                            label25.Text = x + " a " + Variables.debst[0].untilDate.ToString("dd-MM-yyyy");
                        }
                    }
                }
                catch (Exception)
                {

                }



                cargar();
                

                ivatotal = resultado;
                totalT = subtotal + ivatotal;
                label11.Text = "$" + subtotal.ToString();
                label13.Text = "$" + ivatotal.ToString();
                label23.Text = "$" + totalT.ToString();
                textBox1.Text = totalT.ToString();



                debs.Rows.Clear();

                for (int i = 0; i < Variables.debst.Count; i++)
                {

                    DataRow rowq = debs.NewRow();
                    rowq[0] = Variables.debst[i].id;
                    rowq["amount"] = Variables.debst[i].amount;
                    rowq["onAccount"] = Variables.debst[i].onAccount;
                    rowq["name_concept"] = "Periodo " + Convert.ToDateTime(Variables.debst[i].fromDate).ToString("dd-MM-yyyy") + " a " + Convert.ToDateTime(Variables.debst[i].untilDate).ToString("dd-MM-yyyy");
                    rowq["deuda"] = Convert.ToDecimal(Variables.debst[i].amount) - Convert.ToDecimal(Variables.debst[i].onAccount);
                    debs.Rows.Add(rowq);

                    foreach (var item in Variables.debst[i].debtdetails)
                    {

                        DataRow row1 = debs.NewRow();
                        row1["Id"] = item.id;
                        row1["amount"] = item.amount;
                        row1["onAccount"] = item.onAccount;
                        row1["have_tax"] = item.haveTax;
                        row1["code_concept"] = item.codeConcept;
                        row1["name_concept"] = "-->" + item.nameConcept;
                        row1["debtId"] = item.debtId.ToString();
                        row1["deuda"] = item.amount;
                        debs.Rows.Add(row1);
                    }
                }

                
                if (Variables.Configuration.IsMunicipal == true)
                {
                    predial.Rows.Clear();
                    limpia.Rows.Clear();
                    for (int i = 0; i < Variables.debst.Count; i++)
                    {
                        if (Variables.debst[i].type == "TIP01")
                        {
                            DataRow row = predial.NewRow();
                            row["Id"] = Variables.debst[i].id;
                            row["name_concept"] = Variables.debst[i].year;

                            foreach (var item in Variables.debst[i].debtdetails)
                            {

                                if (item.nameConcept == "PREDIAL")
                                {

                                    row["amount"] = Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount);
                                }
                                else if (item.nameConcept == "RECARGO")
                                {
                                    row["onAccount"] = Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount);
                                }
                            }
                            predial.Rows.Add(row);
                        }
                    }

                    for (int i = 0; i < Variables.debst.Count; i++)
                    {

                        if (Variables.debst[i].type == "TIP04")
                        {

                            DataRow row = limpia.NewRow();
                            row["Id"] = Variables.debst[i].id;
                            row["name_concept"] = Variables.debst[i].year;
                            foreach (var item in Variables.debst[i].debtdetails)
                            {

                                if (item.nameConcept == "LIMPIA")
                                {

                                    row["amount"] = Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount);
                                }
                                else if (item.nameConcept == "RECARGO")
                                {
                                    row["onAccount"] = Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount);
                                }
                            }
                            limpia.Rows.Add(row);
                        }
                    }


                    for (int i = 0; i < Variables.debst.Count; i++)
                    {

                        if (Variables.debst[i].type == "TIP02")
                        {

                            DataRow rowt = productos.NewRow();
                            rowt["Id"] = Variables.debst[i].id;
                            rowt["name_concept"] = Variables.debst[i].fromDate.ToString("dd-MM-yyyy") + " a " + Variables.debst[i].untilDate.ToString("dd-MM-yyyy");
                            rowt["amount"] = Convert.ToDecimal(Variables.debst[i].amount) - Convert.ToDecimal(Variables.debst[i].onAccount);
                            rowt["description"] = "PRODUCTOS";
                            productos.Rows.Add(rowt);
                        }

                    }

                    predials();
                    limpias();
                    productossA();
                    dataGridViewTextBoxColumn1.Visible = false;
                    dataGridViewTextBoxColumn9.Visible = false;
                    dataGridViewTextBoxColumn17.Visible = false;

                }

                else
                {

                    servicos.Rows.Clear();
                    productos.Rows.Clear();
                    notificaciones.Rows.Clear();

                    for (int i = 0; i < Variables.debst.Count; i++)
                    {
                        if (Variables.debst[i].type == "TIP01")
                        {
                            DataRow roww = servicos.NewRow();
                            roww["Id"] = Variables.debst[i].id;
                            roww["name_concept"] = Variables.debst[i].fromDate.ToString("dd-MM-yyyy") + " a " + Variables.debst[i].untilDate.ToString("dd-MM-yyyy");
                            roww["amount"] = Convert.ToDecimal(Variables.debst[i].amount) - Convert.ToDecimal(Variables.debst[i].onAccount);
                            roww["description"] = "SERVICIO";
                            servicos.Rows.Add(roww);
                        }

                    }
                    
                    for (int i = 0; i < Variables.debst.Count; i++)
                    {

                        if (Variables.debst[i].type == "TIP02")
                        {

                            DataRow rowt = productos.NewRow();
                            rowt["Id"] = Variables.debst[i].id;
                            rowt["name_concept"] = Variables.debst[i].fromDate.ToString("dd-MM-yyyy") + " a " + Variables.debst[i].untilDate.ToString("dd-MM-yyyy");
                            rowt["amount"] = Convert.ToDecimal(Variables.debst[i].amount) - Convert.ToDecimal(Variables.debst[i].onAccount);
                            rowt["description"] = "PRODUCTOS";
                            productos.Rows.Add(rowt);
                        }

                    }

                    
                    for (int i = 0; i < Variables.debst.Count; i++)
                    {

                        if (Variables.debst[i].type == "TIP03")
                        {

                            DataRow rows = notificaciones.NewRow();
                            rows["Id"] = Variables.debst[i].id;
                            rows["name_concept"] = Variables.debst[i].fromDate.ToString("dd-MM-yyyy") + " a " + Variables.debst[i].untilDate.ToString("dd-MM-yyyy");
                            rows["amount"] = Convert.ToDecimal(Variables.debst[i].amount) - Convert.ToDecimal(Variables.debst[i].onAccount);
                            rows["description"] = "NOTIFICACIONES";
                            notificaciones.Rows.Add(rows);

                        }

                    }

                    servicios();
                    notificacione();
                    productoss();

                    
                }

                cargardetalle();
                dtcombo1 = await q.GETPayMethod("/api/PayMethod");
                if (dtcombo1 != null)
                {
                    foreach (DataRow row in dtcombo1.Rows)
                    {
                        separadas = row[0].ToString().Split('/');
                        if (separadas[0].ToString() == "error")
                        {
                            mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                            mensaje.ShowDialog();
                            dtcombo1.Rows.Clear();
                            break;
                        }

                    }
                }
                else
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                }

                cargarmetodosdepago();
                loading.Close();

                if (Variables.Configuration.IsMunicipal != true)
                { 
                if (label23.Text.Replace("$", "") == "0")
                {
                        Form mensaje = new MessageBoxForm("Alerta", "No hay adeudos deseas generar un anticipo", TypeIcon.Icon.Info, true);
                        result = mensaje.ShowDialog();
                        if (result == DialogResult.OK)
                            {
                                Anticipo ac = new Anticipo();
                                ac.ShowDialog(this);
                            }
                        else
                        {
                            Variables.anticipo = 0;
                        }
                }
                }
            }
        }

        public void cobroagua_Load(object sender, EventArgs e)
        {
            alzheimer();
            //Detalle
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Id";
            debs.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "amount";
            debs.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "onAccount";
            debs.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "have_tax";
            debs.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "code_concept";
            debs.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "name_concept";
            debs.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "debtId";
            debs.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "deuda";
            debs.Columns.Add(column);


            //Predial
            DataColumn columns;
            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.Int32");
            columns.ColumnName = "Id";
            predial.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.String");
            columns.ColumnName = "name_concept";
            predial.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.Decimal");
            columns.ColumnName = "amount";
            columns.DefaultValue = 0.00;
            predial.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.Decimal");
            columns.ColumnName = "onAccount";
            columns.DefaultValue = 0.00;
            predial.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.Boolean");
            columns.ColumnName = "have_tax";
            predial.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.Int32");
            columns.ColumnName = "code_concept";
            predial.Columns.Add(columns);
            
            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.Int32");
            columns.ColumnName = "debtId";
            predial.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.Decimal");
            columns.ColumnName = "deuda";
            predial.Columns.Add(columns);

            columns = new DataColumn();
            columns.DataType = System.Type.GetType("System.Boolean");
            columns.ColumnName = "check";
            columns.DefaultValue = true;
            predial.Columns.Add(columns);


            //Limpia
            DataColumn columnsw;
            columnsw = new DataColumn();
            columnsw.DataType = System.Type.GetType("System.Int32");
            columnsw.ColumnName = "Id";
            limpia.Columns.Add(columnsw);

            columnsw = new DataColumn();
            columnsw.DataType = System.Type.GetType("System.String");
            columnsw.ColumnName = "name_concept";
            limpia.Columns.Add(columnsw);

            columnsw = new DataColumn();
            columnsw.DataType = System.Type.GetType("System.Decimal");
            columnsw.ColumnName = "amount";
            columnsw.DefaultValue = 0.00;
            limpia.Columns.Add(columnsw);

            columnsw = new DataColumn();
            columnsw.DataType = System.Type.GetType("System.Decimal");
            columnsw.ColumnName = "onAccount";
            columnsw.DefaultValue = 0.00;
            limpia.Columns.Add(columnsw);

            columnsw = new DataColumn();
            columnsw.DataType = System.Type.GetType("System.Boolean");
            columnsw.ColumnName = "have_tax";
            limpia.Columns.Add(columnsw);

            columnsw = new DataColumn();
            columnsw.DataType = System.Type.GetType("System.Int32");
            columnsw.ColumnName = "code_concept";
            limpia.Columns.Add(columnsw);
            
            columnsw = new DataColumn();
            columnsw.DataType = System.Type.GetType("System.Int32");
            columnsw.ColumnName = "debtId";
            limpia.Columns.Add(columnsw);

            columnsw = new DataColumn();
            columnsw.DataType = System.Type.GetType("System.Decimal");
            columnsw.ColumnName = "deuda";
            limpia.Columns.Add(columnsw);

            columnsw = new DataColumn();
            columnsw.DataType = System.Type.GetType("System.Boolean");
            columnsw.ColumnName = "check";
            columnsw.DefaultValue = true;
            limpia.Columns.Add(columnsw);


            //Servicios
            DataColumn columnsw1;
            columnsw1 = new DataColumn();
            columnsw1.DataType = System.Type.GetType("System.Int32");
            columnsw1.ColumnName = "Id";
            servicos.Columns.Add(columnsw1);

            columnsw1 = new DataColumn();
            columnsw1.DataType = System.Type.GetType("System.Boolean");
            columnsw1.ColumnName = "check";
            columnsw1.DefaultValue = true;
            servicos.Columns.Add(columnsw1);

            columnsw1 = new DataColumn();
            columnsw1.DataType = System.Type.GetType("System.String");
            columnsw1.ColumnName = "description";
            servicos.Columns.Add(columnsw1);

            columnsw1 = new DataColumn();
            columnsw1.DataType = System.Type.GetType("System.String");
            columnsw1.ColumnName = "name_concept";
            servicos.Columns.Add(columnsw1);

            columnsw1 = new DataColumn();
            columnsw1.DataType = System.Type.GetType("System.Decimal");
            columnsw1.ColumnName = "amount";
            columnsw1.DefaultValue = 0.00;
            servicos.Columns.Add(columnsw1);

            columnsw1 = new DataColumn();
            columnsw1.DataType = System.Type.GetType("System.Decimal");
            columnsw1.ColumnName = "onAccount";
            columnsw1.DefaultValue = 0.00;
            servicos.Columns.Add(columnsw1);

            columnsw1 = new DataColumn();
            columnsw1.DataType = System.Type.GetType("System.Boolean");
            columnsw1.ColumnName = "have_tax";
            servicos.Columns.Add(columnsw1);

            columnsw1 = new DataColumn();
            columnsw1.DataType = System.Type.GetType("System.Int32");
            columnsw1.ColumnName = "code_concept";
            servicos.Columns.Add(columnsw1);

            columnsw1 = new DataColumn();
            columnsw1.DataType = System.Type.GetType("System.Decimal");
            columnsw1.ColumnName = "deuda";
            servicos.Columns.Add(columnsw1);

            columnsw1 = new DataColumn();
            columnsw1.DataType = System.Type.GetType("System.Int32");
            columnsw1.ColumnName = "debtId";
            servicos.Columns.Add(columnsw1);

            //Notificaciones
            DataColumn columnsw2;
            columnsw2 = new DataColumn();
            columnsw2.DataType = System.Type.GetType("System.Int32");
            columnsw2.ColumnName = "Id";
            notificaciones.Columns.Add(columnsw2);

            columnsw2 = new DataColumn();
            columnsw2.DataType = System.Type.GetType("System.Boolean");
            columnsw2.ColumnName = "check";
            columnsw2.DefaultValue = true;
            notificaciones.Columns.Add(columnsw2);

            columnsw2 = new DataColumn();
            columnsw2.DataType = System.Type.GetType("System.String");
            columnsw2.ColumnName = "description";
            notificaciones.Columns.Add(columnsw2);

            columnsw2 = new DataColumn();
            columnsw2.DataType = System.Type.GetType("System.String");
            columnsw2.ColumnName = "name_concept";
            notificaciones.Columns.Add(columnsw2);

            columnsw2 = new DataColumn();
            columnsw2.DataType = System.Type.GetType("System.Decimal");
            columnsw2.ColumnName = "amount";
            columnsw2.DefaultValue = 0.00;
            notificaciones.Columns.Add(columnsw2);

            columnsw2 = new DataColumn();
            columnsw2.DataType = System.Type.GetType("System.Decimal");
            columnsw2.ColumnName = "onAccount";
            columnsw2.DefaultValue = 0.00;
            notificaciones.Columns.Add(columnsw2);

            columnsw2 = new DataColumn();
            columnsw2.DataType = System.Type.GetType("System.Boolean");
            columnsw2.ColumnName = "have_tax";
            notificaciones.Columns.Add(columnsw2);

            columnsw2 = new DataColumn();
            columnsw2.DataType = System.Type.GetType("System.Int32");
            columnsw2.ColumnName = "code_concept";
            notificaciones.Columns.Add(columnsw2);

            columnsw2 = new DataColumn();
            columnsw2.DataType = System.Type.GetType("System.Decimal");
            columnsw2.ColumnName = "deuda";
            notificaciones.Columns.Add(columnsw2);

            columnsw2 = new DataColumn();
            columnsw2.DataType = System.Type.GetType("System.Int32");
            columnsw2.ColumnName = "debtId";
            notificaciones.Columns.Add(columnsw2);

            //Productos
            DataColumn columnsw3;
            columnsw3 = new DataColumn();
            columnsw3.DataType = System.Type.GetType("System.Int32");
            columnsw3.ColumnName = "Id";
            productos.Columns.Add(columnsw3);

            columnsw3 = new DataColumn();
            columnsw3.DataType = System.Type.GetType("System.Boolean");
            columnsw3.ColumnName = "check";
            columnsw3.DefaultValue = true;
            productos.Columns.Add(columnsw3);

            columnsw3 = new DataColumn();
            columnsw3.DataType = System.Type.GetType("System.String");
            columnsw3.ColumnName = "description";
            productos.Columns.Add(columnsw3);

            columnsw3 = new DataColumn();
            columnsw3.DataType = System.Type.GetType("System.String");
            columnsw3.ColumnName = "name_concept";
            productos.Columns.Add(columnsw3);

            columnsw3 = new DataColumn();
            columnsw3.DataType = System.Type.GetType("System.Decimal");
            columnsw3.ColumnName = "amount";
            columnsw3.DefaultValue = 0.00;
            productos.Columns.Add(columnsw3);

            columnsw3 = new DataColumn();
            columnsw3.DataType = System.Type.GetType("System.Decimal");
            columnsw3.ColumnName = "onAccount";
            columnsw3.DefaultValue = 0.00;
            productos.Columns.Add(columnsw3);

            columnsw3 = new DataColumn();
            columnsw3.DataType = System.Type.GetType("System.Boolean");
            columnsw3.ColumnName = "have_tax";
            productos.Columns.Add(columnsw3);

            columnsw3 = new DataColumn();
            columnsw3.DataType = System.Type.GetType("System.Int32");
            columnsw3.ColumnName = "code_concept";
            productos.Columns.Add(columnsw3);

            columnsw3 = new DataColumn();
            columnsw3.DataType = System.Type.GetType("System.Decimal");
            columnsw3.ColumnName = "deuda";
            productos.Columns.Add(columnsw3);

            columnsw3 = new DataColumn();
            columnsw3.DataType = System.Type.GetType("System.Int32");
            columnsw3.ColumnName = "debtId";
            productos.Columns.Add(columnsw3);
            

            Variables.sitieneiva = false;
            
            if (Variables.Configuration.IsMunicipal != true)
            {
                tableLayoutPanel10.Visible = false;
                tabControl1.TabPages[1].Hide();
                tableLayoutPanel1.Location = new System.Drawing.Point(14, 377);
                tableLayoutPanel6.Location = new System.Drawing.Point(14, 600);
                tabControl1.TabPages.Remove(tabPage3);
                tabControl1.TabPages.Remove(tabPage4);
                tabControl1.TabPages.Remove(tabPage5);
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage7);
                tabControl1.TabPages.Remove(tabPage8);
                tabControl1.TabPages.Remove(tabPage9);

            }

            if (Variables.oprtions==true)
            {
                cargars();
            }
        }

        
        private async void button2_Click(object sender, EventArgs e)
        {
            bool on = false;
            string xmltimbrado = string.Empty;
            if ((label23.Text.Replace("$", "") != "0")  || Variables.anticipo == 2 )
            {
                if (checkbancos == true && textBox2.Text=="")
                {    

                    mensaje = new MessageBoxForm("Alerta", "No a ristrado el numero de autorización?", TypeIcon.Icon.Info);
                    mensaje.ShowDialog();
                    return;
                }
                

                if (textBox1.Text!="")
                {
                    string resultado = string.Empty;
                    string[] separadas;
                    string pagoaux = string.Empty;
                    
                    Form mensaje = new MessageBoxForm("Alerta", "¿Esta seguro de generar la operación?", TypeIcon.Icon.Info, true);
                    result = mensaje.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        Form loading = new Loading();
                        loading.Show(this);

                    if (comboBox2.Text != "")
                    {
                        pagoaux = comboBox2.SelectedValue.ToString();
                    }
                    else
                    {
                        pagoaux = "1";
                    }

                    if (Variables.anticipo == 1)
                    {
                            string check = string.Empty;
                            string k = comboBox1.SelectedValue.ToString();
                            check = await q.POSTTransactionAntA("/api/Transaction/Prepaid/" + Variables.idagrement + "", "True", Convert.ToDecimal(label11.Text.Replace("$", "")), Convert.ToDecimal(label13.Text.Replace("$", "")), "0", Convert.ToDecimal(label14.Text.Replace("$", "")), Convert.ToDecimal(label23.Text.Replace("$", "")), "SISCOMCAJA", "3", k, Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString(), textBox2.Text, "1", pagoaux, "PAY06", "ED005", Convert.ToDecimal(textBox1.Text.Replace("$", "")), label8.Text, desiredResult, dt, porcentaje,label1.Text);
                            separadas = check.Split('/');
                            if (separadas[0].ToString() == "error")
                            {
                                loading.Close();
                                MessageBox.Show(separadas[1].ToString(), Variables.titleprincipal);
                            }
                            else
                            {

                                dt = await q.GETTransactionID("/api/Transaction/" + Variables.idtransaction);
                                loading.Close();
                                mensaje = new MessageBoxForm(Variables.titleprincipal, "Se a generado el anticipo correctamente", TypeIcon.Icon.Success);
                                mensaje.ShowDialog();

                                try
                                {
                                    desiredResult = dt.AsEnumerable()
                                      .GroupBy(r => r.Field<string>("name_concept"))
                                      .Select(g =>
                                      {
                                          var row = dt.NewRow();
                                          row["name_concept"] = g.Key;
                                          row["deuda"] = g.Sum(r => r.Field<decimal>("deuda"));
                                          return row;
                                      }).CopyToDataTable();
                                }
                                catch
                                {

                                }

                                on = q.EstaEnLineaLaImpresora(q.ImpresoraPredeterminada());
                                if (on == true)
                                {

                                    
                                    if (Properties.Settings.Default.Printer == true)
                                    {

                                        if (Variables.Configuration.CFDI == "Verdadero")
                                        {

                                            Form loadings = new Loading();
                                            loadings.Show(this);
                                            Facturaelectronica fs = new Facturaelectronica();
                                            //xmltimbrado = await fs.facturar(Variables.idtransaction.ToString(), "ET001","");
                                            loadings.Close();
                                            separadas = xmltimbrado.Split('/');
                                            if (separadas[0].ToString() == "error")
                                            {
                                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                                mensaje.ShowDialog();
                                            }
                                            else
                                            {
                                                PdfDocument pdfdocument = new PdfDocument();
                                                pdfdocument.LoadFromFile(xmltimbrado);
                                                pdfdocument.PrinterName = q.ImpresoraPredeterminada();
                                                pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                                                pdfdocument.PrintDocument.Print();
                                                pdfdocument.Dispose();
                                                //Directory.Delete(xmltimbrado, true);
                                            }

                                        }
                                        else
                                        {
                                            Tiket imp = new Tiket();
                                            imp.Imprime(dt, 2, label11.Text, label13.Text, label14.Text, label23.Text, comboBox1.Text, label2.Text, Variables.foliocaja, label25.Text, label1.Text, label3.Text, Variables.foliotransaccion, label4.Text);
                                            q.sacarcaja(q.ImpresoraPredeterminada(), Variables.Configuration.ANSII);
                                        }
                                    }
                                    else
                                    {
                                        
                                        if (Variables.Configuration.CFDI == "Verdadero")
                                        {
                                            Form loadings = new Loading();
                                            loadings.Show(this);
                                            Facturaelectronica fs = new Facturaelectronica();
                                            //xmltimbrado = await fs.facturar(Variables.idtransaction.ToString(), "ET001","");
                                            xmltimbrado = await fs.generaFactura(Variables.idtransaction.ToString(), "ET001");
                                            loadings.Close();
                                            separadas = xmltimbrado.Split('/');
                                            if (separadas[0].ToString() == "error")
                                            {
                                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                                mensaje.ShowDialog();
                                            }

                                            else
                                            {
                                                PdfDocument pdfdocument = new PdfDocument();
                                                pdfdocument.LoadFromFile(xmltimbrado);
                                                pdfdocument.PrinterName = q.ImpresoraPredeterminada();
                                                pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                                                pdfdocument.PrintDocument.Print();
                                                pdfdocument.Dispose();
                                                //Directory.Delete(xmltimbrado, true);
                                            }

                                        }
                                        else
                                        {
                                            Variables.optionvistaimpresion = 1;
                                            impresionhoja();
                                        }
                                    }

                                }
                                else
                                {
                                    mensaje = new MessageBoxForm(Variables.titleprincipal, "La impresora esta desconectada", TypeIcon.Icon.Cancel);
                                    mensaje.ShowDialog();
                                }

                                if(Variables.oprtions == true && Variables.anticipo !=0)
                                {
                                    IForm formInterface = this.Owner as IForm;
                                    if (formInterface != null)
                                    {
                                        Variables.anticipo = 0;
                                        Variables.oprtions = false;
                                        formInterface.ShowForm("SOAPAP", "cobroagua");
                                    }
                                    this.Close();
                                }

                                else if (Variables.oprtions != true)
                                {
                                    IForm formInterface = this.Owner as IForm;
                                    if (formInterface != null)
                                    {
                                        Variables.anticipo = 0;
                                        Variables.oprtions = false;
                                        formInterface.ShowForm("SOAPAP", "cobroagua");
                                    }
                                    this.Close();
                                }

                                else
                                {
                                    IForm formInterface = this.Owner as IForm;
                                    if (formInterface != null)
                                    {
                                        Variables.anticipo = 0;
                                        Variables.oprtions = false;
                                        formInterface.ShowForm("SOAPAP", "cobro");
                                    }
                                    this.Close();
                                }
                            }
                            }
                            else if (Variables.anticipo == 2)
                            {
                            string check = string.Empty;

                            check = await q.POSTTransactionAnt("/api/Transaction/Prepaid/" + Variables.idagrement + "", "", "True", "" + textBox1.Text + "", 0, "0", 0, Convert.ToDecimal(textBox1.Text), "SISCOMCAJA", "3", "1", Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString(), textBox2.Text, "1", "1", "PAY04", "ED005", Convert.ToDecimal(textBox1.Text), Variables.idagrement, "3", "Anticipo", textBox1.Text);
                            separadas = check.Split('/');
                            if (separadas[0].ToString() == "error")
                            {
                                loading.Close();
                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                mensaje.ShowDialog();
                            }
                            else
                            {
                                dt = await q.GETTransactionID("/api/Transaction/" + Variables.idtransaction);
                                loading.Close();
                                mensaje = new MessageBoxForm(Variables.titleprincipal, "Se a generado el anticipo correctamente", TypeIcon.Icon.Success);
                                mensaje.ShowDialog();
                                
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
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                                    mensaje.ShowDialog();
                                }

                                try
                                {
                                    desiredResult = dt.AsEnumerable()
                                      .GroupBy(r => r.Field<string>("name_concept"))
                                      .Select(g =>
                                      {
                                          var row = dt.NewRow();
                                          row["name_concept"] = g.Key;
                                          row["deuda"] = g.Sum(r => r.Field<decimal>("deuda"));
                                          return row;
                                      }).CopyToDataTable();
                                }
                                catch
                                {

                                }
                                on = q.EstaEnLineaLaImpresora(q.ImpresoraPredeterminada());
                                if (on == true)
                                {

                     
                                    if (Properties.Settings.Default.Printer == true)
                                    {

                                        if (Variables.Configuration.CFDI == "Verdadero")
                                        {
                                            Form loadings = new Loading();
                                            loadings.Show(this);
                                            Facturaelectronica fs = new Facturaelectronica();
                                            //xmltimbrado = await fs.facturar(Variables.idtransaction.ToString(), "ET001","");
                                            xmltimbrado = await fs.generaFactura(Variables.idtransaction.ToString(), "ET001");
                                            loadings.Close();
                                            separadas = xmltimbrado.Split('/');
                                            if (separadas[0].ToString() == "error")
                                            {
                                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                                mensaje.ShowDialog();
                                            }
                                            else
                                            {
                                                PdfDocument pdfdocument = new PdfDocument();
                                                pdfdocument.LoadFromFile(xmltimbrado);
                                                pdfdocument.PrinterName = q.ImpresoraPredeterminada();
                                                pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                                                pdfdocument.PrintDocument.Print();
                                                pdfdocument.Dispose();
                                                //Directory.Delete(xmltimbrado, true);
                                            }

                                        }
                                        else
                                        {
                                            Tiket imp = new Tiket();
                                            imp.Imprime(dt, 2, "$" + textBox1.Text, label13.Text, label14.Text, "$" + textBox1.Text, comboBox1.Text, label2.Text, Variables.foliocaja, label25.Text, label1.Text, label3.Text, Variables.foliotransaccion, label4.Text);
                                            q.sacarcaja(q.ImpresoraPredeterminada(), Variables.Configuration.ANSII);
                                        }
                                    }
                                    else
                                    {

                                        if (Variables.Configuration.CFDI == "Verdadero")
                                        {
                                            Form loadings = new Loading();
                                            loadings.Show(this);
                                            Facturaelectronica fs = new Facturaelectronica();
                                            //xmltimbrado = await fs.facturar(Variables.idtransaction.ToString(), "ET001","");
                                            xmltimbrado = await fs.generaFactura(Variables.idtransaction.ToString(), "ET001");
                                            loadings.Close();
                                            separadas = xmltimbrado.Split('/');
                                            if (separadas[0].ToString() == "error")
                                            {
                                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                                mensaje.ShowDialog();
                                            }

                                            else
                                            {
                                                PdfDocument pdfdocument = new PdfDocument();
                                                pdfdocument.LoadFromFile(xmltimbrado);
                                                pdfdocument.PrinterName = q.ImpresoraPredeterminada();
                                                pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                                                pdfdocument.PrintDocument.Print();
                                                pdfdocument.Dispose();
                                                //Directory.Delete(xmltimbrado, true);
                                            }
                                        }
                                        else
                                        {
                                            Variables.optionvistaimpresion = 1;
                                            impresionhoja();
                                        }
                                    }



                                }
                                else
                                {
                                    mensaje = new MessageBoxForm(Variables.titleprincipal, "La impresora esta desconectada", TypeIcon.Icon.Cancel);
                                    mensaje.ShowDialog();
                                }

                                if (Variables.oprtions == true && Variables.anticipo != 0)
                                {
                                    IForm formInterface = this.Owner as IForm;
                                    if (formInterface != null)
                                    {
                                        Variables.anticipo = 0;
                                        Variables.oprtions = false;
                                        formInterface.ShowForm("SOAPAP", "cobroagua");
                                    }
                                    this.Close();
                                }
                                else if (Variables.oprtions != true)
                                {
                                    IForm formInterface = this.Owner as IForm;
                                    if (formInterface != null)
                                    {
                                        Variables.anticipo = 0;
                                        Variables.oprtions = false;
                                        formInterface.ShowForm("SOAPAP", "cobroagua");
                                    }
                                    this.Close();
                                }
                                else
                                {
                                    IForm formInterface = this.Owner as IForm;
                                    if (formInterface != null)
                                    {
                                        Variables.anticipo = 0;
                                        Variables.oprtions = false;
                                        formInterface.ShowForm("SOAPAP", "cobro");
                                    }
                                    this.Close();
                                }
                              }
                            }
                            else
                            {
  
                            if (parcial != true && pl != true)
                            {
                                if (Variables.sitieneiva == true)
                                    {
                                    //TODO
                                    string k = comboBox1.SelectedValue.ToString();
                                    resultado = await q.POSTTransactionDebT( "/api/Transaction", "True", Convert.ToDecimal(label11.Text.Replace("$", "")), Convert.ToDecimal(label13.Text.Replace("$", "")), Variables.Configuration.IVA, Convert.ToDecimal(label14.Text.Replace("$", "")), Convert.ToDecimal(label23.Text.Replace("$", "")), "SISCOMCAJA", "3", k, Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString(), textBox2.Text, "1", pagoaux, "PAY01", "ED005", Convert.ToDecimal(textBox1.Text.Replace("$", "")), label8.Text, desiredResult, label1.Text);
                                    }
                                    else
                                    {   
                                    string k = comboBox1.SelectedValue.ToString();
                                    resultado = await q.POSTTransactionDebT( "/api/Transaction", "True", Convert.ToDecimal(label11.Text.Replace("$", "")), Convert.ToDecimal(label13.Text.Replace("$", "")), "0", Convert.ToDecimal(label14.Text.Replace("$", "")), Convert.ToDecimal(label23.Text.Replace("$", "")), "SISCOMCAJA", "3", k, Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString(), textBox2.Text, "1", pagoaux, "PAY01", "ED005", Convert.ToDecimal(textBox1.Text.Replace("$", "")), label8.Text, desiredResult, label1.Text);
                                    }
                            }

                            else if (pl == true)
                            {
                                if (label23.Text.Replace("$", "")==textBox1.Text)
                                { 
                                Venta ven = new Venta();
                                ven.debt = n;
                                TransactionVMS ms = new TransactionVMS();
                                List<TransactionDetail> lista = new List<TransactionDetail> ();
                                lista.Add(new TransactionDetail()
                                {
                                    codeConcept = "PAGO",
                                    description="PAGO",
                                    amount= Convert.ToDecimal(label11.Text.Replace("$", ""))

                                });
                                
                                ms.transactionDetails = lista;
                                ven.transaction = ms;
                                
                                if (Variables.sitieneiva == true)
                                {

                                        foreach (var item in ven.debt)
                                        {

                                            foreach( var n in item.debtdetails)
                                            {

                                                if(n.haveTax == true)
                                                {
                                                    n.tax = Math.Round(((Convert.ToDecimal(n.amount) - Convert.ToDecimal(n.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA)/100),2);
                                                }
                                                
                                            }
                                            
                                        }

                                    string k = comboBox1.SelectedValue.ToString();
                                    resultado =await q.POSTTransactionPL("/api/Transaction", "True", Convert.ToDecimal(label11.Text.Replace("$", "")), Convert.ToDecimal(label13.Text.Replace("$", "")), Variables.Configuration.IVA, Convert.ToDecimal(label14.Text.Replace("$", "")), Convert.ToDecimal(label23.Text.Replace("$", "")), "SISCOMCAJA", "3", k, Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString(), textBox2.Text, "1", pagoaux, "PAY01", "ED005", Convert.ToDecimal(textBox1.Text.Replace("$", "")), label8.Text, desiredResult, ven, label1.Text);
                                }

                                else
                                {
                                    string k = comboBox1.SelectedValue.ToString();
                                    resultado = await q.POSTTransactionPL("/api/Transaction", "True", Convert.ToDecimal(label11.Text.Replace("$", "")), Convert.ToDecimal(label13.Text.Replace("$", "")), "0", Convert.ToDecimal(label14.Text.Replace("$", "")), Convert.ToDecimal(label23.Text.Replace("$", "")), "SISCOMCAJA", "3", k, Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString(), textBox2.Text, "1", pagoaux, "PAY01", "ED005", Convert.ToDecimal(textBox1.Text.Replace("$", "")), label8.Text, desiredResult, ven, label1.Text);
                                }

                                }
                                else
                                {
                                    loading.Close();
                                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Los montos no coinciden", TypeIcon.Icon.Cancel);
                                    mensaje.ShowDialog();
                                    return;
                                }
                            }
                            else

                            {
                            if (Variables.sitieneiva == true)
                                    {
                                    string k = comboBox1.SelectedValue.ToString();
                                    resultado = await q.POSTTransactionDeb("/api/Transaction", "True", Convert.ToDecimal(label11.Text.Replace("$", "")), Convert.ToDecimal(label13.Text.Replace("$", "")), Variables.Configuration.IVA, Convert.ToDecimal(label14.Text.Replace("$", "")), Convert.ToDecimal(label23.Text.Replace("$", "")), "SISCOMCAJA", "3", k, Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString(), textBox2.Text, "1", pagoaux, "PAY01", "ED004", Convert.ToDecimal(textBox1.Text.Replace("$", "")), label8.Text, desiredResult, dt, contador, label1.Text);
                                    }
                                else
                                   {
                                    string k = comboBox1.SelectedValue.ToString();
                                    resultado = await q.POSTTransactionDeb( "/api/Transaction", "True", Convert.ToDecimal(label11.Text.Replace("$", "")), Convert.ToDecimal(label13.Text.Replace("$", "")), "0", Convert.ToDecimal(label14.Text.Replace("$", "")), Convert.ToDecimal(label23.Text.Replace("$", "")), "SISCOMCAJA", "3", k, Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString(), textBox2.Text, "1", pagoaux, "PAY01", "ED004", Convert.ToDecimal(textBox1.Text.Replace("$", "")), label8.Text, desiredResult, dt, contador, label1.Text);
                                }
                                }
                                separadas = resultado.Split('/');
                                if (separadas[0].ToString() == "error")
                                {
                                loading.Close();
                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                mensaje.ShowDialog();
                                }
                                else
                                {
                                dt = await q.GETTransactionID("/api/Transaction/" + Variables.idtransaction);
                                loading.Close();
                                mensaje = new MessageBoxForm(Variables.titleprincipal, "Se a registrado la operacion correctamente", TypeIcon.Icon.Success);
                                mensaje.ShowDialog();
                              
                                on = q.EstaEnLineaLaImpresora(q.ImpresoraPredeterminada());
                                if (on == true)
                                {
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
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                                        mensaje.ShowDialog();
                                    }

                                    
                                    if (Properties.Settings.Default.Printer == true)
                                    {

                                        if (Variables.Configuration.CFDI == "Verdadero")
                                        {
                                            Form loadings = new Loading();
                                            loadings.Show(this);
                                            Facturaelectronica fs = new Facturaelectronica();
                                            //xmltimbrado = await fs.facturar(Variables.idtransaction.ToString(), "ET001","");
                                            xmltimbrado = await fs.generaFactura(Variables.idtransaction.ToString(), "ET001");
                                            loadings.Close();
                                            separadas = xmltimbrado.Split('/');
                                            if(separadas[0].ToString() == "error")
                                            {
                                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                                mensaje.ShowDialog();
                                            }
                                            else
                                            {

                                                PdfDocument pdfdocument = new PdfDocument();
                                                pdfdocument.LoadFromFile(xmltimbrado);
                                                pdfdocument.PrinterName = q.ImpresoraPredeterminada();
                                                pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                                                pdfdocument.PrintDocument.Print();
                                                pdfdocument.Dispose();
                                 //               Directory.Delete(xmltimbrado, true);
                                            }
                                            
                                        }
                                        else
                                        { 
                                        Tiket imp = new Tiket();
                                        imp.Imprime(dt, 2, label11.Text, label13.Text, label14.Text, label23.Text, comboBox1.Text, label2.Text, Variables.foliocaja, label25.Text, label1.Text, label3.Text, Variables.foliotransaccion, label4.Text);
                                        q.sacarcaja(q.ImpresoraPredeterminada(), Variables.Configuration.ANSII);
                                        }
                                    }
                                    else
                                    {
                                       
                                        if (Variables.Configuration.CFDI == "Verdadero")
                                        {
                                            Form loadings = new Loading();
                                            loadings.Show(this);
                                            Facturaelectronica fs = new Facturaelectronica();
                                            //xmltimbrado = await fs.facturar(Variables.idtransaction.ToString(), "ET001","");
                                            xmltimbrado = await fs.generaFactura(Variables.idtransaction.ToString(), "ET001");
                                            loadings.Close();
                                            separadas = xmltimbrado.Split('/');
                                            if (separadas[0].ToString() == "error")
                                            {
                                                mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                                                mensaje.ShowDialog();
                                            }

                                            else
                                            {
                                                PdfDocument pdfdocument = new PdfDocument();
                                                pdfdocument.LoadFromFile(xmltimbrado);
                                                pdfdocument.PrinterName = q.ImpresoraPredeterminada();
                                                pdfdocument.PrintDocument.PrinterSettings.Copies = 1;
                                                pdfdocument.PrintDocument.Print();
                                                pdfdocument.Dispose();
                                               // Directory.Delete(xmltimbrado, true);
                                            }

                                        }
                                        else
                                        {
                                            Variables.optionvistaimpresion = 1;
                                            impresionhoja();
                                        }
                                    }
                                }
                                else
                                {
                                    
                                    mensaje = new MessageBoxForm(Variables.titleprincipal, "La impresora esta desconectada", TypeIcon.Icon.Success);
                                    mensaje.ShowDialog();
                                }

                                if (Variables.oprtions == true && Variables.anticipo != 0)
                                {
                                    IForm formInterface = this.Owner as IForm;
                                    if (formInterface != null)
                                    {
                                        Variables.anticipo = 0;
                                        Variables.oprtions = false;
                                        formInterface.ShowForm("SOAPAP", "cobroagua");
                                    }
                                    this.Close();
                                }
                                else if (Variables.oprtions != true)
                                {
                                    IForm formInterface = this.Owner as IForm;
                                    if (formInterface != null)
                                    {
                                        Variables.anticipo = 0;
                                        Variables.oprtions = false;
                                        formInterface.ShowForm("SOAPAP", "cobroagua");
                                    }
                                    this.Close();
                                }

                                else
                                {
                                    IForm formInterface = this.Owner as IForm;
                                    if (formInterface != null)
                                    {
                                        Variables.anticipo = 0;
                                        Variables.oprtions = false;
                                        formInterface.ShowForm("SOAPAP", "cobro");
                                    }
                                    this.Close();
                                }
                            }
                        }
                    }
            }
            else
            {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "El monto capturado esta en 0", TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
            }
            }
            else
            {
                mensaje = new MessageBoxForm(Variables.titleprincipal, "No existen adeudos para cobrar", TypeIcon.Icon.Cancel);
                mensaje.ShowDialog();
            }
            
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] separadas;
            if (comboBox1.SelectedValue.ToString() == "4" || comboBox1.SelectedValue.ToString() == "2" || comboBox1.SelectedValue.ToString() == "3")
            {
                Form loading = new Loading();
                loading.Show(this);
                dtcombo2 = await q.GETExternalOriginPayments("/api/ExternalOriginPayments");
                if (dtcombo2 != null)
                {
                    foreach (DataRow row in dtcombo2.Rows)
                    {
                        separadas = row[0].ToString().Split('/');
                        if (separadas[0].ToString() == "error")
                        {
                            mensaje = new MessageBoxForm(Variables.titleprincipal, separadas[1].ToString(), TypeIcon.Icon.Cancel);
                            mensaje.ShowDialog();
                            dtcombo2.Rows.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                }


                comboBox2.Enabled = true;
                textBox2.Enabled = true;
                checkbancos = true;
                cargarbancos();
                loading.Close();
            }
            else
            {
                comboBox2.DataSource = null;
                comboBox2.Enabled = false;
                textBox2.Enabled = false;
                checkbancos = false;
            }
        }
        
        public async void cargardatos()
        {
            int kas = 0;
            contador = 0;
            decimal total = 0;
            decimal resultado = 0;
            decimal acumulado = 0;
            decimal iva = 0;
            decimal redondeo = 0;
            decimal totals = 0;
            decimal totalp = 0;
            decimal totalx = 0;
            decimal ivast = 0;
            decimal montosresultantes = 0;
            decimal saldoaplicar = 0;
            decimal acomulador2 = 0;
            decimal total3 = 0;
            decimal divi = 0;
            bool start = false;
            decimal totalw = 0;
            decimal totalws = 0;
            decimal s = 0;
            decimal k = 0;
            decimal subtotalt = 0;
            decimal ivat = 0;
            decimal redondeot = 0;
            decimal totalt = 0;
            decimal r = 0;
            decimal check = 0;
            decimal cantidad = 0;
            string[] separadas;
            bool checks = false;
            cantidad = Convert.ToDecimal(textBox1.Text);
            decimal ivask = 0;
            decimal saber = 0;

            if (cantidad <= totalT)
            {

                if(cantidad != totalT)
                {
                total = Convert.ToDecimal(textBox1.Text);
                totalp = Convert.ToDecimal(textBox1.Text);
                dt.Rows.Clear();
                desiredResult.Rows.Clear();
                for (int x = 0; x < Variables.debst.Count; x++)
                {

                    foreach (var item in Variables.debst[x].debtdetails)
                    {
                        if (item.haveTax == true)
                        {
                            ivast = ivast + (Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100;
                            Variables.sitieneiva = true;
                        }
                    }

                    resultado = Convert.ToDecimal(Variables.debst[x].amount) - Convert.ToDecimal(Variables.debst[x].onAccount) + ivast;
                    acumulado = acumulado + resultado;
                    // redondeo = Math.Ceiling(acumulado) - acumulado;
                    totals = acumulado + redondeo;
                    montosresultantes = montosresultantes + resultado;
                    totalx = total - totals;
                    
                    if (totalx <= 0)
                    {
                        if(start == false)
                        {
                                saldoaplicar = resultado - (resultado-total);
                                ivask = ivask + ((ivast / resultado) * saldoaplicar);
                        }
                        else
                        {
                                saldoaplicar = resultado - (resultado - saldoaplicar);
                                ivask = ivask + ((ivast / resultado) * saldoaplicar);
                        }

                        foreach (var item in Variables.debst[x].debtdetails)
                        {
                            DataRow row = dt.NewRow();
                            row["Id"] = item.id;
                            row["amount"] = item.amount;
                            row["onAccount"] = item.onAccount;
                            row["have_tax"] = item.haveTax;
                            row["code_concept"] = item.codeConcept;
                            row["name_concept"] = item.nameConcept;
                            row["debtId"] = item.debtId.ToString();
                            totalws = resultado;
                            total3 = (Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) / resultado;
                            divi = total3 * saldoaplicar;
                            check = Math.Round(divi, 2);
                             //check = divi;
                             row["deuda"] = Math.Round(divi,2);

                                if ((bool)item.haveTax == true)
                                {

                                    decimal xs = check * Convert.ToDecimal(Variables.Configuration.IVA) / 100;
                                    decimal xt = Math.Round(xs,2);
                                    saber = saber +  Math.Round(check * Convert.ToDecimal(Variables.Configuration.IVA) / 100, 2);
                                    row["TaxResult"] = Math.Round(check* Convert.ToDecimal(Variables.Configuration.IVA) / 100, 2);
                                }
                                else
                                {
                                    row["TaxResult"] = 0;
                                }


                                dt.Rows.Add(row);
                            if (checks==false)
                                {
                                    kas = dt.Rows.Count;
                                    contador = contador + 1;
                                    checks = true;
                                }
                            
                            }

                        totalw = 0;

                        if (start == false)
                        {
                            decimal redondeos = 0;
                            totalp = 0;
                            totalw = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                s = Convert.ToDecimal(row[7]);
                                totalw = totalw + s;
                            }

                                subtotalt = totalw;
                                ivat = saber;
                                totalt = (totalw + ivat);
                                

                            if (totalt != Convert.ToDecimal(textBox1.Text))
                            {
                               
                                if (totalt < Convert.ToDecimal(textBox1.Text))
                                {
                                    decimal resta = 0;
                                    resta = Convert.ToDecimal(textBox1.Text) - totalt;
                                    totalw = 0;
                                    foreach (var item in Variables.debst[x].debtdetails)
                                    {
                                        foreach (DataRow rows in dt.Rows)
                                        {
                                            if ((decimal)rows[7] != (decimal)rows[1])
                                            {
                                                k = (decimal)rows[7] + resta;
                                                if (resta != 0 && (Boolean)rows[3] == false)
                                                {
                                                    rows[7] = k;
                                                    resta = 0;
                                                }
                            
                                            }
                                        }
                                    }

                                    foreach (DataRow row in dt.Rows)
                                    {
                                        s = Convert.ToDecimal(row[7]);
                                        totalw = totalw + s;
                                    }
                                    

                                    subtotalt = totalw;
                                    totalt = (totalw + ivat);
                                    label11.Text = "$" + subtotalt.ToString();
                                    label13.Text = "$" + ivat.ToString();
                                    label14.Text = "$0";
                                    label23.Text = "$" + totalt.ToString();

                                }

                                else if (totalt > Convert.ToDecimal(textBox1.Text))
                                {
                                    decimal resta = 0;
                                    resta = Convert.ToDecimal(textBox1.Text) - totalt;
                                    totalw = 0;
                                    foreach (var item in Variables.debst[x].debtdetails)
                                    {
                                        foreach (DataRow rows in dt.Rows)
                                        {
                                            if ((decimal)rows[7] != (decimal)rows[1])
                                            {
                                                k = (decimal)rows[7] + resta;
                                                if (resta != 0 && (Boolean)rows[3]==false)
                                                {
                                                    rows[7] = k;
                                                    resta = 0;
                                                }
                                              
                                            }
                                        }
                                    }


                                    foreach (DataRow row in dt.Rows)
                                    {
                                        s = Convert.ToDecimal(row[7]);
                                        totalw = totalw + s;
                                    }

                                    
                                    subtotalt = totalw;
                                    totalt = (subtotalt + ivat);
                                    label11.Text = "$" + subtotalt.ToString();
                                    label13.Text = "$" + ivat.ToString();
                                    label14.Text = "$0";
                                    label23.Text = "$" + totalt.ToString();
                                    
                                }
                            }
                            else
                            {
                                    totalw = 0;
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        s = Convert.ToDecimal(row[7]);
                                        totalw = totalw + s;
                                    }

                                    subtotalt = totalw;
                                    totalt = (subtotalt + ivat);
                                    label11.Text = "$" + subtotalt.ToString();
                                    label13.Text = "$" + ivat.ToString();
                                    label14.Text = "$0";
                                    label23.Text = "$" + totalt.ToString();
                            }
                        }
                        else
                        {
                            decimal redondeos = 0;
                            totalp = 0;
                            totalw = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                s = Convert.ToDecimal(row[7]);
                                totalw = totalw + s;
                            }

                            subtotalt = totalw;
                            ivat = saber;
                            totalt = (totalw + ivat);
                            
                            if (totalt != Convert.ToDecimal(textBox1.Text))
                            {
                                if (totalt < Convert.ToDecimal(textBox1.Text))
                                {
                                    decimal resta = 0;
                                    resta = Convert.ToDecimal(textBox1.Text) - totalt;
                                    totalw = 0;
                                    foreach (var item in Variables.debst[x].debtdetails)
                                    {
                                        foreach(DataRow rows in dt.Rows)
                                        {
                                            if((decimal)rows[7] != (decimal)rows[1])
                                            {
                                                k = (decimal)rows[7] + resta;
                                                if (resta != 0 && (Boolean)rows[3] == false)
                                                {
                                                    rows[7] = k;
                                                    resta = 0;
                                                }
                                            }
                                        }
                                    }

                                    foreach (DataRow row in dt.Rows)
                                    {
                                        s = Convert.ToDecimal(row[7]);
                                        totalw = totalw + s;
                                    }
                                    r = 0;

                                    subtotalt = totalw;
                                    totalt = (totalw + ivat);

                                    label11.Text = "$" + subtotalt.ToString();
                                    label13.Text = "$" + ivat.ToString();
                                    label14.Text = "$0";
                                    label23.Text = "$" + totalt.ToString();

                                }

                                else if (totalt > Convert.ToDecimal(textBox1.Text))
                                {
                                    decimal resta = 0;
                                    resta = Convert.ToDecimal(textBox1.Text) - totalt;
                                    totalw = 0;
                                    foreach (var item in Variables.debst[x].debtdetails)
                                    {
                                        foreach (DataRow rows in dt.Rows)
                                        {
                                            if ((decimal)rows[7] != (decimal)rows[1])
                                            {
                                                k = (decimal)rows[7] + resta;
                                                if (resta != 0 && (Boolean)rows[3] == false)
                                                {
                                                    rows[7] = k;
                                                    resta = 0;
                                                }
                                                
                                            }
                                        }
                                    }

                                    foreach (DataRow row in dt.Rows)
                                    {
                                        s = Convert.ToDecimal(row[7]);
                                        totalw = totalw + s;
                                    }
                                    r = 0;

                                    subtotalt = totalw;
                                    totalt = (totalw + ivat);
                                    label11.Text = "$" + subtotalt.ToString();
                                    label13.Text = "$" + ivat.ToString();
                                    label14.Text = "$0";
                                    label23.Text = "$" + totalt.ToString();
                                }
                            }
                            else
                            {
                                    subtotalt = totalw;
                                    totalt = (subtotalt + ivat);
                                    label11.Text = "$" + subtotalt.ToString();
                                    label13.Text = "$" + ivat.ToString();
                                    label14.Text = "$0";
                                    label23.Text = "$" + totalt.ToString();
                                }
                        }

                        cargar();
                        break;

                    }
                    else
                    {
                       
                        acomulador2 = acomulador2 + resultado;
                        saldoaplicar = totalx;
                        start = true;
                        foreach (var item in Variables.debst[x].debtdetails)
                        {
                            DataRow row = dt.NewRow();
                            row["Id"] = item.id;
                            row["amount"] = item.amount;
                            row["onAccount"] = item.onAccount;
                            row["have_tax"] = item.haveTax;
                            row["code_concept"] = item.codeConcept;
                            row["name_concept"] = item.nameConcept;
                            row["debtId"] = item.debtId.ToString();
                            row["deuda"] = (Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount));

                            if ((bool)item.haveTax==true)
                                {

                                saber = saber + Math.Round((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100, 2);
                                row["TaxResult"] = Math.Round((Convert.ToDecimal(item.amount) - Convert.ToDecimal(item.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100,2);

                                }
                                else
                                {
                                    row["TaxResult"] = 0;
                                }

                            dt.Rows.Add(row);
                            kas = dt.Rows.Count;
                            contador = contador + 1;
                                
                        }
                    }                    
                }
            }

            else
                {

                    decimal subtotal = 0;
                    decimal ivatotal = 0;
                    decimal ivattotal = 0;

                    dt = await q.GETDebts( "/api/Debts/" + idp + "");
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
                                break;
                            }
                        }
                    }
                    else
                    {
                        mensaje = new MessageBoxForm(Variables.titleprincipal, "No se encontraron datos.", TypeIcon.Icon.Cancel);
                        mensaje.ShowDialog();
                    }

                    

                    foreach (DataRow row in dt.Rows)
                    {
                        if ((Boolean)row[3] == true)
                        {
                            resultado = resultado + Convert.ToDecimal(row[1]);
                            //if (Convert.ToBoolean(row[3]) == true)
                            //{
                                ivattotal = ivattotal + Math.Round((((Convert.ToDecimal(row[1]) - Convert.ToDecimal(row[2])) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100), 2);
                                row[10] = Math.Round((((Convert.ToDecimal(row[1]) - Convert.ToDecimal(row[2])) * Convert.ToDecimal(Variables.Configuration.IVA)) / 100), 2);
                                Variables.sitieneiva = true;
                           // }
                        }
                        subtotal = subtotal + Convert.ToDecimal(row[7]);
                    }

                    cargar();
                    decimal ren = 0;
                    iva = Convert.ToDecimal(Variables.Configuration.IVA);
                    //ivatotal = Math.Round(((resultado * iva) / 100), 2);
                    ivatotal = ivattotal;
                    //ren = Math.Ceiling(subtotal + ivatotal);
                    // redondeo = Math.Round(ren - (subtotal + ivatotal), 2);
                    totalT = subtotal + ivatotal + redondeo;
                    label11.Text = "$" + subtotal.ToString();
                    label13.Text = "$" + ivatotal.ToString();
                    //label14.Text = "$" + redondeo.ToString();
                    label23.Text = "$" + totalT.ToString();
                    parcial = false;
                }
            }

            else
            {
                mensaje = new MessageBoxForm(Variables.titleprincipal, "La cantidad no debe de ser mayor al total de la operacion", TypeIcon.Icon.Cancel);
                mensaje.ShowDialog();
            }
        }

       

        private void button5_Click_1(object sender, EventArgs e)
        {
            decimal total = 0;
            decimal redondeo = 0;
            n.Clear();

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                bool i = Convert.ToBoolean((row.Cells[8].Value));

                if (i == true)
                {
                    int j = Convert.ToInt32(row.Cells[0].Value);
                    var k = Variables.debst.Find(x => x.id == j);
                    n.Add(k);
                }
            }

            foreach (DataGridViewRow row in dataGridView4.Rows)
            {

                bool i = Convert.ToBoolean((row.Cells[8].Value));

                if (i == true)
                {
                    int j = Convert.ToInt32(row.Cells[0].Value);
                    var k = Variables.debst.Find(x => x.id == j);
                    n.Add(k);
                }
                
            }

            for (int ist = 0; ist < n.Count; ist++)
            {
                foreach (var ns in n[ist].debtdetails)
                {
                   total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                }
            }

            decimal ren = 0;
            // ren = Math.Ceiling(total);
            // redondeo = Math.Round(ren - (total), 2);
            totalT = total + redondeo;
            label11.Text = "$" + total.ToString();
            label13.Text = "$0";
            //label14.Text = "$" + redondeo.ToString();
            label23.Text = "$" + totalT.ToString();
            textBox1.Text = totalT.ToString();
            pl = true;

        }


        public  void impresionhoja()
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
            row["Cuenta"] = cuentap;
            row["Contribuyente"] = namesp;
            row["Rfc"] = rfcp;
            row["Domicilio"] = direccionp;
            row["Caja"] = Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id;
            row["Sucursal"] = Variables.Configuration.Terminal.BranchOffice.Name;
            row["Usuario"] = Variables.LoginModel.FullName;
            row["Periodo"] = label25.Text;
            Variables.datospadron.Rows.Add(row);

            Variables.pagos.Columns.Clear();
            Variables.pagos.Rows.Clear();

            DataColumn columnt;
            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "ID";
            Variables.pagos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Concepto";
            Variables.pagos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Importe";
            Variables.pagos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Year";
            Variables.pagos.Columns.Add(columnt);

            columnt = new DataColumn();
            columnt.DataType = System.Type.GetType("System.String");
            columnt.ColumnName = "Debperiod";
            Variables.pagos.Columns.Add(columnt);

            foreach (DataRow rowr in dt.Rows)
            {
                DataRow rowt = Variables.pagos.NewRow();
                rowt["ID"] = rowr[0].ToString();
                rowt["Concepto"] = rowr[5].ToString();
                rowt["Importe"] = rowr[7].ToString();
                rowt["Year"] = rowr[8].ToString();
                rowt["Debperiod"] = rowr[9].ToString();
                Variables.pagos.Rows.Add(rowt);
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

            var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            var qrCode = qrEncoder.Encode(Variables.foliotransaccion);
            var renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            using (var stream = new FileStream("qrcode.png", FileMode.Create))
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
            MemoryStream ms = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemp = new Bitmap(ms);
            Image image = new Bitmap(imageTemp, new Size(new Point(200, 200)));
            byte[] arrs;
            ImageConverter converters = new ImageConverter();
            arrs = (byte[])converters.ConvertTo(image, typeof(byte[]));

            DataRow rowt1 = Variables.ImagenData.NewRow();
            rowt1["Imagen1"] = arr;
            rowt1["Imagen2"] = arrs;
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

            string k1 = label23.Text.Replace("$", "");
            int totals = Convert.ToInt32(Math.Round(Convert.ToDecimal(k1)));
            string k = totals.ToWords();

            DataRow rowt2 = Variables.Foliotiket.NewRow();
            rowt2["Foliotransaccion"] = Variables.foliotransaccion;
            rowt2["Letra"] =   k.ToUpper() + " PESOS M.N.";
            rowt2["Pago"] = comboBox1.Text;
            rowt2["Subtotal"] = label11.Text.Replace("$", "");
            rowt2["IVA"] = label13.Text.Replace("$", ""); 
            rowt2["Redondeo"] = label14.Text.Replace("$", "");
            rowt2["Total"] = label23.Text.Replace("$", "");
            Variables.Foliotiket.Rows.Add(rowt2);   

            Impresion im = new Impresion();
            im.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            decimal total = 0;
            decimal redondeo = 0;
            n.Clear();

            foreach (DataGridViewRow row in dataGridView7.Rows)
            {
                bool i = Convert.ToBoolean((row.Cells[1].Value));
                if (i == true)
                {
                    int j = Convert.ToInt32(row.Cells[0].Value);
                    var k = Variables.debst.Find(x => x.id == j);
                    n.Add(k);
                }
            }

            foreach (DataGridViewRow row in dataGridView8.Rows)
            {
                bool i = Convert.ToBoolean((row.Cells[1].Value));
                if (i == true)
                {
                    int j = Convert.ToInt32(row.Cells[0].Value);
                    var k = Variables.debst.Find(x => x.id == j);
                    n.Add(k);
                }
            }

            foreach (DataGridViewRow row in dataGridView9.Rows)
            {
                bool i = Convert.ToBoolean((row.Cells[1].Value));
                if (i == true)
                {
                    int j = Convert.ToInt32(row.Cells[0].Value);
                    var k = Variables.debst.Find(x => x.id == j);
                    n.Add(k);
                }
            }


            for (int ist = 0; ist < n.Count; ist++)
            {
                foreach (var ns in n[ist].debtdetails)
                {
                    total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                }
            }

            decimal ren = 0;
            ren = Math.Ceiling(total);
            //         redondeo = Math.Round(ren - (total), 2);
            //totalT = total + redondeo;
            totalT = total;

            label11.Text = "$" + total.ToString();
            label13.Text = "$0";
            //label14.Text = "$" + redondeo.ToString();
            label23.Text = "$" + totalT.ToString();
            textBox1.Text = totalT.ToString();
            pl = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            decimal total = 0;
            decimal redondeo = 0;
            n.Clear();

            foreach (DataGridViewRow row in dataGridView7.Rows)
            {
                bool i = Convert.ToBoolean((row.Cells[1].Value));
                if (i == true)
                {
                    int j = Convert.ToInt32(row.Cells[0].Value);
                    var k = Variables.debst.Find(x => x.id == j);
                    n.Add(k);
                }
            }

            foreach (DataGridViewRow row in dataGridView8.Rows)
            {
                bool i = Convert.ToBoolean((row.Cells[1].Value));
                if (i == true)
                {
                    int j = Convert.ToInt32(row.Cells[0].Value);
                    var k = Variables.debst.Find(x => x.id == j);
                    n.Add(k);
                }
            }

            foreach (DataGridViewRow row in dataGridView9.Rows)
            {
                bool i = Convert.ToBoolean((row.Cells[1].Value));
                if (i == true)
                {
                    int j = Convert.ToInt32(row.Cells[0].Value);
                    var k = Variables.debst.Find(x => x.id == j);
                    n.Add(k);
                }
            }


            for (int ist = 0; ist < n.Count; ist++)
            {
                foreach (var ns in n[ist].debtdetails)
                {
                    total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                }
            }

            decimal ren = 0;
            ren = Math.Ceiling(total);
            //redondeo = Math.Round(ren - (total), 2);
            //totalT = total + redondeo;
            totalT = total ;

            label11.Text = "$" + total.ToString();
            label13.Text = "$0";
            //label14.Text = "$" + redondeo.ToString();
            label23.Text = "$" + totalT.ToString();
            textBox1.Text = totalT.ToString();
            pl = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            decimal total = 0;
            decimal redondeo = 0;
            n.Clear();

            foreach (DataGridViewRow row in dataGridView7.Rows)
            {
                bool i = Convert.ToBoolean((row.Cells[1].Value));
                if (i == true)
                {
                    int j = Convert.ToInt32(row.Cells[0].Value);
                    var k = Variables.debst.Find(x => x.id == j);
                    n.Add(k);
                }
            }

            foreach (DataGridViewRow row in dataGridView8.Rows)
            {
                bool i = Convert.ToBoolean((row.Cells[1].Value));
                if (i == true)
                {
                    int j = Convert.ToInt32(row.Cells[0].Value);
                    var k = Variables.debst.Find(x => x.id == j);
                    n.Add(k);
                }
            }

            foreach (DataGridViewRow row in dataGridView9.Rows)
            {
                bool i = Convert.ToBoolean((row.Cells[1].Value));
                if (i == true)
                {
                    int j = Convert.ToInt32(row.Cells[0].Value);
                    var k = Variables.debst.Find(x => x.id == j);
                    n.Add(k);
                }
            } 


            for (int ist = 0; ist < n.Count; ist++)
            {
                foreach (var ns in n[ist].debtdetails)
                {
                    total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                }
            }

            decimal ren = 0;
            ren = Math.Ceiling(total);
            //redondeo = Math.Round(ren - (total), 2);
            //totalT = total + redondeo;
            totalT = total;

            label11.Text = "$" + total.ToString();
            label13.Text = "$0";
            //label14.Text = "$" + redondeo.ToString();
            label23.Text = "$" + totalT.ToString();
            textBox1.Text = totalT.ToString();
            pl = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (textBox3.Text != "")
                {
                    Variables.anticipo = 0;
                    dataGridView1.DataSource = null;
                    desiredResult.Rows.Clear();
                    dt.Rows.Clear();
                    Variables.cuenta = textBox3.Text;
                    cargars();
                }
                else
                {

                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Ingrese una cuenta", TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                    
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox3.Text!="")
            {
            dataGridView1.DataSource = null;
            desiredResult.Rows.Clear();
            dt.Rows.Clear();
            Variables.cuenta = textBox3.Text;
            Variables.anticipo = 0;
            cargars();
            }
            else
            {

                mensaje = new MessageBoxForm(Variables.titleprincipal, "Ingrese una cuenta", TypeIcon.Icon.Cancel);
                mensaje.ShowDialog();
            }
        }

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal total = 0;
            decimal iva = 0;
            decimal redondeo = 0;
            decimal ren = 0;
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dataGridView7.Rows[dataGridView7.CurrentRow.Index].Cells[1];


            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;
                    n.Clear();
                    foreach (DataGridViewRow row in dataGridView7.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView8.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    ren = Math.Ceiling(total);
                    totalT = total + redondeo + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = iva.ToString();
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;

                    break;
                case "False":
                    ch1.Value = true;
                    n.Clear();
                    foreach (DataGridViewRow row in dataGridView7.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView8.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }

                        }
                    }



                    totalT = total + redondeo + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = iva.ToString();
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;

                    break;
            }



        }

     

        private void dataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal total = 0;
            decimal redondeo = 0;
            decimal ren = 0;
            decimal iva = 0;
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dataGridView8.Rows[dataGridView8.CurrentRow.Index].Cells[1];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;
                    n.Clear();
                    foreach (DataGridViewRow row in dataGridView7.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView8.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    ren = Math.Ceiling(total);
                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" +iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;


                    break;
                    case "False":
                    ch1.Value = true;
                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView7.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView8.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    ren = Math.Ceiling(total);
                    //redondeo = Math.Round(ren - (total), 2);
                    //totalT = total + redondeo;
                    totalT = total + iva;

                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    //label14.Text = "$" + redondeo.ToString();
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;
                    break;
            }
        }

        

        private void dataGridView9_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal total = 0;
            decimal redondeo = 0;
            decimal ren = 0;
            decimal iva = 0;
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dataGridView9.Rows[dataGridView9.CurrentRow.Index].Cells[1];


            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;
                    n.Clear();
                    foreach (DataGridViewRow row in dataGridView7.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView8.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    ren = Math.Ceiling(total);
                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;


                    break;
                case "False":
                    ch1.Value = true;
                    n.Clear();
                    foreach (DataGridViewRow row in dataGridView7.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView8.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }


                    ren = Math.Ceiling(total);
                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$"+ iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;
                    break;
            }
        }

        

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal total = 0;
            decimal iva = 0;
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            
            ch1 = (DataGridViewCheckBoxCell)dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["dataGridViewCheckBoxColumn1"];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;
                    
                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }
                    

                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;

                    break;
                case "False":
                    ch1.Value = true;
                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }


                    
                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$"+iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;
                    break;
            }
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal total = 0;
            decimal redondeo = 0;
            decimal iva = 0;
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dataGridView4.Rows[dataGridView4.CurrentRow.Index].Cells["dataGridViewCheckBoxColumn2"];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;

                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }


                    
                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$"+iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;

                    break;
                case "False":
                    ch1.Value = true;
                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    totalT = total + redondeo + iva;
                    totalT = total;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;
                    break;
            }
        }

       

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (Variables.anticipo != 1 && Variables.anticipo != 2)
                {
                    cargardatos();
                    parcial = true;
                }
            }
            else if (Char.IsDigit(e.KeyChar))
            {

            }

            else
            {
                e.Handled = true;
            }
        }

        private async  void pictureBox2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                dataGridView1.DataSource = null;
                desiredResult.Rows.Clear();
                dt.Rows.Clear();
                Variables.cuenta = textBox3.Text;
                Variables.anticipo = 0;
                Variables.sitieneiva = false;
                await cargars();
            }
            else
            {

                mensaje = new MessageBoxForm(Variables.titleprincipal, "Ingrese una cuenta", TypeIcon.Icon.Cancel);
                mensaje.ShowDialog();
            }
        }

        private async void txtCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                if (textBox3.Text != "")
                {
                    dataGridView1.DataSource = null;
                    desiredResult.Rows.Clear();
                    dt.Rows.Clear();
                    Variables.cuenta = textBox3.Text;
                    Variables.anticipo = 0;
                    Variables.sitieneiva = false;
                    await cargars();
                }
                else
                {

                    mensaje = new MessageBoxForm(Variables.titleprincipal, "Ingrese una cuenta", TypeIcon.Icon.Cancel);
                    mensaje.ShowDialog();
                }


            }
        }

        private void dataGridView7_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal total = 0;
            decimal iva = 0;
            decimal redondeo = 0;
            decimal ren = 0;
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dataGridView7.Rows[dataGridView7.CurrentRow.Index].Cells[1];


            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;
                    n.Clear();
                    foreach (DataGridViewRow row in dataGridView7.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView8.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    ren = Math.Ceiling(total);
                    totalT = total + redondeo + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = iva.ToString();
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;

                    break;
                case "False":
                    ch1.Value = true;
                    n.Clear();
                    foreach (DataGridViewRow row in dataGridView7.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView8.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }

                        }
                    }



                    totalT = total + redondeo + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = iva.ToString();
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;

                    break;
            }

        }

        private void dataGridView8_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal total = 0;
            decimal redondeo = 0;
            decimal ren = 0;
            decimal iva = 0;
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dataGridView8.Rows[dataGridView8.CurrentRow.Index].Cells[1];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;
                    n.Clear();
                    foreach (DataGridViewRow row in dataGridView7.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView8.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {

                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    ren = Math.Ceiling(total);
                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;


                    break;
                case "False":
                    ch1.Value = true;
                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView7.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView8.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    
                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;
                    break;
            }
        }

        private void dataGridView9_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal total = 0;
            decimal redondeo = 0;
            decimal ren = 0;
            decimal iva = 0;
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dataGridView9.Rows[dataGridView9.CurrentRow.Index].Cells[1];


            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;
                    n.Clear();
                    foreach (DataGridViewRow row in dataGridView7.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView8.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;


                    break;
                case "False":
                    ch1.Value = true;
                    n.Clear();
                    foreach (DataGridViewRow row in dataGridView7.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView8.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;
                    break;
            }
        }

        private void dataGridView3_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal total = 0;
            decimal iva = 0;
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();

            ch1 = (DataGridViewCheckBoxCell)dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["dataGridViewCheckBoxColumn1"];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;

                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }


                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;

                    break;
                case "False":
                    ch1.Value = true;
                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }



                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;
                    break;
            }
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal total = 0;
            decimal redondeo = 0;
            decimal iva = 0;
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dataGridView5.Rows[dataGridView5.CurrentRow.Index].Cells["dataGridViewCheckBoxColumn3"];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;

                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$"+iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;

                    break;
                case "False":
                    ch1.Value = true;
                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    totalT = total + redondeo + iva;
                    totalT = total;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$"+iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;
                    break;
            }
        }

        private void dataGridView4_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal total = 0;
            decimal redondeo = 0;
            decimal iva = 0;
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dataGridView4.Rows[dataGridView4.CurrentRow.Index].Cells["dataGridViewCheckBoxColumn2"];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;

                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }



                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;

                    break;
                case "False":
                    ch1.Value = true;
                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    totalT = total + redondeo + iva;
                    totalT = total;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;
                    break;
            }
        }

        private void dataGridView5_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal total = 0;
            decimal redondeo = 0;
            decimal iva = 0;
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dataGridView5.Rows[dataGridView5.CurrentRow.Index].Cells["dataGridViewCheckBoxColumn3"];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "True":
                    ch1.Value = false;

                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }


                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    totalT = total + iva;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;

                    break;
                case "False":
                    ch1.Value = true;
                    n.Clear();

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[8].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView9.Rows)
                    {
                        bool i = Convert.ToBoolean((row.Cells[1].Value));
                        if (i == true)
                        {
                            int j = Convert.ToInt32(row.Cells[0].Value);
                            var k = Variables.debst.Find(x => x.id == j);
                            n.Add(k);
                        }
                    }

                    for (int ist = 0; ist < n.Count; ist++)
                    {
                        foreach (var ns in n[ist].debtdetails)
                        {
                            total = total + (Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount));
                            if (ns.haveTax == true)
                            {
                                iva = iva + Math.Round(((Convert.ToDecimal(ns.amount) - Convert.ToDecimal(ns.onAccount)) * Convert.ToDecimal(Variables.Configuration.IVA) / 100), 2);
                                Variables.sitieneiva = true;
                            }
                        }
                    }

                    totalT = total + redondeo + iva;
                    totalT = total;
                    label11.Text = "$" + total.ToString();
                    label13.Text = "$" + iva;
                    label23.Text = "$" + totalT.ToString();
                    textBox1.Text = totalT.ToString();
                    pl = true;
                    break;
            }
        }
    }
}
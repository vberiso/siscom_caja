using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOAPAP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI.ReportesForms.Finanzas.Ayuntamiento
{
    public partial class FormatoH2 : Form
    {
        private int NumCallGetParams;
        private string formato;
        private List<GroupCataloguesVM> Groups;
        private List<CatalogueVM> Catalogues;
        private int TotalAm, TotalAn, TotalAc;
        private int TotalCU, TotalCR;

        public FormatoH2()
        {
            NumCallGetParams = 0;
            InitializeComponent();

        }
        public void SetFormato(string formato)
        {
            this.formato = formato;
        }

        public void SetGroups(object Group)
        {

            Groups = JsonConvert.DeserializeObject<List<GroupCataloguesVM>>(JsonConvert.SerializeObject(Group));
        }

        public List<object> GetParams()
        {
            List<object> LParams = new List<object>();
            if (NumCallGetParams == 2)
            {
                NumCallGetParams = 1;
            }
            else
            {
                NumCallGetParams++;
            }
            LParams.Add(new { Key = "isAccounts", Value = NumCallGetParams, DbType = DbType.Int32 });



            return LParams;
        }

        public void setVAriables(object data)
        {
            List<SOAPAP.Reportes.Finanzas.Ayuntamiento.Cuentas> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Ayuntamiento.Cuentas>>(data.ToString());
            TotalCU = OData.Where(x => x.type == 1).First().total;
            TotalCR = OData.Where(x => x.type == 2).First().total;
        }
        public void DrawData(object data, string mes, string year)
        {

            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);
            setVAriables(data);
            lblUrbanas.Text = TotalCU.ToString();
            lblRusticas.Text = TotalCR.ToString();
            lblTotalCuentas.Text = (TotalCU + TotalCR).ToString();







        }
        private void SetDataVariablesA(object data, bool IsCurrentMonth, string mes, string year)
        {
            TotalAc = 0;
            TotalAn = 0;
            TotalAm = 0;
            
            List<SOAPAP.Reportes.Finanzas.Ayuntamiento.AccountsPay> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Ayuntamiento.AccountsPay>>(data.ToString());
            
            DateTime dateEnd;
            var dateStart = Convert.ToDateTime("01-" + mes + "-" + year);
            if (mes == "12") {
                dateEnd = Convert.ToDateTime("01-01-" + (int.Parse(year)+1).ToString());
            }
            else
            {
                dateEnd = Convert.ToDateTime("01-" + (int.Parse(mes) + 1) + "-" + year);
            }
            List<SOAPAP.Reportes.Finanzas.Ayuntamiento.AccountsPay> Periods;
            Periods = OData.Where(x => x.payment_date >= dateStart && x.payment_date <= dateEnd).ToList();
            if (!IsCurrentMonth)
            {

                Periods = OData;

            }

            Periods = Periods.Where(x => x.from_date <= int.Parse(year)).ToList();
            var Distict = Periods.Select(x => x.id_agreement).Distinct().ToList();
            Distict.ForEach(x =>
            {
                var Actual = Periods.Where(c => c.id_agreement == x && c.from_date == int.Parse(year)).FirstOrDefault();
                var Anteriores = Periods.Where(c => c.id_agreement == x && c.from_date < int.Parse(year) ).FirstOrDefault();
                if (Actual != null && Anteriores == null)
                {
                    TotalAc = TotalAc + 1;

                }
                else if (Actual == null && Anteriores != null)
                {
                    TotalAn = TotalAn + 1;
                }
                else 
                {
                    TotalAm = TotalAm + 1;
                }
             

            });

        }

        public void DrawDataA(object data, string mes, string year)
        {
           var JdDta = JObject.Parse(data.ToString());
           
            data = JsonConvert.SerializeObject(JdDta["data"]);
            var Odata = JsonConvert.DeserializeObject<List<object>>(data.ToString());
            SetDataVariablesA(Odata.First(), true, mes, year);
           
            label11.Text = "NO. DE CUENTAS QUE REALIZARON ÚNICAMENTE PAGOS DE " + year;
            label14.Text = "NO. DE CUENTAS QUE REALIZARON ÚNICAMENTE PAGOS DE EJERCICIOS ANTERIORES " + year;
            label16.Text = "NO.DE CUENTAS QUE REALIZARON TANTO PAGOS DE " + year + " Y ANTERIORES A " + year;
            //var dateEnd = Convert.ToDateTime("01-" + (int.Parse(mes) + 1) + "-" + year).AddDays(-1);
            //var currentPeriod = OData.Where(x => x.payment_date >= dateStart && x.payment_date <= dateEnd).ToList();
            //lblUActualMes.Text = currentPeriod.Where (x => x.from_date == int.Parse(year) && x.until_date == int.Parse(year)).Select(x => x.id_agreement).Distinct().ToList().Count().ToString();
            //lblAnterioresMes.Text = currentPeriod.Where(x => x.from_date < int.Parse(year) && x.until_date < int.Parse(year)).Select(x => x.id_agreement).Distinct().ToList().Count().ToString();
            lblUActualMes.Text = TotalAc.ToString();
            lblAnterioresMes.Text = TotalAn.ToString();
            lblAmbosMes.Text = TotalAm.ToString();
            lblTotalMes.Text = (TotalAc+ TotalAn+TotalAm).ToString();

            //var Distict = currentPeriod.Select(x => x.id_agreement).Distinct().ToList();
            //Distict.ForEach(x =>
            //{
            //    var result = currentPeriod.Where(c => c.id_agreement == x && c.from_date == int.Parse(year) && c.until_date == int.Parse(year)).FirstOrDefault();
            //    var result2 = currentPeriod.Where(c => c.id_agreement == x && c.from_date < int.Parse(year) && c.until_date < int.Parse(year)).FirstOrDefault();
            //    if(result != null && result2 != null)
            //    {
            //        total = total + 1;
            //    }

            //});

            // ambos = ambos.Where(x => x.from_date == int.Parse(year) && x.until_date == int.Parse(year)).ToList();
            //lblAmbosMes.Text = total.ToString();
            //lblTotalMes.Text = (int.Parse(lblAmbosMes.Text) + int.Parse(lblAnterioresMes.Text) + int.Parse(lblUActualMes.Text)).ToString();

            //Desde el primero de enero
            //SetDataVariablesA(data, false, mes, year);
            label21.Text = "NO. DE CUENTAS QUE REALIZARON ÚNICAMENTE PAGOS DE " + year;
            label23.Text = "NO. DE CUENTAS QUE REALIZARON ÚNICAMENTE PAGOS DE EJERCICIOS ANTERIORES " + year;
            label25.Text = "NO. DE CUENTAS QUE REALIZARON TANTO PAGOS DE " + year + " Y ANTERIORES A " + year;



            var EneroActual = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(Odata.Last()));
            TotalAc = bool.Parse(Odata.ElementAt(1).ToString()) == true ? int.Parse(EneroActual.First().ToString()) + TotalAc : int.Parse(EneroActual.First().ToString());
            TotalAn = bool.Parse(Odata.ElementAt(1).ToString()) == true ? int.Parse(EneroActual.ElementAt(1).ToString()) + TotalAn : int.Parse(EneroActual.ElementAt(1).ToString());
            TotalAm = bool.Parse(Odata.ElementAt(1).ToString()) == true ? int.Parse(EneroActual.ElementAt(2).ToString()) + TotalAm : int.Parse(EneroActual.ElementAt(2).ToString());


            lblUActualEnero.Text = TotalAc.ToString();
            lblAnterioresEnero.Text = TotalAn.ToString();
            lblAmbosEnero.Text = TotalAm.ToString();
            lblTotalEnero.Text = (int.Parse(lblUActualEnero.Text) + int.Parse(lblAnterioresEnero.Text) + int.Parse(lblAmbosEnero.Text)).ToString();


      
            //dateStart = Convert.ToDateTime("01-01-" + year);
            //lblUActualEnero.Text =  OData.Where(x => x.from_date == int.Parse(year) && x.until_date == int.Parse(year)).Select(x => x.id_agreement).Distinct().ToList().Count().ToString();
            //lblAnterioresEnero.Text = OData.Where(x => x.from_date < int.Parse(year) && x.until_date < int.Parse(year)).Select(x => x.id_agreement).Distinct().ToList().Count().ToString();
            // total = 0;
            // Distict = OData.Select(x => x.id_agreement).Distinct().ToList();
            //Distict.ForEach(x =>
            //{
            //    var result = OData.Where(c => c.id_agreement == x && c.from_date == int.Parse(year) && c.until_date == int.Parse(year)).FirstOrDefault();
            //    var result2 = OData.Where(c => c.id_agreement == x && c.from_date < int.Parse(year) && c.until_date < int.Parse(year)).FirstOrDefault();
            //    if (result != null && result2 != null)
            //    {
            //        total = total + 1;
            //    }

            //});
            //lblAmbosEnero.Text = total.ToString();
            //lblTotalEnero.Text = (int.Parse(lblAmbosEnero.Text) + int.Parse(lblAnterioresEnero.Text) + int.Parse(lblUActualEnero.Text)).ToString();

            //Desde el primero de enero





        }
        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Label19_Click(object sender, EventArgs e)
        {

        }
        public string GetHtml1(object data, string year, string mes)
        {
            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);
            setVAriables(data);
            StringBuilder builder = new StringBuilder();
            builder.Append(@" <style>

            p.borde1{
                border-top: black 1px solid;
                border-left: black 1px solid;
                border-right: black 1px solid;
                margin-bottom: 0px; 
                display: inline-block; 
                width: 100%; 
                padding: 5px; 
                margin-top: 2px;
            }

            p.borde2{
                margin-bottom: 2px; 
                display: inline-block; 
                width: 100%; 
                border: black 1px solid;
                padding: 5px; 
                margin-top: 0px
            }

            div.encabezado{
                text-align: center; 
                display: inline-block; 
                width: 100%; 
                border: black 1px solid;
                padding: 5px; 
            }

            table#datos, table#datos tr, table#datos td {
                padding: 5px;
                text-align: right;
                border: 1px solid black;
                border-collapse: collapse;
            }

            table#datos th{
                padding: 5px;
                text-align: center;
                border: 1px solid black;
                border-collapse: collapse;
            }

            td.centro {
                text-align: center !important;
            }

            td.left {
                text-align: left !important;
            }

            td.right {
                text-align: right !important;
            }

            td.snfrmt1{
                border-style: none !important;
                border-collapse:collapse !important; 
                border-top-style: hidden !important;
                border-left-style: hidden !important;
            }

            td.snfrmt2{
                border-style: none !important;
                border-collapse:collapse !important; 
                border-top-style: hidden !important;
                border-right-style: hidden !important;
            }

            td.cuadros{
                width: 25%; 
                padding: 20px !important; 
                background: darkgray;
                text-align: center !important;
            }

            
        </style>");
            builder.Append(@"<br><br><div class='cuadro_f2_A'>");
            builder.Append(@"<div style='text-align: center; display: inline-block; width: 100%; '>");
            builder.Append(@"<p style='margin-bottom: 0px'>CUADRO F2-A</p>");

            builder.Append(@"<p style='margin-top: 5px'>NO. DE CUENTAS EXISTENTES EN EL PADRÓN DE IMPUESTOS PREDIAL AL ÚLTIMO DÍA DEL MES</p>");
            builder.Append(@"</div>");
            builder.Append(@"<div class='datos_conceptos' style='margin-bottom: 30px; text-align: center; '>");
            builder.Append(@"<table id='datos' style='width: 50%; margin-bottom: 50px; margin: auto;'>");
            builder.Append(@"<tr>");
            builder.Append(@"<th style='background: darkgray; '>TIPO DE PREDIO</th>");
            builder.Append(@"<th style='background: darkgray; '>N° DE CUENTAS</th>");
            builder.Append(@"<tr>");
            builder.Append(@"<td class='left' style='width: 50%; '>URBANO</td>");
            builder.Append(@"<td class='right' style='width: 50%; '>"+ TotalCU.ToString() + "</td>");
            builder.Append(@"</tr>");
            builder.Append(@"<tr>");
            builder.Append(@"<td class='left'>RUSTICO</td>");
            builder.Append(@"<td class='right'>"+ TotalCR.ToString()+ "</td>");
            builder.Append(@"</tr>");
            builder.Append(@"<tr>");
            builder.Append(@"<td style='background: darkgray; ' class='left'>TOTAL</td>");
            builder.Append(@"<td class='right'>"+(TotalCU+TotalCR).ToString() +"</td>");
            builder.Append(@"</tr>");
            builder.Append(@"</table>");
            builder.Append(@"</div>");
            builder.Append(@"</div>");
        
           




            return builder.ToString();
        }

        public string GetHtml2(object data, string year, string mes)
        {
            var JdDta = JObject.Parse(data.ToString());

            data = JsonConvert.SerializeObject(JdDta["data"]);
            SetDataVariablesA(data, true, mes, year);

            
            StringBuilder builder = new StringBuilder();

            builder.Append(@"<br><br><div class='cuadro_f2_B'>");
            builder.Append(@"<div style='text-align: center; display: inline - block; width: 100 %; '>");
            builder.Append(@"<p style='margin-bottom: 0px'>CUADRO F2-B</p>");
            builder.Append(@"<p style='margin-top: 5px'>NO. DE CUENTAS QUE REALIZARON PAGO EN EL MES POR CONCEPTO DE IMPUESTO PREDIAL</p>");
            builder.Append(@"</div>");
            builder.Append(@"<div class='datos_conceptos' style='margin-bottom: 30px; text-align: center; '>");
            builder.Append(@"<table id='datos' style='width: 100%; margin-bottom: 50px; margin: auto; '>");
            builder.Append(@"<tr>");
            builder.Append(@"<td class='cuadros' style='background: darkgray;'>N° DE CUENTAS QUE REALIZARON ÚNICAMENTE 2019 PAGOS DE<br>(A)</td>");
            builder.Append(@"<td class='cuadros' style='background: darkgray;'>N° DE CUENTAS QUE REALIZARON ÚNICAMENTE PAGOS DE EJERCICIOS ANTERIORES A 2019 <br>(B)</td>");
            builder.Append(@"<td class='cuadros' style='background: darkgray;'>N° DE CUENTAS QUE REALIZARON TANTO PAGOS DE 2019 COMO DE EJERCICIOS ANTERIORES A 2019 <br>(C)</td>");
            builder.Append(@"<td class='cuadros' style='background: darkgray;'>N° DE CUENTAS QUE REALIZARON PAGOS EN EL MES <br>(D=A+B+C)</td>");
            builder.Append(@"<tr>");
            builder.Append(@"<td class='right'>"+TotalAc+"</td>");
            builder.Append(@"<td class='right'>"+TotalAn+"</td>");
            builder.Append(@"<td class='right'>"+TotalAm+"</td>");
            builder.Append(@"<td class='right'>"+(TotalAc + TotalAn + TotalAm) + "</td>");
           
            builder.Append(@"</tr>");
            builder.Append(@"</table>");
            builder.Append(@" </div>");
            builder.Append(@"</div>");
            SetDataVariablesA(data, false, mes, year);


            builder.Append(@"<br><br><div class='cuadro_f2_B'>");
            builder.Append(@"<div style='text-align: center; display: inline - block; width: 100 %; '>");
            builder.Append(@"<p style='margin-bottom: 0px'>CUADRO F2-B</p>");
            builder.Append(@"<p style='margin-top: 0px; margin-bottom: 0px'>NO. DE CUENTAS QUE REALIZARON PAGOS DEL 1 DE ENERO AL ÚLTIMO DÍA DEL MES POR CONCEPTO</p>");
            builder.Append(@"<p style='margin-top: 0px'>DE IMPUESTO PREDIAL</p>");
            builder.Append(@"</div>");
            builder.Append(@"<div class='datos_conceptos' style='margin-bottom: 30px; text-align: center; '>");
            builder.Append(@"<table id='datos' style='width: 100%; margin-bottom: 50px; margin: auto; '>");
            builder.Append(@"<tr>");
            builder.Append(@"<td class='cuadros' style='background: darkgray;'>N° DE CUENTAS QUE REALIZARON ÚNICAMENTE 2019 PAGOS DE<br>(A)</td>");
            builder.Append(@"<td class='cuadros' style='background: darkgray;'>N° DE CUENTAS QUE REALIZARON ÚNICAMENTE PAGOS DE EJERCICIOS ANTERIORES A 2019 <br>(B)</td>");
            builder.Append(@"<td class='cuadros' style='background: darkgray;'>N° DE CUENTAS QUE REALIZARON TANTO PAGOS DE 2019 COMO DE EJERCICIOS ANTERIORES A 2019 <br>(C)</td>");
            builder.Append(@"<td class='cuadros' style='background: darkgray;'>N° DE CUENTAS QUE REALIZARON PAGOS EN EL MES <br>(D=A+B+C)</td>");
            builder.Append(@"<tr>");
            builder.Append(@"<td class='right'>" + TotalAc + "</td>");
            builder.Append(@"<td class='right'>" + TotalAn + "</td>");
            builder.Append(@"<td class='right'>" + TotalAm + "</td>");
            builder.Append(@"<td class='right'>" + (TotalAc + TotalAn + TotalAm) + "</td>");

            builder.Append(@"</tr>");
            builder.Append(@"</table>");
            builder.Append(@" </div>");
            builder.Append(@"</div>");



            return builder.ToString();
        }


    }
}

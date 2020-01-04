using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOAPAP.Model;
using System.Collections.Generic;

using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOAPAP.UI.ReportesForms.Finanzas.Ayuntamiento
{
    public partial class Formato1 : Form
    {

        private decimal TImpuesto = 0, TRecargos = 0, TMultas = 0,TGastos = 0, TIntereses= 0, TIndemnizaciones= 0, GranTotal = 0;
   
        private int NumCallGetParams;
        private string formato;
        private List<GroupCataloguesVM> Groups;
        private List<CatalogueVM> Catalogues;

        public Formato1()
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
            LParams.Add(new { Key = "IsCurrentYear", Value = NumCallGetParams, DbType = DbType.Int32 });



            return LParams;
        }
        private void setDatatovariables(List<SOAPAP.Reportes.Finanzas.Formato1> OData, int year)
        {
           
            Catalogues = Groups.Where(g => g.Id == 3).First().Catalogues;
            TImpuesto = OData.Where(x =>x.datePayment == year &&  Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);
       
            Catalogues = Groups.Where(g => g.Id == 5).First().Catalogues;
            TRecargos = OData.Where(x => x.datePayment == year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);

            Catalogues = Groups.Where(g => g.Id == 7).First().Catalogues;
            TMultas = OData.Where(x => x.datePayment == year &&  Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);

            Catalogues = Groups.Where(g => g.Id == 8).First().Catalogues;
            TGastos = OData.Where(x => x.datePayment == year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);

            Catalogues = Groups.Where(g => g.Id == 9).First().Catalogues;
            TIntereses = OData.Where(x => x.datePayment == year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);

            Catalogues = Groups.Where(g => g.Id == 10).First().Catalogues;
            TIndemnizaciones = OData.Where(x => x.datePayment == year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);

         
        }
        public void DrawData(object data, string mes, string year)
        {

            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);
            List<SOAPAP.Reportes.Finanzas.Formato1> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato1>>(data.ToString());
            setDatatovariables(OData, int.Parse(year));


            GranTotal = TImpuesto + TRecargos + TMultas + TGastos + TIntereses + TIndemnizaciones;
            textImpuesto.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TImpuesto);
            textRecargos.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TRecargos);
            textMultas.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TMultas);
            textGastos.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TGastos);
            textIntereses.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TIntereses);
            textIndemnizaciones.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TIndemnizaciones);
           
          
            textBoxTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", GranTotal);
           

         


        }
        private void SetDataVariablesA(List<SOAPAP.Reportes.Finanzas.Formato1> OData, int year)
        {


            Catalogues = Groups.Where(g => g.Id == 3).First().Catalogues;
            TImpuesto = OData.Where(x => x.datePayment < year &&  Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) ;

            Catalogues = Groups.Where(g => g.Id == 5).First().Catalogues;
            TRecargos = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) ;

            Catalogues = Groups.Where(g => g.Id == 7).First().Catalogues;
            TMultas = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) ;

            Catalogues = Groups.Where(g => g.Id == 8).First().Catalogues;
            TGastos = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);

            Catalogues = Groups.Where(g => g.Id == 9).First().Catalogues;
            TIntereses = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) ;

            Catalogues = Groups.Where(g => g.Id == 10).First().Catalogues;
            TIndemnizaciones = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) ;


        }

        private void SetDataVariablesAd(List<SOAPAP.Reportes.Finanzas.Formato1> OData, int year)
        {


            Catalogues = Groups.Where(g => g.Id == 3).First().Catalogues;
            TImpuesto = OData.Where(x => x.datePayment > year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) ;

            Catalogues = Groups.Where(g => g.Id == 5).First().Catalogues;
            TRecargos = OData.Where(x => x.datePayment > year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) ;

            Catalogues = Groups.Where(g => g.Id == 7).First().Catalogues;
            TMultas = OData.Where(x => x.datePayment > year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) ;

            Catalogues = Groups.Where(g => g.Id == 8).First().Catalogues;
            TGastos = OData.Where(x => x.datePayment > year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) ;

            Catalogues = Groups.Where(g => g.Id == 9).First().Catalogues;
            TIntereses = OData.Where(x => x.datePayment > year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) ;

            Catalogues = Groups.Where(g => g.Id == 10).First().Catalogues;
            TIndemnizaciones = OData.Where(x => x.datePayment > year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) ;


        }
        public void DrawDataA(object data, string mes, string year)
        {
            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);

            List<SOAPAP.Reportes.Finanzas.Formato1> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato1>>(data.ToString());
            SetDataVariablesA(OData, int.Parse(year));
             GranTotal += TImpuesto + TRecargos + TMultas + TGastos + TIntereses + TIndemnizaciones;
            textImpuestoA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TImpuesto);
            textRecargosA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TRecargos);
            textMultasA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TMultas);
            textGastosA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TGastos);
            textInteresesA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TIntereses);
            textIndemnizacionesA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TIndemnizaciones);
           
          
            textBoxTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", GranTotal);

            SetDataVariablesAd(OData, int.Parse(year));
            GranTotal += TImpuesto + TRecargos + TMultas + TGastos + TIntereses + TIndemnizaciones;
            txtImpuestoAde.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TImpuesto);
            txtRecargoAde.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TRecargos);
            txtMultaAde.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TMultas);
            txtgastoAde.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TGastos);
            txtInteresesAde.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TIntereses);
            txtIndemnizacionesAde.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TIndemnizaciones);


            textBoxTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", GranTotal);

            //textToTaoGeneral.Text = (decimal.Parse(textToTaoGeneral.Text) + OData.Sum(x => x.importe).ToString()).ToString();


            lblEjercicio.Text = $"EJERCICIO {year}";

            lblAnteriores.Text = $"EJERCICIOS ANTERIORES A {year}";
            lblAdelantado.Text = $"EJERCICIOS ADELANTADOS A {year}";


        }

        public string GetHtml1(object data, string year, string mes)
        {
            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);
            List<SOAPAP.Reportes.Finanzas.Formato1> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato1>>(data.ToString());
            setDatatovariables(OData, int.Parse(year));
            GranTotal = TImpuesto + TRecargos + TMultas + TGastos + TIntereses + TIndemnizaciones;
            StringBuilder builder = new StringBuilder();

            builder.Append(@"<div id='datosTop' style='margin-top:50px'>");
            builder.Append(@"<br>");
            builder.Append(@"<table id='datos' style='width: 99%; background:#f1eeec'><tbody>");
            builder.Append(@"<tr  style='height: 70px'><td style='width: 50%' class='centro'>CONCEPTO</td>
                             <td style='width: 50%' class='centro'>IMPORTE (PESOS)</td>
                             </tr>
                            </tbody>
                            </table>");
            if (formato != "formato1")
            {
                builder.Append("<h3 style='text-align: center'>IMPUESTO PREDIALDESCUENTOS, SUSIDIOS, SUBVENCIONES  BONIFICACIONES");
            }
            builder.Append(@"<table id='datos' style='width: 99%' class='actuales'>");
            builder.Append($@"<caption style='background:#f1eeec'>EJERCICIOS {year}</caption>");
            builder.Append(@"<tbody>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>IMPUESTOCOBRADO</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TImpuesto) }</td>
                            </tr>");
      

        
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>RECARGOS</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TRecargos) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>MULTAS</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TMultas) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>GASTOS DE EJECUCIÓN</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TGastos) }</td>
                            </tr>");
            

            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>INTERESES (NO BANCARIOS)</td>
                             <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TIntereses) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>INDEMNIZACIONES</td>
                             <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TIndemnizaciones) }</td>
                            </tr>");
            builder.Append($@"</tbody>
                            </table>");





            return builder.ToString();
        }

        public string GetHtml2(object data, string year, string mes)
        {
            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);

            List<SOAPAP.Reportes.Finanzas.Formato1> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato1>>(data.ToString());
            SetDataVariablesA(OData, int.Parse(year));

            GranTotal += TImpuesto + TRecargos + TMultas + TGastos + TIntereses + TIndemnizaciones;
            StringBuilder builder = new StringBuilder();

            builder.Append(@"<table id='datos' style='width: 99%' class='anteriores'>");
            builder.Append($@"<caption style='background:#f1eeec'>EJERCICIOS ANTERIORES A {year}</caption>");
            builder.Append(@"<tbody>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>IMPUESTOCOBRADO</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TImpuesto) }</td>
                            </tr>");



            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>RECARGOS</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TRecargos) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>MULTAS</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TMultas) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>GASTOS DE EJECUCIÓN</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TGastos) }</td>
                            </tr>");


            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>INTERESES (NO BANCARIOS)</td>
                             <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TIntereses) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>INDEMNIZACIONES</td>
                             <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TIndemnizaciones) }</td>
                            </tr>");
            //builder.Append($@"<tr>
            //                <td style='width: 50%; background:#f1eeec;' class='centro'>TOTAL</td>
            //                <td style='width: 50%'>{string.Format(new CultureInfo("es-MX"), "{0:C2}", GranTotal) }</td>
            //                </tr>");
            builder.Append($@"</tbody>
                            </table>");


            SetDataVariablesAd(OData, int.Parse(year));

            GranTotal += TImpuesto + TRecargos + TMultas + TGastos + TIntereses + TIndemnizaciones;

            builder.Append(@"<table id='datos' style='width: 99%' class='anteriores'>");
            builder.Append($@"<caption style='background:#f1eeec'>EJERCICIOS ADELANTADOS A {year}</caption>");
            builder.Append(@"<tbody>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>IMPUESTOCOBRADO</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TImpuesto) }</td>
                            </tr>");



            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>RECARGOS</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TRecargos) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>MULTAS</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TMultas) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>GASTOS DE EJECUCIÓN</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TGastos) }</td>
                            </tr>");


            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>INTERESES (NO BANCARIOS)</td>
                             <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TIntereses) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>INDEMNIZACIONES</td>
                             <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TIndemnizaciones) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%; background:#f1eeec;' class='centro'>TOTAL</td>
                            <td style='width: 50%'>{string.Format(new CultureInfo("es-MX"), "{0:C2}", GranTotal) }</td>
                            </tr>");
            builder.Append($@"</tbody>
                            </table>");

            builder.Append("</div>");
            builder.Append("</div>");

           

            return builder.ToString();
        }





    }
}

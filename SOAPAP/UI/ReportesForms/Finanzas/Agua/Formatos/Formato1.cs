using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOAPAP.Model;
using System.Collections.Generic;

using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOAPAP.UI.ReportesForms.Finanzas.Agua.Formatos
{
    public partial class Formato1 : Form
    {

        private decimal TEjeciciones, Tagua, TDrenaje, TRecargos, TSaneamiento, TSaneamientA, GranTotal = 0, GranTotalSaneamiento = 0;
        private decimal TReconexion, TConexion, TMulta, TAlcantarillado;
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
            Catalogues = Groups.Where(g => g.Id == 1).First().Catalogues;
            Tagua = OData.Where(x => x.datePayment >= year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);
            Catalogues = Groups.Where(g => g.Id == 2).First().Catalogues;
            TDrenaje = OData.Where(x => x.datePayment >= year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);
            Catalogues = Groups.Where(g => g.Id == 7).First().Catalogues;
            TRecargos = OData.Where(x => x.datePayment >= year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);
            Catalogues = Groups.Where(g => g.Id == 3).First().Catalogues;
            TSaneamiento = OData.Where(x => x.datePayment >= year &&  Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);
            Catalogues = Groups.Where(g => g.Id == 5).First().Catalogues;
            TReconexion = OData.Where(x => x.datePayment >= year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);
            Catalogues = Groups.Where(g => g.Id == 4).First().Catalogues;
            TConexion = OData.Where(x => x.datePayment >= year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);

            Catalogues = Groups.Where(g => g.Id == 9).First().Catalogues;
            TEjeciciones = OData.Where(x => x.datePayment >= year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);

            Catalogues = Groups.Where(g => g.Id == 8).First().Catalogues;
            TMulta = OData.Where(x => Catalogues.Any(c => x.datePayment >= year && c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);

            Catalogues = Groups.Where(g => g.Id == 6).First().Catalogues;
            TAlcantarillado = OData.Where(x => x.datePayment >= year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento);

        }
        public void DrawData(object data, string mes, string year)
        {

            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);
            List<SOAPAP.Reportes.Finanzas.Formato1> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato1>>(data.ToString());
            setDatatovariables(OData, int.Parse(year));


            GranTotal = Tagua + TDrenaje + TRecargos + TReconexion + TConexion + TEjeciciones + TMulta + TAlcantarillado;
            GranTotalSaneamiento = TSaneamiento;


            textServicios.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", Tagua);
            textDrenaje.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TDrenaje);
            textSaneamientoo.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TSaneamiento);
            textRecargos.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TRecargos);
            textReconexiones.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TReconexion);
            textConexiones.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TConexion);
            textGastos.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TEjeciciones);
            textMultas.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TMulta);
            textAlcantarillado.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TAlcantarillado);

            textBoxTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", GranTotal);
            textToTaoGeneral.Text = OData.Sum(x => x.importe).ToString();

         


        }
        private void SetDataVariablesA(List<SOAPAP.Reportes.Finanzas.Formato1> OData, int year)
        {

            Catalogues = Groups.Where(g => g.Id == 1).First().Catalogues;
            Tagua = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) /*- Tagua*/;
            Catalogues = Groups.Where(g => g.Id == 2).First().Catalogues;
            TDrenaje = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) /*- TDrenaje*/;
            Catalogues = Groups.Where(g => g.Id == 7).First().Catalogues;
            TRecargos = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) /*- TRecargos*/;
            Catalogues = Groups.Where(g => g.Id == 3).First().Catalogues;
            TSaneamientA = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) /*- TSaneamiento*/;
            Catalogues = Groups.Where(g => g.Id == 5).First().Catalogues;
            TReconexion = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) /*- TReconexion*/;
            Catalogues = Groups.Where(g => g.Id == 4).First().Catalogues;
            TConexion = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento)/* - TConexion*/;

            Catalogues = Groups.Where(g => g.Id == 9).First().Catalogues;
            TEjeciciones = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) /*- TEjeciciones*/;

            Catalogues = Groups.Where(g => g.Id == 8).First().Catalogues;
            TMulta = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento) /*- TMulta*/;

            Catalogues = Groups.Where(g => g.Id == 6).First().Catalogues;
            TAlcantarillado = OData.Where(x => x.datePayment < year && Catalogues.Any(c => c.Value.Equals(x.code_concept.ToString().Trim()))).ToList().Sum(x => formato == "formato1" ? x.importe : x.descuento)/* - TAlcantarillado*/;


        }
        public void DrawDataA(object data, string mes, string year)
        {
            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);

            List<SOAPAP.Reportes.Finanzas.Formato1> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato1>>(data.ToString());
            SetDataVariablesA(OData, int.Parse(year));
            GranTotal += Tagua + TDrenaje + TRecargos + TReconexion + TConexion + TEjeciciones + TAlcantarillado + TMulta;
            GranTotalSaneamiento += TSaneamientA;

            textServicioA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", Tagua);
            textDrenajeA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TDrenaje);
            textSaneamientoA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TSaneamientA);
            textRecargosA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TRecargos);
            textSaneamiento.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", GranTotalSaneamiento);
            textReconexionesA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TReconexion);
            textConexionesA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TConexion);
            textGastosA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TEjeciciones);
            textMultasA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TMulta);
            textAlcantarilladoA.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", TAlcantarillado);
            textBoxTotal.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", GranTotal);

            //textToTaoGeneral.Text = (decimal.Parse(textToTaoGeneral.Text) + OData.Sum(x => x.importe).ToString()).ToString();

            textToTaoGeneral.Text = string.Format(new CultureInfo("es-MX"), "{0:C2}", OData.Sum(x => x.importe));
            lblEjercicio.Text = $"EJERCICIO {year}";
            textSEjercicio.Text = $"EJERCICIO {year} (A)";
            lblAnteriores.Text = $"EJERCICIOS ANTERIORES A {year}";
            textSEjercicioA.Text = $"EJERCICIOS ANTERIORES A {year} (B)";


        }

        public string GetHtml1(object data, string year, string mes)
        {
            var JdDta = JObject.Parse(data.ToString());
            data = JsonConvert.SerializeObject(JdDta["data"]);
            List<SOAPAP.Reportes.Finanzas.Formato1> OData = JsonConvert.DeserializeObject<List<SOAPAP.Reportes.Finanzas.Formato1>>(data.ToString());
            setDatatovariables(OData, int.Parse(year));
            GranTotal = Tagua + TDrenaje + TRecargos + TReconexion + TConexion + TEjeciciones + TAlcantarillado + TMulta;
            GranTotalSaneamiento = TSaneamiento;
            StringBuilder builder = new StringBuilder();
            
            builder.Append(@"<div id='datosTop'>");
            builder.Append(@"<table id='datos' style='width: 99%; background:#f1eeec'><tbody>");
            builder.Append(@"<tr  style='height: 70px'><td style='width: 50%' class='centro'>CONCEPTO</td>
                             <td style='width: 50%' class='centro'>IMPORTE (PESOS)</td>
                             </tr>
                            </tbody>
                            </table>");
            builder.Append("<h3 style='text-align: center'>DERECHOS POR SIMINISTRO DE AGUA <br> INGRESOS</h3>");
            builder.Append(@"<table id='datos' style='width: 99%' class='actuales'>");
            builder.Append($@"<caption style='background:#f1eeec'>EJERCICIOS {year}</caption>");
            builder.Append(@"<tbody>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>SERVICIO DE AGUA</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", Tagua) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>CONEXIONES</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TConexion) }</td>
                            </tr>");

            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>RECONEXIONES</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TReconexion) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>ALCANTARILLADO</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TAlcantarillado) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>DRENAJE</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TDrenaje) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>RECARGOS</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TRecargos) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>MULTAS</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TMulta) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>GASTOS DE EJECUCIÓN</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TEjeciciones) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>INTERESES (NO BANCARIOS)</td>
                            <td style='width: 50%'>$0.0</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>INDEMNIZACIONES</td>
                            <td style='width: 50%'>$0.0</td>
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

            GranTotal += Tagua + TDrenaje + TRecargos + TReconexion + TConexion + TEjeciciones + TAlcantarillado + TMulta;
            GranTotalSaneamiento += TSaneamientA;
            StringBuilder builder = new StringBuilder();

            builder.Append(@"<table id='datos' style='width: 99%' class='anteriores'>");
            builder.Append($@"<caption style='background:#f1eeec'>EJERCICIOS ANTERIORES A {year}</caption>");
            builder.Append(@"<tbody>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>SERVICIO DE AGUA</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", Tagua) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>CONEXIONES</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TConexion) }</td>
                            </tr>");

            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>RECONEXIONES</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TReconexion) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>ALCANTARILLADO</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TAlcantarillado) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>DRENAJE</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TDrenaje) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>RECARGOS</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TRecargos) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>MULTAS</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TMulta) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>GASTOS DE EJECUCIÓN</td>
                            <td style='width: 50%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", TEjeciciones) }</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>INTERESES (NO BANCARIOS)</td>
                            <td style='width: 50%'>$0.0</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%' class='left'>INDEMNIZACIONES</td>
                            <td style='width: 50%'>$0.0</td>
                            </tr>");
            builder.Append($@"<tr>
                            <td style='width: 50%; background:#f1eeec;' class='centro'>TOTAL</td>
                            <td style='width: 50%'>{string.Format(new CultureInfo("es-MX"), "{0:C2}", GranTotal) }</td>
                            </tr>");
            builder.Append($@"</tbody>
                            </table>");
            builder.Append("</div>");
            builder.Append("<div id='datosBotton'><br>");
            builder.Append(@"<table id='datos' style='width: 99%; margin-top: 10px' class='informativos'>
                            <caption> DATOS ESTADÍSTICOS E INFORMATIVOS </caption> ");
            builder.Append(@"<tbody>");
            builder.Append($@"<tr style='background:#f1eeec;'>
                            <td colspan='3' rowspan='2' style='width: 47%' class='centro'>CONCEPTO</td>
                            <td colspan='3' class='centro'>IMPORTE<br>(PESOS)</td>
                            </tr>");

            builder.Append($@"<tr style='background:#f1eeec;'>                     
                            <td style='width: 18%' class='centro'>{year} <br>(A)</td>
                             <td style='width: 18%' class='centro'>EJERCICIOS ANTERIORES A {year} <br>(B)</td>
                            <td style='width: 20%' class='centro'>TOTAl<br>(C=A+B)</td>
                            </tr>");
            builder.Append($@" <tr>
                        <td style='width: 49%' class='left' colspan='3' >SANEAMIENTO</td>
                        <td style='width: 18%'>  { string.Format(new CultureInfo("es-MX"), "{0:C2}", TSaneamiento)}</td>
                        <td style='width: 18%' >  { string.Format(new CultureInfo("es-MX"), "{0:C2}", TSaneamientA)}</td>
                        <td style='width: 20%'>{ string.Format(new CultureInfo("es-MX"), "{0:C2}", GranTotalSaneamiento)}</td>
                        </tr>");
            
                       

            builder.Append(@"</tbody>
                            </table>   
                            </div>");

            return builder.ToString();
        }



    }
}

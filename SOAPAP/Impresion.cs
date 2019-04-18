using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP
{
    public partial class Impresion : Form
    {
        public Impresion()
        {
            InitializeComponent();
        }


        private void Impresion_Load_1(object sender, EventArgs e)
        {
            //System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            //System.Drawing.Printing.PaperSize size = new PaperSize();
            //size.RawKind = (int)PaperKind.Letter;

            ////var setup = this.reportViewer1.GetPageSettings();
            //pg.Margins = new System.Drawing.Printing.Margins(1, 1, 1, 1);
            //pg.PaperSize = size;

            //this.reportViewer1.SetPageSettings(pg);

            if(Variables.optionvistaimpresion == 1)
            {
               
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SOAPAP.Report1.rdlc";
            ReportDataSource rds1 = new ReportDataSource("DatosGenerales", Variables.datosgenerales);
            this.reportViewer1.LocalReport.DataSources.Add(rds1);
            ReportDataSource rds2 = new ReportDataSource("DatosPadron", Variables.datospadron);
            this.reportViewer1.LocalReport.DataSources.Add(rds2);
            ReportDataSource rds3 = new ReportDataSource("Pago", Variables.pagos);
            this.reportViewer1.LocalReport.DataSources.Add(rds3);
            ReportDataSource rds4 = new ReportDataSource("Imagen", Variables.ImagenData);
            this.reportViewer1.LocalReport.DataSources.Add(rds4);
            ReportDataSource rds5 = new ReportDataSource("Folio", Variables.Foliotiket);
            this.reportViewer1.LocalReport.DataSources.Add(rds5);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            }
            else if (Variables.optionvistaimpresion == 2)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SOAPAP.Report2.rdlc";
                ReportDataSource rds1 = new ReportDataSource("DatosGenerales", Variables.datosgenerales);
                this.reportViewer1.LocalReport.DataSources.Add(rds1);
                ReportDataSource rds2 = new ReportDataSource("DatosPadron", Variables.datospadron);
                this.reportViewer1.LocalReport.DataSources.Add(rds2);
                ReportDataSource rds4 = new ReportDataSource("Imagen", Variables.ImagenData);
                this.reportViewer1.LocalReport.DataSources.Add(rds4);
                ReportDataSource rds3 = new ReportDataSource("Pagos", Variables.pagos);
                this.reportViewer1.LocalReport.DataSources.Add(rds3);
                ReportDataSource rds5 = new ReportDataSource("Folio", Variables.Foliotiket);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.DataSources.Add(rds5);

            }
            else if (Variables.optionvistaimpresion == 3)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SOAPAP.Reporte3.rdlc";
                ReportDataSource rds1 = new ReportDataSource("DatosGenerales", Variables.datosgenerales);
                this.reportViewer1.LocalReport.DataSources.Add(rds1);
                ReportDataSource rds3 = new ReportDataSource("DatosPadron", Variables.datospadron);
                this.reportViewer1.LocalReport.DataSources.Add(rds3);
                ReportDataSource rds2 = new ReportDataSource("Productos", Variables.Productos);
                this.reportViewer1.LocalReport.DataSources.Add(rds2);
                ReportDataSource rds4 = new ReportDataSource("Imagen", Variables.ImagenData);
                this.reportViewer1.LocalReport.DataSources.Add(rds4);
                ReportDataSource rds5 = new ReportDataSource("Folio", Variables.Foliotiket);
                this.reportViewer1.LocalReport.DataSources.Add(rds5);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            }
            else if (Variables.optionvistaimpresion == 4)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SOAPAP.Report4.rdlc";
                ReportDataSource rds1 = new ReportDataSource("DatosGenerales", Variables.datosgenerales);
                this.reportViewer1.LocalReport.DataSources.Add(rds1);
                ReportDataSource rds2 = new ReportDataSource("DatosPadron", Variables.datospadron);
                this.reportViewer1.LocalReport.DataSources.Add(rds2);
                ReportDataSource rds4 = new ReportDataSource("Imagen", Variables.ImagenData);
                this.reportViewer1.LocalReport.DataSources.Add(rds4);
                ReportDataSource rds3 = new ReportDataSource("Pago", Variables.pagos);
                this.reportViewer1.LocalReport.DataSources.Add(rds3);
                ReportDataSource rds5 = new ReportDataSource("Folio", Variables.Foliotiket);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.DataSources.Add(rds5);

            }


            this.reportViewer1.RefreshReport();
        }

        
    }
}

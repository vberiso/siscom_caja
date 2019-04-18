using Humanizer;
using SOAPAP.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public class Tiraauditora : IDisposable
    {

        private IList<Stream> m_streams;
        PrintPreviewDialog ps = new PrintPreviewDialog();
        private int m_currentPageIndex;
        DataTable dts = new DataTable();
        string totals = string.Empty;
        querys q = new querys();


        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            int startX = 130;
            int startY = 0;
            int Offset = 0;
            int width = 150;
            int height =150;
            string cadena1 = string.Empty;
            string cadena2 = string.Empty;
            int nContador = 0;

            Graphics graphics = ev.Graphics;
            Image newImage = null;


            try
            {
                newImage = q.Imagen();
            }
            
            catch (Exception)
            {

                
            }
            StringFormat sfi = new StringFormat();
            sfi.LineAlignment = StringAlignment.Near;
            sfi.Alignment = StringAlignment.Near;

            StringFormat sfc = new StringFormat();
            sfc.LineAlignment = StringAlignment.Center;
            sfc.Alignment = StringAlignment.Center;

            try
            {
                graphics.DrawImage(newImage, 65, startY + Offset, width, height);
                Offset = Offset + 160;
            }
            
            catch (Exception)
            {
                Offset = Offset + 160;
            }

            int cantidad = Variables.Configuration.CompanyName.Length;

            if (cantidad > 27)
            {
                cadena1 = Variables.Configuration.CompanyName;
                cadena2 = Variables.Configuration.CompanyName;
                cadena1 = cadena1.Substring(0, 27);
                int resultado = cantidad - 27;
                cadena2 = cadena2.Substring(27, resultado);

                graphics.DrawString(cadena1, new Font("Courier New", 9),
                              new SolidBrush(Color.Black), startX, startY + Offset, sfc);
                Offset = Offset + 13;

                graphics.DrawString(cadena2, new Font("Courier New", 9),
                              new SolidBrush(Color.Black), startX, startY + Offset, sfc);
                Offset = Offset + 13;
            }
            else
            {
                graphics.DrawString(Variables.Configuration.CompanyName, new Font("Courier New", 9),
                                  new SolidBrush(Color.Black), startX, startY + Offset, sfc);
                Offset = Offset + 13;
            }

            graphics.DrawString("RFC:" + Variables.Configuration.RFC, new Font("Courier New", 9),
                                  new SolidBrush(Color.Black), startX, startY + Offset, sfc);
            Offset = Offset + 13;

            cantidad = Variables.Configuration.Address.Length;

            if (cantidad > 27)
            {
                cadena1 = Variables.Configuration.Address;
                cadena2 = Variables.Configuration.Address;
                cadena1 = cadena1.Substring(0, 27);
                int resultado = cantidad - 27;
                cadena2 = cadena2.Substring(27, resultado);

                graphics.DrawString(cadena1, new Font("Courier New", 9),
                                              new SolidBrush(Color.Black), startX, startY + Offset, sfc);
                Offset = Offset + 13;

                graphics.DrawString(cadena2, new Font("Courier New", 9),
                              new SolidBrush(Color.Black), startX, startY + Offset, sfc);
                Offset = Offset + 13;
            }
            else

            {
                graphics.DrawString(Variables.Configuration.Address, new Font("Courier New", 9),
                                  new SolidBrush(Color.Black), startX, startY + Offset, sfc);
                Offset = Offset + 13;
            }

            graphics.DrawString("Telefono:" + Variables.Configuration.Phone, new Font("Courier New", 9),
                                  new SolidBrush(Color.Black), startX, startY + Offset, sfc);
            Offset = Offset + 13;

            graphics.DrawString(Variables.Configuration.Email, new Font("Courier New", 9),
                                  new SolidBrush(Color.Black), startX, startY + Offset, sfc);
            Offset = Offset + 13;


            cantidad = Variables.Configuration.LegendRegime.Length;

            if (cantidad > 27)
            {
                cadena1 = Variables.Configuration.LegendRegime;
                cadena2 = Variables.Configuration.LegendRegime;
                cadena1 = cadena1.Substring(0, 27);
                int resultado = cantidad - 27;
                cadena2 = cadena2.Substring(27, resultado);

                graphics.DrawString(cadena1, new Font("Courier New", 9),
                                              new SolidBrush(Color.Black), startX, startY + Offset, sfc);
                Offset = Offset + 13;

                graphics.DrawString(cadena2, new Font("Courier New", 9),
                              new SolidBrush(Color.Black), startX, startY + Offset, sfc);
                Offset = Offset + 15;
            }
            else
            {
                graphics.DrawString(Variables.Configuration.LegendRegime, new Font("Courier New", 9),
                                  new SolidBrush(Color.Black), startX, startY + Offset, sfc);
                Offset = Offset + 15;
            }

            graphics.DrawString("MOVIMIENTOS", new Font("Arial", 9),
                                 new SolidBrush(Color.Black), startX, startY + Offset, sfc);
            Offset = Offset + 15;

            startX = 0;

            graphics.DrawString("-------------------------------------------------------", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 2;
            graphics.DrawString("-------------------------------------------------------", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            
            graphics.DrawString("CAJA:" + Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString()   , new Font("Courier New", 9),
                             new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("SUCURSAL:"+ Variables.Configuration.Terminal.BranchOffice.Name, new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("FECHA Y HORA:"+DateTime.Now.ToString("yyyy-MM-dd HH:mm"), new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX , startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("USUARIO:"+ Variables.LoginModel.FullName, new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("-------------------------------------------------------", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 2;
            graphics.DrawString("-------------------------------------------------------", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 25;
            
            startX = 0;

            graphics.DrawString("CONCEPTO", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            

            startX =  startX + 180;
            graphics.DrawString("TOTAL", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;

            startX = 0;

            foreach (DataRow dtRow in dts.Rows)
            {

                startX = 0;
                
                graphics.DrawString(dtRow[13].ToString(), new Font("Courier New", 9),
                                   new SolidBrush(Color.Black), startX, startY + Offset);

                startX = 180;

                if (Convert.ToBoolean(dtRow[3])==true)
                {
                    graphics.DrawString("+ "+dtRow[7].ToString(), new Font("Courier New", 9),
                                  new SolidBrush(Color.Black), startX, startY + Offset);
                }
                else
                {
                    graphics.DrawString("- " + dtRow[7].ToString(), new Font("Courier New", 9),
                                  new SolidBrush(Color.Black), startX, startY + Offset);
                }

                Offset = Offset + 20;
               
            }

            startX = 0;

            graphics.DrawString("-----------------------------------------------------", new Font("Courier New", 9),
                              new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 2;
            graphics.DrawString("-----------------------------------------------------", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;

            startX = 0;
            graphics.DrawString("Total", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 180;
            graphics.DrawString(totals, new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;


            startX = 0;

            
            int totalst = Convert.ToInt32(Math.Round(Convert.ToDecimal(totals)));
            string k = totalst.ToWords();

            graphics.DrawString("**" + k.ToUpper() + " PESOS M.N.**", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            
            Offset = Offset + 20;


            startX = 0;
            graphics.DrawString("Efectivo:", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 180;

            graphics.DrawString(Variables.efectivo.ToString(), new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;

            startX = 0;
            graphics.DrawString("Cheques:", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 180;
            graphics.DrawString(Variables.cheques.ToString(), new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;

            startX = 0;
            graphics.DrawString("Tarjetas:", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 180;
            graphics.DrawString(Variables.tarjetas.ToString(), new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;


            startX = 130;

            graphics.DrawString("______________________________", new Font("Courier New", 9),
                             new SolidBrush(Color.Black), startX, startY + Offset,sfc);
            Offset = Offset + 16;

            graphics.DrawString("Cajero", new Font("Courier New", 9),
                             new SolidBrush(Color.Black), startX, startY + Offset, sfc);
            Offset = Offset + 120;
            
            graphics.DrawString("______________________________", new Font("Courier New", 9),
                           new SolidBrush(Color.Black), startX, startY + Offset, sfc);
            Offset = Offset + 16;

            graphics.DrawString("Supervisor", new Font("Courier New", 9),
                             new SolidBrush(Color.Black), startX, startY + Offset, sfc);
            Offset = Offset + 20;
            
        }

        private void Print(string nombreimpresora)
        {
            PrintDocument printDoc;
            String printerName = nombreimpresora;
            printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;
            if (!printDoc.PrinterSettings.IsValid)
            {
                //throw new Exception(String.Format("No puedo encontrar la impresora \"{0}\".", printerName));
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        private void PrintPreview(string printerName)
        {
            PrintDocument printDoc = new PrintDocument();
            ps.Document = printDoc;
            printDoc.PrinterSettings.PrinterName = printerName;
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            m_currentPageIndex = 0;
            ps.Document = printDoc;
            ps.Show();
        }
       

        public void Imprime(DataTable dt,int mode,string total)
        {
            dts = dt;
            totals = total;
            
            if (mode == 1)
            {
                PrintPreview(q.ImpresoraPredeterminada());
            }

            else if (mode == 2)
            {
                    Print(q.ImpresoraPredeterminada());
            }
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
    }
}
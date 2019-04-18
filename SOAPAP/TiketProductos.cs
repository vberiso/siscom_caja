using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP
{
    class TiketProductos : IDisposable
    {
        private IList<Stream> m_streams;
        PrintPreviewDialog ps = new PrintPreviewDialog();
        private int m_currentPageIndex;
        DataTable dts = new DataTable();
        string subtotal = string.Empty;
        string iva = string.Empty;
        string redondeo = string.Empty;
        string total = string.Empty;
        string metododepago = string.Empty;
        string nombreclientep = string.Empty;
        string foliodecajap = string.Empty;
        string periodop = string.Empty;
        string cuentap = string.Empty;
        string foliotransaccionp = string.Empty;
        string rfcp = string.Empty;
        string direecionp = string.Empty;
        querys q = new querys();

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            int startX = 130;
            int startY = 0;
            int Offset = 0;
            int width = 150;
            int height = 150;
            string cadena1 = string.Empty;
            string cadena2 = string.Empty;
            Image newImage = null;
            Graphics graphics = ev.Graphics;
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

            StringFormat sfd = new StringFormat();
            sfd.LineAlignment = StringAlignment.Far;
            sfd.Alignment = StringAlignment.Far;

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


            graphics.DrawString("TICIKET PRODUCTOS", new Font("Courier New", 9),
                                 new SolidBrush(Color.Black), startX, startY + Offset, sfc);
            Offset = Offset + 13;

            startX = 0;

            graphics.DrawString("---------------------------------------------------------", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 2;
            graphics.DrawString("---------------------------------------------------------", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("FECHA Y HORA:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;

            graphics.DrawString("CUENTA:" + cuentap, new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;

            cantidad = nombreclientep.Length;

            if (cantidad > 20)
            {
                cadena1 = nombreclientep;
                cadena2 = nombreclientep;
                cadena1 = cadena1.Substring(0, 20);
                int resultado = cantidad - 20;
                cadena2 = cadena2.Substring(20, resultado);

                graphics.DrawString("CONTRIBUYENTE:" + cadena1, new Font("Courier New", 9),
                                   new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                graphics.DrawString(cadena2, new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

            }


            graphics.DrawString("RFC:" + rfcp, new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            
            cantidad = direecionp.Length;

            if (cantidad > 27)
            {
                cadena1 = direecionp;
                cadena2 = direecionp;
                cadena1 = cadena1.Substring(0, 27);
                int resultado = cantidad - 27;
                cadena2 = cadena2.Substring(27, resultado);

                graphics.DrawString("DOMICILIO:" + cadena1, new Font("Courier New", 9),
                                   new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                graphics.DrawString(cadena2, new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

            }

            graphics.DrawString("---------------------------------------------------------", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 2;
            graphics.DrawString("---------------------------------------------------------", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;


            graphics.DrawString("CAJA:" + Variables.Configuration.Terminal.TerminalUsers.FirstOrDefault().Id.ToString(), new Font("Courier New", 9),
                             new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;

            graphics.DrawString("SUCURSAL:" + Variables.Configuration.Terminal.BranchOffice.Name.ToString(), new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;

            graphics.DrawString("USUARIO:" + Variables.LoginModel.FullName.ToString(), new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;

            graphics.DrawString("---------------------------------------------------------", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 2;
            graphics.DrawString("---------------------------------------------------------", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            
            graphics.DrawString("CONCEPTO", new Font("Courier New", 9),
                                   new SolidBrush(Color.Black), startX, startY + Offset);
            startX = 180;

            graphics.DrawString("IMPORTE", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);

            startX = 0;
            Offset = Offset + 15;

            graphics.DrawString("__________________________________________________________", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 25;


            foreach (DataRow dtRow in dts.Rows)
            {
                graphics.DrawString(dtRow[1].ToString(), new Font("Courier New", 9),
                                   new SolidBrush(Color.Black), startX, startY + Offset);
                startX = 180;
                graphics.DrawString("$" + dtRow[2].ToString(), new Font("Courier New", 9),
                                   new SolidBrush(Color.Black), startX, startY + Offset);

                startX = 0;

                Offset = Offset + 15;
            }


            graphics.DrawString("__________________________________________________________", new Font("Courier New", 9),
                               new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 25;

            
            startX = 0;
            graphics.DrawString("TOTAL: ", new Font("Courier New", 9),
                              new SolidBrush(Color.Black), startX, startY + Offset);
            startX = 180;

            graphics.DrawString(total, new Font("Courier New", 9),
                              new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 15;

            startX = 0;

            string k1 = total.Replace("$", "");
            int totals = Convert.ToInt32(Math.Round(Convert.ToDecimal(k1)));
            string k = totals.ToWords();

            graphics.DrawString("**" + k.ToUpper() + " PESOS M.N.**", new Font("Courier New", 9),
                              new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;

            
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

        private void PrintPreview()
        {
            PrintDocument printDoc = new PrintDocument();
            ps.Document = printDoc;
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            m_currentPageIndex = 0;
            ps.Show();
            ps.Document = printDoc;
        }

        public void Imprime(DataTable dt, int mode, string totalp,string nombrecliente, string cuenta, string rfc)
        {
            dts = dt;
            total = totalp;
            nombreclientep = nombrecliente;   
            cuentap = cuenta;
            rfcp = rfc;

            foreach (DataRow row in dts.Rows)
            {

            }


            if (mode == 1)
            {
                PrintPreview();
            }

            else if (mode == 2)
            {
                Print(q.ImpresoraPredeterminada());
            }

        }

        public void Imprime(int id, bool mode)
        {

            if (mode == false)
            {
                PrintPreview();
            }

            else if (mode == true)
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.UI.HistorialTransacciones
{
    class DataHistorialElemento
    {
        public int id_payment { get; set; }
        public int id_order_sale { get; set; }
        public string account { get; set; }
        public string status { get; set; }
        public int code_concept { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public decimal Descuento { get; set; }
        public decimal Subtotal { get; set; }
        public decimal iva { get; set; }
        public decimal Total { get; set; }
        public string tipo { get; set; }
    }
}

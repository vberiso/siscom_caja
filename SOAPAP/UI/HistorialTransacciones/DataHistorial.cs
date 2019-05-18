using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.UI.HistorialTransacciones
{
    class DataHistorial
    {        
        public int IdTransaction { get; set; }
        public string FechaTransaction { get; set; }
        public int TypeTransactionId { get; set; }
        public string TypeTransactionName { get; set; }
        public int Sign { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal rounding { get; set; }
        public decimal Total { get; set; }
        public string MetodoPago { get; set; }
        public string Cajero { get; set; }
        public string Detalle { get; set; }
        public decimal Subtotal { get; set; }
    }
}

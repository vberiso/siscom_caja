using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    class DataCollection
    {
        public int id_payment { get; set; }
        public string OFICINA { get; set; }
        public string code_concept { get; set; }
        public string Status { get; set; }
        public string Contribuyente { get; set; }
        public string Serie { get; set; }
        public string CAJERO { get; set; }
        public string FECHA_PAGO { get; set; }
        public string CUENTA { get; set; }
        public string folio_impresion { get; set; }
        public string DESCRIPCION { get; set; }
        public string MetodoPago { get; set; }
        public string OrigenPago { get; set; }
        public string OrigenPagoExterno { get; set; }
        public decimal MONTO { get; set; }        
        public decimal DESCUENTO { get; set; }
        public decimal SUBTOTAL { get; set; }
        public decimal IVA { get; set; }
        public decimal TOTAL { get; set; }        
    }
}

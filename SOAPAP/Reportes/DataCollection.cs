using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    class DataCollection
    {
        public string OFICINA { get; set; }
        public string CAJERO { get; set; }
        public string FECHA_PAGO { get; set; }
        public string DESCRIPCION { get; set; }        
        public string name_concept { get; set; }
        public decimal MONTO { get; set; }        
        public decimal DESCUENTO { get; set; }
        public decimal SUBTOTAL { get; set; }
        public decimal IVA { get; set; }
        public decimal TOTAL { get; set; }        
    }
}

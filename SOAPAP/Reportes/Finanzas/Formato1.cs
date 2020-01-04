using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes.Finanzas
{
    class Formato1
    {
        public decimal importe { get; set; }
        public string concepto { get; set; }
        public int code_concept { get; set; }
        public decimal descuento { get; set; }
        public int datePayment { get; set; }
    }
}

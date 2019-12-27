using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes.Finanzas.Ayuntamiento
{
    public class Cedula
    {

        public decimal monto { get; set; }
        public string type_intake { get; set; }
        public int inicio { get; set; }
        public int fin { get; set; }
        public int code_concept { get; set; }
        public int construccion_no { get; set; }
        public decimal discount_amount { get; set; } 
        public int month_payment { get; set; }
        public string descuento { get; set; }
    }
}

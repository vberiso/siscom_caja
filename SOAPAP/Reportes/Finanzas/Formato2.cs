using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes.Finanzas
{
    class Formato2
    {
        public decimal importe { get; set; }
        public string uso { get; set; }
        public int id_agreement { get; set; }
        public string type_agreement { get; set; }
        public string payment_date { get; set; }
    }
}

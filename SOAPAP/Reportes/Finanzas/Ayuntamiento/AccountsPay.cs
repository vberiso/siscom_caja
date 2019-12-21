using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes.Finanzas.Ayuntamiento
{
    public class AccountsPay
    {
        public string id_agreement { get; set; }
        public int from_date { get; set; }
        public int until_date { get; set; }
        public DateTime payment_date { get; set; }

    }
}

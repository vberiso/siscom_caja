using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    public class DataTaxpayerDebt
    {
        public string Description { get; set; }
        public decimal Monto { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    public class DataTaxpayerOrderSale
    {        
        public string FechaPago { get; set; }
        public string FechaExpiracion { get; set; }
        public string Description { get; set; }
        public string NameConcept { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}

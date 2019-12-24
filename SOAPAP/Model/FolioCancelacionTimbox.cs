using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class FolioCancelacionTimbox
    {
        public string UUID { get; set; }
        public string ReceptorRFC { get; set; }
        public decimal Total { get; set; }
        public int PaymentId { get; set; }
        public int TaxReceiptId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.FacturadoTimbox.Model
{
    public class DataCancelationTimbox
    {
        public string uuid { get; set; }
        public string receptorRFC { get; set; }
        public decimal total { get; set; }
        public int paymentId { get; set; }
        public int taxReceiptId { get; set; }
    }
}

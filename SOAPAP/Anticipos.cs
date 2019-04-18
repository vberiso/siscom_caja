using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Anticipos
    {
        public int  id { get; set; }
        public int folio { get; set; }
        public string notificationDate { get; set; }
        public string fromDate { get; set; }
        public string untilDate { get; set; }
        public decimal subtotal { get; set; }
        public decimal tax { get; set; }
        public decimal rounding { get; set; }
        public decimal total { get; set; }
        public string status { get; set; }
        public int agreementId { get; set; }
        // public int agreement { get; set; }
        public ICollection<notificationDetails> notificationDetails { get; set; }
        
    }
}
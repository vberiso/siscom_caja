using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class XML
    {
        public DateTime taxReceiptDate { get; set; }
        public string xml { get; set; }
        public string fielXML { get; set; }
        public string RFC { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string userId { get; set; }
        public int paymentId { get; set; }
    }
}

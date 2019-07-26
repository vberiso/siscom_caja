using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TaxReceiptCancel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime RequestDateCancel { get; set; }
        public DateTime CancelationDate { get; set; }
        public byte[] AcuseXml { get; set; }
        public int TaxReceiptId { get; set; }
        public TaxReceipt TaxReceipt { get; set; }
    }
}

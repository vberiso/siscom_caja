using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TaxReceipt
    {
        public int Id { get; set; }
        public DateTime TaxReceiptDate { get; set; }
        public string Xml { get; set; }
        public string FielXML { get; set; }
        public string RFC { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public int PaymentId { get; set; }
        public string IdXmlFacturama { get; set; }
        public string UsoCFDI { get; set; }
    }
}

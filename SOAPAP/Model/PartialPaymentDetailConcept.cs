using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PartialPaymentDetailConcept
    {       
        public int Id { get; set; }        
        public decimal Amount { get; set; }        
        public decimal OnAccount { get; set; }       
        public bool HaveTax { get; set; }       
        public string CodeConcept { get; set; }        
        public string NameConcept { get; set; }
        public int PartialPaymentDetailId { get; set; }
        public PartialPaymentDetail PartialPaymentDetail { get; set; }
    }
}

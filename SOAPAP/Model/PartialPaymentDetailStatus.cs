using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PartialPaymentDetailStatus
    {       
        public int Id { get; set; }        
        public string Status { get; set; }       
        public DateTime PartialPaymentDetailStatusDate { get; set; }       
        public string User { get; set; }
        public int PartialPaymentDetailId { get; set; }
        public PartialPaymentDetail PartialPaymentDetail { get; set; }
    }
}

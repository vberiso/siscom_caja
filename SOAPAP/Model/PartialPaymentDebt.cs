using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PartialPaymentDebt
    {        
        public int Id { get; set; }        
        public int DebtId { get; set; }        
        public decimal Amount { get; set; }        
        public decimal OnAccount { get; set; }       
        public string Status { get; set; }
        public int PartialPaymentId { get; set; }
        public PartialPayment PartialPayment { get; set; }
    }
}

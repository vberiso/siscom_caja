using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
     
   class TransactionVMS
    {
        
        public bool sign { get; set; }
        public decimal amount { get; set; }
        public decimal tax { get; set; }
        public string percentageTax { get; set; }
        public decimal rounding { get; set; }
        public decimal total { get; set; }
        public string aplication { get; set; }
        public string cancellationFolio { get; set; }
        public string authorizationOriginPayment { get; set; }
        public int typeTransactionId { get; set; }
        public string type { get; set; }
        public int payMethodId { get; set; }
        public int terminalUserId { get; set; }
        public int originPaymentId { get; set; }
        public int externalOriginPaymentId { get; set; }
        public int agreementId { get; set; }
        public string  paytStatus { get; set; }
        public string account { get; set; }
        public ICollection<TransactionDetail> transactionDetails { get; set; }
        //public ICollection<Deb> deb { get; set; }
    }
}

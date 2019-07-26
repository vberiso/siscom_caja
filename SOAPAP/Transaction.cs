using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    public class Transaction
    {
       public int id { get; set; }
       public string folio { get; set; }
       public DateTime dateTransaction { get; set; }
       public bool sign { get; set; }
       public decimal amount { get; set; }
       public decimal tax { get; set; }
       public decimal rounding { get; set; }
       public decimal total { get; set; }
       public string aplication { get; set; }
       public string cancellationFolio { get; set; }
       public string authorizationOriginPayment { get; set; }
       public TypeTransaction typeTransaction { get; set; }
       public PayMethod payMethod { get; set; }
       public int typeTransactionId { get; set; }
       public int payMethodId { get; set; }
       public int terminalUserId { get; set; }
        public int originPaymentId { get; set; }
        public int externalOriginPaymentId { get; set; }
        public int agreementId { get; set; }
        public string cancellation { get; set; }
        public string accountNumber { get; set; }
        public string account { get; set; }
        public ICollection<TransactionFolios> transactionFolios { get; set; }
       public ICollection<TransactionDetail> transactionDetails { get; set; }
    }
}
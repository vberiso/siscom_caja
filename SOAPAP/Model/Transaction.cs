using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Transaction
    {
        public Transaction()
        {
            TransactionFolios = new HashSet<TransactionFolio>();
            TransactionDetails = new HashSet<TransactionDetail>();
        }
       
        public int Id { get; set; }
        public string Folio { get; set; }
        public DateTime DateTransaction { get; set; }
        public bool Sign { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal Rounding { get; set; }
        public decimal Total { get; set; }
        public string Aplication { get; set; }
        public string CancellationFolio { get; set; }
        public string AuthorizationOriginPayment { get; set; }
        public string NumberBank { get; set; }
        public string AccountNumber { get; set; }
        public string Account { get; set; }
        public int TerminalUserId { get; set; }
        public TerminalUser TerminalUser { get; set; }
        public int TypeTransactionId { get; set; }
        public TypeTransaction TypeTransaction { get; set; }
        public int PayMethodId { get; set; }
        public PayMethod PayMethod { get; set; }
        public int OriginPaymentId { get; set; }
        public OriginPayment OriginPayment { get; set; }
        public int ExternalOriginPaymentId { get; set; }
        public ExternalOriginPayment ExternalOriginPayment { get; set; }
        public ICollection<TransactionFolio> TransactionFolios { get; set; }
        public ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}

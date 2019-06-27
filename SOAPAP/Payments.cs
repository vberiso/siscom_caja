using SOAPAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Payments
    {
        public int id { get; set; }
        public  string paymentDate { get; set; }
        public string branchOffice { get; set; }
        public  decimal subtotal { get; set; }
        public string percentageTax { get; set; }
        public decimal tax { get; set; }
        public decimal rounding { get; set; }
        public decimal total { get; set; }
        public string authorizationOriginPayment { get; set; }
        public string transactionFolio { get; set; }
        public int agreementId { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string PayMethodNumber { get; set; }
        public bool HaveTaxReceipt { get; set; }
        public string Account { get; set; }
        public int OrderSaleId { get; set; }
        public int originPaymentId { get; set; }
        public OriginPayment originPayment { get; set; }
        public int externalOriginPaymentId { get; set; }
        public ExternalOriginPayment externalOriginPayment { get; set; }
        public int payMethodId { get; set; }
        public PayMethod payMethod { get; set; }
        public ICollection<PaymentDetails> paymentDetails { get; set; }
        public ICollection<TaxReceipt> taxReceipts { get; set; }
        
    }
}

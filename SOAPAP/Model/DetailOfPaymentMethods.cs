using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class DetailOfPaymentMethods
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string AuthorizationBank { get; set; }
        public string CheckIssuanceSeries { get; set; }
        public string AccountNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string BankName { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}

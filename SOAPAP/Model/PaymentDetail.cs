using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        public string CodeConcept { get; set; }
        public string AccountNumber { get; set; }
        public string UnitMeasurement { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public int DebtId { get; set; }
        public Debt Debt { get; set; }
        public int PrepaidId { get; set; }
        public Prepaid Prepaid { get; set; }
        public int OrderSaleId { get; set; }
        public OrderSale OrderSale { get; set; }
        public bool HaveTax { get; set; }
        public decimal Tax { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}

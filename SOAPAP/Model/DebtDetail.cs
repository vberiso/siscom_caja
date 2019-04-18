using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class DebtDetail
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal OnAccount { get; set; }
        public decimal OnPayment { get; set; }
        public bool HaveTax { get; set; }
        public string CodeConcept { get; set; }
        public string NameConcept { get; set; }
        public decimal Tax { get; set; }
        public int DebtId { get; set; }
        public Debt Debt { get; set; }
        public decimal quantity { get; set; }
    }
}

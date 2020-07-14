using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class ImpresionFacturaModel
    {
        public int idPaymentDetail { get; set; }
        public int idPayment { get; set; }
        public int idDebt { get; set; }
        public int idDebtDetail { get; set; }
        public string CodeConcept { get; set; }
        public string AccountNumber { get; set; }
        public string UnitMessurement { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public string TypePaymentDetail { get; set; }
        public decimal AmountDebtDetail { get; set; }
        public decimal OnAccountDebtDetail { get; set; }
        public DateTime FromDateDebt { get; set; }
        public DateTime UntilDateDebt { get; set; }
        public int YearDebt { get; set; }
        public string TypeDebt { get; set; }
        public string CodeConceptDebtDetail { get; set; }
        public decimal QuantityDebtDetail { get; set; }

        public int IdDebtRecargo { get; set; }
        public decimal AmountDebtDetailRecargo { get; set; }
    }
}

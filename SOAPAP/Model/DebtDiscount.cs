using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class DebtDiscount
    {
        public int Id { get; set; }
        public string CodeConcept { get; set; }
        public string NameConcept { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int DebtId { get; set; }
        public Debt Debt { get; set; }
    }
}

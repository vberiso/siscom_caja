using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.UI.Condonations
{
    class DetalleCondonations
    {
        public int IdDebt { get; set; }        
        public int IdDebtDetail { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime UntilDate { get; set; }
        public int Year { get; set; }
        public string CodeConcept { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal OnAccount { get; set; }
        public decimal Total { get; set; }
    }
}

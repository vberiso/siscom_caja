using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PaymentConceptsVM
    {
        public TransactionVM Transaction { get; set; }
        public List<Debt> Debt { get; set; }
    }
}

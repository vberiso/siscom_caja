using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class CancelPaymentVM
    {
        public TransactionVM Transaction { get; set; }
        public Payment Payment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PaymentResume
    {
        public Payment payment { get; set; }
        public OrderSale orderSale { get; set; }
    }
}

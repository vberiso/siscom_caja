using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PaymentOrdersVM
    {
        public PaymentOrdersVM()
        {
            OrderSale = new List<OrderSale>();
        }

        public TransactionVM Transaction { get; set; }
        public List<OrderSale> OrderSale { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class DiscountAuthorizationDetail
    {
        public int Id { get; set; }
        [Required]
        public int DebtId { get; set; }
        public Debt Debt { get; set; }
        [Required]
        public int OrderSaleId { get; set; }
        public OrderSale OrderSale { get; set; }
    }
}

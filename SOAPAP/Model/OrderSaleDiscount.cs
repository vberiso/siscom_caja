using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class OrderSaleDiscount
    {
        public int Id { get; set; }      
        public string CodeConcept { get; set; }
        public string NameConcept { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public Int16 DiscountPercentage { get; set; }
        public int OrderSaleDetailId { get; set; }

        public int OrderSaleId { get; set; }
        public OrderSale OrderSale { get; set; }
    }
}

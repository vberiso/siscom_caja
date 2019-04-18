using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAPAP.Model
{
    public class OrderSaleDetails
    {        
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Unity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool HaveTax { get; set; }
        public string Description { get; set; }
        public string CodeConcept { get; set; }
        public string NameConcept { get; set; }
        public decimal Amount { get; set; }
        public decimal OnAccount { get; set; }
        public decimal Tax { get; set; }

        public int OrderSaleId { get; set; }
        public OrderSale OrderSale { get; set; }
    }
}

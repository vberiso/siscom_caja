using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOAPAP.Model
{    
    public class OrderSale
    {
        public OrderSale()
        {
            OrderSaleDetails = new HashSet<OrderSaleDetails>();
        }
                
        public int Id { get; set; }
        public string Folio { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal Amount { get; set; }
        public decimal OnAccount { get; set; }
        public Int16 Year { get; set; }
        public Int16 Period { get; set; }
        public string Type { get; set; }
        public string DescriptionType { get; set; }
        public string Status { get; set; }
        public string DescriptionStatus { get; set; }
        public string Observation { get; set; }
        public int IdOrigin { get; set; }
        public DateTime ExpirationDate { get; set; }        

        public int DivisionId { get; set; }
        public Division Division { get; set; }

        public int TaxUserId { get; set; }
        public TaxUser TaxUser { get; set; }

        public ICollection<OrderSaleDetails> OrderSaleDetails { get; set; }
        public ICollection<OrderSaleDiscount> OrderSaleDiscounts { get; set; }
    }
}

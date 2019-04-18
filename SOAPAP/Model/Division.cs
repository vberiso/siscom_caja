using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SOAPAP.Model
{
    public class Division
    {
        public Division()
        {
            Products = new HashSet<Product>();
            OrderSale = new HashSet<OrderSale>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int IdSolution { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<OrderSale> OrderSale { get; set; }
    }
}

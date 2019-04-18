using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SOAPAP.Model
{   
    public class Product
    {
        public Product()
        {
            TariffProducts = new HashSet<TariffProduct>();
            ProductParams = new HashSet<ProductParam>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Int16 Order { get; set; }
        public int Parent { get; set; }
        public bool HaveTariff { get; set; }
        public bool IsActive { get; set; }

        public int DivisionId { get; set; }
        public Division Division { get; set; }

        public ICollection<TariffProduct> TariffProducts { get; set; }
        public ICollection<ProductParam> ProductParams { get; set; }
    }
}

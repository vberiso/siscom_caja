using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SOAPAP.Model
{ 
    public class TariffProduct
    {      
        public int Id { get; set; }     
        public string AccountNumber { get; set; }
        public bool HaveTax { get; set; }   
        public decimal Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime UntilDate { get; set; }
        public int IsActive { get; set; }
        public Int16 Percentage { get; set; }
        public Int16 TimesFactor { get; set; }
        public bool IsVariable { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    public class Tariff
    {
        public int id { set; get; }
        public int productId { set; get; }
        public int name { set; get; }
        public bool isVariable{ set; get; }
        public int timesFactor { set; get; }
        public int accountNumber { set; get; }
        public bool haveTax { set; get; }
        public bool isActive { set; get; }
        public double amount { set; get; }
        public string fromDate { set; get; }
        public string untilDate { set; get; }
        public decimal percentage { set; get; }
        public string type { set; get; }
        public Model.Product product { get; set; }

    }
}
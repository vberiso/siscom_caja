using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TaxAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Outdoor { get; set; }
        public string Indoor { get; set; }
        public string Zip { get; set; }
        public string Suburb { get; set; }
        public string Town { get; set; }
        public string State { get; set; }

        public int TaxUserId { get; set; }
        public TaxUser TaxUser { get; set; }
    }
}

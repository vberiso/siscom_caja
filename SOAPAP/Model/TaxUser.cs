using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TaxUser
    {
        public TaxUser()
        {
            TaxAddresses = new HashSet<TaxAddress>();
            Breaches = new HashSet<Breach>();
            OrderSales = new HashSet<OrderSale>();
        }
        public int Id { get; set; }
        public String Name { get; set; }
        public String RFC { get; set; }
        public String CURP { get; set; }
        public String PhoneNumber { get; set; }
        public String EMail { get; set; }
        public bool IsActive { get; set; }

        public ICollection<TaxAddress> TaxAddresses { get; set; }
        public ICollection<Breach> Breaches { get; set; }
        public ICollection<OrderSale> OrderSales { get; set; }
    }
}

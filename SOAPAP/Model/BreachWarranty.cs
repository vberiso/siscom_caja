using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class BreachWarranty
    {
        public int Id { get; set; }
        public string References { get; set; }
        public string Observations { get; set; }

        public int BreachId { get; set; }
        public Breach Breach { get; set; }

        public int WarrantyId { get; set; }
        public Warranty Warranty { get; set; }
    }
}

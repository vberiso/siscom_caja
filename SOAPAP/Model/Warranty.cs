using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Warranty
    {
        public Warranty()
        {
            BreachWarranty = new HashSet<BreachWarranty>();
        }
       
        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<BreachWarranty> BreachWarranty { get; set; }
    }
}

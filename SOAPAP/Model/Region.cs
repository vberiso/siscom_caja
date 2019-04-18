using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Region
    {
        public Region()
        {
            Suburbs = new HashSet<Suburb>();
        }

        public int Id { get; set; }
        public int Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<Suburb> Suburbs { get; set; }
    }
}

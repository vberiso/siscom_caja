using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Town
    {
        public Town()
        {
            Suburbs = new HashSet<Suburb>();
        }
     
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public State States { get; set; }

        public ICollection<Suburb> Suburbs { get; set; }
    }
}

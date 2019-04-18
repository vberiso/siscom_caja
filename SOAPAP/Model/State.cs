using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class State
    {
        public State()
        {
            Towns = new HashSet<Town>();
        }

        public int Id { get; set; }        
        public string Name { get; set; }
        public int CountriesId { get; set; }
        public Country Countries { get; set; }

        public ICollection<Town> Towns { get; set; }
    }
}

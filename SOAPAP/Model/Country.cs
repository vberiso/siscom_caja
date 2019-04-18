using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Country
    {
        public Country()
        {
            States = new HashSet<State>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public ICollection<State> States { get; set; }
    }
}

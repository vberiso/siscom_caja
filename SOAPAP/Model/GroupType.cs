using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class GroupType
    {
        public GroupType()
        {
            Types = new HashSet<Types>();
        }

        public int Id { get; set; }
     
        public string Name { get; set; }
        public string Observations { get; set; }

        public ICollection<Types> Types { get; set; }
    }
}

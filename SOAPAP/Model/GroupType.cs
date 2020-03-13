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
            Types = new HashSet<Type>();
        }

        public int Id { get; set; }
     
        public string Name { get; set; }

        public ICollection<Type> Types { get; set; }
    }
}

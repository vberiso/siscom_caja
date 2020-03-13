using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class GroupStatus
    {

        public GroupStatus()
        {
            Statuses = new HashSet<Status>();
        }

      
        public int Id { get; set; }
     
        public string Name { get; set; }

        public ICollection<Status> Statuses { get; set; }
    }
}

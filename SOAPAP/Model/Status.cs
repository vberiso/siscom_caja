using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Status
    {

        public string CodeName { get; set; }
       
        public int GroupStatusId { get; set; }

       
        public string Description { get; set; }

        public GroupStatus GroupStatus { get; set; }
    }
}

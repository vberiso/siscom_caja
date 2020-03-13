using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Types
    {

     
        public string CodeName { get; set; }
       
        public string Description { get; set; }

        //[ForeignKey("GroupType")]
        public int GroupTypeId { get; set; }
        public GroupType GroupType { get; set; }
    }
}

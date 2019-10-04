using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TechnicalStaff
    {        
        public int Id { get; set; }        
        public string Name { get; set; }        
        public string Phone { get; set; }        
        public bool IsActive { get; set; }        
        public int TechnicalRoleId { get; set; }
        //public TechnicalRole TechnicalRole { get; set; }       
        public int TechnicalTeamId { get; set; }
        //public TechnicalTeam TechnicalTeam { get; set; }
        public ICollection<OrderWork> OrderWorks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class ReasonCatalog
    {        
        public int Id { get; set; }       
        public string Description { get; set; }        
        public bool IsActive { get; set; }       
        public string Type { get; set; }
        public ICollection<OrderWorkReasonCatalog> OrderWorkReasonCatalogs { get; set; }
    }
}

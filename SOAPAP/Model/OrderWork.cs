using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class OrderWork
    {        
        public int Id { get; set; }
               
        public string Folio { get; set; }
                
        public DateTime DateOrder { get; set; }
                
        public string Applicant { get; set; }
        
        public string Type { get; set; }
               
        public string Status { get; set; }
               
        public string Observation { get; set; }
               
        public DateTime DateStimated { get; set; }
                
        public DateTime DateRealization { get; set; }
              
        public string Activities { get; set; }
              
        public int AgrementId { get; set; }
              
        public int TaxUserId { get; set; }
               
        public int? TechnicalStaffId { get; set; }

        public TechnicalStaff TechnicalStaff { get; set; }

        public Agreement Agreement { get; set; }

        public ICollection<OrderWorkStatus> OrderWorkStatus { get; set; }

        public ICollection<OrderWorkReasonCatalog> OrderWorkReasonCatalogs { get; set; }
    }
}

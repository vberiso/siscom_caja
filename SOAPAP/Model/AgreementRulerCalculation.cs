using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class AgreementRulerCalculation
    {
     
        public int Id { get; set; }
       
        public int ServiceId { get; set; }
        
        public decimal Amount { get; set; }
       
        public DateTime DateIN { get; set; }
       
        public bool IsActive { get; set; }
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
    }
}

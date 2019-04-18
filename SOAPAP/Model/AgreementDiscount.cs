using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class AgreementDiscount
    {
      
        public int IdDiscount { get; set; }
        public Discount Discount { get; set; }
        public int IdAgreement { get; set; }
        public Agreement Agreement { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}

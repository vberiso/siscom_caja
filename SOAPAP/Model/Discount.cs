using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int16 Percentage { get; set; }
        public bool IsActive { get; set; }
        public Int16 Month { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool InAgreement { get; set; }

        public ICollection<AgreementDiscount> AgreementDiscounts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class DiscountCampaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int16 Percentage { get; set; }
        public bool IsVariable { get; set; }
        public bool profile { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

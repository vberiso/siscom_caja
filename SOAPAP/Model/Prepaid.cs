using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Prepaid
    {
        public int Id { get; set; }
        public DateTime PrepaidDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Accredited { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public string Type { get; set; }
        public string TypeDescription { get; set; }
        public Int16 Percentage { get; set; }
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
        public ICollection<PrepaidDetail> PrepaidDetails { get; set; }
    }
}

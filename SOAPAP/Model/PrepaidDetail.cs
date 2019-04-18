using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PrepaidDetail
    {
        public PrepaidDetail()
        {

            DebtPrepaids = new HashSet<DebtPrepaid>();
        }

        public int Id { get; set; }
        public DateTime PrepaidDetailDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public int PrepaidId { get; set; }
        public Prepaid Prepaid { get; set; }
        public ICollection<DebtPrepaid> DebtPrepaids { get; set; }
    }
}

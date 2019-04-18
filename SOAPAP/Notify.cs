using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Notify
    {
        public int id { get; set; }
        public string prepaidDate { get; set; }
        public decimal amount { get; set; }
        public decimal accredited { get; set; }
        public string status { get; set; }
        public string statusDescription { get; set; }
        public string type { get; set; }
        public int percentage { get; set; }
        public int agreementId { get; set; }
        public string agreement { get; set; }
        public ICollection<prepaidDetails> prepaidDetails { get; set; }
    }
}

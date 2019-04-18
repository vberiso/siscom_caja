using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class pyment
    {
        public int id { get; set; }
        public string debitDate { get; set; }
        public string fromDate { get; set; }
        public string untilDate { get; set; }
        public int derivatives { get; set; }
        public string typeIntake { get; set; }
        public string typeService { get; set; }
        public string consumption { get; set; }
        public decimal amount { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        
    }
}

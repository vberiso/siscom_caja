using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Debst
    {
        public int id { set; get; }
        public DateTime debitDate { set; get; }
        public DateTime fromDate { set; get; }
        public DateTime untilDate { set; get; }
        public int derivatives { set; get; }
        public string typeIntake { set; get; }
        public string typeService { set; get; }
        public string consumption { set; get; }
        public string discount { set; get; }
        public decimal amount { set; get; }
        public string onAccount { set; get; }
        public int year { set; get; }
        public string type { set; get; }
        public string status { set; get; }
        public string debtPeriodId { set; get; }
        public string debtPeriod { set; get; }
        public string agreementId { set; get; }
        public string agreement { set; get; }
        public string newStatus { set; get; }
        public ICollection<Debtdetails> debtdetails { get; set; }
    }
}

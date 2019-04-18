using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class prepaidDetails
    {
        public int id { set; get; }
        public string prepaidDetailDate { set; get; }
        public decimal amount{ set; get; }
        public string status { set; get; }
        public int prepaidId{ set; get; }
        public ICollection<debtPrepaids> debtPrepaids { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Debtdetails
    {
        public int id { set; get; }
        public decimal amount { set; get; }
        public string onAccount { set; get; }
        public string onPayment { set; get; }
        public bool haveTax { set; get; }
        public string codeConcept { set; get; }
        public string nameConcept { set; get; }
        public decimal tax { set; get; }
        public int debtId { set; get; }
    }
}

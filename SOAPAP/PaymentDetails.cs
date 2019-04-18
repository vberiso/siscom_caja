using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class PaymentDetails
    {
       public int id { get; set; }
       public string codeConcept { get; set; }
       public string accountNumber { get; set; }
       public string unitMeasurement { get; set; }
       public string description { get; set; }
       public decimal amount { get; set; }
       public int debtId { get; set; }
       public int prepaidId { get; set; }
       public int paymentId { get; set; }
       public bool haveTax { get; set; }
       public decimal tax { get; set; }
       public Deb debt { get; set; }
    }
}

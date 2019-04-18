using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class debtPrepaids
    {
         public int id { set; get; }
         public int codeConcept { set; get; }
         public string nameConcept{ set; get; }
         public decimal originalAmount{ set; get; }
         public decimal paymentAmount{ set; get; }
         public int debtId{ set; get; }
         public int prepaidDetailId{ set; get; }
    }
}
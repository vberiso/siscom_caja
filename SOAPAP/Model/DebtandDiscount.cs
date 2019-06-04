using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class DebtandDiscount
    {
        public List<DebtDetail> Detail { get; set; }
        public List<DebtDiscount> DebtDiscount { get; set; }
    }
}

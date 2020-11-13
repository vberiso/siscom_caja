using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model.Discounts
{
    public class PeriodsDiscount
    {
        public int año { get; set; }
        public List<int> meses { get; set; }
        public int Descuento { get; set; }       
    }
}

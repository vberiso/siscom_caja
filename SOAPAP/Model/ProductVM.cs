using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class ProductVM
    {
        public AgreementProductVM Agreement { get; set; }
        public Debt Debt { get; set; }
    }
}

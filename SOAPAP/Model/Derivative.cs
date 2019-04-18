using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Derivative
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
        public int AgreementDerivative { get; set; }
    }
}

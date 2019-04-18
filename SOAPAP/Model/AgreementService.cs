using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
   public class AgreementService
    {
        public int IdService { get; set; }
        public Service Service { get; set; }

        public int IdAgreement { get; set; }
        public Agreement Agreement { get; set; }

        public DateTime DateAgreement { get; set; }
        public bool IsActive { get; set; }
    }
}

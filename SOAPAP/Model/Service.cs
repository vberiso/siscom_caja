using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Service
    {
        public Service()
        {
            AgreementServices = new HashSet<AgreementService>();
            Tariffs = new HashSet<Tariff>();
            ServiceParams = new HashSet<ServiceParam>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Int16 Order { get; set; }
        public bool IsCommercial { get; set; }
        public bool IsActive { get; set; }
        public bool InAgreement { get; set; }
        public ICollection<AgreementService> AgreementServices { get; set; }
        public ICollection<Tariff> Tariffs { get; set; }
        public ICollection<ServiceParam> ServiceParams { get; set; }
    }
}

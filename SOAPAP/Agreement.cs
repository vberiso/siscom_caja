using SOAPAP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Agreement
    {
        public string id { set; get; }
        public string account { set; get; }
        public DateTime accountDate { set; get; }
        public int numDerivatives { set; get; }
        public DateTime stratDate { set; get; }
        public string typeAgreement { set; get; }
        public TypeService TypeService { get; set; }
        public TypeUse TypeUse { get; set; }
        public TypeConsume TypeConsume { get; set; }
        public TypeRegime TypeRegime { get; set; }
        public TypePeriod TypePeriod { get; set; }
        public TypeCommercialBusiness TypeCommertialBusiness { get; set; }
        public TypeStateService TypeStateService { get; set; }
        public TypeIntake TypeIntake { get; set; }
        public Diameter Diameter { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<Adress> Addresses { get; set; }
        public ICollection<AgreementService> AgreementServices { get; set; }
        public ICollection<AgreementDiscount> AgreementDiscounts { get; set; }
        public ICollection<AgreementDetails> agreementDetails { get; set; }
        public ICollection<AgreementComent> AgreementComents { get; set; }
        public ICollection<OrderWork> OrderWork { get; set; }
    }
}
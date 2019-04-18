using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Agreement
    {
        public Agreement()
        {
            Clients = new HashSet<Client>();
            Addresses = new HashSet<Address>();
            Meters = new HashSet<Meter>();
            Debts = new HashSet<Debt>();
            Derivatives = new HashSet<Derivative>();
            AgreementServices = new HashSet<AgreementService>();
            Prepaids = new HashSet<Prepaid>();
            Notifications = new HashSet<Notification>();
            AgreementDetails = new HashSet<AgreementDetail>();
        }
       
        public int Id { get; set; }
        public string Account { get; set; }
        public DateTime AccountDate { get; set; }
        public int NumDerivatives { get; set; }
        public DateTime StratDate { get; set; }
        public string TypeAgreement { get; set; }
        public string Route { get; set; }
        public int TypeServiceId { get; set; }
        public TypeService TypeService { get; set; }
        public int TypeUseId { get; set; }
        public TypeUse TypeUse { get; set; }
        public int TypeConsumeId { get; set; }
        public TypeConsume TypeConsume { get; set; }
        public int TypeRegimeId { get; set; }
        public TypeRegime TypeRegime { get; set; }
        public int TypePeriodId { get; set; }
        public TypePeriod TypePeriod { get; set; }
        public int TypeCommertialBusinessId { get; set; }
        public TypeCommercialBusiness TypeCommertialBusiness { get; set; }
        public int TypeStateServiceId { get; set; }
        public TypeStateService TypeStateService { get; set; }
        public int TypeIntakeId { get; set; }
        public TypeIntake TypeIntake { get; set; }
        public int DiameterId { get; set; }
        public Diameter Diameter { get; set; }
        public int TypeClassificationId { get; set; }
        public TypeClassification TypeClassification { get; set; }

        public ICollection<Client> Clients { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<AgreementService> AgreementServices { get; set; }
        public ICollection<AgreementDiscount> AgreementDiscounts { get; set; }
        public ICollection<Meter> Meters { get; set; }
        public ICollection<Debt> Debts { get; set; }
        public ICollection<Derivative> Derivatives { get; set; }
        public ICollection<AgreementLog> AgreementLogs { get; set; }
        public ICollection<Prepaid> Prepaids { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<AgreementDetail> AgreementDetails { get; set; }
    }
}

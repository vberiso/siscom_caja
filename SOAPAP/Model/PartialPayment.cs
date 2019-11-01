using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PartialPayment
    {
        public PartialPayment()
        {
            PartialPaymentDetails = new HashSet<PartialPaymentDetail>();
            PartialPaymentDebts = new HashSet<PartialPaymentDebt>();
        }

        
        public int Id { get; set; }
        
        public string Folio { get; set; }
        
        public DateTime PartialPaymentDate { get; set; }
        
        public decimal Amount { get; set; }
       
        public int NumberOfPayments { get; set; }
        
        public decimal InitialPayment { get; set; }
        
        public string Status { get; set; }
        
        public string TypeIntake { get; set; }
     
        public string TypeService { get; set; }
      
        public DateTime ExpirationDate { get; set; }
      
        public DateTime FromDate { get; set; }
       
        public DateTime UntilDate { get; set; }
        
        public string Observations { get; set; }
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
        
        public string SignatureName { get; set; }
       
        public string IdentificationCard { get; set; }
       
        public string IdentificationNumber { get; set; }
       
        public string Email { get; set; }
       
        public string Phone { get; set; }
        public ICollection<PartialPaymentDetail> PartialPaymentDetails { get; set; }
        public ICollection<PartialPaymentDebt> PartialPaymentDebts { get; set; }
    }
}

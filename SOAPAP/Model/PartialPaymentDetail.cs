using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PartialPaymentDetail
    {
        public PartialPaymentDetail()
        {
            PartialPaymentDetailStatuses = new HashSet<PartialPaymentDetailStatus>();
            PartialPaymentDetailConcepts = new HashSet<PartialPaymentDetailConcept>();
        }
                
        public int Id { get; set; }
        
        public int PaymentNumber { get; set; }
        
        public decimal Amount { get; set; }
       
        public decimal OnAccount { get; set; }
       
        public string Status { get; set; }
        
        public DateTime RelaseDate { get; set; }
        
        public int RelaseDebtId { get; set; }
       
        public DateTime PaymentDate { get; set; }
        public int PaymentId { get; set; }
        public int PartialPaymentId { get; set; }
        public PartialPayment PartialPayment { get; set; }
        public ICollection<PartialPaymentDetailStatus> PartialPaymentDetailStatuses { get; set; }
        public ICollection<PartialPaymentDetailConcept> PartialPaymentDetailConcepts { get; set; }
    }
}

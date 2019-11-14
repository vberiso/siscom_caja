using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class DebtAnnual
    {
      
        public int Id { get; set; }
       
        public DateTime DebitDate { get; set; }
     
        public DateTime FromDate { get; set; }
       
        public DateTime UntilDate { get; set; }
       
        public string TypeIntake { get; set; }
       
        public string TypeService { get; set; }
        
        public Int16 Year { get; set; }
        
        public string Type { get; set; }
       
        public string Status { get; set; }
       
        public int Sequential { get; set; }
       
        public string CodeConcept { get; set; }
        
        public string NameConcept { get; set; }
     
        public decimal Amount { get; set; }
        
        public bool HaveTax { get; set; }
       
        public int DebtId { get; set; }
        
        public int AgreementId { get; set; }
    }
}

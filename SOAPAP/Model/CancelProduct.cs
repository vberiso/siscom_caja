using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class CancelProduct
    {        
        public int Id { get; set; }        
        public string Account { get; set; }        
        public string CodeConcept { get; set; }        
        public string NameConcept { get; set; }       
        public DateTime RequestDate { get; set; }       
        public string RequesterId { get; set; }        
        public DateTime AuthorisationDate { get; set; }        
        public string SupervisorId { get; set; }        
        public string Status { get; set; }       
        public string Type { get; set; }       
        public string MotivoCancelacion { get; set; }        
        public int DebtId { get; set; }        
        public int OrderSaleId { get; set; }        
    }
}

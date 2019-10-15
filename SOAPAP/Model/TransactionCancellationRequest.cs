using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TransactionCancellationRequest
    {       
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime DateRequest { get; set; }       
        public string Reason { get; set; }
        public string Manager { get; set; }
        public DateTime DateAuthorization { get; set; }       
        public string ManagerObservation { get; set; }        
        public string KeyFirebase { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}

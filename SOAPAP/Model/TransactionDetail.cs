using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TransactionDetail
    {        
        public int Id { get; set; }
        public string CodeConcept { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}

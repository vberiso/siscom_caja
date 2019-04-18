using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class OriginPayment
    {
        public OriginPayment()
        {
            Payments = new HashSet<Payment>();
            Transactions = new HashSet<Transaction>();
        }
       
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}

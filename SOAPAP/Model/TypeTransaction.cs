using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TypeTransaction
    {
        public TypeTransaction()
        {
            Transactions = new HashSet<Transaction>();
        }
       
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}

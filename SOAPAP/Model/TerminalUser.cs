using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TerminalUser
    {
        public TerminalUser()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public bool InOperation { get; set; }
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}

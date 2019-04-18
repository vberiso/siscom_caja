using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Terminal
    {
        public Terminal()
        {
            TerminalUsers = new HashSet<TerminalUser>();
        }
       
        public int Id { get; set; }
        public string MacAdress { get; set; }
        public decimal CashBox { get; set; }
        public bool IsActive { get; set; }
        public string SerialNumber { get; set; }
        public int BranchOfficeId { get; set; }
        public BranchOffice BranchOffice { get; set; }

        public ICollection<TerminalUser> TerminalUsers { get; set; }
    }
}

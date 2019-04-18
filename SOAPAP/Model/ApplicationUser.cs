using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class ApplicationUser
    {
        public ApplicationUser()
        {
            TerminalUsers = new HashSet<TerminalUser>();
            AgreementLogs = new HashSet<AgreementLog>();
        }
        
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public int DivitionId { get; set; }

        public ICollection<TerminalUser> TerminalUsers { get; set; }
        public ICollection<AgreementLog> AgreementLogs { get; set; }
    }
}

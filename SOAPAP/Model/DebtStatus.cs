using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class DebtStatus
    {
        public int Id { get; set; }
        public string id_status { get; set; }
        public DateTime DebtStatusDate { get; set; }
        public string User { get; set; }
        public int DebtId { get; set; }
        public Debt Debt { get; set; }
    }
}

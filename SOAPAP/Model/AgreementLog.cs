using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class AgreementLog
    {
        public int Id { get; set; }
        public DateTime AgreementLogDate { get; set; }
        public string Description { get; set; }
        public string Observation { get; set; }
        public bool Visible { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
    }
}

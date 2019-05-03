using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PushNotification
    {
        public string UserRequestId { get; set; }
        public string UserResponseId { get; set; }
        public string BranchOffice { get; set; }
        public int AuthorizationDiscountId { get; set; }
        public bool IsReply { get; set; }
        public string Observation { get; set; }
        public string Status { get; set; }
        public string Account { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Notification
    {
        public Notification()
        {
            NotificationDetails = new HashSet<NotificationDetail>();
        }

        public int Id { get; set; }
        public string Folio { get; set; }
        public DateTime NotificationDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime UntilDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Rounding { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
        public ICollection<NotificationDetail> NotificationDetails { get; set; }
    }
}

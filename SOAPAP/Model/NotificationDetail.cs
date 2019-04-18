using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class NotificationDetail
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public bool HaveTax { get; set; }
        public string CodeConcept { get; set; }
        public string NameConcept { get; set; }
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
    }
}

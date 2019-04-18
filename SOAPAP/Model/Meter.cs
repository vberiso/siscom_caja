using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Meter
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Consumption { get; set; }
        public DateTime InstallDate { get; set; }
        public DateTime DeinstallDate { get; set; }
        public string Serial { get; set; }
        public string Wheels { get; set; }
        public bool IsActive { get; set; }
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
        public ICollection<Consumption> Consumptions { get; set; }
    }
}

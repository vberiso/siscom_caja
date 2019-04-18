using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Tariff
    {
        public int Id { get; set; }
        public string Concept { get; set; }
        public string AccountNumber { get; set; }
        public bool HaveTax { get; set; }
        public Int16 Percentage { get; set; }
        public decimal Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime UntilDate { get; set; }
        public int IsActive { get; set; }
        public decimal StartConsume { get; set; }
        public decimal EndConsume { get; set; }
        public bool HaveConsume { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int TypeIntakeId { get; set; }
        public TypeIntake TypeIntake { get; set; }
        public int TypeConsumeId { get; set; }
        public TypeConsume TypeConsume { get; set; }
    }
}

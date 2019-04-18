using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Consumption
    {
        public int Id { get; set; }
        public DateTime ConsumptionDate { get; set; }
        public decimal PreviousConsumption { get; set; }
        public decimal CurrentConsumption { get; set; }
        public decimal consumption { get; set; }
        public bool is_active { get; set; }
        public int DebtId { get; set; }
        public bool InDebt { get; set; }
        public int MeterId { get; set; }
        public Meter Meter { get; set; }
    }
}

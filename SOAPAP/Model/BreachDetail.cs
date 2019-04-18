using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class BreachDetail
    {
        public int Id { get; set; }
        public int AplicationDays { get; set; }
        public decimal Amount { get; set; }
        public decimal PercentBonification { get; set; }
        public decimal Bonification { get; set; }

        public int BreachId { get; set; }
        public Breach Breach { get; set; }

        public int BreachListId { get; set; }
        public BreachList BreachList { get; set; }
    }
}

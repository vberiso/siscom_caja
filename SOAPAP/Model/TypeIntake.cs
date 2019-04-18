using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TypeIntake
    {
        public TypeIntake()
        {
            Agreements = new HashSet<Agreement>();
            Tariffs = new HashSet<Tariff>();
        }
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Agreement> Agreements { get; set; }
        public ICollection<Tariff> Tariffs { get; set; }
    }
}

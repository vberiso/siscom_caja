using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TypePeriod
    {
        public TypePeriod()
        {
            Agreements = new HashSet<Agreement>();
        }
       
        public int Id { get; set; }
        public string Name { get; set; }
        public Int16 Mounth { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Agreement> Agreements { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TypeClassification
    {
        public TypeClassification()
        {
            Agreements = new HashSet<Agreement>();
        }
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string IntakeAcronym { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Agreement> Agreements { get; set; }
    }
}

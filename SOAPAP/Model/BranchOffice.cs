using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class BranchOffice
    {
        public BranchOffice()
        {
            Terminals = new HashSet<Terminal>();
            Folios = new HashSet<Folio>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Opening { get; set; }
        public DateTime Closing { get; set; }
        public bool DontClose { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Terminal> Terminals { get; set; }
        public ICollection<Folio> Folios { get; set; }
    }
}

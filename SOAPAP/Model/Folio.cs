using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Folio
    {
        public int Id { get; set; }
        public string Range { get; set; }
        public int Initial { get; set; }
        public int Secuential { get; set; }
        public DateTime DateCurrent { get; set; }
        public int IsActive { get; set; }

        public int BranchOfficeId { get; set; }
        public BranchOffice BranchOffice { get; set; }
    }
}

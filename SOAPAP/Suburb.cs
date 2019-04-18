using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Suburb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Town Towns { get; set; }
        public Region Regions { get; set; }
        public Clasification Clasifications { get; set; }
    }
}

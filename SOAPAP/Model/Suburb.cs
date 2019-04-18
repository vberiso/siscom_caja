using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Suburb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TownsId { get; set; }
        public Town Towns { get; set; }
        public int RegionsId { get; set; }
        public Region Regions { get; set; }
        public int ClasificationsId { get; set; }
        public Clasification Clasifications { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public int suburbId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Adress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Outdoor { get; set; }
        public string Indoor { get; set; }
        public string Zip { get; set; }
        public string Reference { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string TypeAddress { get; set; }
        public int AgreementsId { get; set; }
        public Suburb Suburbs { get; set; }
    }
}
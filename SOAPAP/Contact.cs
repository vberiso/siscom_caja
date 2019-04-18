using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Contact
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string TypeNumber { get; set; }
        public int IsActive { get; set; }
        public Client Client { get; set; }
    }
}

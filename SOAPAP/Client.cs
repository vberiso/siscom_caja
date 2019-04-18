using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string secondLastName { get; set; }
        public string rfc { get; set; }
        public string curp { get; set; }
        public string ine { get; set; }
        public string eMail { get; set; }
        public string typeUser { get; set; }
        public string agreementId { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Client
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public Client()
        {
            Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string INE { get; set; }
        public string EMail { get; set; }
        public string TypeUser { get; set; }
        public bool IsActive { get; set; }
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
        public ICollection<Contact> Contacts { get; set; }

    }
}

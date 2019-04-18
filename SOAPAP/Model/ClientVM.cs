using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class ClientVM
    {
        public int Id { get; set; }
        [ Required]
        public string Name { get; set; }
        [ Required]
        public string LastName { get; set; }
        [ Required]
        public string SecondLastName { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string INE { get; set; }
        public string EMail { get; set; }
        public string TypeUser { get; set; }
        public bool IsMale { get; set; }
        public bool IsActive { get; set; }

        public List<ContactVM> Contacts { get; set; }
    }

    public class UpdateClient
    {
        public UpdateClient()
        {
            Client = new List<ClientVM>();
        }
        public List<ClientVM> Client { get; set; }
    }
}

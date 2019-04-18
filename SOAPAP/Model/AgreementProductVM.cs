using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class AgreementProductVM
    {
        public int Id { get; set; }
        public List<AdressVM> Adresses { get; set; }
        public List<ClientVM> Clients { get; set; }
    }
}

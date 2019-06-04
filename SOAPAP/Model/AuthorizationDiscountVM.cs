using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class AuthorizationDiscountVM
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string UserId { get; set; }
        public string ResponseObservations { get; set; }
        public string Status { get; set; }
    }
}

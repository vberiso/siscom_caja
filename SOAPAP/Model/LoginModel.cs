using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace SOAPAP.Model
{
    public class LoginModel
    {
        public string User { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public List<string> RolName { get; set; }
        public int Divition { get; set; }
        public bool CanStamp { get; set; }
        public string Serial { get; set; }
    }
}

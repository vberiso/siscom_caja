using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class AccountStatusInFile
    {
        public int Id { get; set; }        
        public string UserId { get; set; }        
        public string UserName { get; set; }        
        public DateTime GenerationDate { get; set; }        
        public string FileName { get; set; }        
        public string Folio { get; set; }       
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class OrderWorkStatus
    {        
        public int Id { get; set; }
               
        public string IdStatus { get; set; }
                
        public DateTime OrderWorkStatusDate { get; set; }
               
        public string User { get; set; }
               
        public int OrderWorkId { get; set; }
        public OrderWork OrderWork { get; set; }
    }
}

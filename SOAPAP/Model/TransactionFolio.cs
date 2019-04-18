using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TransactionFolio
    {       
        public string Folio { get; set; }
        public DateTime DatePrint { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}

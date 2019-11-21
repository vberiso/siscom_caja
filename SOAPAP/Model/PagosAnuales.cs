using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PagosAnuales
    {
        public int Id { get; set; }        
        public int AgreementId { get; set; }        
        public int DebtId { get; set; }        
        public DateTime DateDebt { get; set; }        
        public string Status { get; set; }
    }
}

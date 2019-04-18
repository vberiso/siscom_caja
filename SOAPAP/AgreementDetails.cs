using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class AgreementDetails
    {
            public int id { get; set; }
            public string folio { get; set; }
            public string register { get; set; }
            public decimal taxableBase { get; set; }
            public decimal ground { get; set; } 
            public decimal built { get; set; }
            public string agreementDetailDate { get; set; }
            public string lastUpdate { get; set; }
            public int sector { get; set; }
            public string observation { get; set; }
            public int agreementId { get; set; }
    } 
}

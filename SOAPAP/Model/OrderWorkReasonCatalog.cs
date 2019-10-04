using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class OrderWorkReasonCatalog
    {
        public int OrderWorkId { get; set; }
        public OrderWork OrderWork { get; set; }
        public int ReasonCatalogId { get; set; }
        public ReasonCatalog ReasonCatalog { get; set; }
    }
}

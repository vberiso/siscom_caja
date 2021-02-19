using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model.Discounts
{
    public class Descuentos
    {
        public List<string> tipos { get; set; }
        public List<int> codes { get; set; }
        public int descuento { get; set; }
        public DateTime condonacionDeudaDesde { get; set; }
        public DateTime condonacionDeudaHasta { get; set; }
    }
}

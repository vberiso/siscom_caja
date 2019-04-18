using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP
{
    class Venta
    {

        public TransactionVMS transaction { get; set; }
        public ICollection<Deb> debt { get; set; }

    }
}


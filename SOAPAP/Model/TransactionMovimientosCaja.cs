using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class TransactionMovimientosCaja
    {
        public int IdTransaction { get; set; }
        public string FolioTransaccion { get; set; }
        public string Cuenta { get; set; }
        public string Operacion { get; set; }
        public string FolioImpresion { get; set; }
        public string Hora { get; set; }
        public decimal Total { get; set; }
        public bool Signo { get; set; }
        public bool HaveInvoice { get; set; }
    }
}

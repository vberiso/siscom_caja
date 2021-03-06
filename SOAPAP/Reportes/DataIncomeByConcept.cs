using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    class DataIncomeByConcept
    {
        public string fecha_pago { get; set; }
        public int id_payment { get; set; }
        public string CUENTA { get; set; }
        public string description { get; set; }
        public string tipo_movimiento { get; set; }
        public decimal importe { get; set; }
        public decimal iva { get; set; }
        public decimal Descuento { get; set; }
        public string folioTransaction { get; set; }
        public string folio_impresion { get; set; }
        public string branch_office { get; set; }
        public string cliente { get; set; }
        public string RUTA { get; set; }
        public string metodo_pago { get; set; }
        public string origen_pago { get; set; }
        public string banco { get; set; }
        public string cajero { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public int code_concept { get; set; }
    }
}

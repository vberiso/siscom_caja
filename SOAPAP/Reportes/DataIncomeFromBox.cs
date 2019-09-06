using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    class DataIncomeFromBox
    {
        public int id_transaction { get; set; }
        public int id_payment { get; set; }
        public int id_DebtOrOrder { get; set; }
        public int id_PuO_detail { get; set; }
        public string FECHA { get; set; }
        public bool sign { get; set; }
        public string CUENTA { get; set; }
        public string code_concept { get; set; }
        public string Descripcion { get; set; }
        public decimal monto { get; set; }
        public decimal DESC { get; set; }
        public decimal SUBTOTAL { get; set; }
        public decimal iva { get; set; }        
        public decimal TOTAL_PD { get; set; }
        public decimal TOTAL_P { get; set; }        
        public string folioTransaction { get; set; }
        public string folio_impresion { get; set; }
        public string branch_office { get; set; }
        public string cliente { get; set; }
        public string rfc { get; set; }
        public string cajero { get; set; }
        public string Serial { get; set; }
        public string status { get; set; }
        public string ESTATUS { get; set; }
        public string TipoTransaccion { get; set; }
        public string MetodoPago { get; set; }
        public string OrigenPago { get; set; }
        public string OrigenPagoExterno { get; set; }
        public string TipoId { get; set; }
        public string TipoNombre { get; set; }
        public int DivisionId { get; set; }
        public string Division { get; set; }
    }
}

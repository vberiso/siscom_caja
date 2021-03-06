using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    class DataCollection
    {
        public int id_payment { get; set; }
        public int id_PuO_detail { get; set; }
        public string TipoPredio { get; set; }
        public int year { get; set; }
        public string Agrupado { get; set; }
        public string AccountNumber { get; set; }
        public string Construccion { get; set; }
        public string OFICINA { get; set; }        
        public string CAJERO { get; set; }
        public string FECHA_PAGO { get; set; }
        public string CUENTA { get; set; }
        public string folio_impresion { get; set; }
        public string type { get; set; }
        public string Status { get; set; }
        public string code_concept { get; set; }
        public string DESCRIPCION { get; set; }
        public string MetodoPago { get; set; }
        public string OrigenPago { get; set; }
        public string OrigenPagoExterno { get; set; }
        public decimal MONTO { get; set; }
        public decimal DESCUENTO { get; set; }
        public decimal SUBTOTAL { get; set; }
        public decimal IVA { get; set; }
        public decimal TOTAL { get; set; }
        public string Serie { get; set; }
        public string IdContribuyente { get; set; }
        public string Contribuyente { get; set; }
        public string ContribuyenteDireccion { get; set; }
        public string ContribuyenteColonia { get; set; }
        public int DivisionId { get; set; }
        public string Division { get; set; }
        public string discount { get; set; }
        public string GrupoVulnerable { get; set; }
        public string discountAuto { get; set; }

        public string MotivoDescuento { get { return DESCUENTO == 0 ? "" : (string.IsNullOrEmpty(discount) ? (string.IsNullOrEmpty(GrupoVulnerable) ? (discountAuto) : GrupoVulnerable) : discount); } }

        public string FolioFiscal { get; set; }

    }
}

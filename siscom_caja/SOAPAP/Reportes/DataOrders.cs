using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    class DataOrders
    {
        public int id_order_sale { get; set; }
        public int id_payment { get; set; }
        public string folio { get; set; }
        public string account { get; set; }
        public string FechaOS { get; set; }
        public string FechaPago { get; set; }
        public int DivisionId { get; set; }
        public string Division { get; set; }
        public string branch_office { get; set; }
        public string description { get; set; }
        public int TaxUserId { get; set; }
        public string Cliente { get; set; }
        public decimal MONTO { get; set; }
        public decimal DESCUENTO { get; set; }
        public decimal SUBTOTAL { get; set; }
        public decimal IVA { get; set; }
        public decimal TOTAL { get; set; }
        public decimal on_account { get; set; }
        public int year { get; set; }
        public int period { get; set; }
        public string Estado { get; set; }
        public string EstadoPago { get; set; }
        public string expiration_date { get; set; }
        public string Serial { get; set; }
        public string cajero { get; set; }
        public int count { get { return 1; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    class DataIncomeOfTreasury
    {
        public string id_payment { get; set; }
        public string Fecha { get; set; }
        public string Oficina { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string Cuenta { get; set; }
        public int DivisionId { get; set; }
        public string Area { get; set; }
        public int ClienteId { get; set; }
        public string Cliente { get; set; }
        public string TransactionFolio { get; set; }
        public string Folio { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto_dt { get; set; }
        public decimal Descuento_dt { get; set; }
        public decimal Subtotal_dt { get; set; }
        public decimal iva_dt { get; set; }
        public decimal Total_dt { get; set; }
        public string Cajero { get; set; }
        public string OrigenPago { get; set; }
        public string NombreOrigenPago { get; set; }
        public string MetodoPago { get; set; }
        public int count { get { return 1; } }

        public decimal TotalCS
        {
            get { return Total * (Estado.Equals("Activo") ? 1 : -1); }
        }
        public decimal Monto_dt_CS
        {
            get { return Monto_dt * (Estado.Equals("Activo") ? 1 : -1); }
        }
        public decimal Descuento_dt_CS
        {
            get { return Descuento_dt * (Estado.Equals("Activo") ? 1 : -1); }
        }
        public decimal Subtotal_dt_CS
        {
            get { return Subtotal_dt * (Estado.Equals("Activo") ? 1 : -1); }
        }
        public decimal iva_dt_CS
        {
            get { return iva_dt * (Estado.Equals("Activo") ? 1 : -1); }
        }
        public decimal Total_dt_CS
        {
            get { return Total_dt * (Estado.Equals("Activo") ? 1 : -1); }
        }
    }
}

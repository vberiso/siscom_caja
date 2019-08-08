using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.UI.HistorialTransacciones
{
    class DataHistorial
    {        
        public int id_transaction { get; set; }
        public int id_payment { get; set; }
        public int id_DebtOrOrder { get; set; }
        public int id_PuO_detail { get; set; }
        public string folio { get; set; }
        public string date_transaction { get; set; }        
        public Boolean sign { get; set; }
        public decimal amount { get; set; }
        public decimal tax { get; set; }
        public decimal rounding { get; set; }
        public decimal total { get; set; }
        public string cancellation_folio { get; set; }
        public string account { get; set; }
        public int TypeTransactionId { get; set; }
        public string TypeTransactionName { get; set; }
        public int PayMetodId { get; set; }
        public string TipoPago { get; set; }
        public string Oficina { get; set; }
        public string cajero { get; set; }
        public string Descripcion_dt { get; set; }
        public decimal Monto_dt { get; set; }
        public decimal Descuento_dt { get; set; }
        public decimal Subtotal_dt { get; set; }
        public decimal iva_dt { get; set; }
        public decimal Total_dt { get; set; }
        public string tipo_dt { get; set; }

        public decimal TotalConSigno
        {
            get { return total * (sign ? 1 : -1); }
        }


        public decimal MontoConSigno_dt
        {
            get { return Monto_dt * (sign ? 1 : -1); }
        }
        public decimal DescuentoConSigno_dt
        {
            get { return Descuento_dt * (sign ? 1 : -1); }
        }
        public decimal SubtotalConSigno_dt
        {
            get { return Subtotal_dt * (sign ? 1 : -1); }
        }
        public decimal ivaConSigno_dt
        {
            get { return iva_dt * (sign ? 1 : -1); }
        }
        public decimal TotalConSigno_dt
        {
            get { return Total_dt * (sign ? 1 : -1); }
        }


        public string Fecha
        {
            get { return date_transaction.Split('T')[0]; }
        }
        public string Hora
        {
            get { return date_transaction.Split('T')[1].Substring(0,8); }
        }
    }
}

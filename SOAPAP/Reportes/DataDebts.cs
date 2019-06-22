using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    class DataDebts
    {
        public int id_agreement { get; set; }
        public int Cuenta { get; set; }
        public string FechaContrato { get; set; }
        public string TipoPredio { get; set; }
        public string TipoEstadoServicio { get; set; }
        public string TipoComercio { get; set; }
        public string cliente { get; set; }
        public string rfc { get; set; }
        public string DOMICILIO { get; set; }
        public int idCOLONIA { get; set; }
        public string COLONIA { get; set; }
        public int Region { get; set; }
        public string Clasificacion { get; set; }
        public string id_debt { get; set; }
        public string DESDE { get; set; }
        public string HASTA { get; set; }
        public string Servicio_Ag { get; set; }
        public decimal Monto_Ag { get; set; }
        public decimal Recarto_Ag { get; set; }
        public string Sevicio_Dr { get; set; }
        public decimal Monto_Dr { get; set; }
        public decimal Recargo_Dr { get; set; }
        public string Sevicio_Sa { get; set; }
        public decimal Monto_Sa { get; set; }
        public decimal Recargo_Sa { get; set; }
        public decimal Total_Debt { get; set; }
        public decimal Total_DebtDetail { get; set; }
        public string Descuento { get; set; }
        public int DescuentoPorcentaje { get; set; }
        public int count { get { return 1; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    class DataDebtsAyunt
    {
        public int id_agreement { get; set; }
        public string Cuenta { get; set; }
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
                
        public string Servicio_Pr { get; set; }
        public decimal Monto_Pr { get; set; }
        public decimal Recargo_Pr { get; set; }
        public decimal Total_Pr { get; set; }
        public string DESDE_Pr { get; set; }
        public string HASTA_Pr { get; set; }

        public string Servicio_Li { get; set; }
        public decimal Monto_Li { get; set; }
        public decimal Recargo_Li { get; set; }
        public decimal Total_Li { get; set; }
        public string DESDE_Li { get; set; }
        public string HASTA_Li { get; set; }
                       
        public string Descuento { get; set; }
        public int DescuentoPorcentaje { get; set; }
        public int count { get { return 1; } }
    }
}

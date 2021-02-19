using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model.Discounts
{
    public class Promotions
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombrePublico { get; set; }
        public string DescripcionPublico { get; set; }
        public string ObservacionFactura { get; set; }
        public DateTime VigenciaInicio { get; set; }
        public DateTime VigenciaFinal { get; set; }
        public string Src { get; set; }
        public string imageBase64 { get; set; }
        public List<int> TiposToma { get; set; }
        public bool AplicaEnOnline { get; set; }
        public int PromocionAño { get; set; }
        public int PromocionMesIncio { get; set; }
        public int PromocionMesFinal { get; set; }
        public List<PeriodsDiscount> PromocionAplicar { get; set; }        
        public bool BorrarDeudaAñoPromocion { get; set; }
        public List<Descuentos> Descuentos { get; set; }
        public List<Descuentos> Condonaciones { get; set; }
    }
}

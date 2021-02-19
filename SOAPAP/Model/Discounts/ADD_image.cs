using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model.Discounts
{
    public class ADD_image
    {
        public string Nombre { get; set; }
        public string NombrePublico { get; set; }
        public string DescripcionPublico { get; set; }        
        public DateTime VigenciaInicio { get; set; }
        public DateTime VigenciaFinal { get; set; }
        public string Src { get; set; }
        public bool AplicaEnOnline { get; set; }        
    }
}

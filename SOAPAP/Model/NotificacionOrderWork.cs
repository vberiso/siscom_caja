using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class NotificacionOrderWork
    {
        public string Account { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Cheked { get; set; }
    }
}

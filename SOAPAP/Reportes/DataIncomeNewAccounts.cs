using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    class DataIncomeNewAccounts
    {
        public int id_agreement { get; set; }
        public string CUENTA { get; set; }
        public string CONTRATO_FECHA { get; set; }
        public int CONTRATO_AÑO { get; set; }
        public int CONTRATO_MES { get; set; }
        public int idCOLONIA { get; set; }
        public string COLONIA { get; set; }
        public string TipoViviendas { get; set; }
        public decimal AguaMonto { get; set; }
        public decimal DrenajeMonto { get; set; }
        public decimal SaneamientoMonto { get; set; }
        public int count { get { return 1; } }
    }
}

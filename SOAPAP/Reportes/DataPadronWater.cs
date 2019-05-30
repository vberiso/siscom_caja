using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    class DataPadronWater
    {
        public int id_agreement { get; set; }
        public string CUENTA { get; set; }
        public string NOMBRE { get; set; }
        public string DOMICILIO { get; set; }
        public int idCOLONIA { get; set; }
        public string COLONIA { get; set; }
        public string RUTA { get; set; }
        public string CONTRATO { get; set; }
        public string CONTRATO_AÑO { get; set; }
        public string CONTRATO_MES { get; set; }
        public string FECHA_ULTIMO_PAGO { get; set; }
        public decimal? ULTIMO_PAGO { get; set; }
        public string SUCURSAL_CONTRATO { get; set; }
        public string AD_DESDE { get; set; }
        public string AD_HASTA { get; set; }
        public decimal? Monto { get; set; }
        public decimal? Acuenta { get; set; }
        public decimal? ADEUDO { get; set; }
        public int idESTATUS { get; set; }
        public string ESTATUS { get; set; }
        public int idTIPO_TOMA { get; set; }
        public string TIPO_TOMA { get; set; }

        public int ContadorCuenta { get { return 1; } }
    }
}

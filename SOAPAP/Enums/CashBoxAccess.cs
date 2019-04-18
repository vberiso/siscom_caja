using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Enums
{
    public class CashBoxAccess
    {
        public enum Access
        {
            Admin = -1,
            SinAcceso = 0,  
            Abierta = 1,
            Fondo= 2,
            Cobro =3,  
            Cierre= 5,
            Liquidada = 7,
            SinCierreAnterior = 8,
            SinApertura = 9
        }
    }
}

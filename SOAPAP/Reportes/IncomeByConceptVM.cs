using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Reportes
{
    class IncomeByConceptVM
    {
        //Agua
        public string FOLIO { get; set; }
        public string CUENTA { get; set; }
        public string NOMBRE { get; set; }
        public string RUTA { get; set; }
        public decimal MONTO { get; set; }
        public decimal DCTO { get; set; }
        public decimal AGUA { get; set; }
        public decimal DRENAJE { get; set; }
        public decimal SAN { get; set; }
        public decimal REC { get; set; }
        public decimal NOTIF { get; set; }        
        public decimal OTROS { get; set; }        
        public decimal ANTI { get; set; }
        public decimal SUBTOTAL { get; set; }
        public decimal IVA { get; set; }
        public decimal TOTAL { get; set; }
        public string ESTA { get; set; }
        public string MP { get; set; }
        public string OFICINA { get; set; }
        public string FECHA { get; set; }
        public string CAJERO { get; set; }
        public string BANCO { get; set; }

        //AYUNTAMIENTO
        public decimal PREDIAL { get; set; }
        public decimal LIMPIA { get; set; }
        public decimal MULTAS { get; set; }

    }
}

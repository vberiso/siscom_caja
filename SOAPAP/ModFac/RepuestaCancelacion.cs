using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    //{
    //  "Status": "canceled",
    //  "Message": "Cancelado sin Aceptacion",
    //  "RequestDate": "2018-11-01T12:00:00",
    //  "AcuseXmlBase64": "PENhbmNlbGFjaW9uIHhtbG5zOnhzaT0iaH......W9uPg==",
    //  "CancelationDate": "2018-11-01T12:00:00"
    //}


namespace SOAPAP.ModFac
{
    public class RepuestaCancelacion
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Uuid { get; set; }
        public DateTime RequestDate { get; set; }
        public string AcuseXmlBase64 { get; set; }
        public DateTime CancelationDate { get; set; }
    }
}

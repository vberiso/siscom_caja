using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//{
//        "Status": "canceled",
//        "Uuid": "41A49A5E-7E57-4339-88F9-DEEE9B901B55",
//        "RequestDate": "2018-11-14T10:21:43",
//        "ResponseDate": "2018-11-17T10:21:43",
//        "ExpirationDate": "2018-11-17T10:21:43",
//        "AcuseXmlBase64": "PD94bWwgdmVyc2lvbj0iMS...3VzZT4="
//    }


namespace SOAPAP.ModFac
{
    public class RepuestaCancelacion
    {
        public string Status { get; set; }
        public string Uuid { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ResponseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string AcuseXmlBase64 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOAPAP.Model
{
    [XmlRootAttribute(Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class Comprobante
    {
        [XmlAttributeAttribute()]
        public string Version;

        [XmlAttributeAttribute()]
        public string Folio;

        [XmlAttributeAttribute()]
        public string Fecha;
        
        [XmlAttributeAttribute()]
        public string FormaPago;

        [XmlAttributeAttribute()]
        public string subTotal;

        [XmlAttributeAttribute()]
        public string Moneda;

        [XmlAttributeAttribute()]
        public string Total;

        [XmlAttributeAttribute()]
        public string TipoDeComprobante;

        [XmlAttributeAttribute()]
        public string MetodoDePago;

        [XmlAttributeAttribute()]
        public string LugarExpedicion;

        [XmlElementAttribute()]
        public TEmisor Emisor;

        [XmlElementAttribute()]
        public TReceptor Receptor;

        [XmlArrayItemAttribute("Concepto")]
        public TConcepto[] Conceptos;

        [XmlElementAttribute()]
        public TImpuestos Impuestos;

    }

    public partial class TEmisor
    {

        [XmlAttributeAttribute()]
        public string Rfc;

        [XmlAttributeAttribute()]
        public string Nombre;
        
        [XmlElementAttribute()]
        public string RegimenFiscal;

    }
    
    public partial class TReceptor
    {
        [XmlAttributeAttribute()]
        public string Rfc;

        [XmlAttributeAttribute()]
        public string Nombre;

        [XmlElementAttribute()]
        public string UsoCFDI;
    }

    

    public partial class TConcepto
    {
        [XmlAttributeAttribute()]
        public string cantidad;

        [XmlAttributeAttribute()]
        public string unidad;

        [XmlAttributeAttribute()]
        public string descripcion;

        [XmlAttributeAttribute()]
        public string valorUnitario;

        [XmlAttributeAttribute()]
        public string importe;
    }

    public partial class TImpuestos
    {
        [XmlArrayItemAttribute("Traslado")]
        public TTraslado[] Traslados;
    }

    public partial class TTraslado
    {
        [XmlAttributeAttribute()]
        public string impuesto;

        [XmlAttributeAttribute()]
        public string tasa;

        [XmlAttributeAttribute()]
        public string importe;

    }

}

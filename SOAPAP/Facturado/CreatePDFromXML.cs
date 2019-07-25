﻿using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SOAPAP.Facturado
{

    // NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/cfd/3", IsNullable = false)]
    public partial class Comprobante
    {

        private ComprobanteEmisor emisorField;

        private ComprobanteReceptor receptorField;

        private ComprobanteConcepto[] conceptosField;

        private ComprobanteImpuestos impuestosField;

        private ComprobanteComplemento complementoField;

        private decimal versionField;

        private string serieField;

        private uint folioField;

        private System.DateTime fechaField;

        private string selloField;

        private byte formaPagoField;

        private ulong noCertificadoField;

        private string certificadoField;

        private string condicionesDePagoField;

        private decimal subTotalField;

        private byte descuentoField;

        private string monedaField;

        private decimal totalField;

        private string tipoDeComprobanteField;

        private string metodoPagoField;

        private uint lugarExpedicionField;

        /// <remarks/>
        public ComprobanteEmisor Emisor
        {
            get
            {
                return this.emisorField;
            }
            set
            {
                this.emisorField = value;
            }
        }

        /// <remarks/>
        public ComprobanteReceptor Receptor
        {
            get
            {
                return this.receptorField;
            }
            set
            {
                this.receptorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Concepto", IsNullable = false)]
        public ComprobanteConcepto[] Conceptos
        {
            get
            {
                return this.conceptosField;
            }
            set
            {
                this.conceptosField = value;
            }
        }

        /// <remarks/>
        public ComprobanteImpuestos Impuestos
        {
            get
            {
                return this.impuestosField;
            }
            set
            {
                this.impuestosField = value;
            }
        }

        /// <remarks/>
        public ComprobanteComplemento Complemento
        {
            get
            {
                return this.complementoField;
            }
            set
            {
                this.complementoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Serie
        {
            get
            {
                return this.serieField;
            }
            set
            {
                this.serieField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint Folio
        {
            get
            {
                return this.folioField;
            }
            set
            {
                this.folioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime Fecha
        {
            get
            {
                return this.fechaField;
            }
            set
            {
                this.fechaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Sello
        {
            get
            {
                return this.selloField;
            }
            set
            {
                this.selloField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte FormaPago
        {
            get
            {
                return this.formaPagoField;
            }
            set
            {
                this.formaPagoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong NoCertificado
        {
            get
            {
                return this.noCertificadoField;
            }
            set
            {
                this.noCertificadoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Certificado
        {
            get
            {
                return this.certificadoField;
            }
            set
            {
                this.certificadoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CondicionesDePago
        {
            get
            {
                return this.condicionesDePagoField;
            }
            set
            {
                this.condicionesDePagoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal SubTotal
        {
            get
            {
                return this.subTotalField;
            }
            set
            {
                this.subTotalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Descuento
        {
            get
            {
                return this.descuentoField;
            }
            set
            {
                this.descuentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Moneda
        {
            get
            {
                return this.monedaField;
            }
            set
            {
                this.monedaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoDeComprobante
        {
            get
            {
                return this.tipoDeComprobanteField;
            }
            set
            {
                this.tipoDeComprobanteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MetodoPago
        {
            get
            {
                return this.metodoPagoField;
            }
            set
            {
                this.metodoPagoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint LugarExpedicion
        {
            get
            {
                return this.lugarExpedicionField;
            }
            set
            {
                this.lugarExpedicionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteEmisor
    {

        private string rfcField;

        private string nombreField;

        private ushort regimenFiscalField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort RegimenFiscal
        {
            get
            {
                return this.regimenFiscalField;
            }
            set
            {
                this.regimenFiscalField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteReceptor
    {

        private string rfcField;

        private string nombreField;

        private string usoCFDIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UsoCFDI
        {
            get
            {
                return this.usoCFDIField;
            }
            set
            {
                this.usoCFDIField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteConcepto
    {

        private ComprobanteConceptoImpuestos impuestosField;

        private uint claveProdServField;

        private string noIdentificacionField;

        private byte cantidadField;

        private string claveUnidadField;

        private string unidadField;

        private string descripcionField;

        private decimal valorUnitarioField;

        private decimal importeField;

        private byte descuentoField;

        /// <remarks/>
        public ComprobanteConceptoImpuestos Impuestos
        {
            get
            {
                return this.impuestosField;
            }
            set
            {
                this.impuestosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint ClaveProdServ
        {
            get
            {
                return this.claveProdServField;
            }
            set
            {
                this.claveProdServField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NoIdentificacion
        {
            get
            {
                return this.noIdentificacionField;
            }
            set
            {
                this.noIdentificacionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Cantidad
        {
            get
            {
                return this.cantidadField;
            }
            set
            {
                this.cantidadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ClaveUnidad
        {
            get
            {
                return this.claveUnidadField;
            }
            set
            {
                this.claveUnidadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Unidad
        {
            get
            {
                return this.unidadField;
            }
            set
            {
                this.unidadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Descripcion
        {
            get
            {
                return this.descripcionField;
            }
            set
            {
                this.descripcionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ValorUnitario
        {
            get
            {
                return this.valorUnitarioField;
            }
            set
            {
                this.valorUnitarioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Descuento
        {
            get
            {
                return this.descuentoField;
            }
            set
            {
                this.descuentoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteConceptoImpuestos
    {

        private ComprobanteConceptoImpuestosTraslado[] trasladosField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Traslado", IsNullable = false)]
        public ComprobanteConceptoImpuestosTraslado[] Traslados
        {
            get
            {
                return this.trasladosField;
            }
            set
            {
                this.trasladosField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteConceptoImpuestosTraslado
    {

        private decimal baseField;

        private byte impuestoField;

        private string tipoFactorField;

        private decimal tasaOCuotaField;

        private decimal importeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Base
        {
            get
            {
                return this.baseField;
            }
            set
            {
                this.baseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Impuesto
        {
            get
            {
                return this.impuestoField;
            }
            set
            {
                this.impuestoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoFactor
        {
            get
            {
                return this.tipoFactorField;
            }
            set
            {
                this.tipoFactorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TasaOCuota
        {
            get
            {
                return this.tasaOCuotaField;
            }
            set
            {
                this.tasaOCuotaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteImpuestos
    {

        private ComprobanteImpuestosTraslados trasladosField;

        private decimal totalImpuestosTrasladadosField;

        /// <remarks/>
        public ComprobanteImpuestosTraslados Traslados
        {
            get
            {
                return this.trasladosField;
            }
            set
            {
                this.trasladosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TotalImpuestosTrasladados
        {
            get
            {
                return this.totalImpuestosTrasladadosField;
            }
            set
            {
                this.totalImpuestosTrasladadosField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteImpuestosTraslados
    {

        private ComprobanteImpuestosTrasladosTraslado trasladoField;

        /// <remarks/>
        public ComprobanteImpuestosTrasladosTraslado Traslado
        {
            get
            {
                return this.trasladoField;
            }
            set
            {
                this.trasladoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteImpuestosTrasladosTraslado
    {

        private byte impuestoField;

        private string tipoFactorField;

        private decimal tasaOCuotaField;

        private decimal importeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Impuesto
        {
            get
            {
                return this.impuestoField;
            }
            set
            {
                this.impuestoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TipoFactor
        {
            get
            {
                return this.tipoFactorField;
            }
            set
            {
                this.tipoFactorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal TasaOCuota
        {
            get
            {
                return this.tasaOCuotaField;
            }
            set
            {
                this.tasaOCuotaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/cfd/3")]
    public partial class ComprobanteComplemento
    {

        private TimbreFiscalDigital timbreFiscalDigitalField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
        public TimbreFiscalDigital TimbreFiscalDigital
        {
            get
            {
                return this.timbreFiscalDigitalField;
            }
            set
            {
                this.timbreFiscalDigitalField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/TimbreFiscalDigital", IsNullable = false)]
    public partial class TimbreFiscalDigital
    {

        private decimal versionField;

        private string uUIDField;

        private System.DateTime fechaTimbradoField;

        private string rfcProvCertifField;

        private string selloCFDField;

        private ulong noCertificadoSATField;

        private string selloSATField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UUID
        {
            get
            {
                return this.uUIDField;
            }
            set
            {
                this.uUIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime FechaTimbrado
        {
            get
            {
                return this.fechaTimbradoField;
            }
            set
            {
                this.fechaTimbradoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RfcProvCertif
        {
            get
            {
                return this.rfcProvCertifField;
            }
            set
            {
                this.rfcProvCertifField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SelloCFD
        {
            get
            {
                return this.selloCFDField;
            }
            set
            {
                this.selloCFDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong NoCertificadoSAT
        {
            get
            {
                return this.noCertificadoSATField;
            }
            set
            {
                this.noCertificadoSATField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SelloSAT
        {
            get
            {
                return this.selloSATField;
            }
            set
            {
                this.selloSATField = value;
            }
        }
    }


}


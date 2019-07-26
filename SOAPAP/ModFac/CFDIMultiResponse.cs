using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.ModFac
{


    public class Rootobject
    {
        public string Id { get; set; }
        public string CfdiType { get; set; }
        public string Type { get; set; }
        public string Serie { get; set; }
        public string Folio { get; set; }
        public DateTime Date { get; set; }
        public string CertNumber { get; set; }
        public string PaymentTerms { get; set; }
        public string PaymentConditions { get; set; }
        public string PaymentMethod { get; set; }
        public string ExpeditionPlace { get; set; }
        public float ExchangeRate { get; set; }
        public string Currency { get; set; }
        public float Subtotal { get; set; }
        public float Discount { get; set; }
        public float Total { get; set; }
        public string Observations { get; set; }
        public Issuer Issuer { get; set; }
        public Receiver Receiver { get; set; }
        public Item[] Items { get; set; }
        public Tax[] Taxes { get; set; }
        public Complement Complement { get; set; }
        public string Status { get; set; }
        public string OriginalString { get; set; }
    }

    public class Issuer
    {
        public string FiscalRegime { get; set; }
        public string Rfc { get; set; }
        public string TaxName { get; set; }
        public string Phone { get; set; }
    }

    public class Receiver
    {
        public string Rfc { get; set; }
        public string Name { get; set; }
    }

    public class Complement
    {
        public Taxstamp TaxStamp { get; set; }
    }

    public class Taxstamp
    {
        public string Uuid { get; set; }
        public DateTime Date { get; set; }
        public string CfdiSign { get; set; }
        public string SatCertNumber { get; set; }
        public string SatSign { get; set; }
        public string RfcProvCertif { get; set; }
        public string AutNumProvCertif { get; set; }
    }

    public class Item
    {
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public float UnitValue { get; set; }
        public float Total { get; set; }
    }

    public class Tax
    {
        public float Total { get; set; }
        public string Name { get; set; }
        public float Rate { get; set; }
        public string Type { get; set; }
    }

}

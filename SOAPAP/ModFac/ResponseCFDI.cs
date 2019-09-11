using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.ModFac
{
    public class ResponseCFDI : Facturama.Models.Response.Cfdi
    {
        public ResponseCFDI()
        {

        }

        public ResponseCFDI(Facturama.Models.Response.Cfdi pCfdi)
        {

            this.Items = pCfdi.Items;
            this.Receiver = pCfdi.Receiver;
            this.Issuer = pCfdi.Issuer;
            this.Observations = pCfdi.Observations;
            this.Total = pCfdi.Total;
            this.Discount = pCfdi.Discount;
            this.Subtotal = pCfdi.Subtotal;
            this.Currency = pCfdi.Currency;
            this.ExchangeRate = pCfdi.ExchangeRate;
            this.Taxes = pCfdi.Taxes;
            this.ExpeditionPlace = pCfdi.ExpeditionPlace;
            this.PaymentMethod = pCfdi.PaymentMethod;
            this.PaymentConditions = pCfdi.PaymentConditions;
            this.PaymentTerms = pCfdi.PaymentTerms;
            this.CertNumber = pCfdi.CertNumber;
            this.Date = pCfdi.Date;
            this.Folio = pCfdi.Folio;
            this.Serie = pCfdi.Serie;
            this.CfdiType = pCfdi.CfdiType;
            this.Id = pCfdi.Id;
            this.PaymentAccountNumber = pCfdi.PaymentAccountNumber;
            this.Complement = pCfdi.Complement;
            this.Status = "active";
        }

        [JsonProperty("Status")]
        public string Status { get; set; }
    }
}

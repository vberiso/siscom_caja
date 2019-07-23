using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Configuration
    {
        public string RFC { get; set; }
        public string ANSII { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string IVA { get; set; }
        public bool Anual { get; set; }
        public string LegendRegime { get; set; }
        public bool IsMunicipal { get; set; }
        public string DefaultPrinter { get; set; }
        public Int16 StateOperation { get; set; }
        public Terminal Terminal { get; set; }
        public string minimumsalary { get; set; }
        public string CFDI { get; set; }
        public string CFDITest { get; set; }
        public string CFDIUser { get; set; }
        public string CFDIPassword { get; set; }
        public string CFDIKeyCancel{ get; set; }
        public string CFDICertificado { get; set; }
        public bool CFDIFacturacionPasada { get; set; }
        public string StringURLFirebase { get; set; }
        public List<DiscountCampaign> DiscountCampaigns { get; set; }
        public decimal Percentage { get; set; }
        public VersionApp VersionApp { get; set; }
    }
}
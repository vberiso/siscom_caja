using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model.Reports
{
    public class InfoReport
    {
        [JsonProperty("signatures")]
        public List<SignaturesReport> signatures { get; set; }
    }
}

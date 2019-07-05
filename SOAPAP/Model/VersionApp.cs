using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class VersionApp
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public bool IsActive { get; set; }
        public string Platform { get; set; }
        public DateTime PublishDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class LogginLog
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Terminal { get; set; }
        public string User { get; set; }
        public string VersionSiscom { get; set; }
        public string BranchOffice { get; set; }
        public string UserName { get; set; }
    }
}

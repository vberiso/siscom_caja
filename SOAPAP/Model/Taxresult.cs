using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Taxresult
    {
        public int id { get; set; }
        public string folio { get; set; }
        public DateTime dateOrder { get; set; }
        public decimal amount { get; set; }
        public decimal onAccount { get; set; }
        public int year { set; get; }
        public int period { set; get; }
        public string type { set; get; }
        public string descriptionType { get; set; }
        public string status { get; set; }
        public string descriptionStatus { get; set; }
        public string observation { get; set; }
        public int idOrigin { get; set; }
        public DateTime expirationDate { get; set; }
        public int divisionId { get; set; }
        public int taxUserId { get; set; }
        public Model.TaxUser TaxUser { get; set; }


    }
}

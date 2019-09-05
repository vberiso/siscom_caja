using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class CatalogueVM
    {
        public string Id { get; set; }

        public string Value { get; set; }

        public int GroupCatalogueId { get; set; }

        public GroupCataloguesVM GroupCatalogue { get; set; }
    }
}

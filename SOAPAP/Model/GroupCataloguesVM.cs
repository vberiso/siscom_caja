using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class GroupCataloguesVM
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public List<CatalogueVM> Catalogues { get; set; }
    }
}

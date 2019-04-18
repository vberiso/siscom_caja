using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class BreachArticle
    {
        public BreachArticle()
        {
            BreachLists = new HashSet<BreachList>();
        }
       
        public int Id { get; set; }
        public string Article { get; set; }
        public bool IsActive { get; set; }

        public ICollection<BreachList> BreachLists { get; set; }
    }
}

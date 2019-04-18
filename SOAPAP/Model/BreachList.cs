using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class BreachList
    {
        public BreachList()
        {
            BreachDetails = new HashSet<BreachDetail>();
        }

        public int Id { get; set; }
        public string Fraction { get; set; }
        public string Description { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public bool HaveBonification { get; set; }
        public bool IsActive { get; set; }

        public int BreachArticleId { get; set; }
        public BreachArticle BreachArticle { get; set; }

        public ICollection<BreachDetail> BreachDetails { get; set; }
    }
}

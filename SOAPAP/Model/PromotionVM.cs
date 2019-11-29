using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class PromotionVM
    {


    
        public int Id { get; set; }
      
        public string description { get; set; }


       
        public bool IsActive { get; set; }

        
        public int PromotionGroupId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class CondonationCampaing
    {
        public int Id { get; set; }        
        public string Name { get; set; }        
        public string Alias { get; set; }        
        public string Types { get; set; }        
        public string Codes { get; set; }        
        public Int16 Percentage { get; set; }       
        public bool IsActive { get; set; }       
        public DateTime EndDate { get; set; }        
        public DateTime StartDate { get; set; }
        public DateTime CondonationFrom { get; set; }        
        public DateTime CondonationUntil { get; set; }
    }
}

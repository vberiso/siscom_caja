using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class Breach
    {
        public Breach()
        {
            BreachWarranties = new HashSet<BreachWarranty>();
            BreachDetails = new HashSet<BreachDetail>();
        }
       
        public int Id { get; set; }
        public string Series { get; set; }
        public string Folio { get; set; }
        public DateTime CaptureDate { get; set; }
        public string Place { get; set; }
        public string Sector { get; set; }
        public string Zone { get; set; }
        public string Car { get; set; }
        public string TypeCar { get; set; }
        public string Service { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public string Reason { get; set; }
        public decimal Judge { get; set; }
        public DateTime DateBreach { get; set; }
        public string Status { get; set; }
        public int AssignmentTicketId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int TaxUserId { get; set; }
        public TaxUser TaxUser { get; set; }

        public ICollection<BreachWarranty> BreachWarranties { get; set; }
        public ICollection<BreachDetail> BreachDetails { get; set; }
    }
}

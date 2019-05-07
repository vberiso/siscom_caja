using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class DiscountAuthorization
    {

        public DiscountAuthorization()
        {
            DiscountAuthorizationDetails = new HashSet<DiscountAuthorizationDetail>();
        }

        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime AuthorizationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public decimal AmountDiscount { get; set; }
        public Int16 DiscountPercentage { get; set; }
        [Required]
        public string Account { get; set; }
        [Required]
        public string Folio { get; set; }
        public string KeyFirebase { get; set; }
        public int IdOrigin { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Status { get; set; }
        public string Observation { get; set; }
        public string ObservationResponse { get; set; }
        [Required]
        public string BranchOffice { get; set; }
        public string UserAuthorizationId { get; set; }
        [Required]
        public string UserRequestId { get; set; }
        public string FileName { get; set; }
        public string NameUserResponse { get; set; }
        public ApplicationUser UserRequest { get; set; }
        public ICollection<DiscountAuthorizationDetail> DiscountAuthorizationDetails { get; set; }
    }
}

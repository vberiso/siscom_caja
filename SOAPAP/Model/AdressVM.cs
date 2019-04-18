using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPAP.Model
{
    public class AdressVM
    {
        public int Id { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Outdoor { get; set; }
        public string Indoor { get; set; }
        [Required]
        public string Zip { get; set; }
        public string Reference { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        [ Required]
        public string TypeAddress { get; set; }
        [Required]
        public int SuburbsId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }

    public class UpdateAddress
    {
        public UpdateAddress()
        {
            Addresses = new List<AdressVM>();
        }

        public List<AdressVM> Addresses { get; set; }
    }
}

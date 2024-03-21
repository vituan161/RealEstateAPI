using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Models
{
    public class Seller
    {
        public int id { get; set; }

        [ForeignKey(nameof(AppUser))]
        public int UserId { get; set; }
        public AppUser User { get;set; }

        [ForeignKey(nameof(AgencyCompany))]
        public int? AgencyCompanyId { get; set; }
        public AgencyCompany AgencyCompany { get; set; }
        public ICollection<RealEstate> RealEstates { get; set; }
        public ICollection<Plan> Plans { get; set; }
    }
}

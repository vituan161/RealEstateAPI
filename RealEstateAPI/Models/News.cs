using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Models
{
    public class News
    {
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public IList<string> Imageurl { get; set; }
        [Required]
        public IList<string> Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [ForeignKey(nameof(AgencyCompany))]
        public int AgencyCompanyId { get; set; }
        public AgencyCompany AgencyCompany { get; set; }
    }
}

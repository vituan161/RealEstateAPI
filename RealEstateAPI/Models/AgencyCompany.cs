using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Models
{
    public class AgencyCompany
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Link { get; set; }
        public IList<string> Imageurl { get; set; }
        [Display(Name = "Phone number")]
        [RegularExpression("[0-9]{10}")]
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public ICollection<News> News { get; set; }
    }
}

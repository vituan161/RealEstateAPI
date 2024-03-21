using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Models
{
    public class Profile
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Address { get; set; }
        public DateOnly DoB { get; set; }
        public IList<string> ImageURL { get; set; }
        [Display(Name = "Phone number")]
        [RegularExpression("[0-9]{10}")]
        public string Phone { get; set; }
        public double Rating { get; set; }
        public ICollection<UserProfile> Followers { get; set; }
        public string IdentiticationNumber { get; set; }
        public string Description { get; set; }
        public AppUser AppUser { get; set; }
    }
}

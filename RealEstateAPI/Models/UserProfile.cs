using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Models
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Models
{
    public class AppUser:IdentityUser<int>
    {
        public Boolean IsOfficial { get; set; }
        [ForeignKey(nameof(Profile))]
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public ICollection<UserProfile> FollowedProfiles { get; set; }

    }
}

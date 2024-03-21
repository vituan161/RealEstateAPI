using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Models
{
    public class Plan
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Area { get; set; }
        public string Address { get; set; }
        public string Link { get; set; }
        public IList<string> Design { get; set; }
        public string Legality { get; set; }
        public IList<string> Imageurl { get; set; }
        public IList<string> Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public IList<string> Progress { get; set; }
        public ICollection<RealEstate> realEstates { get; set; }

        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public Seller Seller { get; set; }


    }
}

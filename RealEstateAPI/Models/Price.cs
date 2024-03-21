using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Models
{
    public class Price
    {
        public int id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceValue { get; set; }
        public DateOnly DateCreated { get; set; }

        [ForeignKey(nameof(RealEstate))]
        public int RealEstateId { get; set; }
        public RealEstate RealEstate { get; set; }
    }
}

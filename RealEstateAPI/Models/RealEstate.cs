using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Models
{
    public class RealEstate
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Link { get; set; }
        public IList<string> Imageurl { get; set; }
        public IList<string> Description { get; set; }
        public IList<string> Design { get; set; }
        public string Legality { get; set; }
        public RealEstateType Type { get; set; }
        public DateOnly DateCreated { get; set; }
        public DateOnly DateExprired { get; set; }
        public string Status { get; set; }
        public ICollection<Price> Prices { get; set; }

        [ForeignKey(nameof(Plan))]
        public int? PlanId { get; set; }
        public Plan Plan { get; set; }

        [ForeignKey(nameof(Seller))]
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
    }

    public enum RealEstateType
    {
        Apartment,
        House,
        Land
    }
}

using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Models
{
    public class RegisterModel
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateOnly DoB { get; set; }
        [RegularExpression("[0-9]{10}")]
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? IdentiticationNumber { get; set; }
    }
}

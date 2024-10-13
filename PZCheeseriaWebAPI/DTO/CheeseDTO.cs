using System.ComponentModel.DataAnnotations;

namespace PZCheeseriaWebAPI.DTO
{
    public class CheeseDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public decimal PricePerKilo { get; set; }
        public string Color { get; set; }
    }
}
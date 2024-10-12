using System.ComponentModel.DataAnnotations;

namespace PZCheeseriaWebAPI.DTO
{
    public class CheeseDTO
    {
        public CheeseDTO(int id, string name, string imageUrl, decimal pricePerKilo, string color)
        {
            this.Id = id;
            this.Name = name;
            this.ImageUrl = imageUrl;
            this.PricePerKilo = pricePerKilo;
            this.Color = color;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public decimal PricePerKilo { get; set; }
        public string Color { get; set; }
    }
}
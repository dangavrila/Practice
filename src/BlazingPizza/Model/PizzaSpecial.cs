using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingPizza
{
    /// <summary>
    /// Represents a pre-configured template for a pizza a user can order
    /// </summary>
    public class PizzaSpecial
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal BasePrice { get; set; }

        public string Description { get; set; }

        public bool Vegetarian { get; set; } = false;

        public bool Vegan { get; set; } = false;

        public string ImageUrl { get; set; }

        public string GetFormattedBasePrice() => BasePrice.ToString("0.00");
    }
}

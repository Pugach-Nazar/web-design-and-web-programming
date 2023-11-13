using System.ComponentModel.DataAnnotations;

namespace lab15.Models
{
    public class Device
    {
        public int Id { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        [Required]
        public int SellerId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        public Category? Category { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public Seller? Seller { get; set; }
    }
}

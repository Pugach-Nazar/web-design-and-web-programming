using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using курсач.Models;

namespace курсач.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        [Remote("IsNameUnique", "Manufacturer", ErrorMessage = "This name is already in use")]
        public string Name { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        public string PhotoPath { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }
    }
}

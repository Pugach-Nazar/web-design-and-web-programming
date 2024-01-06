
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace курсач.Models
{
    public class Manufacturer
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
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        public string Country { get; set; }
        public List<Product>? Products { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace курсач.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        //[Remote("IsNameUnique", "Category", ErrorMessage = "This name is already in use")]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}

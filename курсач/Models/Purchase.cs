using System.ComponentModel.DataAnnotations;

namespace курсач.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public DateTime DateTime { get; set; }
        public Product? Product { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace lab15.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Device>? Devices { get; set; }
    }
}

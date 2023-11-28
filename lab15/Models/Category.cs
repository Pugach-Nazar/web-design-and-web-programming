using lab15.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace lab15.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Device>? Devices { get; set; }

        public Category() { }

        public Category(CategoryViewModel categoryViewModel) 
        {
            Id = categoryViewModel.Id;

            Name = categoryViewModel.Name;
        }
    }
}

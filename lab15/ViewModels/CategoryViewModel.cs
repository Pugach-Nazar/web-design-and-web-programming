using lab15.Models;
using System.ComponentModel.DataAnnotations;

namespace lab15.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        public string Name { get; set; }

        public List<DeviceViewModel>? DevicesView { get; set; }

        public CategoryViewModel() { }
        public CategoryViewModel(Category category)
        {
            Id = category.Id;

            Name = category.Name;

            if(category.Devices != null)
            {
                DevicesView = new List<DeviceViewModel>();
                foreach (var device in category.Devices)
                {
                    DevicesView.Add(new DeviceViewModel(device));
                }
            }
        }
    }
}

using lab15.Models;
using System.ComponentModel.DataAnnotations;

namespace lab15.ViewModels
{
    public class ManufacturerViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        public string Description { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        public string Country { get; set; }
        public List<DeviceViewModel>? DevicesView { get; set; }

        public ManufacturerViewModel() { }
        public ManufacturerViewModel(Manufacturer manufacturer)
        {
            Id = manufacturer.Id;
            Name = manufacturer.Name;
            Description = manufacturer.Description;
            Country = manufacturer.Country;

            if (manufacturer.Devices != null)
            {
                DevicesView = new List<DeviceViewModel>();
                foreach (var device in manufacturer.Devices)
                {
                    DevicesView.Add(new DeviceViewModel(device));
                }
                
            }
        }
    }
}

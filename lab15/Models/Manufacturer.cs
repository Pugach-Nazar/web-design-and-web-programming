using lab15.ViewModels;

namespace lab15.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public List<Device>? Devices { get; set; }
        public Manufacturer() { }
        public Manufacturer(ManufacturerViewModel manufacturerViewModel)
        {
            Id = manufacturerViewModel.Id;
            Name = manufacturerViewModel.Name;
            Description = manufacturerViewModel.Description;
            Country = manufacturerViewModel.Country;

        }

        public void UpdateFromViewModel(ManufacturerViewModel manufacturerViewModel)
        {
            Id = manufacturerViewModel.Id;
            Name = manufacturerViewModel.Name;
            Description = manufacturerViewModel.Description;
            Country = manufacturerViewModel.Country;
        }
    }
}

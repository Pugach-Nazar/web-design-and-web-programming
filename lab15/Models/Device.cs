using lab15.ViewModels;

namespace lab15.Models
{
    public class Device
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount {  get; set; }

        public Category? Category { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public Seller? Seller { get; set; }


        public Device() { }
        public Device(DeviceViewModel deviceViewModel)
        {
            Id = deviceViewModel.Id;
            ManufacturerId = deviceViewModel.ManufacturerId;
            SellerId = deviceViewModel.SellerId;
            CategoryId = deviceViewModel.CategoryId;
            Name = deviceViewModel.Name;
            Description = deviceViewModel.Description;
            Price = deviceViewModel.Price;
            Amount = deviceViewModel.Amount;
        }
        public void UpdateFromViewModel(DeviceViewModel deviceViewModel)
        {
            Id = deviceViewModel.Id;
            ManufacturerId = deviceViewModel.ManufacturerId;
            SellerId = deviceViewModel.SellerId;
            CategoryId = deviceViewModel.CategoryId;
            Name = deviceViewModel.Name;
            Description = deviceViewModel.Description;
            Price = deviceViewModel.Price;
            Amount = deviceViewModel.Amount;
        }
    }
}

using lab15.Models;
using System.ComponentModel.DataAnnotations;

namespace lab15.ViewModels
{
    public class DeviceViewModel
    {
        public int Id { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        [Required]
        public int SellerId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(0,int.MaxValue, ErrorMessage = "Amount must 0 or greater than 0")]
        public int Amount { get; set; }

        public string? Category { get; set; }
        public string? Manufacturer { get; set; }
        public string? Seller { get; set; }

        public DeviceViewModel() { }
        public DeviceViewModel(Device device)
        {
            Id = device.Id;
            ManufacturerId = device.ManufacturerId;
            SellerId = device.SellerId;
            CategoryId = device.CategoryId;
            Name = device.Name;
            Description = device.Description;
            Price = device.Price;
            Amount = device.Amount;

            Category = device.Category != null ? device.Category.Name : "Not found";

            Manufacturer = device.Manufacturer != null ? device.Manufacturer.Name : "Not found";

            Seller = device.Seller != null ? device.Seller.Name : "Not found";
        }
    }
}
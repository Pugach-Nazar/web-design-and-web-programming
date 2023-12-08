using lab15.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace lab15.ViewModels
{
    public class SellerViewModel
    {
        public int Id { get; set; }
        [Required]
        [Remote("IsNameUnique", "Seller", ErrorMessage = "This name is already in use")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        public string Description { get; set; }

        public List<DeviceViewModel>? DevicesView { get; set; }

        public SellerViewModel() { }
        public SellerViewModel(Seller seller)
        {
            Id = seller.Id;
            Name = seller.Name;
            Description = seller.Description;

            if (seller.Devices != null)
            {
                DevicesView = new List<DeviceViewModel>();
                foreach (var device in seller.Devices)
                {
                    DevicesView.Add(new DeviceViewModel(device));
                }

            }
        }
    }
}

using lab15.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace lab15.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        
        public List<Device>? Devices { get; set; }

        public Seller() { }
        public Seller(SellerViewModel sellerViewModel)
        {
            Id = sellerViewModel.Id;
            Name = sellerViewModel.Name;
            Description = sellerViewModel.Description;
        }

        public void UpdateFromViewModel(SellerViewModel sellerViewModel)
        {
            Id = sellerViewModel.Id;
            Name = sellerViewModel.Name;
            Description = sellerViewModel.Description;
        }
    }
}

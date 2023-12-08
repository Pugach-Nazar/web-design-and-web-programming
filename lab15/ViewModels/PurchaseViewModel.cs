using lab15.Models;
using System.ComponentModel.DataAnnotations;

namespace lab15.ViewModels
{
    public class PurchaseViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be more than 2 characters")]
        public string UserName { get; set; }
        [Required]
        public int DeviceId { get; set; }
        public DateTime DateTime { get; set; }

        public DeviceViewModel? DeviceView { get; set; }

        public PurchaseViewModel() { }
        public PurchaseViewModel(Purchase purchase)
        {
            Id = purchase.Id;
            UserName = purchase.UserName;
            DeviceId = purchase.DeviceId;
            DateTime = purchase.DateTime;

            if (purchase.Device != null)
            {
                DeviceView = new DeviceViewModel(purchase.Device);
            }
        }
    }
}

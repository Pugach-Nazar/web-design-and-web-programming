using lab15.Models;
using System.ComponentModel.DataAnnotations;

namespace lab15.ViewModels
{
    public class PurchaseViewModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
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

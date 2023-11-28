using lab15.ViewModels;

namespace lab15.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int DeviceId { get; set;}
        public DateTime DateTime { get; set; }
        public Device Device { get; set; }

        public Purchase() { }

        public Purchase(PurchaseViewModel purchaseViewModel) 
        {
            Id = purchaseViewModel.Id;
            UserName = purchaseViewModel.UserName;
            DeviceId = purchaseViewModel.DeviceId;
            DateTime = purchaseViewModel.DateTime;

            if(purchaseViewModel.DeviceView != null)
            {
                Device = new Device(purchaseViewModel.DeviceView);
            }
        }
    }
}

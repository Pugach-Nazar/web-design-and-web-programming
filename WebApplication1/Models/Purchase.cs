namespace WebApplication1.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int DeviceId { get; set; }
        public DateTime Date { get; set; }
    }
}

namespace lab15.Models
{
    public class Device
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public int SellerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price {  get; set; }

        public Manufacturer Manufacturer { get; set; }
        public Seller Seller { get; set; }
    }
}

namespace lab15.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }

        public List<Device> Devices { get; set; }
    }
}

namespace ConsoleApp1.Core
{
    public class Package
    {
        public int Id { get; set; }
        public string Destination { get; set; } = string.Empty;
        public double Weight { get; set; }
        public double Cost { get; set; }
        public string Status { get; set; } = "Pending";
        
        public Package() { }
        
        public Package(int id, string destination, double weight, double cost)
        {
            Id = id;
            Destination = destination;
            Weight = weight;
            Cost = cost;
        }
        
        public override string ToString()
        {
            return $"Package {Id}: {Destination} | Weight: {Weight}kg | Cost: ${Cost}";
        }
    }
}

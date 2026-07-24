namespace ConsoleApp1.Core
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public double Capacity { get; set; }
        public double CurrentLoad { get; set; }
        public string Status { get; set; } = "Available";
        
        public Vehicle() { }
        
        public Vehicle(int id, string model, double capacity)
        {
            Id = id;
            Model = model;
            Capacity = capacity;
            CurrentLoad = 0;
        }
        
        public bool CanAddPackage(double weight)
        {
            return CurrentLoad + weight <= Capacity;
        }
        
        public void AddPackage(double weight)
        {
            if (CanAddPackage(weight))
                CurrentLoad += weight;
        }
        
        public override string ToString()
        {
            return $"Vehicle {Id}: {Model} | Capacity: {Capacity}kg | Load: {CurrentLoad}kg";
        }
    }
}

using System;

namespace EcoDrive.Core
{
    public class Truck : Vehicle
    {
        public int AxleCount { get; private set; }
        public double MaintenanceCostPerKm { get; private set; }

        public Truck(string vehicleId, double maxCapacity = 5000, int axleCount = 2)
            : base(vehicleId, $"Truck-{axleCount}Axle", maxCapacity, fuelCost: 0.25)
        {
            AxleCount = axleCount;
            MaintenanceCostPerKm = 0.05;
        }

        public override double CalculateShippingCost(double distance)
        {
            double baseCost = distance * FuelCostPerKm;
            double maintenanceCost = distance * MaintenanceCostPerKm;
            double loadCost = (CurrentLoadKg / MaxCapacityKg) * 10;
            return baseCost + maintenanceCost + loadCost;
        }

        public override double CalculateShippingCost(double distance, DeliveryPriority priority)
        {
            double baseCost = CalculateShippingCost(distance);
            double priorityMultiplier = priority switch
            {
                DeliveryPriority.Standard => 1.0,
                DeliveryPriority.Express => 1.5,
                DeliveryPriority.NextDay => 1.25,
                _ => 1.0
            };
            return baseCost * priorityMultiplier;
        }

        public override double CalculateShippingCost(double distance, DeliveryPriority priority, WeatherCondition weather)
        {
            double baseCost = CalculateShippingCost(distance, priority);
            double weatherMultiplier = weather switch
            {
                WeatherCondition.Clear => 1.0,
                WeatherCondition.Rainy => 1.1,
                WeatherCondition.Snowy => 1.35,
                WeatherCondition.Stormy => 1.6,
                _ => 1.0
            };
            return baseCost * weatherMultiplier;
        }
    }
}

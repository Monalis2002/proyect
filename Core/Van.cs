using System;

namespace EcoDrive.Core
{
    public class Van : Vehicle
    {
        public double BatteryCapacityKwh { get; private set; }
        public bool IsElectric { get; private set; }

        public Van(string vehicleId, double maxCapacity = 1500, bool isElectric = true)
            : base(vehicleId, isElectric ? "Electric-Van" : "Fuel-Van", maxCapacity, 
                fuelCost: isElectric ? 0.08 : 0.15)
        {
            IsElectric = isElectric;
            BatteryCapacityKwh = isElectric ? 100 : 0;
        }

        public override double CalculateShippingCost(double distance)
        {
            double baseCost = distance * FuelCostPerKm;
            double loadCost = (CurrentLoadKg / MaxCapacityKg) * 5;
            
            if (IsElectric)
            {
                double chargingCost = distance / 5.0;
                return baseCost + loadCost + chargingCost;
            }
            return baseCost + loadCost;
        }

        public override double CalculateShippingCost(double distance, DeliveryPriority priority)
        {
            double baseCost = CalculateShippingCost(distance);
            double priorityMultiplier = priority switch
            {
                DeliveryPriority.Standard => 1.0,
                DeliveryPriority.Express => 1.4,
                DeliveryPriority.NextDay => 1.1,
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
                WeatherCondition.Rainy => 1.05,
                WeatherCondition.Snowy => 1.2,
                WeatherCondition.Stormy => 1.4,
                _ => 1.0
            };

            if (IsElectric)
                weatherMultiplier = Math.Min(weatherMultiplier, 1.15);

            return baseCost * weatherMultiplier;
        }
    }
}

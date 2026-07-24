using System;
using System.Collections.Generic;

namespace EcoDrive.Core
{
    public abstract class Vehicle
    {
        public string VehicleId { get; protected set; }
        public string Name { get; protected set; }
        public double MaxCapacityKg { get; protected set; }
        public double CurrentLoadKg { get; private set; }
        public double FuelCostPerKm { get; protected set; }
        public VehicleStatus Status { get; set; }
        public List<Package> Packages { get; private set; }

        protected Vehicle(string vehicleId, string name, double maxCapacity, double fuelCost)
        {
            VehicleId = vehicleId;
            Name = name;
            MaxCapacityKg = maxCapacity;
            FuelCostPerKm = fuelCost;
            CurrentLoadKg = 0;
            Status = VehicleStatus.Idle;
            Packages = new List<Package>();
        }

        public bool AddPackage(Package package)
        {
            if (CurrentLoadKg + package.WeightKg <= MaxCapacityKg)
            {
                Packages.Add(package);
                CurrentLoadKg += package.WeightKg;
                return true;
            }
            return false;
        }

        public bool RemovePackage(Package package)
        {
            if (Packages.Remove(package))
            {
                CurrentLoadKg -= package.WeightKg;
                return true;
            }
            return false;
        }

        public abstract double CalculateShippingCost(double distance);
        public abstract double CalculateShippingCost(double distance, DeliveryPriority priority);
        public abstract double CalculateShippingCost(double distance, DeliveryPriority priority, WeatherCondition weather);

        public override string ToString()
        {
            return $"{Name} (ID: {VehicleId}) - Load: {CurrentLoadKg:F2}/{MaxCapacityKg:F2} kg - Status: {Status}";
        }
    }

    public enum VehicleStatus
    {
        Idle,
        InTransit,
        Loading,
        Unloading,
        Maintenance
    }
}

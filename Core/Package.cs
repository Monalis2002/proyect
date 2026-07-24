using System;

namespace EcoDrive.Core
{
    public abstract class Package
    {
        public string PackageId { get; protected set; }
        public string Description { get; protected set; }
        public double WeightKg { get; protected set; }
        public string OriginCity { get; protected set; }
        public string DestinationCity { get; protected set; }
        public PackageStatus Status { get; set; }
        public DateTime CreatedDate { get; protected set; }

        protected Package(string packageId, double weightKg, string origin, string destination, string description)
        {
            PackageId = packageId;
            WeightKg = weightKg;
            OriginCity = origin;
            DestinationCity = destination;
            Description = description;
            Status = PackageStatus.Pending;
            CreatedDate = DateTime.Now;
        }

        public abstract double CalculateBaseCost();

        public override string ToString()
        {
            return $"Package {PackageId}: {WeightKg}kg - {OriginCity} → {DestinationCity} - Status: {Status}";
        }
    }

    public enum PackageStatus
    {
        Pending,
        InTransit,
        Delivered,
        Failed,
        Returned
    }

    public enum DeliveryPriority
    {
        Standard,
        NextDay,
        Express
    }

    public enum WeatherCondition
    {
        Clear,
        Rainy,
        Snowy,
        Stormy
    }
}

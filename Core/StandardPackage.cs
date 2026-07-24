using System;

namespace EcoDrive.Core
{
    public class StandardPackage : Package
    {
        public const double BaseCostPerKg = 1.5;

        public StandardPackage(string packageId, double weightKg, string origin, string destination)
            : base(packageId, weightKg, origin, destination, "Standard Delivery")
        {
        }

        public override double CalculateBaseCost()
        {
            return WeightKg * BaseCostPerKg;
        }
    }
}

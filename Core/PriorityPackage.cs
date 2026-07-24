using System;

namespace EcoDrive.Core
{
    public class PriorityPackage : Package
    {
        public const double BaseCostPerKg = 3.0;
        public bool RequiresRefrigeration { get; private set; }
        public bool RequiresSignature { get; private set; }

        public PriorityPackage(string packageId, double weightKg, string origin, string destination, 
            bool requiresRefrigeration = false, bool requiresSignature = true)
            : base(packageId, weightKg, origin, destination, "Priority/Expedited Delivery")
        {
            RequiresRefrigeration = requiresRefrigeration;
            RequiresSignature = requiresSignature;
        }

        public override double CalculateBaseCost()
        {
            double baseCost = WeightKg * BaseCostPerKg;
            if (RequiresRefrigeration) baseCost *= 1.3;
            if (RequiresSignature) baseCost *= 1.15;
            return baseCost;
        }
    }
}

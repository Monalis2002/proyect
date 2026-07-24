using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using EcoDrive.Core;
using EcoDrive.Data;

namespace EcoDrive.GUI
{
    public class LogisticsManager
    {
        private DynamicLinkedList<Vehicle> vehicles;
        private DynamicLinkedList<Package> packages;
        private LogisticsGraph routeGraph;

        public LogisticsManager()
        {
            vehicles = new DynamicLinkedList<Vehicle>();
            packages = new DynamicLinkedList<Package>();
            routeGraph = new LogisticsGraph();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.AddLast(vehicle);
        }

        public void AddPackage(Package package)
        {
            packages.AddLast(package);
        }

        public void AddRoute(string fromCity, string toCity, double distanceKm)
        {
            routeGraph.AddRoute(fromCity, toCity, distanceKm);
        }

        public List<Vehicle> GetVehicles()
        {
            return vehicles.ToArray().ToList();
        }

        public List<Package> GetPackages()
        {
            return packages.ToArray().ToList();
        }

        public int GetVehiclesCount() => vehicles.Count;

        public int GetPackagesCount() => packages.Count;

        public int GetRoutesCount() => routeGraph.GetRouteCount();

        public void OptimizePackageDistribution()
        {
            var vehicleArray = vehicles.ToArray();
            var packageArray = packages.ToArray();

            if (vehicleArray.Length == 0 || packageArray.Length == 0)
                return;

            foreach (var vehicle in vehicleArray)
            {
                foreach (var package in packageArray)
                {
                    if (package.Status == PackageStatus.Pending)
                    {
                        if (vehicle.AddPackage(package))
                        {
                            package.Status = PackageStatus.InTransit;
                        }
                    }
                }
            }
        }

        public void SortPackagesByWeight()
        {
            var packageArray = packages.ToArray();
            if (packageArray.Length > 1)
            {
                LogisticsAlgorithms.QuickSortByWeight(packageArray, 0, packageArray.Length - 1);
            }
        }

        public double CalculateTotalShippingCosts()
        {
            double totalCost = 0;
            var vehicleArray = vehicles.ToArray();

            foreach (var vehicle in vehicleArray)
            {
                if (vehicle.Packages.Count > 0)
                {
                    double averageDistance = 100;
                    double vehicleCost = LogisticsAlgorithms.CalculateTotalShippingCost(
                        vehicle.Packages.ToArray(), vehicle, averageDistance);
                    totalCost += vehicleCost;
                }
            }

            return totalCost;
        }

        public double FindShortestRoute(string origin, string destination)
        {
            return routeGraph.GetShortestDistance(origin, destination);
        }

        public string GenerateSystemReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("═══════════════════════════════════════════════════════════");
            sb.AppendLine("         EcoDrive-Manager System Report");
            sb.AppendLine("═══════════════════════════════════════════════════════════");
            sb.AppendLine();
            sb.AppendLine("📊 FLEET OVERVIEW:");
            sb.AppendLine($"  Total Vehicles: {vehicles.Count}");
            var vehicleArray = vehicles.ToArray();
            double totalCapacity = vehicleArray.Sum(v => v.MaxCapacityKg);
            double totalLoad = vehicleArray.Sum(v => v.Packages.Sum(p => p.WeightKg));
            sb.AppendLine($"  Total Capacity: {totalCapacity:F2} kg");
            sb.AppendLine($"  Current Load: {totalLoad:F2} kg");
            sb.AppendLine($"  Utilization: {(totalLoad / totalCapacity * 100):F1}%");
            sb.AppendLine();
            sb.AppendLine("📦 INVENTORY STATUS:");
            sb.AppendLine($"  Total Packages: {packages.Count}");
            var packageArray = packages.ToArray();
            var pendingCount = packageArray.Count(p => p.Status == PackageStatus.Pending);
            var intransitCount = packageArray.Count(p => p.Status == PackageStatus.InTransit);
            var deliveredCount = packageArray.Count(p => p.Status == PackageStatus.Delivered);
            sb.AppendLine($"  Pending: {pendingCount} | In Transit: {intransitCount} | Delivered: {deliveredCount}");
            sb.AppendLine();
            sb.AppendLine("🗺️ ROUTE NETWORK:");
            var cities = routeGraph.GetAllCities();
            sb.AppendLine($"  Total Cities: {cities.Count}");
            sb.AppendLine($"  Total Routes: {routeGraph.GetRouteCount()}");
            sb.AppendLine($"  Cities: {string.Join(", ", cities)}");
            sb.AppendLine();
            sb.AppendLine("💰 COST ANALYSIS:");
            double totalCosts = CalculateTotalShippingCosts();
            sb.AppendLine($"  Total Shipping Cost: ${totalCosts:F2}");
            sb.AppendLine($"  Average Cost per Package: ${(totalCosts / (packages.Count > 0 ? packages.Count : 1)):F2}");
            sb.AppendLine();
            sb.AppendLine("═══════════════════════════════════════════════════════════");

            return sb.ToString();
        }
    }
}

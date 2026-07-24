using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoDrive.Core
{
    public static class LogisticsAlgorithms
    {
        public static void QuickSortByWeight(Package[] packages, int left, int right)
        {
            if (left < right)
            {
                int partition = PartitionByWeight(packages, left, right);
                QuickSortByWeight(packages, left, partition - 1);
                QuickSortByWeight(packages, partition + 1, right);
            }
        }

        private static int PartitionByWeight(Package[] packages, int left, int right)
        {
            double pivot = packages[right].WeightKg;
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (packages[j].WeightKg < pivot)
                {
                    i++;
                    (packages[i], packages[j]) = (packages[j], packages[i]);
                }
            }

            (packages[i + 1], packages[right]) = (packages[right], packages[i + 1]);
            return i + 1;
        }

        public static void MergeSortByPriority(Package[] packages, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSortByPriority(packages, left, mid);
                MergeSortByPriority(packages, mid + 1, right);
                MergeByPriority(packages, left, mid, right);
            }
        }

        private static void MergeByPriority(Package[] packages, int left, int mid, int right)
        {
            int leftSize = mid - left + 1;
            int rightSize = right - mid;
            Package[] leftArray = new Package[leftSize];
            Package[] rightArray = new Package[rightSize];

            Array.Copy(packages, left, leftArray, 0, leftSize);
            Array.Copy(packages, mid + 1, rightArray, 0, rightSize);

            int i = 0, j = 0, k = left;
            while (i < leftSize && j < rightSize)
            {
                if (ComparePriority(leftArray[i], rightArray[j]) <= 0)
                {
                    packages[k++] = leftArray[i++];
                }
                else
                {
                    packages[k++] = rightArray[j++];
                }
            }

            while (i < leftSize)
                packages[k++] = leftArray[i++];
            while (j < rightSize)
                packages[k++] = rightArray[j++];
        }

        private static int ComparePriority(Package a, Package b)
        {
            return a.DestinationCity.Length.CompareTo(b.DestinationCity.Length);
        }

        public static int BinarySearchPackageId(Package[] packages, string packageId, int left, int right)
        {
            if (left > right)
                return -1;

            int mid = (left + right) / 2;
            int comparison = packages[mid].PackageId.CompareTo(packageId);

            if (comparison == 0)
                return mid;
            else if (comparison > 0)
                return BinarySearchPackageId(packages, packageId, left, mid - 1);
            else
                return BinarySearchPackageId(packages, packageId, mid + 1, right);
        }

        public static double CalculateOptimalDistributionCost(List<Package> packages, 
            Vehicle[] vehicles, int packageIndex, double currentCost)
        {
            if (packageIndex >= packages.Count)
                return currentCost;

            double minCost = double.MaxValue;
            Package currentPackage = packages[packageIndex];

            foreach (var vehicle in vehicles)
            {
                if (vehicle.AddPackage(currentPackage))
                {
                    double cost = currentCost + currentPackage.CalculateBaseCost();
                    double recursiveCost = CalculateOptimalDistributionCost(packages, vehicles, 
                        packageIndex + 1, cost);
                    minCost = Math.Min(minCost, recursiveCost);
                    vehicle.RemovePackage(currentPackage);
                }
            }

            return minCost == double.MaxValue ? currentCost : minCost;
        }

        public static double FindShortestDeliveryRoute(string current, string destination,
            Dictionary<string, List<(string, double)>> graph, HashSet<string> visited,
            double currentDistance = 0)
        {
            if (current == destination)
                return currentDistance;

            if (!graph.ContainsKey(current))
                return double.MaxValue;

            visited.Add(current);
            double minDistance = double.MaxValue;

            foreach (var (nextCity, distance) in graph[current])
            {
                if (!visited.Contains(nextCity))
                {
                    double result = FindShortestDeliveryRoute(nextCity, destination, graph,
                        visited, currentDistance + distance);
                    minDistance = Math.Min(minDistance, result);
                }
            }

            visited.Remove(current);
            return minDistance;
        }

        public static double CalculateTotalShippingCost(Package[] packages, Vehicle vehicle,
            double distance, int index = 0)
        {
            if (index >= packages.Length)
                return 0;

            double currentCost = packages[index].CalculateBaseCost() 
                + vehicle.CalculateShippingCost(distance / packages.Length);
            return currentCost + CalculateTotalShippingCost(packages, vehicle, distance, index + 1);
        }
    }
}

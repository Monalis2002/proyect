using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoDrive.Data
{
    public class LogisticsGraph
    {
        private Dictionary<string, List<(string destination, double distance)>> adjacencyList;

        public LogisticsGraph()
        {
            adjacencyList = new Dictionary<string, List<(string, double)>>();
        }

        public void AddCity(string cityName)
        {
            if (!adjacencyList.ContainsKey(cityName))
                adjacencyList[cityName] = new List<(string, double)>();
        }

        public void AddRoute(string fromCity, string toCity, double distanceKm)
        {
            AddCity(fromCity);
            AddCity(toCity);

            if (!adjacencyList[fromCity].Any(r => r.destination == toCity))
                adjacencyList[fromCity].Add((toCity, distanceKm));
        }

        public List<(string, double)>? GetAdjacentCities(string cityName)
        {
            return adjacencyList.ContainsKey(cityName) ? adjacencyList[cityName] : null;
        }

        public double GetShortestDistance(string startCity, string endCity)
        {
            if (!adjacencyList.ContainsKey(startCity) || !adjacencyList.ContainsKey(endCity))
                return -1;

            var distances = new Dictionary<string, double>();
            var queue = new Queue<string>();

            foreach (var city in adjacencyList.Keys)
                distances[city] = double.MaxValue;

            distances[startCity] = 0;
            queue.Enqueue(startCity);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                foreach (var (nextCity, dist) in adjacencyList[current])
                {
                    double newDistance = distances[current] + dist;
                    if (newDistance < distances[nextCity])
                    {
                        distances[nextCity] = newDistance;
                        queue.Enqueue(nextCity);
                    }
                }
            }

            return distances[endCity] == double.MaxValue ? -1 : distances[endCity];
        }

        public List<string> GetAllCities()
        {
            return adjacencyList.Keys.ToList();
        }

        public int GetRouteCount()
        {
            return adjacencyList.Values.Sum(routes => routes.Count);
        }

        public override string ToString()
        {
            return $"LogisticsGraph - Cities: {adjacencyList.Count}, Routes: {GetRouteCount()}";
        }
    }
}

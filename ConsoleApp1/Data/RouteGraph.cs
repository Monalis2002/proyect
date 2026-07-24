using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Data
{
    public class RouteGraph
    {
        private Dictionary<string, List<(string destination, double distance)>> _graph;
        
        public RouteGraph()
        {
            _graph = new Dictionary<string, List<(string, double)>>();
        }
        
        public void AddNode(string location)
        {
            if (!_graph.ContainsKey(location))
                _graph[location] = new List<(string, double)>();
        }
        
        public void AddEdge(string from, string to, double distance)
        {
            if (!_graph.ContainsKey(from))
                AddNode(from);
            if (!_graph.ContainsKey(to))
                AddNode(to);
            
            _graph[from].Add((to, distance));
        }
        
        public double GetDistance(string from, string to)
        {
            if (_graph.ContainsKey(from))
            {
                var edge = _graph[from].FirstOrDefault(e => e.destination == to);
                return edge.distance;
            }
            return 0;
        }
        
        public List<string> GetAdjacentNodes(string node)
        {
            if (_graph.ContainsKey(node))
                return _graph[node].Select(e => e.destination).ToList();
            return new List<string>();
        }
        
        public List<string> FindShortestPath(string start, string end)
        {
            var visited = new HashSet<string>();
            var distances = new Dictionary<string, double>();
            var previous = new Dictionary<string, string>();
            
            foreach (var node in _graph.Keys)
                distances[node] = double.MaxValue;
            distances[start] = 0;
            
            for (int i = 0; i < _graph.Count - 1; i++)
            {
                var current = distances.Where(d => !visited.Contains(d.Key))
                    .OrderBy(d => d.Value).FirstOrDefault().Key;
                
                if (current == null) break;
                visited.Add(current);
                
                foreach (var neighbor in _graph[current])
                {
                    var newDistance = distances[current] + neighbor.distance;
                    if (newDistance < distances[neighbor.destination])
                    {
                        distances[neighbor.destination] = newDistance;
                        previous[neighbor.destination] = current;
                    }
                }
            }
            
            var path = new List<string>();
            string? current_node = end;
            while (current_node != null)
            {
                path.Insert(0, current_node);
                previous.TryGetValue(current_node, out current_node);
            }
            
            return path;
        }
    }
}

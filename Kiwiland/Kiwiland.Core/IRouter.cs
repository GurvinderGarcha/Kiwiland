using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwiland.Core
{
    public interface IRouter
    {
        TotalDistanceResult FindTotalDistance(string inputCities);

        List<string> FindNumberOfRoutesByDistance(string start,int roofDistance);
        List<string> FindTrips(string start, int numofStops, bool lengthCheck);

        int ShortestRoute(string input);
    }

    public class Router : IRouter
    {
        private IInputData _inputData = InstanceFactory.GetInputData();

        public List<string> FindNumberOfRoutesByDistance(string start, int roofDistance)
        {
            int routeCount = 0;
            var route = CheckInput(start);
            var routes = new List<string>();
            var trip = GetRoute(route[0], roofDistance).Distinct<string>();
            foreach (var v in trip)
            {
                if (v[v.Length - 1] == route[1] && v.Length > 1)
                {
                    routeCount += 1;
                    routes.Add(v);
                }
            }
            return routes;
        }

        public TotalDistanceResult FindTotalDistance(string inputCities)
        {
            int sum = 0;
            int inCheck = 0;
            int outCheck = 0;
            var cities = CheckInput(inputCities);
            if (cities.Count <= 1) return new TotalDistanceResult(-1, TotalDistanceResult.Status.InvalidInput);
            for (int x = 0; x < cities.Count - 1; x++)
            {
                foreach (Edge edge in _inputData.NList)
                {
                    if (edge.Start == cities[x] && edge.Last == cities[x + 1])
                    {
                        sum += edge.Distance;
                        inCheck++;
                    }
                }
                outCheck++;
            }
            if (outCheck != inCheck) return new TotalDistanceResult(-1, TotalDistanceResult.Status.NoRouteFound);
            return new TotalDistanceResult(sum, TotalDistanceResult.Status.Success);                 
        }

        public List<string> FindTrips(string start, int numofStops, bool lengthCheck)
        {
            var routeCount = 0;
            var route = CheckInput(start);
            var routes = new List<string>();
            var trip = GetRoute(route[0], numofStops, lengthCheck).Distinct<string>();
            foreach (var v in trip)
            {
                if (v[v.Length - 1] == route[1] && v.Length > 1)
                {
                    routes.Add(v);
                    routeCount += 1;
                }
                    
            }
            return routes;
        }


        public int ShortestRoute(string inputData)
        {
            var input = CheckInput(inputData);
            var start = input[0];
            var last = input[1];
            Dictionary<char, bool> visited = new Dictionary<char, bool>();
            Dictionary<char, int> distance = new Dictionary<char, int>();
            Dictionary<char, bool> visited1 = new Dictionary<char, bool>();
            Dictionary<char, string> parent = new Dictionary<char, string>();
            int i = 0;

            foreach (Edge edge in _inputData.NList)
            {
                if (!distance.ContainsKey(edge.Start))
                    distance.Add(edge.Start, Int32.MaxValue);
                if (!parent.ContainsKey(edge.Start))
                    parent.Add(edge.Start, null);
            }
            distance[start] = 0;
            List<char> keys = Traverse(start, visited);
            visited.Clear();
            while (visited1.Keys.Count < keys.Count)
            {
                List<Edge> list = _inputData.Connections(keys[i]);

                foreach (Edge e in list)
                {
                    if (distance[e.Last] > distance[keys[i]] + e.Distance)
                    {
                        distance[e.Last] = distance[keys[i]] + e.Distance;
                        parent[e.Last] = parent[keys[i]] + "   " + keys[i];
                    }
                }
                if (start == last && distance[start] == 0)
                    distance[start] = Int32.MaxValue;
                if (!visited1.ContainsKey(keys[i]))
                    visited1.Add(keys[i], true);

                i++;
            }
            return distance[last];
        }

        private List<char> Traverse(char v, Dictionary<char, bool> visited)
        {
            if (!visited.ContainsKey(v))
            {
                visited.Add(v, true);
                List<Edge> list = _inputData.Connections(v);
                foreach (Edge e in list)
                {
                    Traverse(e.Last, visited);
                }
            }
            List<char> keys = new List<char>(visited.Count);
            keys.AddRange(visited.Keys.Cast<char>());
            return keys;
        }

        public List<string> GetRoute(char start, int maxDistance)
        {
            List<string> route = new List<string>();

            if (maxDistance <= 0)
            {
                return route;
            }
            IList<Edge> list = _inputData.Connections(start);
            foreach (Edge e in list)
            {
                List<string> edgeList = GetRoute(e.Last, maxDistance - e.Distance);
                foreach (string s in edgeList)
                {
                    route.Add(start.ToString() + " - " + s);
                }
                route.Add(e.Start.ToString());
            }
            return route;
        }

        //to find all the routes
        public List<string> GetRoute(char start, int routeStops, bool lengthCheck)
        {
            List<string> route = new List<string>();

            if (routeStops == 0)
            {
                route.Add(start.ToString());
                return route;
            }
            IList<Edge> list = _inputData.Connections(start);
            foreach (Edge e in list)
            {
                List<string> edgeList = GetRoute(e.Last, routeStops - 1, lengthCheck);
                foreach (string s in edgeList)
                {
                    route.Add(start.ToString() + " - " + s);
                }
                if (lengthCheck == false)
                    route.Add(e.Start.ToString());
            }
            return route;
        }

        private List<char> CheckInput(string s)
        {
            string input = s.ToUpper();                 // convert input to upperCase
            List<char> inputChar = new List<char>();

            foreach (char text in input)                // get rid of spaces and digits;
                if (char.IsLetter(text))
                    inputChar.Add(text);

            return inputChar;
        }
    }
}

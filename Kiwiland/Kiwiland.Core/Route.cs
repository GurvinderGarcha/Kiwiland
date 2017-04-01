//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Kiwiland.Core
//{
//    public class Route
//    {
//        protected Graph GraphRoute { get; set; }
//        Dictionary<char, bool> visited = new Dictionary<char, bool>();

//        public Route(Graph graph)
//        {
//            GraphRoute = graph;
//        }

//        public List<string> getRoute(char start, int routeStops, bool lengthCheck)
//        {
//            List<string> route = new List<string>();

//            if (routeStops == 0)
//            {
//                route.Add(start.ToString());
//                return route;
//            }
//            IList<Edge> list = GraphRoute.Connections(start);
//            foreach (Edge e in list)
//            {
//                List<string> edgeList = getRoute(e.Last, routeStops - 1, lengthCheck);
//                foreach (string s in edgeList)
//                {
//                    route.Add(start.ToString() + " - " + s);
//                }
//                if (lengthCheck == false)
//                    route.Add(e.Start.ToString());
//            }
//            return route;
//        }


//        public void ShortestRoute(char start, char last)
//        {
//            Dictionary<char, int> distance = new Dictionary<char, int>();
//            Dictionary<char, bool> visited1 = new Dictionary<char, bool>();
//            Dictionary<char, string> parent = new Dictionary<char, string>();
//            int i = 0;

//            foreach (Edge edge in GraphRoute.nList)
//            {
//                if (!distance.ContainsKey(edge.Start))
//                    distance.Add(edge.Start, Int32.MaxValue);
//                if (!parent.ContainsKey(edge.Start))
//                    parent.Add(edge.Start, null);
//            }
//            distance[start] = 0;
//            List<char> keys = Traverse(start);
//            visited.Clear();
//            while (visited1.Keys.Count < keys.Count)
//            {
//                List<Edge> list = GraphRoute.Connections(keys[i]);

//                foreach (Edge e in list)
//                {
//                    if (distance[e.Last] > distance[keys[i]] + e.Distance)
//                    {
//                        distance[e.Last] = distance[keys[i]] + e.Distance;
//                        parent[e.Last] = parent[keys[i]] + "   " + keys[i];
//                    }
//                }
//                if (start == last && distance[start] == 0)
//                    distance[start] = Int32.MaxValue;
//                if (!visited1.ContainsKey(keys[i]))
//                    visited1.Add(keys[i], true);

//                i++;
//            }

//            Console.WriteLine("\n\n\t\t " + parent[last] + "   " + last);
//            Console.WriteLine("\n\tShortest Route {0} to {1} is        : {2} ", start, last, distance[last]);
//        }

//        public List<char> Traverse(char v)
//        {
//            if (!visited.ContainsKey(v))
//            {
//                visited.Add(v, true);
//                List<Edge> list = GraphRoute.Connections(v);
//                foreach (Edge e in list)
//                {
//                    Traverse(e.Last);
//                }
//            }
//            List<char> keys = new List<char>(visited.Count);
//            keys.AddRange(visited.Keys.Cast<char>());
//            return keys;
//        }
//    }
//}

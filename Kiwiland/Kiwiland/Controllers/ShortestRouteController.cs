using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Kiwiland.Models;

namespace Kiwiland.Controllers
{
    public class ShortestRouteController : Controller
    {
        FileData fileData = new FileData();
        // GET: ShortestRoute
        public ActionResult ShortRouteInfo()
        {
            return View();
        }

        Dictionary<char, bool> visited = new Dictionary<char, bool>();

        public ActionResult ShortRoute(FormCollection form)
        {
            List<char> route = new List<char>();            
            string pattern = @"^[A-Za-z]{1}$";
            Match match1 = Regex.Match(form["Start"], pattern);
            Match match2 = Regex.Match(form["Desti"], pattern);
            
            if (match1.Success && match2.Success)
            {
                route = new Methods().CheckInput(form["Start"]+form["Desti"]);
                Dictionary<char, int> distance = new Dictionary<char, int>();
                Dictionary<char, bool> visited1 = new Dictionary<char, bool>();
                Dictionary<char, string> parent = new Dictionary<char, string>();
                int i = 0;

                foreach (Edge edge in fileData.nList)
                {
                    if (!distance.ContainsKey(edge.Start))
                        distance.Add(edge.Start, Int32.MaxValue);
                    if (!parent.ContainsKey(edge.Start))
                        parent.Add(edge.Start, null);
                }
                distance[route[0]] = 0;
                Traverse(route[0]);
                List<char> keys = new List<char>(visited.Count);
                keys.AddRange(visited.Keys.Cast<char>());
                visited.Clear();
                while (visited1.Keys.Count < keys.Count)
                {
                    List<Edge> list = new Methods().Connections(keys[i]);

                    foreach (Edge e in list)
                    {
                        if (distance[e.Last] > distance[keys[i]] + e.Distance)
                        {
                            distance[e.Last] = distance[keys[i]] + e.Distance;
                            parent[e.Last] = parent[keys[i]] + "   " + keys[i];
                        }
                    }
                    if (route[0] == route[1] && distance[route[0]] == 0)
                        distance[route[0]] = Int32.MaxValue;
                    if (!visited1.ContainsKey(keys[i]))
                        visited1.Add(keys[i], true);
                    i++;
                }
                ViewBag.Answer = (parent[route[1]] == null) ? "Such Route Does Not Exist" : parent[route[1]] + " " + route[1] + "\n is the Shortest Route from " + route[0] + " to " + route[1] + " with a distance of " + distance[route[1]];
            }
            else
                ViewBag.Answer = "Corrupt Input";
            return View("RouteDistance");
        }
        public void Traverse(char v)
        {
            if (!visited.ContainsKey(v))
            {
                visited.Add(v, true);
                List<Edge> list = new Methods().Connections(v);
                foreach (Edge e in list)
                {
                    Traverse(e.Last);
                }
            }          
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiwiland.Models;

namespace Kiwiland.Controllers
{
    public class DistanceAsRoofController : Controller
    {
        FileData fileData = new FileData();

        // GET: DistanceAsRoof
        public ActionResult GetInfo()
        {
            return View();
        }
        public ActionResult ByDistance(FormCollection form)
        {
            int routeCount = 0;
            List<char> route = new List<char>();
            route = new Methods().CheckInput(form["Start"]);
            var trip = getRoute(route[0], Int32.Parse(form["RoofDistance"])).Distinct<string>();
            foreach (var v in trip)
            {
                if (v[v.Length - 1] == route[1] && v.Length > 1)
                {
                    routeCount += 1;
                }
            }
            ViewBag.Answer = (routeCount > 0)?"There Are "+routeCount.ToString()+" Different Routes with a Distance Less than "+form["RoofDistance"]:"No Such Routes Exist.";
            return View("RouteDistance");
        }
        //check number of route by distance
        public List<string> getRoute(char start, int maxDistance)
        {
            List<string> route = new List<string>();

            if (maxDistance <= 0)
            {
                return route;
            }
            IList<Edge> list = new Methods().Connections(start);
            foreach (Edge e in list)
            {
                List<string> edgeList = getRoute(e.Last, maxDistance - e.Distance);
                foreach (string s in edgeList)
                {
                    route.Add(start.ToString() + " - " + s);
                }
                route.Add(e.Start.ToString());
            }
            return route;
        }
    }
}
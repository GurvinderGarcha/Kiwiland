using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiwiland.Models;

namespace Kiwiland.Controllers
{
    public class NumberOfTripsController : Controller
    {
        // GET: NumberOfTrips
        public ActionResult GetTripInfo()
        {
            return View();
        }
        
        public ActionResult Trips(FormCollection form)
        {
            List<char> route = new List<char>();
            route = new Methods().CheckInput(form["Start"]);
            int routeCount = 0;
            bool NumStop = form["Query1"] == null ? false : true;

            var trip = getRoute(route[0], Int32.Parse(form["NumStops"]), NumStop).Distinct<string>();
            foreach (var v in trip)
            {
                if (v[v.Length - 1] == route[1] && v.Length > 1)
                    routeCount += 1;
            }
            ViewBag.Answer= (routeCount>0)?"Number Of Trips is "+ routeCount.ToString():"No Such Route Exists.";
            
            return View("RouteDistance");
        }
        
        //to find all the routes
        public List<string> getRoute(char start, int routeStops, bool lengthCheck)
        {
            List<string> route = new List<string>();

            if (routeStops == 0)
            {
                route.Add(start.ToString());
                return route;
            }
            IList<Edge> list = new Methods().Connections(start);
            foreach (Edge e in list)
            {
                List<string> edgeList = getRoute(e.Last, routeStops - 1, lengthCheck);
                foreach (string s in edgeList)
                {
                    route.Add(start.ToString() + " - " + s);
                }
                if (lengthCheck == false)
                    route.Add(e.Start.ToString());
            }
            return route;
        }       
    }
}
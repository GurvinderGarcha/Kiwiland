using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Kiwiland.Core;

namespace Kiwiland.Controllers
{
    public class DistanceAsRoofController : Controller
    {
        // GET: DistanceAsRoof
        public ActionResult GetInfo()
        {
            return View();
        }
        public ActionResult ByDistance(FormCollection form)
        {
            
            int routeCount = 0;
            int distance = 0;
            List<char> route = new List<char>();
            var start = form["Start"];
            var roofDistance = form["RoofDistance"];
            if (int.TryParse(roofDistance, out distance))
            {
                var router = InstanceFactory.GetRouter();
                var result = router.FindNumberOfRoutesByDistance(start, distance);
                ViewBag.Answer = (result.Count > 0) ? "There Are " + result.Count.ToString() + " Different Routes with a Distance Less than " + form["RoofDistance"] : "No Such Routes Exist.";
            }
            else
            {
                ViewBag.Answer = string.Format("Invalid RoofDistance {0}", roofDistance);
            }
                        
            return View("RouteDistance");
        }       
    }
}
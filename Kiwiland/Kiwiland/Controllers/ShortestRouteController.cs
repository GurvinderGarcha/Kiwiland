using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Kiwiland.Core;

namespace Kiwiland.Controllers
{
    public class ShortestRouteController : Controller
    {
        // GET: ShortestRoute
        public ActionResult ShortRouteInfo()
        {
            return View();
        }
        
        public ActionResult ShortRoute(FormCollection form)
        {
            List<char> route = new List<char>();            
            string pattern = @"^[A-Za-z]{1}$";
            Match match1 = Regex.Match(form["Start"], pattern);
            Match match2 = Regex.Match(form["Desti"], pattern);
            
            if (match1.Success && match2.Success)
            {
                var router = InstanceFactory.GetRouter();
                var result = router.ShortestRoute(form["Start"] + form["Desti"]);
                //ViewBag.Answer = (parent[route[1]] == null) ? "Such Route Does Not Exist" : parent[route[1]] + " " + route[1] + "\n is the Shortest Route from " + route[0] + " to " + route[1] + " with a distance of " + distance[route[1]];
            }
            else
                ViewBag.Answer = "Corrupt Input";
            return View("RouteDistance");
        }        
    }
}
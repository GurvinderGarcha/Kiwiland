using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiwiland.Core;

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
            var start = form["Start"];                      
            bool NumStop = form["Query1"] == null ? false : true;

            var router = InstanceFactory.GetRouter();
            var result = router.FindTrips(start, Int32.Parse(form["NumStops"]), NumStop);            
            ViewBag.Answer= (result.Count > 0)?"Number Of Trips is "+ result.Count.ToString():"No Such Route Exists.";
            
            return View("RouteDistance");
        }
        
            
    }
}
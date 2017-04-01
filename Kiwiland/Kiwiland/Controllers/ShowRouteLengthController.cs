using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiwiland.Core;

namespace Kiwiland.Controllers
{
    public class ShowRouteLengthController : Controller
    {
        public ActionResult GetInfo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RouteDistance(FormCollection form)
        {
            var inputData = form["Start"];
            if (string.IsNullOrEmpty(inputData) && inputData.Length <= 1)
            {
                ViewBag.Answer = "Please Define the Route in Correct Manner";
                return View();
            }

            var router = InstanceFactory.GetRouter();
            var result = router.FindTotalDistance(inputData);

            if (result.Result == TotalDistanceResult.Status.NoRouteFound) ViewBag.Answer = " ****    No Such Route Found ! *****";
            else if (result.Result == TotalDistanceResult.Status.InvalidInput) ViewBag.Answer = "Please Define the Route in Correct Manner";

            else ViewBag.Answer = string.Format("Total Distance Of Route {0} : {1}", inputData.ToUpper(), result);
            return View();
        }
    }
}
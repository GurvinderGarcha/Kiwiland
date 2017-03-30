using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiwiland.Models;

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
            List<char> route = new List<char>();
            route = new Methods().CheckInput(form["Start"]);
            if (route.Count > 1)
            {
                FileData fileData = new FileData();
                int sum = 0;
                int inCheck = 0;
                int outCheck = 0;
                string answer;

                for (int x = 0; x < route.Count - 1; x++)
                {
                    foreach (Edge edge in fileData.nList)
                    {
                        if (edge.Start == route[x] && edge.Last == route[x + 1])
                        {
                            sum += edge.Distance;
                            inCheck++;
                        }
                    }
                    outCheck++;
                }
                if (outCheck != inCheck)
                    answer = " ****    No Such Route Found ! *****";
                else
                    answer = "Total Distance Of Route " + form["Start"].ToUpper() + " : " + sum;

                ViewBag.Answer = answer;
            }
            else
                ViewBag.Answer = "Please Define the Route in Correct Manner";
            return View();
        }
    }
}
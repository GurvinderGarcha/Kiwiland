using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Kiwiland.Core;

namespace RailRoute
{
    class GetInquiry
    {

        public GetInquiry()
        {
            
        }
        Dictionary<char, bool> visited = new Dictionary<char, bool>();

        public void userInput()
        {
            int i = 0;

            while (i != 9)
            {
                Console.WriteLine("\n=========================   KiwiLand RailRoads   ========================== \n\n");
                Console.WriteLine("To Check the distance between the routes                             1");
                Console.WriteLine("To Check Number of trips between two stations upto maximum Stops     2");
                Console.WriteLine("To Check Number of trips between two stations w/ Exact Stops         3");
                Console.WriteLine("To Check Shortest Route between to Station                           4");
                Console.WriteLine("To Check Different routes by providing distance as roof value        5");
                Console.WriteLine("\n                  To Exit                                            9");
                Console.WriteLine("\nPlease Enter the Start Point and End Point in SE format(e.g AB or ABC).");
                Console.Write("\n************  Please enter Your Choice                 :-   ");


                i = int.Parse(Console.ReadLine());

                if (i == 1)         // to show the distance of the route
                {
                    ShowDistance();
                }

                else if (i == 2)    // to show all routes upto the number of stops
                {
                    ShowRoute(false);       // calls Showroute Function to get input and Show Route passing false to show all routes                    
                }

                else if (i == 3)       // to show routes with Exact number of stops on trip
                {
                    ShowRoute(true);        // calls Showroute Function to get input and Show Route passing true to show routes w/ exact # of stops
                }

                else if (i == 4)// Shortest route
                {                   
                    List<char> input = new List<char>();
                    Console.Write("\n\n\t\tEnter the route  \t: ");                    
                    var router = InstanceFactory.GetRouter();
                    var result = router.ShortestRoute(Console.ReadLine());                
                }

                else if (i == 5)
                {
                    int maxDistance = 0;                    
                    Console.Write("\n\nEnter the Start point and End Point for Route       : ");
                    var input = Console.ReadLine();
                    Console.Write("\nEnter the max Distance to travel                    : ");
                    maxDistance = int.Parse(Console.ReadLine());
                    var router = InstanceFactory.GetRouter();
                    var result = router.FindNumberOfRoutesByDistance(input, maxDistance);
                   
                    Console.WriteLine("\n\n The number of Diffent Routes between {0} and {1}    : {2}", input[0], input[1], result.Count);
                    Console.WriteLine("\n\n The routes are {0}", result.Aggregate((x, y) => x + "," + y));
                }
                else if (i == 9)     // exit
                    break;
                else
                    Console.WriteLine("\n\n\t***********   - Invalid  Enter -**************");

                Console.ReadLine();
                Console.Clear();
            }
        }
               

        //To check the distance of the route
        public void ShowDistance()
        {
            Console.Write("\n\nEnter the City Initials to find the Distance (eg : ABCE)         : ");
            var inputData = Console.ReadLine();

            if (string.IsNullOrEmpty(inputData) && inputData.Length <= 1)
            {
                Console.WriteLine("Please Define the Route in Correct Manner");
                return;
            }

            var output = String.Empty;
            var router = InstanceFactory.GetRouter();
            var result = router.FindTotalDistance(inputData);

            if (result.Result == TotalDistanceResult.Status.NoRouteFound) output = " ****    No Such Route Found ! *****";
            else if (result.Result == TotalDistanceResult.Status.InvalidInput) output = "Please Define the Route in Correct Manner";

            else output = string.Format("Total Distance Of Route {0} : {1}", inputData.ToUpper(), result);
            Console.WriteLine(output);
        }

        //to show the # of routes between stations either all routes of exact number of routes depending on bool value            
        public int ShowRoute(bool value)
        {
            int answer = 0;
            int stops = 0;
            
            Console.Write("\n\nEnter the Start point and End Point for Route       : ");
            var input = Console.ReadLine();
            Console.Write("\nEnter the number of Stops                           : ");
            stops = int.Parse(Console.ReadLine());

            var router = InstanceFactory.GetRouter();
            var result = router.FindTrips(input, stops, value);   
            Console.WriteLine("\n\n\tNumber Of Trips from {0} to {1} is     :-- {2} ", input[0], input[1], result.Count);
            Console.WriteLine("\n\n The Trips are {0}", result.Aggregate((x, y) => x + "," + y));
            return answer;
        }        
    }
}
using Kiwiland.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailRoute
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileData = null;

            try
            {
                ReadInput data = new ReadInput("inputFile.txt");
                fileData = data.GData;
            }
            catch(Exception e)
            {
                Console.WriteLine("problem reading File" + e);

            }
            IList<Edge> list = new List<Edge>();
            
            
            GetInquiry getInput = new GetInquiry();
            getInput.userInput();

           // Route nodeRoute = new Route(graph);
            

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Kiwiland.Models;

namespace Kiwiland.Controllers
{
    public class FileData
    {
        String[] data = null;
        public IList<Edge> nList = new List<Edge>();

        public FileData()
        {
            String[] fileData = null;

            try
            {
                fileData = File.ReadAllLines(@"C:\Users\DELL\Documents\Visual Studio 2015\Projects\Test\RailRouteMVC\inputFile.txt");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not find the File {0}.Exception thrown {1}", "inputFile.txt", e.Message);
                throw e;
            }

            string pattern = "[\\s]*[,][\\s]+";
            data = Regex.Split(fileData[0], pattern);

            foreach (string d in data)
            {
                nList.Add(new Edge { Start = d[0], Last = d[1], Distance = int.Parse(d.Substring(2)) });
            }
        }
    }
}
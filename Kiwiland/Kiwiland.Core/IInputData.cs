using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kiwiland.Core
{
    public class InstanceFactory
    {
        private static IInputData _inputData;
        private static IRouter _router;

        public static IInputData GetInputData()
        {
            return _inputData ?? (_inputData = new FileInputData());
        }

        public static IRouter GetRouter()
        {
            return _router ?? (_router = new Router());
        }
    }

    public interface IInputData
    {
        IList<Edge> NList { get; }
        List<Edge> Connections(char start);
    }

    public class FileInputData : IInputData
    {
        private IList<Edge> _nList;
        public IList<Edge> NList
        {
            get
            {
                return _nList ?? (_nList = LoadFromFile());
            }
        }

        public List<Edge> Connections(char start)
        {
            return (from e in NList
                    where e.Start == start
                    select e).ToList<Edge>();
        }

        private IList<Edge> LoadFromFile()
        {
            String[] fileData = null;
            IList<Edge> nList = new List<Edge>();
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
            var data = Regex.Split(fileData[0], pattern);

            foreach (string d in data)
            {
                nList.Add(new Edge(start:d[0], last: d[1], distance: int.Parse(d.Substring(2))));
            }
            return nList;
        }
    }
}

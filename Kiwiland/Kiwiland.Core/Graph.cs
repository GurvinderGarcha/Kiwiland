using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwiland.Core
{
    public class Graph
    {
        public Graph() { }

        Dictionary<char, bool> visited = new Dictionary<char, bool>();
        public IList<Edge> nList = new List<Edge>();
        //public IList<Edge> eList;
        //int[] distance;

        String[] data = null;
        public Graph(string fileData)
        {
            //string pattern = "[\\s]*[,][\\s]+";
            //data = Regex.Split(fileData, pattern);

            //foreach (string d in data)
            //{
            //    nList.Add(new Edge(d[0], d[1], int.Parse(d.Substring(2))));
            //}
        }

        // for all connecting routes from one station
        public List<Edge> Connections(char start)
        {
            return (from e in nList
                    where e.Start == start
                    select e).ToList<Edge>();
        }
    }
}

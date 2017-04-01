using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwiland.Core
{
    public class Edge
    {
        public char Start { get; set; }
        public char Last { get; set; }
        public int Distance { get; set; }

        public Edge(char start, char last, int distance)
        {
            Start = start;
            Last = last;
            Distance = distance;
        }
    }
}

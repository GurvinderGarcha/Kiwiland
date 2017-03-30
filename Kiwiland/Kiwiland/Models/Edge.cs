using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kiwiland.Models
{
    public class Edge
    {
        public char Start { get; set; }
        public char Last { get; set; }
        public int Distance { get; set; }
    }
}
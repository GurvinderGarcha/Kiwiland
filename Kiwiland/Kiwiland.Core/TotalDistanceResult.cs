using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwiland.Core
{
    public class TotalDistanceResult
    {
        public enum Status
        {
            NoRouteFound,
            InvalidInput,
            Success
        }

        public TotalDistanceResult(int totalDistance, Status result)
        {
            Result = result;
            TotalDistance = totalDistance;
        }

        public Status Result { get; set; }

        public int TotalDistance { get; set; }
    }
}

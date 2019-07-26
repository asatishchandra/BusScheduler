using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusScheduleApi.DTO
{
    public class BusStopRouteDto
    {
        public string BusStop { get; set; }
        public Dictionary<string, List<string>> BusRoutes { get; set; }
    }
}

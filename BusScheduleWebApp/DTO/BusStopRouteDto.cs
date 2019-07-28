using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusScheduleWebApp.DTO
{
    public class BusStopRouteDto
    {
        public string BusStop { get; set; }
        public List<BusRouteDto> BusRoutes { get; set; }
    }

    public class BusRouteDto
    {
        public string RouteName { get; set; }
        public List<string> Schedule { get; set; }
    }
}

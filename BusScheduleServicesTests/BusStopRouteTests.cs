using BusScheduleSevices.Models;
using BusScheduleSevices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BusScheduleServicesTests
{
    public class BusStopRouteTests
    {
        private readonly int _routeCount = 3;
        private readonly int _stopCount = 10;
        private readonly int _distance = 2;
        private readonly int _serviceGap = 15;

        [Fact]
        public void TestBusStopNameNumber()
        {
            BusScheduleService svc = new BusScheduleService();
            BusStop modelStop = new BusStop { StopName = "Stop1", StopNumber = 1 };
            List<BusStop> busStops = svc.InitializeBusStops();
            Assert.Equal(busStops.ElementAt(0).StopName, modelStop.StopName);
            Assert.Equal(busStops.ElementAt(0).StopNumber, modelStop.StopNumber);

            modelStop = new BusStop { StopName = "Stop2", StopNumber = 2 };
            Assert.Equal(busStops.ElementAt(1).StopName, modelStop.StopName);
            Assert.Equal(busStops.ElementAt(1).StopNumber, modelStop.StopNumber);

            modelStop = new BusStop { StopName = "Stop5", StopNumber = 5 };
            Assert.Equal(busStops.ElementAt(4).StopName, modelStop.StopName);
            Assert.Equal(busStops.ElementAt(4).StopNumber, modelStop.StopNumber);

            modelStop = new BusStop { StopName = "Stop10", StopNumber = 10 };
            Assert.Equal(busStops.ElementAt(9).StopName, modelStop.StopName);
            Assert.Equal(busStops.ElementAt(9).StopNumber, modelStop.StopNumber);
        }

        [Fact]
        public void TestBusRoute()
        {
            BusScheduleService svc = new BusScheduleService();
            BusRoute modelRoute = new BusRoute { RouteName = "Route1", RouteNumber = 1 };
            List<BusRoute> routes = svc.InitializeRoutes();
            Assert.Equal(routes.ElementAt(0).RouteName, modelRoute.RouteName);
            Assert.Equal(routes.ElementAt(0).RouteNumber, modelRoute.RouteNumber);

            modelRoute = new BusRoute { RouteName = "Route2", RouteNumber = 2 };
            Assert.Equal(routes.ElementAt(1).RouteName, modelRoute.RouteName);
            Assert.Equal(routes.ElementAt(1).RouteNumber, modelRoute.RouteNumber);

            modelRoute = new BusRoute { RouteName = "Route3", RouteNumber = 3 };
            Assert.Equal(routes.ElementAt(2).RouteName, modelRoute.RouteName);
            Assert.Equal(routes.ElementAt(2).RouteNumber, modelRoute.RouteNumber);
        }
    }
}

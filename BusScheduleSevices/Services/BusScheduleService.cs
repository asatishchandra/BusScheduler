using BusScheduleSevices.Interfaces;
using BusScheduleSevices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusScheduleSevices.Services
{
    public class BusScheduleService : IBusScheduleService
    {
        private readonly int _routeCount = 3;
        private readonly int _stopCount = 10;
        private readonly int _distance = 2;
        private readonly int _serviceGap = 15;
        
        public BusScheduleService()
        {
            
        }

        public List<BusStop> GetAllStopRouteData()
        {
            List<BusRoute> busRoutes = InitializeRoutes();
            List<BusStop> busStops = InitializeBusStops();
            DateTime startTime = DateTime.Today;

            foreach (BusStop stop in busStops)
            {
                stop.StopSchedule = GetFullRouteSchduleByStop(busRoutes, startTime);
                startTime = startTime.AddMinutes(_distance);
            }
            return busStops;
        }

        public List<BusStop> GetNextTwoBusArrivalDataByTime(string time)
        {
            List<BusRoute> busRoutes = InitializeRoutes();
            List<BusStop> busStops = InitializeBusStops();

            DateTime userCurrentTime = DateTime.Parse(time);
            DateTime startTime = DateTime.Today.Add(TimeSpan.Parse(string.Format("{0}:00", userCurrentTime.Hour)));
            while (DateTime.Compare(userCurrentTime, startTime) != -1)
            {
                startTime = startTime.AddMinutes(_serviceGap);
            }
            double startMinute = (startTime - userCurrentTime).TotalMinutes;

            foreach (BusStop stop in busStops)
            {
                stop.StopSchedule = GetNextTwoRouteSchduleByTime(busRoutes, startMinute);
                if (startMinute + _distance > _serviceGap)
                    startMinute = startMinute + _distance - _serviceGap;
                else
                    startMinute += _distance;
            }
            return busStops;
        }

        #region private methods
        private List<BusRoute> InitializeRoutes()
        {
            List<BusRoute> busRoutes = new List<BusRoute>();
            for (int i = 1; i <= _routeCount; i++)
            {
                string routeName = string.Format("Route{0}", i);
                busRoutes.Add(new BusRoute { RouteName = routeName, RouteNumber = i });
            }
            return busRoutes;
        }

        private List<BusStop> InitializeBusStops()
        {
            List<BusStop> busStops = new List<BusStop>();
            for (int i = 1; i <= _stopCount; i++)
            {
                string stopName = string.Format("Stop{0}", i);
                busStops.Add(new BusStop { StopName = stopName, StopNumber = i });
            }
            return busStops;
        }

        private Dictionary<BusRoute, List<string>> GetFullRouteSchduleByStop(List<BusRoute> busRoutes, DateTime startTime)
        {
            DateTime ts;
            int counter = 0;
            Dictionary<BusRoute, List<string>> schedule = new Dictionary<BusRoute, List<string>>();
            foreach (BusRoute route in busRoutes)
            {
                ts = startTime.AddMinutes(counter);
                schedule.Add(route, GetFullRouteTime(ts));
                counter += _distance;
            }
            return schedule;
        }

        private List<string> GetFullRouteTime(DateTime startTime)
        {
            List<string> timings = new List<string>();
            for (int i = 0; i < 96; i++)
            {
                timings.Add(startTime.ToString("HH:mm"));
                startTime = startTime.AddMinutes(_serviceGap);
            }
            return timings;
        }

        private Dictionary<BusRoute, List<string>> GetNextTwoRouteSchduleByTime(List<BusRoute> busRoutes, double startMinute)
        {
            Dictionary<BusRoute, List<string>> schedule = new Dictionary<BusRoute, List<string>>();
            foreach (BusRoute route in busRoutes)
            {
                schedule.Add(route, new List<string> { string.Format("Arriving in {0} minutes", startMinute), string.Format("{0} minutes", startMinute + _serviceGap) });
                if (startMinute + _distance > _serviceGap)
                    startMinute = startMinute + _distance - _serviceGap;
                else
                    startMinute += _distance;
            }
            return schedule;
        }

        #endregion

    }
}

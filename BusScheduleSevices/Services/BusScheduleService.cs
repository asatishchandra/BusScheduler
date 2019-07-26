using BusScheduleSevices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusScheduleSevices.Services
{
    public class BusScheduleService
    {
        private readonly int _routeCount = 3;
        private readonly int _stopCount = 10;
        private readonly int _distance = 2;
        private readonly int _serviceGap = 15;
        private DateTime _startTime = DateTime.Today;


        //public List<BusRoute> BusRoutes;
        //public List<BusStop> BusStops;

        //public BusRoute busroute;
        //public BusStop  busStop;

        public BusScheduleService()
        {
            //_startTime = DateTime.Today;
            //BusRoutes = InitializeRoutes();
            //BusStops = InitializeStops();
        }

        public List<BusStop> GetAllStopRouteData()
        {
            List<BusRoute> busRoutes = InitializeRoutes();
            List<BusStop> busStops = InitializeBusStops();

            foreach (BusStop stop in busStops)
            {
                stop.StopSchedule = GetFullRouteSchduleByStop(busRoutes);
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
            //startTime = DateTime.Today.Add(TimeSpan.Parse(string.Format("{0}:{1}", userCurrentTime.Hour, startMinute)));

            //var t = InitializeRoutesToStops(InitializeRoutes(), startTime);

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

        private Dictionary<string, List<string>> GetFullRouteSchduleByStop(List<BusRoute> busRoutes)
        {
            DateTime ts;
            int counter = 0;
            Dictionary<string, List<string>> schedule = new Dictionary<string, List<string>>();
            foreach (BusRoute route in busRoutes)
            {
                ts = _startTime.AddMinutes(counter);
                schedule.Add(route.RouteName, GetRouteTime(ts));
                counter += _distance;
            }
            _startTime = _startTime.AddMinutes(_distance);
            return schedule;
        }

        private List<string> GetRouteTime(DateTime startTime)
        {
            List<string> timings = new List<string>();
            for (int i = 0; i < 96; i++)
            {
                timings.Add(startTime.ToString("HH:mm"));
                startTime = startTime.AddMinutes(_serviceGap);
            }
            return timings;
        }

        private Dictionary<string, List<string>> GetNextTwoRouteSchduleByTime(List<BusRoute> busRoutes, double startMinute)
        {
            Dictionary<string, List<string>> schedule = new Dictionary<string, List<string>>();
            foreach (BusRoute route in busRoutes)
            {
                schedule.Add(route.RouteName, new List<string> { startMinute.ToString(), (startMinute + _serviceGap).ToString() });
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

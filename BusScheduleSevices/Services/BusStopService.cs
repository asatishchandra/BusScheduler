using BusScheduleSevices.Interfaces;
using BusScheduleSevices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusScheduleSevices.Services
{
    public class BusStopService : IBusStopService
    {
        private readonly int _stopCount = 10;

        public List<BusStop> GetAllBusStops() {
            List<BusStop> busStops = new List<BusStop>();

            for (int i = 1; i <= _stopCount; i++)
            {
                busStops.Add(new BusStop { StopName = string.Format("Stop{0}", i), StopNumber = i });
            }
            return busStops;
        }
    }
}

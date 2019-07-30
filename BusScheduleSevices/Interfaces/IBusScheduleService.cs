using BusScheduleSevices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusScheduleSevices.Interfaces
{
    public interface IBusScheduleService
    {
        List<BusStop> GetAllStopRouteData();
        List<BusStop> GetNextTwoBusArrivalDataByTime(string time);
        BusStop GetNextTwoBusArrivalDataByStop(int stopId, string time);
    }
}

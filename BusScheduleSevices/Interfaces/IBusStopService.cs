using BusScheduleSevices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusScheduleSevices.Interfaces
{
    public interface IBusStopService
    {
        List<BusStop> GetAllBusStops();
    }
}

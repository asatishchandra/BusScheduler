using System;
using System.Collections.Generic;
using System.Text;

namespace BusScheduleSevices.Models
{
    public class BusStop
    {
        public int StopNumber { get; set; }
        public string StopName { get; set; }
        public Dictionary<BusRoute, List<string>> StopSchedule { get; set; }
    }
}

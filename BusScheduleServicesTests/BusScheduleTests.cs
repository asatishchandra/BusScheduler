using BusScheduleSevices.Models;
using BusScheduleSevices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BusScheduleServicesTests
{
    public class BusScheduleTests
    {
        [Fact]
        public void TestBusSchdule()
        {
            List<string> stop1Route1 = new List<string> { "00:00", "00:15", "00:30", "00:45" };
            List<string> stop1Route2 = new List<string> { "00:02", "00:17", "00:32", "00:47" };
            List<string> stop1Route3 = new List<string> { "00:04", "00:19", "00:34", "00:49" };

            List<string> stop3Route1 = new List<string> { "00:04", "00:19", "00:34", "00:49" };
            List<string> stop3Route2 = new List<string> { "00:06", "00:21", "00:36", "00:51" };
            List<string> stop3Route3 = new List<string> { "00:08", "00:23", "00:38", "00:53" };

            BusScheduleService svc = new BusScheduleService();
            List<BusStop> fullRoutes = svc.GetAllStopRouteData();

            var s1r1 = fullRoutes.ElementAt(0).StopSchedule.Values.ElementAt(0);
            Assert.Equal(s1r1.ElementAt(0), stop1Route1.ElementAt(0));
            Assert.Equal(s1r1.ElementAt(1), stop1Route1.ElementAt(1));
            Assert.Equal(s1r1.ElementAt(2), stop1Route1.ElementAt(2));
            Assert.Equal(s1r1.ElementAt(3), stop1Route1.ElementAt(3));

            var s1r2 = fullRoutes.ElementAt(0).StopSchedule.Values.ElementAt(1);
            Assert.Equal(s1r2.ElementAt(0), stop1Route2.ElementAt(0));
            Assert.Equal(s1r2.ElementAt(1), stop1Route2.ElementAt(1));
            Assert.Equal(s1r2.ElementAt(2), stop1Route2.ElementAt(2));
            Assert.Equal(s1r2.ElementAt(3), stop1Route2.ElementAt(3));

            var s1r3 = fullRoutes.ElementAt(0).StopSchedule.Values.ElementAt(2);
            Assert.Equal(s1r3.ElementAt(0), stop1Route3.ElementAt(0));
            Assert.Equal(s1r3.ElementAt(1), stop1Route3.ElementAt(1));
            Assert.Equal(s1r3.ElementAt(2), stop1Route3.ElementAt(2));
            Assert.Equal(s1r3.ElementAt(3), stop1Route3.ElementAt(3));

            var s3r1 = fullRoutes.ElementAt(2).StopSchedule.Values.ElementAt(0);
            Assert.Equal(s3r1.ElementAt(0), stop3Route1.ElementAt(0));
            Assert.Equal(s3r1.ElementAt(1), stop3Route1.ElementAt(1));
            Assert.Equal(s3r1.ElementAt(2), stop3Route1.ElementAt(2));
            Assert.Equal(s3r1.ElementAt(3), stop3Route1.ElementAt(3));

            var s3r2 = fullRoutes.ElementAt(2).StopSchedule.Values.ElementAt(1);
            Assert.Equal(s3r2.ElementAt(0), stop3Route2.ElementAt(0));
            Assert.Equal(s3r2.ElementAt(1), stop3Route2.ElementAt(1));
            Assert.Equal(s3r2.ElementAt(2), stop3Route2.ElementAt(2));
            Assert.Equal(s3r2.ElementAt(3), stop3Route2.ElementAt(3));

            var s3r3 = fullRoutes.ElementAt(2).StopSchedule.Values.ElementAt(2);
            Assert.Equal(s3r3.ElementAt(0), stop3Route3.ElementAt(0));
            Assert.Equal(s3r3.ElementAt(1), stop3Route3.ElementAt(1));
            Assert.Equal(s3r3.ElementAt(2), stop3Route3.ElementAt(2));
            Assert.Equal(s3r3.ElementAt(3), stop3Route3.ElementAt(3));
        }

        [Fact]
        public void TestBusSchduleByTime()
        {
            string time = "3:01";
            List<string> stop1Route1 = new List<string> { "Arriving in: 14 minutes", "29 minutes" };
            List<string> stop1Route2 = new List<string> { "Arriving in: 1 minutes", "16 minutes" };
            List<string> stop1Route3 = new List<string> { "Arriving in: 3 minutes", "18 minutes" };

            List<string> stop2Route1 = new List<string> { "Arriving in: 1 minutes", "16 minutes" };
            List<string> stop2Route2 = new List<string> { "Arriving in: 3 minutes", "18 minutes" };
            List<string> stop2Route3 = new List<string> { "Arriving in: 5 minutes", "20 minutes" };

            List<string> stop3Route1 = new List<string> { "Arriving in: 3 minutes", "18 minutes" };
            List<string> stop3Route2 = new List<string> { "Arriving in: 5 minutes", "20 minutes" };
            List<string> stop3Route3 = new List<string> { "Arriving in: 7 minutes", "22 minutes" };

            BusScheduleService svc = new BusScheduleService();
            List<BusStop> fullRoutes = svc.GetNextTwoBusArrivalDataByTime(time);

            var s1r1 = fullRoutes.ElementAt(0).StopSchedule.Values.ElementAt(0);
            Assert.Equal(string.Join(",",s1r1), string.Join(",", stop1Route1));
            
            var s1r2 = fullRoutes.ElementAt(0).StopSchedule.Values.ElementAt(1);
            Assert.Equal(string.Join(",", s1r2), string.Join(",", stop1Route2));

            var s1r3 = fullRoutes.ElementAt(0).StopSchedule.Values.ElementAt(2);
            Assert.Equal(string.Join(",", s1r3), string.Join(",", stop1Route3));

            var s2r1 = fullRoutes.ElementAt(1).StopSchedule.Values.ElementAt(0);
            Assert.Equal(string.Join(",", s2r1), string.Join(",", stop2Route1));

            var s2r2 = fullRoutes.ElementAt(1).StopSchedule.Values.ElementAt(1);
            Assert.Equal(string.Join(",", s2r2), string.Join(",", stop2Route2));

            var s2r3 = fullRoutes.ElementAt(1).StopSchedule.Values.ElementAt(2);
            Assert.Equal(string.Join(",", s2r3), string.Join(",", stop2Route3));

            var s3r1 = fullRoutes.ElementAt(2).StopSchedule.Values.ElementAt(0);
            Assert.Equal(string.Join(",", s3r1), string.Join(",", stop3Route1));

            var s3r2 = fullRoutes.ElementAt(2).StopSchedule.Values.ElementAt(1);
            Assert.Equal(string.Join(",", s3r2), string.Join(",", stop3Route2));

            var s3r3 = fullRoutes.ElementAt(2).StopSchedule.Values.ElementAt(2);
            Assert.Equal(string.Join(",", s3r3), string.Join(",", stop3Route3));
        }
        [Fact]
        public void TestBusSchduleByStop()
        {
            string time = "3:01";
            string stopName = "Stop1";
            int stopNUmber = 1;
            List<string> stop1Route1 = new List<string> { "Arriving in: 14 minutes", "29 minutes" };
            List<string> stop1Route2 = new List<string> { "Arriving in: 1 minutes", "16 minutes" };
            List<string> stop1Route3 = new List<string> { "Arriving in: 3 minutes", "18 minutes" };

            BusScheduleService svc = new BusScheduleService();
            BusStop fullRoutes = svc.GetNextTwoBusArrivalDataByStop(1,time);

            Assert.Equal(fullRoutes.StopName, stopName);
            Assert.Equal(fullRoutes.StopNumber, stopNUmber);

            var s1r1 = fullRoutes.StopSchedule.Values.ElementAt(0);
            Assert.Equal(string.Join(",", s1r1), string.Join(",", stop1Route1));

            var s1r2 = fullRoutes.StopSchedule.Values.ElementAt(1);
            Assert.Equal(string.Join(",", s1r2), string.Join(",", stop1Route2));

            var s1r3 = fullRoutes.StopSchedule.Values.ElementAt(2);
            Assert.Equal(string.Join(",", s1r3), string.Join(",", stop1Route3));
        }
    }
}

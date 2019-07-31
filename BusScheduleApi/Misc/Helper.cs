using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace BusScheduleApi.Misc
{
    public static class Helper
    {
        public static List<Timer> timerTracker = new List<Timer>();

        public static void StopExistingTimers(Timer timer)
        {
            if (timerTracker.Count > 0)
            {
                foreach (Timer t in Helper.timerTracker)
                {
                    t.Stop();
                    t.Close();
                    t.Dispose();
                }
                timerTracker.Add(timer);
            }
            else
            {
                timerTracker.Add(timer);
            }
        }
    }
}

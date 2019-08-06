using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace BusScheduleApi.Misc
{
    public static class Helper
    {
        private static readonly int _refershMinutes = 1;
        public static List<Timer> timerTracker = new List<Timer>();
        public static Timer timer = new Timer(TimeSpan.FromMinutes(_refershMinutes).TotalMilliseconds);

        public static void StopExistingTimers(Timer timer)
        {
            if (timerTracker.Count > 0)
            {
                foreach (Timer t in timerTracker)
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

        public static Timer StartTimer()
        {
            if(timer == null)
                timer = new Timer(TimeSpan.FromMinutes(_refershMinutes).TotalMilliseconds);
            return timer;
        }
    }
}

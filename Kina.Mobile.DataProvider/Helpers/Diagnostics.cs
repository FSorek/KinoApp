using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Kina.Mobile.DataProvider.Helpers
{
    public static class Diagnostics
    {
        private static Stopwatch timer = new Stopwatch();
        private static TimeSpan lastRegisteredTime = new TimeSpan();
        public static void CreateReportMessage(string message)
        {
            StartReport();
            Debug.WriteLine("~~~~~~DEBUG REPORT~~~~~~" + timer.Elapsed + " SECONDS @ " + message + ". Being Called " + (timer.Elapsed - lastRegisteredTime) + "seconds after the last message.");
            lastRegisteredTime = timer.Elapsed;
        }

        public static void StartReport()
        {
            if (timer.IsRunning)
                return;
            timer.Start();
            lastRegisteredTime = timer.Elapsed;
            Debug.WriteLine("#####################################");
            Debug.WriteLine("#####################################");
            Debug.WriteLine("~~~~~ REPORT GENERATION STARTED ~~~~~");
            Debug.WriteLine("##### ------------------------- #####");
            Debug.WriteLine("##### ------------------------- #####");

        }
    }
}

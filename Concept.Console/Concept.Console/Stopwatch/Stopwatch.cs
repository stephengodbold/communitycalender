using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concept.Console.Stopwatch
{
    public class Stopwatch
    {
        System.Diagnostics.Stopwatch internalStopwatch;

        public double Minutes
        {
            get { return internalStopwatch.Elapsed.TotalMinutes; }
        }

        public void Start()
        {
            if (internalStopwatch == null)
            {
                internalStopwatch = new System.Diagnostics.Stopwatch();
            }

            if (!internalStopwatch.IsRunning)
            {
                System.Console.WriteLine("Starting stopwatch @ " + DateTime.Now.ToShortTimeString());
                internalStopwatch.Start();
            }
        }

        public void Stop()
        {
            if (internalStopwatch.IsRunning)
            {
                System.Console.WriteLine("Stopping stopwatch @ " + DateTime.Now.ToShortTimeString());
                internalStopwatch.Stop();
            }
        }

        public void Reset()
        {
            System.Console.WriteLine("Stopwatch reset...");
            internalStopwatch.Reset();
        }

    }
}

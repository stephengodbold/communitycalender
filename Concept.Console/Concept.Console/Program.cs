using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Concept.Console.DisposableProxy;
using Concept.Console.GoogleCalender.Contracts;

namespace Concept.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            PostGoogleEvent();
            
            System.Console.WriteLine("Any key to exit...");
            System.Console.ReadKey();
        }

        private static void PostGoogleEvent()
        {
            string user = string.Empty;
            string eventName = string.Empty;

            System.Console.Write("Enter a username: ");
            user = System.Console.ReadLine();

            System.Console.Write("Enter an event name: ");
            eventName = System.Console.ReadLine();

            var calenderEvent = new Event
                {
                    Category = "360",
                    Content = eventName,
                    End = DateTime.Now.AddDays(1),
                    Start = DateTime.Now,
                    Title = eventName,
                };


            try
            {
                GoogleCalender.EventPublisher.PublishEvent(calenderEvent);
                PublishInfo("Go check your G-Calender!");
            }
            catch (Exception ex)
            {
                ReportError(ex, false);
            }
        }

        private static void RunStopwatch()
        {
            ConsoleKeyInfo controlKey = new ConsoleKeyInfo('B', ConsoleKey.B, false, false, false);
            Stopwatch.Stopwatch watch = new Concept.Console.Stopwatch.Stopwatch();

            while (controlKey.Key != ConsoleKey.X)
            {
                System.Console.WriteLine("Press s to start stopwatch, e to stop, r to reset and x to exit");
                controlKey = System.Console.ReadKey();
                System.Console.WriteLine();

                switch (controlKey.Key)
                {
                    case ConsoleKey.S:
                        {
                            watch.Start();
                        }
                        break;
                    case ConsoleKey.E:
                        {
                            watch.Stop();
                            System.Console.WriteLine("Total Minutes Elapsed: {0}", Math.Floor(watch.Minutes));
                        }
                        break;
                    case ConsoleKey.R:
                        {
                            watch.Reset();
                            System.Console.Clear();
                        }
                        break;
                }
            }
        }

        private static void RunDisposableCheck()
        {
            IServiceContract serviceContract = null;

            System.Console.Write("Pick a number between 1 and 10: ");
            int inputValue = System.Console.Read();

            if (inputValue % 2 == 0)
            {
                serviceContract = new DisposableServiceProxy();
                serviceContract.CheckACondition(inputValue);
            }
            else
            {
                var mockManager = new Moq.Mock<IServiceContract>();

                mockManager.Setup(mockedCall => mockedCall.CheckACondition(inputValue))
                                                    .Returns(false);

                serviceContract = mockManager.Object;
                serviceContract.CheckACondition(inputValue);
            }

            if (serviceContract is IDisposable)
            {
                ((IDisposable)serviceContract).Dispose();
            }
            else
            {
                System.Console.WriteLine("I MOCK YOU!");
            }

            
            System.Console.WriteLine("We're done...");
            System.Console.ReadKey();
        }

        #region Console Helpers

        private static void ReportError(Exception ex, bool verbose)
        {
            var baseColour = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Red;

            if (verbose)
            {
                System.Console.WriteLine(ex.ToString());
            }
            else
            {
                System.Console.WriteLine(ex.Message);
            }

            System.Console.ForegroundColor = baseColour;
        }

        private static void PublishInfo(string info)
        {
            var baseColour = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Green;

            System.Console.WriteLine(info);

            System.Console.ForegroundColor = baseColour;
        }
       
        #endregion
    }
}

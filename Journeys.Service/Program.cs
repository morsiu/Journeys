using Nancy.Hosting.Self;
using System;
using System.Threading;

namespace Journeys.Service
{
    public class Program
    {
        public static void Main()
        {
            var bootstrapper = new Bootstrapper();
            var eventFilePath = "../data/events.txt";
            bootstrapper.Bootstrap(eventFilePath);
            var hostBoostrapper = new HostBootstrapper(bootstrapper.QueryDispatcher, bootstrapper.CommandDispatcher);
            var host = new NancyHost(
                hostBoostrapper,
                new HostConfiguration { UrlReservations = new UrlReservations { CreateAutomatically = true, User = "LOCALSERVICE" } },
                new Uri("http://localhost:65363"));
            host.Start();
            Wait();
        }

        private static void Wait()
        {
            var closeEvent = new ManualResetEvent(false);
            closeEvent.WaitOne();
        }
    }
}
using Journeys.Application.Service.Host;
using Journeys.Application.Service.Properties;
using Nancy.Hosting.Self;
using System;
using System.Threading;

namespace Journeys.Application.Service
{
    public class Program
    {
        public static void Main()
        {
            var configuration = Settings.Default;
            var bootstrapper = new Bootstrapper();
            bootstrapper.Bootstrap(configuration.EventFilePath);
            var hostBoostrapper = new HostBootstrapper(bootstrapper.QueryDispatcher, bootstrapper.CommandDispatcher, configuration.SitePath);
            var host = new NancyHost(
                hostBoostrapper,
                new HostConfiguration { UrlReservations = new UrlReservations { CreateAutomatically = true, User = configuration.UrlReservationUser } },
                new Uri(configuration.HostUri));
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
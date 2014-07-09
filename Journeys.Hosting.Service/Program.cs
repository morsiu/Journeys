using Journeys.Hosting.Service.Host;
using Journeys.Hosting.Service.Properties;
using Nancy.Hosting.Self;
using System;

namespace Journeys.Hosting.Service
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

            bootstrapper.RunScheduledHandlers();
        }
    }
}
using Journeys.Application;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Data;
using Journeys.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Client.Wpf
{
    internal class Bootstrapper
    {
        public MainWindow Bootstrap()
        {
            var eventBus = new Journeys.Eventing.EventBus();
            var commandBootstrapper = new Journeys.Application.Bootstrapper(eventBus);
            var dataBootstrapper = new Journeys.Data.Bootstrapper(eventBus);
            dataBootstrapper.Bootstrap();
            commandBootstrapper.Bootstrap(dataBootstrapper.QueryDispatcher);
            var viewEventBus = new Infrastructure.EventBus();
            return new MainWindow(commandBootstrapper.CommandDispatcher, dataBootstrapper.QueryDispatcher, viewEventBus);
        }
    }
}

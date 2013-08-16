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
            var dataBootstrapper = new Journeys.Data.Bootstrapper(eventBus);
            dataBootstrapper.Bootstrap();
            var domainBootstrapper = new Journeys.Domain.Bootstrapper();
            domainBootstrapper.Bootstrap();
            var commandBootstrapper = new Journeys.Application.Bootstrapper(eventBus, domainBootstrapper.DomainRepositories);
            commandBootstrapper.Bootstrap(dataBootstrapper.QueryDispatcher);
            var eventSourcingBootstrapper = new Journeys.EventSourcing.Bootstrapper(eventBus, domainBootstrapper.DomainRepositories, "Events.txt");
            eventSourcingBootstrapper.Bootstrap();
            var viewEventBus = new Infrastructure.EventBus();
            return new MainWindow(commandBootstrapper.CommandDispatcher, dataBootstrapper.QueryDispatcher, viewEventBus);
        }
    }
}

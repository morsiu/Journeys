using Journeys.Hosting.Adapters;
using Journeys.Hosting.Adapters.Modules.Command;
using Journeys.Hosting.Adapters.Modules.Query;
using Journeys.Hosting.Adapters.Modules.EventSourcing;
using Journeys.Support.Dispatching;
using Journeys.Support.Repositories;
using Journeys.Hosting.Adapters.Modules.Service;

namespace Journeys.Hosting.Service
{
    internal class Bootstrapper
    {
        public ServiceQueryDispatcher QueryDispatcher { get; private set; }

        public ServiceCommandDispatcher CommandDispatcher { get; private set; }

        public void Bootstrap(string eventFileName)
        {
            var eventBus = new Journeys.Support.Events.EventBus();
            var idFactory = new GuidIdFactory();
            var handlerRegistry = new HandlerRegistry();
            var handlerDispatcher = new HandlerDispatcher(handlerRegistry);

            var queryBootstrapper = new Application.Query.Bootstrapper(
                new QueryEventBus(eventBus),
                new QueryDispatcher(handlerDispatcher),
                new QueryHandlerRegistry(handlerRegistry));
            queryBootstrapper.Bootstrap();

            var repositories = new Repositories();

            var eventSourcingModule = new Journeys.Support.EventSourcing.Module(
                new EventSourcingModuleEventBus(eventBus),
                idFactory.IdImplementationType,
                eventFileName);

            var commandBootstrapper = new Application.Command.Bootstrapper(
                new CommandEventBus(eventBus),
                new CommandRepositories(repositories),
                new CommandIdFactory(idFactory),
                new CommandHandlerRegistry(handlerRegistry),
                new CommandQueryDispatcher(handlerDispatcher));
            commandBootstrapper.Bootstrap();

            var eventSourcingBootstrapper = new Application.EventSourcing.Bootstrapper(
                new EventSourcingEventBus(eventBus),
                new EventSourcingRepositories(repositories),
                new EventSourcingIdFactory(idFactory),
                new EventSourcingQueryDispatcher(handlerDispatcher),
                new EventSourcing(eventSourcingModule));
            eventSourcingBootstrapper.Bootstrap();

            eventSourcingModule.ReplayEvents();
            eventSourcingModule.StoreNewEvents();

            QueryDispatcher = new ServiceQueryDispatcher(handlerDispatcher);
            CommandDispatcher = new ServiceCommandDispatcher(handlerDispatcher);
        }
    }
}

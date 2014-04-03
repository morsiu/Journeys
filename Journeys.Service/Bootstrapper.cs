using Journeys.Adapters;
using Journeys.Dispatching;
using Journeys.Repositories;

namespace Journeys.Service
{
    internal class Bootstrapper
    {
        public ServiceQueryDispatcher QueryDispatcher { get; private set; }

        public ServiceCommandDispatcher CommandDispatcher { get; private set; }

        public void Bootstrap(string eventFileName)
        {
            var eventBus = new Event.EventBus();
            var idFactory = new IdFactory();
            var handlerRegistry = new HandlerRegistry();
            var handlerDispatcher = new HandlerDispatcher(handlerRegistry);

            var queryBootstrapper = new Query.Bootstrapper(
                new QueryEventBus(eventBus),
                new QueryDispatcher(handlerDispatcher),
                new QueryHandlerRegistry(handlerRegistry));
            queryBootstrapper.Bootstrap();

            var repositories = new Repositories.Repositories();

            var eventSourcingModule = new EventSourcing.Module(
                new EventSourcingEventBus(eventBus),
                idFactory.IdImplementationType,
                eventFileName);

            var applicationBootstrapper = new Application.Bootstrapper(
                new ApplicationEventBus(eventBus),
                new ApplicationRepositories(repositories),
                new ApplicationIdFactory(idFactory),
                new ApplicationCommandHandlerRegistry(handlerRegistry),
                new ApplicationQueryDispatcher(handlerDispatcher),
                new ApplicationEventSourcing(eventSourcingModule));
            applicationBootstrapper.Bootstrap();

            eventSourcingModule.ReplayEvents();
            eventSourcingModule.StoreNewEvents();

            QueryDispatcher = new ServiceQueryDispatcher(handlerDispatcher);
            CommandDispatcher = new ServiceCommandDispatcher(handlerDispatcher);
        }
    }
}

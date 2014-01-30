using Journeys.Adapters;
using Journeys.Dispatching;
using Journeys.Repositories;

namespace Journeys.Client
{
    internal class Bootstrapper
    {
        public void Bootstrap()
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
                "Events.txt");

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

            var wpfClientBootstrapper = new Client.Wpf.Bootstrapper(
                new WpfClientEventBus(eventBus),
                new WpfClientCommandDispatcher(handlerDispatcher),
                new WpfClientCommandHandlerRegistry(handlerRegistry),
                new WpfClientQueryDispatcher(handlerDispatcher),
                new WpfClientQueryHandlerRegistry(handlerRegistry),
                new WpfClientIdFactory(idFactory));
            wpfClientBootstrapper.Bootstrap();
            wpfClientBootstrapper.Run();
        }
    }
}

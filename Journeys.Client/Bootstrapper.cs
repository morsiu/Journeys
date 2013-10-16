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
                new Adapters.QueryDispatcher(handlerDispatcher),
                new QueryHandlerRegistry(handlerRegistry));
            queryBootstrapper.Bootstrap();

            var repositories = new Repositories.Repositories();

            var eventSourcingModule = new EventSourcing.Module(
                new EventSourcingEventBus(eventBus),
                idFactory.IdImplementationType,
                "Events.txt");

            var commandBootstrapper = new Command.Bootstrapper(
                new CommandEventBus(eventBus),
                new CommandRepositories(repositories),
                new CommandIdFactory(idFactory),
                new CommandHandlerRegistry(handlerRegistry),
                new CommandQueryDispatcher(handlerDispatcher),
                new CommandEventSourcing(eventSourcingModule));
            commandBootstrapper.Bootstrap();

            eventSourcingModule.ReplayEvents();
            eventSourcingModule.StoreNewEvents();

            var wpfClientBootstrapper = new Client.Wpf.Bootstrapper(
                new WpfClientEventBus(eventBus),
                new WpfClientCommandDispatcher(handlerDispatcher),
                new WpfClientQueryDispatcher(handlerDispatcher),
                new WpfClientIdFactory(idFactory));
            wpfClientBootstrapper.Run();
        }
    }
}

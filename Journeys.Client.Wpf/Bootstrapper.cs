using Journeys.Adapters;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Dispatching;
using Journeys.Repositories;

namespace Journeys.Client.Wpf
{
    internal class Bootstrapper
    {
        public MainWindow Bootstrap()
        {
            var eventBus = new Event.EventBus();
            var idFactory = new IdFactory();
            var commandProcessor = new CommandProcessor();
            var handlerRegistry = new HandlerRegistry();
            var handlerDispatcher = new HandlerDispatcher(handlerRegistry);

            var queryBootstrapper = new Query.Bootstrapper(
                new QueryEventBus(eventBus),
                new Adapters.QueryDispatcher(handlerDispatcher),
                new QueryHandlerRegistry(handlerRegistry));
            queryBootstrapper.Bootstrap();

            var repositories = new Repositories.Repositories();
            var commandBootstrapper = new Command.Bootstrapper(
                new CommandEventBus(eventBus),
                new CommandRepositories(repositories),
                new CommandIdFactory(idFactory),
                new CommandHandlerRegistry(commandProcessor),
                new CommandQueryDispatcher(handlerDispatcher));
            commandBootstrapper.Bootstrap();
            
            var eventSourcingBootstrapper = new EventSourcing.Bootstrapper(
                new EventSourcingEventBus(eventBus),
                new EventSourcingRepositories(repositories),
                idFactory.IdImplementationType,
                "Events.txt");
            eventSourcingBootstrapper.Bootstrap();
            
            var viewEventBus = new Infrastructure.EventBus();
            return new MainWindow(
                new CommandDispatcher(commandProcessor),
                new Infrastructure.QueryDispatcher(handlerDispatcher),
                viewEventBus,
                idFactory);
        }
    }
}

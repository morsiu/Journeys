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
            var queryProcessor = new QueryProcessor();

            var queryBootstrapper = new Query.Bootstrapper(
                new QueryEventBus(eventBus),
                new Adapters.QueryDispatcher(queryProcessor),
                new QueryHandlerRegistry(queryProcessor));
            queryBootstrapper.Bootstrap();

            var repositories = new Repositories.Repositories();
            var commandBootstrapper = new Command.Bootstrapper(
                new CommandEventBus(eventBus),
                new CommandRepositories(repositories),
                new CommandIdFactory(idFactory),
                new CommandHandlerRegistry(commandProcessor),
                new CommandQueryDispatcher(queryProcessor));
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
                new Infrastructure.QueryDispatcher(queryProcessor),
                viewEventBus,
                idFactory);
        }
    }
}

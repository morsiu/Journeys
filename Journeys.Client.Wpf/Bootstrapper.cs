namespace Journeys.Client.Wpf
{
    internal class Bootstrapper
    {
        public MainWindow Bootstrap()
        {
            var eventBus = new Eventing.EventBus();

            var queryBootstrapper = new Query.Bootstrapper(eventBus);
            queryBootstrapper.Bootstrap();

            var domainBootstrapper = new Domain.Bootstrapper();
            domainBootstrapper.Bootstrap();

            var commandBootstrapper = new Command.Bootstrapper(eventBus, domainBootstrapper.DomainRepositories);
            commandBootstrapper.Bootstrap(queryBootstrapper.QueryDispatcher);
            
            var eventSourcingBootstrapper = new EventSourcing.Bootstrapper(eventBus, domainBootstrapper.DomainRepositories, "Events.txt");
            eventSourcingBootstrapper.Bootstrap();
            
            var viewEventBus = new Infrastructure.EventBus();
            return new MainWindow(commandBootstrapper.CommandDispatcher, queryBootstrapper.QueryDispatcher, viewEventBus);
        }
    }
}

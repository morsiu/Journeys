using Journeys.Command.CommandHandlers;
using Journeys.Command.Infrastructure;
using Journeys.Commands;
using Journeys.Domain.Infrastructure.Repositories;
using Journeys.Eventing;
using Journeys.Query;

namespace Journeys.Command
{
    public class Bootstrapper
    {
        private readonly EventBus _eventBus;
        private readonly IDomainRepositories _domainRepositories;

        public Bootstrapper(EventBus eventBus, IDomainRepositories domainRepositories)
        {
            _eventBus = eventBus;
            _domainRepositories = domainRepositories;
        }

        public ICommandDispatcher CommandDispatcher { get; private set; }

        public void Bootstrap(IQueryDispatcher queryDispatcher)
        {
            var commandProcessor = new CommandProcessor();
            var addJourneyWithLiftCommandHandler = new AddJourneyWithLiftCommandHandler(_eventBus, _domainRepositories, queryDispatcher);
            commandProcessor.SetHandler<AddJourneyWithLiftCommand>(addJourneyWithLiftCommandHandler.ExecuteTransacted);

            CommandDispatcher = new CommandDispatcher(commandProcessor);
        }
    }
}

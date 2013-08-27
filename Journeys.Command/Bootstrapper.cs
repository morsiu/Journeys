using Journeys.Command.CommandHandlers;
using Journeys.Command.Infrastructure;
using Journeys.Commands;
using Journeys.Eventing;
using Journeys.Query;

namespace Journeys.Command
{
    public class Bootstrapper
    {
        private readonly EventBus _eventBus;
        private readonly IRepositories _repositories;
        private readonly IIdFactory _idFactory;

        public Bootstrapper(EventBus eventBus, IRepositories repositories, IIdFactory idFactory)
        {
            _eventBus = eventBus;
            _idFactory = idFactory;
            _repositories = repositories;
        }

        public ICommandDispatcher CommandDispatcher { get; private set; }

        public void Bootstrap(IQueryDispatcher queryDispatcher)
        {
            var commandProcessor = new CommandProcessor();
            commandProcessor.SetHandler<AddJourneyWithLiftCommand>(
                new AddJourneyWithLiftCommandHandler(_eventBus, _repositories, _idFactory, queryDispatcher).ExecuteTransacted);

            CommandDispatcher = new CommandDispatcher(commandProcessor);
        }
    }
}

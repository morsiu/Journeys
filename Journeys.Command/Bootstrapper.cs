using Journeys.Command.Handlers;
using Journeys.Commands;

namespace Journeys.Command
{
    public class Bootstrapper
    {
        private readonly IEventBus _eventBus;
        private readonly IRepositories _repositories;
        private readonly IIdFactory _idFactory;
        private readonly ICommandHandlerRegistry _commandHandlerRegistry;
        private readonly IQueryDispatcher _queryDispatcher;

        public Bootstrapper(
            IEventBus eventBus,
            IRepositories repositories,
            IIdFactory idFactory,
            ICommandHandlerRegistry commandHandlerRegistry,
            IQueryDispatcher queryDispatcher)
        {
            _eventBus = eventBus;
            _idFactory = idFactory;
            _repositories = repositories;
            _commandHandlerRegistry = commandHandlerRegistry;
            _queryDispatcher = queryDispatcher;
        }

        public void Bootstrap()
        {
            _commandHandlerRegistry.SetHandler<AddJourneyWithLiftCommand>(
                new AddJourneyWithLiftCommandHandler(_eventBus, _repositories, _idFactory, _queryDispatcher).ExecuteTransacted);
        }
    }
}

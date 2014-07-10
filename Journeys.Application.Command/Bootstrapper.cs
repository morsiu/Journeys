using Journeys.Application.Command.Handlers;
using Journeys.Data.Commands;

namespace Journeys.Application.Command
{
    public sealed class Bootstrapper
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
            _commandHandlerRegistry.SetHandler<AddJourneyWithLiftsCommand>(
                new AddJourneyWithLiftsCommandHandler(_eventBus, _repositories, _idFactory, _queryDispatcher).ExecuteTransacted);
        }
    }
}

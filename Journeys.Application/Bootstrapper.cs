using Journeys.Application.CommandHandlers;
using Journeys.Application.EventReplayers;
using Journeys.Commands;
using Journeys.Events;

namespace Journeys.Application
{
    public class Bootstrapper
    {
        private readonly IEventBus _eventBus;
        private readonly IRepositories _repositories;
        private readonly IIdFactory _idFactory;
        private readonly ICommandHandlerRegistry _commandHandlerRegistry;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IEventSourcing _eventSourcing;

        public Bootstrapper(
            IEventBus eventBus,
            IRepositories repositories,
            IIdFactory idFactory,
            ICommandHandlerRegistry commandHandlerRegistry,
            IQueryDispatcher queryDispatcher,
            IEventSourcing eventSourcing)
        {
            _eventBus = eventBus;
            _idFactory = idFactory;
            _repositories = repositories;
            _commandHandlerRegistry = commandHandlerRegistry;
            _queryDispatcher = queryDispatcher;
            _eventSourcing = eventSourcing;
        }

        public void Bootstrap()
        {
            _commandHandlerRegistry.SetHandler<AddJourneyWithLiftsCommand>(
                new AddJourneyWithLiftsCommandHandler(_eventBus, _repositories, _idFactory, _queryDispatcher).ExecuteTransacted);

            _eventSourcing.RegisterEventReplayer<JourneyCreatedEvent>(new JourneyCreatedEventReplayer(_repositories, _eventBus).Replay);
            _eventSourcing.RegisterEventReplayer<LiftAddedEvent>(new LiftAddedEventReplayer(_repositories).Replay);
            _eventSourcing.RegisterEventReplayer<PersonCreatedEvent>(new PersonCreatedEventReplayer(_repositories, _eventBus).Replay);
        }
    }
}

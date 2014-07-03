using Journeys.Application.EventSourcing.EventReplayers;
using Journeys.Data.Events;

namespace Journeys.Application.EventSourcing
{
    public class Bootstrapper
    {
        private readonly IEventBus _eventBus;
        private readonly IRepositories _repositories;
        private readonly IIdFactory _idFactory;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IEventSourcing _eventSourcing;

        public Bootstrapper(
            IEventBus eventBus,
            IRepositories repositories,
            IIdFactory idFactory,
            IQueryDispatcher queryDispatcher,
            IEventSourcing eventSourcing)
        {
            _eventBus = eventBus;
            _idFactory = idFactory;
            _repositories = repositories;
            _queryDispatcher = queryDispatcher;
            _eventSourcing = eventSourcing;
        }

        public void Bootstrap()
        {
            _eventSourcing.RegisterEventReplayer<JourneyCreatedEvent>(new JourneyCreatedEventReplayer(_repositories, _eventBus).Replay);
            _eventSourcing.RegisterEventReplayer<LiftAddedEvent>(new LiftAddedEventReplayer(_repositories).Replay);
            _eventSourcing.RegisterEventReplayer<PersonCreatedEvent>(new PersonCreatedEventReplayer(_repositories, _eventBus).Replay);
        }
    }
}

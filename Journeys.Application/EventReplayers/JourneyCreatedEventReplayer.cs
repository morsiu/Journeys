using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Operations;
using Journeys.Events;

namespace Journeys.Application.EventReplayers
{
    internal class JourneyCreatedEventReplayer
    {
        private readonly IRepositories _journeyRepository;
        private readonly IEventBus _eventBus;

        public JourneyCreatedEventReplayer(IRepositories journeyRepository, IEventBus eventBus)
        {
            _journeyRepository = journeyRepository;
            _eventBus = eventBus;
        }

        public void Replay(JourneyCreatedEvent @event)
        {
            var routeDistance = new Distance(@event.RouteDistance, DistanceUnit.Kilometer);
            var journey = new Journey(@event.JourneyId, @event.DateOfOccurrence, routeDistance, _eventBus.ForDomain());
            _journeyRepository.Store(journey);
        }
    }
}

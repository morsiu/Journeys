using Mors.AppPlatform.Common.Services;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Domain.Journeys.Capabilities;
using Mors.Journeys.Domain.Journeys.Operations;

namespace Mors.Journeys.Application.EventReplayers
{
    internal sealed class JourneyCreatedEventReplayer
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
            var journey = new Journey(@event.JourneyId, @event.DateOfOccurrence, routeDistance, _eventBus);
            _journeyRepository.Store(journey);
        }
    }
}

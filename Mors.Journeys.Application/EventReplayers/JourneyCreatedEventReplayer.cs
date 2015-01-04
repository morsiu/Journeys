using System;
using Mors.Journeys.Application;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Domain.Journeys.Capabilities;
using Mors.Journeys.Domain.Journeys.Operations;

namespace Mors.Journeys.Application.EventReplayers
{
    internal sealed class JourneyCreatedEventReplayer
    {
        private readonly IRepositories _journeyRepository;
        private readonly Action<object> _eventPublisher;

        public JourneyCreatedEventReplayer(IRepositories journeyRepository, Action<object> eventPublisher)
        {
            _journeyRepository = journeyRepository;
            _eventPublisher = eventPublisher;
        }

        public void Replay(JourneyCreatedEvent @event)
        {
            var routeDistance = new Distance(@event.RouteDistance, DistanceUnit.Kilometer);
            var journey = new Journey(@event.JourneyId, @event.DateOfOccurrence, routeDistance, _eventPublisher);
            _journeyRepository.Store(journey);
        }
    }
}

using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Data;
using Journeys.Domain.Journeys.Operations;
using Journeys.Eventing;
using Journeys.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.EventSourcing.Replayers
{
    internal class JourneyCreatedEventReplayer
    {
        private readonly IDomainRepository<Journey> _journeyRepository;
        private readonly IEventBus _eventBus;

        public JourneyCreatedEventReplayer(IDomainRepository<Journey> journeyRepository, IEventBus eventBus)
        {
            _journeyRepository = journeyRepository;
            _eventBus = eventBus;
        }

        public void Replay(JourneyCreatedEvent @event)
        {
            var routeDistance = new Distance(@event.RouteDistance, DistanceUnit.Kilometer);
            var journeyId = new Id<Journey>(@event.JourneyId);
            var journey = new Journey(journeyId, @event.DateOfOccurrence, routeDistance, _eventBus);
            _journeyRepository.Store(journey);
        }
    }
}

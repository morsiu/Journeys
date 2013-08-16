using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Data;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using Journeys.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.EventSourcing.Replayers
{
    internal class LiftAddedEventReplayer
    {
        private readonly IDomainRepository<Journey> _journeyRepository;

        public LiftAddedEventReplayer(IDomainRepository<Journey> journeyRepository)
        {
            _journeyRepository = journeyRepository;
        }

        public void Replay(LiftAddedEvent @event)
        {
            var journeyId = new Id<Journey>(@event.JourneyId);
            var personId = new Id<Person>(@event.PersonId);
            var liftDistance = new Distance(@event.LiftDistance, DistanceUnit.Kilometer);
            var journey = _journeyRepository
                .Get(journeyId)
                .AddLift(personId, liftDistance);
            _journeyRepository.Store(journey);
        }
    }
}

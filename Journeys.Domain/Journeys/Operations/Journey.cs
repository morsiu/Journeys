using System;
using System.Linq;
using Journeys.Common;
using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Collections;
using Journeys.Domain.Infrastructure.Exceptions;
using Journeys.Domain.Infrastructure.Markers;
using Journeys.Domain.Infrastructure.Messages;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.People;
using Journeys.Events;

namespace Journeys.Domain.Journeys.Operations
{
    [Aggregate]
    public class Journey : IHasId
    {
        private readonly IEventBus _eventBus;
        private readonly IId _id;
        private readonly DateTime _dateOfOccurrence;
        private readonly Distance _routeDistance;
        private readonly ImmutableList<Lift> _lifts = ImmutableList<Lift>.Empty;

        public Journey(IId id, DateTime dateOfOccurrence, Distance routeDistance, IEventBus eventBus)
        {
            _dateOfOccurrence = dateOfOccurrence;
            _eventBus = eventBus;
            _id = id;
            _routeDistance = routeDistance;
            _eventBus.Publish(new JourneyCreatedEvent(id, dateOfOccurrence, routeDistance));
        }

        private Journey(Journey journey, ImmutableList<Lift> lifts)
        {
            _dateOfOccurrence = journey._dateOfOccurrence;
            _eventBus = journey._eventBus;
            _id = journey._id;
            _lifts = lifts;
            _routeDistance = journey._routeDistance;
        }

        public Journey AddLift(IId personId, Distance liftDistance)
        {
            if (_lifts.Any(aLift => aLift.EqualsByPerson(personId))) 
                throw new InvariantViolationException(FailureMessages.JourneyAlreadyContainsLiftWithSamePerson);
            if (liftDistance > _routeDistance) 
                throw new InvariantViolationException(FailureMessages.CannotAddLiftWithDistanceLargerThanJourneyDistance);
            var lift = new Lift(personId, liftDistance);
            var newLifts = _lifts.Add(lift);
            _eventBus.Publish(new LiftAddedEvent(_id, personId, liftDistance));
            return new Journey(this, newLifts);
        }

        IId IHasId.Id
        {
            get { return _id; }
        }
    }
}

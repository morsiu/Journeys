using System;
using System.Linq;
using Mors.Journeys.Data;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Domain.Infrastructure.Collections;
using Mors.Journeys.Domain.Infrastructure.Exceptions;
using Mors.Journeys.Domain.Infrastructure.Markers;
using Mors.Journeys.Domain.Journeys.Capabilities;

namespace Mors.Journeys.Domain.Journeys.Operations
{
    [Aggregate]
    public sealed class Journey : IHasId
    {
        private readonly object _id;
        private readonly DateTime _dateOfOccurrence;
        private readonly Distance _routeDistance;
        private readonly ImmutableList<Lift> _lifts = ImmutableList<Lift>.Empty;
        private readonly Action<object> _eventPublisher;

        public Journey(object id, DateTime dateOfOccurrence, Distance routeDistance, Action<object> eventPublisher)
        {
            _dateOfOccurrence = dateOfOccurrence;
            _eventPublisher = eventPublisher;
            _id = id;
            _routeDistance = routeDistance;
            _eventPublisher(new JourneyCreatedEvent(id, dateOfOccurrence, routeDistance));
        }

        private Journey(Journey journey, ImmutableList<Lift> lifts)
        {
            _dateOfOccurrence = journey._dateOfOccurrence;
            _eventPublisher = journey._eventPublisher;
            _id = journey._id;
            _lifts = lifts;
            _routeDistance = journey._routeDistance;
        }

        public Journey AddLift(object personId, Distance liftDistance)
        {
            if (ContainsLiftForPerson(personId))
                throw new InvariantViolationException(Messages.JourneyAlreadyContainsLiftForThatPerson);
            if (liftDistance > _routeDistance) 
                throw new InvariantViolationException(Messages.CannotAddLiftWithDistanceLargerThanJourneyDistance);
            var lift = new Lift(personId);
            var newLifts = _lifts.Add(lift);
            _eventPublisher(new LiftAddedEvent(_id, personId, liftDistance));
            return new Journey(this, newLifts);
        }

        private bool ContainsLiftForPerson(object personId)
        {
            return _lifts.Any(lift => lift.IsForPerson(personId));
        }

        object IHasId.Id
        {
            get { return _id; }
        }
    }
}

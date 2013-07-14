using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Exceptions;
using Journeys.Domain.Infrastructure.Markers;
using Journeys.Domain.Infrastructure.Messages;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Events;
using Journeys.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Domain.Journeys.Operations
{
    [Entity]
    public class Journey
    {
        private readonly IEventBus _eventBus;
        private readonly Id<Journey> _id;
        private readonly DateTime _dateOfOccurence;
        private readonly Distance _routeDistance;
        private readonly List<Lift> _lifts = new List<Lift>();

        internal Journey(Id<Journey> id, DateTime dateOfOccurence, Distance routeDistance, IEventBus eventBus)
        {
            _id = id;
            _dateOfOccurence = dateOfOccurence;
            _routeDistance = routeDistance;
            _eventBus = eventBus;
        }

        public void AddLift(Id<Person> personId, Distance liftDistance)
        {
            if (_lifts.Any(aLift => aLift.EqualsByPerson(personId))) 
                throw new InvariantViolatedException(FailureMessages.JourneyAlreadyContainsLiftWithSamePerson);
            if (liftDistance > _routeDistance) 
                throw new InvariantViolatedException(FailureMessages.CannotAddLiftWithDistanceLargerThanJourneyDistance);
            var lift = new Lift(personId, liftDistance);
            _lifts.Add(lift);
            _eventBus.Publish<LiftAddedEvent>(new LiftAddedEvent(_id, personId, liftDistance));
        }
    }
}

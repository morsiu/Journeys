using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Exceptions;
using Journeys.Domain.Infrastructure.Markers;
using Journeys.Domain.Infrastructure.Messages;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Domain.Journeys.Operations
{
    [Entity]
    public class Journey
    {
        private DateTime _dateOfOccurence;
        private Distance _routeDistance; 
        private List<Lift> _lifts = new List<Lift>();

        public Journey(DateTime dateOfOccurence, Distance routeDistance)
        {
            _dateOfOccurence = dateOfOccurence;
            _routeDistance = routeDistance;
        }

        public void AddLift(Id<Person> personId, Distance liftDistance)
        {
            if (_lifts.Any(aLift => aLift.EqualsByPerson(personId))) 
                throw new InvariantViolatedException(FailureMessages.JourneyAlreadyContainsLiftWithSamePerson);
            if (liftDistance > _routeDistance) 
                throw new InvariantViolatedException(FailureMessages.CannotAddLiftWithDistanceLargerThanJourneyDistance);
            var lift = new Lift(personId, liftDistance);
            _lifts.Add(lift);
        }
    }
}

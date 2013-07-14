using Journeys.Domain.Exceptions;
using Journeys.Domain.Identities;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Markers;
using Journeys.Domain.Messages;
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
            if (liftDistance.CompareTo(_routeDistance) > 0) 
                throw new InvariantViolatedException(FailureMessages.CannotAddLiftWithDistanceLargerThanJourneyDistance);
            var lift = new Lift(personId, liftDistance);
            _lifts.Add(lift);
        }
    }
}

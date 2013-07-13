using Journeys.Domain.Exceptions;
using Journeys.Domain.Identities;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Markers;
using Journeys.Domain.Messages;
using Journeys.Domain.People;
using Journeys.Domain.Routes.Operations;
using System;
using System.Collections.Generic;

namespace Journeys.Domain.Journeys.Operations
{
    [Factory]
    public class JourneyFactory
    {
        private DateTime? _dateOfOccurence;
        private Id<Route>? _routeId;
        private List<Lift> _lifts = new List<Lift>();

        public void SetDateOfOccurence(DateTime dateOfOccurence)
        {
            _dateOfOccurence = dateOfOccurence;
        }

        public void SetRoute(Id<Route> routeId)
        {
            _routeId = routeId;
        }

        public void AddLift(Id<Person> personId, Distance distance)
        {
            var lift = new Lift(personId, distance);
            _lifts.Add(lift);
        }

        public Journey BuildJourney()
        {
            if (_dateOfOccurence == null) throw new EntityBuildException(FailureMessages.DateOfOccurenceMustBeProvidedForNewJourney);
            if (_routeId == null) throw new EntityBuildException(FailureMessages.RouteMustBeProvidedForNewJourney);
            var journey = new Journey(_dateOfOccurence.Value, _routeId.Value);
            foreach (var lift in _lifts)
            {
                journey.AddLift(lift);
            }
            return journey;
        }
    }
}

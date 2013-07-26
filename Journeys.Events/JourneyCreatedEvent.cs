using System;

namespace Journeys.Events
{
    public class JourneyCreatedEvent
    {
        public JourneyCreatedEvent(Guid journeyId, DateTime dateOfOccurrence, decimal routeDistance)
        {
            JourneyId = journeyId;
            DateOfOccurrence = dateOfOccurrence;
            RouteDistance = routeDistance;
        }

        public Guid JourneyId { get; private set; }

        public DateTime DateOfOccurrence { get; private set; }

        public decimal RouteDistance { get; private set; }
    }
}

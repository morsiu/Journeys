using System;

namespace Journeys.Events
{
    public class JourneyCreatedEvent
    {
        public JourneyCreatedEvent(Guid journeyId, DateTime dateOfOccurence, decimal routeDistance)
        {
            JourneyId = journeyId;
            DateOfOccurence = dateOfOccurence;
            RouteDistance = routeDistance;
        }

        public Guid JourneyId { get; private set; }

        public DateTime DateOfOccurence { get; private set; }

        public decimal RouteDistance { get; private set; }
    }
}

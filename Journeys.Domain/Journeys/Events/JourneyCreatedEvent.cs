using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Operations;
using System;

namespace Journeys.Domain.Journeys.Events
{
    public class JourneyCreatedEvent
    {
        public JourneyCreatedEvent(Id<Journey> journeyId, DateTime dateOfOccurence, Distance routeDistance)
        {
            JourneyId = journeyId;
            DateOfOccurence = dateOfOccurence;
            RouteDistance = routeDistance;
        }

        public Id<Journey> JourneyId { get; private set; }

        public DateTime DateOfOccurence { get; private set; }

        public Distance RouteDistance { get; private set; }
    }
}

using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

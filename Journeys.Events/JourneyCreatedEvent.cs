using System;
using System.Runtime.Serialization;
using Journeys.Common;

namespace Journeys.Events
{
    [DataContract]
    public class JourneyCreatedEvent
    {
        public JourneyCreatedEvent(IId journeyId, DateTime dateOfOccurrence, decimal routeDistance)
        {
            JourneyId = journeyId;
            DateOfOccurrence = dateOfOccurrence;
            RouteDistance = routeDistance;
        }

        [DataMember]
        public IId JourneyId { get; private set; }

        [DataMember]
        public DateTime DateOfOccurrence { get; private set; }

        [DataMember]
        public decimal RouteDistance { get; private set; }
    }
}

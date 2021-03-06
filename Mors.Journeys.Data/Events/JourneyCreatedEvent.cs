﻿using System;
using System.Runtime.Serialization;

namespace Mors.Journeys.Data.Events
{
    [DataContract]
    public sealed class JourneyCreatedEvent
    {
        public JourneyCreatedEvent(object journeyId, DateTime dateOfOccurrence, decimal routeDistance)
        {
            JourneyId = journeyId;
            DateOfOccurrence = dateOfOccurrence;
            RouteDistance = routeDistance;
        }

        [DataMember]
        public object JourneyId { get; private set; }

        [DataMember]
        public DateTime DateOfOccurrence { get; private set; }

        [DataMember]
        public decimal RouteDistance { get; private set; }
    }
}

using System;
using System.Runtime.Serialization;
using Journeys.Common;

namespace Journeys.Events
{
    [DataContract]
    public class LiftAddedEvent
    {
        public LiftAddedEvent(IId journeyId, IId personId, decimal liftDistance)
        {
            JourneyId = journeyId;
            PersonId = personId;
            LiftDistance = liftDistance;
        }

        [DataMember]
        public IId JourneyId { get; private set; }

        [DataMember]
        public IId PersonId { get; private set; }

        [DataMember]
        public decimal LiftDistance { get; private set; }
    }
}

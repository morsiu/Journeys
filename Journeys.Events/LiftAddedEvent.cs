using System;
using System.Runtime.Serialization;

namespace Journeys.Events
{
    [DataContract]
    public class LiftAddedEvent
    {
        public LiftAddedEvent(Guid journeyId, Guid personId, decimal liftDistance)
        {
            JourneyId = journeyId;
            PersonId = personId;
            LiftDistance = liftDistance;
        }

        [DataMember]
        public Guid JourneyId { get; private set; }

        [DataMember]
        public Guid PersonId { get; private set; }

        [DataMember]
        public decimal LiftDistance { get; private set; }
    }
}

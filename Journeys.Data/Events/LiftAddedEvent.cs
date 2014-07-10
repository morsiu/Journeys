using System.Runtime.Serialization;

namespace Journeys.Data.Events
{
    [DataContract]
    public sealed class LiftAddedEvent
    {
        public LiftAddedEvent(object journeyId, object personId, decimal liftDistance)
        {
            JourneyId = journeyId;
            PersonId = personId;
            LiftDistance = liftDistance;
        }

        [DataMember]
        public object JourneyId { get; private set; }

        [DataMember]
        public object PersonId { get; private set; }

        [DataMember]
        public decimal LiftDistance { get; private set; }
    }
}

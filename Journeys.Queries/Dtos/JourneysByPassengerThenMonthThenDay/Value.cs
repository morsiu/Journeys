using System.Runtime.Serialization;

namespace Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    [DataContract]
    public struct Value
    {
        public Value(int journeyCount, decimal journeyDistance, int liftCount, decimal liftDistance) : this()
        {
            JourneyCount = journeyCount;
            JourneyDistance = journeyDistance;
            LiftCount = liftCount;
            LiftDistance = liftDistance;
        }

        [DataMember]
        public decimal LiftDistance { get; private set; }

        [DataMember]
        public int LiftCount { get; private set; }

        [DataMember]
        public decimal JourneyDistance { get; private set; }

        [DataMember]
        public int JourneyCount { get; private set; }
    }
}

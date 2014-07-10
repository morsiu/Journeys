using System.Runtime.Serialization;

namespace Journeys.Data.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    [DataContract]
    public sealed class Fact
    {
        public Fact(Key key, Value value)
        {
            Key = key;
            Value = value;
        }

        [DataMember]
        public Key Key { get; private set; }

        [DataMember]
        public Value Value { get; private set; }
    }
}

using System.Runtime.Serialization;

namespace Journeys.Data.Queries.Dtos
{
    [DataContract]
    public sealed class Lift
    {
        public Lift(object passengerId, decimal distance)
        {
            PassengerId = passengerId;
            Distance = distance;
        }

        [DataMember]
        public object PassengerId { get; private set; }

        [DataMember]
        public decimal Distance { get; private set; }
    }
}

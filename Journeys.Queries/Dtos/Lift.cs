using Journeys.Common;
using System.Runtime.Serialization;

namespace Journeys.Queries.Dtos
{
    [DataContract]
    public class Lift
    {
        public Lift(IId passengerId, decimal distance)
        {
            PassengerId = passengerId;
            Distance = distance;
        }

        [DataMember]
        public IId PassengerId { get; private set; }

        [DataMember]
        public decimal Distance { get; private set; }
    }
}

using Journeys.Data.Queries.Dtos;
using System.Runtime.Serialization;

namespace Journeys.Data.Queries
{
    [DataContract]
    public sealed class GetCostOfPassengerLiftsInPeriodQuery : IQuery<PassengerLiftsCost>
    {
        public GetCostOfPassengerLiftsInPeriodQuery(object passengerId, Period period)
        {
            Period = period;
            PassengerId = passengerId;
        }

        [DataMember]
        public object PassengerId { get; private set; }

        [DataMember]
        public Period Period { get; private set; }
    }
}

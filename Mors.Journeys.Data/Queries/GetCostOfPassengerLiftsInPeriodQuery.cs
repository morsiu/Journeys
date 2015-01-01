using System.Runtime.Serialization;
using Mors.Journeys.Data.Queries.Dtos;

namespace Mors.Journeys.Data.Queries
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

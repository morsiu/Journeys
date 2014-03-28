using Journeys.Common;
using Journeys.Queries.Dtos;
using System.Runtime.Serialization;

namespace Journeys.Queries
{
    [DataContract]
    public class GetCostOfPassengerLiftsInPeriodQuery : IQuery<PassengerLiftsCost>
    {
        public GetCostOfPassengerLiftsInPeriodQuery(IId passengerId, Period period)
        {
            Period = period;
            PassengerId = passengerId;
        }

        [DataMember]
        public IId PassengerId { get; private set; }

        [DataMember]
        public Period Period { get; private set; }
    }
}

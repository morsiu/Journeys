using Journeys.Common;
using Journeys.Queries.Dtos;

namespace Journeys.Queries
{
    public class GetCostOfPassengerLiftsInPeriodQuery : IQuery<PassengerLiftsCost>
    {
        public IId PassengerId { get; private set; }

        public Period Period { get; private set; }
    }
}

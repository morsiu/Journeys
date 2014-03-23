using Journeys.Common;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Capabilities
{
    [Entity]
    internal class Journey
    {
        private readonly Money _cost;

        public Journey(Money journeyCost)
        {
            _cost = journeyCost;
        }

        public void AddLift(IId passengerId, Distance distance)
        {
        }

        public Money GetCostFor(IId passengerId)
        {
            return new Money();
        }
    }
}

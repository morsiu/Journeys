using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Infrastructure.Markers;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Domain.Expenses.Operations
{
    [Aggregate]
    public sealed class JourneyList
    {
        public void AddJourney(IId journeyId, decimal distance)
        {
        }

        public void AddLift(IId journeyId, IId passengerId, decimal distance)
        {
        }

        public decimal GetPassengerLiftsCost(IId passengerId)
        {
            return 0m;
        }
    }
}

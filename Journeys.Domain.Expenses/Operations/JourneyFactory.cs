using Journeys.Common;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Operations
{
    [Factory]
    public sealed class JourneyFactory
    {
        public JourneyBuilder Create(IId journeyId, decimal routeDistance)
        {
            return new JourneyBuilder(journeyId, routeDistance);
        }
    }
}

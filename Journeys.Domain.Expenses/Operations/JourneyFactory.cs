using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Operations
{
    [Factory]
    public sealed class JourneyFactory
    {
        public JourneyBuilder Create(object journeyId, decimal routeDistance)
        {
            return new JourneyBuilder(journeyId, routeDistance);
        }
    }
}

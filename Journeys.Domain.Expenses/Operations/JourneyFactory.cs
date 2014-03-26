using Journeys.Common;

namespace Journeys.Domain.Expenses.Operations
{
    public sealed class JourneyFactory
    {
        public JourneyBuilder Create(IId journeyId, decimal distance)
        {
            return new JourneyBuilder(journeyId, distance);
        }
    }
}

using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Capabilities
{
    [ValueObject]
    public struct Distance
    {
        private readonly decimal _amount;

        public Distance(decimal amount)
        {
            _amount = amount;
        }

        public decimal Amount { get { return _amount; } }
    }
}

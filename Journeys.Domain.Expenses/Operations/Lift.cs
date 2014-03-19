using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Operations
{
    [Entity]
    public sealed class Lift
    {
        public Lift(IId personId, Distance distance)
        {
            _personId = personId;
            _distance = distance;
        }

        private readonly IId _personId;
        private readonly Distance _distance;

        public IId Id { get { return _personId; } }

        public Distance Distance { get { return _distance; } }
    }
}

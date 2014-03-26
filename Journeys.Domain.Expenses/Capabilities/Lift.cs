using Journeys.Common;

namespace Journeys.Domain.Expenses.Capabilities
{
    internal struct Lift
    {
        private readonly IId _passengerId;
        private readonly Distance _distance;

        public Lift(IId passengerId, Distance liftDistance)
        {
            _passengerId = passengerId;
            _distance = liftDistance;
        }

        public IId PassengerId { get { return _passengerId; } }

        public Distance Distance { get { return _distance; } }
    }
}

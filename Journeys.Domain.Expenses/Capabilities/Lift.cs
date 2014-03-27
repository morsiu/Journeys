using Journeys.Common;

namespace Journeys.Domain.Expenses.Capabilities
{
    internal struct Lift
    {
        private readonly IId _passengerId;
        private readonly RouteDistance _distance;

        public Lift(IId passengerId, RouteDistance liftDistance)
        {
            _passengerId = passengerId;
            _distance = liftDistance;
        }

        public IId PassengerId { get { return _passengerId; } }

        public RouteDistance Distance { get { return _distance; } }
    }
}

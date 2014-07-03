using Journeys.Common;

namespace Journeys.Domain.Expenses.Capabilities
{
    internal struct Lift
    {
        private readonly object _passengerId;
        private readonly RouteDistance _distance;

        public Lift(object passengerId, RouteDistance liftDistance)
        {
            _passengerId = passengerId;
            _distance = liftDistance;
        }

        public object PassengerId { get { return _passengerId; } }

        public RouteDistance Distance { get { return _distance; } }
    }
}

using Journeys.Common;

namespace Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal class PassengerPickup : IJourneyEvent
    {
        private readonly object _passengerId;
        private readonly RoutePoint _point;

        public PassengerPickup(object passengerId, RoutePoint point)
        {
            _passengerId = passengerId;
            _point = point;
        }

        public object PassengerId { get { return _passengerId; } }

        public RoutePoint Point { get { return _point; } }

        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

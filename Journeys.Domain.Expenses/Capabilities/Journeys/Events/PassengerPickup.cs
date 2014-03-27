using Journeys.Common;

namespace Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal class PassengerPickup : IJourneyEvent
    {
        private readonly IId _passengerId;
        private readonly RoutePoint _point;

        public PassengerPickup(IId passengerId, RoutePoint point)
        {
            _passengerId = passengerId;
            _point = point;
        }

        public IId PassengerId { get { return _passengerId; } }

        public RoutePoint Point { get { return _point; } }

        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

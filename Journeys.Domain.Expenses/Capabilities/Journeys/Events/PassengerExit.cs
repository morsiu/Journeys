using Journeys.Common;

namespace Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal class PassengerExit : IJourneyEvent
    {
        private readonly IId _passengerId;
        private readonly RoutePoint _point;

        public PassengerExit(IId passengerId, RoutePoint point)
        {
            _passengerId = passengerId;
            _point = point;
        }

        public RoutePoint Point { get { return _point; } }

        public IId PassengerId { get { return _passengerId; } }

        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

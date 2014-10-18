namespace Mors.Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal sealed class PassengerExit : IJourneyEvent
    {
        private readonly object _passengerId;
        private readonly RoutePoint _point;

        public PassengerExit(object passengerId, RoutePoint point)
        {
            _passengerId = passengerId;
            _point = point;
        }

        public RoutePoint Point { get { return _point; } }

        public object PassengerId { get { return _passengerId; } }

        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

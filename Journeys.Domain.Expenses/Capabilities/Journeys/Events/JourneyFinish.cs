namespace Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal class JourneyFinish : IJourneyEvent
    {
        private readonly RoutePoint _endPoint;

        public JourneyFinish(RoutePoint endPoint)
        {
            _endPoint = endPoint;
        }

        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }

        public RoutePoint Point
        {
            get { return _endPoint; }
        }
    }
}

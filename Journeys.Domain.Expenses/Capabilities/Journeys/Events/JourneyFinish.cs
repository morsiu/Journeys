namespace Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal class JourneyFinish : IJourneyEvent
    {
        private readonly Point _rideEndPoint;

        public JourneyFinish(Point rideEndPoint)
        {
            _rideEndPoint = rideEndPoint;
        }

        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Point Distance
        {
            get { return _rideEndPoint; }
        }
    }
}

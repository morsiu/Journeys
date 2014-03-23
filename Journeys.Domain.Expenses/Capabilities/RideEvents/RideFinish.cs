
namespace Journeys.Domain.Expenses.Capabilities.RideEvents
{
    internal class RideFinish : IRideEvent
    {
        private readonly Point _rideEndPoint;

        public RideFinish(Point rideEndPoint)
        {
            _rideEndPoint = rideEndPoint;
        }

        public void Visit(IRideVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Point Distance
        {
            get { return _rideEndPoint; }
        }
    }
}

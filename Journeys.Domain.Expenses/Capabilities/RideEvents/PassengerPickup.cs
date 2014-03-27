using Journeys.Common;

namespace Journeys.Domain.Expenses.Capabilities.RideEvents
{
    internal class PassengerPickup : IRideEvent
    {
        public PassengerPickup(IId passengerId, Point distance)
        {
            PassengerId = passengerId;
            Distance = distance;
        }

        public IId PassengerId { get; private set; }

        public Point Distance { get; private set; }

        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

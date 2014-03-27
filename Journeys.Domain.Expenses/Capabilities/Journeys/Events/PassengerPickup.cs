using Journeys.Common;

namespace Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal class PassengerPickup : IJourneyEvent
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

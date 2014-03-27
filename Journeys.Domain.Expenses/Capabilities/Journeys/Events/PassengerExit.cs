using Journeys.Common;

namespace Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal class PassengerExit : IJourneyEvent
    {
        public PassengerExit(IId passengerId, Point distance)
        {
            PassengerId = passengerId;
            Distance = distance;
        }

        public Point Distance { get; private set; }

        public IId PassengerId { get; private set; }

        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

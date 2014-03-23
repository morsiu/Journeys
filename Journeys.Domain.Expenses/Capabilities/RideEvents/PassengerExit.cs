using Journeys.Common;

namespace Journeys.Domain.Expenses.Capabilities.RideEvents
{
    internal class PassengerExit : IRideEvent
    {
        public PassengerExit(IId passengerId, Point distance)
        {
            PassengerId = passengerId;
            Distance = distance;
        }

        public Point Distance { get; private set; }

        public IId PassengerId { get; private set; }

        public void Visit(IRideVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

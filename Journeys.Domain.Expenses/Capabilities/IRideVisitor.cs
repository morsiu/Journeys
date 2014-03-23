using Journeys.Domain.Expenses.Capabilities.RideEvents;

namespace Journeys.Domain.Expenses.Capabilities
{
    internal interface IRideVisitor
    {
        void Visit(Drive drive);

        void Visit(PassengerExit exit);

        void Visit(PassengerPickup pickup);

        void Visit(RideStart start);

        void Visit(RideFinish finish);
    }
}

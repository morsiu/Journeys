using Journeys.Domain.Expenses.Capabilities.Journeys.Events;

namespace Journeys.Domain.Expenses.Capabilities
{
    internal interface IJourneyVisitor
    {
        void Visit(Drive drive);

        void Visit(PassengerExit exit);

        void Visit(PassengerPickup pickup);

        void Visit(JourneyStart start);

        void Visit(JourneyFinish finish);
    }
}

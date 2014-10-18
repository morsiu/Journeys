using Mors.Journeys.Domain.Expenses.Capabilities.Journeys;

namespace Mors.Journeys.Domain.Expenses.Capabilities
{
    internal interface IJourneyEvent
    {
        void Visit(IJourneyVisitor visitor);

        RoutePoint Point { get; }
    }
}

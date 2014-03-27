namespace Journeys.Domain.Expenses.Capabilities
{
    internal interface IJourneyEvent
    {
        void Visit(IJourneyVisitor visitor);

        RoutePoint Point { get; }
    }
}

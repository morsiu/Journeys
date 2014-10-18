namespace Mors.Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal sealed class JourneyStart : IJourneyEvent
    {
        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }

        public RoutePoint Point { get { return new RoutePoint(); } }
    }
}

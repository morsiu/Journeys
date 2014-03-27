namespace Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal class JourneyStart : IJourneyEvent
    {
        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }

        public RoutePoint Point { get { return new RoutePoint(); } }
    }
}

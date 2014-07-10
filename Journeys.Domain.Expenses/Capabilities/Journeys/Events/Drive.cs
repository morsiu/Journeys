namespace Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal sealed class Drive
    {
        public Drive(RouteDistance distance)
        {
            Distance = distance;
        }

        public RouteDistance Distance { get; private set; }

        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

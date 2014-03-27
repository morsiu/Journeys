namespace Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal class Drive
    {
        public Drive(Distance distance)
        {
            Distance = distance;
        }

        public Distance Distance { get; private set; }

        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

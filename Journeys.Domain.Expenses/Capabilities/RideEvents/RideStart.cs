namespace Journeys.Domain.Expenses.Capabilities.RideEvents
{
    internal class RideStart : IRideEvent
    {
        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Point Distance { get { return new Point(); } }
    }
}

namespace Journeys.Domain.Expenses.Capabilities.Journeys.Events
{
    internal class JourneyStart : IJourneyEvent
    {
        public void Visit(IJourneyVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Point Distance { get { return new Point(); } }
    }
}

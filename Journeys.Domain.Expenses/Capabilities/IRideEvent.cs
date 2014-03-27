namespace Journeys.Domain.Expenses.Capabilities
{
    internal interface IRideEvent
    {
        void Visit(IJourneyVisitor visitor);

        Point Distance { get; }
    }
}

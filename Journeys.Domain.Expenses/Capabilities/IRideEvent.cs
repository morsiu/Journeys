namespace Journeys.Domain.Expenses.Capabilities
{
    internal interface IRideEvent
    {
        void Visit(IRideVisitor visitor);

        Point Distance { get; }
    }
}


namespace Journeys.Domain.Expenses.Capabilities.RideEvents
{
    internal class Drive
    {
        public Drive(Distance distance)
        {
            Distance = distance;
        }

        public Distance Distance { get; private set; }

        public void Visit(IRideVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

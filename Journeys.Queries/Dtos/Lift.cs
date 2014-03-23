using Journeys.Common;

namespace Journeys.Queries.Dtos
{
    public class Lift
    {
        public Lift(IId passengerId, decimal distance)
        {
            PassengerId = passengerId;
            Distance = distance;
        }

        public IId PassengerId { get; private set; }

        public decimal Distance { get; private set; }
    }
}

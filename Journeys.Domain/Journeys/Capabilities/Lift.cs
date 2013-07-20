using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Markers;
using Journeys.Domain.People;

namespace Journeys.Domain.Journeys.Capabilities
{
    [ValueObject]
    public class Lift
    {
        private readonly Id _personId;
        private readonly Distance _distance;

        public Lift(Id personId, Distance distance)
        {
            _personId = personId;
            _distance = distance;
        }

        public bool EqualsByPerson(Id personId)
        {
            return _personId == personId;
        }
    }
}

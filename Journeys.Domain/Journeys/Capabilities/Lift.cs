using Journeys.Common;
using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Markers;
using Journeys.Domain.People;

namespace Journeys.Domain.Journeys.Capabilities
{
    [ValueObject]
    public class Lift
    {
        private readonly IId _personId;
        private readonly Distance _distance;

        public Lift(IId personId, Distance distance)
        {
            _personId = personId;
            _distance = distance;
        }

        public bool EqualsByPerson(IId personId)
        {
            return _personId.Equals(personId);
        }
    }
}

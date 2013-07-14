using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Markers;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.People;

namespace Journeys.Domain.Journeys.Capabilities
{
    [ValueObject]
    public class Lift
    {
        private Id<Person> _personId;
        private Distance _distance;

        public Lift(Id<Person> personId, Distance distance)
        {
            _personId = personId;
            _distance = distance;
        }

        public bool EqualsByPerson(Id<Person> personId)
        {
            return _personId == personId;
        }
    }
}

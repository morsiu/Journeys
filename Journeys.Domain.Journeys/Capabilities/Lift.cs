using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Journeys.Capabilities
{
    [ValueObject]
    internal class Lift
    {
        private readonly object _personId;

        public Lift(object personId)
        {
            _personId = personId;
        }

        public bool IsForPerson(object personId)
        {
            return _personId.Equals(personId);
        }
    }
}

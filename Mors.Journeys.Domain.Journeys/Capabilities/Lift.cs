using Mors.Journeys.Domain.Infrastructure.Markers;

namespace Mors.Journeys.Domain.Journeys.Capabilities
{
    [ValueObject]
    internal sealed class Lift
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

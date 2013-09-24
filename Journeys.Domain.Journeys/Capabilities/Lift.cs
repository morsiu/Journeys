using Journeys.Common;
using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Journeys.Capabilities
{
    [ValueObject]
    public class Lift
    {
        private readonly IId _personId;

        public Lift(IId personId)
        {
            _personId = personId;
        }

        public bool IsForPerson(IId personId)
        {
            return _personId.Equals(personId);
        }
    }
}

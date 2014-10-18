using Mors.AppPlatform.Common;
using Mors.AppPlatform.Common.Services;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Domain.Infrastructure.Exceptions;
using Mors.Journeys.Domain.Infrastructure.Markers;

namespace Mors.Journeys.Domain.People
{
    [Aggregate]
    public sealed class Person : IHasId
    {
        private readonly object _id;

        public Person(object id, string name, IEventBus eventBus)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvariantViolationException(Messages.PersonMustHaveAName);
            _id = id;
            eventBus.Publish(new PersonCreatedEvent(id, name));
        }

        object IHasId.Id
        {
            get { return _id; }
        }
    }
}

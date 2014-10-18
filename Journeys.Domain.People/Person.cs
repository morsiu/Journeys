using Journeys.Data.Events;
using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Exceptions;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.People
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

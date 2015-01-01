using System;
using Mors.Journeys.Data;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Domain.Infrastructure.Exceptions;
using Mors.Journeys.Domain.Infrastructure.Markers;

namespace Mors.Journeys.Domain.People
{
    [Aggregate]
    public sealed class Person : IHasId
    {
        private readonly object _id;

        public Person(object id, string name, Action<object> eventPublisher)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvariantViolationException(Messages.PersonMustHaveAName);
            _id = id;
            eventPublisher(new PersonCreatedEvent(id, name));
        }

        object IHasId.Id
        {
            get { return _id; }
        }
    }
}

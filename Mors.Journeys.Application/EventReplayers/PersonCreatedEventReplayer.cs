using System;
using Mors.Journeys.Application;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Domain.People;

namespace Mors.Journeys.Application.EventReplayers
{
    internal sealed class PersonCreatedEventReplayer
    {
        private readonly IRepositories _repositories;
        private readonly Action<object> _eventPublisher;

        public PersonCreatedEventReplayer(IRepositories repositories, Action<object> eventPublisher)
        {
            _repositories = repositories;
            _eventPublisher = eventPublisher;
        }

        public void Replay(PersonCreatedEvent @event)
        {
            var personId = @event.PersonId;
            var person = new Person(personId, @event.PersonName, _eventPublisher);
            _repositories.Store(person);
        }
    }
}

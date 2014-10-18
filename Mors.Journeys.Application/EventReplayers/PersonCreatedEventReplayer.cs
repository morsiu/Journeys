using Mors.AppPlatform.Common.Services;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Domain.People;

namespace Mors.Journeys.Application.EventReplayers
{
    internal sealed class PersonCreatedEventReplayer
    {
        private readonly IRepositories _repositories;
        private readonly IEventBus _eventBus;

        public PersonCreatedEventReplayer(IRepositories repositories, IEventBus eventBus)
        {
            _repositories = repositories;
            _eventBus = eventBus;
        }

        public void Replay(PersonCreatedEvent @event)
        {
            var personId = @event.PersonId;
            var person = new Person(personId, @event.PersonName, _eventBus);
            _repositories.Store(person);
        }
    }
}

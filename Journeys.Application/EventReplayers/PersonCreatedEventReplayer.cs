using Journeys.Domain.People;
using Journeys.Data.Events;

namespace Journeys.Application.EventReplayers
{
    internal class PersonCreatedEventReplayer
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
            var person = new Person(personId, @event.PersonName, _eventBus.ForDomain());
            _repositories.Store(person);
        }
    }
}

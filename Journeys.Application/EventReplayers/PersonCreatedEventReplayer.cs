using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Data;
using Journeys.Domain.People;
using Journeys.Eventing;
using Journeys.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Application.EventReplayers
{
    internal class PersonCreatedEventReplayer
    {
        private readonly IDomainRepository<Person> _personRepository;
        private readonly IEventBus _eventBus;

        public PersonCreatedEventReplayer(IDomainRepository<Person> personRepository, IEventBus eventBus)
        {
            _personRepository = personRepository;
            _eventBus = eventBus;
        }

        public void Replay(PersonCreatedEvent @event)
        {
            var personId = new Id<Person>(@event.PersonId);
            var person = new Person(personId, @event.PersonName, _eventBus);
            _personRepository.Store(person);
        }
    }
}

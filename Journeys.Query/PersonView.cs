using System;
using System.Collections.Generic;
using Journeys.Events;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using Journeys.Query.Infrastructure.Views;

namespace Journeys.Query
{
    using PersonId = Guid;

    internal class PersonView
    {
        private readonly Set<PersonId, PersonName> _peopleNames = new Set<PersonId, PersonName>(personName => personName.OwnerId);
        private readonly Set<string, PersonName> _peopleByName = new Set<string, PersonName>(personName => personName.Name);

        public Guid? Execute(GetPersonIdByNameQuery query)
        {
            var personName = _peopleByName.Get(query.PersonName, () => null);
            return personName == null ? default(Guid?) : personName.OwnerId;
        }

        public string Execute(GetPersonNameByIdQuery query)
        {
            var personName = _peopleNames.Get(query.PersonId, () => null);
            return personName == null ? default(string) : personName.Name;
        }

        public IEnumerable<PersonName> Execute(GetPeopleNamesQuery query)
        {
            return _peopleNames.Retrieve();
        }

        public void Update(PersonCreatedEvent @event)
        {
            var personName = new PersonName(@event.PersonId, @event.PersonName);
            _peopleNames.Add(personName);
            _peopleByName.Add(personName);
        }
    }
}

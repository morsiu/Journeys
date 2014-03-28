using Journeys.Common;
using Journeys.Events;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using Journeys.Query.Infrastructure.Views;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Query
{
    using PersonId = IId;

    internal class PersonView
    {
        private readonly ValueSet<PersonId, PersonName> _peopleNames = new ValueSet<PersonId, PersonName>(personName => personName.OwnerId);
        private readonly ValueSet<string, PersonName> _peopleByName = new ValueSet<string, PersonName>(personName => personName.Name);

        public IId Execute(GetPersonIdByNameQuery query)
        {
            var personName = _peopleByName.Get(query.PersonName, () => null);
            return personName == null ? null : personName.OwnerId;
        }

        public IEnumerable<PersonName> Execute(GetPeopleNamesQuery query)
        {
            return _peopleNames.Retrieve().ToList();
        }

        public void Update(PersonCreatedEvent @event)
        {
            var personName = new PersonName(@event.PersonId, @event.PersonName);
            _peopleNames.Add(personName);
            _peopleByName.Add(personName);
        }
    }
}

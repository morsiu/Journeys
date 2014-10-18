using System.Collections.Generic;
using System.Linq;
using Mors.Journeys.Application.QueryHandlers.Infrastructure.Views;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Data.Queries;
using Mors.Journeys.Data.Queries.Dtos;

namespace Mors.Journeys.Application.QueryHandlers
{
    internal sealed class PersonView
    {
        private readonly ValueSet<object, PersonName> _peopleNames = new ValueSet<object, PersonName>(personName => personName.OwnerId);
        private readonly ValueSet<string, PersonName> _peopleByName = new ValueSet<string, PersonName>(personName => personName.Name);

        public object Execute(GetPersonIdByNameQuery query)
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

using System;
using System.Collections.Generic;
using Journeys.Events;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using Journeys.Query.Infrastructure.Views;

namespace Journeys.Query
{
    internal class PersonView
    {
        private readonly Lookup<Guid, string> _peopleNames = new Lookup<Guid, string>();
        private readonly Lookup<string, Guid> _peopleByName = new Lookup<string, Guid>();

        public Guid? Execute(GetPersonIdByNameQuery query)
        {
            var result = _peopleByName.Get(query.PersonName);
            return result.HasValue ? result.Value : default(Guid?);
        }

        public string Execute(GetPersonNameQuery query)
        {
            return _peopleNames.Get(query.PersonId, () => null);
        }

        public IEnumerable<PersonName> Execute(GetPeopleNamesQuery query)
        {
            foreach (var pair in _peopleNames.Retrieve())
            {
                yield return new PersonName(pair.Key, pair.Value);
            }
        }

        public void Update(PersonCreatedEvent @event)
        {
            _peopleNames.Set(@event.PersonId, @event.PersonName);
            _peopleByName.Set(@event.PersonName, @event.PersonId);
        }
    }
}

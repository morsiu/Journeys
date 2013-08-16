using System;
using System.Collections.Generic;
using Journeys.Events;
using Journeys.Queries;

namespace Journeys.Query
{
    internal class PersonView
    {
        private readonly Dictionary<Guid, string> _personNames = new Dictionary<Guid, string>();
        private readonly Dictionary<string, Guid> _personByNameLookup = new Dictionary<string, Guid>();

        public Guid? Execute(GetIdOfPersonWithNameQuery query)
        {
            return query.PersonName != null && _personByNameLookup.ContainsKey(query.PersonName)
                ? _personByNameLookup[query.PersonName]
                : default(Guid?);
        }

        public string Execute(GetPersonNameQuery query)
        {
            return _personNames[query.PersonId];
        }

        public void Update(PersonCreatedEvent @event)
        {
            _personNames[@event.PersonId] = @event.PersonName;
            _personByNameLookup[@event.PersonName] = @event.PersonId;
        }
    }
}

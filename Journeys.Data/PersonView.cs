using Journeys.Events;
using Journeys.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Query
{
    internal class PersonView
    {
        private Dictionary<Guid, string> _personNames = new Dictionary<Guid, string>();
        private Dictionary<string, Guid> _personByNameLookup = new Dictionary<string, Guid>();

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

using Journeys.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Data
{
    internal class PersonView
    {
        private Dictionary<Guid, string> _personNames = new Dictionary<Guid, string>();
        private Dictionary<string, Guid> _personByNameLookup = new Dictionary<string, Guid>();

        public Guid? GetIdOfPersonWithName(string personName)
        {
            return _personByNameLookup.ContainsKey(personName)
                ? _personByNameLookup[personName]
                : default(Guid?);
        }

        public string GetPersonName(Guid personId)
        {
            return _personNames[personId];
        }

        public void Update(PersonCreatedEvent @event)
        {
            _personNames[@event.PersonId] = @event.PersonName;
            _personByNameLookup[@event.PersonName] = @event.PersonId;
        }
    }
}

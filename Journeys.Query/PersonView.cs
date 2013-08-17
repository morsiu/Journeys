using System;
using Journeys.Events;
using Journeys.Queries;
using Journeys.Query.Infrastructure.Views;

namespace Journeys.Query
{
    internal class PersonView
    {
        private readonly Lookup<Guid, string> _peopleNames = new Lookup<Guid, string>();
        private readonly Lookup<string, Guid> _peopleByNames = new Lookup<string, Guid>();

        public Guid? Execute(GetPersonIdByNameQuery query)
        {
            var result = _peopleByNames.Get(query.PersonName);
            return result.HasValue ? result.Value : default(Guid?);
        }

        public string Execute(GetPersonNameQuery query)
        {
            return _peopleNames.Get(query.PersonId, () => null);
        }

        public void Update(PersonCreatedEvent @event)
        {
            _peopleNames.Set(@event.PersonId, @event.PersonName);
            _peopleByNames.Set(@event.PersonName, @event.PersonId);
        }
    }
}

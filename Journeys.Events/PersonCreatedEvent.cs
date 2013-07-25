using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Events
{
    public class PersonCreatedEvent
    {
        public PersonCreatedEvent(Guid id, string name)
        {
            PersonId = id;
            PersonName = name;
        }

        public Guid PersonId { get; private set; }

        public string PersonName { get; private set; }
    }
}

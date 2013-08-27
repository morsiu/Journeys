using System;
using System.Runtime.Serialization;
using Journeys.Common;

namespace Journeys.Events
{
    [DataContract]
    public class PersonCreatedEvent
    {
        public PersonCreatedEvent(IId id, string name)
        {
            PersonId = id;
            PersonName = name;
        }

        [DataMember]
        public IId PersonId { get; private set; }

        [DataMember]
        public string PersonName { get; private set; }
    }
}

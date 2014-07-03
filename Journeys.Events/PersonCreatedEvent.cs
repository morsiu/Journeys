using System;
using System.Runtime.Serialization;

namespace Journeys.Events
{
    [DataContract]
    public class PersonCreatedEvent
    {
        public PersonCreatedEvent(object id, string name)
        {
            PersonId = id;
            PersonName = name;
        }

        [DataMember]
        public object PersonId { get; private set; }

        [DataMember]
        public string PersonName { get; private set; }
    }
}

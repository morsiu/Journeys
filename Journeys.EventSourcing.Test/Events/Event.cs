using System.Runtime.Serialization;

namespace Journeys.EventSourcing.Test.Events
{
    [DataContract]
    internal class Event
    {
        [DataMember]
        public string Field { get; private set; }

        public Event(string field)
        {
            Field = field;
        }
    }
}

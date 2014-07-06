using System.Runtime.Serialization;

namespace Journeys.Support.EventSourcing.Test.Events
{
    [DataContract]
    internal sealed class Event
    {
        [DataMember]
        public string Field { get; private set; }

        public Event(string field)
        {
            Field = field;
        }
    }
}

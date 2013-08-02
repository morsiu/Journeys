using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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

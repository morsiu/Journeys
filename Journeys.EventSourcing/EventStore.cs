using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.EventSourcing
{
    public class EventStore
    {
        private readonly FileStream _eventStream;
        private readonly XmlEventWriter _eventWriter;

        public EventStore(string fileName, IEnumerable<Type> supportedEventTypes)
        {
            _eventStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.Read);
            _eventWriter = new XmlEventWriter(_eventStream, supportedEventTypes);
        }

        public void Write<TEvent>(TEvent @event)
        {
            _eventWriter.Write(@event);
            _eventStream.Flush(true);
        }
    }
}

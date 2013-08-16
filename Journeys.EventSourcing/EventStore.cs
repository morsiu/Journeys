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
        private readonly string _fileName;
        private readonly IEnumerable<Type> _eventTypesToSupport;

        public EventStore(string fileName, IEnumerable<Type> eventTypesToSupport)
        {
            _fileName = fileName;
            _eventTypesToSupport = eventTypesToSupport;
        }

        public IEnumerable<object> GetReader()
        {
            FileStream stream;
            try
            {
                stream = new FileStream(_fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (FileNotFoundException)
            {
                yield break;
            }

            using (stream)
            {
                var reader = new XmlEventReader(stream, _eventTypesToSupport);
                while (!reader.IsAtEnd)
                {
                    yield return reader.Read();
                }
            }
        }

        public IEventWriter GetWriter()
        {
            return new EventWriter(_fileName, _eventTypesToSupport);
        }

        private class EventWriter : IEventWriter
        {
            private readonly XmlEventWriter _writer;

            public EventWriter(string fileName, IEnumerable<Type> eventTypesToSupport)
            {
                var stream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.Read);
                _writer = new XmlEventWriter(stream, eventTypesToSupport);
            }

            public void Write<TEvent>(TEvent @event)
            {
                _writer.Write(@event);
            }
        }
    }
}

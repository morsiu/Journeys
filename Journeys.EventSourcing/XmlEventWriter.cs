using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.EventSourcing
{
    public class XmlEventWriter
    {
        private DataContractSerializer _serializer;
        private Stream _stream;

        public XmlEventWriter(Stream stream, IEnumerable<Type> eventTypesToSupport)
        {
            _stream = stream;
            _serializer = new DataContractSerializer(typeof(object), eventTypesToSupport);
        }

        public void Write(object @event)
        {
            _serializer.WriteObject(_stream, @event);
        }
    }
}

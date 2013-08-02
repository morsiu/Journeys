using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.EventSourcing
{
    public class XmlEventReader
    {
        private DataContractSerializer _serializer;
        private Stream _stream;

        public XmlEventReader(Stream stream, IEnumerable<Type> eventTypesToSupport)
        {
            _stream = stream;
            _serializer = new DataContractSerializer(typeof(object), eventTypesToSupport);
        }

        public object Read()
        {
            return _serializer.ReadObject(_stream);
        }
    }
}

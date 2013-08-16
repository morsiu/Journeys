using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Journeys.EventSourcing
{
    internal class XmlEventReader
    {
        private readonly DataContractSerializer _serializer;
        private readonly XmlReader _reader;
        private readonly Stream _stream;

        public XmlEventReader(Stream stream, IEnumerable<Type> eventTypesToSupport)
        {
            _stream = stream;
            _reader = XmlReader.Create(
                _stream,
                new XmlReaderSettings
                {
                    ConformanceLevel = ConformanceLevel.Fragment
                });
            _serializer = new DataContractSerializer(typeof(object), "event", string.Empty, eventTypesToSupport);
        }

        public object Read()
        {
            return _serializer.ReadObject(_reader);
        }

        public bool IsAtEnd
        {
            get
            {
                return _stream.Length == 0 || _reader.EOF;
            }
        }
    }
}

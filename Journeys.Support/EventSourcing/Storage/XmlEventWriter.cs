using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Journeys.Support.EventSourcing.Storage
{
    internal sealed class XmlEventWriter
    {
        private readonly DataContractSerializer _serializer;
        private readonly XmlWriter _writer;
        private readonly Stream _stream;

        public XmlEventWriter(Stream stream, IEnumerable<Type> eventTypesToSupport)
        {
            _stream = stream;
            _writer = XmlWriter.Create(
                _stream,
                new XmlWriterSettings 
                {
                    ConformanceLevel = ConformanceLevel.Fragment,
                    Indent = true,
                    IndentChars = "  ",
                    NewLineOnAttributes = true,
                    WriteEndDocumentOnClose = true 
                });
            _serializer = new DataContractSerializer(typeof(object), "event", string.Empty, eventTypesToSupport);
        }

        public void Write(object @event)
        {
            _serializer.WriteObject(_writer, @event);
            _writer.Flush();
            _stream.Flush();
        }
    }
}

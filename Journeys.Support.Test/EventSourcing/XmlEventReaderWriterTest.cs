using System.IO;
using Journeys.Support.EventSourcing.Storage;
using Journeys.Support.EventSourcing.Test.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Journeys.Support.EventSourcing.Test
{
    [TestClass]
    public sealed class XmlEventReaderWriterTest
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void ShouldWriteThenReadOneEvent()
        {
            var eventTypesToSupport = new[] { typeof(Event) };
            var stream = new MemoryStream();
            var writer = new XmlEventWriter(stream, eventTypesToSupport);
            var @event = new Event("Value");
            writer.Write(@event);

            stream.Seek(0, SeekOrigin.Begin);
            var reader = new XmlEventReader(stream, eventTypesToSupport);
            var readEvent = (Event)reader.Read();
            Assert.AreEqual(@event.Field, readEvent.Field);
        }
    }
}

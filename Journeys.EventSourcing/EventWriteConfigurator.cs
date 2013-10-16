using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.EventSourcing
{
    internal class EventWriteConfigurator
    {
        private readonly HashSet<Action<IEventWriter, IEventBus>> _writerConfigurators = new HashSet<Action<IEventWriter, IEventBus>>();

        public void Add<TEvent>(Action<TEvent> replayHandler)
        {
            var eventType = typeof(TEvent);
            _writerConfigurators.Add((writer, bus) => bus.RegisterListener<TEvent>(writer.Write));
        }

        public void Configure(IEventBus eventBus, IEventWriter eventWriter)
        {
            foreach (var eventBusConfigurator in _writerConfigurators)
            {
                eventBusConfigurator(eventWriter, eventBus);
            }
        }
    }
}

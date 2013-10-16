using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.EventSourcing
{
    internal class EventWriteConfigurator
    {
        private readonly HashSet<Action<IEventBus, IEventWriter>> _eventWriteConfigurators = new HashSet<Action<IEventBus, IEventWriter>>();

        public void Add<TEvent>(Action<TEvent> replayHandler)
        {
            _eventWriteConfigurators.Add(ConfigureEventWrite<TEvent>);
        }

        public void Configure(IEventBus eventBus, IEventWriter eventWriter)
        {
            foreach (var eventWriteConfigurator in _eventWriteConfigurators)
            {
                eventWriteConfigurator(eventBus, eventWriter);
            }
        }

        private static void ConfigureEventWrite<TEvent>(IEventBus eventBus, IEventWriter eventWriter)
        {
            eventBus.RegisterListener<TEvent>(eventWriter.Write<TEvent>);
        }
    }
}

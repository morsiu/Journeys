using System;
using System.Collections.Generic;

namespace Journeys.Support.EventSourcing
{
    internal sealed class EventWriteConfigurator
    {
        private readonly HashSet<Action<IEventBus, IEventWriter>> _eventWriteConfigurators = new HashSet<Action<IEventBus, IEventWriter>>();

        public void Add<TEvent>()
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
            eventBus.RegisterListener<TEvent>(eventWriter.Write);
        }
    }
}

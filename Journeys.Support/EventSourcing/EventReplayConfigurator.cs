using System;
using System.Collections.Generic;

namespace Journeys.Support.EventSourcing
{
    internal sealed class EventReplayConfigurator
    {
        private readonly HashSet<Action<EventReplayer>> _eventReplayConfigurators = new HashSet<Action<EventReplayer>>();

        public void Add<TEvent>(Action<TEvent> eventReplayHandler)
        {
            _eventReplayConfigurators.Add(eventReplayer => eventReplayer.Register(eventReplayHandler));
        }

        public void Configure(EventReplayer eventReplayer)
        {
            foreach (var eventReplayConfigurator in _eventReplayConfigurators)
            {
                eventReplayConfigurator(eventReplayer);
            }
        }
    }
}

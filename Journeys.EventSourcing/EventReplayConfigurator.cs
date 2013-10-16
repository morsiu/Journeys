using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.EventSourcing
{
    internal class EventReplayConfigurator
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

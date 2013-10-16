using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.EventSourcing
{
    internal class EventReplayConfigurator
    {
        private readonly HashSet<Action<EventReplayer>> _replayerConfigurators = new HashSet<Action<EventReplayer>>();

        public void Add<TEvent>(Action<TEvent> replayHandler)
        {
            var eventType = typeof(TEvent);
            _replayerConfigurators.Add(replayer => replayer.Register(replayHandler));
        }

        public void Configure(EventReplayer eventReplayer)
        {
            foreach (var replayerConfigurator in _replayerConfigurators)
            {
                replayerConfigurator(eventReplayer);
            }
        }
    }
}

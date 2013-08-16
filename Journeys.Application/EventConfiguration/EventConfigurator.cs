using Journeys.Eventing;
using Journeys.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Application.EventConfiguration
{
    [Flags]
    internal enum EventRegistrationOptions
    {
        Store,
        Replay,
        StoreAndReplay = Store | Replay
    }

    internal class EventConfigurator
    {
        private readonly HashSet<Type> _typesOfEventsToStore = new HashSet<Type>();
        private readonly HashSet<Action<EventReplayer>> _replayerConfigurators = new HashSet<Action<EventReplayer>>();
        private readonly HashSet<Action<IEventWriter, EventBus>> _writerConfigurators = new HashSet<Action<IEventWriter, EventBus>>();

        public void Register<TEvent>(Action<TEvent> replayHandler)
        {
            var eventType = typeof(TEvent);
            _replayerConfigurators.Add(replayer => replayer.Register<TEvent>(replayHandler));
            _writerConfigurators.Add((writer, bus) => bus.RegisterListener<TEvent>(writer.Write));
            _typesOfEventsToStore.Add(eventType);
        }

        public IEnumerable<Type> GetEventTypesForStoring()
        {
            return _typesOfEventsToStore;
        }

        public void ConfigureReplayer(EventReplayer replayer)
        {
            foreach (var replayerConfigurator in _replayerConfigurators)
            {
                replayerConfigurator(replayer);
            }
        }

        public void ConfigureWriter(IEventWriter writer, EventBus eventBus)
        {
            foreach (var eventBusConfigurator in _writerConfigurators)
            {
                eventBusConfigurator(writer, eventBus);
            }
        }
    }
}

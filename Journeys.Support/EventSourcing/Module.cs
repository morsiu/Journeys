using System;
using System.Collections.Generic;
using Journeys.Support.EventSourcing.Storage;

namespace Journeys.Support.EventSourcing
{
    public sealed class Module
    {
        private readonly string _eventsFileName;
        private readonly HashSet<Type> _typesOfEventsToStore = new HashSet<Type>();
        private readonly EventReplayConfigurator _eventReplayConfigurator = new EventReplayConfigurator();
        private readonly EventWriteConfigurator _eventWriteConfigurator = new EventWriteConfigurator();
        private readonly IEventBus _eventBus;
        private readonly Type _idImplementationType;

        public Module(IEventBus eventBus, Type idImplementationType, string eventsFileName)
        {
            _eventBus = eventBus;
            _eventsFileName = eventsFileName;
            _idImplementationType = idImplementationType;
        }
     
        public void ReplayEvents()
        {
            var eventStore = GetEventStore();
            var storedEvents = eventStore.GetReader();
            var eventReplayer = GetReplayer();
            eventReplayer.Replay(storedEvents);
        }

        public void StoreNewEvents()
        {
            var eventStore = GetEventStore();
            var eventWriter = eventStore.GetWriter();
            _eventWriteConfigurator.Configure(_eventBus, eventWriter);
        }

        public void Register<TEvent>(Action<TEvent> replayHandler)
        {
            var eventType = typeof(TEvent);
            _eventWriteConfigurator.Add<TEvent>(replayHandler);
            _eventReplayConfigurator.Add<TEvent>(replayHandler);
            _typesOfEventsToStore.Add(eventType);
        }

        private EventStore GetEventStore()
        {
            var eventStore = new EventStore(_eventsFileName, GetSupportedEventTypes());
            return eventStore;
        }

        private IEnumerable<Type> GetSupportedEventTypes()
        {
            foreach (var eventType in _typesOfEventsToStore)
            {
                yield return eventType;
            }
            yield return _idImplementationType;
        }

        private EventReplayer GetReplayer()
        {
            var replayer = new EventReplayer();
            _eventReplayConfigurator.Configure(replayer);
            return replayer;
        }
    }
}

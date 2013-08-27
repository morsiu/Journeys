using System;
using System.Collections.Generic;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using Journeys.Events;
using Journeys.EventSourcing.Replayers;

namespace Journeys.EventSourcing
{
    public class Bootstrapper
    {
        private readonly string _eventsFileName;
        private readonly HashSet<Type> _typesOfEventsToStore = new HashSet<Type>();
        private readonly HashSet<Action<EventReplayer>> _replayerConfigurators = new HashSet<Action<EventReplayer>>();
        private readonly HashSet<Action<IEventWriter, IEventBus>> _writerConfigurators = new HashSet<Action<IEventWriter, IEventBus>>();
        private readonly IEventBus _eventBus;
        private readonly IRepositories _repositories;
        private Type _idImplementationType;

        public Bootstrapper(IEventBus eventBus, IRepositories repositories, Type idImplementationType, string eventsFileName)
        {
            _repositories = repositories;
            _eventBus = eventBus;
            _eventsFileName = eventsFileName;
            _idImplementationType = idImplementationType;
        }

        public void Bootstrap()
        {
            ConfigureEvents();
            ReplayEvents();
            StoreNewEvents();
        }

        private void ReplayEvents()
        {
            var eventStore = GetEventStore();
            var storedEvents = eventStore.GetReader();
            var eventReplayer = GetReplayer();
            eventReplayer.Replay(storedEvents);
        }

        private void StoreNewEvents()
        {
            var eventStore = GetEventStore();
            var eventWriter = eventStore.GetWriter();
            foreach (var eventBusConfigurator in _writerConfigurators)
            {
                eventBusConfigurator(eventWriter, _eventBus);
            }
        }

        private void ConfigureEvents()
        {
            Register<JourneyCreatedEvent>(new JourneyCreatedEventReplayer(_repositories, _eventBus).Replay);
            Register<LiftAddedEvent>(new LiftAddedEventReplayer(_repositories).Replay);
            Register<PersonCreatedEvent>(new PersonCreatedEventReplayer(_repositories, _eventBus).Replay);
        }

        private void Register<TEvent>(Action<TEvent> replayHandler)
        {
            var eventType = typeof(TEvent);
            _replayerConfigurators.Add(replayer => replayer.Register(replayHandler));
            _writerConfigurators.Add((writer, bus) => bus.RegisterListener<TEvent>(writer.Write));
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
            foreach (var replayerConfigurator in _replayerConfigurators)
            {
                replayerConfigurator(replayer);
            }
            return replayer;
        }
    }
}

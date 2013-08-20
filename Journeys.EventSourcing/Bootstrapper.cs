using System;
using System.Collections.Generic;
using Journeys.Domain.Infrastructure.Repositories;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using Journeys.Eventing;
using Journeys.Events;
using Journeys.EventSourcing.Replayers;

namespace Journeys.EventSourcing
{
    public class Bootstrapper
    {
        private readonly string _eventsFileName;
        private readonly HashSet<Type> _typesOfEventsToStore = new HashSet<Type>();
        private readonly HashSet<Action<EventReplayer>> _replayerConfigurators = new HashSet<Action<EventReplayer>>();
        private readonly HashSet<Action<IEventWriter, EventBus>> _writerConfigurators = new HashSet<Action<IEventWriter, EventBus>>();
        private readonly EventBus _eventBus;
        private readonly IDomainRepositories _domainRepositories;

        public Bootstrapper(EventBus eventBus, IDomainRepositories domainRepositories, string eventsFileName)
        {
            _domainRepositories = domainRepositories;
            _eventBus = eventBus;
            _eventsFileName = eventsFileName;
        }

        public void Bootstrap()
        {
            ConfigureEvents();
            ReplayEvents();
            StoreNewEvents();
        }

        private void ConfigureEvents()
        {
            Register<JourneyCreatedEvent>(new JourneyCreatedEventReplayer(_domainRepositories.Get<Journey>(), _eventBus).Replay);
            Register<LiftAddedEvent>(new LiftAddedEventReplayer(_domainRepositories.Get<Journey>()).Replay);
            Register<PersonCreatedEvent>(new PersonCreatedEventReplayer(_domainRepositories.Get<Person>(), _eventBus).Replay);
        }

        private void ReplayEvents()
        {
            var eventStore = GetEventStore();
            var storedEvents = eventStore.GetReader();
            var eventReplayer = GetReplayer();
            eventReplayer.Replay(storedEvents);
        }

        private void Register<TEvent>(Action<TEvent> replayHandler)
        {
            var eventType = typeof(TEvent);
            _replayerConfigurators.Add(replayer => replayer.Register(replayHandler));
            _writerConfigurators.Add((writer, bus) => bus.RegisterListener<TEvent>(writer.Write));
            _typesOfEventsToStore.Add(eventType);
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

        private EventStore GetEventStore()
        {
            var eventStore = new EventStore(_eventsFileName, _typesOfEventsToStore);
            return eventStore;
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

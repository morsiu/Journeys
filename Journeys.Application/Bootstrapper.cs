using System;
using Journeys.Application.Commands;
using Journeys.Application.Repositories;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.Infrastructure;
using Journeys.Eventing;
using Journeys.Transactions;
using Journeys.Commands;
using Journeys.Application.CommandHandlers;
using Journeys.Data;
using Journeys.Domain.People;
using Journeys.EventSourcing;
using Journeys.Events;
using System.Collections.Generic;
using Journeys.Application.EventReplayers;
using Journeys.Application.EventConfiguration;

namespace Journeys.Application
{
    public class Bootstrapper
    {
        private const string EventsFileName = "Events.xml";

        private readonly EventBus _eventBus;
        private readonly EventConfigurator _eventConfigurator;
        private readonly DomainRepository<Journey> _journeyRepository;
        private readonly DomainRepository<Person> _personRepository;

        public Bootstrapper(EventBus eventBus)
        {
            _eventBus = eventBus;
            _journeyRepository = new DomainRepository<Journey>();
            _personRepository = new DomainRepository<Person>();
            _eventConfigurator = GetEventConfigurator();
        }

        public ICommandDispatcher CommandDispatcher { get; private set; }

        public void Bootstrap(IQueryDispatcher queryDispatcher)
        {
            SetupCommandHandling(queryDispatcher);
            ReplayStoredEvents();
            SetupStoringOfEvents();
        }

        private void SetupCommandHandling(IQueryDispatcher queryDispatcher)
        {
            var commandProcessor = new CommandProcessor();
            commandProcessor.SetHandler<AddJourneyCommand>(cmd =>
                RunInTransaction(
                    (tEventBus, tJourneyRepository, tPersonRepository) => new AddJourneyCommandHandler(tEventBus, tPersonRepository, queryDispatcher).Execute(cmd, tJourneyRepository),
                    _eventBus, _journeyRepository, _personRepository));

            CommandDispatcher = new CommandDispatcher(commandProcessor);
        }

        private EventConfigurator GetEventConfigurator()
        {
            var configurator = new EventConfigurator();
            configurator.Register<JourneyCreatedEvent>(new JourneyCreatedEventReplayer(_journeyRepository, _eventBus).Replay);
            configurator.Register<LiftAddedEvent>(new LiftAddedEventReplayer(_journeyRepository).Replay);
            configurator.Register<PersonCreatedEvent>(new PersonCreatedEventReplayer(_personRepository, _eventBus).Replay);
            return configurator;
        }

        private void ReplayStoredEvents()
        {
            var eventStore = GetEventStore();
            var storedEvents = eventStore.GetReader();
            var eventReplayer = new EventReplayer();
            _eventConfigurator.ConfigureReplayer(eventReplayer);
            eventReplayer.Replay(storedEvents);
        }

        private void SetupStoringOfEvents()
        {
            var eventStore = GetEventStore();
            var eventWriter = eventStore.GetWriter();
            _eventConfigurator.ConfigureWriter(eventWriter, _eventBus);
        }

        private EventStore GetEventStore()
        {
            var eventTypesToStore = _eventConfigurator.GetEventTypesForStoring();
            var eventStore = new EventStore(EventsFileName, eventTypesToStore);
            return eventStore;
        }

        private static void RunInTransaction<TA, TB, TC>(
            Action<TA, TB, TC> commandHandler,
            IProvideTransacted<TA> a,
            IProvideTransacted<TB> b,
            IProvideTransacted<TC> c)
        {
            var transactedA = a.Lift();
            var transactedB = b.Lift();
            var transactedC = c.Lift();
            try
            {
                commandHandler(transactedA.Object, transactedB.Object, transactedC.Object);
            }
            catch (Exception)
            {
                transactedA.Abort();
                transactedB.Abort();
                transactedC.Abort();
                throw;
            }
            transactedA.Commit();
            transactedB.Commit();
            transactedC.Commit();
        }
    }
}

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

namespace Journeys.Application
{
    public class Bootstrapper
    {
        private const string EventsFileName = "Events.xml";
        private static readonly IEnumerable<Type> TypesOfEventsToPersist =
            new Type[]
            {
                typeof(JourneyCreatedEvent), typeof(LiftAddedEvent), typeof(PersonCreatedEvent)
            };

        private readonly EventBus _eventBus;
        private readonly DomainRepository<Journey> _journeyRepository;
        private readonly DomainRepository<Person> _personRepository;

        public Bootstrapper(EventBus eventBus)
        {
            _eventBus = eventBus;
            _journeyRepository = new DomainRepository<Journey>();
            _personRepository = new DomainRepository<Person>();
        }

        public ICommandDispatcher CommandDispatcher { get; private set; }

        public void Bootstrap(IQueryDispatcher queryDispatcher)
        {
            var commandProcessor = new CommandProcessor();

            commandProcessor.SetHandler<AddJourneyCommand>(cmd => 
                RunInTransaction(
                    (tEventBus, tJourneyRepository, tPersonRepository) => new AddJourneyCommandHandler(tEventBus, tPersonRepository, queryDispatcher).Execute(cmd, tJourneyRepository),
                    _eventBus, _journeyRepository, _personRepository));

            var eventStore = new EventStore(EventsFileName, TypesOfEventsToPersist);
            ReplayStoredEvents(eventStore);
            SetupStoringOfEvents(eventStore);

            CommandDispatcher = new CommandDispatcher(commandProcessor);
        }

        private void ReplayStoredEvents(EventStore eventStore)
        {
            var storedEvents = eventStore.GetReader();
            var eventReplayer = new EventReplayer();
            eventReplayer.Register<JourneyCreatedEvent>(new JourneyCreatedEventReplayer(_journeyRepository, _eventBus).Replay);
            eventReplayer.Register<LiftAddedEvent>(new LiftAddedEventReplayer(_journeyRepository).Replay);
            eventReplayer.Register<PersonCreatedEvent>(new PersonCreatedEventReplayer(_personRepository, _eventBus).Replay);
            eventReplayer.Replay(storedEvents);
        }

        private void SetupStoringOfEvents(EventStore eventStore)
        {
            var eventWriter = eventStore.GetWriter();
            _eventBus.RegisterListener<JourneyCreatedEvent>(eventWriter.Write);
            _eventBus.RegisterListener<LiftAddedEvent>(eventWriter.Write);
            _eventBus.RegisterListener<PersonCreatedEvent>(eventWriter.Write);
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

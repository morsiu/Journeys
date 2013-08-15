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
        private readonly EventBus _eventBus;

        private static readonly IEnumerable<Type> TypesOfEventToPersist =
            new Type[]
            {
                typeof(JourneyCreatedEvent), typeof(LiftAddedEvent), typeof(PersonCreatedEvent)
            };

        public Bootstrapper(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public ICommandDispatcher CommandDispatcher { get; private set; }

        public void Bootstrap(IQueryDispatcher queryDispatcher)
        {
            var commandProcessor = new CommandProcessor();
            var journeyRepository = new DomainRepository<Journey>();
            var personRepository = new DomainRepository<Person>();

            commandProcessor.SetHandler<AddJourneyCommand>(cmd => 
                RunInTransaction(
                    (tEventBus, tJourneyRepository, tPersonRepository) => new AddJourneyCommandHandler(tEventBus, tPersonRepository, queryDispatcher).Execute(cmd, tJourneyRepository),
                    _eventBus, journeyRepository, personRepository));

            var eventReplayer = new EventReplayer("Events.xml", TypesOfEventToPersist);
            eventReplayer.Register<JourneyCreatedEvent>(new JourneyCreatedEventReplayer(journeyRepository, _eventBus).Replay);
            eventReplayer.Register<LiftAddedEvent>(new LiftAddedEventReplayer(journeyRepository).Replay);
            eventReplayer.Register<PersonCreatedEvent>(new PersonCreatedEventReplayer(personRepository, _eventBus).Replay);
            eventReplayer.Replay();
            eventReplayer.Close();

            var eventStore = new EventStore("Events.xml", TypesOfEventToPersist);
            _eventBus.RegisterListener<JourneyCreatedEvent>(eventStore.Write);
            _eventBus.RegisterListener<LiftAddedEvent>(eventStore.Write);
            _eventBus.RegisterListener<PersonCreatedEvent>(eventStore.Write);

            CommandDispatcher = new CommandDispatcher(commandProcessor);
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

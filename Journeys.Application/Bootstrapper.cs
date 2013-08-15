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

namespace Journeys.Application
{
    public class Bootstrapper
    {
        private readonly EventBus _eventBus;

        public Bootstrapper(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public ICommandDispatcher CommandDispatcher { get; private set; }

        public void Bootstrap(IQueryDispatcher queryDispatcher)
        {
            var commandProcessor = new CommandProcessor();
            var eventStore = new EventStore("Events.xml", new[] { typeof(JourneyCreatedEvent) });
            var journeyRepository = new DomainRepository<Journey>();
            var personRepository = new DomainRepository<Person>();

            commandProcessor.SetHandler<AddJourneyCommand>(cmd => 
                RunInTransaction(
                    (tEventBus, tJourneyRepository, tPersonRepository) => new AddJourneyCommandHandler(tEventBus, tPersonRepository, queryDispatcher).Execute(cmd, tJourneyRepository),
                    _eventBus, journeyRepository, personRepository));

            _eventBus.RegisterListener<JourneyCreatedEvent>(eventStore.Write);

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

using System;
using Journeys.Command.Infrastructure;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.Infrastructure;
using Journeys.Eventing;
using Journeys.Transactions;
using Journeys.Commands;
using Journeys.Command.CommandHandlers;
using Journeys.Data;
using Journeys.Domain.People;
using Journeys.EventSourcing;
using Journeys.Events;
using System.Collections.Generic;
using Journeys.Domain.Repositories;
using Journeys.Query;

namespace Journeys.Command
{
    public class Bootstrapper
    {
        private const string EventsFileName = "Events.xml";

        private readonly EventBus _eventBus;
        private readonly DomainRepositories _domainRepositories;

        public Bootstrapper(EventBus eventBus, DomainRepositories domainRepositories)
        {
            _eventBus = eventBus;
            _domainRepositories = domainRepositories;
        }

        public ICommandDispatcher CommandDispatcher { get; private set; }

        public void Bootstrap(IQueryDispatcher queryDispatcher)
        {
            var commandProcessor = new CommandProcessor();
            commandProcessor.SetHandler<AddJourneyCommand>(cmd =>
                RunInTransaction(
                    (tEventBus, tJourneyRepository, tPersonRepository) => new AddJourneyCommandHandler(tEventBus, tPersonRepository, queryDispatcher).Execute(cmd, tJourneyRepository),
                    _eventBus, _domainRepositories.Get<Journey>(), _domainRepositories.Get<Person>()));

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

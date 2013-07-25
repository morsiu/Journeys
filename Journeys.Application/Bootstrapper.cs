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
            var journeyRepository = new DomainRepository<Journey>();
            var personRepository = new DomainRepository<Person>();

            commandProcessor.SetHandler<AddJourneyCommand>(cmd => RunInTransaction(AddJourneyCommandHandler.Execute, cmd, _eventBus, journeyRepository, personRepository, queryDispatcher));

            CommandDispatcher = new CommandDispatcher(commandProcessor);
        }

        private static void RunInTransaction<TCommand, TA, TB, TC, TD>(
            Action<TCommand, TA, TB, TC, TD> commandHandler,
            TCommand command,
            IProvideTransacted<TA> a,
            IProvideTransacted<TB> b,
            IProvideTransacted<TC> c,
            TD d)
        {
            var transactedA = a.Lift();
            var transactedB = b.Lift();
            var transactedC = c.Lift();
            try
            {
                commandHandler(command, transactedA.Object, transactedB.Object, transactedC.Object, d);
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

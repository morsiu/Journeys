using System;
using Journeys.Application.Commands;
using Journeys.Application.Infrastructure;
using Journeys.Application.Infrastructure.Commands;
using Journeys.Application.Infrastructure.Events;
using Journeys.Application.Infrastructure.Transactions;
using Journeys.Domain.Journeys.Operations;

namespace Journeys.Application
{
    public class Bootstrapper
    {
        public ICommandDispatcher CommandDispatcher { get; private set; }

        public void Bootstrap()
        {
            var commandProcessor = new CommandProcessor();
            var eventBus = new EventBus();

            var journeyRepository = new DomainRepository<Journey>();

            commandProcessor.SetHandler<AddJourneyCommand>(cmd => RunInTransaction(cmd.Execute, eventBus, journeyRepository));

            CommandDispatcher = new CommandDispatcher(commandProcessor);
        }

        private static void RunInTransaction<TA, TB>(
            Action<TA, TB> action,
            IProvideTransacted<TA> a,
            IProvideTransacted<TB> b)
        {
            var transactedA = a.Escalate();
            var transactedB = b.Escalate();
            try
            {
                action(transactedA.Object, transactedB.Object);
            }
            catch (Exception)
            {
                transactedA.Abort();
                transactedB.Abort();
                throw;
            }
            transactedA.Commit();
            transactedB.Commit();
        }
    }
}

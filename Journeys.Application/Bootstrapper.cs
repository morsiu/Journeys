using System;
using Journeys.Application.Commands;
using Journeys.Application.Infrastructure.Commands;
using Journeys.Application.Infrastructure.Repositories;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.Infrastructure;
using Journeys.Eventing;
using Journeys.Transactions;

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

        public void Bootstrap()
        {
            var commandProcessor = new CommandProcessor();

            var journeyRepository = new DomainRepository<Journey>();

            commandProcessor.SetHandler<AddJourneyCommand>(cmd => RunInTransaction(cmd.Execute, _eventBus, journeyRepository));

            CommandDispatcher = new CommandDispatcher(commandProcessor);
        }

        private static void RunInTransaction<TA, TB>(
            Action<TA, TB> action,
            IProvideTransacted<TA> a,
            IProvideTransacted<TB> b)
        {
            var transactedA = a.Lift();
            var transactedB = b.Lift();
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

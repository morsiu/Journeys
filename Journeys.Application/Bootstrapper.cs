using System;
using Journeys.Application.Commands;
using Journeys.Application.Infrastructure.Commands;
using Journeys.Application.Infrastructure.Repositories;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.Infrastructure;
using Journeys.Eventing;
using Journeys.Transactions;
using Journeys.Commands;

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

            commandProcessor.SetHandler<AddJourneyCommand>(cmd => RunInTransaction(AddJourneyCommandHandler.Execute, cmd, _eventBus, journeyRepository));

            CommandDispatcher = new CommandDispatcher(commandProcessor);
        }

        private static void RunInTransaction<TCommand, TA, TB>(
            Action<TCommand, TA, TB> commandHandler,
            TCommand command,
            IProvideTransacted<TA> a,
            IProvideTransacted<TB> b)
        {
            var transactedA = a.Lift();
            var transactedB = b.Lift();
            try
            {
                commandHandler(command, transactedA.Object, transactedB.Object);
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

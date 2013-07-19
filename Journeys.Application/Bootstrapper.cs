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
            var eventBus = new TransactedEventBus(new EventBus());

            var journeyRepository = new TransactedDomainRepository<Journey>(new DomainRepository<Journey>());

            commandProcessor.SetHandler<AddJourneyCommand>(cmd => InTransaction(() => cmd.Execute(eventBus, journeyRepository), eventBus, journeyRepository));

            CommandDispatcher = new CommandDispatcher(commandProcessor);
        }

        private static void InTransaction(Action action, params ISupportTransaction[] transactables)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                foreach (var transactable in transactables)
                {
                    transactable.Abort();
                }
                throw;
            }
            foreach (var transactable in transactables)
            {
                transactable.Commit();
            }
        }
    }
}

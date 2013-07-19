using Journeys.Application.Commands;
using Journeys.Application.Infrastructure;
using Journeys.Application.Infrastructure.Commands;
using Journeys.Application.Infrastructure.Events;
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

            commandProcessor.SetHandler<AddJourneyCommand>(cmd => cmd.Execute(eventBus, journeyRepository));

            CommandDispatcher = new CommandDispatcher(commandProcessor);
        }
    }
}

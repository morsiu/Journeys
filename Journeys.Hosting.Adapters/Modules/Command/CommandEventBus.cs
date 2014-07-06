using Journeys.Application.Command;
using Journeys.Hosting.Adapters.Modules.Domain;
using Journeys.Support.Transactions;
using Implementation = Journeys.Support.Events;

namespace Journeys.Hosting.Adapters.Modules.Command
{
    public class CommandEventBus : IEventBus
    {
        private readonly Implementation.IEventBus _eventBus;

        public CommandEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Journeys.Domain.Infrastructure.IEventBus ForDomain()
        {
            return new DomainEventBusAdapter(_eventBus);
        }

        public ITransactional<IEventBus> Lift()
        {
            return new CommandTransactedEventBus(_eventBus);
        }
    }
}

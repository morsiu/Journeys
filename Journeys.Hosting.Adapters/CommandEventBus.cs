using Journeys.Application.Command;
using Mors.Support.Transactions;
using Implementation = Mors.Support.Events;

namespace Journeys.Application.Adapters
{
    public class CommandEventBus : IEventBus
    {
        private readonly Implementation.IEventBus _eventBus;

        public CommandEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Domain.Infrastructure.IEventBus ForDomain()
        {
            return new DomainEventBusAdapter(_eventBus);
        }

        public ITransactional<IEventBus> Lift()
        {
            return new CommandTransactedEventBus(_eventBus);
        }
    }
}

using Journeys.Command;
using Journeys.Transactions;

namespace Journeys.Adapters
{
    public class CommandEventBus : IEventBus
    {
        private readonly Event.IEventBus _eventBus;

        public CommandEventBus(Event.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Domain.IEventBus ForDomain()
        {
            return new DomainEventBusAdapter(_eventBus);
        }

        public ITransactional<IEventBus> Lift()
        {
            return new CommandTransactedEventBus(_eventBus);
        }
    }
}

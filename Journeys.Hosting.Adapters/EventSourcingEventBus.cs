using Journeys.Application.EventSourcing;
using Mors.Support.Transactions;
using Implementation = Mors.Support.Events;

namespace Journeys.Application.Adapters
{
    public class EventSourcingEventBus : IEventBus
    {
        private readonly Implementation.IEventBus _eventBus;

        public EventSourcingEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Domain.Infrastructure.IEventBus ForDomain()
        {
            return new DomainEventBusAdapter(_eventBus);
        }

        public ITransactional<IEventBus> Lift()
        {
            return new EventSourcingTransactedEventBus(_eventBus);
        }
    }
}

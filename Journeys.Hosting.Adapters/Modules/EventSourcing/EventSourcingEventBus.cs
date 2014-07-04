using Journeys.Application.EventSourcing;
using Journeys.Hosting.Adapters.Modules.Domain;
using Mors.Support.Transactions;
using Implementation = Mors.Support.Events;

namespace Journeys.Hosting.Adapters.Modules.EventSourcing
{
    public class EventSourcingEventBus : IEventBus
    {
        private readonly Implementation.IEventBus _eventBus;

        public EventSourcingEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Journeys.Domain.Infrastructure.IEventBus ForDomain()
        {
            return new DomainEventBusAdapter(_eventBus);
        }

        public ITransactional<IEventBus> Lift()
        {
            return new EventSourcingTransactedEventBus(_eventBus);
        }
    }
}

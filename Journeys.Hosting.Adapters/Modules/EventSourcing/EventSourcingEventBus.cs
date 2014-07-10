using Journeys.Application.EventSourcing;
using Journeys.Hosting.Adapters.Modules.Domain;
using Journeys.Support.Transactions;
using Implementation = Journeys.Support.Events;

namespace Journeys.Hosting.Adapters.Modules.EventSourcing
{
    public sealed class EventSourcingEventBus : IEventBus
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

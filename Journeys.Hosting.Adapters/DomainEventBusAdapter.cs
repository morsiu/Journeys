using Implementation = Mors.Support.Events;

namespace Journeys.Hosting.Adapters
{
    public class DomainEventBusAdapter : Journeys.Domain.Infrastructure.IEventBus
    {
        private readonly Implementation.IEventBus _eventBus;

        public DomainEventBusAdapter(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Publish<TEvent>(TEvent @event)
        {
            _eventBus.Publish(@event);
        }
    }
}

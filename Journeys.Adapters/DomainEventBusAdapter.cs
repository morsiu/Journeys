using Journeys.Domain;

namespace Journeys.Adapters
{
    public class DomainEventBusAdapter : IEventBus
    {
        private readonly Event.IEventBus _eventBus;

        public DomainEventBusAdapter(Event.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Publish<TEvent>(TEvent @event)
        {
            _eventBus.Publish<TEvent>(@event);
        }
    }
}

using Journeys.Eventing;

namespace Journeys.Client.Wpf.Infrastructure
{
    internal class EventBus
    {
        private readonly Eventing.EventBus _eventBus = new Eventing.EventBus();

        public void Publish<TEvent>(TEvent @event)
        {
            _eventBus.Publish(@event);
        }

        public void Subscribe<TEvent>(EventListener<TEvent> listener)
        {
            _eventBus.RegisterListener(listener);
        }
    }
}

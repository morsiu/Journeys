using Journeys.Event;

namespace Journeys.Client.Wpf.Infrastructure
{
    internal class EventBus
    {
        private readonly Event.EventBus _eventBus = new Event.EventBus();

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

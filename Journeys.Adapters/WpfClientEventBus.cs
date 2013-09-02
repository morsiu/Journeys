using System;
using Journeys.Client.Wpf;

namespace Journeys.Adapters
{
    public class WpfClientEventBus : IEventBus
    {
        private readonly Event.EventBus _eventBus;

        public WpfClientEventBus(Event.EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Publish<TEvent>(TEvent @event)
        {
            _eventBus.Publish(@event);
        }

        public void Subscribe<TEvent>(Action<TEvent> listener)
        {
            _eventBus.RegisterListener(new Event.EventListener<TEvent>(listener));
        }
    }
}

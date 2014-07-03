using System;
using Journeys.Client.Wpf;
using Implementation = Mors.Support.Events;

namespace Journeys.Adapters
{
    public class WpfClientEventBus : IEventBus
    {
        private readonly Implementation.IEventBus _eventBus;

        public WpfClientEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Publish<TEvent>(TEvent @event)
        {
            _eventBus.Publish(@event);
        }

        public void Subscribe<TEvent>(Action<TEvent> listener)
        {
            _eventBus.RegisterListener(new Implementation.EventListener<TEvent>(listener));
        }
    }
}

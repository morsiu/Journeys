using System.Collections.Generic;

namespace Journeys.Event
{
    internal class EventPublisher<TEvent>
    {
        private readonly List<EventListener<TEvent>> _listeners = new List<EventListener<TEvent>>();

        public void RegisterListener(EventListener<TEvent> listener)
        {
            _listeners.Add(listener);
        }

        public void Publish(TEvent @event)
        {
            foreach (var listener in _listeners)
            {
                listener(@event);
            }
        }
    }
}

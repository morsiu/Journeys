using Journeys.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Application.Events
{
    internal class EventBus
    {
        private readonly Dictionary<Type, object> _eventPublishers = new Dictionary<Type, object>();

        public void RegisterListener<TEvent>(EventListener<TEvent> listener)
        {
            var eventType = typeof(TEvent);
            if (!_eventPublishers.ContainsKey(eventType))
            {
                _eventPublishers.Add(eventType, new EventPublisher<TEvent>());
            }
            var publisher = (EventPublisher<TEvent>)_eventPublishers[eventType];
            publisher.RegisterListener(listener);
        }

        public void Publish<TEvent>(TEvent @event)
        {
            var eventType = typeof(TEvent);
            if (!_eventPublishers.ContainsKey(eventType))
            {
                return;
            }
            var publisher = (EventPublisher<TEvent>)_eventPublishers[eventType];
            publisher.Publish(@event);
        }
    }
}

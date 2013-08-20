using System;
using System.Collections.Generic;
using Journeys.Transactions;

namespace Journeys.Eventing
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, object> _eventPublishers = new Dictionary<Type, object>();

        public void RegisterListener<TEvent>(EventListener<TEvent> listener)
        {
            var publisher = GetPublisher<TEvent>();
            publisher.RegisterListener(listener);
        }

        public void Publish<TEvent>(TEvent @event)
        {
            var publisher = GetPublisher<TEvent>();
            publisher.Publish(@event);
        }

        private EventPublisher<TEvent> GetPublisher<TEvent>()
        {
            var eventType = typeof(TEvent);
            if (!_eventPublishers.ContainsKey(eventType))
            {
                _eventPublishers[eventType] = new EventPublisher<TEvent>();
            }
            var publisher = (EventPublisher<TEvent>)_eventPublishers[eventType];
            return publisher;
        }

        ITransacted<IEventBus> IProvideTransacted<IEventBus>.Lift()
        {
            return new TransactedEventBus(this);
        }
    }
}

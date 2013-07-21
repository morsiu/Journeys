using System;
using System.Collections.Generic;
using Journeys.Transactions;

namespace Journeys.Eventing
{
    public class EventBus : IEventBus, IProvideTransacted<IEventBus>
    {
        private readonly Dictionary<Type, object> _eventPublishers = new Dictionary<Type, object>();

        public void RegisterListener<TEvent>(EventListener<TEvent> listener)
        {
            var eventType = typeof(TEvent);
            if (!_eventPublishers.ContainsKey(eventType))
            {
                _eventPublishers[eventType] = new EventPublisher<TEvent>();
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

        ITransacted<IEventBus> IProvideTransacted<IEventBus>.Lift()
        {
            return new TransactedEventBus(this);
        }
    }
}

using System;
using System.Collections.Generic;
using Journeys.Domain.Infrastructure;
using Journeys.Application.Infrastructure.Transactions;

namespace Journeys.Application.Infrastructure.Events
{
    internal class EventBus : IEventBus, IProvideTransacted<IEventBus>
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

        public ITransacted<IEventBus> Escalate()
        {
            return new TransactedEventBus(this);
        }
    }
}

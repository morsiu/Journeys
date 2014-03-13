using Journeys.Domain.Infrastructure;
using Journeys.Transactions;
using System;
using System.Collections.Generic;

namespace Journeys.Domain.Test.Infrastructure
{
    internal class EventBusMock : IEventBus, ITransactional<IEventBus>
    {
        private readonly List<EventMatcher> _eventMatchers = new List<EventMatcher>();

        public void Publish<TEvent>(TEvent @event)
        {
            foreach (var eventMatcher in _eventMatchers)
            {
                eventMatcher.Add(@event);
            }
        }
        
        public EventMatcher Listen(Action action)
        {
            var matcher = new EventMatcher();
            _eventMatchers.Add(matcher);
            action();
            _eventMatchers.Remove(matcher);
            return matcher;
        }

        public ITransactional<IEventBus> Lift()
        {
            return this;
        }

        public IEventBus Object
        {
            get { return this; }
        }

        public void Abort()
        {
        }

        public void Commit()
        {
        }
    }
}

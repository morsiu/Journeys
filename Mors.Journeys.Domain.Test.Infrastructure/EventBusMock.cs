using System;
using System.Collections.Generic;
using Mors.AppPlatform.Common.Services;
using Mors.AppPlatform.Common.Transactions;

namespace Mors.Journeys.Domain.Test
{
    public sealed class EventBusMock : IEventBus
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

        public void RegisterListener<TEvent>(Action<TEvent> handler)
        {
            throw new NotImplementedException();
        }

        public ITransactional<IEventBus> Lift()
        {
            throw new NotImplementedException();
        }
    }
}

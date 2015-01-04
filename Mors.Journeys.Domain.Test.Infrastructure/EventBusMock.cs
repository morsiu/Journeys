using System;
using System.Collections.Generic;
using Mors.Journeys.Domain.Infrastructure;

namespace Mors.Journeys.Domain.Test
{
    public sealed class EventBus
    {
        private readonly List<EventMatcher> _eventMatchers = new List<EventMatcher>();

        public void Publish(object @event)
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
    }
}

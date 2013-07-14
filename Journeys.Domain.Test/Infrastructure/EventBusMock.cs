using Journeys.Domain.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Test.Infrastructure
{
    internal class EventBusMock : IEventBus
    {
        private List<EventMatcher> _eventMatchers = new List<EventMatcher>();

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
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Journeys.Domain.Test.Infrastructure
{
    public sealed class EventMatcher
    {
        private readonly List<object> _receivedEvents = new List<object>();

        public void Add(object @event)
        {
            _receivedEvents.Add(@event);
        }

        public void AssertReceivedOneEvent<TEvent>(Func<TEvent, bool> eventMatcher)
        {
            Assert.AreEqual(1, _receivedEvents.Count, "Expected one event and got {0}.", _receivedEvents.Count);
            var receivedEventType = _receivedEvents[0].GetType();
            Assert.AreEqual(typeof(TEvent), receivedEventType, "Expected event of type `{0}`, got `{1}`", typeof(TEvent), receivedEventType);
            var receivedEvent = (TEvent)_receivedEvents[0];
            Assert.AreEqual(true, eventMatcher(receivedEvent), "Received event did not match criteria.");
        }
    }
}

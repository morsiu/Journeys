using System;
using System.Collections.Generic;

namespace Journeys.EventSourcing
{
    internal class EventReplayer
    {
        private readonly Dictionary<Type, Action<object>> _eventHandlers = new Dictionary<Type,Action<object>>();

        public void Register<TEvent>(Action<TEvent> eventHandler)
        {
            _eventHandlers[typeof(TEvent)] = @event => eventHandler((TEvent)@event);
        }

        public void Replay(IEnumerable<object> events)
        {
            foreach (var @event in events)
            {
                var eventHandler = GetEventHandler(@event);
                eventHandler(@event);
            }
        }

        private Action<object> GetEventHandler(object @event)
        {
            var eventType = @event.GetType();
            if (!_eventHandlers.ContainsKey(eventType))
            {
                throw new InvalidOperationException(string.Format("Event store contains unsupported event of type `{0}`.", eventType));
            }
            return _eventHandlers[eventType];            
        }
    }
}

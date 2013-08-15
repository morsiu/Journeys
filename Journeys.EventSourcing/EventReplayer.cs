using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.EventSourcing
{
    public class EventReplayer
    {
        private readonly FileStream _eventStream; 
        private readonly XmlEventReader _eventReader;
        private readonly Dictionary<Type, Action<object>> _eventHandlers = new Dictionary<Type,Action<object>>();

        public EventReplayer(string fileName, IEnumerable<Type> knownEventTypes)
        {
            _eventStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            _eventReader = new XmlEventReader(_eventStream, knownEventTypes);
        }

        public void Register<TEvent>(Action<TEvent> eventHandler)
        {
            _eventHandlers[typeof(TEvent)] = @event => eventHandler((TEvent)@event);
        }

        public void Replay()
        {
            while (!_eventReader.IsAtEnd)
            {
                var @event = _eventReader.Read();
                var eventHandler = GetEventHandler(@event);
                eventHandler(@event);
            }
        }

        public void Close()
        {
            _eventStream.Close();
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

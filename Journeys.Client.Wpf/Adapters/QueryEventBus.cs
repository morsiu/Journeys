using System;
using Journeys.Query;

namespace Journeys.Client.Wpf.Adapters
{
    internal class QueryEventBus : IEventBus
    {
        private readonly Event.IEventBus _eventBus;
        public QueryEventBus(Event.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void RegisterListener<TEvent>(Action<TEvent> handler)
        {
            _eventBus.RegisterListener(new Event.EventListener<TEvent>(handler));
        }
    }
}

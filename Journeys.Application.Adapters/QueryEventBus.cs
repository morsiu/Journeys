using System;
using Implementation = Mors.Support.Events;

namespace Journeys.Application.Adapters
{
    public class QueryEventBus : Query.IEventBus
    {
        private readonly Implementation.IEventBus _eventBus;

        public QueryEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void RegisterListener<TEvent>(Action<TEvent> handler)
        {
            _eventBus.RegisterListener(new Implementation.EventListener<TEvent>(handler));
        }
    }
}

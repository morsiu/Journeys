using System;
using Implementation = Journeys.Support.Events;

namespace Journeys.Hosting.Adapters.Modules.Query
{
    public sealed class QueryEventBus : Application.Query.IEventBus
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

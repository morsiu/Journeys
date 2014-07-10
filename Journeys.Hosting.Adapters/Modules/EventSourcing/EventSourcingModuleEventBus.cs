using System;
using Journeys.Support.Transactions;
using Implementation = Journeys.Support.Events;

namespace Journeys.Hosting.Adapters.Modules.EventSourcing
{
    public sealed class EventSourcingModuleEventBus : Journeys.Support.EventSourcing.IEventBus
    {
        private readonly Implementation.IEventBus _eventBus;

        public EventSourcingModuleEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void RegisterListener<TEvent>(Action<TEvent> handler)
        {
            _eventBus.RegisterListener(new Implementation.EventListener<TEvent>(handler));
        }

        public ITransactional<Journeys.Support.EventSourcing.IEventBus> Lift()
        {
            return new EventSourcingModuleTransactedEventBus(_eventBus);
        }
    }
}

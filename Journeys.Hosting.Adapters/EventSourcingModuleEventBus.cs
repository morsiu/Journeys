using System;
using Mors.Support.Transactions;
using Implementation = Mors.Support.Events;

namespace Journeys.Application.Adapters
{
    public class EventSourcingModuleEventBus : Mors.Support.EventSourcing.IEventBus
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

        public ITransactional<Mors.Support.EventSourcing.IEventBus> Lift()
        {
            return new EventSourcingModuleTransactedEventBus(_eventBus);
        }
    }
}

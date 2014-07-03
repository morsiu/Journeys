using System;
using Mors.Support.Transactions;
using Implementation = Mors.Support.Events;

namespace Journeys.Application.Adapters
{
    public class EventSourcingEventBus : Mors.Support.EventSourcing.IEventBus
    {
        private readonly Implementation.IEventBus _eventBus;

        public EventSourcingEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void RegisterListener<TEvent>(Action<TEvent> handler)
        {
            _eventBus.RegisterListener(new Implementation.EventListener<TEvent>(handler));
        }

        public ITransactional<Mors.Support.EventSourcing.IEventBus> Lift()
        {
            return new EventSourcingTransactedEventBus(_eventBus);
        }
    }
}

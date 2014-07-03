using System;
using Mors.Support.EventSourcing;
using Mors.Support.Transactions;
using Implementation = Mors.Support.Events;

namespace Journeys.Adapters
{
    public class EventSourcingEventBus : IEventBus
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

        public ITransactional<IEventBus> Lift()
        {
            return new EventSourcingTransactedEventBus(_eventBus);
        }
    }
}

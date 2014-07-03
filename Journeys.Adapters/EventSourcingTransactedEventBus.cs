using System;
using Mors.Support.EventSourcing;
using Mors.Support.Transactions;
using Implementation = Mors.Support.Events;

namespace Journeys.Adapters
{
    internal class EventSourcingTransactedEventBus : IEventBus, ITransactional<IEventBus>
    {
        private ITransactional<Implementation.IEventBus> _eventBus;

        public EventSourcingTransactedEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus.Lift();
        }

        public void RegisterListener<TEvent>(Action<TEvent> handler)
        {
            _eventBus.Object.RegisterListener(new Implementation.EventListener<TEvent>(handler));
        }

        public ITransactional<IEventBus> Lift()
        {
            return this;
        }

        public IEventBus Object
        {
            get { return this; }
        }

        public void Abort()
        {
            _eventBus.Abort();
        }

        public void Commit()
        {
            _eventBus.Commit();
        }
    }
}

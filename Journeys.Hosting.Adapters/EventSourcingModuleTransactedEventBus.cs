using System;
using Mors.Support.Transactions;
using Implementation = Mors.Support.Events;

namespace Journeys.Application.Adapters
{
    internal class EventSourcingModuleTransactedEventBus : Mors.Support.EventSourcing.IEventBus, ITransactional<Mors.Support.EventSourcing.IEventBus>
    {
        private ITransactional<Implementation.IEventBus> _eventBus;

        public EventSourcingModuleTransactedEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus.Lift();
        }

        public void RegisterListener<TEvent>(Action<TEvent> handler)
        {
            _eventBus.Object.RegisterListener(new Implementation.EventListener<TEvent>(handler));
        }

        public ITransactional<Mors.Support.EventSourcing.IEventBus> Lift()
        {
            return this;
        }

        public Mors.Support.EventSourcing.IEventBus Object
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

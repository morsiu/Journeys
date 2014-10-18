using System;
using Journeys.Support.Transactions;
using Implementation = Journeys.Support.Events;

namespace Journeys.Hosting.Adapters.Modules.EventSourcing
{
    internal sealed class EventSourcingModuleTransactedEventBus : Journeys.Support.EventSourcing.IEventBus, ITransactional<Journeys.Support.EventSourcing.IEventBus>
    {
        private readonly ITransactional<Implementation.IEventBus> _eventBus;

        public EventSourcingModuleTransactedEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus.Lift();
        }

        public void RegisterListener<TEvent>(Action<TEvent> handler)
        {
            _eventBus.Object.RegisterListener(new Implementation.EventListener<TEvent>(handler));
        }

        public ITransactional<Journeys.Support.EventSourcing.IEventBus> Lift()
        {
            return this;
        }

        public Journeys.Support.EventSourcing.IEventBus Object
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

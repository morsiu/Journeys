using System;
using Journeys.Transactions;
using Journeys.EventSourcing;

namespace Journeys.Adapters
{
    internal class EventSourcingTransactedEventBus : IEventBus, ITransactional<IEventBus>
    {
        private ITransactional<Event.IEventBus> _eventBus;

        public EventSourcingTransactedEventBus(Event.IEventBus eventBus)
        {
            _eventBus = eventBus.Lift();
        }

        public void RegisterListener<TEvent>(Action<TEvent> handler)
        {
            _eventBus.Object.RegisterListener<TEvent>(new Event.EventListener<TEvent>(handler));
        }

        public Domain.Infrastructure.IEventBus ForDomain()
        {
            return new DomainEventBusAdapter(_eventBus.Object);
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

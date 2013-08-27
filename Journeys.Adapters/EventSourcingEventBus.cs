using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journeys.EventSourcing;
using Journeys.Transactions;

namespace Journeys.Adapters
{
    public class EventSourcingEventBus : IEventBus
    {
        private readonly Event.IEventBus _eventBus;

        public EventSourcingEventBus(Event.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void RegisterListener<TEvent>(Action<TEvent> handler)
        {
            _eventBus.Publish(handler);
        }

        public Domain.IEventBus ForDomain()
        {
            return new DomainEventBusAdapter(_eventBus);
        }

        public ITransactional<IEventBus> Lift()
        {
            return new EventSourcingTransactedEventBus(_eventBus);
        }
    }
}

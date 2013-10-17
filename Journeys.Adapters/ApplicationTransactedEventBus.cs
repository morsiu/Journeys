using Journeys.Application;
using Journeys.Transactions;

namespace Journeys.Adapters
{
    internal class ApplicationTransactedEventBus : IEventBus, ITransactional<IEventBus>
    {
        private ITransactional<Event.IEventBus> _eventBus;

        public ApplicationTransactedEventBus(Event.IEventBus eventBus)
        {
            _eventBus = eventBus.Lift();
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

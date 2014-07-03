using Journeys.Application;
using Mors.Support.Transactions;
using Implementation = Mors.Support.Events;

namespace Journeys.Adapters
{
    internal class ApplicationTransactedEventBus : IEventBus, ITransactional<IEventBus>
    {
        private ITransactional<Implementation.IEventBus> _eventBus;

        public ApplicationTransactedEventBus(Implementation.IEventBus eventBus)
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

using Journeys.Application;
using Mors.Support.Transactions;
using Implementation = Mors.Support.Events;

namespace Journeys.Adapters
{
    public class ApplicationEventBus : IEventBus
    {
        private readonly Implementation.IEventBus _eventBus;

        public ApplicationEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Domain.Infrastructure.IEventBus ForDomain()
        {
            return new DomainEventBusAdapter(_eventBus);
        }

        public ITransactional<IEventBus> Lift()
        {
            return new ApplicationTransactedEventBus(_eventBus);
        }
    }
}

using Journeys.Application;
using Journeys.Transactions;

namespace Journeys.Adapters
{
    public class ApplicationEventBus : IEventBus
    {
        private readonly Event.IEventBus _eventBus;

        public ApplicationEventBus(Event.IEventBus eventBus)
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

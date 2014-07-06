using Journeys.Application.Command;
using Journeys.Hosting.Adapters.Modules.Domain;
using Journeys.Support.Transactions;
using Implementation = Journeys.Support.Events;

namespace Journeys.Hosting.Adapters.Modules.Command
{
    internal class CommandTransactedEventBus : IEventBus, ITransactional<IEventBus>
    {
        private ITransactional<Implementation.IEventBus> _eventBus;

        public CommandTransactedEventBus(Implementation.IEventBus eventBus)
        {
            _eventBus = eventBus.Lift();
        }

        public Journeys.Domain.Infrastructure.IEventBus ForDomain()
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

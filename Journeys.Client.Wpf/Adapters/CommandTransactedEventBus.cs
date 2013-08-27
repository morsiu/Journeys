using Journeys.Command;
using Journeys.Transactions;

namespace Journeys.Client.Wpf.Adapters
{
    internal class CommandTransactedEventBus : IEventBus, ITransactional<IEventBus>
    {
        private ITransactional<Event.IEventBus> _eventBus;

        public CommandTransactedEventBus(Event.IEventBus eventBus)
        {
            _eventBus = eventBus.Lift();
        }

        public Domain.IEventBus ForDomain()
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

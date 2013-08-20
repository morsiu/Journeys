using System;
using System.Collections.Generic;
using Journeys.Transactions;

namespace Journeys.Eventing
{
    public class TransactedEventBus : IEventBus, ITransactional<IEventBus>
    {
        private readonly IEventBus _eventBus;
        private readonly List<Action> _publishments = new List<Action>();

        public TransactedEventBus(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Publish<TEvent>(TEvent @event)
        {
            _publishments.Add(() => _eventBus.Publish(@event));
        }

        public void Abort()
        {
            _publishments.Clear();
        }

        public void Commit()
        {
            foreach (var publishment in _publishments)
            {
                publishment();
            }
            _publishments.Clear();
        }

        public IEventBus Object
        {
            get { return this; }
        }

        public ITransactional<IEventBus> Lift()
        {
            return this;
        }
    }
}

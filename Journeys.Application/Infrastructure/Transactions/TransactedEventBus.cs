using System.Collections.Generic;
using Journeys.Domain.Infrastructure;
using System;

namespace Journeys.Application.Infrastructure.Transactions
{
    public class TransactedEventBus : IEventBus, ITransacted<IEventBus>
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
        }

        public IEventBus Object
        {
            get { return this; }
        }
    }
}

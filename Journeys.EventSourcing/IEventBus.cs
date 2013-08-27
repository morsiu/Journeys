using System;
using Journeys.Transactions;

namespace Journeys.EventSourcing
{
    public interface IEventBus : IProvideTransactional<IEventBus>
    {
        void RegisterListener<TEvent>(Action<TEvent> handler);

        Domain.IEventBus ForDomain();
    }
}

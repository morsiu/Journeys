using System;
using Journeys.Support.Transactions;

namespace Journeys.Support.EventSourcing
{
    public interface IEventBus : IProvideTransactional<IEventBus>
    {
        void RegisterListener<TEvent>(Action<TEvent> handler);
    }
}
